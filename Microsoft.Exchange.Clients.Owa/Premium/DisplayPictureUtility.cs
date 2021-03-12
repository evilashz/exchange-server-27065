using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020002BF RID: 703
	internal static class DisplayPictureUtility
	{
		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06001B75 RID: 7029 RVA: 0x0009D4D3 File Offset: 0x0009B6D3
		public static MruDictionaryCache<string, DateTime> RecipientsNegativeCache
		{
			get
			{
				return DisplayPictureUtility.recipientsNegativeCache;
			}
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x0009D4DC File Offset: 0x0009B6DC
		public static bool IsInRecipientsNegativeCache(string emailHash)
		{
			DateTime minValue = DateTime.MinValue;
			if (!DisplayPictureUtility.RecipientsNegativeCache.TryGetValue(emailHash, out minValue))
			{
				return false;
			}
			if (!minValue.Equals(DateTime.MinValue) && minValue.AddMinutes((double)DisplayPictureUtility.NegativeCacheExpirationMinutes).CompareTo(DateTime.UtcNow) <= 0)
			{
				DisplayPictureUtility.RecipientsNegativeCache.Remove(emailHash);
				return false;
			}
			return true;
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x0009D53C File Offset: 0x0009B73C
		public static SetDisplayPictureResult ClearDisplayPicture(UserContext userContext)
		{
			SetDisplayPictureResult setDisplayPictureResult = DisplayPictureUtility.SaveToAD(userContext, true);
			if (setDisplayPictureResult.ResultCode != SetDisplayPictureResultCode.NoError)
			{
				return setDisplayPictureResult;
			}
			string imageSmallHtml = string.Empty;
			string imageLargeHtml = string.Empty;
			using (StringWriter stringWriter = new StringWriter())
			{
				RenderingUtilities.RenderDisplayPictureImage(stringWriter, userContext, string.Empty, 64, true, ThemeFileId.DoughboyPerson);
				imageLargeHtml = stringWriter.ToString();
			}
			using (StringWriter stringWriter2 = new StringWriter())
			{
				RenderingUtilities.RenderDisplayPictureImage(stringWriter2, userContext, string.Empty, 32, true, ThemeFileId.DoughboyPersonSmall);
				imageSmallHtml = stringWriter2.ToString();
			}
			setDisplayPictureResult.SetSuccessResult(imageSmallHtml, imageLargeHtml);
			return setDisplayPictureResult;
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x0009D5EC File Offset: 0x0009B7EC
		public static SetDisplayPictureResult SaveDisplayPicture(UserContext userContext)
		{
			SetDisplayPictureResult setDisplayPictureResult = DisplayPictureUtility.SaveToAD(userContext, false);
			if (setDisplayPictureResult.ResultCode != SetDisplayPictureResultCode.NoError)
			{
				return setDisplayPictureResult;
			}
			string imageSmallHtml = string.Empty;
			string imageLargeHtml = string.Empty;
			string adpictureUrl = RenderingUtilities.GetADPictureUrl(userContext.ExchangePrincipal.LegacyDn, "EX", userContext, true);
			using (StringWriter stringWriter = new StringWriter())
			{
				RenderingUtilities.RenderDisplayPictureImage(stringWriter, userContext, adpictureUrl, 64, true, ThemeFileId.DoughboyPerson);
				imageLargeHtml = stringWriter.ToString();
			}
			using (StringWriter stringWriter2 = new StringWriter())
			{
				RenderingUtilities.RenderDisplayPictureImage(stringWriter2, userContext, adpictureUrl, 32, true, ThemeFileId.DoughboyPersonSmall);
				imageSmallHtml = stringWriter2.ToString();
			}
			setDisplayPictureResult.SetSuccessResult(imageSmallHtml, imageLargeHtml);
			return setDisplayPictureResult;
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x0009D6B0 File Offset: 0x0009B8B0
		public static SetDisplayPictureResult UploadDisplayPicture(HttpPostedFile file, UserContext userContext)
		{
			if (file == null)
			{
				throw new ArgumentNullException("file");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			SetDisplayPictureResult noError = SetDisplayPictureResult.NoError;
			if (file.ContentLength == 0)
			{
				noError.SetErrorResult(SetDisplayPictureResultCode.GeneralError, SanitizedHtmlString.GetNonEncoded(-1496582182));
				return noError;
			}
			int num = 20;
			int num2 = num * 1048576;
			if (file.ContentLength > num2)
			{
				noError.SetErrorResult(SetDisplayPictureResultCode.FileExceedsSizeLimit, SanitizedHtmlString.Format(LocalizedStrings.GetNonEncoded(1511892651), new object[]
				{
					num
				}));
				return noError;
			}
			byte[] uploadedDisplayPicture = DisplayPictureUtility.ProcessImage(file.InputStream, noError);
			if (noError.ResultCode != SetDisplayPictureResultCode.NoError)
			{
				return noError;
			}
			userContext.UploadedDisplayPicture = uploadedDisplayPicture;
			return noError;
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x0009D75C File Offset: 0x0009B95C
		private static SetDisplayPictureResult SaveToAD(UserContext userContext, bool isClearPicture)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			SetDisplayPictureResult noError = SetDisplayPictureResult.NoError;
			byte[] array = null;
			if (!isClearPicture)
			{
				if (userContext.UploadedDisplayPicture == null)
				{
					noError.SetErrorResult(SetDisplayPictureResultCode.SaveError, SanitizedHtmlString.GetNonEncoded(-1306631087));
					return noError;
				}
				array = userContext.UploadedDisplayPicture;
			}
			IRecipientSession recipientSession = Utilities.CreateADRecipientSession(false, ConsistencyMode.IgnoreInvalid, userContext);
			ADRecipient adrecipient = Utilities.CreateADRecipientFromProxyAddress(userContext.ExchangePrincipal.ObjectId, null, recipientSession);
			if (adrecipient == null)
			{
				noError.SetErrorResult(SetDisplayPictureResultCode.GeneralError, SanitizedHtmlString.GetNonEncoded(-1496582182));
				return noError;
			}
			try
			{
				adrecipient.SetProperties(new PropertyDefinition[]
				{
					ADRecipientSchema.ThumbnailPhoto
				}, new object[]
				{
					array
				});
				recipientSession.Save(adrecipient);
			}
			catch (Exception ex)
			{
				noError.SetErrorResult(SetDisplayPictureResultCode.SaveError, SanitizedHtmlString.GetNonEncoded(-1496582182));
				ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "Failed to save display picture to AD  - {0}", ex.Message);
				return noError;
			}
			userContext.UpdateDisplayPictureCanary();
			userContext.UploadedDisplayPicture = null;
			userContext.HasPicture = new bool?(array != null);
			string stringHash = Utilities.GetStringHash(userContext.ExchangePrincipal.LegacyDn);
			if (isClearPicture)
			{
				DisplayPictureUtility.RecipientsNegativeCache[stringHash] = DateTime.UtcNow;
			}
			else if (DisplayPictureUtility.IsInRecipientsNegativeCache(stringHash))
			{
				DisplayPictureUtility.RecipientsNegativeCache.Remove(stringHash);
			}
			return noError;
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x0009D8A8 File Offset: 0x0009BAA8
		private static byte[] ProcessImage(Stream stream, SetDisplayPictureResult result)
		{
			byte[] result2;
			try
			{
				Image image = Image.FromStream(stream);
				if (image == null)
				{
					result.SetErrorResult(SetDisplayPictureResultCode.FailedProcessImage, SanitizedHtmlString.GetNonEncoded(1235679903));
					result2 = null;
				}
				else
				{
					Size imageScaledSize = DisplayPictureUtility.GetImageScaledSize(image.Size);
					Bitmap bitmap = new Bitmap(imageScaledSize.Width, imageScaledSize.Height);
					using (Graphics graphics = Graphics.FromImage(bitmap))
					{
						graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
						graphics.DrawImage(image, 0, 0, imageScaledSize.Width, imageScaledSize.Height);
					}
					PointF imageCenterPosition = DisplayPictureUtility.GetImageCenterPosition((float)imageScaledSize.Width, (float)imageScaledSize.Height);
					RectangleF rect = new RectangleF(imageCenterPosition, new SizeF((float)Math.Min(DisplayPictureUtility.maxImageSize, imageScaledSize.Width), (float)Math.Min(DisplayPictureUtility.maxImageSize, imageScaledSize.Height)));
					Bitmap bitmap2 = bitmap.Clone(rect, bitmap.PixelFormat);
					byte[] array = null;
					using (Stream stream2 = new MemoryStream())
					{
						bitmap2.Save(stream2, ImageFormat.Jpeg);
						stream2.Position = 0L;
						array = DisplayPictureUtility.ReadFromStream(stream2);
					}
					result2 = array;
				}
			}
			catch (Exception ex)
			{
				result.SetErrorResult(SetDisplayPictureResultCode.FailedProcessImage, SanitizedHtmlString.GetNonEncoded(1235679903));
				ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "Failed to process image  - {0}", ex.Message);
				result2 = null;
			}
			return result2;
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x0009DA40 File Offset: 0x0009BC40
		private static Size GetImageScaledSize(Size imageSize)
		{
			Size result = new Size(imageSize.Width, imageSize.Height);
			if (Math.Min(imageSize.Width, imageSize.Height) > DisplayPictureUtility.maxImageSize)
			{
				float num = (float)DisplayPictureUtility.maxImageSize / (float)Math.Min(imageSize.Width, imageSize.Height);
				result.Width = (int)Math.Floor((double)((float)imageSize.Width * num));
				result.Height = (int)Math.Floor((double)((float)imageSize.Height * num));
			}
			return result;
		}

		// Token: 0x06001B7D RID: 7037 RVA: 0x0009DACC File Offset: 0x0009BCCC
		private static PointF GetImageCenterPosition(float width, float height)
		{
			PointF result = new PointF(0f, 0f);
			if (width > height && width > (float)DisplayPictureUtility.maxImageSize)
			{
				result.X = (width - (float)DisplayPictureUtility.maxImageSize) / 2f;
			}
			else if (height > width && height > (float)DisplayPictureUtility.maxImageSize)
			{
				result.Y = (height - (float)DisplayPictureUtility.maxImageSize) / 2f;
			}
			return result;
		}

		// Token: 0x06001B7E RID: 7038 RVA: 0x0009DB34 File Offset: 0x0009BD34
		private static byte[] ReadFromStream(Stream inputStream)
		{
			byte[] array = new byte[32768];
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				int count;
				while ((count = inputStream.Read(array, 0, array.Length)) > 0)
				{
					memoryStream.Write(array, 0, count);
				}
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x040013F2 RID: 5106
		private const int MAX_FILE_SIZE_MB = 20;

		// Token: 0x040013F3 RID: 5107
		private static readonly int NegativeCacheSize = AppSettings.GetConfiguredValue<int>("RecipientsNegativeCacheSize", 200000);

		// Token: 0x040013F4 RID: 5108
		private static readonly int NegativeCacheExpirationMinutes = AppSettings.GetConfiguredValue<int>("RecipientsNegativeCacheExpirationMinutes", 1440);

		// Token: 0x040013F5 RID: 5109
		private static int maxImageSize = 64;

		// Token: 0x040013F6 RID: 5110
		private static MruDictionaryCache<string, DateTime> recipientsNegativeCache = new MruDictionaryCache<string, DateTime>(DisplayPictureUtility.NegativeCacheSize, DisplayPictureUtility.NegativeCacheExpirationMinutes);
	}
}
