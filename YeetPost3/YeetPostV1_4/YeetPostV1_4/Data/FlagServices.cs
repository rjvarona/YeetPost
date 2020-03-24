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
    public class FlagServices
    {
        private FirestoreDb db;

        public FlagServices()
        {

            GoogleCredential credential = GoogleCredential
               .FromFile(@"R:\YeetPost\YeetPost2\keys\yeetpost-firestore.json");
            ChannelCredentials channelCredentials = credential.ToChannelCredentials();
            Channel channel = new Channel(FirestoreClient.DefaultEndpoint.ToString(), channelCredentials);
            FirestoreClient firestoreClient = FirestoreClient.Create(channel);
            db = FirestoreDb.Create("yeetpost", client: firestoreClient);
        }


        /// <summary>
        /// flagging a post
        /// </summary>
        /// <param name="yeetID"></param>
        /// <param name="userID"></param>
        /// <param name="whoFlags"></param>
        /// <param name="remove"></param>
        /// <param name="reason"></param>
        public void flagPost(string yeetID, string userID, List<string> whoFlags, bool remove, string reason)
        {
           
            whoFlags = addRemoveFlag(whoFlags, userID, remove, reason);
            Query query = db.Collection("Flags").WhereEqualTo("yeetID", yeetID);


            if (query == null)
            {
                createNewFlagDoc(userID, yeetID, whoFlags);
            }
            else
            {
                addFlag(query, whoFlags);
            }
        }

        public void addFlag(Query query, List<string> whoFlags)
        {
            var flagID = "x";

                QuerySnapshot querySnapshot = query.GetSnapshotAsync().GetAwaiter().GetResult();

                foreach (DocumentSnapshot queryResult in querySnapshot)
                {
                    flagID = queryResult.Id;
                }

                DocumentReference docRef = db.Collection("Flags").Document(flagID);
                Dictionary<string, object> updates = new Dictionary<string, object>
                {
                    { "whoLikes", whoFlags }
                };

                _ = docRef.UpdateAsync(updates)
                         .GetAwaiter()
                         .GetResult();
        }


        public List<string> addRemoveFlag(List<string> whoFlags, string userID, bool remove, string reason)
        {


            if (remove)
            {

                whoFlags.RemoveAll(x => x.Contains(userID + ":::" + reason));
            }
            else
            {
                whoFlags.Add(userID + ":::" + reason);
            }

            return whoFlags;
        }


        public void createNewFlagDoc(string userID, string yeetID, List<string> whoFlags)
        {
            Query query = db.Collection("Flags");

            Dictionary<string, object> Flag = new Dictionary<string, object>
                {
                  { "GUID", userID},
                  { "yeetID", yeetID},
                  { "whoFlags", whoFlags }
                };

            DocumentReference addedDocRef = db.Collection("Flags").AddAsync(Flag).GetAwaiter().GetResult();
        }
    }
}
