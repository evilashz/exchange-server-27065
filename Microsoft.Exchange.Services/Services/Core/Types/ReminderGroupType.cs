using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000539 RID: 1337
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum ReminderGroupType
	{
		// Token: 0x040015E0 RID: 5600
		Calendar,
		// Token: 0x040015E1 RID: 5601
		Task
	}
}
