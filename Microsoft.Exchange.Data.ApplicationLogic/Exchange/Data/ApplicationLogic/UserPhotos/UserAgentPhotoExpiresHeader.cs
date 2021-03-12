using System;
using System.Globalization;
using System.Net;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x0200021B RID: 539
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class UserAgentPhotoExpiresHeader
	{
		// Token: 0x0600136C RID: 4972 RVA: 0x0005056B File Offset: 0x0004E76B
		private UserAgentPhotoExpiresHeader()
		{
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x00050574 File Offset: 0x0004E774
		public string ComputeExpiresHeader(DateTime utcNow, HttpStatusCode statusCode, PhotosConfiguration configuration)
		{
			if (statusCode <= HttpStatusCode.NotModified)
			{
				if (statusCode != HttpStatusCode.OK)
				{
					switch (statusCode)
					{
					case HttpStatusCode.Found:
					case HttpStatusCode.NotModified:
						break;
					case HttpStatusCode.SeeOther:
						goto IL_64;
					default:
						goto IL_64;
					}
				}
				return UserAgentPhotoExpiresHeader.FormatTimestampForExpiresHeader(utcNow.Add(configuration.UserAgentCacheTimeToLive));
			}
			if (statusCode == HttpStatusCode.NotFound)
			{
				return UserAgentPhotoExpiresHeader.FormatTimestampForExpiresHeader(utcNow.Add(configuration.UserAgentCacheTimeToLiveNotFound));
			}
			if (statusCode != HttpStatusCode.InternalServerError)
			{
			}
			IL_64:
			return string.Empty;
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x000505EA File Offset: 0x0004E7EA
		private static string FormatTimestampForExpiresHeader(DateTime timeStamp)
		{
			return timeStamp.ToString(CultureInfo.InvariantCulture.DateTimeFormat.RFC1123Pattern, CultureInfo.InvariantCulture);
		}

		// Token: 0x04000AD8 RID: 2776
		internal static readonly UserAgentPhotoExpiresHeader Default = new UserAgentPhotoExpiresHeader();
	}
}
