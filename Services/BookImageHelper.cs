
using System;
using System.Collections.Generic;

namespace LibrarieOnline.Services
{
    public static class BookImageHelper
    {
        private static readonly Dictionary<string, string> ImageMap =
            new(StringComparer.OrdinalIgnoreCase)
            {
                ["Ion"] = "ion.jpg",
                ["Morometii"] = "Morometii.jpg",
                ["Moromeții"] = "Morometii.jpg",
                ["Enigma Otiliei"] = "enigmaOtiliei.jpg",
                ["Patul lui Procust"] = "patulluiprocust.jpg",
                ["Amintiri din copilărie"] = "amintiridincopilarie.jpg",
                ["Orbitor"] = "orbitor.jpg",
                ["Marile speranțe"] = "marilesperante.jpg",
                ["Jane Eyre"] = "janeeyre.jpg",
                ["Numele trandafirului"] = "numeletrandafirului.jpg",
                ["Poezii"] = "poeziieminescu.jpg",
                ["Alchimistul"] = "alchimistul.jpg",
                ["De ce iubim femeile"] = "deceiubimfemeile.jpg",
                ["Mitul lui Sisif"] = "mitulluisisif.jpg",
                ["Ultima noapte de dragoste, întâia noapte de război"] = "ultimanoapte.jpg",
                ["Mizerabilii"] = "mizerabilii.jpg",
                ["Don Quijote"] = "donquijote.jpg",
                ["Război și pace"] = "razboisipace.jpg",
                ["Procesul"] = "procesul.jpg",
                ["Harry Potter și piatra filozofală"] = "harrypottersipiatrafilozofala.jpg",
                ["Străinul"] = "strainul.jpg",
                ["Dragostea în vremea holerei"] = "dragosteainvremeaholerei.jpg",
                ["Un veac de singurătate"] = "unveacdesinguratate.jpg",
                ["Marele Gatsby"] = "marelegatsby.jpg",
                ["Contele de Monte Cristo"] = "conteledecristomonte.jpg",
                ["Germinal"] = "germinal.jpg",
                ["În căutarea timpului pierdut"] = "incautareatimpuluipierdut.jpg",
                ["Greața"] = "greata.jpg",
                ["Greaţă"] = "greata.jpg",
                ["Romeo și Julieta"] = "romeosijulieta.jpg",
                ["Crima din Orient Express"] = "crimadinorientexpress.jpg",
                ["Câinele grădinarului"] = "cainelegradinarului.jpg",
                ["Suflete moarte"] = "sufletemoarte.jpg",
                ["Umbra vântului"] = "umbravantului.jpg",
                ["Portretul lui Dorian Gray"] = "portretulluidoriangray.jpg",
                ["Pe aripile vântului"] = "pearipilevantului.jpg",
                ["Desculț"] = "descult.jpg",
                ["Apus de soare"] = "apusdesoare.jpg",
                ["Proiecte de trecut"] = "proiectedetrecut.jpg",
                ["Papillon"] = "papillon.jpg",
                ["Gestapo"] = "gestapo.jpg",

                // Cartile nou adaugate
                ["Fiica căpitanului"] = "fiicacapitanului.jpg",
                ["Dama de pică și alte proze"] = "damadepica.jpg",
                ["Micul prinț"] = "miculprint.jpg",
                ["Livada de vișini"] = "livadadevisini.jpg",
                ["Groapa"] = "groapa.jpg",
                ["Teatru"] = "teatru-lorca.jpg",
                ["Crimă și pedeapsă"] = "crimasipedeapsa.jpg",
                ["1984"] = "1984.jpg",
                ["Părinți și copii"] = "parintisicopii.jpg",
                ["Mândrie și prejudecată"] = "mandriesiprejudecata.jpg",
                ["Baltagul"] = "baltagul.jpg",
                ["Hanu Ancuței. Baltagul"] = "hanuancutei-baltagul.jpg",
                ["Maitreyi"] = "maitreyi.jpg",
                ["Doamna Dalloway"] = "doamnadalloway.jpg"
            };

        public static string GetImagePath(string? title)
        {
            if (!string.IsNullOrWhiteSpace(title) &&
                ImageMap.TryGetValue(title.Trim(), out var imageFile))
            {
                return "/images/books/" + imageFile;
            }

            return "/images/books/default.jpg";
        }
    }
}
