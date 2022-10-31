# Création de la VM pour le projet V3

## Étapes obligatoires

1. Créer une VM et y installer Ubuntu Server (version la plus récente)
    - ou vous pouvez installer une autre distribution, mais il se pourrait qu'il y aie des problèmes de version de
      paquet
    - présentement, l'API nécessite au moins la version 1.18 de `golang`
    - il se peut que ça fonctionne sur des versions précédentes, mais l'API a été testé seulement sur golang 1.18
    - il est préférable de nommer l'utilisateur principal `passman` lors de l'installation pour simplifier la connexion
      à la BD et la création des tables
2. Installer les paquets nécessaires dans la VM
    - `sudo apt-get update`
    - `sudo apt-get upgrade`
    - `sudo apt-get install git golang postgresql`
3. Cloner le `passman_api`
    - `git clone https://github.com/drinfret-cshawi/passman_api.git`
4. Compiler l'API, en tant qu'utilisateur régulier (`passman` dans mon cas)
   - `cd passman_api`
   - `go build passman.go`
5. Lancer l'API
   - `./passman`
   - note : la BD n'étant pas encore configurée, l'API ne fonctionnera pas correctement
   - `Ctrl-C` pour stopper l'API
6. Devenir l'utilisateur de la bd et se connecter à la BD
   - `sudo su postgres`
   - `cd`
   - `psql`
7. Création de l'utilisateur BD `passman`
   - `create user passman login;`
   - `create database passman owner passman;`
   - `\password passman`
     - entrer `passman` comme mot de passe, ou sinon, modifier l'API pour mettre à jour le mot de passe
8. pour revenir comme utilisateur OS `passman`
   - `\q`
   - `exit`
9. Création de la BD `passman`
   - `cd db` pour aller dans `passman_api/db`
   - `psql < create.sql`

## Étapes optionnelles

### Pour pouvoir se connecter à la BD directement (à ne pas faire normalement, sauf pour faciliter le développement de l'application)

1. Modifier sur quelle adresse le serveur écoute
   - `sudo vi /etc/postgresql/14/main/postgresql.conf`
     - ou utiliser `nano` ou un autre éditeur de texte 
   - décommenter la ligne `listen_adresses = '*'`
2. Modifier qui peut se connecter au serveur de BD
   - `sudo vi /etc/postgresql/14/main/pg_hba.conf`
   - ajouter à la fin en choisissant les bonnes adresses ip 
     - `host passman passman 192.168.255.255/16 scram-sha-256`
3. Redémarrer le serveur
   - `sudo service postgresql restart`