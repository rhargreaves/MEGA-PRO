
# runtime id (e.g. osx-arm64)
RID:=$(shell dotnet --info | grep "RID:" | awk '{print $$2}')

all: publish
.PHONY: all

clean:
	dotnet clean
.PHONY: clean

publish:
	dotnet publish -r $(RID) -c Release --self-contained
.PHONY: publish

/usr/local/bin/megalink:
	ln -s $(shell pwd)/megalink/bin/Release/net9.0/osx-arm64/publish/megalink $@

install: publish /usr/local/bin/megalink
.PHONY: install

uninstall:
	rm /usr/local/bin/megalink
.PHONY: uninstall