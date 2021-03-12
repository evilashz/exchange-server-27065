using System;
using System.Globalization;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020000A3 RID: 163
	internal interface INormalizationCacheFileStore
	{
		// Token: 0x0600059F RID: 1439
		bool UploadCache(string filePath, string fileNamePrefix, CultureInfo culture, string version, MailboxSession mbxSession);

		// Token: 0x060005A0 RID: 1440
		bool DownloadCache(string destinationFilePath, string fileNamePrefix, CultureInfo culture, string version);
	}
}
