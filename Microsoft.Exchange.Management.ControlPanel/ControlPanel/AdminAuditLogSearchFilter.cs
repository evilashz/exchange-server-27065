using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003B7 RID: 951
	[DataContract]
	public class AdminAuditLogSearchFilter : WebServiceParameters
	{
		// Token: 0x060031CA RID: 12746 RVA: 0x00099CAC File Offset: 0x00097EAC
		public AdminAuditLogSearchFilter()
		{
			this.OnDeserializing(default(StreamingContext));
		}

		// Token: 0x17001F86 RID: 8070
		// (get) Token: 0x060031CB RID: 12747 RVA: 0x00099CCE File Offset: 0x00097ECE
		public override string RbacScope
		{
			get
			{
				return "@R:Organization";
			}
		}

		// Token: 0x17001F87 RID: 8071
		// (get) Token: 0x060031CC RID: 12748 RVA: 0x00099CD5 File Offset: 0x00097ED5
		public override string AssociatedCmdlet
		{
			get
			{
				return "Search-AdminAuditLog";
			}
		}

		// Token: 0x17001F88 RID: 8072
		// (get) Token: 0x060031CD RID: 12749 RVA: 0x00099CDC File Offset: 0x00097EDC
		// (set) Token: 0x060031CE RID: 12750 RVA: 0x00099CEE File Offset: 0x00097EEE
		[DataMember]
		public string StartDate
		{
			get
			{
				return (string)base["StartDate"];
			}
			set
			{
				base["StartDate"] = AuditHelper.GetDateForAuditReportsFilter(value, false);
			}
		}

		// Token: 0x17001F89 RID: 8073
		// (get) Token: 0x060031CF RID: 12751 RVA: 0x00099D02 File Offset: 0x00097F02
		// (set) Token: 0x060031D0 RID: 12752 RVA: 0x00099D14 File Offset: 0x00097F14
		[DataMember]
		public string EndDate
		{
			get
			{
				return (string)base["EndDate"];
			}
			set
			{
				base["EndDate"] = AuditHelper.GetDateForAuditReportsFilter(value, true);
			}
		}

		// Token: 0x17001F8A RID: 8074
		// (get) Token: 0x060031D1 RID: 12753 RVA: 0x00099D28 File Offset: 0x00097F28
		// (set) Token: 0x060031D2 RID: 12754 RVA: 0x00099D3F File Offset: 0x00097F3F
		[DataMember]
		public virtual string ObjectIds
		{
			get
			{
				return base["ObjectIds"].StringArrayJoin(",");
			}
			set
			{
				base["ObjectIds"] = value.ToArrayOfStrings();
			}
		}

		// Token: 0x17001F8B RID: 8075
		// (get) Token: 0x060031D3 RID: 12755 RVA: 0x00099D52 File Offset: 0x00097F52
		// (set) Token: 0x060031D4 RID: 12756 RVA: 0x00099D69 File Offset: 0x00097F69
		public string Cmdlets
		{
			get
			{
				return base["Cmdlets"].StringArrayJoin(",");
			}
			set
			{
				base["Cmdlets"] = value.ToArrayOfStrings();
			}
		}

		// Token: 0x17001F8C RID: 8076
		// (get) Token: 0x060031D5 RID: 12757 RVA: 0x00099D7C File Offset: 0x00097F7C
		// (set) Token: 0x060031D6 RID: 12758 RVA: 0x00099D93 File Offset: 0x00097F93
		public string Parameters
		{
			get
			{
				return base["Parameters"].StringArrayJoin(",");
			}
			set
			{
				base["Parameters"] = value.ToArrayOfStrings();
			}
		}

		// Token: 0x060031D7 RID: 12759 RVA: 0x00099DA6 File Offset: 0x00097FA6
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
			if (!(this is NonOwnerAccessFilter))
			{
				base["IsSuccess"] = true;
			}
		}

		// Token: 0x04002425 RID: 9253
		public const string RbacParameters = "?StartDate&EndDate&ObjectIds&Cmdlets";
	}
}
