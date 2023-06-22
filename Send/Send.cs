using RabbitMQ.Client;
using System;
using System.Text;

namespace Send
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true){

            
                string input = "";
                string message = "";
                string exchangeName = "";
                string exchangeType = "";
                string routingKey = "";
                
                Console.WriteLine("Ingresa 1 para crear un exchange Direct");
                Console.WriteLine("Ingresa 2 para crear un exchange Topic");
                Console.WriteLine("Ingresa 3 para crear un exchange Fanout");
                Console.WriteLine("Presiona otra tecla para salir");
                input = Console.ReadLine();

                if (input != "1" && input != "2" && input != "3")
                {
                    break;
                }

                Console.WriteLine("Ingresa El nombre del exchange");
                exchangeName = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        // exchangeName = "direct_exchange";
                        exchangeType = "direct";

                        Console.WriteLine("Ingresa la routingKey: ");
                        routingKey = Console.ReadLine();
                        break;

                    case "2":
                        // exchangeName = "topic_exchange";
                        exchangeType = "topic";

                        Console.WriteLine("Ingresa la routingKey: ");
                        routingKey = Console.ReadLine();
                        break;

                    case "3":
                        // exchangeName = "fanout_exchange";
                        exchangeType = "fanout";
                        break;

                    default:
                        break;
                }

                Console.WriteLine("Ingresa un mensaje: ");
                message = Console.ReadLine();


                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.ExchangeDeclare(exchange: exchangeName, type: exchangeType);
                        
                        //message = "Hola, esto es un mensaje para 1";
                        var body = Encoding.UTF8.GetBytes(message);
                        //var routingKey = "mi_ruta";

                        // Console.WriteLine("Presiona una tecla para enviar el mensaje");
                        // var aux = Console.ReadLine();

                        channel.BasicPublish(exchange: exchangeName, routingKey: routingKey, basicProperties: null, body: body);
                        Console.WriteLine("1 Mensaje publicado: {0}", message);
                    }
                }

                Console.WriteLine("Presiona 1 para continuar u otra tecla para salir...");
                input = Console.ReadLine();
                if (input != "1"){
                    break;
                }
            }
            Console.WriteLine("El programa se ha detenido.");
        }
    }
}
