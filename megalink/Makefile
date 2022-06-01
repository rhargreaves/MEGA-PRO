
all: publish

RUNTIME:=osx-x64

clean:
	dotnet clean

publish:
	dotnet publish -r $(RUNTIME) -c Release -o bin

/usr/local/bin/megalink:
	ln -s $(shell pwd)/bin/megalink

install: publish /usr/local/bin/megalink

uninstall:
	rm /usr/local/bin/megalink

.PHONY: publish clean install uninstall
