using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Assignment4
{
    public class Function
    {
        private static AmazonDynamoDBClient client = new AmazonDynamoDBClient();
        //setting table name
        private string tableName = "Assignment4";

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<string> FunctionHandler(APIGatewayProxyRequest input, ILambdaContext context)
        {

            //declaring dictionary for items to send to db
            Dictionary<string, AttributeValue> myDictionary = new Dictionary<string, AttributeValue>();

            //for deserializing input obj if neeeded
            //obj = JsonConvert.DeserializeObject<myJSONBody>(input.Body);

            //adding item to dict
            //specifiying what type of data is being inserted to db AKA SHORTHAND
            myDictionary.Add("id", new AttributeValue() { S = input.Body.ToUpper() });
            PutItemRequest myRequest = new PutItemRequest(tableName, myDictionary);
            //async struct
            PutItemResponse res = await client.PutItemAsync(myRequest);
            return input.Body.ToUpper();





        }
    }
}
