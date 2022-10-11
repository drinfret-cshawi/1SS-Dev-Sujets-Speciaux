# 420-1SS-SW: Développement: Sujets Spéciaux

## Projet : Gestionnaire de mots de passe

### Version #1

## Description générale

Vous devez créer un gestionnaire de mots de passe en C#. Cette version sera une
application console sans accès à une base de données. Les versions suivantes
ajouteront un accès à une base de données, ainsi qu'une interface graphique.

Vous devez vous baser en partie sur les exemples donnés en classe
(génération d'un mot de passe aléatoire, cryptage à l'aide de l'algorithme
AES, ...). Les mots de passe doivent être enregistrés de façon sécuritaire
dans un fichier, en format JSON, qui doit être crypté.

## Structure du projet

Prenez soin de donner des noms appropriés à vos projets dans votre solution.
Vous devriez utiliser des projets de type *.Net/.Net Core 6*. Placez votre
solution dans un dépôt Git privé sur GitHub, et partagez-le seulement avec
l'utilisateur `drinfret-cshawi` . Votre code devrait être dans la branche
principale du dépôt Git.

### Bibliothèque(s) de classes

Tout ce qui ne dépend pas de l'application console, et qui pourrait être
réutilisé ailleurs, comme dans une application bureau avec une interface
graphique ou dans une application web, doit être placé dans une (ou des)
bibliothèque(s) de classes. Par exemple, les méthodes de cryptage de
données doivent être dans une bibliothèque de classe.

### Tests unitaires

Des tests unitaires doivent être placés dans un projet de type `xUnit`. Vous
devriez tester ce qui peut être testé dans les bibliothèques de classes.

### Application console

L'application devrait, au minimum, avoir ces fonctionnalités :

1. Générer un mot de passe d'une longueur donnée.
    - L'utilisateur peut utiliser la longueur par défaut égale à 16, ou
      préciser une autre longueur entre 8 et 64 inclusivement.
2. Créer un nouveau fichier pouvant contenir des mots de passe cryptés 
3. Ouvrir un fichier contenant des mots de passe cryptés
4. Enregistrer le fichier présentement ouvert
5. Lister tous les mots de passe dans le fichier présentement ouvert, sans 
   les déchiffrer
6. Chercher un mot de passe dans le fichier présentement ouvert en utilisant 
   les propriétés associées au mot de passe (nom d'utilisateur, site web ou 
   système, etc...), sans déchiffrer les mots de passe
7. Sélectionner un mot de passe spécifique (après l'affichage de la liste 
   ou la recherche), et ensuite, on peut
   1. déchiffrer le mot de passe
   2. cacher le mot de passe déchiffré
   3. mettre à jour le mot de passe (et ses propriétés associées)
   4. effacer le mot de passe
   