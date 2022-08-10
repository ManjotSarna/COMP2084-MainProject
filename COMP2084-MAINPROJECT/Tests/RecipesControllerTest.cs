using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using COMP2084_MAINPROJECT.Controllers;
using COMP2084_MAINPROJECT.Data;
using COMP2084_MAINPROJECT.Models;
using Xunit;

namespace COMP2084_MAINPROJECT.Tests

{
    public class RecipesControllerTest
    {

        [Fact]
        public async Task Index_Returns_ViewResult() {
            using(var testDb = new ApplicationDbContext(this.GetTestDbOpts()))
            {
                var testCtrl = new RecipesController(testDb);
                var fakeRecipes = MakeFakeRecipes(3);

                foreach(var recipe in fakeRecipes)
                {
                    var res = await testCtrl.Create(recipe);
                    var resVr = Assert.IsType<RedirectToActionResult>(res);
                    Assert.Equal("Index", resVr.ActionName);
                }

                var idxRes = await testCtrl.Index();
                var idxResVr = Assert.IsType<ViewResult>(idxRes);
                var returnedRecipes = Assert.IsAssignableFrom<IEnumerable<Recipe>>(idxResVr.ViewData.Model);
                foreach(var recipe in fakeRecipes)
                {
                    Assert.Contains(recipe, returnedRecipes);
                }

                foreach (var recipe in returnedRecipes)
                {
                    var res = await testCtrl.DeleteConfirmed(recipe.Id);
                    var resVr = Assert.IsType<RedirectToActionResult>(res);
                    Assert.Equal("Index", resVr.ActionName);
                }


            }
            
        }
        private DbContextOptions<ApplicationDbContext> GetTestDbOpts()
        {
            var opts = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=aspnet-53bc9b9d-9d6a-45d4-8429-2a2761773502;Trusted_Connection=True;MultipleActiveResultSets=true").Options;
            return opts;
        }

        private List<Recipe> MakeFakeRecipes(int i)
        {
            var recipes = new List<Recipe>();

            for(int j = 0; j < i; j++)
            {
                recipes.Add(new Recipe
                {
                    Name = $"test{j}",
                    Ingredients = $"testIng{j}",
                    Process = $"testProc{j}"
                });
                    
            }
            return recipes;
        }
    }
}
