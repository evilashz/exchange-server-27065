using System;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x02000017 RID: 23
	internal interface IWatermarkFileHelper
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000112 RID: 274
		string WatermarkFileDirectory { get; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000113 RID: 275
		string LogFileDirectory { get; }

		// Token: 0x06000114 RID: 276
		IWatermarkFile CreateWaterMarkFileObj(string logFileName, string logPrefix);

		// Token: 0x06000115 RID: 277
		string DeduceDoneFileFullNameFromLogFileName(string logFileName);

		// Token: 0x06000116 RID: 278
		string DeduceWatermarkFileFullNameFromLogFileName(string logFileName);

		// Token: 0x06000117 RID: 279
		string DeduceLogFullFileNameFromDoneOrWatermarkFileName(string fileFullName);
	}
}
