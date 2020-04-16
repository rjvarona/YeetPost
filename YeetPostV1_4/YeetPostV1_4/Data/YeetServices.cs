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
using System.Security.Claims;


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
        public List<Yeet> GetYeetsByNew(string location, string guid)
        {

            if (location == null) { location = "chattanooga";}
            
            CollectionReference collection = db.Collection("Yeets");


            Query query = collection.WhereEqualTo("location", location).OrderByDescending("date");

            QuerySnapshot querySnapshot = query.GetSnapshotAsync().GetAwaiter().GetResult();


            List<Yeet> yeets = new List<Yeet>();

            foreach (DocumentSnapshot queryResult in querySnapshot)
            {
                yeets.Add(new Yeet
                {
                    date = queryResult.GetValue<DateTime>("date").ToShortTimeString(),
                    username = queryResult.GetValue<string>("username"),
                    Guid = queryResult.GetValue<string>("Guid"),
                    header = queryResult.GetValue<string>("header"),
                    totalLikes = queryResult.GetValue<List<string>>("whoLikes").Count().ToString(),
                    whoLikes = queryResult.GetValue<List<string>>("whoLikes"),
                    whoFlags = queryResult.GetValue<List<string>>("whoFlags"),
                    yeet = queryResult.GetValue<string>("yeet"),
                    location = queryResult.GetValue<string>("location"),
                    yeetID = queryResult.Id,
                    iLiked = (queryResult.GetValue<List<string>>("whoLikes").Any(str => str.Contains(guid))),
                    iFlagged = (queryResult.GetValue<List<string>>("whoFlags").Any(str => str.Contains(guid))),
                    isMine = (queryResult.GetValue<string>("Guid") == guid) ? true : false,
                    deleteModal =  false,
                    modal = false,
                });

            }
            return yeets;
        }


        /// <summary>
        /// Getting Users
        /// </summary>
        /// <returns></returns>
        public List<Yeet> GetYeetsById( string guid)
        {


            CollectionReference collection = db.Collection("Yeets");


            Query query = collection.WhereEqualTo("Guid", guid).OrderByDescending("date");

            QuerySnapshot querySnapshot = query.GetSnapshotAsync().GetAwaiter().GetResult();


            List<Yeet> yeets = new List<Yeet>();

            foreach (DocumentSnapshot queryResult in querySnapshot)
            {
                yeets.Add(new Yeet
                {
                    date = queryResult.GetValue<DateTime>("date").ToShortTimeString(),
                    username = queryResult.GetValue<string>("username"),
                    Guid = queryResult.GetValue<string>("Guid"),
                    header = queryResult.GetValue<string>("header"),
                    totalLikes = queryResult.GetValue<List<string>>("whoLikes").Count().ToString(),
                    whoLikes = queryResult.GetValue<List<string>>("whoLikes"),
                    whoFlags = queryResult.GetValue<List<string>>("whoFlags"),
                    yeet = queryResult.GetValue<string>("yeet"),
                    location = queryResult.GetValue<string>("location"),
                    yeetID = queryResult.Id,
                    iLiked = (queryResult.GetValue<List<string>>("whoLikes").Any(str => str.Contains(guid))),
                    iFlagged = (queryResult.GetValue<List<string>>("whoFlags").Any(str => str.Contains(guid))),
                    isMine = (queryResult.GetValue<string>("Guid") == guid) ? true : false,
                    deleteModal =  false,
                    modal = false,
                });

            }
            return yeets;
        }



        public List<Yeet> GetYeetsByTrend(string location, string guid)
        {

            if (location == null) { location = "chattanooga"; }

            CollectionReference collection = db.Collection("Yeets");


            Query query = collection.WhereEqualTo("location", location).OrderByDescending("totalLikes");
            QuerySnapshot querySnapshot = query.GetSnapshotAsync().GetAwaiter().GetResult();


            List<Yeet> yeets = new List<Yeet>();

            foreach (DocumentSnapshot queryResult in querySnapshot)
            {
                yeets.Add(new Yeet
                {
                    date = queryResult.GetValue<DateTime>("date").ToShortTimeString(),
                    username = queryResult.GetValue<string>("username"),
                    Guid = queryResult.GetValue<string>("Guid"),
                    header = queryResult.GetValue<string>("header"),
                    totalLikes = queryResult.GetValue<List<string>>("whoLikes").Count().ToString(),
                    whoLikes = queryResult.GetValue<List<string>>("whoLikes"),
                    whoFlags = queryResult.GetValue<List<string>>("whoFlags"),
                    yeet = queryResult.GetValue<string>("yeet"),
                    location = queryResult.GetValue<string>("location"),
                    yeetID = queryResult.Id,
                    iLiked = (queryResult.GetValue<List<string>>("whoLikes").Any(str => str.Contains(guid))),
                    iFlagged = (queryResult.GetValue<List<string>>("whoFlags").Any(str => str.Contains(guid))),
                    isMine = (queryResult.GetValue<string>("Guid") == guid) ? true : false,
                    deleteModal =  false,
                    modal = false,
                });

            }
            return yeets;
        }





        public void newYeet(string header, string yeet, string location, string userId, string name)
        {
            Query query = db.Collection("Yeets");

            QuerySnapshot querySnap = query.GetSnapshotAsync().GetAwaiter().GetResult();
            string[] whoLikes = new string[] { "id1" };
            string[] whoFlags = new string[] { "id1" };


            DateTime utcDate = DateTime.UtcNow;
            Dictionary<string, object> Yeet = new Dictionary<string, object>
                {
                  { "header", header},
                  { "yeet", yeet },
                  { "totalLikes", "1"},
                  { "whoLikes", whoLikes},
                  { "whoFlags", whoFlags},
                  {"date", utcDate},
                  { "location", location },
                  {"Guid", userId },
                  {"username", name },


                };

            DocumentReference addedDocRef =  db.Collection("Yeets").AddAsync(Yeet).GetAwaiter().GetResult();

        }




    }
}
