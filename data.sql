CREATE TABLE products(
    id SERIAL PRIMARY KEY,
    url TEXT NOT NULL,
    price INT NOT NULL,
    cpu TEXT DEFAULT NULL,
    memory INT DEFAULT NULL,
    ddr TEXT DEFAULT NULL,
    drive TEXT DEFAULT NULL,
    extras TEXT DEFAULT NULL,
    bargain TEXT NOT NULL,
    frozen BOOLEAN NOT NULL
);