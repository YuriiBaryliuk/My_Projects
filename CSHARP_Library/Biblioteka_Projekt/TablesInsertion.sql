ALTER TABLE Genre NOCHECK CONSTRAINT ALL
INSERT Genre (Genre_id, GenreName) VALUES (1, 'Fantasy')
INSERT Genre (Genre_id, GenreName) VALUES (2, 'Science Fiction')
INSERT Genre (Genre_id, GenreName) VALUES (3, 'Romance')
INSERT Genre (Genre_id, GenreName) VALUES (4, 'Mystery')
INSERT Genre (Genre_id, GenreName) VALUES (5, 'Thriller')
INSERT Genre (Genre_id, GenreName) VALUES (6, 'Horror')
INSERT Genre (Genre_id, GenreName) VALUES (7, 'Historical Fiction')
INSERT Genre (Genre_id, GenreName) VALUES (8, 'Action')
INSERT Genre (Genre_id, GenreName) VALUES (9, 'Adventure')
INSERT Genre (Genre_id, GenreName) VALUES (10, 'Literary Fiction')
INSERT Genre (Genre_id, GenreName) VALUES (11, 'Young Adult')
INSERT Genre (Genre_id, GenreName) VALUES (12, 'Children''s')
INSERT Genre (Genre_id, GenreName) VALUES (13, 'Biography')
INSERT Genre (Genre_id, GenreName) VALUES (14, 'Self-Help')
INSERT Genre (Genre_id, GenreName) VALUES (15, 'True Crime')
ALTER TABLE Genre CHECK CONSTRAINT ALL


ALTER TABLE Reader NOCHECK CONSTRAINT ALL
insert into Reader (Reader_id, LastName, FirstName, Gender, BirthDate, "Address", City, Phone, Email, Registered) values (1, 'Aikman', 'Raffaello', 'M', '1980-01-14', 'Vista Street 204', 'Bibis', '+48 871 197 321', 'raikman0@purevolume.com', '2025-10-30 10:03:29');
insert into Reader (Reader_id, LastName, FirstName, Gender, BirthDate, "Address", City, Phone, Email, Registered) values (2, 'Dykins', 'Shelly', 'F', '2003-06-24', 'Bath Way 3/25', 'Xiaoya', '+48 650 836 566', 'sdykins1@columbia.edu', '2025-11-13 12:11:48');
insert into Reader (Reader_id, LastName, FirstName, Gender, BirthDate, "Address", City, Phone, Email, Registered) values (3, 'Shoreman', 'Vina', 'F', '1988-08-23', 'Railway Passage 261', 'Austin', '+48 512 305 142', 'vshoreman2@etsy.com', '2025-11-30 09:19:25');
insert into Reader (Reader_id, LastName, FirstName, Gender, BirthDate, "Address", City, Phone, Email, Registered) values (4, 'Raulston', 'Sianna', 'F', '1990-10-24', 'Providence Route 1/84', 'Kribi', '+48 453 225 287', 'sraulston3@miitbeian.gov.cn', '2025-08-20 13:51:43');
insert into Reader (Reader_id, LastName, FirstName, Gender, BirthDate, "Address", City, Phone, Email, Registered) values (5, 'Cullimore', 'Raddie', 'M', '1969-10-27', 'Quarry Row 7', 'Jinshi', '+48 337 505 722', 'rcullimore4@sitemeter.com', '2025-10-22 14:35:46');
insert into Reader (Reader_id, LastName, FirstName, Gender, BirthDate, "Address", City, Phone, Email, Registered) values (6, 'Smidmor', 'Liam', 'M', '2002-10-14', 'Broom Passage 18/33', 'Batawa', '+48 395 861 990', 'lsmidmor5@java.com', '2025-12-25 18:13:10');
insert into Reader (Reader_id, LastName, FirstName, Gender, BirthDate, "Address", City, Phone, Email, Registered) values (7, 'Antonik', 'Flora', 'F', '1999-07-12', 'Ranger Route 4/2', 'Kuala Lumpur', '+48 962 542 570', 'fantonik6@apple.com', '2025-05-19 17:32:00');
insert into Reader (Reader_id, LastName, FirstName, Gender, BirthDate, "Address", City, Phone, Email, Registered) values (8, 'Franken', 'Rona', 'F', '1997-05-09', 'Flint Boulevard 63', 'Vilemov', '+48 421 389 757', 'rfranken7@parallels.com', '2025-03-24 10:03:56');
insert into Reader (Reader_id, LastName, FirstName, Gender, BirthDate, "Address", City, Phone, Email, Registered) values (9, 'Cruttenden', 'Nerty', 'F', '1999-07-14', 'Redwood Way 23/4', 'Ru-ye Sang', '+48 965 843 606', 'ncruttenden8@desdev.cn', '2025-05-31 16:32:38');
insert into Reader (Reader_id, LastName, FirstName, Gender, BirthDate, "Address", City, Phone, Email, Registered) values (10, 'Blackborow', 'Ewell', 'M', '1985-05-30', 'Olive Lane 17', 'Jezerce', '+48 414 602 725', 'eblackborow9@washingtonpost.com', '2025-01-09 10:28:00');
ALTER TABLE Reader CHECK CONSTRAINT ALL


