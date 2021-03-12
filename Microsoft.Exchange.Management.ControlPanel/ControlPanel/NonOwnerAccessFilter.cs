using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003E7 RID: 999
	[DataContract]
	public class NonOwnerAccessFilter : AdminAuditLogSearchFilter
	{
		// Token: 0x17002018 RID: 8216
		// (get) Token: 0x0600334E RID: 13134 RVA: 0x0009F340 File Offset: 0x0009D540
		public override string RbacScope
		{
			get
			{
				return "@R:Organization";
			}
		}

		// Token: 0x17002019 RID: 8217
		// (get) Token: 0x0600334F RID: 13135 RVA: 0x0009F347 File Offset: 0x0009D547
		public override string AssociatedCmdlet
		{
			get
			{
				return "Search-MailboxAuditLog";
			}
		}

		// Token: 0x1700201A RID: 8218
		// (get) Token: 0x06003350 RID: 13136 RVA: 0x0009F34E File Offset: 0x0009D54E
		// (set) Token: 0x06003351 RID: 13137 RVA: 0x0009F365 File Offset: 0x0009D565
		[DataMember]
		public override string ObjectIds
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

		// Token: 0x1700201B RID: 8219
		// (get) Token: 0x06003352 RID: 13138 RVA: 0x0009F378 File Offset: 0x0009D578
		// (set) Token: 0x06003353 RID: 13139 RVA: 0x0009F39E File Offset: 0x0009D59E
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

		// Token: 0x1700201C RID: 8220
		// (get) Token: 0x06003354 RID: 13140 RVA: 0x0009F3C1 File Offset: 0x0009D5C1
		// (set) Token: 0x06003355 RID: 13141 RVA: 0x0009F3D3 File Offset: 0x0009D5D3
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

		// Token: 0x040024E2 RID: 9442
		public new const string RbacParameters = "?ResultSize&StartDate&EndDate&Mailboxes&LogonTypes&ExternalAccess";
	}
}
