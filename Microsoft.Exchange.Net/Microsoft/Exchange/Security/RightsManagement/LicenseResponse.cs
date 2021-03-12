using System;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x020009C6 RID: 2502
	internal sealed class LicenseResponse
	{
		// Token: 0x06003691 RID: 13969 RVA: 0x0008B192 File Offset: 0x00089392
		public LicenseResponse(string license, ContentRight? usageRights)
		{
			this.Exception = null;
			this.License = license;
			this.UsageRights = usageRights;
		}

		// Token: 0x06003692 RID: 13970 RVA: 0x0008B1AF File Offset: 0x000893AF
		public LicenseResponse(RightsManagementException exception)
		{
			this.Exception = exception;
		}

		// Token: 0x04002EBA RID: 11962
		public readonly RightsManagementException Exception;

		// Token: 0x04002EBB RID: 11963
		public readonly string License;

		// Token: 0x04002EBC RID: 11964
		public readonly ContentRight? UsageRights;
	}
}
