using System;

namespace System.Security.Permissions
{
	// Token: 0x020002BC RID: 700
	[Serializable]
	internal enum BuiltInPermissionFlag
	{
		// Token: 0x04000DF9 RID: 3577
		EnvironmentPermission = 1,
		// Token: 0x04000DFA RID: 3578
		FileDialogPermission,
		// Token: 0x04000DFB RID: 3579
		FileIOPermission = 4,
		// Token: 0x04000DFC RID: 3580
		IsolatedStorageFilePermission = 8,
		// Token: 0x04000DFD RID: 3581
		ReflectionPermission = 16,
		// Token: 0x04000DFE RID: 3582
		RegistryPermission = 32,
		// Token: 0x04000DFF RID: 3583
		SecurityPermission = 64,
		// Token: 0x04000E00 RID: 3584
		UIPermission = 128,
		// Token: 0x04000E01 RID: 3585
		PrincipalPermission = 256,
		// Token: 0x04000E02 RID: 3586
		PublisherIdentityPermission = 512,
		// Token: 0x04000E03 RID: 3587
		SiteIdentityPermission = 1024,
		// Token: 0x04000E04 RID: 3588
		StrongNameIdentityPermission = 2048,
		// Token: 0x04000E05 RID: 3589
		UrlIdentityPermission = 4096,
		// Token: 0x04000E06 RID: 3590
		ZoneIdentityPermission = 8192,
		// Token: 0x04000E07 RID: 3591
		KeyContainerPermission = 16384
	}
}
