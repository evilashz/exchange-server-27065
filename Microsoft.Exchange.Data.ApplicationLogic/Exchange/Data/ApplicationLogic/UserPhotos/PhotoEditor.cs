using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001F1 RID: 497
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PhotoEditor : IPhotoEditor
	{
		// Token: 0x06001233 RID: 4659 RVA: 0x0004D1A0 File Offset: 0x0004B3A0
		private PhotoEditor()
		{
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x0004D1A8 File Offset: 0x0004B3A8
		public IDictionary<UserPhotoSize, byte[]> CropAndScale(Stream photo)
		{
			if (photo == null)
			{
				throw new ArgumentNullException("photo");
			}
			if (photo.Position != 0L)
			{
				throw new ArgumentException("Position within stream MUST be at the beginning.", "photo");
			}
			Dictionary<UserPhotoSize, Image> allScaleCroppedImages = UserPhotoUtilities.GetAllScaleCroppedImages(photo);
			Dictionary<UserPhotoSize, byte[]> dictionary = new Dictionary<UserPhotoSize, byte[]>();
			IDictionary<UserPhotoSize, byte[]> result;
			try
			{
				foreach (KeyValuePair<UserPhotoSize, Image> keyValuePair in allScaleCroppedImages)
				{
					if (keyValuePair.Value != null)
					{
						using (MemoryStream memoryStream = new MemoryStream())
						{
							keyValuePair.Value.Save(memoryStream, ImageFormat.Jpeg);
							dictionary[keyValuePair.Key] = memoryStream.ToArray();
						}
					}
				}
				result = dictionary;
			}
			finally
			{
				foreach (Image image in allScaleCroppedImages.Values)
				{
					if (image != null)
					{
						image.Dispose();
					}
				}
			}
			return result;
		}

		// Token: 0x040009B2 RID: 2482
		internal static readonly PhotoEditor Default = new PhotoEditor();
	}
}
