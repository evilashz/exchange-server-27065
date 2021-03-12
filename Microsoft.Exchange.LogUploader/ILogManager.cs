using System;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x02000013 RID: 19
	internal interface ILogManager
	{
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060000E6 RID: 230
		string Instance { get; }

		// Token: 0x060000E7 RID: 231
		void Start();

		// Token: 0x060000E8 RID: 232
		void Stop();

		// Token: 0x060000E9 RID: 233
		LogFileInfo GetLogForReaderToProcess();

		// Token: 0x060000EA RID: 234
		void ReaderCompletedProcessingLog(LogFileInfo logFile);

		// Token: 0x060000EB RID: 235
		IWatermarkFile FindWatermarkFileObject(string logFileName);
	}
}
