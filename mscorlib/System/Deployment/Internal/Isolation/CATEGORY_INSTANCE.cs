using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000652 RID: 1618
	internal struct CATEGORY_INSTANCE
	{
		// Token: 0x0400213E RID: 8510
		public IDefinitionAppId DefinitionAppId_Application;

		// Token: 0x0400213F RID: 8511
		[MarshalAs(UnmanagedType.LPWStr)]
		public string XMLSnippet;
	}
}
