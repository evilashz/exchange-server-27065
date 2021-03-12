using System;
using System.Management.Automation;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003C9 RID: 969
	[DataContract]
	public class ExportMailboxChangesParameters : WebServiceParameters
	{
		// Token: 0x17001F97 RID: 8087
		// (get) Token: 0x06003221 RID: 12833 RVA: 0x0009BCDE File Offset: 0x00099EDE
		public override string AssociatedCmdlet
		{
			get
			{
				return "New-MailboxAuditLogSearch";
			}
		}

		// Token: 0x17001F98 RID: 8088
		// (get) Token: 0x06003222 RID: 12834 RVA: 0x0009BCE5 File Offset: 0x00099EE5
		public override string RbacScope
		{
			get
			{
				return "@R:Organization";
			}
		}

		// Token: 0x17001F99 RID: 8089
		// (get) Token: 0x06003223 RID: 12835 RVA: 0x0009BCEC File Offset: 0x00099EEC
		// (set) Token: 0x06003224 RID: 12836 RVA: 0x0009BCFE File Offset: 0x00099EFE
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

		// Token: 0x17001F9A RID: 8090
		// (get) Token: 0x06003225 RID: 12837 RVA: 0x0009BD12 File Offset: 0x00099F12
		// (set) Token: 0x06003226 RID: 12838 RVA: 0x0009BD24 File Offset: 0x00099F24
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

		// Token: 0x17001F9B RID: 8091
		// (get) Token: 0x06003227 RID: 12839 RVA: 0x0009BD38 File Offset: 0x00099F38
		// (set) Token: 0x06003228 RID: 12840 RVA: 0x0009BD4F File Offset: 0x00099F4F
		[DataMember]
		public string Mailboxes
		{
			get
			{
				return base["Mailboxes"].StringArrayJoin(",");
			}
			set
			{
				base["Mailboxes"] = value.ToArrayOfStrings();
			}
		}

		// Token: 0x17001F9C RID: 8092
		// (get) Token: 0x06003229 RID: 12841 RVA: 0x0009BD62 File Offset: 0x00099F62
		// (set) Token: 0x0600322A RID: 12842 RVA: 0x0009BD88 File Offset: 0x00099F88
		[DataMember]
		public string LogonTypes
		{
			get
			{
				if (base["LogonTypes"] == null)
				{
					return null;
				}
				return base["LogonTypes"].StringArrayJoin(",");
			}
			set
			{
				if (value == null)
				{
					base["LogonTypes"] = value;
					return;
				}
				base["LogonTypes"] = value.ToArrayOfStrings();
			}
		}

		// Token: 0x17001F9D RID: 8093
		// (get) Token: 0x0600322B RID: 12843 RVA: 0x0009BDAB File Offset: 0x00099FAB
		// (set) Token: 0x0600322C RID: 12844 RVA: 0x0009BDBD File Offset: 0x00099FBD
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

		// Token: 0x17001F9E RID: 8094
		// (get) Token: 0x0600322D RID: 12845 RVA: 0x0009BDD0 File Offset: 0x00099FD0
		// (set) Token: 0x0600322E RID: 12846 RVA: 0x0009BDE7 File Offset: 0x00099FE7
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

		// Token: 0x17001F9F RID: 8095
		// (get) Token: 0x0600322F RID: 12847 RVA: 0x0009BDFC File Offset: 0x00099FFC
		// (set) Token: 0x06003230 RID: 12848 RVA: 0x0009BE30 File Offset: 0x0009A030
		[DataMember]
		public bool ShowDetails
		{
			get
			{
				return base.ParameterIsSpecified("ShowDetails") && ((SwitchParameter)base["ShowDetails"]).ToBool();
			}
			set
			{
				base["ShowDetails"] = new SwitchParameter(value);
			}
		}

		// Token: 0x04002475 RID: 9333
		public const string RbacParameters = "?StartDate&EndDate&Mailboxes&StatusMailRecipients&LogonTypes&ExternalAccess&ShowDetails";
	}
}
