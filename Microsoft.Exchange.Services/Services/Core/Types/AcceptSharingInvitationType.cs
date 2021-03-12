using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000654 RID: 1620
	[DataContract(Name = "AcceptSharingInvitation", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class AcceptSharingInvitationType : ReferenceItemResponseType
	{
	}
}
