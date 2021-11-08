using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;
using System.IO;


namespace PhotoSharingApplication.Models
{
    public class PhotoSharingInitializer : DropCreateDatabaseAlways <PhotoSharingContext>
    {
        private byte[] getFileBytes(string path)
        {
            FileStream fileOnDisk = new FileStream(HttpRuntime.AppDomainAppPath + path, FileMode.Open);
            byte[] fileBytes;
            using (BinaryReader br = new BinaryReader(fileOnDisk))
            {
                fileBytes = br.ReadBytes((int)fileOnDisk.Length);
            }
            return fileBytes;
        }

        protected override void Seed(PhotoSharingContext context)
        {
            base.Seed(context);

            var photos = new List<Photo>
            {
                new Photo {
                    Title = "Me standing on top of a mountain",
                    Description = "I was very impressed with myself",
                    UserName = "Fred",
                    PhotoFile = getFileBytes("\\Images\\flower.jpg"),
                    ImageMimeType = "image/jpeg",
                    CreatedDate = DateTime.Today
                },

            };

            photos.ForEach(s => context.Photos.Add(s));
            context.SaveChanges();

            var comments = new List<Comment>
            {
                new Comment {
                    PhotoID = 1,
                    UserName = "Bert",
                    Subject = "A Big Mountain",
                    Body = "That looks like a very high mountain you have climbed"
                },
            };

            comments.ForEach(s => context.Comments.Add(s));
            context.SaveChanges();
        }
    }
}
