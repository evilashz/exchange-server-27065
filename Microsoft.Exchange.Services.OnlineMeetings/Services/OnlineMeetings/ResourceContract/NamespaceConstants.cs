using System;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x020000A3 RID: 163
	internal static class NamespaceConstants
	{
		// Token: 0x020000A4 RID: 164
		internal static class ContainerNames
		{
			// Token: 0x040002B3 RID: 691
			public const string EventsRequest = "EventsRequest";

			// Token: 0x040002B4 RID: 692
			public const string EventsResponse = "EventsResponse";

			// Token: 0x040002B5 RID: 693
			public const string MultipartRelatedEventsResponse = "MultipartRelatedEventsResponse";

			// Token: 0x040002B6 RID: 694
			public const string MultipartRelatedRequest = "MultipartRelatedRequest";
		}

		// Token: 0x020000A5 RID: 165
		internal static class MediaTypes
		{
			// Token: 0x020000A6 RID: 166
			internal static class Hal
			{
				// Token: 0x040002B7 RID: 695
				public const string Json = "application/hal+json";

				// Token: 0x040002B8 RID: 696
				public const string Xml = "application/hal+xml";
			}

			// Token: 0x020000A7 RID: 167
			internal static class Xml
			{
				// Token: 0x040002B9 RID: 697
				public const string Common = "application/vnd.microsoft.com.ucwa+xml";

				// Token: 0x040002BA RID: 698
				public const string Error = "application/vnd.microsoft.com.ucwa.error+xml";

				// Token: 0x040002BB RID: 699
				public const string Request = "application/vnd.microsoft.com.ucwa.request+xml";

				// Token: 0x040002BC RID: 700
				public const string Events = "application/vnd.microsoft.com.ucwa.events+xml";
			}

			// Token: 0x020000A8 RID: 168
			internal static class Json
			{
				// Token: 0x040002BD RID: 701
				public const string Common = "application/vnd.microsoft.com.ucwa+json";

				// Token: 0x040002BE RID: 702
				public const string Error = "application/vnd.microsoft.com.ucwa.error+json";

				// Token: 0x040002BF RID: 703
				public const string Request = "application/vnd.microsoft.com.ucwa.request+json";

				// Token: 0x040002C0 RID: 704
				public const string Events = "application/vnd.microsoft.com.ucwa.events+json";
			}

			// Token: 0x020000A9 RID: 169
			internal static class Text
			{
				// Token: 0x040002C1 RID: 705
				public const string Plain = "text/plain";

				// Token: 0x040002C2 RID: 706
				public const string Html = "text/html";
			}

			// Token: 0x020000AA RID: 170
			internal static class Mime
			{
				// Token: 0x040002C3 RID: 707
				public const string Related = "multipart/related";

				// Token: 0x040002C4 RID: 708
				public const string Alternative = "multipart/alternative";

				// Token: 0x040002C5 RID: 709
				public const string BatchingSubtype = "batching";
			}

			// Token: 0x020000AB RID: 171
			internal static class MimeXml
			{
				// Token: 0x040002C6 RID: 710
				public const string Related = "multipart/related+xml";
			}

			// Token: 0x020000AC RID: 172
			internal static class MimeJson
			{
				// Token: 0x040002C7 RID: 711
				public const string Related = "multipart/related+json";
			}

			// Token: 0x020000AD RID: 173
			internal static class Application
			{
				// Token: 0x040002C8 RID: 712
				public const string Sdp = "application/sdp";

				// Token: 0x040002C9 RID: 713
				public const string Json = "application/json";

				// Token: 0x040002CA RID: 714
				public const string Xml = "application/xml";
			}

			// Token: 0x020000AE RID: 174
			internal static class Photo
			{
				// Token: 0x040002CB RID: 715
				public const string Jpeg = "image/jpeg";
			}
		}
	}
}
