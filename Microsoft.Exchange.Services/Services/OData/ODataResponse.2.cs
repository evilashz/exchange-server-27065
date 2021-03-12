using System;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000E33 RID: 3635
	internal class ODataResponse<TResult> : ODataResponse
	{
		// Token: 0x06005DB8 RID: 23992 RVA: 0x00123CA1 File Offset: 0x00121EA1
		public ODataResponse(ODataRequest request) : base(request)
		{
		}

		// Token: 0x17001536 RID: 5430
		// (get) Token: 0x06005DB9 RID: 23993 RVA: 0x00123CAA File Offset: 0x00121EAA
		// (set) Token: 0x06005DBA RID: 23994 RVA: 0x00123CB7 File Offset: 0x00121EB7
		public TResult Result
		{
			get
			{
				return (TResult)((object)base.InternalResult);
			}
			set
			{
				base.InternalResult = value;
			}
		}
	}
}
