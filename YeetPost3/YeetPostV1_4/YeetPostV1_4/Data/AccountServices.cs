﻿using Google.Apis.Auth.OAuth2;
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
        /// Getting Users
        /// </summary>
        /// <returns></returns>
        public List<Account> GetUsers()
        {


            CollectionReference collection = db.Collection("Users");


            Query query = collection;

            QuerySnapshot querySnapshot = query.GetSnapshotAsync().GetAwaiter().GetResult();


            List<Account> users = new List<Account>();

            foreach (DocumentSnapshot queryResult in querySnapshot)
            {
                users.Add(new Account
                {
                    username = queryResult.GetValue<string>("username"),
                    dateCreated = queryResult.GetValue<DateTime>("dateCreated"),

                });

            }
            return users;
        }

        public async Task addUserToFireStoreAsync(AspNetUsers AccountContext)
        {

            Query query = db.Collection("Users");

            QuerySnapshot querySnap = query.GetSnapshotAsync().GetAwaiter().GetResult();

            if(AccountContext.PasswordHash == null)
            {
                AccountContext.PasswordHash = "OAuthenticated"; 
            }


            Dictionary<string, object> newUser = new Dictionary<string, object>
                {
                  { "username", AccountContext.UserName },
                  { "password", AccountContext.PasswordHash},
                  { "Guid", AccountContext.Id}

                };


            DocumentReference addedDocRef2 = db.Collection("Users").Document(AccountContext.Id);
                
            await addedDocRef2.SetAsync(newUser);
           


            //}


        }
    }
}