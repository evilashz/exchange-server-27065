using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000544 RID: 1348
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("ResolveNamesResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class ResolveNamesResponse : BaseInfoResponse
	{
		// Token: 0x06002636 RID: 9782 RVA: 0x000A63F4 File Offset: 0x000A45F4
		public ResolveNamesResponse() : base(ResponseType.ResolveNamesResponseMessage)
		{
		}

		// Token: 0x06002637 RID: 9783 RVA: 0x000A63FE File Offset: 0x000A45FE
		internal override ResponseMessage CreateResponseMessage<TValue>(ServiceResultCode code, ServiceError error, TValue value)
		{
			return new ResolveNamesResponseMessage(code, error, value as ResolutionSet);
		}
	}
}
