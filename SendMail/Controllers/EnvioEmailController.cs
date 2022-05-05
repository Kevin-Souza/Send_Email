using Microsoft.AspNetCore.Mvc;
using SendMail.Services;

namespace SendMail.Controllers
{
    public class EnvioEmailController : Controller
    {
        private readonly IEmailSender _emailSender;

        public EnvioEmailController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EnvioEmail(EmailSettings email)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    TesteEnvioEmail(email.Destino, email.Assunto, email.Mensagem);
                    return RedirectToAction("EmailEnviado");
                }
                catch (Exception ex)
                {
                    return RedirectToAction("EmailFalhou");
                }
            }
            return View(email);
        }
        public void TesteEnvioEmail(string email, string assunto, string mensagem)
        {
            try
            {
                //email destino, assunto do email, mensagem a enviar
                var teste = _emailSender.SendEmailAsync(email,assunto,mensagem);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult EmailEnviado()
        {
            return View();
        }
        public ActionResult EmailFalhou()
        {
            return View();
        }
    }
}
