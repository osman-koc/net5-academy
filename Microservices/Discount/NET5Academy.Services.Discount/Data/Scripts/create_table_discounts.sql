create table discounts(
id serial primary key,
userId varchar(200) unique not null,
rate smallint not null,
code varchar(50) not null,
startDate timestamp not null default CURRENT_TIMESTAMP,
endDate timestamp null,
createdDate timestamp not null default CURRENT_TIMESTAMP,
deletedDate timestamp null,
isDeleted boolean default false
)