# SCTS - Android Remote Control Application (SCTS – ARCA)

Application .NET MAUI pour le chronométrage de runs sur voies de vitesse et le contrôle d'appareils via Bluetooth (RFCOMM / SPP).

## Résumé

Application mobile simple pour démarrer/arrêter un chronomètre, enregistrer les temps et consulter l'historique. Elle inclut un service Android natif capable d'envoyer des commandes texte à des périphériques Bluetooth SPP (RFCOMM).

Fonctionnalités principales
- Chronomètre avec démarrage/arrêt, réinitialisation et enregistrement des temps.
- Historique des temps sauvegardé localement (Preferences, JSON).
- Pages : `LoginPage`, `CreateAccountPage`, `MainPage`, `RunsPage`, `HistoryPage`, `SettingsPage`.
- Service Android : `Platforms/Android/BluetoothService.cs` (envoi de texte via RFCOMM).

## Prérequis

- .NET 10 SDK
- Visual Studio 2022/2023 (ou compatible MAUI) avec workload MAUI
- Appareil Android pour tester (ou émulateur avec support Bluetooth)

## Permissions Android

Le manifeste contient les permissions Bluetooth, y compris `android.permission.BLUETOOTH_CONNECT`. Sur Android 12+ (API 31+), cette permission doit aussi être accordée au runtime.

- Déclarer la permission dans `Platforms/Android/AndroidManifest.xml` n'est pas suffisant.
- Avant d'appeler les APIs Bluetooth, l'application doit vérifier/obtenir `BLUETOOTH_CONNECT` à l'exécution.

Remarque : le service `BluetoothService` vérifie la permission et journalise les erreurs mais ne demande pas la permission lui‑même. La demande doit être déclenchée depuis l'UI (Activity/Page).

## Construire et exécuter

1. Ouvrir la solution dans Visual Studio.
2. Sélectionner le projet Android comme startup.
3. Déployer sur un appareil Android avec Bluetooth activé.

Commande CLI (exemple) :

```bash
dotnet build -t:Run -f net10-android
```

## Utilisation

- Se connecter via `LoginPage` (valeurs de test intégrées : licence `420130`, mot de passe `123456`).
- Depuis l'onglet `Accueil`, appuyer sur `Démarrer un run` pour ouvrir `RunsPage`.
- Sur `RunsPage` : démarrer/arrêter le chronomètre, réinitialiser, puis `Enregistrer le temps`. Les temps sont sauvegardés et visibles dans `HistoryPage`.

## Architecture / emplacement du code

- Chronomètre et historique : `Pages/RunsPage.xaml(.cs)` et `Pages/HistoryPage.xaml(.cs)` (utilisent `Preferences` pour stocker les temps).
- Auth / inscription : `Pages/LoginPage.xaml(.cs)`, `Pages/CreateAccountPage.xaml(.cs)` (simulées).
- Bluetooth Android : `Platforms/Android/BluetoothService.cs` implémente `IBluetoothService.SendTextAsync`.

## Sécurité et limitations

- Authentification et données utilisateur sont actuellement simulées (valeurs en dur). Pas de backend.
- Les fonctionnalités réseau (tester la connexion) et la logique multi-voie ne sont pas implémentées.

## Débogage

- Consultez `Logcat` pour les messages Android, en particulier les erreurs journalisées par `BluetoothService`.
- En cas d'`Android.Runtime.JavaProxyThrowable`, le log enregistré contient la cause Java (p.ex. `SecurityException` si permission manquante).

## Prochaines améliorations suggérées

- Implémenter la demande runtime de permission `BLUETOOTH_CONNECT` depuis l'UI.
- Connecter l'enregistrement d'un temps à l'envoi automatique via Bluetooth (utiliser `IBluetoothService`).
- Ajouter prise en charge de deux voies (Voie A / Voie B) et export CSV de l'historique.

## Contributions

Pull requests bienvenues. Merci d'ajouter des tests et d'expliquer les changements dans la description.

## Licence

Préciser la licence du projet (par ex. MIT).