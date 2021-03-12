using System;
using System.ServiceModel.Channels;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000DBB RID: 3515
	internal interface IOutboundInspector
	{
		// Token: 0x06005976 RID: 22902
		void ProcessOutbound(ExchangeVersion requestVersion, Message reply);
	}
}
