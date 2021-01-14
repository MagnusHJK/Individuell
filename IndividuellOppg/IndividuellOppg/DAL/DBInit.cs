using IndividuellOppg.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndividuellOppg.DAL
{
    public class DBInit
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<KundeServiceContext>();

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Kategori bestilling = new Kategori { Navn = "Bestilling" };
                Kategori stasjoner = new Kategori { Navn = "Stasjoner" };
                Kategori hittegods = new Kategori { Navn = "Hittegods" };
                Kategori busser = new Kategori { Navn = "Busser" };
                Kategori annet = new Kategori { Navn = "Annet" };

                FAQ faq1 = new FAQ()
                {
                    Kategori = bestilling,
                    Sporsmal = "Jeg fikk ingen mail med billetten min?",
                    Svar = "Sjekk spam filteret ditt i epost klienten, mailen burde kommet fra NORWAY.ITPE3200@gmail.com",
                    Oppstemmer = 0,
                    Nedstemmer = 0
                };

                FAQ faq2 = new FAQ()
                {
                    Kategori = stasjoner,
                    Sporsmal = "Hvor kan jeg se hvilke stasjoner jeg kan reise til?",
                    Svar = "Hvis du går til stasjonslisten, øverst på siden kan du finne en oversikt.",
                    Oppstemmer = 0,
                    Nedstemmer = 0
                };

                FAQ faq3 = new FAQ()
                {
                    Kategori = hittegods,
                    Sporsmal = "Jeg har glemt/mistet noe på bussen",
                    Svar = "Du kan komme i kontakt med hittegods sentralen via tlf 12345678, eller på mail NORWAY.ITPE3200@gmail.com",
                    Oppstemmer = 0,
                    Nedstemmer = 0
                };

                FAQ faq4 = new FAQ()
                {
                    Kategori = hittegods,
                    Sporsmal = "Jeg stjal noe for morroskyld, og har nå fått skyldfølelse. Hvordan kan jeg returnere tingene?",
                    Svar = "Du kan komme i kontakt med hittegods sentralen via tlf 12345678, eller på mail NORWAY.ITPE3200@gmail.com",
                    Oppstemmer = 0,
                    Nedstemmer = 0
                };

                FAQ faq5 = new FAQ()
                {
                    Kategori = busser,
                    Sporsmal = "Hva slags fasiliteter har bussene?",
                    Svar = "Bussene vi bruker har komfertable seter med justerbar rygg. Toalett finnes bakerst og er ofte renngjort. Det er gratis WIFI ombord og strømuttakk ved alle seter",
                    Oppstemmer = 0,
                    Nedstemmer = 0
                };

                FAQ faq6 = new FAQ()
                {
                    Kategori = annet,
                    Sporsmal = "Bussjåføren har helt elendig musikksmak og enda verre sangstemme!",
                    Svar = "Vi har en regel om at sjåførene ikke skal spille for høy musikk, hvis dette fortsetter å være et problem kan du ta kontakt på <a href='mailto: NORWAY.ITPE3200@gmail.com'>NOR-WAY kontakt</a> " +
                    "Angående sangstemme kan vi anbefale å kjøpe et par ørepropper.",
                    Oppstemmer = 0,
                    Nedstemmer = 0
                };

                List<Kategori> kategorier = new List<Kategori>
                {
                    bestilling,
                    stasjoner,
                    hittegods,
                    busser,
                    annet
                };


                List<FAQ> FAQs = new List<FAQ>
                {
                    faq1,
                    faq2,
                    faq3,
                    faq4,
                    faq5,
                    faq6
                };

                context.Kategori.AddRange(kategorier);
                context.FAQ.AddRange(FAQs);
                context.SaveChanges();
            }
        }
    }
}
