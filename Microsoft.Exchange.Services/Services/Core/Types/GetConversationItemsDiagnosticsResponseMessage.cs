using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004F2 RID: 1266
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetConversationItemsDiagnosticsResponseMessage : ResponseMessage
	{
		// Token: 0x060024CB RID: 9419 RVA: 0x000A52E4 File Offset: 0x000A34E4
		public GetConversationItemsDiagnosticsResponseMessage()
		{
		}

		// Token: 0x060024CC RID: 9420 RVA: 0x000A52EC File Offset: 0x000A34EC
		internal GetConversationItemsDiagnosticsResponseMessage(ServiceResultCode code, ServiceError error, GetConversationItemsDiagnosticsResponseType diagnostics) : base(code, error)
		{
			this.Diagnostics = diagnostics;
		}

		// Token: 0x060024CD RID: 9421 RVA: 0x000A52FD File Offset: 0x000A34FD
		public override ResponseType GetResponseType()
		{
			return ResponseType.GetConversationItemsDiagnosticsResponseMessage;
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x060024CE RID: 9422 RVA: 0x000A5301 File Offset: 0x000A3501
		// (set) Token: 0x060024CF RID: 9423 RVA: 0x000A5309 File Offset: 0x000A3509
		[DataMember(EmitDefaultValue = false)]
		public GetConversationItemsDiagnosticsResponseType Diagnostics { get; set; }
	}
}
