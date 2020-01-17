using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Grpc.Auth;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YeetPostV1_4.DataModels;
using YeetPostV1_4.DataModel;


namespace YeetPostV1_4.Data
{
    public class YeetServices
    {

        private FirestoreDb db;


        public YeetServices()
        {
            GoogleCredential credential = GoogleCredential
               .FromFile(@"R:\YeetPost\YeetPost2\keys\yeetpost-firestore.json");
            ChannelCredentials channelCredentials = credential.ToChannelCredentials();
            Channel channel = new Channel(FirestoreClient.DefaultEndpoint.ToString(), channelCredentials);
            FirestoreClient firestoreClient = FirestoreClient.Create(channel);
            db = FirestoreDb.Create("yeetpost", client: firestoreClient);

        }


        /// <summary>
        /// Getting Users
        /// </summary>
        /// <returns></returns>
        public List<Yeet> GetYeets(string location)
        {

            if (location == null) { location = "chattanooga";}
            
            CollectionReference collection = db.Collection("Yeets");


            Query query = collection.WhereEqualTo("location", location);

            QuerySnapshot querySnapshot = query.GetSnapshotAsync().GetAwaiter().GetResult();


            List<Yeet> yeets = new List<Yeet>();

            foreach (DocumentSnapshot queryResult in querySnapshot)
            {
                yeets.Add(new Yeet
                {
                    date = queryResult.GetValue<DateTime>("date"),
                    username = queryResult.GetValue<string>("username"),
                    Guid = queryResult.GetValue<string>("Guid"),
                    header = queryResult.GetValue<string>("header"),
                    totalLikes = queryResult.GetValue<string>("totalLikes"),
                    whoLikes = queryResult.GetValue<List<string>>("whoLikes"),
                    yeet = queryResult.GetValue<string>("yeet"),
                });

            }
            return yeets;
        }

      
    }
}
