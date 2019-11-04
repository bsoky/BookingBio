USE BookingDB;

insert into Lounges (seatCount) values (20);

insert into AllSeats (loungeId,rowNumber,seatNumber) values (1,'a',1);
insert into AllSeats (loungeId,rowNumber,seatNumber) values (1,'a',2);
insert into AllSeats (loungeId,rowNumber,seatNumber) values (1,'a',3);
insert into AllSeats (loungeId,rowNumber,seatNumber) values (1,'a',4);
insert into AllSeats (loungeId,rowNumber,seatNumber) values (1,'a',5);

insert into AllSeats (loungeId,rowNumber,seatNumber) values (1,'b',1);
insert into AllSeats (loungeId,rowNumber,seatNumber) values (1,'b',2);
insert into AllSeats (loungeId,rowNumber,seatNumber) values (1,'b',3);
insert into AllSeats (loungeId,rowNumber,seatNumber) values (1,'b',4);
insert into AllSeats (loungeId,rowNumber,seatNumber) values (1,'b',5);

insert into AllSeats (loungeId,rowNumber,seatNumber) values (1,'c',1);
insert into AllSeats (loungeId,rowNumber,seatNumber) values (1,'c',2);
insert into AllSeats (loungeId,rowNumber,seatNumber) values (1,'c',3);
insert into AllSeats (loungeId,rowNumber,seatNumber) values (1,'c',4);
insert into AllSeats (loungeId,rowNumber,seatNumber) values (1,'c',5);

insert into AllSeats (loungeId,rowNumber,seatNumber) values (1,'d',1);
insert into AllSeats (loungeId,rowNumber,seatNumber) values (1,'d',2);
insert into AllSeats (loungeId,rowNumber,seatNumber) values (1,'d',3);
insert into AllSeats (loungeId,rowNumber,seatNumber) values (1,'d',4);
insert into AllSeats (loungeId,rowNumber,seatNumber) values (1,'d',5);

insert into Movies (movieName) values ('Joker');
insert into Movies (movieName) values ('Terminator: Dark Fate');
insert into Movies (movieName) values ('Maleficent: Mistress of Evil');
insert into Movies (movieName) values ('El Camino: A Breaking Bad Movie');
insert into Movies (movieName) values ('47 Meters Down: Uncaged');
insert into Movies (movieName) values ('Zombieland: Double Tap');
insert into Movies (movieName) values ('Good Boys');
insert into Movies (movieName) values ('Fast & Furious Presents: Hobbs & Shaw');
--insert into Movies (movieName) values ('ワンピーススタンピード'); 
insert into Movies (movieName) values ('Gemini Man');
insert into Movies (movieName) values ('Doctor Sleep');
insert into Movies (movieName) values ('It Chapter Two');
insert into Movies (movieName) values ('Scary Stories to Tell in the Dark');
insert into Movies (movieName) values ('The Addams Family');
insert into Movies (movieName) values ('Dolemite Is My Nam');
insert into Movies (movieName) values ('The Angry Birds Movie 2');
insert into Movies (movieName) values ('Descendants 3');
insert into Movies (movieName) values ('Black and Blue');
insert into Movies (movieName) values ('Red Shoes and the Seven Dwarfs');
insert into Movies (movieName) values ('Rattlesnake');

insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'1','2019-11-04 13:00:00');
insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'1','2019-11-05 14:00:00');
insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'1','2019-11-06 14:00:00');
insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'1','2019-11-07 18:00:00');
insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'1','2019-11-08 19:00:00');

insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'2','2019-11-09 10:00:00');
insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'2','2019-11-10 20:00:00');
insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'2','2019-11-11 17:00:00');
insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'2','2019-11-12 19:00:00');
insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'2','2019-11-13 21:00:00');

insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'3','2019-11-14 21:00:00');
insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'3','2019-11-15 22:00:00');
insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'3','2019-11-16 18:00:00');
insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'3','2019-11-17 19:00:00');
insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'3','2019-11-18 20:00:00');

insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'4','2019-11-19 11:00:00');
insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'4','2019-11-20 13:00:00');
insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'4','2019-11-21 16:00:00');
insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'4','2019-11-22 18:00:00');
insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'4','2019-11-23 22:00:00');

insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'5','2019-11-24 21:00:00');
insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'5','2019-11-25 14:00:00');
insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'5','2019-11-26 15:00:00');
insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'5','2019-11-27 17:00:00');
insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'5','2019-11-28 19:00:00');

insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'6','2019-11-29 21:00:00');
insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'6','2019-11-30 13:00:00');
insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'6','2019-12-01 14:00:00');
insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'6','2019-12-02 17:00:00');
insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'6','2019-12-03 20:00:00');

insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'7','2019-12-04 20:00:00');
insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'7','2019-12-05 19:00:00');
insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'7','2019-12-06 18:00:00');
insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'7','2019-12-07 22:00:00');
insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'7','2019-12-08 14:00:00');

insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'8','2019-12-09 14:00:00');
insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'8','2019-12-10 16:00:00');
insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'8','2019-12-11 17:00:00');
insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'8','2019-12-12 10:00:00');
insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'8','2019-12-13 22:00:00');

insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'9','2019-12-14 21:00:00');
insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'9','2019-12-15 00:00:00');
insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'9','2019-12-16 12:00:00');
insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'9','2019-12-17 15:00:00');
insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'9','2019-12-18 18:00:00');

insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'10','2019-12-19 20:00:00');
insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'10','2019-12-20 18:00:00');
insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'10','2019-12-21 19:00:00');
insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'10','2019-12-22 21:00:00');
insert into MovieShowings (loungeId,movieId,movieShowingTime) values (1,'10','2019-12-23 15:00:00');

