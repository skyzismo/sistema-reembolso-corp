import { Component, OnInit, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ThemeService } from './services/theme';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit{

  constructor(public themeService: ThemeService) {}

  ngOnInit(): void {
    this.themeService.initTheme();
  }

  protected readonly title = signal('frontend');
}
