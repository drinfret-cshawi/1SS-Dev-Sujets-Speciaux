create table users
(
    id       integer primary key autoincrement,
    username varchar(20) not null unique check ( length(username) >= 1 ),
    fullname text,
    password text        not null check ( length(password) >= 8 ),
    email    text
);

insert into users (username, fullname, password, email)
values ('denis', 'Denis Rinfret', '12345678', 'drinfret@example.com'),
       ('alice', 'Alice', '12123232', null);

create table passwords
(
    id       integer primary key autoincrement,
    user_id  int  not null references users (id),
    site     text not null,
    login text,
    password text not null,
    unique (user_id, site, login)
);

insert into passwords(user_id, site, login, password)
VALUES (1, 'google.com', 'denis', '12345678'),
       (1, 'facebook.com', 'denis', '87654321');

create table user_data
(
    id      integer primary key autoincrement,
    user_id int  not null references users (id),
    key     text not null,
    data    text not null
);

