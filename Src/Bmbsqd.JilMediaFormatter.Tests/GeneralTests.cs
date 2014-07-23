using System.IO;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Bmbsqd.JilMediaFormatter.Tests
{
	[TestFixture]
	public class GeneralTests
	{

		public class SomeValues
		{
			public int Id { get; set; }
			public string Name { get; set; }
		}

		[Test]
		public async Task Hello()
		{
			var f = new JilMediaTypeFormatter();

			var value = new SomeValues {
				Id = 10,
				Name = "Elon Musk"
			};

			var stream = new MemoryStream();
			await f.WriteToStreamAsync( value.GetType(), value, stream, null, null );

			stream.Position = 0;

			var result = (SomeValues)await f.ReadFromStreamAsync( typeof( SomeValues ), stream, null, null );

			Assert.That( result, Is.Not.Null );
			Assert.That( result.Id, Is.EqualTo( value.Id ) );
			Assert.That( result.Name, Is.EqualTo( value.Name ) );
		}
	}
}
