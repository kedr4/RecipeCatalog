

namespace UserService.Infrastructure.Email
{
    public interface IEmailService
    {
        /// <summary>
        /// Отправляет электронное письмо с указанными параметрами.
        /// </summary>
        /// <param name="to">Адрес электронной почты получателя.</param>
        /// <param name="subject">Тема письма.</param>
        /// <param name="body">Тело письма.</param>
        /// <returns>Значение true, если письмо отправлено успешно; в противном случае false.</returns>
        Task<bool> SendEmailAsync(string to, string subject, string body);
    }
}

