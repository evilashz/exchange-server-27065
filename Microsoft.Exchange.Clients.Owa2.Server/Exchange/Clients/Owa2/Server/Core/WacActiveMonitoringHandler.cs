using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Web;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200004E RID: 78
	internal class WacActiveMonitoringHandler
	{
		// Token: 0x06000227 RID: 551 RVA: 0x00008860 File Offset: 0x00006A60
		public static bool IsWacActiveMonitoringRequest(HttpRequest request, HttpResponse response)
		{
			if (string.Equals(request.QueryString["owaatt"], "Exch_WopiTest", StringComparison.InvariantCultureIgnoreCase))
			{
				switch (WacRequest.GetRequestType(request))
				{
				case WacRequestType.CheckFile:
				{
					DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(WacCheckFileResponse));
					using (MemoryStream memoryStream = new MemoryStream())
					{
						dataContractJsonSerializer.WriteObject(memoryStream, WacActiveMonitoringHandler.DefaultCheckFileResponse());
						memoryStream.Position = 0L;
						response.OutputStream.Write(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
						return true;
					}
					break;
				}
				case WacRequestType.GetFile:
					break;
				default:
					return false;
				}
				using (MemoryStream memoryStream2 = new MemoryStream(WacActiveMonitoringHandler.fileContentBytes))
				{
					WacUtilities.WriteStreamBody(response, memoryStream2);
					return true;
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00008938 File Offset: 0x00006B38
		private static WacCheckFileResponse DefaultCheckFileResponse()
		{
			WacActiveMonitoringHandler.Initialize();
			return WacActiveMonitoringHandler.defaultMonitoringResponse;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00008944 File Offset: 0x00006B44
		private static void Initialize()
		{
			if (WacActiveMonitoringHandler.initialized)
			{
				return;
			}
			lock (WacActiveMonitoringHandler.initializationLock)
			{
				if (!WacActiveMonitoringHandler.initialized)
				{
					WacActiveMonitoringHandler.fileContentBytes = Encoding.Default.GetBytes("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");
					using (MemoryStream memoryStream = new MemoryStream(WacActiveMonitoringHandler.fileContentBytes))
					{
						WacActiveMonitoringHandler.shaHash256ForContentStream = WacUtilities.GenerateSHA256HashForStream(memoryStream);
					}
					WacActiveMonitoringHandler.defaultMonitoringResponse = new WacCheckFileResponse("DummyAttachment", 1911L, WacActiveMonitoringHandler.shaHash256ForContentStream, "https://DummyWopiDownloadUrl", "DummyOwnerId", "DummyOwnerId", "DummyOwnerId", "DummyOwnerId", false, true, true, false);
					Thread.MemoryBarrier();
					WacActiveMonitoringHandler.initialized = true;
				}
			}
		}

		// Token: 0x040000E6 RID: 230
		private const string DefaultFileRep = "Exch_WopiTest";

		// Token: 0x040000E7 RID: 231
		private const string DefaultAttachmentFileName = "DummyAttachment";

		// Token: 0x040000E8 RID: 232
		private const string DummyHash = "DummyHash";

		// Token: 0x040000E9 RID: 233
		private const string DefaultOwnerId = "DummyOwnerId";

		// Token: 0x040000EA RID: 234
		private const long DefaultAttachmentSize = 1911L;

		// Token: 0x040000EB RID: 235
		private const string DefaultdownloadUrl = "https://DummyWopiDownloadUrl";

		// Token: 0x040000EC RID: 236
		private const bool DefaultDirectFileAccessEnabled = true;

		// Token: 0x040000ED RID: 237
		private const bool DefaultIsContentProtected = false;

		// Token: 0x040000EE RID: 238
		private const string FileContent = "UEsDBBQACAAIADN2cD4AAAAAAAAAAAAAAAALAAAAX3JlbHMvLnJlbHONj7sOwyAMRX8F+QNi2qFDBcnSJWuUH0DgkCjhIaCvvy9Dh6bq0NHXR8e+onu4jd0o5SV4CYeGQ9eKgTZVapDnJWZWCZ8lzKXEM2LWMzmVmxDJ180UklOljsliVHpVlvDI+QnTpwP2TjaqZKlIuIdk0AR9deRLU3XAeiMhrnboDQc2PiP9czlM06Lp8hb9eOCLAGwF7lq2L1BLBwhBmmpvmAAAAAoBAABQSwMEFAAIAAgAM3ZwPgAAAAAAAAAAAAAAABwAAAB3b3JkL19yZWxzL2RvY3VtZW50LnhtbC5yZWxzrZBNCsIwEEavEuYATerChTTtxk23pReI6TQNNj8kqdjbGxHEioILlzPz8b3HVM3VzOSCIWpnOZQFg6auOpxFyos4aR9JTtjIYUrJHyiNckIjYuE82nwZXTAi5TEo6oU8C4V0x9iehtcO2HaSXgSFiYNdzAmDtqrIVUDagcPgZNcODEi/evyF6sZRSzw6uRi06QOcPilAv3jEtM4Y3yXKf0o8EHcDunlvfQNQSwcIK9lUlKoAAACDAQAAUEsDBBQACAAIADN2cD4AAAAAAAAAAAAAAAARAAAAd29yZC9kb2N1bWVudC54bWzlUsFOwzAM/ZUqd5bSw0DVumlC2gkJDiDOWeKuQWkS2dnK+HqStN0Yv8Aljp+fn+3Eq81Xb4oTIGlnG3a/KFkBVjql7aFh72+7u0dWUBBWCeMsNOwMxDbr1VArJ4892FBEAUv10LAuBF9zTrKDXtDCebAx1jrsRYguHvjgUHl0Eoiifm94VZZL3gttWZLcO3VO1ufjFZMhL2TkFjEKUSp2EDscatEGwIbF/OQZnVqrHpas4ClJWxVR1IcuTHwD7XxtNVJ4zhnlxP+UET8J07DEm0AcG8Cds4FSSZJaN+xJGL1HnZS6raVbRNKNC4LClrT4BWZp6YzDuaQ4Bjfh12FzZG7PO9Ih/s9fnL5npKpmqFN5RHMRnhjSgMBM4tNo/PLG+B/GHeq8qrlo/HqPQIAnYOuXQXxQpxMxjPTxbfI5riS/rvv6B1BLBwis8/duRgEAADIDAABQSwMEFAAIAAgAM3ZwPgAAAAAAAAAAAAAAABIAAAB3b3JkL251bWJlcmluZy54bWwNjEEOwjAMBL8S+U5dOCAUNe2tL4AHhMS0lRq7igOB35PjamZ2mL5pNx/Kugk7OHc9GOIgcePFweM+n25gtHiOfhcmBz9SmMahWn6nJ+WmmfbAaquDtZTDImpYKXnt5CBu7CU5+dJmXrBKjkeWQKqtTDte+v6KyW8MBsc/UEsHCCwxv3F8AAAAjQAAAFBLAwQUAAgACAAzdnA+AAAAAAAAAAAAAAAADwAAAHdvcmQvc3R5bGVzLnhtbA2MQQ7CIBAAv0L2bkEPxpDS3nyBPoDA2pLAbsMSsb+X42QyM6+/ktUXqyQmB9fJgEIKHBNtDt6v5+UBSpqn6DMTOjhRYF3mbqWdGUWNnMR2B3trh9Vawo7Fy8QH0nAfrsW3gXXTnWs8KgcUGfeS9c2Yuy4+ESi9/AFQSwcIr1PIQXkAAACKAAAAUEsDBBQACAAIADN2cD4AAAAAAAAAAAAAAAATAAAAW0NvbnRlbnRfVHlwZXNdLnhtbK2Sy07DMBBFfyXyFtUTWLBASboAtsCCHzDOJLHqlzyT0v49k4Z2gZAQUpf2fZwry832EHy1x0IuxVbd6lptu+b9mJEqUSK1amLODwBkJwyGdMoYRRlSCYblWEbIxu7MiHBX1/dgU2SMvOGlQ3XNEw5m9lw9H+R6pRT0pKrH1biwWmVy9s4aFh32sf9B2XwTtCRPHppcphsxKPiVsCj/AqRhcBb7ZOcgEf2ZSp9Lskjk4hi8vijBuHgGv8q7Fddj9WYKv5ggGFiSEOfwgUWS+upDLtV/jiA+eqTrL1h7z3g4/ZXuC1BLBwi17ehY5AAAAEgCAABQSwECLQAUAAgACAAzdnA+QZpqb5gAAAAKAQAACwAAAAAAAAAAAAAAAAAAAAAAX3JlbHMvLnJlbHNQSwECLQAUAAgACAAzdnA+K9lUlKoAAACDAQAAHAAAAAAAAAAAAAAAAADRAAAAd29yZC9fcmVscy9kb2N1bWVudC54bWwucmVsc1BLAQItABQACAAIADN2cD6s8/duRgEAADIDAAARAAAAAAAAAAAAAAAAAMUBAAB3b3JkL2RvY3VtZW50LnhtbFBLAQItABQACAAIADN2cD4sMb9xfAAAAI0AAAASAAAAAAAAAAAAAAAAAEoDAAB3b3JkL251bWJlcmluZy54bWxQSwECLQAUAAgACAAzdnA+r1PIQXkAAACKAAAADwAAAAAAAAAAAAAAAAAGBAAAd29yZC9zdHlsZXMueG1sUEsBAi0AFAAIAAgAM3ZwPrXt6FjkAAAASAIAABMAAAAAAAAAAAAAAAAAvAQAAFtDb250ZW50X1R5cGVzXS54bWxQSwUGAAAAAAYABgCAAQAA4QUAAAAA";

		// Token: 0x040000EF RID: 239
		private static byte[] fileContentBytes;

		// Token: 0x040000F0 RID: 240
		private static string shaHash256ForContentStream;

		// Token: 0x040000F1 RID: 241
		private static WacCheckFileResponse defaultMonitoringResponse;

		// Token: 0x040000F2 RID: 242
		private static bool initialized;

		// Token: 0x040000F3 RID: 243
		private static object initializationLock = new object();
	}
}
