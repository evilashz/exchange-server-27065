using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200040B RID: 1035
	[XmlType("CreateUMCallDataRecordType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class CreateUMCallDataRecordRequest : BaseRequest
	{
		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06001D8E RID: 7566 RVA: 0x0009F4AA File Offset: 0x0009D6AA
		// (set) Token: 0x06001D8F RID: 7567 RVA: 0x0009F4B2 File Offset: 0x0009D6B2
		[XmlElement("CDRData")]
		[DataMember(Name = "CDRData")]
		public CDRData CDRData { get; set; }

		// Token: 0x06001D90 RID: 7568 RVA: 0x0009F4BB File Offset: 0x0009D6BB
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new CreateUMCallDataRecord(callContext, this);
		}

		// Token: 0x06001D91 RID: 7569 RVA: 0x0009F4C4 File Offset: 0x0009D6C4
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x06001D92 RID: 7570 RVA: 0x0009F4C7 File Offset: 0x0009D6C7
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}
	}
}
