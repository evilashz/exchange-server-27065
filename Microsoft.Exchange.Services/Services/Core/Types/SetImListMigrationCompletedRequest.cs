using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200047F RID: 1151
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("SetImListMigrationCompletedRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class SetImListMigrationCompletedRequest : BaseRequest
	{
		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x0600222A RID: 8746 RVA: 0x000A2CE1 File Offset: 0x000A0EE1
		// (set) Token: 0x0600222B RID: 8747 RVA: 0x000A2CE9 File Offset: 0x000A0EE9
		[DataMember(Name = "ImListMigrationCompleted", IsRequired = true, Order = 1)]
		[XmlElement(ElementName = "ImListMigrationCompleted")]
		public bool ImListMigrationCompleted { get; set; }

		// Token: 0x0600222C RID: 8748 RVA: 0x000A2CF2 File Offset: 0x000A0EF2
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new SetImListMigrationCompleted(callContext, this);
		}

		// Token: 0x0600222D RID: 8749 RVA: 0x000A2CFB File Offset: 0x000A0EFB
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return IdConverter.GetServerInfoForCallContext(callContext);
		}

		// Token: 0x0600222E RID: 8750 RVA: 0x000A2D03 File Offset: 0x000A0F03
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(true, callContext);
		}
	}
}
