using System;
using System.ServiceModel.Channels;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000DBA RID: 3514
	internal interface IInboundInspector
	{
		// Token: 0x06005975 RID: 22901
		void ProcessInbound(ExchangeVersion requestVersion, Message request);
	}
}
