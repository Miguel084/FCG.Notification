using FCG.Notification.Application.Interface.Service;
using FCG.Notification.Application.UseCases.Feature.Email.Command.SendEmail;
using FCG.Notification.Application.UseCases.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Notification.Application.UseCases.Feature.Email.Command.SendEmailSaudacao
{
    public class SendEmailPaymentCommandHandler : IRequestHandler<SendEmailPaymentCommand, bool>
    {
        private readonly IEmailService _emailService;
        private readonly GameApiService _gameApiService;
        private readonly UserApiService _userApiService;

        public SendEmailPaymentCommandHandler(IEmailService emailService, GameApiService gameApiService, UserApiService userApiService)
        {
            _emailService = emailService;
            _gameApiService = gameApiService;
            _userApiService = userApiService;
        }

        public async Task<bool> Handle(SendEmailPaymentCommand request, CancellationToken cancellationToken)
        {
            var game = await _gameApiService.GetGameAsync(request.GameId);
            var user = await _userApiService.GetUserAsync(request.UserId);

            try
            {
                var subject = "FCG - Bem-vindo(a)!";
                var body = $@"<!DOCTYPE html>
                            <html lang=""pt-br"">
                            <head>
                                <meta charset=""UTF-8"">
                                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                <style>
                                    /* Estilos Base */
                                    body {{ font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; background-color: #0b0e11; color: #ffffff; margin: 0; padding: 0; }}
                                    .email-container {{ max-width: 600px; margin: 20px auto; background-color: #161a1e; border: 1px solid #2d333b; border-radius: 12px; overflow: hidden; }}
        
                                    /* Cabeçalho */
                                    .header {{ background-color: #1c2128; padding: 30px; text-align: center; border-bottom: 4px solid #f39c12; }}
                                    .logo {{ width: 160px; height: auto; margin-bottom: 10px; }}
        
                                    /* Corpo */
                                    .content {{ padding: 40px 30px; }}
                                    .status-box {{ background-color: #1b3d2a; border: 1px solid #27ae60; color: #2ecc71; padding: 15px; border-radius: 8px; text-align: center; font-weight: bold; margin-bottom: 25px; }}
                                    h2 {{ color: #f39c12; margin-top: 0; }}
                                    p {{ line-height: 1.6; color: #cbd5e0; }}
        
                                    /* Tabela de Itens */
                                    .order-summary {{ width: 100%; border-collapse: collapse; margin-top: 25px; }}
                                    .order-summary th {{ text-align: left; color: #718096; font-size: 12px; text-transform: uppercase; padding-bottom: 10px; }}
                                    .order-summary td {{ padding: 12px 0; border-top: 1px solid #2d333b; }}
                                    .total-row {{ color: #f39c12; font-weight: bold; font-size: 18px; }}

                                    /* Botão */
                                    .btn-cta {{ display: inline-block; padding: 15px 35px; background-color: #f39c12; color: #0b0e11 !important; text-decoration: none; border-radius: 6px; font-weight: bold; margin-top: 30px; }}
        
                                    /* Rodapé */
                                    .footer {{ padding: 25px; text-align: center; font-size: 12px; color: #4a5568; background-color: #0b0e11; }}
                                </style>
                            </head>
                            <body>
                                <div class=""email-container"">
                                    <div class=""header"">
                                        <h1 style=""color: #f39c12; margin: 0;"">FCG GAMES</h1>
                                    </div>
                                    <div class=""content"">
                                        <div class=""status-box"">
                                            PAGAMENTO APROVADO (STATUS: APPROVED)
                                        </div>
                                        <h2>Olá, {user.Name}!</h2>
                                        <p>Boas notícias, O sistema confirmou o seu pagamento e sua compra na <strong>FCG GAMES</strong> está garantida.</p>
                                        <p>Confira abaixo os detalhes da sua aquisição:</p>
                                        <table class=""order-summary"">
                                            <thead>
                                                <tr>
                                                    <th>Produto</th>
                                                    <th align=""right"">Valor</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>{game.Title}</td>
                                                    <td align=""right"">R$ {game.PriceDiscount}</td>
                                                </tr>
                                                <tr>
                                                    <td>Frete / Envio Digital</td>
                                                    <td align=""right"">Grátis</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class=""footer"">
                                        <p>Este é um e-mail automático enviado por FCG GAMES.<br>
                                        Não responda a este e-mail. Para suporte, use: <a href=""mailto:contato@fcggames.com"" style=""color: #f39c12;"">contato@fcggames.com</a></p>
                                        <p>&copy; 2026 FCG GAMES - Level Up Your Life.</p>
                                    </div>
                                </div>
                            </body>
                            </html>
                            ";

                await _emailService.SendAsync(
                    user.Email,
                    subject,
                    body
                );

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error no envio do email: {ex.Message}");
                return false;
            }
        }
    }
}
