using System;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004D2 RID: 1234
	[XmlType("ExecuteDiagnosticMethodResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public sealed class ExecuteDiagnosticMethodResponse : BaseInfoResponse
	{
		// Token: 0x06002429 RID: 9257 RVA: 0x000A4837 File Offset: 0x000A2A37
		public ExecuteDiagnosticMethodResponse() : base(ResponseType.ExecuteDiagnosticMethodResponseMessage)
		{
		}

		// Token: 0x0600242A RID: 9258 RVA: 0x000A4841 File Offset: 0x000A2A41
		internal override ResponseMessage CreateResponseMessage<TValue>(ServiceResultCode code, ServiceError error, TValue value)
		{
			return new ExecuteDiagnosticMethodResponseMessage(code, error, value as XmlNode);
		}

		// Token: 0x0600242B RID: 9259 RVA: 0x000A4855 File Offset: 0x000A2A55
		internal override void ProcessServiceResult<TValue>(ServiceResult<TValue> result)
		{
			base.AddResponse(this.CreateResponseMessage<TValue>(result.Code, result.Error, result.Value));
		}
	}
}
