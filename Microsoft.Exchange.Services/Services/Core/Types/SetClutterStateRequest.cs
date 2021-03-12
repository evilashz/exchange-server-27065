using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200047B RID: 1147
	[XmlType(TypeName = "SetClutterStateRequestMessage", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class SetClutterStateRequest : BaseRequest
	{
		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x060021F6 RID: 8694 RVA: 0x000A2B2A File Offset: 0x000A0D2A
		// (set) Token: 0x060021F7 RID: 8695 RVA: 0x000A2B32 File Offset: 0x000A0D32
		[DataMember]
		[XmlElement(ElementName = "Command")]
		public SetClutterStateCommand Command { get; set; }

		// Token: 0x060021F8 RID: 8696 RVA: 0x000A2B3B File Offset: 0x000A0D3B
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new SetClutterState(callContext, this);
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x000A2B44 File Offset: 0x000A0D44
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x000A2B47 File Offset: 0x000A0D47
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(false, callContext);
		}
	}
}
