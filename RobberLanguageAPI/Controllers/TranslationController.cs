using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RobberLanguageAPI.Data;
using RobberLanguageAPI.Models;
using System.Threading.Tasks;

namespace RobberLanguageAPI.Controllers
{


    [ApiController]
    [Route("api/RobberLanguage")] // Här ändrar jag Route-attributet för min Controller

    //Controller : en klass som innehåller flera actions. Actions är något som händer i
    // ASP.NET Core programmet. Men här, istället för att retuerna en view, kan man se det som
    // om att varje action returnera ett json-objekt. Ett slags svar på ett web-Api-anrop.
 
    public class TranslationController : ControllerBase
    {

        
        
        /*
          Här skapar jag en metod som tar in en sträng och
          översätter den till Rövarspråket. Vanligtvis vill man inte lägga dessa
          metoder i Controllern, men i övningen ska det göras ändå.. 
         */
        private static string TranslateSentence(string word)
        {


            string robberLanguage = "";

            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] == 'a' || word[i] == 'A' || word[i] == 'o' || word[i] == 'O' ||
                    word[i] == 'u' || word[i] == 'U' || word[i] == 'å' || word[i] == 'Å' ||
                    word[i] == 'e' || word[i] == 'E' || word[i] == 'i' || word[i] == 'I' ||
                    word[i] == 'y' || word[i] == 'Y' || word[i] == 'ä' || word[i] == 'Ä' ||
                    word[i] == 'ö' || word[i] == 'Ö')
                {
                    robberLanguage += word[i];
                }
                else
                {
                    robberLanguage += word[i];
                    robberLanguage += 'o';
                    robberLanguage += word[i];
                }
            }

            return robberLanguage;
        }
    
        private readonly ILogger<TranslationController> _logger;
        private readonly RobberTranslationDBContext _context;

        public TranslationController(ILogger<TranslationController> logger, RobberTranslationDBContext context)
        {
            _logger = logger;
            _context = context;
        }
       

        //POST: api/RobberLanguage/CreateNewTranslation
        [HttpPost]
        [Route("createTranslation")] //jag ändrar mitt Route-attribut för action (POST-endpoint)

        /*Här skapar jag en POST-endpoint som returernar ett Translation-objekt. Detta objekt
         innehåller värden för OriginalSentence och TraslatedSentence.
        Nu kan man köra och testa programmet. */
        public async Task <ActionResult<Translation>> createTranslation(Translation originalSentence/*Translation translation*/)
        {
     



            var Translation = new Translation
             {
              
                OriginalSentence = originalSentence.OriginalSentence,

                TranslatedSentence = $"{TranslateSentence(originalSentence.OriginalSentence) }"
          
            };
            if (string.IsNullOrWhiteSpace(Translation.OriginalSentence))
            {
                return BadRequest();
            }
            /* Här sparar vi ner översättningen till Databasen.
          */
            await _context.Translations.AddAsync(Translation);
            await _context.SaveChangesAsync();
       

            return Translation;


        }


    }
}


