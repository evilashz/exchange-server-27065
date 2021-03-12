using System;

namespace Microsoft.Exchange.Clients.Owa.Core.Controls
{
	// Token: 0x020002BC RID: 700
	internal sealed class DefaultClientViewState : ClientViewState
	{
		// Token: 0x06001B6B RID: 7019 RVA: 0x0009D3FC File Offset: 0x0009B5FC
		public override PreFormActionResponse ToPreFormActionResponse()
		{
			return new PreFormActionResponse
			{
				ApplicationElement = ApplicationElement.StartPage
			};
		}
	}
}
