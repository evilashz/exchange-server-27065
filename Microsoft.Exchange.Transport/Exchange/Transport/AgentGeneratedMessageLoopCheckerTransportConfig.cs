using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000004 RID: 4
	internal class AgentGeneratedMessageLoopCheckerTransportConfig : AgentGeneratedMessageLoopCheckerConfig
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000022D6 File Offset: 0x000004D6
		internal AgentGeneratedMessageLoopCheckerTransportConfig(ITransportConfiguration transportConfiguration)
		{
			this.transportConfiguration = transportConfiguration;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022E5 File Offset: 0x000004E5
		internal override bool GetIsEnabledInSubmission()
		{
			return this.transportConfiguration.TransportSettings.TransportSettings.AgentGeneratedMessageLoopDetectionInSubmissionEnabled;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022FC File Offset: 0x000004FC
		internal override bool GetIsEnabledInSmtp()
		{
			return this.transportConfiguration.TransportSettings.TransportSettings.AgentGeneratedMessageLoopDetectionInSmtpEnabled;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002313 File Offset: 0x00000513
		internal override uint GetMaxAllowedMessageDepth()
		{
			return this.transportConfiguration.TransportSettings.TransportSettings.MaxAllowedAgentGeneratedMessageDepth;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000232A File Offset: 0x0000052A
		internal override uint GetMaxAllowedMessageDepthPerAgent()
		{
			return this.transportConfiguration.TransportSettings.TransportSettings.MaxAllowedAgentGeneratedMessageDepthPerAgent;
		}

		// Token: 0x04000004 RID: 4
		private readonly ITransportConfiguration transportConfiguration;
	}
}
