using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003AF RID: 943
	[DataContract]
	public class AdminAuditLogDetailRow : BaseRow
	{
		// Token: 0x0600318D RID: 12685 RVA: 0x00098F79 File Offset: 0x00097179
		public AdminAuditLogDetailRow(AdminAuditLogEvent logEvent) : base(logEvent)
		{
			this.AdminAuditLogEvent = logEvent;
			this.selectedObjectName = logEvent.ObjectModified;
			this.searchObjectName = logEvent.SearchObject;
		}

		// Token: 0x0600318E RID: 12686 RVA: 0x00098FA1 File Offset: 0x000971A1
		internal AdminAuditLogDetailRow(Identity id, AdminAuditLogEvent searchResult) : base(id, searchResult)
		{
			this.AdminAuditLogEvent = searchResult;
			this.selectedObjectName = searchResult.ObjectModified;
			this.searchObjectName = searchResult.SearchObject;
		}

		// Token: 0x0600318F RID: 12687 RVA: 0x00098FCA File Offset: 0x000971CA
		internal AdminAuditLogDetailRow(Identity id, string objectName, AdminAuditLogEvent searchResult) : base(id, searchResult)
		{
			this.AdminAuditLogEvent = searchResult;
			this.selectedObjectName = objectName;
			this.searchObjectName = searchResult.SearchObject;
		}

		// Token: 0x17001F76 RID: 8054
		// (get) Token: 0x06003190 RID: 12688 RVA: 0x00098FEE File Offset: 0x000971EE
		// (set) Token: 0x06003191 RID: 12689 RVA: 0x00098FF6 File Offset: 0x000971F6
		public AdminAuditLogEvent AdminAuditLogEvent { get; private set; }

		// Token: 0x17001F77 RID: 8055
		// (get) Token: 0x06003192 RID: 12690 RVA: 0x00098FFF File Offset: 0x000971FF
		public string UserFriendlyObjectSelected
		{
			get
			{
				return AuditHelper.MakeUserFriendly(this.selectedObjectName);
			}
		}

		// Token: 0x17001F78 RID: 8056
		// (get) Token: 0x06003193 RID: 12691 RVA: 0x0009900C File Offset: 0x0009720C
		public string UserFriendlyCaller
		{
			get
			{
				return AuditHelper.MakeUserFriendly(this.AdminAuditLogEvent.Caller);
			}
		}

		// Token: 0x17001F79 RID: 8057
		// (get) Token: 0x06003194 RID: 12692 RVA: 0x0009901E File Offset: 0x0009721E
		public string SearchObjectName
		{
			get
			{
				return this.searchObjectName;
			}
		}

		// Token: 0x0400240E RID: 9230
		private readonly string searchObjectName;

		// Token: 0x0400240F RID: 9231
		private string selectedObjectName;
	}
}
