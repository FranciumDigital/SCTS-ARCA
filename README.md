# SCTS - Android Remote Control Application

Application .NET MAUI pour contrôler des appareils Android via Bluetooth (RFCOMM / SPP).

## Description

Cette application permet d'envoyer des commandes texte sur des périphériques Bluetooth classiques (SPP) en utilisant une adresse MAC et un socket RFCOMM.

## Prérequis

- .NET 10 SDK
- Visual Studio 2022/2023 (ou version compatible MAUI) avec le workload MAUI installé
- Un appareil Android (ou un émulateur avec support Bluetooth) pour tester la fonctionnalité Bluetooth

## Configuration Android importante

Sur Android 12+ (API 31 / Android S) et supérieur, l'accès aux API Bluetooth requiert des permissions au runtime. Le manifeste contient déjà la permission suivante :

- `android.permission.BLUETOOTH_CONNECT`

Cependant, déclarer la permission dans `Platforms/Android/AndroidManifest.xml` n'est pas suffisant : l'application doit demander et obtenir la permission à l'exécution avant d'appeler les APIs Bluetooth (ex. `CreateRfcommSocketToServiceRecord`, `ConnectAsync`).

Sans cette permission, l'appel natif lance une exception Java (p.ex. `SecurityException`) qui remonte en .NET comme `Android.Runtime.JavaProxyThrowable`.

### Recommandations

- Avant d'exécuter des opérations Bluetooth, vérifier et demander `BLUETOOTH_CONNECT` depuis une `Activity` ou via un flux de permissions MAUI.
- Gérer le résultat de la demande de permission et réessayer l'opération seulement si la permission est accordée.

## Construire et exécuter

1. Ouvrir la solution dans Visual Studio (ou via `dotnet build`).
2. Sélectionner la startup project `SCTS - Android Remote Control Application` et la configuration Android cible.
3. Déployer sur un appareil Android avec Bluetooth activé.

Exemple de commande CLI :

```bash
dotnet build -t:Run -f net10-android
```

(Note : l'utilisation de `dotnet` pour déployer sur un appareil Android peut nécessiter des arguments supplémentaires et un environnement configuré.)

## Points d'attention dans le code

- Le service de communication Bluetooth se trouve dans `Platforms/Android/BluetoothService.cs`.
  - Il vérifie désormais la permission `BLUETOOTH_CONNECT` pour Android 12+ et journalise les exceptions au lieu de les ignorer.
  - La demande de permission à l'utilisateur n'est pas effectuée automatiquement par le service : c'est une action d'interface utilisateur qui doit être déclenchée depuis une `Activity`/Page.
- Interface publique : `IBluetoothService.SendTextAsync(string macAddress, string message)`.

## Débogage

- Si vous voyez `Android.Runtime.JavaProxyThrowable` dans les logs/débogueur, consultez les logs Android (`Logcat`) pour le message complet journalisé par `BluetoothService` (le texte de l'exception Java y sera visible).
- Vérifiez que l'app a la permission `BLUETOOTH_CONNECT` sur l'appareil via les paramètres d'application.

## Contributions

Contributions bienvenues via pull requests : corriger des bugs, améliorer la gestion des permissions ou ajouter des tests.

## Licence

Indiquer la licence du projet (ex. MIT) ici.