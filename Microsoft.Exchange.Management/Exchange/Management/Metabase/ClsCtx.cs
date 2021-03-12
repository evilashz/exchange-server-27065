using System;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x020004A1 RID: 1185
	[Flags]
	internal enum ClsCtx
	{
		// Token: 0x04001EA6 RID: 7846
		InprocServer = 1,
		// Token: 0x04001EA7 RID: 7847
		InprocHandler = 2,
		// Token: 0x04001EA8 RID: 7848
		LocalServer = 4,
		// Token: 0x04001EA9 RID: 7849
		InprocServer16 = 8,
		// Token: 0x04001EAA RID: 7850
		RemoteServer = 16,
		// Token: 0x04001EAB RID: 7851
		InprocHandler16 = 32,
		// Token: 0x04001EAC RID: 7852
		Reserved1 = 64,
		// Token: 0x04001EAD RID: 7853
		Reserved2 = 128,
		// Token: 0x04001EAE RID: 7854
		Reserved3 = 256,
		// Token: 0x04001EAF RID: 7855
		Reserved4 = 512,
		// Token: 0x04001EB0 RID: 7856
		NoCodeDownload = 1024,
		// Token: 0x04001EB1 RID: 7857
		Reserved5 = 2048,
		// Token: 0x04001EB2 RID: 7858
		NoCustomMarshal = 4096,
		// Token: 0x04001EB3 RID: 7859
		EnableCodeDownload = 8192,
		// Token: 0x04001EB4 RID: 7860
		NoFailureLog = 16384,
		// Token: 0x04001EB5 RID: 7861
		DisableAAA = 32768,
		// Token: 0x04001EB6 RID: 7862
		EnableAAA = 65536,
		// Token: 0x04001EB7 RID: 7863
		FromDefaultContext = 131072,
		// Token: 0x04001EB8 RID: 7864
		Activate32BitServer = 262144,
		// Token: 0x04001EB9 RID: 7865
		Activate64BitServer = 524288
	}
}
