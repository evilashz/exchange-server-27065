using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000A9 RID: 169
	[Flags]
	public enum SmtpReceiveCapabilities
	{
		// Token: 0x04000289 RID: 649
		None = 0,
		// Token: 0x0400028A RID: 650
		[LocDescription(DataStrings.IDs.SmtpReceiveCapabilitiesAcceptOorgHeader)]
		AcceptOorgHeader = 1,
		// Token: 0x0400028B RID: 651
		[LocDescription(DataStrings.IDs.SmtpReceiveCapabilitiesAcceptOorgProtocol)]
		AcceptOorgProtocol = 2,
		// Token: 0x0400028C RID: 652
		[LocDescription(DataStrings.IDs.SmtpReceiveCapabilitiesAcceptProxyProtocol)]
		AcceptProxyProtocol = 4,
		// Token: 0x0400028D RID: 653
		[LocDescription(DataStrings.IDs.SmtpReceiveCapabilitiesAcceptProxyFromProtocol)]
		AcceptProxyFromProtocol = 8,
		// Token: 0x0400028E RID: 654
		[LocDescription(DataStrings.IDs.SmtpReceiveCapabilitiesAcceptProxyToProtocol)]
		AcceptProxyToProtocol = 16,
		// Token: 0x0400028F RID: 655
		[LocDescription(DataStrings.IDs.SmtpReceiveCapabilitiesAcceptXAttrProtocol)]
		AcceptXAttrProtocol = 32,
		// Token: 0x04000290 RID: 656
		[LocDescription(DataStrings.IDs.SmtpReceiveCapabilitiesAcceptOrgHeaders)]
		AcceptOrgHeaders = 64,
		// Token: 0x04000291 RID: 657
		[LocDescription(DataStrings.IDs.SmtpReceiveCapabilitiesAcceptXSysProbeProtocol)]
		AcceptXSysProbeProtocol = 128,
		// Token: 0x04000292 RID: 658
		[LocDescription(DataStrings.IDs.SmtpReceiveCapabilitiesAcceptCrossForestMail)]
		AcceptCrossForestMail = 256,
		// Token: 0x04000293 RID: 659
		[LocDescription(DataStrings.IDs.SmtpReceiveCapabilitiesAcceptCloudServicesMail)]
		AcceptCloudServicesMail = 512,
		// Token: 0x04000294 RID: 660
		[LocDescription(DataStrings.IDs.SmtpReceiveCapabilitiesAllowSubmit)]
		AllowSubmit = 1024,
		// Token: 0x04000295 RID: 661
		[LocDescription(DataStrings.IDs.SmtpReceiveCapabilitiesAcceptXOriginalFromProtocol)]
		AcceptXOriginalFromProtocol = 2048,
		// Token: 0x04000296 RID: 662
		[LocDescription(DataStrings.IDs.SmtpReceiveCapabilitiesAllowConsumerMail)]
		AllowConsumerMail = 4096
	}
}
