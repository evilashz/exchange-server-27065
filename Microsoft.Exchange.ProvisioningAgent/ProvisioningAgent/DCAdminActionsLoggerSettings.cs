using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ProvisioningAgent;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x02000018 RID: 24
	internal class DCAdminActionsLoggerSettings : ActivityContextLogFileSettings
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00005471 File Offset: 0x00003671
		protected override string LogTypeName
		{
			get
			{
				return "Admin Audit Logs for DC Admin Actions";
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00005478 File Offset: 0x00003678
		protected override string LogSubFolderName
		{
			get
			{
				return "DCAdminActionsLog";
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x0000547F File Offset: 0x0000367F
		protected override Trace Tracer
		{
			get
			{
				return ExTraceGlobals.AdminAuditLogTracer;
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00005486 File Offset: 0x00003686
		internal static DCAdminActionsLoggerSettings Load()
		{
			return new DCAdminActionsLoggerSettings();
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x0000548D File Offset: 0x0000368D
		private DCAdminActionsLoggerSettings()
		{
		}

		// Token: 0x04000069 RID: 105
		internal const string DCAdminActionsLoggerSubFolderName = "DCAdminActionsLog";
	}
}
