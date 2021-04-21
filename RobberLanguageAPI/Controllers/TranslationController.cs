﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RobberLanguageAPI.Modell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RobberLanguageAPI.Controllers
{
  
    [ApiController]
    [Route("api/RobberLanguage")] // jag ändrar Route-attributet för min Controller
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

        public TranslationController(ILogger<TranslationController> logger)
        {
            _logger = logger;
        }

        //POST: pi/RobberLanguage/CreateNewTranslation
        [HttpPost]
        [Route("createTranslation")] //jag ändrar mitt Route-attribut för POST-endpoint

        /*Här skapar jag en POST-endpoint som returernar ett Translation-objekt. Detta objekt
         innehåller värden för OriginalSentence och TraslatedSentence.
        Nu kan man köra och testa programmet. */
        public Translation createTranslation(Translation originalSentence)
        {
            var Translation = new Translation
            {
                OriginalSentence = originalSentence.OriginalSentence,
                TranslatedSentence = $"{TranslateSentence(originalSentence.OriginalSentence) }"

            };
            return Translation;
        }


    }
}


