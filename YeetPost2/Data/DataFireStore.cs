using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Grpc.Auth;
using Grpc.Core;

namespace YeetPost2.Data
{
    public class DataFireStore
    {
        private FirestoreDb db;


        public DataFireStore()
        {
            GoogleCredential credential = GoogleCredential
               .FromFile(@"R:\YeetPost\YeetPost2\keys\yeetpost-firestore.json");
            ChannelCredentials channelCredentials = credential.ToChannelCredentials();
            Channel channel = new Channel(FirestoreClient.DefaultEndpoint.ToString(), channelCredentials);
            FirestoreClient firestoreClient = FirestoreClient.Create(channel);
            db = FirestoreDb.Create("YeetPost", client: firestoreClient);

        }


        /// <summary>
        /// Getting Users
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<DocumentSnapshot> GetSnapshotInstance()
        {
            // Create a document with a random ID in the "users" collection.
            CollectionReference collection = db.Collection("labItems");

            // Query the collection for all documents where doc.Born < 1900.
            Query query = collection;
            QuerySnapshot querySnapshot = query.GetSnapshotAsync().GetAwaiter().GetResult();


            return querySnapshot;
        }




    }
}
