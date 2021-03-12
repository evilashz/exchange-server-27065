using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Security.RightsManagement.SOAP.Server
{
	// Token: 0x020009EA RID: 2538
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlType(Namespace = "http://microsoft.com/DRM/ServerService")]
	[Serializable]
	public enum ServiceType
	{
		// Token: 0x04002F0E RID: 12046
		EnrollmentService,
		// Token: 0x04002F0F RID: 12047
		LicensingService,
		// Token: 0x04002F10 RID: 12048
		PublishingService,
		// Token: 0x04002F11 RID: 12049
		CertificationService,
		// Token: 0x04002F12 RID: 12050
		ActivationService,
		// Token: 0x04002F13 RID: 12051
		PrecertificationService,
		// Token: 0x04002F14 RID: 12052
		ServerService,
		// Token: 0x04002F15 RID: 12053
		DrmRemoteDirectoryServices,
		// Token: 0x04002F16 RID: 12054
		GroupExpansionService,
		// Token: 0x04002F17 RID: 12055
		LicensingInternalService,
		// Token: 0x04002F18 RID: 12056
		CertificationInternalService,
		// Token: 0x04002F19 RID: 12057
		ServerLicensingWSService,
		// Token: 0x04002F1A RID: 12058
		CertificationWSService,
		// Token: 0x04002F1B RID: 12059
		PreLicensingWSService,
		// Token: 0x04002F1C RID: 12060
		PublishingWSService,
		// Token: 0x04002F1D RID: 12061
		TemplateDistributionWSService,
		// Token: 0x04002F1E RID: 12062
		ServerLicensingMexService,
		// Token: 0x04002F1F RID: 12063
		CertificationMexService,
		// Token: 0x04002F20 RID: 12064
		PreLicensingMexService,
		// Token: 0x04002F21 RID: 12065
		PublishingMexService,
		// Token: 0x04002F22 RID: 12066
		TemplateDistributionMexService
	}
}
