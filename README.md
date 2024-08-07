# 6002CEM Mobile App Development - Helena's Recipes

# Background and Motivation
<p> Helena’s Recipes was born out of a personal frustration with existing recipe management apps. As a passionate cook, I often try to find apps that offer good recipe browsing experiences but they generally lacked the ability to publish my own creations, and those that allowed me to save my recipes but lacked a comprehensive database of diverse recipes. </p>
<p> Helena’s Recipes aims to bring these two concepts together allowing the user to have the best of both worlds. It is a mobile app to manage your recipes and browser through recipes of other users of the app and also through online ones.
This app enable the user user to create an account and login. Following this, the user can add its own recipes, see other people’s recipes and  their own, and also see online recipes that will redirect to the website with the full recipe page. Another functionality includes the account page where the user can check its details and update its password. </p>

# Features
<h3> User Account Management: </h3>
<p> •	Create a user account. </p>
<p> •	Log in and log out securely. </p>
<p> These features use Supabase to securely store the users’ details in their cloud database and its authentication to verify the users’ credentials when trying to login. </p>
<h3> Recipe Management: </h3>
<p> •	Add your own recipes with detailed instructions and ingredients. </p>
<p> •	Access a list of "My Recipes". </p>
<p> These features post and get recipes from the recipes table in Supabase based on the id of user currently logged in. </p>
<h3> Recipe Discovery: </h3>
<p> •	Explore a diverse range of recipes from various users within the app. </p>
<p> •	Discover new recipes from external sources through integration with the Spoonacular API. </p>
<p> •	Access full online recipes directly from the app, with redirection to the original website for more details. </p>
<p> The users’ recipes feature gets all the recipes from the recipes table in the database hosted in Supabase. </p>
<p> The API features get 10 random recipes from Spoonacular API. These recipes have their own page in the app with some information about it and that also redirects the user to the original recipe’s web page. </p>
<h3> User Profile Management: </h3>
<p> •	View and manage your user profile details. </p>
<p> •	Update your password securely to ensure account safety. </p>
<p> These features get the user’s details from the users table in the database hosted on Supabase and use their authentication to update the user’s password, first checking in the current password is current and only then allowing the user to update it. </p>
