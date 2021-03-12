using System;

namespace Microsoft.Exchange.Connections.Eas
{
	// Token: 0x02000007 RID: 7
	[Flags]
	internal enum EasExtensionsVersion1
	{
		// Token: 0x04000014 RID: 20
		FolderTypes = 1,
		// Token: 0x04000015 RID: 21
		SystemCategories = 2,
		// Token: 0x04000016 RID: 22
		DefaultFromAddress = 4,
		// Token: 0x04000017 RID: 23
		Archive = 8,
		// Token: 0x04000018 RID: 24
		Unsubscribe = 16,
		// Token: 0x04000019 RID: 25
		MessageUpload = 32,
		// Token: 0x0400001A RID: 26
		AdvanedSearch = 64,
		// Token: 0x0400001B RID: 27
		PicwData = 128,
		// Token: 0x0400001C RID: 28
		TrueMessageRead = 256,
		// Token: 0x0400001D RID: 29
		Rules = 512,
		// Token: 0x0400001E RID: 30
		ExtendedDateFilters = 1024,
		// Token: 0x0400001F RID: 31
		SmsExtensions = 2048,
		// Token: 0x04000020 RID: 32
		ActionableSearch = 4096,
		// Token: 0x04000021 RID: 33
		FolderPermission = 8192,
		// Token: 0x04000022 RID: 34
		FolderExtensionType = 16384,
		// Token: 0x04000023 RID: 35
		VoiceMailExtension = 32768
	}
}
