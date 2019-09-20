using FoodTracker;
using FoodTracker.Models;
using FoodTracker.Models.ViewModels;
using FoodTrackerTest.Test;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FoodTrackerTest
{
	public class ImportExportNomralizeIDTest
	{				
		[Fact]
		public void TestNormalize_Categ_AndDepedencies()
		{
			PortDBViewModel originalDBV = new PortDBViewModel()
			{
				Categories = new List<TestCategory>
				{
					new TestCategory { Id= 3, Name="Elso"},
					new TestCategory { Id = 6, Name = "Második" },
					new TestCategory { Id= 7, Name="Harmadik"}
				},
				Subcategories = new List<TestSubcategory>
				{
					new TestSubcategory { Id=1, Name = "Elso", CategoryId=3}
				},
				Foods = new List<TestFood>
				{
					new TestFood { ID =2, Name = "Elso", CategoryId=3, SubCategoryId=1}
				}
			};

			PortDBViewModel normalizedDBM = Testing.normalizeIDs(originalDBV);

			PortDBViewModel expectedDBM = new PortDBViewModel()
			{
				Categories = new List<TestCategory>
				{
					new TestCategory { Id= 0, Name="Elso"},
					new TestCategory { Id =0, Name = "Második" },
					new TestCategory { Id= 0, Name="Harmadik"}
				},
				Subcategories = new List<TestSubcategory>
				{
					new TestSubcategory { Id=0, Name = "Elso", CategoryId=1}
				},
				Foods = new List<TestFood>
				{
					new TestFood { ID =0, Name = "Elso", CategoryId=1, SubCategoryId=1}
				}
			};
			Assert.Equal(expectedDBM.Categories, normalizedDBM.Categories);
			Assert.Equal(expectedDBM.Subcategories, normalizedDBM.Subcategories);
			Assert.Equal(expectedDBM.Foods, normalizedDBM.Foods);
		}

		[Fact]
		public void TestNormalize_Categ_Subcateg_AndDependencies()
		{
			PortDBViewModel originalDBV = new PortDBViewModel()
			{
				Categories = new List<TestCategory>
				{
					new TestCategory { Id= 1, Name="Elso"},
				},
				Subcategories = new List<TestSubcategory>
				{
					new TestSubcategory { Id=2, Name = "Elso", CategoryId=1},
					new TestSubcategory { Id=5, Name = "Masodik", CategoryId=1},
					new TestSubcategory { Id=6, Name = "Harmadik", CategoryId=1},
				},
				Foods = new List<TestFood>
				{
					new TestFood { ID =1, Name = "Elso", CategoryId=1, SubCategoryId=5},
				}
			};

			PortDBViewModel normalizedDBM = Testing.normalizeIDs(originalDBV);

			PortDBViewModel expectedDBM = new PortDBViewModel()
			{
				Categories = new List<TestCategory>
				{
					new TestCategory { Id= 0, Name="Elso"},
				},
				Subcategories = new List<TestSubcategory>
				{
					new TestSubcategory { Id=0, Name = "Elso", CategoryId=1},
					new TestSubcategory { Id=0, Name = "Masodik", CategoryId=1},
					new TestSubcategory { Id=0, Name = "Harmadik", CategoryId=1},
				},
				Foods = new List<TestFood>
				{
					new TestFood { ID =0, Name = "Elso", CategoryId=1, SubCategoryId=2},
				}
			};
			Assert.Equal(expectedDBM.Categories, normalizedDBM.Categories);
			Assert.Equal(expectedDBM.Subcategories, normalizedDBM.Subcategories);
			Assert.Equal(expectedDBM.Foods, normalizedDBM.Foods);
		}



		[Fact]
		public void TestNormalize_Categ_SubCateg_Food_AndDependencies()
		{
			PortDBViewModel originalDBV = new PortDBViewModel()
			{
				Categories = new List<TestCategory>
				{
					new TestCategory { Id= 3, Name="Elso"},
					new TestCategory { Id = 4, Name = "Második" },
					new TestCategory { Id= 5, Name="Harmadik"}
				},
				Subcategories = new List<TestSubcategory>
				{
					new TestSubcategory { Id=2, Name = "Elso", CategoryId=4},
					new TestSubcategory { Id=6, Name = "Masodik", CategoryId=5},
					new TestSubcategory { Id=9, Name = "Harmadik", CategoryId=3},
				},
				Foods = new List<TestFood>
				{
					new TestFood { ID =3, Name = "Elso", CategoryId=3, SubCategoryId=9},
					new TestFood { ID =6, Name = "Masodik", CategoryId=5, SubCategoryId=2},
					new TestFood { ID =7, Name = "Harmadik", CategoryId=4, SubCategoryId=6}
				}
			};

			PortDBViewModel normalizedDBM = Testing.normalizeIDs(originalDBV);

			PortDBViewModel expectedDBM = new PortDBViewModel()
			{
				Categories = new List<TestCategory>
				{
					new TestCategory { Id= 0, Name="Elso"},
					new TestCategory { Id =0, Name = "Második" },
					new TestCategory { Id= 0, Name="Harmadik"}
				},
				Subcategories = new List<TestSubcategory>
				{
					new TestSubcategory { Id=0, Name = "Elso", CategoryId=2},
					new TestSubcategory { Id=0, Name = "Masodik", CategoryId=3},
					new TestSubcategory { Id=0, Name = "Harmadik", CategoryId=1},
				},
				Foods = new List<TestFood>
				{
					new TestFood { ID =0, Name = "Elso", CategoryId=1, SubCategoryId=3},
					new TestFood { ID =0, Name = "Masodik", CategoryId=3, SubCategoryId=1},
					new TestFood { ID =0, Name = "Harmadik", CategoryId=2, SubCategoryId=2}
				}
			};

			Assert.Equal(expectedDBM.Categories, normalizedDBM.Categories);
			Assert.Equal(expectedDBM.Subcategories, normalizedDBM.Subcategories);
			Assert.Equal(expectedDBM.Foods, normalizedDBM.Foods);
		}

		[Fact]
		public void TestNormalize_Mes2_Categ_SubCateg_Food_AndDependencies()
		{
			PortDBViewModel originalDBV = new PortDBViewModel()
			{
				Categories = new List<TestCategory>
				{
					new TestCategory { Id= 7, Name="Fagyasztó"},
					new TestCategory { Id = 8, Name = "Spejz" },
					new TestCategory { Id= 9, Name="Hűtő"}
				},
				Subcategories = new List<TestSubcategory>
				{
					new TestSubcategory { Id=2, Name = "Húsok", CategoryId=9},
					new TestSubcategory { Id=5, Name = "Tejtemék", CategoryId=9},
					new TestSubcategory { Id=6, Name = "Zöldség", CategoryId=9},
					new TestSubcategory { Id=7, Name = "Mirelit Gyümölcs", CategoryId=7},
					new TestSubcategory { Id=8, Name = "Félkész ételek", CategoryId=7},
					new TestSubcategory { Id=10, Name = "Konzerv", CategoryId=8},
					new TestSubcategory { Id=11, Name = "Tartós zöldség", CategoryId=8},
					new TestSubcategory { Id=13, Name = "Olajok, mártások, szószok", CategoryId=8},
					new TestSubcategory { Id=14, Name = "Gyümölcs", CategoryId=9},
					new TestSubcategory { Id=15, Name = "Zöldség", CategoryId=7},
					new TestSubcategory { Id=16, Name = "Gabonafélék", CategoryId=8},
					new TestSubcategory { Id=17, Name = "Fűszerek", CategoryId=8},
					new TestSubcategory { Id=18, Name = "Disznósajt", CategoryId=9},

				},
				Foods = new List<TestFood>
				{
					new TestFood { ID =10, Name = "Kolbász", CategoryId=9, SubCategoryId=2},
					new TestFood { ID =11, Name = "Padlizsán", CategoryId=9, SubCategoryId=6},
					new TestFood { ID =12, Name = "barack", CategoryId=9, SubCategoryId=14},
					new TestFood { ID =13, Name = "Rántott sajt", CategoryId=7, SubCategoryId=8},
					new TestFood { ID =14, Name = "Kelbimbó", CategoryId=7, SubCategoryId=15},
					new TestFood { ID =15, Name = "Dobozos Tej", CategoryId=9, SubCategoryId=5},
					new TestFood { ID =16, Name = "Sajt", CategoryId=9, SubCategoryId=5},
					new TestFood { ID =17, Name = "Kenyér", CategoryId=8, SubCategoryId=16},
					new TestFood { ID =18, Name = "Kifli", CategoryId=8, SubCategoryId=16},
					new TestFood { ID =19, Name = "OIiva olaj", CategoryId=8, SubCategoryId=13},
					new TestFood { ID =20, Name = "Túró Rudi", CategoryId=9, SubCategoryId=5},
					new TestFood { ID =21, Name = "Meggy", CategoryId=7, SubCategoryId=7},
					new TestFood { ID =22, Name = "Szilva", CategoryId=7, SubCategoryId=7},
					new TestFood { ID =23, Name = "Zalai", CategoryId=9, SubCategoryId=2},
				}
			};

			PortDBViewModel normalizedDBM = Testing.normalizeIDs(originalDBV);

			PortDBViewModel expectedDBM = new PortDBViewModel()
			{
				Categories = new List<TestCategory>
				{
					new TestCategory { Id= 0, Name="Fagyasztó"},
					new TestCategory { Id = 0, Name = "Spejz" },
					new TestCategory { Id= 0, Name="Hűtő"}
				},
				Subcategories = new List<TestSubcategory>
				{
					new TestSubcategory { Id=0, Name = "Húsok", CategoryId=3},
					new TestSubcategory { Id=0, Name = "Tejtemék", CategoryId=3},
					new TestSubcategory { Id=0, Name = "Zöldség", CategoryId=3},
					new TestSubcategory { Id=0, Name = "Mirelit Gyümölcs", CategoryId=1},
					new TestSubcategory { Id=0, Name = "Félkész ételek", CategoryId=1},
					new TestSubcategory { Id=0, Name = "Konzerv", CategoryId=2},
					new TestSubcategory { Id=0, Name = "Tartós zöldség", CategoryId=2},
					new TestSubcategory { Id=0, Name = "Olajok, mártások, szószok", CategoryId=2},
					new TestSubcategory { Id=0, Name = "Gyümölcs", CategoryId=3},
					new TestSubcategory { Id=0, Name = "Zöldség", CategoryId=1},
					new TestSubcategory { Id=0, Name = "Gabonafélék", CategoryId=2},
					new TestSubcategory { Id=0, Name = "Fűszerek", CategoryId=2},
					new TestSubcategory { Id=0, Name = "Disznósajt", CategoryId=3},

				},
				Foods = new List<TestFood>
				{
					new TestFood { ID =0, Name = "Kolbász", CategoryId=3, SubCategoryId=1},
					new TestFood { ID =0, Name = "Padlizsán", CategoryId=3, SubCategoryId=3},
					new TestFood { ID =0, Name = "barack", CategoryId=3, SubCategoryId=9},
					new TestFood { ID =0, Name = "Rántott sajt", CategoryId=1, SubCategoryId=5},
					new TestFood { ID =0, Name = "Kelbimbó", CategoryId=1, SubCategoryId=10},
					new TestFood { ID =0, Name = "Dobozos Tej", CategoryId=3, SubCategoryId=2},
					new TestFood { ID =0, Name = "Sajt", CategoryId=3, SubCategoryId=2},
					new TestFood { ID =0, Name = "Kenyér", CategoryId=2, SubCategoryId=11},
					new TestFood { ID =0, Name = "Kifli", CategoryId=2, SubCategoryId=11},
					new TestFood { ID =0, Name = "OIiva olaj", CategoryId=2, SubCategoryId=8},
					new TestFood { ID =0, Name = "Túró Rudi", CategoryId=3, SubCategoryId=2},
					new TestFood { ID =0, Name = "Meggy", CategoryId=1, SubCategoryId=4},
					new TestFood { ID =0, Name = "Szilva", CategoryId=1, SubCategoryId=4},
					new TestFood { ID =0, Name = "Zalai", CategoryId=3, SubCategoryId=1},
				}
			};
			Assert.Equal(expectedDBM.Categories, normalizedDBM.Categories);
			Assert.Equal(expectedDBM.Subcategories, normalizedDBM.Subcategories);
			Assert.Equal(expectedDBM.Foods, normalizedDBM.Foods);
		}
	}
}
