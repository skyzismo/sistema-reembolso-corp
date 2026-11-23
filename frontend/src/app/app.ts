import { Component, OnInit } from '@angular/core';
import { Router, RouterOutlet, RouterLink, NavigationEnd } from '@angular/router';
import { CommonModule } from '@angular/common';
import { filter } from 'rxjs';
import { ThemeService } from './services/theme';
import { AuthService } from './services/auth';
@Component({
  selector: 'app-root', standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink],
  templateUrl: './app.html', styleUrl: './app.css'
})
export class App implements OnInit {
  rotaAtual = '';
  constructor(public themeService: ThemeService, public auth: AuthService, private router: Router) {
    this.router.events.pipe(filter(e => e instanceof NavigationEnd)).subscribe(e => this.rotaAtual = (e as NavigationEnd).urlAfterRedirects);
  }
  ngOnInit(): void { this.themeService.initTheme(); }
  sair(): void { this.auth.logout(); this.router.navigate(['/login']); }
}
