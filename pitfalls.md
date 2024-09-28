### If .net not found by extension, Try one of the following:
- restart vscode(kill the app, and restart it)
- sudo ln -s /usr/local/share/dotnet/dotnet /usr/local/bin/dotnet(also restart vscode)

### Android 
- install java 17: `brew install openjdk@17`
- add `export JAVA_HOME=$(/usr/libexec/java_home -v 17)` and `export PATH=$JAVA_HOME/bin:$PATH` to .zshrc