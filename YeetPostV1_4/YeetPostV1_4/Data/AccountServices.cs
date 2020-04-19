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

namespace YeetPostV1_4.Data
{
    public class AccountServices
    {

        private FirestoreDb db;


        public AccountServices()
        {
            GoogleCredential credential = GoogleCredential
               .FromFile(@"R:\YeetPost\YeetPost2\keys\yeetpost-firestore.json");
            ChannelCredentials channelCredentials = credential.ToChannelCredentials();
            Channel channel = new Channel(FirestoreClient.DefaultEndpoint.ToString(), channelCredentials);
            FirestoreClient firestoreClient = FirestoreClient.Create(channel);
            db = FirestoreDb.Create("yeetpost", client: firestoreClient);

        }

/// <summary>
/// adding a user to the bank
/// </summary>
/// <param name="AccountContext"></param>
/// <returns></returns>
        public async Task addUserToFireStoreAsync(AspNetUsers AccountContext)
        {

            Query query = db.Collection("Users");

            QuerySnapshot querySnap = query.GetSnapshotAsync().GetAwaiter().GetResult();

            if(AccountContext.PasswordHash == null)
            {
                AccountContext.PasswordHash = "OAuthenticated"; 
            }

            //first location is always chattanooga
            Dictionary<string, object> newUser = new Dictionary<string, object>
                {
                  { "username", AccountContext.UserName },
                  { "email", AccountContext.Email },
                  { "password", AccountContext.PasswordHash},
                  { "Guid", AccountContext.Id},
                  { "location", "chattanooga"   },
                  { "status", "approved"   },

                };


            DocumentReference addedDocRef2 = db.Collection("Users").Document(AccountContext.Id);
                
            await addedDocRef2.SetAsync(newUser);
          

        }


        /// <summary>
        /// Getting the location of the user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string getLocation(string userId)
        {

            CollectionReference collection = db.Collection("Users");


            Query query = collection.WhereEqualTo("Guid", userId);

            QuerySnapshot querySnapshot = query.GetSnapshotAsync().GetAwaiter().GetResult();

            List<AspNetUsers> userProfile = new List<AspNetUsers>();

            foreach (DocumentSnapshot queryResult in querySnapshot)
            {
                userProfile.Add(new AspNetUsers
                {
                    location = queryResult.GetValue<string>("location"),

                });

           }

            return userProfile[0].location;
        }

        public string getStatus(string userId)
        {
            CollectionReference collection = db.Collection("Users");


            Query query = collection.WhereEqualTo("Guid", userId);

            QuerySnapshot querySnapshot = query.GetSnapshotAsync().GetAwaiter().GetResult();

            List<AspNetUsers> userProfile = new List<AspNetUsers>();

            foreach (DocumentSnapshot queryResult in querySnapshot)
            {
                userProfile.Add(new AspNetUsers
                {
                    status = queryResult.GetValue<string>("status"),

                });

            }

            return userProfile[0].status;

        }



    }
}
