using System;
using System.Linq;
using System.Data.Common;
using System.Diagnostics;

using Microsoft.EntityFrameworkCore;

using Xamarin.Forms;

namespace EFCoreNullable
{
    public partial class EFCoreNullablePage : ContentPage
    {
        private static IFileHelper fileHelper = DependencyService.Get<IFileHelper>();
        private DatabaseContext database = null;

        public EFCoreNullablePage()
        {
            InitializeComponent();

            // Set the final bool to false to retain database files between runs.
            string path = fileHelper.GetPath("store.db", true);
            Debug.WriteLine(path);

            database = new DatabaseContext(path);
            database.Database.EnsureCreated();
        }

        protected override void OnAppearing()
		{
            Nullables();
			NullableMultiFields();
            MultiFields();
        }

        private void Nullables()
		{
			Debug.WriteLine("// ----- Nullables ----- //");
			Debug.WriteLine("");

			try
			{
				Foo[] foos = database
					.Set<Foo>()
					.ToArray();

                Debug.WriteLine("First Foos length: " + foos.Length);
			}
			catch (Exception e)
			{
				Debug.WriteLine("First Foo load: " + e.Message);
			}

			try
			{
				database.Add(new Foo()
				{
					Name = "Foo 1"
				});
				database.SaveChanges();

				Debug.WriteLine("Foo without related Bar created");
			}
			catch (Exception e)
			{
				Debug.WriteLine("Create Foo without related Bar: " + e.Message);
			}

			try
			{
				Bar bar = new Bar()
				{
					Name = "Bar 1"
				};
				database.Add(bar);
				database.Add(new Foo()
				{
					Name = "Foo 2",
					Bar = bar

				});
				database.SaveChanges();

				Debug.WriteLine("Foo with related Bar created");
			}
			catch (Exception e)
			{
				Debug.WriteLine("Create Foo with related Bar: " + e.Message);
			}

			try
			{
				using (DbConnection connection = database.Database.GetDbConnection())
				using (DbCommand command = connection.CreateCommand())
				{
					connection.Open();

					command.CommandText = "insert into Bar (Name) values ('Bar 2'); select last_insert_rowid();";
					object barId = command.ExecuteScalar();

					command.CommandText = $"insert into Foo (Name, BarId) values ('Foo 2', {barId});";
					command.ExecuteNonQuery();
				}

				Debug.WriteLine("Foo with related Bar manually created");
			}
			catch (Exception e)
			{
				Debug.WriteLine("Manual Create: " + e.Message);
			}

			try
			{
				Foo[] foos = database
					.Set<Foo>()
					.ToArray();

				Debug.WriteLine("Second Foos length: " + foos.Length);
			}
			catch (Exception e)
			{
				Debug.WriteLine("Second Foo load after manual create: " + e.Message);
			}
        }

        private void NullableMultiFields()
		{
			Debug.WriteLine("");
			Debug.WriteLine("// ----- NullableMultiFields ----- //");
			Debug.WriteLine("");

			try
			{
				BigFoo[] foos = database
					.Set<BigFoo>()
					.ToArray();

				Debug.WriteLine("First BigFoos length: " + foos.Length);
			}
			catch (Exception e)
			{
				Debug.WriteLine("First BigFoos load: " + e.Message);
			}

			try
			{
				database.Add(new BigFoo()
				{
					Name = "Big Foo 1"
				});
				database.SaveChanges();

				Debug.WriteLine("BigFoo without related Foo created");
			}
			catch (Exception e)
			{
				Debug.WriteLine("Create BigFoo without related Foo: " + e.Message);
			}

			try
			{
				Foo foo = new Foo()
				{
					Name = "Little Foo 1"
				};
				database.Add(foo);
				database.Add(new BigFoo()
				{
					Name = "Foo 2",
                    LittleFoo = foo
				});
				database.SaveChanges();

				Debug.WriteLine("BigFoo with related Foo created");
			}
			catch (Exception e)
			{
				Debug.WriteLine("Create BigFoo with related Foo: " + e.Message);
			}

			try
			{
				using (DbConnection connection = database.Database.GetDbConnection())
				using (DbCommand command = connection.CreateCommand())
				{
					connection.Open();

					command.CommandText = "insert into Foo (Name) values ('Little Foo 2'); select last_insert_rowid();";
                    object fooId = command.ExecuteScalar();

					command.CommandText = $"insert into BigFoo (Name, LittleFooId) values ('Big Foo 2', {fooId});";
					command.ExecuteNonQuery();
				}

				Debug.WriteLine("BigFoo with related Foo manually created");
			}
			catch (Exception e)
			{
				Debug.WriteLine("Manual Create: " + e.Message);
			}

			try
			{
				BigFoo[] foos = database
					.Set<BigFoo>()
					.ToArray();

				Debug.WriteLine("Second BigFoos length: " + foos.Length);
			}
			catch (Exception e)
			{
				Debug.WriteLine("Second BigFoo load after manual create: " + e.Message);
			}
        }

