using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004FD RID: 1277
	[DataContract]
	[KnownType(typeof(EndUserRoleRow))]
	public class EndUserRoleRow : GroupedCheckBoxTreeItem
	{
		// Token: 0x17002431 RID: 9265
		// (get) Token: 0x06003D98 RID: 15768 RVA: 0x000B8B8F File Offset: 0x000B6D8F
		// (set) Token: 0x06003D99 RID: 15769 RVA: 0x000B8B97 File Offset: 0x000B6D97
		[DataMember]
		public bool IsEndUserRole { get; private set; }

		// Token: 0x17002432 RID: 9266
		// (get) Token: 0x06003D9A RID: 15770 RVA: 0x000B8BA0 File Offset: 0x000B6DA0
		// (set) Token: 0x06003D9B RID: 15771 RVA: 0x000B8BA8 File Offset: 0x000B6DA8
		[DataMember]
		public string MailboxPlanIndex { get; private set; }

		// Token: 0x06003D9C RID: 15772 RVA: 0x000B8BB4 File Offset: 0x000B6DB4
		public EndUserRoleRow(ExchangeRole role) : base(role)
		{
			base.Name = role.Name;
			base.Description = role.Description;
			base.Group = role.RoleType.ToString();
			base.Parent = (role.IsRootRole ? null : role.RoleType.ToString());
			this.IsEndUserRole = role.IsEndUserRole;
			this.MailboxPlanIndex = role.MailboxPlanIndex;
		}
	}
}
