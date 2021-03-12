using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200065D RID: 1629
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Name = "RemoveItem", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class RemoveItemType : ResponseObjectType
	{
	}
}
