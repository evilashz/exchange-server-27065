using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x0200001C RID: 28
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[KnownType(typeof(UnseenDataType))]
	[Serializable]
	public class UnseenCountNotification : ApplicationNotification
	{
		// Token: 0x06000092 RID: 146 RVA: 0x000033CD File Offset: 0x000015CD
		public UnseenCountNotification() : base(NotificationType.UnseenCount)
		{
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000093 RID: 147 RVA: 0x000033D6 File Offset: 0x000015D6
		// (set) Token: 0x06000094 RID: 148 RVA: 0x000033DE File Offset: 0x000015DE
		[DataMember(EmitDefaultValue = false)]
		public UnseenDataType UnseenData { get; set; }
	}
}
