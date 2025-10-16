CREATE DATABASE cadastrocontatos;

USE cadastrocontatos;

CREATE TABLE Contato(
                        id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
                        nome VARCHAR(100) NOT NULL,
                        idade INT(3)
);

CREATE TABLE Telefone(
                         idcontato INT NOT NULL,
                         id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
                         numero VARCHAR(16) NOT NULL,

                         FOREIGN KEY(idcontato) REFERENCES Contato(id)
);

