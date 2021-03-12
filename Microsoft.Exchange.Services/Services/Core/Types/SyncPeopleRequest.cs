using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200048F RID: 1167
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "SyncPeopleType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class SyncPeopleRequest : SyncPersonaContactsRequestBase
	{
		// Token: 0x060022DB RID: 8923 RVA: 0x000A35D4 File Offset: 0x000A17D4
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new SyncPeople(callContext, this);
		}
	}
}
