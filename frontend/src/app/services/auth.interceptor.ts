import { HttpInterceptorFn } from '@angular/common/http';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const token = localStorage.getItem('reembolso.auth');
  if (!token) return next(req);
  try {
    const sessao = JSON.parse(token) as { token?: string; expiraEm?: string };
    if (!sessao.token || !sessao.expiraEm || new Date(sessao.expiraEm).getTime() <= Date.now()) {
      localStorage.removeItem('reembolso.auth');
      return next(req);
    }
    return next(req.clone({ setHeaders: { Authorization: `Bearer ${sessao.token}` } }));
  } catch {
    localStorage.removeItem('reembolso.auth');
    return next(req);
  }
};
