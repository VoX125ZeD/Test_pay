CREATE DATABASE test_pay;
GO
USE test_pay;
CREATE TABLE [Order]
(
ID int IDENTITY(1,1) NOT NULL,
Dates smalldatetime NOT NULL,
Price decimal(12,2) NOT NULL,
Payment decimal(12,2)NOT NULL,
PRIMARY KEY(ID)
);
CREATE TABLE AccountMoney
(
ID int IDENTITY(1,1) NOT NULL,
Dates smalldatetime NOT NULL,
Balance decimal(12,2) NOT NULL,
Remainder decimal(12,2)NOT NULL,
PRIMARY KEY(ID)
);
CREATE TABLE Payment
(
ID_Order int  NOT NULL,
ID_Account int  NOT NULL,
AmountPay decimal(12,2)NOT NULL,
PRIMARY KEY(ID_Order,ID_Account)
);
ALTER TABLE Payment
   ADD CONSTRAINT FK_Pay_Order FOREIGN KEY (ID_Order)
      REFERENCES [Order] (ID)
      ON DELETE CASCADE
      ON UPDATE CASCADE
;
ALTER TABLE Payment
   ADD CONSTRAINT FK_Pay_Acc FOREIGN KEY (ID_Account)
      REFERENCES  AccountMoney(ID)
      ON DELETE CASCADE
      ON UPDATE CASCADE
;


go
CREATE TRIGGER Pay
ON Payment
AFTER INSERT
AS Begin
declare @pay decimal(12,2);
declare @balance decimal(12,2);
declare @ID_Order int;
declare @ID_Acc int;
declare @AmountPay decimal(12,2);
select @ID_Acc=ID_Account, @ID_Order=ID_Order,@AmountPay=AmountPay from inserted;
SET @balance=( select Remainder from AccountMoney where AccountMoney.ID=@ID_Acc);
if(@balance>0)
begin
SET @pay=( select Price- Payment from [Order] where [Order].ID=@ID_Order);
if(@pay>0)
if(@pay-@balance>=0)
begin
update [Order] set Payment=Price-(@pay-@balance) where ID=@ID_Order;
update AccountMoney set Remainder=0 where ID=@ID_Acc;
update Payment set AmountPay=@balance where ID_Order=@ID_Order and ID_Account=@ID_Acc;
end else 
begin
update [Order] set Payment=Price where ID=@ID_Order;
update AccountMoney set Remainder=Remainder-@pay where ID=@ID_Acc;
update Payment set AmountPay=@pay where ID_Order=@ID_Order and ID_Account=@ID_Acc;
end;
end;

End;
go
INSERT INTO[Order]
values
(GETDATE() ,1000,0),(GETDATE() ,1500,750);
INSERT INTO AccountMoney
values
(GETDATE() ,500,500),(GETDATE() ,100,100),(GETDATE() ,100,0),(GETDATE() ,150,100),(GETDATE() ,1500,1500);