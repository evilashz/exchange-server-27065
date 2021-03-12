using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000531 RID: 1329
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("MarkAsJunkResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class MarkAsJunkResponse : BaseResponseMessage
	{
		// Token: 0x060025EB RID: 9707 RVA: 0x000A6114 File Offset: 0x000A4314
		public MarkAsJunkResponse() : base(ResponseType.MarkAsJunkResponseMessage)
		{
		}

		// Token: 0x060025EC RID: 9708 RVA: 0x000A6140 File Offset: 0x000A4340
		internal void AddResponses(ServiceResult<ItemId>[] results)
		{
			ServiceResult<ItemId>.ProcessServiceResults(results, delegate(ServiceResult<ItemId> result)
			{
				base.AddResponse(new MarkAsJunkResponseMessage(result.Code, result.Error, result.Value));
			});
		}
	}
}
