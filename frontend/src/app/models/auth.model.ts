export type PerfilUsuario = 'Funcionario' | 'Gestor';
export interface LoginRequest { email: string; senha: string; }
export interface LoginResponse {
  token: string;
  nome: string;
  email: string;
  perfil: PerfilUsuario;
  expiraEm: string;
}
export interface CriarUsuarioRequest {
  nome: string;
  email: string;
  senha: string;
  perfil: number;
}
