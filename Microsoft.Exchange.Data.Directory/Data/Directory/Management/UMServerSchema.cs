using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x0200077E RID: 1918
	internal class UMServerSchema : ADPresentationSchema
	{
		// Token: 0x06005FAD RID: 24493 RVA: 0x00146515 File Offset: 0x00144715
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<ActiveDirectoryServerSchema>();
		}

		// Token: 0x0400403E RID: 16446
		public static readonly ADPropertyDefinition Status = ServerSchema.Status;

		// Token: 0x0400403F RID: 16447
		public static readonly ADPropertyDefinition Languages = ServerSchema.Languages;

		// Token: 0x04004040 RID: 16448
		public static readonly ADPropertyDefinition MaxCallsAllowed = ServerSchema.MaxCallsAllowed;

		// Token: 0x04004041 RID: 16449
		public static readonly ADPropertyDefinition DialPlans = ServerSchema.DialPlans;

		// Token: 0x04004042 RID: 16450
		public static readonly ADPropertyDefinition GrammarGenerationSchedule = ServerSchema.GrammarGenerationSchedule;

		// Token: 0x04004043 RID: 16451
		public static readonly ADPropertyDefinition ExternalHostFqdn = ServerSchema.ExternalHostFqdn;

		// Token: 0x04004044 RID: 16452
		public static readonly ADPropertyDefinition SipTcpListeningPort = ActiveDirectoryServerSchema.SipTcpListeningPort;

		// Token: 0x04004045 RID: 16453
		public static readonly ADPropertyDefinition SipTlsListeningPort = ActiveDirectoryServerSchema.SipTlsListeningPort;

		// Token: 0x04004046 RID: 16454
		public static readonly ADPropertyDefinition ExternalServiceFqdn = ActiveDirectoryServerSchema.ExternalServiceFqdn;

		// Token: 0x04004047 RID: 16455
		public static readonly ADPropertyDefinition UMPodRedirectTemplate = ServerSchema.UMPodRedirectTemplate;

		// Token: 0x04004048 RID: 16456
		public static readonly ADPropertyDefinition UMForwardingAddressTemplate = ServerSchema.UMForwardingAddressTemplate;

		// Token: 0x04004049 RID: 16457
		public static readonly ADPropertyDefinition UMStartupMode = ServerSchema.UMStartupMode;

		// Token: 0x0400404A RID: 16458
		public static readonly ADPropertyDefinition UMCertificateThumbprint = ServerSchema.UMCertificateThumbprint;

		// Token: 0x0400404B RID: 16459
		public static readonly ADPropertyDefinition SIPAccessService = ServerSchema.SIPAccessService;

		// Token: 0x0400404C RID: 16460
		public static readonly ADPropertyDefinition IrmLogPath = ServerSchema.IrmLogPath;

		// Token: 0x0400404D RID: 16461
		public static readonly ADPropertyDefinition IrmLogMaxAge = ServerSchema.IrmLogMaxAge;

		// Token: 0x0400404E RID: 16462
		public static readonly ADPropertyDefinition IrmLogMaxDirectorySize = ServerSchema.IrmLogMaxDirectorySize;

		// Token: 0x0400404F RID: 16463
		public static readonly ADPropertyDefinition IrmLogMaxFileSize = ServerSchema.IrmLogMaxFileSize;

		// Token: 0x04004050 RID: 16464
		public static readonly ADPropertyDefinition IrmLogEnabled = ServerSchema.IrmLogEnabled;

		// Token: 0x04004051 RID: 16465
		public static readonly ADPropertyDefinition IPAddressFamilyConfigurable = ActiveDirectoryServerSchema.IPAddressFamilyConfigurable;

		// Token: 0x04004052 RID: 16466
		public static readonly ADPropertyDefinition IPAddressFamily = ActiveDirectoryServerSchema.IPAddressFamily;
	}
}
