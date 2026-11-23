import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { DespesaService } from '../../services/despesa';
import { Despesa, StatusDespesa, TipoDespesa } from '../../models/despesa.model';
@Component({ selector: 'app-painel-gestor', standalone: true, imports: [CommonModule, FormsModule, RouterModule], templateUrl: './painel-gestor.html' })
export class PainelGestorComponent implements OnInit {
  despesas: Despesa[] = []; carregando = false; erro = ''; sucesso = '';
  nome = ''; email = ''; senha = ''; perfil = 1; mensagemUsuario = ''; erroUsuario = '';
  constructor(private service: DespesaService) {}
  ngOnInit(): void { this.carregar(); }
  carregar(): void { this.carregando = true; this.service.listar().subscribe({ next: d => { this.despesas = d; this.carregando = false; }, error: () => { this.erro = 'Não foi possível carregar as despesas.'; this.carregando = false; } }); }
  tipoLabel(tipo: number): string { return ({1:'Alimentação',2:'Transporte',3:'Hospedagem',4:'Outros'} as Record<number,string>)[tipo] ?? 'Desconhecido'; }
  statusLabel(status: number): string { return ({1:'Aprovada automaticamente',2:'Pendente de aprovação',3:'Aprovada manualmente',4:'Rejeitada'} as Record<number,string>)[status] ?? 'Desconhecido'; }
  classeStatus(status: number): string { return `status status-${status}`; }
  pendente(d: Despesa): boolean { return d.status === StatusDespesa.RequerAprovacaoManual; }
  executar(acao: 'aprovar'|'rejeitar'|'excluir', d: Despesa): void {
    this.erro = ''; this.sucesso = '';
    const req = acao === 'aprovar' ? this.service.aprovar(d.id) : acao === 'rejeitar' ? this.service.rejeitar(d.id) : this.service.excluir(d.id);
    req.subscribe({ next: () => { this.sucesso = acao === 'aprovar' ? 'Despesa aprovada.' : acao === 'rejeitar' ? 'Despesa rejeitada.' : 'Despesa excluída.'; this.carregar(); }, error: err => { this.erro = typeof err.error === 'string' ? err.error : 'Não foi possível executar a operação.'; } });
  }
  cadastrarUsuario(): void {
    this.erroUsuario = ''; this.mensagemUsuario = '';
    this.service.criarUsuario({ nome: this.nome, email: this.email, senha: this.senha, perfil: this.perfil }).subscribe({
      next: () => { this.mensagemUsuario = 'Usuário cadastrado com sucesso.'; this.nome=''; this.email=''; this.senha=''; this.perfil=1; },
      error: err => { this.erroUsuario = err.error?.mensagem ?? 'Não foi possível cadastrar o usuário. Verifique os dados e se o e-mail já existe.'; }
    });
  }
}
