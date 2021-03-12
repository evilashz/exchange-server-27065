using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000521 RID: 1313
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("GetUserConfigurationResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetUserConfigurationResponse : BaseResponseMessage
	{
		// Token: 0x060025AF RID: 9647 RVA: 0x000A5D91 File Offset: 0x000A3F91
		public GetUserConfigurationResponse() : base(ResponseType.GetUserConfigurationResponseMessage)
		{
		}

		// Token: 0x060025B0 RID: 9648 RVA: 0x000A5D9B File Offset: 0x000A3F9B
		internal ResponseMessage CreateResponseMessage(ServiceResultCode code, ServiceError error, ServiceUserConfiguration value)
		{
			return new GetUserConfigurationResponseMessage(code, error, value);
		}

		// Token: 0x060025B1 RID: 9649 RVA: 0x000A5DA5 File Offset: 0x000A3FA5
		internal void ProcessServiceResult(ServiceResult<ServiceUserConfiguration> result)
		{
			base.AddResponse(this.CreateResponseMessage(result.Code, result.Error, result.Value));
		}
	}
}
