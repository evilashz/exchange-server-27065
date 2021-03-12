using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200064F RID: 1615
	internal struct STORE_CATEGORY_INSTANCE
	{
		// Token: 0x0400213A RID: 8506
		public IDefinitionAppId DefinitionAppId_Application;

		// Token: 0x0400213B RID: 8507
		[MarshalAs(UnmanagedType.LPWStr)]
		public string XMLSnippet;
	}
}
