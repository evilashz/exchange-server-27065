using System;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004D6 RID: 1238
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("ExportItemsResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class ExportItemsResponse : BaseInfoResponse
	{
		// Token: 0x06002436 RID: 9270 RVA: 0x000A48E7 File Offset: 0x000A2AE7
		public ExportItemsResponse() : base(ResponseType.ExportItemsResponseMessage)
		{
		}

		// Token: 0x06002437 RID: 9271 RVA: 0x000A48F1 File Offset: 0x000A2AF1
		internal override ResponseMessage CreateResponseMessage<TValue>(ServiceResultCode code, ServiceError error, TValue value)
		{
			return new ExportItemsResponseMessage(code, error, value as XmlNode);
		}

		// Token: 0x06002438 RID: 9272 RVA: 0x000A4935 File Offset: 0x000A2B35
		internal void BuildForExportItemsResults(ServiceResult<ExportItemsResponseMessage>[] serviceResults)
		{
			ServiceResult<ExportItemsResponseMessage>.ProcessServiceResults(serviceResults, delegate(ServiceResult<ExportItemsResponseMessage> serviceResult)
			{
				if (serviceResult.Value == null)
				{
					base.AddResponse(this.CreateResponseMessage<ExportItemsResponseMessage>(serviceResult.Code, serviceResult.Error, null));
					return;
				}
				base.AddResponse(serviceResult.Value);
			});
		}
	}
}
