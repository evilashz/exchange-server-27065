using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000521 RID: 1313
	[DataContract]
	public class ManagementRoleFilter : WebServiceParameters
	{
		// Token: 0x1700248C RID: 9356
		// (get) Token: 0x06003EC5 RID: 16069 RVA: 0x000BD395 File Offset: 0x000BB595
		public override string AssociatedCmdlet
		{
			get
			{
				return "Get-ManagementRole";
			}
		}

		// Token: 0x1700248D RID: 9357
		// (get) Token: 0x06003EC6 RID: 16070 RVA: 0x000BD39C File Offset: 0x000BB59C
		// (set) Token: 0x06003EC7 RID: 16071 RVA: 0x000BD3AE File Offset: 0x000BB5AE
		[DataMember]
		public string RoleType
		{
			get
			{
				return (string)base["RoleType"];
			}
			set
			{
				base["RoleType"] = value;
			}
		}

		// Token: 0x1700248E RID: 9358
		// (get) Token: 0x06003EC8 RID: 16072 RVA: 0x000BD3BC File Offset: 0x000BB5BC
		public override string RbacScope
		{
			get
			{
				return "@R:Organization";
			}
		}

		// Token: 0x1700248F RID: 9359
		// (get) Token: 0x06003EC9 RID: 16073 RVA: 0x000BD3C3 File Offset: 0x000BB5C3
		// (set) Token: 0x06003ECA RID: 16074 RVA: 0x000BD3CB File Offset: 0x000BB5CB
		[DataMember]
		public Identity Policy { get; set; }

		// Token: 0x040028A8 RID: 10408
		internal const string GetCmdlet = "Get-ManagementRole";
	}
}
