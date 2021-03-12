using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200053C RID: 1340
	[KnownType(typeof(RoleGroupMembersRow))]
	public class RoleGroupMembersRow : BaseRow
	{
		// Token: 0x06003F58 RID: 16216 RVA: 0x000BEB4F File Offset: 0x000BCD4F
		public RoleGroupMembersRow(RoleGroup roleGroupObject)
		{
			this.RoleGroupObject = roleGroupObject;
		}

		// Token: 0x170024BB RID: 9403
		// (get) Token: 0x06003F59 RID: 16217 RVA: 0x000BEB5E File Offset: 0x000BCD5E
		// (set) Token: 0x06003F5A RID: 16218 RVA: 0x000BEB66 File Offset: 0x000BCD66
		protected RoleGroup RoleGroupObject { get; set; }
	}
}
