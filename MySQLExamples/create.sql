create table users
(
    id       integer primary key auto_increment,
    username varchar(20) not null unique check ( length(username) >= 1 ),
    fullname text,
    password text        not null check ( length(password) >= 8 ),
    email    text
);

insert into users (username, fullname, password, email)
values ('denis', 'Denis Rinfret', '12345678', 'drinfret@cshawi.ca'),
       ('alice', 'Alice', '12123232', null);

create table passwords
(
    id       integer primary key auto_increment,
    user_id  int          not null references users (id),
    site     varchar(255) not null,
    login    varchar(255),
    password text         not null,
    unique (user_id, site, login)
);

insert into passwords(user_id, site, username, password)
VALUES (1, 'google.com', 'denis', '12345678'),
       (1, 'facebook.com', 'denis', '87654321');

create table user_data
(
    id      integer primary key auto_increment,
    user_id int  not null references users (id),
    keyStr  text not null,
    data    text not null
);

