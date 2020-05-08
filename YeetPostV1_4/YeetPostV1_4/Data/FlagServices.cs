using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Grpc.Auth;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using MimeKit;
using MailKit.Net.Smtp;
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


        public string getFLagID(Query query)
        {
            var flagID = "No Flag ID";

            QuerySnapshot querySnapshot = query.GetSnapshotAsync().GetAwaiter().GetResult();
            foreach (DocumentSnapshot queryResult in querySnapshot)
            {
                flagID = queryResult.Id;
            }
            return flagID;
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

            var flagID = getFLagID(query);


            if (flagID == "No Flag ID")
            {
                createNewFlagDoc(userID, yeetID, whoFlags);
            }
            else
            {
                addFlag( whoFlags,flagID, yeetID);
            }

        }


        public void addFlag( List<string> whoFlags,string flagID, string yeetID)
        {

            DocumentReference docRef = db.Collection("Flags").Document(flagID);
            Dictionary<string, object> updates = new Dictionary<string, object>
                {
                    { "whoFlags", whoFlags }
                };

            _ = docRef.UpdateAsync(updates)
                     .GetAwaiter()
                     .GetResult();


            updateFlagYeet(whoFlags, yeetID);

            //sending email
            //if (whoFlags.Count >= 5)
            //{
            //    sendEmail(yeetID);
            //}
        }


        /// <summary>
        /// send an email if it is flagged.
        /// </summary>
        /// <param name="yeetID"></param>
        public void sendEmail(string yeetID)
        {
            var messageToSend = new MimeMessage
            {
                Sender = new MailboxAddress("rj-noreply pls dont", "burgerflipperdestroyer@gmail.com"),
                Subject = "Your Subject",
             };
            messageToSend.To.Add(new MailboxAddress("rudson varona", "rudsonvarona@gmail.com"));
            
            
            messageToSend.Body = new TextPart("plain")
            {
                Text = yeetID + " has gotten more than 5 yeets please check"
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("burgerflipperdestroyer@gmail.com", "Burgerbuns99*");
                client.Send(messageToSend);
                client.Disconnect(true);

            }



        }


        public List<string> addRemoveFlag(List<string> whoFlags, string userID, bool remove, string reason)
        {


            if (remove)
            {

                whoFlags.RemoveAll(x => x.Contains(userID));
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

            //updating the whoFlags                
            updateFlagYeet(whoFlags, yeetID);
        }

        public void updateFlagYeet(List<string> whoFlags, string yeetID)
        {
            DocumentReference docRef = db.Collection("Yeets").Document(yeetID);

            Dictionary<string, object> updates = new Dictionary<string, object>
            {
                    { "whoFlags", whoFlags }
            };

            _ = docRef.UpdateAsync(updates).GetAwaiter().GetResult();

        }
    }
}
