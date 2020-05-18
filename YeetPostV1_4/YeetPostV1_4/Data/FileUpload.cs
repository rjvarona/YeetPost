using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YeetPostV1_4.ViewModel;
using Firebase.Storage;
using Firebase.Auth;
using System.Threading;


namespace YeetPostV1_4.Data
{
    public class FileUpload
    {
        private readonly IHostingEnvironment _env;
        private static string ApiKey = "AIzaSyCJZTKcHdbHiqSRQZXWtOD2TLMsMkr2YhA";
        private static string Bucket = "yeetpost.appspot.com";
        private static string AuthEmail = "picturetest@gmail.com";
        private static string AuthPassword = "Yeet8*";


        [HttpPost]
        public async Task<string> upload(ViewModel.FIleUploadViewModel model)
        {
            var file = model.File;
            FileStream fs;
            FileStream ms;
            if (file.Length > 0)
            {
                string folderName = "Favicon";
                string path = Path.Combine(_env.WebRootPath, $"images/{folderName}");
                ms = new FileStream(Path.Combine(path, file.FileName), FileMode.Open);
                var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
                var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);

                // you can use CancellationTokenSource to cancel the upload midway
                var cancellation = new CancellationTokenSource();

                var task = new FirebaseStorage(
                    Bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true // when you cancel the upload, exception is thrown. By default no exception is thrown
                    })
                    .Child("receipts")
                    .Child("test")
                    .Child($"aspcore.png")
                    .PutAsync(ms, cancellation.Token);

                task.Progress.ProgressChanged += (s, e) => Console.WriteLine($"Progress: {e.Percentage} %");


                try
                {
                    return "ok";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }

            }
            return "bumba";
        }
    }
}
