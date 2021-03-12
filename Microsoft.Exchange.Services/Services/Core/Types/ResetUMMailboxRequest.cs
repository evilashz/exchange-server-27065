using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000473 RID: 1139
	[XmlType("ResetUMMailboxType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class ResetUMMailboxRequest : BaseRequest
	{
		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06002194 RID: 8596 RVA: 0x000A2736 File Offset: 0x000A0936
		// (set) Token: 0x06002195 RID: 8597 RVA: 0x000A273E File Offset: 0x000A093E
		[DataMember(Name = "KeepProperties")]
		[XmlElement("KeepProperties")]
		public bool KeepProperties { get; set; }

		// Token: 0x06002196 RID: 8598 RVA: 0x000A2747 File Offset: 0x000A0947
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new ResetUMMailbox(callContext, this);
		}

		// Token: 0x06002197 RID: 8599 RVA: 0x000A2750 File Offset: 0x000A0950
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x06002198 RID: 8600 RVA: 0x000A2753 File Offset: 0x000A0953
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}
	}
}
