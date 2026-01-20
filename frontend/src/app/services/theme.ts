import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class ThemeService {

  private readonly STORAGE_KEY = 'theme';

  initTheme() {
    const savedTheme = localStorage.getItem(this.STORAGE_KEY);
    if (savedTheme === 'dark') {
      document.body.classList.add('theme-dark');
    }
  }

  toggleTheme() {
    const isDark = document.body.classList.toggle('theme-dark');
    localStorage.setItem(this.STORAGE_KEY, isDark ? 'dark' : 'light');
  }

  isDarkMode(): boolean {
    return document.body.classList.contains('theme-dark');
  }
}
