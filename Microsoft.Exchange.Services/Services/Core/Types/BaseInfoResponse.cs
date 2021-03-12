using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004AE RID: 1198
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public abstract class BaseInfoResponse : BaseResponseMessage
	{
		// Token: 0x060023C5 RID: 9157 RVA: 0x000A434E File Offset: 0x000A254E
		internal BaseInfoResponse(ResponseType responseType) : base(responseType)
		{
		}

		// Token: 0x060023C6 RID: 9158 RVA: 0x000A4357 File Offset: 0x000A2557
		internal virtual void ProcessServiceResult<TValue>(ServiceResult<TValue> result)
		{
			base.AddResponse(this.CreateResponseMessage<TValue>(result.Code, result.Error, result.Value));
		}

		// Token: 0x060023C7 RID: 9159
		internal abstract ResponseMessage CreateResponseMessage<TValue>(ServiceResultCode code, ServiceError error, TValue item);

		// Token: 0x060023C8 RID: 9160 RVA: 0x000A4377 File Offset: 0x000A2577
		internal void BuildForResults<TValue>(ServiceResult<TValue>[] serviceResults)
		{
			ServiceResult<TValue>.ProcessServiceResults(serviceResults, new ProcessServiceResult<TValue>(this.ProcessServiceResult<TValue>));
		}

		// Token: 0x020004AF RID: 1199
		// (Invoke) Token: 0x060023CA RID: 9162
		internal delegate BaseInfoResponse CreateBaseInfoResponse();
	}
}
