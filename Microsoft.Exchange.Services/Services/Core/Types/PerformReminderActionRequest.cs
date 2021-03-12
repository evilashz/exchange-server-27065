using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000468 RID: 1128
	[XmlType(TypeName = "PerformReminderActionType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class PerformReminderActionRequest : BaseRequest
	{
		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06002138 RID: 8504 RVA: 0x000A23DE File Offset: 0x000A05DE
		// (set) Token: 0x06002139 RID: 8505 RVA: 0x000A23E6 File Offset: 0x000A05E6
		[DataMember]
		[XmlArray("ReminderItemActions", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 1)]
		[XmlArrayItem("ReminderItemAction", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(ReminderItemActionType))]
		public ReminderItemActionType[] ReminderItemActions { get; set; }

		// Token: 0x0600213A RID: 8506 RVA: 0x000A23EF File Offset: 0x000A05EF
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new PerformReminderAction(callContext, this);
		}

		// Token: 0x0600213B RID: 8507 RVA: 0x000A23F8 File Offset: 0x000A05F8
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x0600213C RID: 8508 RVA: 0x000A23FB File Offset: 0x000A05FB
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}
	}
}
