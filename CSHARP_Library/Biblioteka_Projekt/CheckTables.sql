use [CSLibrary]
go

set dateformat dmy

declare @notexist bit = 0;

if not exists (select * from sysobjects where name = 'Arrears') 
set @notexist = 1

if not exists (select * from sysobjects where name = 'Book') 
set @notexist = 1

if not exists (select * from sysobjects where name = 'Currently Loaned') 
set @notexist = 1

if not exists (select * from sysobjects where name = 'Genre') 
set @notexist = 1

if not exists (select * from sysobjects where name = 'Loans') 
set @notexist = 1

if not exists (select * from sysobjects where name = 'Payments') 
set @notexist = 1

if not exists (select * from sysobjects where name = 'Reader') 
set @notexist = 1

if not exists (select * from sysobjects where name = 'Recievings') 
set @notexist = 1

if not exists (select * from sysobjects where name = 'Staff') 
set @notexist = 1

select @notexist as mine