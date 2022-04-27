--create table student
create  table Person
(
	st_id serial primary key,
	st_firstname character varying(20),
	st_midname character varying(20),
	st_lastname character varying(20)
)

select * from Person

--insert function
create or replace function insert_person(_firstname character varying, _midname character varying, _lastname character varying)
returns int as
$$
begin
	insert into Person(st_firstname, st_midname, st_lastname)
	values(_firstname, _midname, _lastname);
	if found then --inserted succesfully
		return 1;
	else --inserted fail
		return 0;
	end if;
end	
$$
language plpgsql

--test function insert
select * from insert_person('Brandon','Daniel','Parrillas');
select * from insert_person('David','Alexander','Parrillas');

--create function update student
DROP FUNCTION update_person(integer,character varying,character varying,character varying)
create or replace function update_person(_id int, _firstname character varying, _midname character varying, _lastname character varying)
returns int as
$$
begin
	update Person
	set 
		st_firstname = _firstname,
		st_midname = _midname,
		st_lastname = _lastname
	where st_id = _id;
	if found then --updated successfully
		return 1;
	else --update fail
		return 0;
	end if;
end
$$
language plpgsql

--test update function
select * from update_person(3, 'David', 'Alexander', 'Sanchez')

--select function
DROP FUNCTION person_select()
create or replace function person_select()
returns table
(
	_id int,
	_firstname character varying,
	_midname character varying,
	_lastname character varying
)as
$$
begin
	return query
	select st_id, st_firstname, st_midname, st_lastname from Person order by st_id;
end
$$
language plpgsql

--test select function
select * from person_select()












