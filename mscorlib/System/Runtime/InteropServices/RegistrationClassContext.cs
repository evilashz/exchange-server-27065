using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000947 RID: 2375
	[Flags]
	public enum RegistrationClassContext
	{
		// Token: 0x04002B00 RID: 11008
		InProcessServer = 1,
		// Token: 0x04002B01 RID: 11009
		InProcessHandler = 2,
		// Token: 0x04002B02 RID: 11010
		LocalServer = 4,
		// Token: 0x04002B03 RID: 11011
		InProcessServer16 = 8,
		// Token: 0x04002B04 RID: 11012
		RemoteServer = 16,
		// Token: 0x04002B05 RID: 11013
		InProcessHandler16 = 32,
		// Token: 0x04002B06 RID: 11014
		Reserved1 = 64,
		// Token: 0x04002B07 RID: 11015
		Reserved2 = 128,
		// Token: 0x04002B08 RID: 11016
		Reserved3 = 256,
		// Token: 0x04002B09 RID: 11017
		Reserved4 = 512,
		// Token: 0x04002B0A RID: 11018
		NoCodeDownload = 1024,
		// Token: 0x04002B0B RID: 11019
		Reserved5 = 2048,
		// Token: 0x04002B0C RID: 11020
		NoCustomMarshal = 4096,
		// Token: 0x04002B0D RID: 11021
		EnableCodeDownload = 8192,
		// Token: 0x04002B0E RID: 11022
		NoFailureLog = 16384,
		// Token: 0x04002B0F RID: 11023
		DisableActivateAsActivator = 32768,
		// Token: 0x04002B10 RID: 11024
		EnableActivateAsActivator = 65536,
		// Token: 0x04002B11 RID: 11025
		FromDefaultContext = 131072
	}
}
