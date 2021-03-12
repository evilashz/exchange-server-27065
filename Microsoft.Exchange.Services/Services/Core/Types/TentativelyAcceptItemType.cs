using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200065F RID: 1631
	[DataContract(Name = "TentativelyAcceptItem", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class TentativelyAcceptItemType : MeetingRegistrationResponseObjectType
	{
	}
}
