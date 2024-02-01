using System.Net;

namespace WebAppComercial.Web.Repositories
{
    public class HttpResponseWrapper<T>
    {
        //------------------------- Atributos Privados -------------------------------

        //------------------------- Constructor --------------------------------------
        public HttpResponseWrapper(T? response, bool error, HttpResponseMessage httpResponseMessage)
        {
            Error = error;
            Response = response;
            HttpResponseMessage = httpResponseMessage;
        }

        //------------------------- Propiedades Públicas -----------------------------
        public bool Error { get; set; }
        public T? Response { get; set; }
        public HttpResponseMessage HttpResponseMessage { get; set; }

        //------------------------- Métodos Públicos ---------------------------------
        public async Task<string?> GetErrorMessage()
        {
            if (!Error)
            {
                return null;
            }

            var statusCode = HttpResponseMessage.StatusCode;
            if (statusCode == HttpStatusCode.NotFound)
            {
                return "Recurso no encontrado";
            }
            else if (statusCode == HttpStatusCode.BadRequest)
            {
                return await HttpResponseMessage.Content.ReadAsStringAsync();
            }
            else if (statusCode == HttpStatusCode.Unauthorized)
            {
                return "Tienes que loguearte para hacer esta operación";
            }
            else if (statusCode == HttpStatusCode.Forbidden)
            {
                return "No tienes permisos para hacer esta operación";
            }
            return "Ha ocurrido un error inesperado";
        }

        //------------------------- Métodos Privados --------------------------------
    }
}