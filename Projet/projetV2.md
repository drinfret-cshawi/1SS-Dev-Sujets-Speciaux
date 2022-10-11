# 420-1SS-SW: Développement: Sujets Spéciaux

## Projet : Gestionnaire de mots de passe

### Version #2

## Description générale

Vous devez créer une nouvelle version de votre gestionnaire de mots de passe.
Cette version sera une application console **avec** accès à une base de données.
Vous devez modifier votre projet V1 en ajoutant les fonctionalités décrites plus
bas.

## Structure du projet

Pour ne pas nuire à la correction du projet V1, créez une nouvelle branche,
nommée `version2`, dans votre dépôt Git, et travaillez dans cette branche.

## Configuration de la base de données

1. L'application devrait conserver les fonctionalités de la version 1, mais en
   plaçant les mots de passe dans une base de données située sur un serveur (
   possiblement sur une machine virtuelle). Basez votre base de données sur
   celle présentée en classe dans les exemples, en faisant les modifications
   nécessaires.
2. Vous devez ajouter 2 colonnes à la table `passwords` : `created`
   et `modified`, de type `datetime` (ou possiblement `timestamp`). Le but est
   d'enregistrer la date et l'heure de la création et de la modification de
   chaque mot de passe. Il est suggéré d'utiliser des valeurs par défaut pour
   ces colonnes, pour que la BD insère les bonnes valeurs automatiquement quand
   un mot de passe est créé. Il faut également s'assurer que `modified` sera
   aussi mise à jour quand un mot de passe sera mis à jour. Vous pouvez faire
   d'autres modifications à la BD si nécessaire.
3. Une autre modification importante à l'application est qu'elle doit supporter
   la connexion à la base de données pour accéder aux mots de passe, et que la
   BD est partagée par plusieurs utilisateurs. La table `users` contient les
   détails de connexion des utilisateurs. Les mots de passe dans la
   tables `users` sont les mots de passe des utilisateurs du système, donc ils
   doivent être *haché* avec une fonction unidirectionnelle telle que `bcrypt`,
   et non pas chiffré avec `aes`. Les mots de passe de la table `passwords`
   doivent être chiffés avec `aes` parce qu'on doit pouvoir les déchiffrer plus
   tard, mais les mots de passe de `users` sont utilisés seulement pour la
   connecxion des utilisateurs du système, donc on ne doit pas pouvoir les
   déchiffrer.

## Fonctionalités supplémentaires

### Modes **en ligne** et **hors ligne**

Vous devez supporter 2 modes d'utilisation : **en ligne** et **hors ligne**. Le
mode **en ligne** devra utiliser la base de données sur le serveur directement,
et le mode **hors ligne** sera très semblable à la version 1 du projet.

### Synchronisation

Pour rendre ces 2 modes utiles, il faut ajouter une fonction de synchronisation
des mots de passes. Il faut s'assurer que les mots de passe locaux et distants
sont les mêmes.

1. *Suggestion* : sauvegarder les mots de passe locaux dans un fichier nommé à
   partir du nom de l'utilisateur dans le système, quelque chose du
   genre `username.json`.
2. Si un mot de passe existe seulement à un endroit (local ou distant, mais pas
   les deux), alors il faut créer le mot de passe à l'endroit manquant lors de
   la synchro.
3. Si un mot de passe existe aux 2 endroits, alors il faut vérifier lequel des 2
   a été modifié en dernier, et mettre à jour l'autre.
4. Mais que fait-on si un mot de passe a été effacé ? Va-t-il être recréé au
   moment de la synchronisation ?