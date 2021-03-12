using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000579 RID: 1401
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("UpdateItemResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class UpdateItemResponse : ItemInfoResponse
	{
		// Token: 0x06002706 RID: 9990 RVA: 0x000A6D4F File Offset: 0x000A4F4F
		public UpdateItemResponse() : base(ResponseType.UpdateItemResponseMessage)
		{
		}

		// Token: 0x06002707 RID: 9991 RVA: 0x000A6D59 File Offset: 0x000A4F59
		public UpdateItemResponse(ResponseType responseType) : base(responseType)
		{
		}

		// Token: 0x06002708 RID: 9992 RVA: 0x000A6D62 File Offset: 0x000A4F62
		internal void BuildForUpdateItemResults(ServiceResult<UpdateItemResponseWrapper>[] serviceResults)
		{
			ServiceResult<UpdateItemResponseWrapper>.ProcessServiceResults(serviceResults, new ProcessServiceResult<UpdateItemResponseWrapper>(this.ProcessServiceResult));
		}

		// Token: 0x06002709 RID: 9993 RVA: 0x000A6D77 File Offset: 0x000A4F77
		internal virtual void ProcessServiceResult(ServiceResult<UpdateItemResponseWrapper> result)
		{
			base.AddResponse(this.CreateUpdateItemResponseMessage(result.Code, result.Error, result.Value));
		}

		// Token: 0x0600270A RID: 9994 RVA: 0x000A6D98 File Offset: 0x000A4F98
		internal ResponseMessage CreateUpdateItemResponseMessage(ServiceResultCode code, ServiceError error, UpdateItemResponseWrapper value)
		{
			ConflictResults conflictResults = null;
			ItemType item = null;
			if (value != null)
			{
				item = value.Item;
				if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2007SP1))
				{
					conflictResults = value.ConflictResults;
				}
			}
			return new UpdateItemResponseMessage(code, error, item, conflictResults);
		}
	}
}
