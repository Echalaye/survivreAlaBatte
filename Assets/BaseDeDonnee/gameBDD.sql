CREATE Table
    mobs(
        mobs_id INTEGER PRIMARY KEY AUTO_INCREMENT,
        name TEXT,
        degats FLOAT,
        vitesseDeplacement FLOAT,
        vitesseAttaque FLOAT,
        knockback FLOAT,
        vie FLOAT,
    );

CREATE TABLE
    items(
        items_id INTEGER PRIMARY KEY AUTO_INCREMENT,
        name TEXT,
        vitesseCharge FLOAT,
        knockback FLOAT,
        chanceCrit FLOAT,
        damage FLOAT,
        distance FLOAT,
        description TEXT,
    );

CREATE TABLE
    player(
        name TEXT,
        vie FLOAT,
        vitesseDeplacement FLOAT,
    );

INSERT INTO
    mobs (
        name,
        degats,
        vitesseDeplacement,
        vitesseAttaque,
        knockback,
        vie
    )
VALUES ('zombie', 15, 0.7, 1, 1.5, 80), (
        'squelette',
        10,
        1.2,
        1.5,
        0.5,
        70
    ), ('piaf', 10, 1.5, 2, 0.5, 50), ('mouton', 0, 0.75, 0, 0, 30);

INSERT INTO
    items(
        name,
        vitesseCharge,
        knockback,
        chanceCrit,
        damage,
        distance,
        description
    )
VALUES (
        'épée',
        0.5,
        1,
        0.05,
        15,
        0,
        'une épée qui coupe. AHAH grosse épée.'
    ), (
        'hache',
        1.3,
        2,
        0.02,
        25,
        0,
        'la hache sa tabasse. QOICOUPEEEEEEEEEEEEEEEEE'
    ), (
        'arc',
        1.5,
        1.5,
        0.08,
        15,
        9,
        'Dans la vie il y a le bon chasseur et le mauvais chasseur...'
    ), (
        'boomerang',
        0.8,
        0.5,
        0.05,
        11,
        3,
        'je pars...Et non en faite je reviens.'
    ), (
        'magie de feu',
        2,
        0.1,
        0.10,
        30,
        6,
        'des boouboules Du feufeu...'
    ), (
        'magie de lévitation',
        5,
        10,
        0,
        0,
        0,
        'Tu tenvole tu tenvoles et on volllllll'
    ), (
        'GIGABATTE',
        1,
        20,
        0,
        01,
        0,
        'SMASHHHHHHHH'
    );

INSERT INTO
    player(name, vie, vitesseDeplacement){
VALUES ("Billy", 100, 1)}