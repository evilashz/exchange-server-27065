using System;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000E31 RID: 3633
	internal abstract class EmptyResultRequest : ODataRequest<EmptyResult>
	{
		// Token: 0x06005DAD RID: 23981 RVA: 0x00123B3C File Offset: 0x00121D3C
		public EmptyResultRequest(ODataContext odataContext) : base(odataContext)
		{
		}
	}
}
