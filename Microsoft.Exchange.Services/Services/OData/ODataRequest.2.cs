using System;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000E30 RID: 3632
	internal abstract class ODataRequest<TResult> : ODataRequest
	{
		// Token: 0x06005DAC RID: 23980 RVA: 0x00123B33 File Offset: 0x00121D33
		public ODataRequest(ODataContext odataContext) : base(odataContext)
		{
		}
	}
}
