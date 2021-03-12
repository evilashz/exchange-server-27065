using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020003E3 RID: 995
	[DataContract(Name = "RetentionType", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "RetentionType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum RetentionType
	{
		// Token: 0x04001265 RID: 4709
		Delete,
		// Token: 0x04001266 RID: 4710
		Archive
	}
}
