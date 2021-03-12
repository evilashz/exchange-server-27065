using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200070E RID: 1806
	internal class RequestedLogonType
	{
		// Token: 0x0600371B RID: 14107 RVA: 0x000C543A File Offset: 0x000C363A
		private RequestedLogonType(LogonTypeSource source, LogonType type)
		{
			this.Source = source;
			this.LogonType = type;
		}

		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x0600371C RID: 14108 RVA: 0x000C5450 File Offset: 0x000C3650
		// (set) Token: 0x0600371D RID: 14109 RVA: 0x000C5458 File Offset: 0x000C3658
		public LogonType LogonType { get; private set; }

		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x0600371E RID: 14110 RVA: 0x000C5461 File Offset: 0x000C3661
		// (set) Token: 0x0600371F RID: 14111 RVA: 0x000C5469 File Offset: 0x000C3669
		public LogonTypeSource Source { get; private set; }

		// Token: 0x04001EB4 RID: 7860
		public static readonly RequestedLogonType AdminFromManagementRoleHeader = new RequestedLogonType(LogonTypeSource.ManagementRoleHeader, LogonType.Admin);

		// Token: 0x04001EB5 RID: 7861
		public static readonly RequestedLogonType SystemServiceFromManagementRoleHeader = new RequestedLogonType(LogonTypeSource.ManagementRoleHeader, LogonType.SystemService);

		// Token: 0x04001EB6 RID: 7862
		public static readonly RequestedLogonType AdminFromOpenAsAdminOrSystemServiceHeader = new RequestedLogonType(LogonTypeSource.OpenAsAdminOrSystemServiceHeader, LogonType.Admin);

		// Token: 0x04001EB7 RID: 7863
		public static readonly RequestedLogonType SystemServiceFromOpenAsAdminOrSystemServiceHeader = new RequestedLogonType(LogonTypeSource.OpenAsAdminOrSystemServiceHeader, LogonType.SystemService);

		// Token: 0x04001EB8 RID: 7864
		public static readonly RequestedLogonType Default = new RequestedLogonType(LogonTypeSource.Default, LogonType.BestAccess);
	}
}
