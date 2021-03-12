using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003B6 RID: 950
	[DataContract]
	public class AdminAuditLogResultRow : BaseRow
	{
		// Token: 0x060031BC RID: 12732 RVA: 0x00099BB2 File Offset: 0x00097DB2
		public AdminAuditLogResultRow(AdminAuditLogEvent searchResult)
		{
			this.AuditReportSearchBaseResult = searchResult;
		}

		// Token: 0x060031BD RID: 12733 RVA: 0x00099BC1 File Offset: 0x00097DC1
		internal AdminAuditLogResultRow(Identity id, AdminAuditLogEvent searchResult) : base(id, searchResult)
		{
			this.AuditReportSearchBaseResult = searchResult;
		}

		// Token: 0x17001F80 RID: 8064
		// (get) Token: 0x060031BE RID: 12734 RVA: 0x00099BD2 File Offset: 0x00097DD2
		// (set) Token: 0x060031BF RID: 12735 RVA: 0x00099BDA File Offset: 0x00097DDA
		public AdminAuditLogEvent AuditReportSearchBaseResult { get; private set; }

		// Token: 0x17001F81 RID: 8065
		// (get) Token: 0x060031C0 RID: 12736 RVA: 0x00099BE3 File Offset: 0x00097DE3
		// (set) Token: 0x060031C1 RID: 12737 RVA: 0x00099BF0 File Offset: 0x00097DF0
		[DataMember]
		public string ObjectModified
		{
			get
			{
				return this.AuditReportSearchBaseResult.ObjectModified;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001F82 RID: 8066
		// (get) Token: 0x060031C2 RID: 12738 RVA: 0x00099BF7 File Offset: 0x00097DF7
		// (set) Token: 0x060031C3 RID: 12739 RVA: 0x00099C04 File Offset: 0x00097E04
		[DataMember]
		public string Cmdlet
		{
			get
			{
				return this.AuditReportSearchBaseResult.CmdletName;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001F83 RID: 8067
		// (get) Token: 0x060031C4 RID: 12740 RVA: 0x00099C0B File Offset: 0x00097E0B
		// (set) Token: 0x060031C5 RID: 12741 RVA: 0x00099C18 File Offset: 0x00097E18
		[DataMember]
		public string SearchObject
		{
			get
			{
				return this.AuditReportSearchBaseResult.SearchObject;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001F84 RID: 8068
		// (get) Token: 0x060031C6 RID: 12742 RVA: 0x00099C1F File Offset: 0x00097E1F
		// (set) Token: 0x060031C7 RID: 12743 RVA: 0x00099C40 File Offset: 0x00097E40
		[DataMember]
		public string FriendlyObjectModified
		{
			get
			{
				if (this.internalFriendlyObjectModified == null)
				{
					return AuditHelper.MakeUserFriendly(this.AuditReportSearchBaseResult.ModifiedObjectResolvedName);
				}
				return this.internalFriendlyObjectModified;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("FriendlyObjectModified value");
				}
				this.internalFriendlyObjectModified = value;
			}
		}

		// Token: 0x17001F85 RID: 8069
		// (get) Token: 0x060031C8 RID: 12744 RVA: 0x00099C58 File Offset: 0x00097E58
		// (set) Token: 0x060031C9 RID: 12745 RVA: 0x00099C94 File Offset: 0x00097E94
		[DataMember]
		public string RunDate
		{
			get
			{
				if (this.internalRunDate == null)
				{
					return this.AuditReportSearchBaseResult.RunDate.Value.ToUniversalTime().UtcToUserDateTimeString();
				}
				return this.internalRunDate;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("RunDate value");
				}
				this.internalRunDate = value;
			}
		}

		// Token: 0x04002422 RID: 9250
		private string internalFriendlyObjectModified;

		// Token: 0x04002423 RID: 9251
		private string internalRunDate;
	}
}
