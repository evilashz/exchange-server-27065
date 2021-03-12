using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003C5 RID: 965
	[DataContract]
	public class ExportConfigurationChangesParameters : WebServiceParameters
	{
		// Token: 0x17001F92 RID: 8082
		// (get) Token: 0x06003215 RID: 12821 RVA: 0x0009BB3B File Offset: 0x00099D3B
		public override string AssociatedCmdlet
		{
			get
			{
				return "New-AdminAuditLogSearch";
			}
		}

		// Token: 0x17001F93 RID: 8083
		// (get) Token: 0x06003216 RID: 12822 RVA: 0x0009BB42 File Offset: 0x00099D42
		public override string RbacScope
		{
			get
			{
				return "@R:Organization";
			}
		}

		// Token: 0x17001F94 RID: 8084
		// (get) Token: 0x06003217 RID: 12823 RVA: 0x0009BB49 File Offset: 0x00099D49
		// (set) Token: 0x06003218 RID: 12824 RVA: 0x0009BB5B File Offset: 0x00099D5B
		[DataMember]
		public string StartDate
		{
			get
			{
				return base["StartDate"].ToStringWithNull();
			}
			set
			{
				base["StartDate"] = AuditHelper.GetDateForAuditReportsFilter(value, false);
			}
		}

		// Token: 0x17001F95 RID: 8085
		// (get) Token: 0x06003219 RID: 12825 RVA: 0x0009BB6F File Offset: 0x00099D6F
		// (set) Token: 0x0600321A RID: 12826 RVA: 0x0009BB81 File Offset: 0x00099D81
		[DataMember]
		public string EndDate
		{
			get
			{
				return base["EndDate"].ToStringWithNull();
			}
			set
			{
				base["EndDate"] = AuditHelper.GetDateForAuditReportsFilter(value, true);
			}
		}

		// Token: 0x17001F96 RID: 8086
		// (get) Token: 0x0600321B RID: 12827 RVA: 0x0009BB95 File Offset: 0x00099D95
		// (set) Token: 0x0600321C RID: 12828 RVA: 0x0009BBAC File Offset: 0x00099DAC
		[DataMember]
		public string StatusMailRecipients
		{
			get
			{
				return base["StatusMailRecipients"].StringArrayJoin(",");
			}
			set
			{
				base["StatusMailRecipients"] = value.ToArrayOfStrings();
			}
		}

		// Token: 0x04002471 RID: 9329
		public const string RbacParameters = "?StartDate&EndDate&StatusMailRecipients";
	}
}
