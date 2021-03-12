using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200042C RID: 1068
	[XmlType("GetClientIntentType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public sealed class GetClientIntentRequest : BaseRequest
	{
		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06001F4D RID: 8013 RVA: 0x000A0B17 File Offset: 0x0009ED17
		// (set) Token: 0x06001F4E RID: 8014 RVA: 0x000A0B1F File Offset: 0x0009ED1F
		[XmlElement]
		[DataMember(Name = "GlobalObjectId", IsRequired = true)]
		public string GlobalObjectId { get; set; }

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06001F4F RID: 8015 RVA: 0x000A0B28 File Offset: 0x0009ED28
		// (set) Token: 0x06001F50 RID: 8016 RVA: 0x000A0B30 File Offset: 0x0009ED30
		[XmlArrayItem("DeleteFromFolderStateDefinition", typeof(DeleteFromFolderStateDefinition), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArray("StateDefinition")]
		[XmlArrayItem("LocationBasedStateDefinition", typeof(LocationBasedStateDefinition), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[DataMember(Name = "StateDefinition", IsRequired = true)]
		[XmlArrayItem("DeletedOccurrenceStateDefinition", typeof(DeletedOccurrenceStateDefinition), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public BaseCalendarItemStateDefinition[] StateDefinition { get; set; }

		// Token: 0x06001F51 RID: 8017 RVA: 0x000A0B39 File Offset: 0x0009ED39
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetClientIntent(callContext, this);
		}

		// Token: 0x06001F52 RID: 8018 RVA: 0x000A0B42 File Offset: 0x0009ED42
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x06001F53 RID: 8019 RVA: 0x000A0B45 File Offset: 0x0009ED45
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(false, callContext);
		}
	}
}
