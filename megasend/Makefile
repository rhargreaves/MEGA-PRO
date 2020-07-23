
all: publish

clean:
	dotnet clean

publish:
	dotnet publish -r osx-x64 -c Release -o bin	

install: publish
	ln -s $(PWD)/bin/megalink /usr/local/bin/megalink

uninstall:
	rm /usr/local/bin/megalink

.PHONY: publish clean install uninstall
