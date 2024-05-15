using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Geolocate{
    
    public class Data{
        public string city { get; set; }
        public string region { get; set; }
        public string country { get; set; }
        public string loc { get; set; }
        public string org { get; set; }
        public string postal { get; set; }
    }
    
    internal class Program{
        static async Task Main(string[] args){
            Console.Title = "IP Geolocator";
            Console.Write("Inserisci l'indirizzo IP: ");
            string ip = Console.ReadLine();
            string url = $"https://ipinfo.io/{ip}/json";

            using(HttpClient client = new HttpClient()){
                try{
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    Console.WriteLine("[+] Richiesta effettuata con successo");

                    string responseData = await response.Content.ReadAsStringAsync();
                    Data ipInfo = JsonConvert.DeserializeObject<Data>(responseData);

                    Console.Clear();
                    Console.WriteLine($"Paese: {ipInfo.country}");
                    Console.WriteLine($"Citta': {ipInfo.city}");
                    Console.WriteLine($"Coordinate: {ipInfo.loc}");
                    Console.WriteLine($"Codice Postale: {ipInfo.postal}");
                    Console.WriteLine($"Regione: {ipInfo.region}");
                    Console.WriteLine($"ISP: {ipInfo.org}");   //Fornitore di Servizi Internet
                    string[] Coords = ipInfo.loc.Split(',');
                    Console.WriteLine($"Google Maps: https://www.googlemaps.com/maps/?q={Coords[0]},{Coords[1]}");
                }
                catch(HttpRequestException ex){
                    Console.WriteLine($"Errore: {ex.Message}");
                }
            }
        }
    }
}