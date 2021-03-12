using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004C0 RID: 1216
	[XmlType("CreateUMCallDataRecordResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class CreateUMCallDataRecordResponseMessage : ResponseMessage
	{
		// Token: 0x060023FB RID: 9211 RVA: 0x000A466C File Offset: 0x000A286C
		public CreateUMCallDataRecordResponseMessage()
		{
		}

		// Token: 0x060023FC RID: 9212 RVA: 0x000A4674 File Offset: 0x000A2874
		internal CreateUMCallDataRecordResponseMessage(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}

		// Token: 0x060023FD RID: 9213 RVA: 0x000A467E File Offset: 0x000A287E
		public override ResponseType GetResponseType()
		{
			return ResponseType.CreateUMCallDataRecordResponseMessage;
		}
	}
}
