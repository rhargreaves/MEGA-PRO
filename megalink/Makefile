
all: publish

RUNTIME:=osx-x64

clean:
	dotnet clean

publish:
	dotnet publish -r $(RUNTIME) -c Release -o bin	

install: publish
	ln -s $(shell pwd)/bin/megalink /usr/local/bin/megalink

uninstall:
	rm /usr/local/bin/megalink

.PHONY: publish clean install uninstall
