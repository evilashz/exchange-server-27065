using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Net
{
	// Token: 0x020000D1 RID: 209
	internal static class HttpStrings
	{
		// Token: 0x06000542 RID: 1346 RVA: 0x00013E54 File Offset: 0x00012054
		static HttpStrings()
		{
			HttpStrings.stringIDs.Add(1040461213U, "DownloadPermanentException");
			HttpStrings.stringIDs.Add(155224738U, "DownloadTimeoutException");
			HttpStrings.stringIDs.Add(3994627926U, "DownloadCanceledException");
			HttpStrings.stringIDs.Add(1904454819U, "DownloadTransientException");
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00013EE0 File Offset: 0x000120E0
		public static LocalizedString ServerProtocolViolationException(string size)
		{
			return new LocalizedString("ServerProtocolViolationException", "", false, false, HttpStrings.ResourceManager, new object[]
			{
				size
			});
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00013F10 File Offset: 0x00012110
		public static LocalizedString UnsupportedUriFormatException(string uri)
		{
			return new LocalizedString("UnsupportedUriFormatException", "", false, false, HttpStrings.ResourceManager, new object[]
			{
				uri
			});
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00013F40 File Offset: 0x00012140
		public static LocalizedString BadRedirectedUriException(string uri)
		{
			return new LocalizedString("BadRedirectedUriException", "", false, false, HttpStrings.ResourceManager, new object[]
			{
				uri
			});
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x00013F6F File Offset: 0x0001216F
		public static LocalizedString DownloadPermanentException
		{
			get
			{
				return new LocalizedString("DownloadPermanentException", "", false, false, HttpStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00013F90 File Offset: 0x00012190
		public static LocalizedString DownloadLimitExceededException(string size)
		{
			return new LocalizedString("DownloadLimitExceededException", "", false, false, HttpStrings.ResourceManager, new object[]
			{
				size
			});
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x00013FBF File Offset: 0x000121BF
		public static LocalizedString DownloadTimeoutException
		{
			get
			{
				return new LocalizedString("DownloadTimeoutException", "", false, false, HttpStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x00013FDD File Offset: 0x000121DD
		public static LocalizedString DownloadCanceledException
		{
			get
			{
				return new LocalizedString("DownloadCanceledException", "", false, false, HttpStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x00013FFB File Offset: 0x000121FB
		public static LocalizedString DownloadTransientException
		{
			get
			{
				return new LocalizedString("DownloadTransientException", "", false, false, HttpStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00014019 File Offset: 0x00012219
		public static LocalizedString GetLocalizedString(HttpStrings.IDs key)
		{
			return new LocalizedString(HttpStrings.stringIDs[(uint)key], HttpStrings.ResourceManager, new object[0]);
		}

		// Token: 0x04000463 RID: 1123
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(4);

		// Token: 0x04000464 RID: 1124
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Net.HttpStrings", typeof(HttpStrings).GetTypeInfo().Assembly);

		// Token: 0x020000D2 RID: 210
		public enum IDs : uint
		{
			// Token: 0x04000466 RID: 1126
			DownloadPermanentException = 1040461213U,
			// Token: 0x04000467 RID: 1127
			DownloadTimeoutException = 155224738U,
			// Token: 0x04000468 RID: 1128
			DownloadCanceledException = 3994627926U,
			// Token: 0x04000469 RID: 1129
			DownloadTransientException = 1904454819U
		}

		// Token: 0x020000D3 RID: 211
		private enum ParamIDs
		{
			// Token: 0x0400046B RID: 1131
			ServerProtocolViolationException,
			// Token: 0x0400046C RID: 1132
			UnsupportedUriFormatException,
			// Token: 0x0400046D RID: 1133
			BadRedirectedUriException,
			// Token: 0x0400046E RID: 1134
			DownloadLimitExceededException
		}
	}
}
