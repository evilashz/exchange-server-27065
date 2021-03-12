using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200046D RID: 1133
	[XmlType("RemoveAggregatedAccountRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class RemoveAggregatedAccountRequest : BaseAggregatedAccountRequest
	{
		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06002172 RID: 8562 RVA: 0x000A25E0 File Offset: 0x000A07E0
		// (set) Token: 0x06002173 RID: 8563 RVA: 0x000A25E8 File Offset: 0x000A07E8
		[XmlElement]
		public string EmailAddress { get; set; }

		// Token: 0x06002174 RID: 8564 RVA: 0x000A25F1 File Offset: 0x000A07F1
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new RemoveAggregatedAccount(callContext, this);
		}
	}
}
