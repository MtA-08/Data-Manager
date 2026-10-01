# Data Manager

Data Manager ist eine Web-App zum Verwalten von Produktdaten und Produktbildern. Du kannst einen vorhandenen Datensatz öffnen, Produkte bearbeiten, neue Produkte hinzufügen und die fertigen Daten wieder herunterladen.

Die App ist für ein festes Produktformat gebaut. Der bereitgestellte Beispieldatensatz enthält 70 PC-Komponenten: jeweils zehn CPUs, Grafikkarten, Mainboards, M.2-SSDs, Netzteile, RAM-Produkte und Gehäuse.

[Web-App öffnen](https://mta-08.github.io/Data-Manager/) · [Produktdaten.zip herunterladen](https://github.com/MtA-08/Data-Manager/releases/download/v1.0.0/Produktdaten.zip)

## Funktionen

- **Produktdaten importieren:** Eine ZIP-Datei mit Produktdaten und Bildern auswählen oder in das Uploadfeld ziehen. Die maximale Dateigröße beträgt 100 MiB.
- **Produkte ansehen:** Eine Übersicht zeigt die Produkte mit Bild und Namen. Ein Klick öffnet die vollständigen Produktdetails.
- **Produkte bearbeiten:** Name, Hersteller, Preis, Produkttyp, Stichwörter, Idealo-Link, Eigenschaften und Bild ändern.
- **Neue Produkte hinzufügen:** Neue Einträge erhalten automatisch eine ID oberhalb der bisher höchsten Produkt-ID.
- **Produkte löschen:** Vor dem Löschen erscheint eine Bestätigung.
- **Eigenschaften verwalten:** Einzelne Eigenschaften hinzufügen, bearbeiten oder entfernen. Über die Oberfläche lassen sich bis zu zehn Eigenschaften pro Produkt anlegen.
- **Bilder austauschen:** PNG-, JPG- und JPEG-Dateien auswählen und direkt als Vorschau sehen. Die maximale Bildgröße beträgt 10 MiB.
- **Änderungen speichern oder abbrechen:** Beim Bearbeiten eines vorhandenen Produkts wird zunächst mit einer Kopie gearbeitet.
- **Pflichtfelder prüfen:** Neue Produkte benötigen einen Namen, ein Bild und einen Preis ungleich 0. Beim Bearbeiten vorhandener Produkte werden Name und Preis geprüft.
- **Daten exportieren:** Die aktuelle Produktliste und ihre Bilder gemeinsam als `BearbeitetProduktdaten.zip` herunterladen. Diese ZIP kann später wieder importiert werden.

## Verwendung

Der vorhandene Hinweis aus dem [Produktdaten-Release](https://github.com/MtA-08/Data-Manager/releases/tag/v1.0.0) lautet:

> ZIP der Produktdaten um den der App gebaut wurde. Dieser ZIP ist notwendig für das Funktion der App.

Die Web-App und die benötigte ZIP-Datei sind oben verlinkt.

Die JSON-Datei muss **`bearbeiteteProdukte.json`** heißen und direkt auf der obersten Ebene der ZIP liegen. Jeder `bildPfad` muss genau zu einem enthaltenen Bildeintrag passen, zum Beispiel `Bilder/1.jpg`.

Für die Bildanzeige erzeugt die App zusätzlich eine `bildDataUrl`. Diese Vorschau wird nicht als Feld in die JSON-Datei exportiert. Die Bilder werden als eigene Dateien in der ZIP gespeichert.
