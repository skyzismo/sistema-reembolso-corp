import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home';
import { FormularioComponent } from './pages/formulario/formulario';
import { PainelGestorComponent } from './pages/painel-gestor/painel-gestor';
import { LoginComponent } from './pages/login/login';
import { authGuard, gestorGuard, visitanteGuard } from './guards/auth.guard';

export const routes: Routes = [
  { path: 'login', component: LoginComponent, canActivate: [visitanteGuard] },
  { path: '', component: HomeComponent, canActivate: [authGuard] },
  { path: 'formulario', component: FormularioComponent, canActivate: [authGuard] },
  { path: 'painel-gestor', component: PainelGestorComponent, canActivate: [gestorGuard] },
  { path: '**', redirectTo: '' }
];
