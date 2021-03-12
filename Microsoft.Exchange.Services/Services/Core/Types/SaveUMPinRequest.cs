using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.UM.Rpc;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000475 RID: 1141
	[XmlType("SaveUMPinType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class SaveUMPinRequest : BaseRequest
	{
		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x060021AD RID: 8621 RVA: 0x000A2846 File Offset: 0x000A0A46
		// (set) Token: 0x060021AE RID: 8622 RVA: 0x000A284E File Offset: 0x000A0A4E
		[DataMember(Name = "PinInfo")]
		[XmlElement("PinInfo")]
		public PINInfo PinInfo { get; set; }

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x060021AF RID: 8623 RVA: 0x000A2857 File Offset: 0x000A0A57
		// (set) Token: 0x060021B0 RID: 8624 RVA: 0x000A285F File Offset: 0x000A0A5F
		[DataMember(Name = "UserUMMailboxPolicyGuid")]
		[XmlElement("UserUMMailboxPolicyGuid")]
		public Guid UserUMMailboxPolicyGuid { get; set; }

		// Token: 0x060021B1 RID: 8625 RVA: 0x000A2868 File Offset: 0x000A0A68
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new SaveUMPin(callContext, this);
		}

		// Token: 0x060021B2 RID: 8626 RVA: 0x000A2871 File Offset: 0x000A0A71
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x060021B3 RID: 8627 RVA: 0x000A2874 File Offset: 0x000A0A74
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}
	}
}
