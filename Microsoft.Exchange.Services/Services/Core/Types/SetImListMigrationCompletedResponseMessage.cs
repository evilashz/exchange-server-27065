using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000559 RID: 1369
	[XmlType("SetImListMigrationCompletedResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class SetImListMigrationCompletedResponseMessage : ResponseMessage
	{
		// Token: 0x0600266D RID: 9837 RVA: 0x000A663A File Offset: 0x000A483A
		public SetImListMigrationCompletedResponseMessage()
		{
		}

		// Token: 0x0600266E RID: 9838 RVA: 0x000A6642 File Offset: 0x000A4842
		internal SetImListMigrationCompletedResponseMessage(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}

		// Token: 0x0600266F RID: 9839 RVA: 0x000A664C File Offset: 0x000A484C
		public override ResponseType GetResponseType()
		{
			return ResponseType.SetImListMigrationCompletedResponseMessage;
		}
	}
}
