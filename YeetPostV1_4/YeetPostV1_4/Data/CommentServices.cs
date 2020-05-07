using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Grpc.Auth;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YeetPostV1_4.DataModel;
using YeetPostV1_4.ViewModel;

namespace YeetPostV1_4.Data
{
    public class CommentServices
    {
        private FirestoreDb db;


        public CommentServices()
        {


            GoogleCredential credential = GoogleCredential
               .FromFile(@"R:\YeetPost\YeetPost2\keys\yeetpost-firestore.json");
            ChannelCredentials channelCredentials = credential.ToChannelCredentials();
            Channel channel = new Channel(FirestoreClient.DefaultEndpoint.ToString(), channelCredentials);
            FirestoreClient firestoreClient = FirestoreClient.Create(channel);
            db = FirestoreDb.Create("yeetpost", client: firestoreClient);
        }



        //before we do this 
        // we need to check the DB
        public CommentViewModel getComment(string yeetID, string guid)
        {

            var comments = new CommentViewModel();

            Query query = db.Collection("Yeets").Document(yeetID).Collection("Comments").OrderBy("dateadded");

            QuerySnapshot querySnapshot = query.GetSnapshotAsync().GetAwaiter().GetResult();
            comments.yeet = getYeetAsync(yeetID, guid);

            if (querySnapshot.Documents.Count() == 0)
            {
                return comments;
            }


            List<Comments> come = new List<Comments>();

            foreach (DocumentSnapshot item in querySnapshot)
            {
                come.Add(new Comments
                {
                    dateadded = item.GetValue<DateTime>("dateadded").ToShortTimeString(),
                    comment = item.GetValue<string>("comment"),
                    Guid = item.GetValue<string>("Guid"),
                    username = item.GetValue<string>("username"),
                    commentID = item.Id,

                });
            }

            //comments.yeet = getYeetAsync(yeetID);
            comments.comments = come;

            return comments;

        }

        public Yeet getYeetAsync(string yeetID, string guid)
        {

            var yeet = new Yeet();

            DocumentReference docRef = db.Collection("Yeets").Document(yeetID);
            DocumentSnapshot querySnapshot = docRef.GetSnapshotAsync().GetAwaiter().GetResult();


            if (querySnapshot.Exists)
            {
                yeet.date = querySnapshot.GetValue<DateTime>("date").ToShortTimeString();
                yeet.username = querySnapshot.GetValue<string>("username");
                yeet.Guid = querySnapshot.GetValue<string>("Guid");
                yeet.header = querySnapshot.GetValue<string>("header");
                yeet.totalLikes = querySnapshot.GetValue<List<string>>("whoLikes").Count().ToString();
                yeet.whoFlags = querySnapshot.GetValue<List<string>>("whoFlags");
                yeet.whoLikes = querySnapshot.GetValue<List<string>>("whoLikes");
                yeet.yeet = querySnapshot.GetValue<string>("yeet");
                yeet.location = querySnapshot.GetValue<string>("location");
                yeet.yeetID = querySnapshot.Id;
                yeet.iLiked = (querySnapshot.GetValue<List<string>>("whoLikes").Any(str => str.Contains(guid)));
                yeet.iFlagged = (querySnapshot.GetValue<List<string>>("whoFlags").Any(str => str.Contains(guid)));
                yeet.isMine = (querySnapshot.GetValue<string>("Guid") == guid) ? true : false;
                yeet.deleteModal = false;
                yeet.modal = false;
            }

            return yeet;

        }

        //add to who likes

        /// <summary>
        /// adding the new comment to the DB
        /// </summary>
        /// <param name="username"></param>
        /// <param name="comment"></param>
        /// <param name="guid"></param>
        /// <param name="yeetId"></param>
        public void newComment(string username, string comment, string guid, string yeetId)
        {
            Query query = db.Collection("Yeets").Document(yeetId).Collection("Comments");

            QuerySnapshot querySnap = query.GetSnapshotAsync().GetAwaiter().GetResult();



            DateTime utcDate = DateTime.UtcNow;
            Dictionary<string, object> Yeet = new Dictionary<string, object>
                {
                  { "Guid", guid},
                  { "comment", comment },
                  { "dateadded", utcDate},
                  { "username", username},


                };

            DocumentReference addedDocRef = db.Collection("Yeets").Document(yeetId)
                .Collection("Comments").AddAsync(Yeet).GetAwaiter().GetResult();

        }







    }
}
