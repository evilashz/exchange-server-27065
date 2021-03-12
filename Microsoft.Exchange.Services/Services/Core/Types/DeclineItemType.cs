using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200065A RID: 1626
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Name = "DeclineItem", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class DeclineItemType : MeetingRegistrationResponseObjectType
	{
	}
}
