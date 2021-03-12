using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000E28 RID: 3624
	internal abstract class ODataCommand<TRequest, TResponse> : ODataCommand where TRequest : ODataRequest where TResponse : ODataResponse
	{
		// Token: 0x06005D6B RID: 23915 RVA: 0x0012312A File Offset: 0x0012132A
		public ODataCommand(TRequest request)
		{
			ArgumentValidator.ThrowIfNull("request", request);
			this.Request = request;
		}

		// Token: 0x17001520 RID: 5408
		// (get) Token: 0x06005D6C RID: 23916 RVA: 0x00123149 File Offset: 0x00121349
		// (set) Token: 0x06005D6D RID: 23917 RVA: 0x00123151 File Offset: 0x00121351
		public TRequest Request { get; private set; }

		// Token: 0x06005D6E RID: 23918 RVA: 0x0012315C File Offset: 0x0012135C
		public override object Execute()
		{
			object result;
			try
			{
				result = this.InternalExecute();
			}
			catch (Exception)
			{
				throw;
			}
			return result;
		}

		// Token: 0x06005D6F RID: 23919 RVA: 0x0012318C File Offset: 0x0012138C
		protected override void InternalDispose(bool disposing)
		{
		}

		// Token: 0x06005D70 RID: 23920 RVA: 0x0012318E File Offset: 0x0012138E
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ODataCommand<TRequest, TResponse>>(this);
		}

		// Token: 0x06005D71 RID: 23921
		protected abstract TResponse InternalExecute();
	}
}
