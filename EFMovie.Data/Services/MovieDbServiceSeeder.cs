using System;
using System.Collections.Generic;
using System.Text;
using EFMovie.Data.Models;

namespace EFMovie.Data.Services
{
    public class MovieDbServiceSeeder
    {
        public static void Seed(IMovieService svc)
        {
            svc.Initialise();

            //create movies:
            var m1 = new Movie { Duration = 185, Title = "The Godfather", Director = "Francis Ford Coppola", Year=1972, Budget=22.3, Genre=Genre.Action, Cast="Marlon Brando, Al Pacino, James Caan", Plot= "Widely regarded as one of the greatest films of all time, this mob drama, based on Mario Puzo's novel of the same name, focuses on the powerful Italian-American crime family of Don Vito Corleone (Marlon Brando). When the don's youngest son, Michael (Al Pacino), reluctantly joins the Mafia, he becomes involved in the inevitable cycle of violence and betrayal. Although Michael tries to maintain a normal relationship with his wife, Kay (Diane Keaton), he is drawn deeper into the family business.", PosterUrl= "https://image.tmdb.org/t/p/w600_and_h900_bestv2/rPdtLWNsZmAtoZl9PK7S2wE3qiS.jpg" };
            var m2 = new Movie { Duration = 123, Title = "Good Will Hunting", Director = "Gus Van Sant", Year=1998, Budget=4.7, Genre=Genre.Family, Cast="Robin Williams, Matt Damon, Ben Affleck", Plot= "Will Hunting (Matt Damon) has a genius-level IQ but chooses to work as a janitor at MIT. When he solves a difficult graduate-level math problem, his talents are discovered by Professor Gerald Lambeau (Stellan Skarsgard), who decides to help the misguided youth reach his potential. When Will is arrested for attacking a police officer, Professor Lambeau makes a deal to get leniency for him if he will get treatment from therapist Sean Maguire (Robin Williams).", PosterUrl= "https://image.tmdb.org/t/p/original/mc5GiK4bYn0rCCTja3iMa4oUWUi.jpg" };
            var m3 = new Movie { Duration = 145, Title = "Casino", Director = "Martin Scorsese", Year=1995, Budget=52, Genre=Genre.Action, Cast="Joe Pesci, Robert DeNiro, Sharon Stone", Plot= "In early-1970s Las Vegas, low-level mobster Sam 'Ace' Rothstein (Robert De Niro) gets tapped by his bosses to head the Tangiers Casino. At first, he's a great success in the job, but over the years, problems with his loose-cannon enforcer Nicky Santoro (Joe Pesci), his ex-hustler wife Ginger (Sharon Stone), her con-artist ex Lester Diamond (James Woods) and a handful of corrupt politicians put Sam in ever-increasing danger. Martin Scorsese directs this adaptation of Nicholas Pileggi's book. ", PosterUrl= "https://image.tmdb.org/t/p/w185_and_h278_bestv2/jC0dqpkRMwh3A1X2PHahTxOaLbp.jpg"};
            var m4 = new Movie { Duration = 175, Title = "Scarface", Director = "Brian DePalma", Year=1983, Budget=65.4, Genre=Genre.Action, Cast="Al Pacino, Michelle Pfeiffer, Steven Bauer", Plot= "After getting a green card in exchange for assassinating a Cuban government official, Tony Montana (Al Pacino) stakes a claim on the drug trade in Miami. Viciously murdering anyone who stands in his way, Tony eventually becomes the biggest drug lord in the state, controlling nearly all the cocaine that comes through Miami. But increased pressure from the police, wars with Colombian drug cartels and his own drug-fueled paranoia serve to fuel the flames of his eventual downfall.", PosterUrl= "https://image.tmdb.org/t/p/w185_and_h278_bestv2/zr2p353wrd6j3wjLgDT4TcaestB.jpg"};

            //add movies to database through service object:
            svc.AddMovie(m1);
            svc.AddMovie(m2);
            svc.AddMovie(m3);
            svc.AddMovie(m4);

            //create reviews:
            var r1 = new Review { Name = "Chris", On = new DateTime(1994, 10, 23, 12, 24, 36), MovieId = m1.Id, Comment = "Violent but very enjoyable movie", Rating = 5 };
            var r2 = new Review { Name = "Julie", On = new DateTime(1997, 10, 13, 02, 14, 12), MovieId = m1.Id, Comment = "A fascinating look into the cosa nostra", Rating = 8 };
            var r3 = new Review { Name = "Andy", On = new DateTime(2001, 12, 11, 05, 14, 12), MovieId = m1.Id, Comment = "Boring and very long, hard to stay awake to see the end", Rating = 2 };
            var r4 = new Review { Name = "Simon", On = new DateTime(2010, 1, 21, 06, 14, 12), MovieId = m2.Id, Comment = "A very interesting film about discovering oneself", Rating = 9 };
            var r5 = new Review { Name = "David", On = new DateTime(2017, 4, 20, 07, 14, 12), MovieId = m2.Id, Comment = "My mother was particularly fond of this film", Rating = 7 };
            var r6 = new Review { Name = "Imogen", On = new DateTime(2010, 5, 29, 08, 14, 12), MovieId = m3.Id, Comment = "Anyone who's into gambling will love this film", Rating = 10 };
            var r7 = new Review { Name = "Dana", On = new DateTime(2016, 7, 27, 09, 14, 12), MovieId = m3.Id, Comment = "It contains a lot of graphic violence, not for everyone", Rating = 9 };
            var r8 = new Review { Name = "Joanne", On = new DateTime(1999, 9, 24, 10, 14, 12), MovieId = m4.Id, Comment = "It is a timeless classic", Rating = 10 };
            var r9 = new Review { Name = "Catherine", On = new DateTime(2000, 11, 25, 11, 14, 12), MovieId = m4.Id, Comment = "My husband still raves on about this film 10 years later", Rating = 7 };

            //add reviews to database using service object:
            svc.AddReview(r1);
            svc.AddReview(r2);
            svc.AddReview(r3);
            svc.AddReview(r4);
            svc.AddReview(r5);
            svc.AddReview(r6);
            svc.AddReview(r7);
            svc.AddReview(r8);
            svc.AddReview(r9);
        }
    }
}
