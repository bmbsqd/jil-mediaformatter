using System;
using System.Collections.Concurrent;
using System.IO;
using System.Reflection;
using Jil;

namespace Bmbsqd.JilMediaFormatter
{
	static class TypedDeserializers
	{
		private static readonly ConcurrentDictionary<Type, Func<TextReader, Options, object>> _methods;
		private static readonly MethodInfo _method = typeof( JSON ).GetMethod( "Deserialize", new[] { typeof( TextReader ), typeof( Options ) } );

		static TypedDeserializers()
		{
			_methods = new ConcurrentDictionary<Type, Func<TextReader, Options, object>>();
		}

		public static Func<TextReader, Options, object> GetTyped( Type type )
		{
			return _methods.GetOrAdd( type, CreateDelegate );
		}

		private static Func<TextReader, Options, object> CreateDelegate( Type type )
		{
			return (Func<TextReader, Options, object>)_method
				.MakeGenericMethod( type )
				.CreateDelegate( typeof( Func<TextReader, Options, object> ) );
		}
	}
}