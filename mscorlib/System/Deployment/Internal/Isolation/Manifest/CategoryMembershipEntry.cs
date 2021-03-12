using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006B6 RID: 1718
	[StructLayout(LayoutKind.Sequential)]
	internal class CategoryMembershipEntry
	{
		// Token: 0x04002280 RID: 8832
		public IDefinitionIdentity Identity;

		// Token: 0x04002281 RID: 8833
		public ISection SubcategoryMembership;
	}
}
