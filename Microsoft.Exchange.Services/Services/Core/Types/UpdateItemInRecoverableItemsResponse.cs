using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000576 RID: 1398
	[XmlType("UpdateItemInRecoverableItemsResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UpdateItemInRecoverableItemsResponse : ItemInfoResponse
	{
		// Token: 0x060026F5 RID: 9973 RVA: 0x000A6C32 File Offset: 0x000A4E32
		public UpdateItemInRecoverableItemsResponse() : base(ResponseType.UpdateItemInRecoverableItemsResponseMessage)
		{
		}

		// Token: 0x060026F6 RID: 9974 RVA: 0x000A6C3C File Offset: 0x000A4E3C
		internal void BuildForUpdateItemInRecoverableItemsResults(ServiceResult<UpdateItemInRecoverableItemsResponseWrapper>[] serviceResults)
		{
			ServiceResult<UpdateItemInRecoverableItemsResponseWrapper>.ProcessServiceResults(serviceResults, new ProcessServiceResult<UpdateItemInRecoverableItemsResponseWrapper>(this.ProcessServiceResult));
		}

		// Token: 0x060026F7 RID: 9975 RVA: 0x000A6C51 File Offset: 0x000A4E51
		internal virtual void ProcessServiceResult(ServiceResult<UpdateItemInRecoverableItemsResponseWrapper> result)
		{
			base.AddResponse(this.CreateUpdateItemInRecoverableItemsResponseMessage(result.Code, result.Error, result.Value));
		}

		// Token: 0x060026F8 RID: 9976 RVA: 0x000A6C74 File Offset: 0x000A4E74
		internal ResponseMessage CreateUpdateItemInRecoverableItemsResponseMessage(ServiceResultCode code, ServiceError error, UpdateItemInRecoverableItemsResponseWrapper value)
		{
			ConflictResults conflictResults = null;
			ItemType item = null;
			AttachmentType[] attachments = null;
			if (value != null)
			{
				item = value.Item;
				attachments = value.Attachments;
				if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2007SP1))
				{
					conflictResults = value.ConflictResults;
				}
			}
			return new UpdateItemInRecoverableItemsResponseMessage(code, error, item, attachments, conflictResults);
		}
	}
}
