using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004EA RID: 1258
	internal class MiniVirtualDirectorySchema : ADObjectSchema
	{
		// Token: 0x040025F9 RID: 9721
		public static readonly ADPropertyDefinition Server = ADVirtualDirectorySchema.Server;

		// Token: 0x040025FA RID: 9722
		public static readonly ADPropertyDefinition ExternalUrl = ADVirtualDirectorySchema.ExternalUrl;

		// Token: 0x040025FB RID: 9723
		public static readonly ADPropertyDefinition ExternalAuthenticationMethods = ADVirtualDirectorySchema.ExternalAuthenticationMethods;

		// Token: 0x040025FC RID: 9724
		public static readonly ADPropertyDefinition ExternalAuthenticationMethodFlags = ADVirtualDirectorySchema.ExternalAuthenticationMethodFlags;

		// Token: 0x040025FD RID: 9725
		public static readonly ADPropertyDefinition InternalUrl = ADVirtualDirectorySchema.InternalUrl;

		// Token: 0x040025FE RID: 9726
		public static readonly ADPropertyDefinition InternalAuthenticationMethods = ADVirtualDirectorySchema.InternalAuthenticationMethods;

		// Token: 0x040025FF RID: 9727
		public static readonly ADPropertyDefinition InternalAuthenticationMethodFlags = ADVirtualDirectorySchema.InternalAuthenticationMethodFlags;

		// Token: 0x04002600 RID: 9728
		public static readonly ADPropertyDefinition MetabasePath = ExchangeVirtualDirectorySchema.MetabasePath;

		// Token: 0x04002601 RID: 9729
		public static readonly ADPropertyDefinition LiveIdAuthentication = ExchangeWebAppVirtualDirectorySchema.LiveIdAuthentication;

		// Token: 0x04002602 RID: 9730
		public static readonly ADPropertyDefinition AvailabilityForeignConnectorType = ADAvailabilityForeignConnectorVirtualDirectorySchema.AvailabilityForeignConnectorType;

		// Token: 0x04002603 RID: 9731
		public static readonly ADPropertyDefinition AvailabilityForeignConnectorDomains = ADAvailabilityForeignConnectorVirtualDirectorySchema.AvailabilityForeignConnectorDomains;

		// Token: 0x04002604 RID: 9732
		public static readonly ADPropertyDefinition ADFeatureSet = ADEcpVirtualDirectorySchema.ADFeatureSet;

		// Token: 0x04002605 RID: 9733
		public static readonly ADPropertyDefinition AdminEnabled = ADEcpVirtualDirectorySchema.AdminEnabled;

		// Token: 0x04002606 RID: 9734
		public static readonly ADPropertyDefinition OwaOptionsEnabled = ADEcpVirtualDirectorySchema.OwaOptionsEnabled;

		// Token: 0x04002607 RID: 9735
		public static readonly ADPropertyDefinition MobileClientCertificateProvisioningEnabled = ADMobileVirtualDirectorySchema.MobileClientCertificateProvisioningEnabled;

		// Token: 0x04002608 RID: 9736
		public static readonly ADPropertyDefinition MobileClientCertificateAuthorityURL = ADMobileVirtualDirectorySchema.MobileClientCertificateAuthorityURL;

		// Token: 0x04002609 RID: 9737
		public static readonly ADPropertyDefinition MobileClientCertTemplateName = ADMobileVirtualDirectorySchema.MobileClientCertTemplateName;

		// Token: 0x0400260A RID: 9738
		public static readonly ADPropertyDefinition OfflineAddressBooks = ADOabVirtualDirectorySchema.OfflineAddressBooks;

		// Token: 0x0400260B RID: 9739
		public static readonly ADPropertyDefinition OwaVersion = ADOwaVirtualDirectorySchema.OwaVersion;

		// Token: 0x0400260C RID: 9740
		public static readonly ADPropertyDefinition AnonymousFeaturesEnabled = ADOwaVirtualDirectorySchema.AnonymousFeaturesEnabled;

		// Token: 0x0400260D RID: 9741
		public static readonly ADPropertyDefinition FailbackUrl = ADOwaVirtualDirectorySchema.FailbackUrl;

		// Token: 0x0400260E RID: 9742
		public static readonly ADPropertyDefinition IntegratedFeaturesEnabled = ADOwaVirtualDirectorySchema.IntegratedFeaturesEnabled;

		// Token: 0x0400260F RID: 9743
		public static readonly ADPropertyDefinition IISAuthenticationMethods = ADRpcHttpVirtualDirectorySchema.IISAuthenticationMethods;

		// Token: 0x04002610 RID: 9744
		public static readonly ADPropertyDefinition ExternalClientAuthenticationMethod = ADRpcHttpVirtualDirectorySchema.ExternalClientAuthenticationMethod;

		// Token: 0x04002611 RID: 9745
		public static readonly ADPropertyDefinition XropUrl = ADRpcHttpVirtualDirectorySchema.XropUrl;

		// Token: 0x04002612 RID: 9746
		public static readonly ADPropertyDefinition InternalNLBBypassUrl = ADWebServicesVirtualDirectorySchema.InternalNLBBypassUrl;

		// Token: 0x04002613 RID: 9747
		public static readonly ADPropertyDefinition MRSProxyEnabled = ADWebServicesVirtualDirectorySchema.MRSProxyEnabled;
	}
}
