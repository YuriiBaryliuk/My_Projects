declare @allExist bit = 1;

if not exists (select * from sysobjects where name = 'Arrears') 
set @allExist = 0

if not exists (select * from sysobjects where name = 'Book') 
set @allExist = 0

if not exists (select * from sysobjects where name = 'Currently Loaned') 
set @allExist = 0

if not exists (select * from sysobjects where name = 'Genre') 
set @allExist = 0

if not exists (select * from sysobjects where name = 'Loans') 
set @allExist = 0

if not exists (select * from sysobjects where name = 'Payments') 
set @allExist = 0

if not exists (select * from sysobjects where name = 'Reader') 
set @allExist = 0

if not exists (select * from sysobjects where name = 'Recievings') 
set @allExist = 0

if not exists (select * from sysobjects where name = 'Staff') 
set @allExist = 0

select @allExist as allExist