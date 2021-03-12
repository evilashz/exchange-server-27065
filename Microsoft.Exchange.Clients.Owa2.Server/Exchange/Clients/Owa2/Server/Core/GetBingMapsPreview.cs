using System;
using System.IO;
using System.Net;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200030D RID: 781
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class GetBingMapsPreview : ServiceCommand<GetBingMapsPreviewResponse>
	{
		// Token: 0x060019EF RID: 6639 RVA: 0x0005D64E File Offset: 0x0005B84E
		public GetBingMapsPreview(CallContext callContext, GetBingMapsPreviewRequest request) : base(callContext)
		{
			this.request = request;
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x0005D660 File Offset: 0x0005B860
		private static byte[] GetContentFromStream(Stream stream, int maxBytesToRead, out int numBytesRead)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				byte[] array = new byte[4096];
				int num = 0;
				int num2;
				while ((num2 = stream.Read(array, 0, array.Length)) != 0 && num <= maxBytesToRead)
				{
					memoryStream.Write(array, 0, num2);
					num += num2;
				}
				memoryStream.Position = 0L;
				if (num == 0)
				{
					throw new OwaException("ImageAttachmentDataNullError");
				}
				if (num > maxBytesToRead)
				{
					throw new OwaException("ImageMaxStreamSizeExceededError");
				}
				byte[] array2 = memoryStream.ToArray();
				if (array2 == null)
				{
					throw new OwaException("ImageAttachmentDataNullError");
				}
				numBytesRead = num;
				result = array2;
			}
			return result;
		}

		// Token: 0x060019F1 RID: 6641 RVA: 0x0005D708 File Offset: 0x0005B908
		protected override GetBingMapsPreviewResponse InternalExecute()
		{
			string str = "Function start";
			GetBingMapsPreviewResponse result;
			try
			{
				string requestString = GetBingMapsPreview.BuildImageRequestString(this.request.Latitude, this.request.Longitude, this.request.LocationName, this.request.MapWidth, this.request.MapHeight, this.request.MapControlKey);
				str = "Executing web request";
				using (HttpWebResponse httpWebResponse = this.ExecuteWebRequest(requestString))
				{
					if (httpWebResponse == null)
					{
						throw new OwaException("BingMapsRequestNullError");
					}
					str = "Inspecting response status code";
					if (httpWebResponse.StatusCode != HttpStatusCode.OK)
					{
						result = GetBingMapsPreview.CreateErrorResponse("WebResponse status code: " + httpWebResponse.StatusCode, httpWebResponse.StatusDescription);
					}
					else
					{
						if (httpWebResponse.ContentLength > 204800L)
						{
							throw new OwaException("ImageMaxStreamSizeExceededError");
						}
						str = "Getting attachment data from the response";
						int imageDataSize = 0;
						Stream responseStream = httpWebResponse.GetResponseStream();
						if (responseStream == null)
						{
							throw new OwaException("WebResponseStreamNullError");
						}
						byte[] contentFromStream = GetBingMapsPreview.GetContentFromStream(responseStream, 204800, out imageDataSize);
						string imageData = Convert.ToBase64String(contentFromStream);
						str = "Building response with encoded image data string";
						result = new GetBingMapsPreviewResponse
						{
							ImageData = imageData,
							ImageDataSize = imageDataSize,
							ImageDataType = "image/jpeg"
						};
					}
				}
			}
			catch (OwaException exception)
			{
				result = GetBingMapsPreview.CreateErrorResponse(exception);
			}
			catch (NullReferenceException ex)
			{
				result = GetBingMapsPreview.CreateErrorResponse("NullReferenceException", "Null reference exception in GetBingMapsPreview InternalExecute. The last executed step was: " + str + ". Details: " + ex.Message);
			}
			return result;
		}

		// Token: 0x060019F2 RID: 6642 RVA: 0x0005D8C8 File Offset: 0x0005BAC8
		private static GetBingMapsPreviewResponse CreateErrorResponse(string error, string errorMessage)
		{
			return new GetBingMapsPreviewResponse
			{
				Error = error,
				ErrorMessage = errorMessage
			};
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x0005D8EC File Offset: 0x0005BAEC
		private static GetBingMapsPreviewResponse CreateErrorResponse(OwaException exception)
		{
			if (exception.Message == "ImageMaxStreamSizeExceededError")
			{
				return GetBingMapsPreview.CreateErrorResponse("ImageMaxStreamSizeExceededError", "The image size exceeded the maximum size");
			}
			if (exception.Message == "ImageAttachmentDataNullError")
			{
				return GetBingMapsPreview.CreateErrorResponse("ImageAttachmentDataNullError", "Image attachment data was null");
			}
			if (exception.Message == "CreateAttachmentRequestError")
			{
				return GetBingMapsPreview.CreateErrorResponse("CreateAttachmentRequestError", "Error building CreateAttachmentRequest");
			}
			if (exception.Message == "CreateAttachmentResultNullError")
			{
				return GetBingMapsPreview.CreateErrorResponse("CreateAttachmentResultNullError", "No response message from CreateAttachmentHelper.CreateAttachment");
			}
			if (exception.Message == "BingMapsRequestNullError")
			{
				return GetBingMapsPreview.CreateErrorResponse("BingMapsRequestNullError", "Request to Bing Maps to get the map image returned null");
			}
			if (exception.Message == "WebRequestNullError")
			{
				return GetBingMapsPreview.CreateErrorResponse("WebRequestNullError", "The HttpWebRequest created to query Bing Maps was null");
			}
			if (exception.Message == "WebResponseStreamNullError")
			{
				return GetBingMapsPreview.CreateErrorResponse("WebResponseStreamNullError", "The response stream for the maps image was null");
			}
			if (exception.InnerException != null)
			{
				return GetBingMapsPreview.CreateErrorResponse(exception.Message, exception.InnerException.Message);
			}
			return GetBingMapsPreview.CreateErrorResponse(exception.Message, exception.ToString());
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x0005DA18 File Offset: 0x0005BC18
		private CreateAttachmentRequest CreateImageAttachmentRequest(byte[] attachmentData)
		{
			CreateAttachmentRequest createAttachmentRequest = null;
			if (attachmentData != null)
			{
				string fileName = "BingMap";
				string contentType = "image/jpeg";
				createAttachmentRequest = CreateAttachmentHelper.CreateAttachmentRequest(this.request.ParentItemId, fileName, attachmentData.Length, contentType, attachmentData, true, null);
			}
			if (createAttachmentRequest == null)
			{
				throw new OwaException("CreateAttachmentRequestError");
			}
			return createAttachmentRequest;
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x0005DA60 File Offset: 0x0005BC60
		private static string BuildImageRequestString(string latitude, string longitude, string name, int mapWidth, int mapHeight, string apiKey)
		{
			if (apiKey == null)
			{
				apiKey = "Ah3Ntem0Es72C4bf193UI3hJ1CgJRGhDjnosFUNKSQlzj-6MViA2NyaNuroXCa4S";
			}
			return string.Concat(new object[]
			{
				"https://dev.virtualearth.net/REST/v1/Imagery/Map/Road/",
				latitude,
				",",
				longitude,
				"/15?mapSize=",
				mapWidth,
				",",
				mapHeight,
				"&pp=",
				latitude,
				",",
				longitude,
				";21;",
				(name != null) ? name : "",
				"&format=jpeg&key=",
				apiKey
			});
		}

		// Token: 0x060019F6 RID: 6646 RVA: 0x0005DB08 File Offset: 0x0005BD08
		private HttpWebResponse ExecuteWebRequest(string requestString)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(requestString);
			if (httpWebRequest == null)
			{
				throw new OwaException("WebRequestNullError");
			}
			httpWebRequest.Method = "GET";
			HttpWebResponse result;
			try
			{
				result = (httpWebRequest.GetResponse() as HttpWebResponse);
			}
			catch (ProtocolViolationException innerException)
			{
				throw new OwaException("ProtocolViolationException", innerException);
			}
			catch (WebException innerException2)
			{
				throw new OwaException("WebException", innerException2);
			}
			catch (InvalidOperationException innerException3)
			{
				throw new OwaException("InvalidOperationException", innerException3);
			}
			catch (NotSupportedException innerException4)
			{
				throw new OwaException("NotSupportedException", innerException4);
			}
			return result;
		}

		// Token: 0x04000E55 RID: 3669
		private const string CreateAttachmentRequestError = "CreateAttachmentRequestError";

		// Token: 0x04000E56 RID: 3670
		private const string CreateAttachmentRequestErrorMessage = "Error building CreateAttachmentRequest";

		// Token: 0x04000E57 RID: 3671
		private const string CreateAttachmentResultNullError = "CreateAttachmentResultNullError";

		// Token: 0x04000E58 RID: 3672
		private const string CreateAttachmentResultNullErrorMessage = "No response message from CreateAttachmentHelper.CreateAttachment";

		// Token: 0x04000E59 RID: 3673
		private const string ImageMaxStreamSizeExceededError = "ImageMaxStreamSizeExceededError";

		// Token: 0x04000E5A RID: 3674
		private const string ImageMaxStreamSizeExceededErrorMessage = "The image size exceeded the maximum size";

		// Token: 0x04000E5B RID: 3675
		private const string ImageAttachmentDataNullError = "ImageAttachmentDataNullError";

		// Token: 0x04000E5C RID: 3676
		private const string ImageAttachmentDataNullErrorMessage = "Image attachment data was null";

		// Token: 0x04000E5D RID: 3677
		private const string BingMapsRequestNullError = "BingMapsRequestNullError";

		// Token: 0x04000E5E RID: 3678
		private const string BingMapsRequestNullErrorMessage = "Request to Bing Maps to get the map image returned null";

		// Token: 0x04000E5F RID: 3679
		private const string WebRequestNullError = "WebRequestNullError";

		// Token: 0x04000E60 RID: 3680
		private const string WebRequestNullErrorMessage = "The HttpWebRequest created to query Bing Maps was null";

		// Token: 0x04000E61 RID: 3681
		private const string WebResponseStreamNullError = "WebResponseStreamNullError";

		// Token: 0x04000E62 RID: 3682
		private const string WebResponseStreamNullErrorMessage = "The response stream for the maps image was null";

		// Token: 0x04000E63 RID: 3683
		private const int MaxImageSize = 204800;

		// Token: 0x04000E64 RID: 3684
		private readonly GetBingMapsPreviewRequest request;

		// Token: 0x04000E65 RID: 3685
		private static readonly string GetBingMapsPreviewActionName = typeof(GetBingMapsPreview).Name;
	}
}
