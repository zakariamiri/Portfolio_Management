# Portfolio Management

Application web ASP.NET Core MVC permettant à des professionnels (Creators) de construire et publier un portfolio en ligne, et à des recruteurs (Recruiters) de consulter ces portfolios, de télécharger un CV au format PDF et de contacter directement les candidats.

Projet réalisé dans le cadre d'un cursus ASP.NET Core.

---

## Sommaire

- [Fonctionnalités](#fonctionnalités)
- [Stack technique](#stack-technique)
- [Architecture](#architecture)
- [Modèle de données](#modèle-de-données)
- [Structure du dépôt](#structure-du-dépôt)
- [Installation](#installation)
- [Configuration](#configuration)
- [Exécution](#exécution)
- [Parcours applicatifs](#parcours-applicatifs)
- [Référence des routes](#référence-des-routes)
- [Limites connues](#limites-connues)
- [Auteur](#auteur)

---

## Fonctionnalités

### Espace Creator

- Inscription et authentification par email / mot de passe.
- Assistant de complétion de profil en trois étapes (identité, profil détaillé, langues).
- Gestion du profil : biographie, photo de profil, liens LinkedIn et GitHub.
- Gestion CRUD complète des sections du portfolio :
  - Projets
  - Expériences professionnelles
  - Certifications
  - Compétences
  - Langues
- Consultation des messages reçus de la part des recruteurs.
- Modification des informations du compte (nom, prénom, téléphone, pays, email).
- Export du portfolio en CV PDF.

### Espace Recruiter

- Inscription, authentification et création d'un profil recruteur.
- Annuaire paginé des Creators inscrits.
- Consultation du portfolio complet d'un Creator.
- Téléchargement du CV d'un Creator au format PDF.
- Envoi d'un message de contact à un Creator.

---

## Stack technique

| Composant | Technologie |
|---|---|
| Framework | ASP.NET Core MVC |
| Plateforme cible | .NET 9.0 |
| Langage | C# (nullable et implicit usings activés) |
| ORM | Entity Framework Core 9.0.11 |
| Base de données | Microsoft SQL Server |
| Génération PDF | QuestPDF 2025.12.0 (licence Community) |
| Vues | Razor (`.cshtml`) |
| Ressources front-end | LibMan (`libman.json`) |
| Gestion de l'état | Session serveur (`AddSession`) |

---

## Architecture

L'application suit une architecture en couches classique avec injection de dépendances.

```
Controllers  -->  Services (interfaces + implémentations)  -->  Repositories  -->  DbContext (EF Core)  -->  SQL Server
     |
     v
   Views (Razor) / ViewModels
```

| Couche | Rôle | Emplacement |
|---|---|---|
| Controllers | Réception des requêtes HTTP, gestion de la session, sélection des vues | `Web_MZ/Controllers` |
| Services | Logique métier, orchestration | `Web_MZ/Services` |
| Repositories | Accès aux données et requêtes EF Core | `Web_MZ/Repository` |
| Entities | Modèle de domaine persisté | `Web_MZ/Entities` |
| Models | ViewModels dédiés aux vues | `Web_MZ/Models` |
| Data | `MyDbContext`, configuration du modèle relationnel | `Web_MZ/Data` |
| Pdf | Composition des documents CV via QuestPDF | `Web_MZ/Pdf` |

Chaque service et chaque repository est exposé via une interface et enregistré en portée `Scoped` dans `Program.cs`, ce qui rend les couches substituables et testables indépendamment.

---

## Modèle de données

Neuf entités composent le domaine.

| Entité | Description | Relations |
|---|---|---|
| `User` | Compte utilisateur commun, discriminé par le champ `Role` (`Creator` ou `Recruiter`) | 1-1 vers `Creator` et `Recruiter` |
| `Creator` | Profil du candidat : biographie, image, LinkedIn, GitHub | 1-N vers `Project`, `Experience`, `Certification`, `Competence`, `Langue` |
| `Recruiter` | Profil du recruteur | Émetteur des `Contact` |
| `Project` | Projet du portfolio | N-1 vers `Creator` |
| `Experience` | Expérience professionnelle | N-1 vers `Creator` |
| `Certification` | Certification obtenue | N-1 vers `Creator` |
| `Competence` | Compétence technique ou transverse | N-1 vers `Creator` |
| `Langue` | Langue maîtrisée | N-1 vers `Creator` |
| `Contact` | Message envoyé par un recruteur à un créateur | N-1 vers `Recruiter` et `Creator` |

Comportements de suppression configurés dans `MyDbContext.OnModelCreating` :

- `Contact` vers `Creator` : `Cascade`.
- `Contact` vers `Recruiter` : `Restrict`, afin d'éviter les chemins de cascade multiples rejetés par SQL Server.

### Migrations

Le dépôt contient cinq migrations EF Core appliquées dans cet ordre :

1. `InitCreation` — création initiale du schéma.
2. `FixPhone` — correction du type du champ téléphone.
3. `AddLinkedInAndGitHubToCreator` — ajout des liens professionnels.
4. `AddLanguesTable` — ajout de la table des langues.
5. `UpdateProjectModel` — évolution du modèle projet.

---

## Structure du dépôt

```
Portfolio_MiriZakaria.sln          Solution Visual Studio
Web_MZ/
    Program.cs                     Point d'entrée, DI, pipeline HTTP
    Web_MZ.csproj                  Configuration du projet et dépendances
    appsettings.json               Configuration applicative (chaîne de connexion)
    libman.json                    Dépendances front-end
    Controllers/                   11 contrôleurs MVC
    Services/                      Couche métier (interfaces + implémentations)
    Repository/                    Couche d'accès aux données
    Entities/                      Modèle de domaine EF Core
    Models/                        ViewModels
    Data/                          MyDbContext
    Migrations/                    Migrations EF Core
    Pdf/                           Génération des CV (QuestPDF)
    Views/                         Vues Razor par contrôleur
    Properties/launchSettings.json Profils de lancement
    wwwroot/                       Ressources statiques, uploads, librairies
```

---

## Installation

### Prérequis

- [.NET SDK 9.0](https://dotnet.microsoft.com/download) ou version ultérieure.
- Microsoft SQL Server (SQL Server Express suffit).
- Optionnel : Visual Studio 2022 (17.12+) ou JetBrains Rider.
- Optionnel : `dotnet-ef` pour la gestion des migrations en ligne de commande.

```bash
dotnet tool install --global dotnet-ef
```

### Récupération du code

```bash
git clone https://github.com/zakariamiri/Portfolio_Management.git
cd Portfolio_Management
dotnet restore
```

---

## Configuration

La chaîne de connexion est lue depuis la clé `ConnectionStrings:PortfolioDbConnection` de `Web_MZ/appsettings.json`.

```json
{
  "ConnectionStrings": {
    "PortfolioDbConnection": "Data Source=VOTRE_SERVEUR\\SQLEXPRESS;Initial Catalog=DB_PORTFOLIO;Integrated Security=True;TrustServerCertificate=True"
  }
}
```

Remplacez `Data Source` par l'instance SQL Server locale. Pour éviter de committer une configuration machine, la chaîne peut être surchargée via les secrets utilisateur :

```bash
cd Web_MZ
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:PortfolioDbConnection" "Data Source=...;Initial Catalog=DB_PORTFOLIO;Integrated Security=True;TrustServerCertificate=True"
```

### Création de la base de données

```bash
cd Web_MZ
dotnet ef database update
```

Sous Visual Studio, la commande équivalente dans la console du gestionnaire de packages est `Update-Database`.

---

## Exécution

```bash
cd Web_MZ
dotnet run
```

L'application démarre sur les URLs définies dans `Properties/launchSettings.json`. La route par défaut est `{controller=Home}/{action=Index}/{id?}`.

Les images de profil téléversées sont écrites dans `Web_MZ/wwwroot/uploads`, créé automatiquement au premier envoi.

---

## Parcours applicatifs

### Creator

1. Inscription via `SignUpCreator/Creator`.
2. Connexion via `Login/login`. Les informations de l'utilisateur sont stockées en session.
3. Redirection conditionnelle pilotée par `LoginController` selon l'état de complétion du profil :
   - aucun enregistrement `Creator` : `Demon/FirstDemon` ;
   - biographie, image, LinkedIn ou GitHub manquants : `Demon/SecondDemon` ;
   - aucune langue enregistrée : `Demon/ThirdDemon` ;
   - profil complet : `Dashboard/Dash`.
4. Gestion du portfolio depuis le tableau de bord.

### Recruiter

1. Inscription via `SignUpRecruiter/recruiter`.
2. Connexion via `Login/login`.
3. Si aucun profil recruteur n'existe, redirection vers `DashboardRecruiter/AddProfil`, sinon vers `DashboardRecruiter/Welcom`.
4. Parcours de l'annuaire, consultation d'un portfolio, téléchargement du CV, envoi d'un message.

---

## Référence des routes

### Authentification et inscription

| Méthode | Route | Description |
|---|---|---|
| GET, POST | `/Login/login` | Authentification et redirection selon le rôle |
| GET, POST | `/SignUpCreator/Creator` | Inscription d'un créateur |
| GET, POST | `/SignUpRecruiter/recruiter` | Inscription d'un recruteur |
| GET | `/Choices/choices` | Choix du type de compte |

### Assistant de profil (Creator)

| Méthode | Route | Description |
|---|---|---|
| GET | `/Demon/FirstDemon` | Étape 1 de l'assistant |
| GET | `/Demon/SecondDemon` | Étape 2 de l'assistant |
| GET | `/Demon/ThirdDemon` | Étape 3 de l'assistant |
| GET, POST | `/Creator/SecondDemon` | Saisie du profil détaillé |
| GET | `/Langue/Langues` | Sélection des langues |
| POST | `/Langue/SaveLangues` | Enregistrement des langues |

### Tableau de bord Creator

| Méthode | Route | Description |
|---|---|---|
| GET | `/Dashboard/Dash` | Accueil du tableau de bord |
| GET | `/Dashboard/Profil` | Consultation du profil |
| POST | `/Dashboard/EditProfile` | Mise à jour du profil et de la photo |
| POST | `/Dashboard/EditAccount` | Mise à jour des informations du compte |
| GET | `/Dashboard/Projet` | Liste des projets |
| POST | `/Dashboard/AddProjet` | Ajout d'un projet |
| GET, POST | `/Dashboard/EditProjet` | Modification d'un projet |
| POST | `/Dashboard/DeleteProjet` | Suppression d'un projet |
| GET | `/Dashboard/Experience` | Liste des expériences |
| POST | `/Dashboard/AddExperience` | Ajout d'une expérience |
| GET, POST | `/Dashboard/EditExperience` | Modification d'une expérience |
| POST | `/Dashboard/DeleteExperience` | Suppression d'une expérience |
| GET | `/Dashboard/Certif` | Liste des certifications |
| POST | `/Dashboard/AddCertif` | Ajout d'une certification |
| GET, POST | `/Dashboard/EditCertif` | Modification d'une certification |
| POST | `/Dashboard/DeleteCertif` | Suppression d'une certification |
| GET | `/Dashboard/competence` | Liste des compétences |
| POST | `/Dashboard/Addcompetence` | Ajout d'une compétence |
| GET, POST | `/Dashboard/Editcompetence` | Modification d'une compétence |
| POST | `/Dashboard/Deletecompetence` | Suppression d'une compétence |
| GET | `/Dashboard/Message` | Messages reçus des recruteurs |
| GET | `/Dashboard/DownloadCv` | Export du CV en PDF |

### Tableau de bord Recruiter

| Méthode | Route | Description |
|---|---|---|
| GET | `/DashboardRecruiter/Welcom` | Accueil du recruteur |
| GET | `/DashboardRecruiter/AddProfil` | Création du profil recruteur |
| GET | `/DashboardRecruiter/DashRecruiter` | Annuaire paginé des créateurs |
| GET | `/DashboardRecruiter/ConsulterPortfolio/{id}` | Consultation d'un portfolio |
| POST | `/DashboardRecruiter/SendContact` | Envoi d'un message à un créateur |
| GET | `/DashboardRecruiter/DownloadCV/{id}` | Téléchargement du CV d'un créateur |

### Pages générales

| Méthode | Route | Description |
|---|---|---|
| GET | `/` ou `/Home/Index` | Page d'accueil |
| GET | `/Home/Privacy` | Politique de confidentialité |
| GET | `/Home/Error` | Page d'erreur |

---

## Limites connues

Ces points sont documentés en toute transparence. Le projet a une vocation pédagogique et n'est pas destiné en l'état à un déploiement en production.

- **Mots de passe stockés en clair.** `LoginService.Authenticate` compare directement l'email et le mot de passe en base. Une migration vers un algorithme de hachage (`PasswordHasher<T>` d'ASP.NET Core Identity ou BCrypt) est nécessaire avant toute mise en production.
- **Autorisation applicative.** Le contrôle d'accès repose sur la présence de clés en session plutôt que sur le middleware d'authentification et les attributs `[Authorize]`.
- **Chaîne de connexion versionnée.** `appsettings.json` contient une chaîne de connexion pointant vers une instance locale ; elle doit être remplacée ou surchargée par les secrets utilisateur.
- **Artefacts de compilation versionnés.** Les répertoires `bin/` et `obj/` sont actuellement suivis par Git, faute de fichier `.gitignore`. L'ajout du `.gitignore` standard .NET et le retrait de ces fichiers du suivi allégerait le dépôt.
- **Instanciation manuelle du `DbContext`.** `LoginController` construit un `MyDbContext` en dehors du conteneur d'injection de dépendances ; le passage par le constructeur serait plus cohérent avec le reste du code.

---

## Auteur

Zakaria Miri

Dépôt : https://github.com/zakariamiri/Portfolio_Management
