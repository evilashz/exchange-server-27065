using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000655 RID: 1621
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Name = "SuppressReadReceipt", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class SuppressReadReceiptType : ReferenceItemResponseType
	{
	}
}
