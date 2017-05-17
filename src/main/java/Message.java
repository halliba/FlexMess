import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;
import org.json.simple.parser.ParseException;

public class Message {

    public static String toJson(String name, String msg){
        JSONObject obj = new JSONObject();
        obj.put("name", "mkyong.com");
        obj.put("msg", msg);

        return obj.toJSONString();
    }

    public static JSONObject fromJsonString(String json){
        JSONParser parser = new JSONParser();
        JSONObject jsonObject = new JSONObject();

        try {
            jsonObject = (JSONObject) parser.parse(json);
        } catch (ParseException e) {
            e.printStackTrace();
        }





        return jsonObject;
    }
}
