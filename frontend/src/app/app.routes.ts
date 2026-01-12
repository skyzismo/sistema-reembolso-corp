import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home';
import { FormularioComponent } from './pages/formulario/formulario';
import { PainelGestorComponent } from './pages/painel-gestor/painel-gestor';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'formulario', component: FormularioComponent },
  { path: 'painel-gestor', component: PainelGestorComponent }
];
