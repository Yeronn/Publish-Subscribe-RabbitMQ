import pika

def callback(ch, method, properties, body):
    print("Mensaje recibido:", body.decode('utf-8'))

# Establecer conexión con RabbitMQ
connection = pika.BlockingConnection(pika.ConnectionParameters('localhost'))
channel = connection.channel()

print("Ingresa 1 para conectar al consumer a un Direct exchange")
print("Ingresa 2 para conectar al consumer a un Topic exchange")
print("Ingresa 3 para conectar al consumer a un Fanout exchange")

print("Recuerda que todos los consumers están conectados a un exchange fanout con nombre 'fanout_exchange'",
      "mediante una cola llamada 'fanout' ")

entrada = input("Ingresa la opcion: ")

exchange_name = input("Ingresa el nombre que deseas ponerle al exchange (No el tipo): ")

queue_name = input("Ingresa el nombre de la cola: ")

if entrada == '3':
    # exchange_name = "fanout_exchange";
    print("Fanout Exchange")
    exchange_type = "fanout";
    # queue_name = 'fanout'
    routing_key = ''
else:
    routing_key = input("Ingresa la routingKey: ")
    if entrada == '1':
        # exchange_name = "direct_exchange"
        print("Direct Exchange")
        exchange_type = 'direct'
        # queue_name = 'mi_cola'
        # routing_key = input("Ingresa la routingKey: ")

    elif entrada == '2':
        # exchange_name = "topic_exchange"
        print("Topic Exchange")
        exchange_type = "topic";
        # queue_name = 'topic'
        # routing_key = input("Ingresa la routingKey: ")



channel.exchange_declare(exchange=exchange_name, exchange_type=exchange_type)
channel.queue_declare(queue=queue_name)
channel.queue_bind(exchange=exchange_name, queue=queue_name, routing_key=routing_key)

if queue_name != 'fanout':
    channel.exchange_declare(exchange='fanout_exchange', exchange_type='fanout')
    # channel.queue_declare(queue='fanout')
    channel.queue_bind(exchange='fanout_exchange', queue=queue_name, routing_key=routing_key)

channel.basic_consume(queue=queue_name, on_message_callback=callback, auto_ack=True)

channel.start_consuming()