using System;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200020B RID: 523
	internal struct RequestTypeInfo
	{
		// Token: 0x04001F9A RID: 8090
		public bool Need302Redirect;

		// Token: 0x04001F9B RID: 8091
		public bool NeedRedirectTargetTenant;

		// Token: 0x04001F9C RID: 8092
		public bool IsEsoRequest;

		// Token: 0x04001F9D RID: 8093
		public string EsoMailboxSmtpAddress;

		// Token: 0x04001F9E RID: 8094
		public bool IsDelegatedAdminRequest;

		// Token: 0x04001F9F RID: 8095
		public bool IsByoidAdmin;

		// Token: 0x04001FA0 RID: 8096
		public string TargetTenant;

		// Token: 0x04001FA1 RID: 8097
		public bool UseImplicitPathRewrite;

		// Token: 0x04001FA2 RID: 8098
		public bool IsSecurityTokenPresented;
	}
}
