using System;

namespace Microsoft.Exchange.Rpc.ExchangeCertificate
{
	// Token: 0x02000248 RID: 584
	internal enum RpcOutput
	{
		// Token: 0x04000CA3 RID: 3235
		ExchangeCertList,
		// Token: 0x04000CA4 RID: 3236
		ExchangeCert,
		// Token: 0x04000CA5 RID: 3237
		CertRequest = 100,
		// Token: 0x04000CA6 RID: 3238
		ExportBase64 = 300,
		// Token: 0x04000CA7 RID: 3239
		ExportFile,
		// Token: 0x04000CA8 RID: 3240
		ExportBinary,
		// Token: 0x04000CA9 RID: 3241
		ExportPKCS10,
		// Token: 0x04000CAA RID: 3242
		TaskErrorString = 1000,
		// Token: 0x04000CAB RID: 3243
		TaskErrorCategory,
		// Token: 0x04000CAC RID: 3244
		TaskWarningString,
		// Token: 0x04000CAD RID: 3245
		TaskConfirmation,
		// Token: 0x04000CAE RID: 3246
		TaskWarningList,
		// Token: 0x04000CAF RID: 3247
		TaskConfirmationList
	}
}
