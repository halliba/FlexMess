

import org.json.simple.JSONObject;

import javax.swing.*;

public class Main {

    public static void main(String[] args) {
        System.out.println("Hello World!");

        Thread discoveryThread = new Thread(DiscoveryThread.getInstance());
        discoveryThread.start();

        Client client = new Client();

        String msg = Message.toJson("Sergej","was geht");

        System.out.println(msg);

        JSONObject o = Message.fromJsonString(msg);
        Timer timer = new Timer(3000, evt -> {
            client.send(msg);
        });

        timer.start();


    }


}
