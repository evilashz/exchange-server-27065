using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.Prompts.Provisioning
{
	// Token: 0x02000017 RID: 23
	internal interface IPublishingSession : IDisposeTrackable, IDisposable
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001C1 RID: 449
		// (set) Token: 0x060001C2 RID: 450
		TimeSpan TestHookKeepOrphanFilesInterval { get; set; }

		// Token: 0x060001C3 RID: 451
		void Upload(string source, string destinationName);

		// Token: 0x060001C4 RID: 452
		void Download(string sourceName, string destination);

		// Token: 0x060001C5 RID: 453
		ITempWavFile DownloadAsWav(string sourceName);

		// Token: 0x060001C6 RID: 454
		void DownloadAllAsWma(DirectoryInfo directory);

		// Token: 0x060001C7 RID: 455
		void Delete();
	}
}
