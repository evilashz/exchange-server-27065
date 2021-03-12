using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.ApplicationLogic.TextMessaging;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x02000040 RID: 64
	internal class SmtpToSmsGatewayCapability : MobileServiceCapability
	{
		// Token: 0x06000157 RID: 343 RVA: 0x00007DCE File Offset: 0x00005FCE
		internal SmtpToSmsGatewayCapability(PartType supportedPartType, int segmentPerPart, IList<CodingSupportability> codingSupportabilities, FeatureSupportability featureSupportabilities, TextMessagingHostingDataServicesServiceSmtpToSmsGateway gatewayParameters) : base(supportedPartType, segmentPerPart, codingSupportabilities, featureSupportabilities)
		{
			this.Parameters = gatewayParameters;
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00007DE3 File Offset: 0x00005FE3
		// (set) Token: 0x06000159 RID: 345 RVA: 0x00007DEB File Offset: 0x00005FEB
		public TextMessagingHostingDataServicesServiceSmtpToSmsGateway Parameters { get; private set; }
	}
}
