using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004DA RID: 1242
	[DataContract]
	public class AdminRoleGroupFilter : SearchTextFilter
	{
		// Token: 0x170023E4 RID: 9188
		// (get) Token: 0x06003CA9 RID: 15529 RVA: 0x000B635C File Offset: 0x000B455C
		public override string AssociatedCmdlet
		{
			get
			{
				return "Get-RoleGroup";
			}
		}

		// Token: 0x170023E5 RID: 9189
		// (get) Token: 0x06003CAA RID: 15530 RVA: 0x000B6363 File Offset: 0x000B4563
		// (set) Token: 0x06003CAB RID: 15531 RVA: 0x000B636B File Offset: 0x000B456B
		[DataMember]
		internal string ManagementScope { get; set; }

		// Token: 0x170023E6 RID: 9190
		// (get) Token: 0x06003CAC RID: 15532 RVA: 0x000B6374 File Offset: 0x000B4574
		public override string RbacScope
		{
			get
			{
				return "@R:Organization";
			}
		}

		// Token: 0x170023E7 RID: 9191
		// (get) Token: 0x06003CAD RID: 15533 RVA: 0x000B637B File Offset: 0x000B457B
		protected override string[] FilterableProperties
		{
			get
			{
				return AdminRoleGroupFilter.filterableProperties;
			}
		}

		// Token: 0x170023E8 RID: 9192
		// (get) Token: 0x06003CAE RID: 15534 RVA: 0x000B6382 File Offset: 0x000B4582
		protected override SearchTextFilterType FilterType
		{
			get
			{
				return SearchTextFilterType.Contains;
			}
		}

		// Token: 0x040027BB RID: 10171
		private static string[] filterableProperties = new string[]
		{
			"Name"
		};
	}
}
