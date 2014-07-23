using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Jil;

namespace Bmbsqd.JilMediaFormatter
{
	public class JilMediaTypeFormatter : MediaTypeFormatter
	{
		private static readonly MediaTypeHeaderValue _applicationJsonMediaType = new MediaTypeHeaderValue( "application/json" );
		private static readonly MediaTypeHeaderValue _textJsonMediaType = new MediaTypeHeaderValue( "text/json" );
		private static readonly Task<bool> _done = Task.FromResult( true );

		private readonly Options _options;

		public JilMediaTypeFormatter( Options options )
		{
			_options = options;
			SupportedMediaTypes.Add( _applicationJsonMediaType );
			SupportedMediaTypes.Add( _textJsonMediaType );

			SupportedEncodings.Add( new UTF8Encoding( false, true ) );
			SupportedEncodings.Add( new UnicodeEncoding( false, true, true ) );
		}

		public JilMediaTypeFormatter() : this( GetDefaultOptions() ) { }

		private static Options GetDefaultOptions()
		{
			return new Options();
		}

		public override bool CanReadType( Type type )
		{
			return true;
		}

		public override bool CanWriteType( Type type )
		{
			return true;
		}

		public override Task<object> ReadFromStreamAsync( Type type, Stream input, HttpContent content, IFormatterLogger formatterLogger )
		{
			var reader = new StreamReader( input );
			var deserialize = TypedDeserializers.GetTyped( type );
			var result = deserialize( reader, _options );
			return Task.FromResult( result );
		}

		public override Task WriteToStreamAsync( Type type, object value, Stream output, HttpContent content, TransportContext transportContext )
		{
			var writer = new StreamWriter( output );
			JSON.Serialize( value, writer, _options );
			writer.Flush();
			return _done;
		}
	}
}
