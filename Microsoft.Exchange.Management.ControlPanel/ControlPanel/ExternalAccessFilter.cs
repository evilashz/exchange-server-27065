using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003CC RID: 972
	[DataContract]
	public class ExternalAccessFilter : AdminAuditLogSearchFilter
	{
		// Token: 0x17001FA0 RID: 8096
		// (get) Token: 0x06003234 RID: 12852 RVA: 0x0009BF3C File Offset: 0x0009A13C
		public override string RbacScope
		{
			get
			{
				return "@R:Organization";
			}
		}

		// Token: 0x17001FA1 RID: 8097
		// (get) Token: 0x06003235 RID: 12853 RVA: 0x0009BF43 File Offset: 0x0009A143
		public override string AssociatedCmdlet
		{
			get
			{
				return "Search-AdminAuditLog";
			}
		}

		// Token: 0x17001FA2 RID: 8098
		// (get) Token: 0x06003236 RID: 12854 RVA: 0x0009BF4A File Offset: 0x0009A14A
		// (set) Token: 0x06003237 RID: 12855 RVA: 0x0009BF5C File Offset: 0x0009A15C
		[DataMember]
		public bool? ExternalAccess
		{
			get
			{
				return (bool?)base["ExternalAccess"];
			}
			set
			{
				base["ExternalAccess"] = value;
			}
		}

		// Token: 0x0400247A RID: 9338
		public new const string RbacParameters = "?ResultSize&StartDate&EndDate&ExternalAccess";
	}
}
