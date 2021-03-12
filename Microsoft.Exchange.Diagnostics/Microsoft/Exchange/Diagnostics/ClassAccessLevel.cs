using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000008 RID: 8
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface)]
	public class ClassAccessLevel : Attribute
	{
		// Token: 0x06000018 RID: 24 RVA: 0x000024E5 File Offset: 0x000006E5
		public ClassAccessLevel(AccessLevel accessLevel)
		{
			this.AccessLevel = accessLevel;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000024F4 File Offset: 0x000006F4
		// (set) Token: 0x0600001A RID: 26 RVA: 0x000024FC File Offset: 0x000006FC
		public AccessLevel AccessLevel { get; private set; }
	}
}
