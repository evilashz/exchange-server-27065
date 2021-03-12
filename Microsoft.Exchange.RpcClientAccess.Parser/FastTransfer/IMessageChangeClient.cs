using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer
{
	// Token: 0x02000190 RID: 400
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMessageChangeClient : IDisposable
	{
		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060007E1 RID: 2017
		IMessage Message { get; }

		// Token: 0x060007E2 RID: 2018
		void ReportMessageSize(int messageSize);

		// Token: 0x060007E3 RID: 2019
		void ReportIsAssociatedMessage(bool isAssociatedMessage);

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060007E4 RID: 2020
		IPropertyBag MessageHeaderPropertyBag { get; }

		// Token: 0x060007E5 RID: 2021
		void SetPartial();

		// Token: 0x060007E6 RID: 2022
		void ScrubGroupProperties(PropertyGroupMapping mapping, int groupIndex);
	}
}
