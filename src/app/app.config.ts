import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes'; // Upewnij się, że ścieżka do pliku z trasami jest poprawna

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }), // Użycie zmiany wykrywania
    provideRouter(routes) // Dodanie konfiguracji routingu
  ]
};
