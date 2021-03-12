using System;

namespace Microsoft.Exchange.Management.Hybrid.Entity
{
	// Token: 0x020008F4 RID: 2292
	internal class OrganizationalUnit : IOrganizationalUnit
	{
		// Token: 0x17001857 RID: 6231
		// (get) Token: 0x06005143 RID: 20803 RVA: 0x001521D5 File Offset: 0x001503D5
		// (set) Token: 0x06005144 RID: 20804 RVA: 0x001521DD File Offset: 0x001503DD
		public string Name { get; set; }

		// Token: 0x06005145 RID: 20805 RVA: 0x001521E6 File Offset: 0x001503E6
		public override string ToString()
		{
			return this.Name;
		}
	}
}
