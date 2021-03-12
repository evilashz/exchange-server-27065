using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004AC RID: 1196
	[XmlType("ApplyConversationActionResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ApplyConversationActionResponse : BaseResponseMessage
	{
		// Token: 0x060023BE RID: 9150 RVA: 0x000A42A0 File Offset: 0x000A24A0
		public ApplyConversationActionResponse() : base(ResponseType.ApplyConversationActionResponseMessage)
		{
		}

		// Token: 0x060023BF RID: 9151 RVA: 0x000A4317 File Offset: 0x000A2517
		internal void AddResponses(ServiceResult<ApplyConversationActionResponseMessage>[] results)
		{
			ServiceResult<ApplyConversationActionResponseMessage>.ProcessServiceResults(results, delegate(ServiceResult<ApplyConversationActionResponseMessage> result)
			{
				if (ExchangeVersion.Current.Supports(ExchangeVersion.ExchangeV2_1))
				{
					if (result.Value != null)
					{
						base.AddResponse(result.Value);
						return;
					}
					base.AddResponse(new ApplyConversationActionResponseMessage(result.Code, result.Error));
					return;
				}
				else
				{
					if (result.Value != null)
					{
						base.AddResponse(result.Value);
						return;
					}
					base.AddResponse(new ApplyConversationActionResponseMessage());
					return;
				}
			});
		}
	}
}
