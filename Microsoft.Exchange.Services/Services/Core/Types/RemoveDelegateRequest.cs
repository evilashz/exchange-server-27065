using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200046F RID: 1135
	[XmlType("RemoveDelegateType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class RemoveDelegateRequest : BaseDelegateRequest
	{
		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x0600217C RID: 8572 RVA: 0x000A2642 File Offset: 0x000A0842
		// (set) Token: 0x0600217D RID: 8573 RVA: 0x000A264A File Offset: 0x000A084A
		[XmlArrayItem("UserId", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public UserId[] UserIds
		{
			get
			{
				return this.userIds;
			}
			set
			{
				this.userIds = value;
			}
		}

		// Token: 0x0600217E RID: 8574 RVA: 0x000A2653 File Offset: 0x000A0853
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new RemoveDelegate(callContext, this);
		}

		// Token: 0x0600217F RID: 8575 RVA: 0x000A265C File Offset: 0x000A085C
		public RemoveDelegateRequest() : base(true)
		{
		}

		// Token: 0x04001498 RID: 5272
		private UserId[] userIds;
	}
}
