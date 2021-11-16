# 5 Les contrôles utilisateur (UserControl)

On peut être amené à créer nos propres contrôles WPF pour les besoins suivants :

-  Découper la description d’un visuel très riche (code xaml très long)
   en parties plus petites, décrites dans des fichiers indépendants. Par
   exemple, si une fenêtre comporte plusieurs onglets, on peut décrire
   le contenu de chacun d’eux au moyen de contrôles utilisateurs.

-  Charger dynamiquement une partie du visuel d’une fenêtre. Par
   exemple, changer le contenu de la zone centrale de l’application en
   fonction du menu cliqué.

-  Réutiliser un morceau d’interface visuelle dans plusieurs fenêtres de
   l’application, ou dans plusieurs applications.

Un contrôle utilisateur est un assemblage de contrôles WPF existants et
est modélisé par une classe dérivant de `UserControl`.

NB/ Il est également possible de créer des contrôles personnalisés
(custom controls en anglais), qui héritent directement de `Control` ou
`ContentControl`, mais nous ne traiterons pas ce sujet dans ce cours.

## 5.1 Créer un contrôle

***Créer un contrôle utilisateur dans la solution courante***

Si le contrôle est destiné à être utilisé uniquement dans la solution
Visual Studio courante, il peut être créé de la même façon qu’une
fenêtre. Dans le menu contextuel du projet, choisir « ajouter \\
Contrôle utilisateur », puis donner un nom au contrôle. Ceci crée un
nouveau fichier xaml semblable à celui d’une fenêtre, avec l’élément
UserControl à la place de Window :

   <UserControl x:Class="ControleUtilisateur.MonControle"
   ...
   </UserControl>

On peut ensuite créer le visuel du contrôle, comme celui d’une fenêtre.

***Créer un contrôle utilisateur utilisable dans d’autres solutions***

Si le contrôle est destiné à être utilisé dans d’autres solutions Visual
Studio, il faut créer un nouveau projet de type « Bibliothèque de
contrôles utilisateurs ». Cette bibliothèque sera générée sous forme de
dll, et pourra être distribuée sous cette forme. Pour l’utiliser, il
suffit alors d’ajouter une référence vers la dll dans le projet.

## 5.2 Utiliser un contrôle

Pour utiliser le contrôle dans une fenêtre, il faut faire référence à
son espace de noms et éventuellement à son assembly, s’il est dans un
autre projet.

Exemple dans le cas où le contrôle est dans le même projet que la
fenêtre :

```xml
<Window x:Class="ControleUtilisateur.MainWindow"
         xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
         xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
         xmlns:local="clr-namespace:ControleUtilisateur"
         Title="MainWindow" Height="350" Width="525">
      <Grid>
         <local:MonControle/>
      </Grid>
</Window>
```

Exemple dans le cas où le contrôle est dans un autre projet :

```xml
xmlns:wtk="clr-namespace:Xceed.Wpf.Toolkit;assembly=Xceed.Wpf.Toolkit"
```

NB/ L’[Extended WPF Toolkit](http://wpftoolkit.codeplex.com/) est une
librairie de contrôles WPF très connue, créée par la société Xceed.
L’édition Community est gratuite et disponible sous forme de package NuGet.

Si on utilise des contrôles utilisateurs pour changer dynamiquement la
zone centrale d’une application, on peut dans ce cas les instancier par
le code et les afficher au moyen d’un ContentControl.

Exemple :

Dans le xaml de la fenêtre :

```xml
<DockPanel>
   <Menu DockPanel.Dock="Top">
      <MenuItem x:Name="menuProduits" Header="Produits" />
      <MenuItem x:Name="menuClients" Header="Clients"/>
   </Menu>
   <ContentControl x:Name="contentCtrl" Margin="5"/>
</DockPanel>
```
Dans le code-behind de la fenêtre :

```csharp
public partial class MainWindow : Window
{
   private UCProduits _ucProduits;
   private UCClients _ucClients;
   
   public MainWindow()
   {
      InitializeComponent();
   
      menuProduits.Click += MenuProduits_Click;
      menuClients.Click += MenuClients_Click;
   }

   private void MenuProduits_Click(object sender, RoutedEventArgs e)
   {
      if (_ucProduits == null) _ucProduits = new UCProduits();
      contentCtrl.Content = _ucProduits;
   }
   
   private void MenuClients_Click(object sender, RoutedEventArgs e)
   {
      if (_ucClients == null) _ucClients = new UCClients();
      contentCtrl.Content = _ucClients;
   }
}
```

Dans ce code, on instancie chaque contrôle la première fois que
l’utilisateur clique sur le menu correspondant, puis on mémorise
l’instance pour éviter de réinstancier le contrôle à chaque clic, ce qui
optimise les performances.
