using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x0200069A RID: 1690
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("a504e5b0-8ccf-4cb4-9902-c9d1b9abd033")]
	[ComImport]
	internal interface ICMS
	{
		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x06004F20 RID: 20256
		IDefinitionIdentity Identity { [SecurityCritical] get; }

		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x06004F21 RID: 20257
		ISection FileSection { [SecurityCritical] get; }

		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x06004F22 RID: 20258
		ISection CategoryMembershipSection { [SecurityCritical] get; }

		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x06004F23 RID: 20259
		ISection COMRedirectionSection { [SecurityCritical] get; }

		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x06004F24 RID: 20260
		ISection ProgIdRedirectionSection { [SecurityCritical] get; }

		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x06004F25 RID: 20261
		ISection CLRSurrogateSection { [SecurityCritical] get; }

		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x06004F26 RID: 20262
		ISection AssemblyReferenceSection { [SecurityCritical] get; }

		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x06004F27 RID: 20263
		ISection WindowClassSection { [SecurityCritical] get; }

		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x06004F28 RID: 20264
		ISection StringSection { [SecurityCritical] get; }

		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x06004F29 RID: 20265
		ISection EntryPointSection { [SecurityCritical] get; }

		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x06004F2A RID: 20266
		ISection PermissionSetSection { [SecurityCritical] get; }

		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x06004F2B RID: 20267
		ISectionEntry MetadataSectionEntry { [SecurityCritical] get; }

		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x06004F2C RID: 20268
		ISection AssemblyRequestSection { [SecurityCritical] get; }

		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x06004F2D RID: 20269
		ISection RegistryKeySection { [SecurityCritical] get; }

		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x06004F2E RID: 20270
		ISection DirectorySection { [SecurityCritical] get; }

		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x06004F2F RID: 20271
		ISection FileAssociationSection { [SecurityCritical] get; }

		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x06004F30 RID: 20272
		ISection CompatibleFrameworksSection { [SecurityCritical] get; }

		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x06004F31 RID: 20273
		ISection EventSection { [SecurityCritical] get; }

		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x06004F32 RID: 20274
		ISection EventMapSection { [SecurityCritical] get; }

		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x06004F33 RID: 20275
		ISection EventTagSection { [SecurityCritical] get; }

		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x06004F34 RID: 20276
		ISection CounterSetSection { [SecurityCritical] get; }

		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x06004F35 RID: 20277
		ISection CounterSection { [SecurityCritical] get; }
	}
}
