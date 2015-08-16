using System;
using Lucene.Net.Database.Builders;
using Lucene.Net.Database.Tests.Models;
using Xunit;

namespace Lucene.Net.Database.Tests
{
	// This project can output the Class library as a NuGet Package.
	// To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
	public class WhenIBuildADocument
	{

		[Fact]
		public void ThenIWantTheFieldsToBeAddedToTheDocument()
		{
			// Arrange
			var book = new Book
			{
				Title = "Harry Potter and the Sorcerers Stone",
				Author = new Author
				{
					FirstName = "J.K.",
					LastName = "Rowling",
					Age = 50
				}
			};
			var builder = new ReflectionDocumentBuilder(book);

			// Act
			var document = builder.Build();

			// Assert
			Assert.Equal(book.Title, document.Get("Title"));
			Assert.Equal(book.Author.FirstName, document.Get("Author.FirstName"));
			Assert.Equal(book.Author.LastName, document.Get("Author.LastName"));
			Assert.Equal(book.Author.Age.ToString(), document.Get("Author.Age"));
		}

		[Fact(Skip = "Incomplete")]
		public void Foo()
		{
			// Arrange
			var connection = new LuceneConnection
			{
				InMemory = true
			};
			var input = new Book
			{
				Title = "Harry Potter and the Sorcerers Stone",
				Author = new Author
				{
					FirstName = "J.K.",
					LastName = "Rowling",
					Age = 50
				}
			};

			// Act
			using (var session = connection.OpenSession())
			{
				session.Add(input);
				session.Commit();

				var output = session.Load<Book>("5");

				// Assert
			}
		}
	}

	public class LuceneConnection
	{
		public String DirectoryPath { get; set; }

		public Boolean InMemory { get; set; }

		public ISession OpenSession()
		{
			return new Session();
		}
	}

	public interface ISession : IDisposable
	{
		void Add<T>(T value);

		T Load<T>(String id);

		void Commit();

	}

	public class Session : ISession
	{
		public void Add<T>(T value)
		{
			throw new NotImplementedException();
		}

		public T Load<T>(String id)
		{
			throw new NotImplementedException();
		}

		public void Commit()
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}
