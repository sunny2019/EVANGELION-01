using System.IO;
using Google.Protobuf;
using GxTest;
using Mode;

namespace HotFix
{
    using UnityEngine;

    public class Init
    {
        public static void HotFixInit()
        {
            Debug.Log("Enter HotFix Succeeded !");

            
            gx_data obj = new gx_data()
            {
                ScBool = true,
                ScFloat = 1.03f,
                ScInt32 = 32,
                ScString = "khehehe",
                RepInt32 = {1, 2, 3, 4, 5}
            };
            byte[] bytes = ExProtobuf.Serialize(obj);
            
            gx_data obj2 = ExProtobuf.Deserialize<gx_data>(bytes);
            
            ExProtobuf.DumpAsString(obj2);
        }
    }


   
}