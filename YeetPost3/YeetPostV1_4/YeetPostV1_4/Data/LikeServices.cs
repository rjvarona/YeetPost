using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Grpc.Auth;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YeetPostV1_4.Data
{
    public class LikeServices
    {
        private FirestoreDb db;


        public LikeServices()
        {


            GoogleCredential credential = GoogleCredential
               .FromFile(@"R:\YeetPost\YeetPost2\keys\yeetpost-firestore.json");
            ChannelCredentials channelCredentials = credential.ToChannelCredentials();
            Channel channel = new Channel(FirestoreClient.DefaultEndpoint.ToString(), channelCredentials);
            FirestoreClient firestoreClient = FirestoreClient.Create(channel);
            db = FirestoreDb.Create("yeetpost", client: firestoreClient);
        }


        public void likePost(string yeetID, string userID, List<string> whoLikes, bool remove)
        {
            whoLikes = addRemoveLike(whoLikes, userID, remove);


            DocumentReference docRef = db.Collection("Yeets").Document(yeetID);

            Dictionary<string, object> updates = new Dictionary<string, object>
            {
                    { "whoLikes", whoLikes }
            };

            _ = docRef.UpdateAsync(updates).GetAwaiter().GetResult();


        }

        public List<string> addRemoveLike(List<string> whoLikes, string userID, bool remove)
        {

            if(remove)
            {

                whoLikes.RemoveAll(x => x.Contains(userID));
            }
            else
            {
                whoLikes.Add(userID);
            }

            return whoLikes;
        }

    }
}
