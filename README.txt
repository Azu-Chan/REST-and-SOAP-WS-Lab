README.txt - RITROVATO Dylan
==========+-+===============



Arboréscence de la solution :
/
|
+--- packages (contient les bibliothèques externes nécessaires à la solution, pour parser les JSON (.NET JSON newtonsoft))
|
+--- SOAP_CLIENT_VELIB (client SOAP graphique)
|
+--- SOAP_CONSOLE_VELIB (client SOAP en ligne de commande)
|
+--- SOAP_LIB_VELIB (service SOAP servant d'intermédiaire entre le client SOAP et l'API REST)
|
+--- SOAP_LIB_VELIB.sln (fichier .sln de la solution)


Axes choisis :
* MVP (Mark Scale : 10 points)
* Graphical User Interface for the client (Mark Scale : 2 points)
* Replace all the accesses to WS (beetween Velib WS and IWS, between IWS and WS Clients) with asynchronous ones. Some indications can be find just below. (Mark Scale : 3 points)
* Add a cache in IWS, to reduce communications between Velib WS and IWS (Mark Scale : 4 points)


Utilisation :
* Application graphique, 3 valeurs sont attendues pour afficher le nombre de vélos disponibles (client async).
1. Le nom du contrat (Combobox du haut).
2. Le nom de la station (Combobox du bas).
3. Temps minimal d'acceptation de la donnée (dernier rafraîchissement du nombre de vélos disponibles pour la station voulue) en secondes,
   si ce champ est vide, on considère 0 (rafraîchissemene obligatoire). Ce champ utilise la notion de cache de données présente dans le service.
4. Cliquer sur le bouton de Recherche.
-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-
La valeur s'affiche en bas de la fenêtre dans un label.

* Application console (non async).
Liste des commandes disponibles :
exit : permet de quitter le programme proprement.
help : affiche une aide concernant la liste des commandes disponibles.
contracts : affiche la liste des contrats disponibles.
stations [contract] : affiche la liste des stations du contrat passé en paramètre.
bikes [contract] [station_reg] : affiche le nombre de vélos disponibles aux stations contenant [station_reg] du contrat [contract].


Bonus (ascii-art de Twilight Sparkle (Santé mentale affaiblie par le stress), MLP par Dolkar <https://www.equestriaforums.com/index.php?topic=11672.0>) :

                     ░████
                    ▒▓  ▓█
                         ██▓
                   ▓█  ▒████▓
                 ████▓███████
                ▓████████████▓▓▓▓▓▓▓▓▓▓
        ▓▒     ▓███████████▓▒▒░░░░░░░▓▒
        ▒█▓  ▓████████████████░░░░░░▓▒
          ██▓█████████████████░░░░▒▓▒
     ▒▓█▓▓▓██████████████████▓░░▒▓▓▓▓▓▓
        ████████████████████▒▒░▓▓▓▒▒░ ▒▓▓▓▓▒
   ▓██▓  ░▒███████▓▓▓▓███████░▒▓           ▓▓▓
     ▓████████▓▓▓▓▓▓▓▓▓▓█████▒▓░             ▒▓▓
        ░▒▓▓▒▒▒▓▓▓▓▒▒▒▓▓▓▓███▓▒              ░░▒▓▓
       ░░     ░░░░░░▒▒▒▓▓███▓░▓░      ░▒    ░░░░▒▓░
      ░  ▒▒▒▒▒▒▓▓▓▓▓████▓▓▒█▓░▓▓▒    ▓█▒  ░░░░░░░▓▓
     ░   ▒░░░░░░▓░░░░██▓▒▒▒██░░░▒▒▒▒▒▒▒▒▒▒▒░░░▒░░░▓▓
      ░░  ▒▒▓▓▓░▒░░▓█▓▒▒▒▒█▒░░░░░░░░░░░░░░░▓▒░░▓▓▒░▓░
        ░  ░░░▓▓████▓▒▒▒▓█████░░░░░░░░░░░▒▓░░░░▒▓█▒▓▓▓▓
         ░░░░▒▒▒▓█▓▒▒▒▓▓▒▒▒▒▒█▒░░░▒▒░░░░░░▒▒░░░▒▓█▒▓▓░░▓▒
           ░▒▒▓▓▓▒▒▒▒▒▒▒▒▒▓▓▓██▒▒▒▒▒▒▒▓▒░░░░░░▒▓▓▒▓▓░░░░▓▓
           ▒▓██▓▓▒▒▒▒▓█████████      ▒█▓    ░░▒░░▒▓▒░░░░▒▓▓
         ▓  ██   ██▓████████▓▓▒             ▒░░░░▓▓░░░░░░░▓░
        ▒▓███▓    ▒████████████▓            ░░░▓▓▓░░░░░░░░▓▓
                   ▓░░▓████████▒           ░░░▓▓▒░░░░░░░░░░▓▒
                   ▓▓░░░▓██████▓▒        ░▒▓▓▓▓░░░░░░░░░░░░░▓
                   ░▓▓░░░▓█████▓▓▓▓▓▓▓▓▓▓▓▓██▓▓░░░░░▒▓▓▓▓▒░░░▓▓
                    ░▓▓░░░░▓██▓▓  ██  █  █████▓░░▒▓▓▓░░░▒▓▓▓▓▓▓▓▓▓▓▓
                     ▒▓▓▓░░░░░░▓  █▒ █▓ █████▓▒▒▓▓░░░░░▓▓▓▓▒░░░░░░▓▓▓
                       ▒▓▓▓░░░░▓  ▓  ███▓  █▓▒▓▓░░░░░░▓▓░░░░░░░░░░░░░▓▓▓
                          ▓▓▓▓░▓          ▓▓▒▓▓░░░░░░░▓▓░░░░░░░░░░░░░░░▓▓
                             ▒▓▓▓     ▓  █▓▒▒▓░░░░░░░░▓▓░░░░░░░░░░░░░░░▓▓
                                      ████▒░░░░░░░░░░░░▓▒░░░░░░░░░░░░░░▓▓
                                      ███▓░░░░░░░░░▒▓▓▓▓▓▒▒▒▒▒▒▒▒▒▓▓▓▓▓▓
                                       ██▓▓░░░░░░░▓▓▓░░░░▓▓▓▓▓▓▓▓▓▓▓▓
                                         ██▓▓░░░▓▓▓░░░░░░░░░░░░░▓
                                          ███▓▓▓▓░░░░░░░░░░░░░░░▓