        private void MultiFields()
		{
			Debug.WriteLine("");
			Debug.WriteLine("// ----- MultiFields ----- //");
			Debug.WriteLine("");

			try
			{
				TenFields[] tens = database
					.Set<TenFields>()
					.ToArray();

				Debug.WriteLine("TenFields length: " + tens.Length);
			}
			catch (Exception e)
			{
				Debug.WriteLine("TenFields load: " + e.Message);
			}

			try
			{
                database.Add(new TenFields()
				{
					Field2 = "Field2",
                    Field3 = "Field3",
                    Field4 = "Field4",
                    Field5 = "Field5",
                    Field6 = "Field6",
                    Field7 = "Field7",
                    Field8 = "Field8",
                    Field9 = "Field9",
                    Field10 = "Field10"
				});
				database.SaveChanges();

				Debug.WriteLine("TenFields created");
			}
			catch (Exception e)
			{
				Debug.WriteLine("Create TenFields: " + e.Message);
			}

			try
			{
				TenFields[] tens = database
					.Set<TenFields>()
					.ToArray();

				Debug.WriteLine("Second TenFields length: " + tens.Length);
			}
			catch (Exception e)
			{
				Debug.WriteLine("Second TenFields load: " + e.Message);
			}

			try
			{
                FifteenFields[] fifteens = database
					.Set<FifteenFields>()
					.ToArray();
                
				Debug.WriteLine("FifteenFields length: " + fifteens.Length);
			}
			catch (Exception e)
			{
				Debug.WriteLine("FifteenFields load: " + e.Message);
			}

			try
			{
				database.Add(new FifteenFields()
				{
					Field2 = "Field2",
					Field3 = "Field3",
					Field4 = "Field4",
					Field5 = "Field5",
					Field6 = "Field6",
					Field7 = "Field7",
					Field8 = "Field8",
					Field9 = "Field9",
					Field10 = "Field10",
					Field11 = "Field11",
					Field12 = "Field12",
					Field13 = "Field13",
					Field14 = "Field14",
					Field15 = "Field15"
				});
				database.SaveChanges();

				Debug.WriteLine("FifteenFields created");
			}
			catch (Exception e)
			{
				Debug.WriteLine("Create FifteenFields: " + e.Message);
			}

			try
			{
				FifteenFields[] fifteens = database
					.Set<FifteenFields>()
					.ToArray();

				Debug.WriteLine("Second FifteenFields length: " + fifteens.Length);
			}
			catch (Exception e)
			{
				Debug.WriteLine("Second FifteenFields load: " + e.Message);
			}

			try
			{
				TwentyFields[] twenties = database
					.Set<TwentyFields>()
					.ToArray();

				Debug.WriteLine("TwentyFields length: " + twenties.Length);
			}
			catch (Exception e)
			{
				Debug.WriteLine("TwentyFields load: " + e.Message);
			}

			try
			{
                database.Add(new TwentyFields()
				{
					Field2 = "Field2",
					Field3 = "Field3",
					Field4 = "Field4",
					Field5 = "Field5",
					Field6 = "Field6",
					Field7 = "Field7",
					Field8 = "Field8",
					Field9 = "Field9",
					Field10 = "Field10",
					Field11 = "Field11",
					Field12 = "Field12",
					Field13 = "Field13",
					Field14 = "Field14",
					Field15 = "Field15",
					Field16 = "Field16",
					Field17 = "Field17",
					Field18 = "Field18",
					Field19 = "Field19",
					Field20 = "Field20"
				});
				database.SaveChanges();

				Debug.WriteLine("TwentyFields created");
			}
			catch (Exception e)
			{
				Debug.WriteLine("Create TwentyFields: " + e.Message);
			}

			try
			{
				TwentyFields[] twenties = database
					.Set<TwentyFields>()
					.ToArray();

				Debug.WriteLine("Second TwentyFields length: " + twenties.Length);
			}
			catch (Exception e)
			{
				Debug.WriteLine("Second TwentyFields load: " + e.Message);
			}
        }
    }
}
