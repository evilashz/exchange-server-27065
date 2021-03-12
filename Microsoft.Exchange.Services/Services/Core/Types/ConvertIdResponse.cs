using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004B6 RID: 1206
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "ConvertIdResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class ConvertIdResponse : BaseResponseMessage
	{
		// Token: 0x060023E8 RID: 9192 RVA: 0x000A4561 File Offset: 0x000A2761
		public ConvertIdResponse() : base(ResponseType.ConvertIdResponseMessage)
		{
		}

		// Token: 0x060023E9 RID: 9193 RVA: 0x000A458A File Offset: 0x000A278A
		internal void AddResponses(ServiceResult<AlternateIdBase>[] serviceResults)
		{
			ServiceResult<AlternateIdBase>.ProcessServiceResults(serviceResults, delegate(ServiceResult<AlternateIdBase> result)
			{
				base.AddResponse(new ConvertIdResponseMessage(result.Code, result.Error, result.Value));
			});
		}
	}
}
