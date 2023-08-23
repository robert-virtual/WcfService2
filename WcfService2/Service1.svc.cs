using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Text.Json;

namespace WcfService2
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            var client = new HttpClient();
            var values = new Dictionary<string, string>
            {
                {"msg","Hola mundo desde WCF" }
            };
            var content = new FormUrlEncodedContent(values);
            var res = client.PostAsync("http://localhost:5001/fir-100s/us-central1/ping",content).Result;
            PingBody pingBody = JsonSerializer.DeserializeAsync<PingBody>(res.Content.ReadAsStreamAsync().Result).Result;

            //return res.Content.ReadAsStringAsync().Result;
            return pingBody.Msg;
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