ALTER TABLE Book NOCHECK CONSTRAINT ALL
insert into Book (Book_id, Author, Title, YearOfRelease, Genre_id, "Description") values (1, 'Marabel Guinery', 'Slacker', 2000, 1, '-');
insert into Book (Book_id, Author, Title, YearOfRelease, Genre_id, "Description") values (2, 'Renault Tellenbrok', 'Homeward Bound II: Lost in San Francisco', 1992, 14, '-');
insert into Book (Book_id, Author, Title, YearOfRelease, Genre_id, "Description") values (3, 'Luther Bielby', 'Delivery, The', 2001, 4, '-');
insert into Book (Book_id, Author, Title, YearOfRelease, Genre_id, "Description") values (4, 'Crawford Bellerby', 'Ivans xtc.', 1995, 7, '-');
insert into Book (Book_id, Author, Title, YearOfRelease, Genre_id, "Description") values (5, 'Elsey Dunkinson', 'English Teacher, The', 2007, 7, '-');
insert into Book (Book_id, Author, Title, YearOfRelease, Genre_id, "Description") values (6, 'Inesita Braybrookes', 'Taming the Fire (Ukroshcheniye ognya)', 1991, 10, '-');
insert into Book (Book_id, Author, Title, YearOfRelease, Genre_id, "Description") values (7, 'Burgess Hrus', 'Episode 3: Enjoy Poverty', 1997, 10, '-');
insert into Book (Book_id, Author, Title, YearOfRelease, Genre_id, "Description") values (8, 'Madelle Josey', '3rd Voice, The', 1967, 14, '-');
insert into Book (Book_id, Author, Title, YearOfRelease, Genre_id, "Description") values (9, 'Rochelle Glasgow', 'Wagons East', 1995, 15, '-');
insert into Book (Book_id, Author, Title, YearOfRelease, Genre_id, "Description") values (10, 'Melloney Mordan', 'Christine', 2008, 10, '-');
ALTER TABLE Book CHECK CONSTRAINT ALL


ALTER TABLE Staff NOCHECK CONSTRAINT ALL
insert into Staff (Staff_id, LastName, FirstName, Title) values (1, 'McVeighty', 'Candide', 'Librarian');
insert into Staff (Staff_id, LastName, FirstName, Title) values (2, 'Awde', 'Berna', 'Manager');
insert into Staff (Staff_id, LastName, FirstName, Title) values (3, 'Abbate', 'Livvy', 'Librarian');
ALTER TABLE Staff CHECK CONSTRAINT ALL


ALTER TABLE Loans NOCHECK CONSTRAINT ALL
insert into Loans (Reader_id, Staff_id, Book_id, LoanDate, Note) values (10, 1, 5, '2025-01-09 10:30:00', null);
insert into Loans (Reader_id, Staff_id, Book_id, LoanDate, Note) values (8, 1, 3, '2025-03-24 10:05:00', null);
insert into Loans (Reader_id, Staff_id, Book_id, LoanDate, Note) values (7, 1, 10, '2025-05-19 17:34:00', null);
insert into Loans (Reader_id, Staff_id, Book_id, LoanDate, Note) values (9, 3, 7, '2025-05-31 16:35:38', null);
insert into Loans (Reader_id, Staff_id, Book_id, LoanDate, Note) values (4, 1, 5, '2025-08-20 13:52:43', null);
insert into Loans (Reader_id, Staff_id, Book_id, LoanDate, Note) values (2, 3, 1, '2025-11-13 12:13:48', null);
insert into Loans (Reader_id, Staff_id, Book_id, LoanDate, Note) values (6, 3, 8, '2025-12-25 18:13:10', null);
insert into Loans (Reader_id, Staff_id, Book_id, LoanDate, Note) values (3, 1, 2, '2025-12-29 12:12:12', null);
ALTER TABLE Loans CHECK CONSTRAINT ALL


ALTER TABLE Recievings NOCHECK CONSTRAINT ALL
insert into Recievings (Loan_id, Staff_id, RecievingDate, Note) values (1, 1, '2025-01-15 18:32:02', null);
insert into Recievings (Loan_id, Staff_id, RecievingDate, Note) values (2, 3, '2025-04-24 14:02:19', null);
insert into Recievings (Loan_id, Staff_id, RecievingDate, Note) values (3, 3, '2025-05-25 15:16:17', null);
insert into Recievings (Loan_id, Staff_id, RecievingDate, Note) values (4, 1, '2025-06-08 17:10:33', null);
insert into Recievings (Loan_id, Staff_id, RecievingDate, Note) values (5, 3, '2025-08-29 12:11:12', null);
ALTER TABLE Recievings CHECK CONSTRAINT ALL


ALTER TABLE [Currently Loaned] NOCHECK CONSTRAINT ALL
insert into [Currently Loaned] (Loan_id) values (6)
insert into [Currently Loaned] (Loan_id) values (7)
insert into [Currently Loaned] (Loan_id) values (8)
ALTER TABLE [Currently Loaned] CHECK CONSTRAINT ALL


ALTER TABLE Payments NOCHECK CONSTRAINT ALL
insert into Payments (Reader_id, Amount, PaymentDate) values (8, 3.40, '2025-04-24')
ALTER TABLE Payments CHECK CONSTRAINT ALL