Thank you for using Authed!

Discord: https://discord.gg/aVFudVf

How to use: 
1.) Create a free account at https://authed.io 
2.) Create a application from the dashboard.
3.) Download the files from https://authed.io/download
4.) Using your existing project or create a new one add the 3 DLL's as references and add the AuthedChecker.cs file to your project
5.) Create a new line and type in AuthedChecker checker = new AuthedChecker(); and then checker.CheckFiles(); this is a bool check if its true and continue. eg. 
	if(checker.CheckFiles()) 
	{ 
		//Rest of the code below 
	} 
	else 
	{ 
		Enviornment.Exit(0); //Bad Attempt 
	}
6.) In your login page call a new instance of the Authed class with Auth auth = new Auth(); (You will be asked what to do with Auth click using Authed;)
7.) Copy your Application Secret from the Authed.IO dashboard.
8.) Call the authenticate method like so bool authed = auth.Authenticate("APP SECRET");
9.) Login or Register a user.
   9.A.) bool loggedIn = auth.Login("USERNAME","PASSWORD");
   9.B) bool registered = auth.Register("USERNAME", "PASSWORD", "EMAIL", "LICENSE");
10.) Enjoy.

Also you can hook two events.

auth.OnBannedUser
auth.OnInvalidUser

To do so just type that and +=(space) and hit tab to auto generate two event handlers.

InvalidUser event fires when a invalid user is caught on the application (AKA a bypasser)
BannedUser event fires when a user gets banned to make sure they dont get unwarented time. 

Also be sure to always check if a user is banned or expired when logging them in with 

auth.user.expired and auth.user.banned (both bools)