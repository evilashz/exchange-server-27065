using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200062C RID: 1580
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "NoEndRecurrence")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class NoEndRecurrenceRangeType : RecurrenceRangeBaseType
	{
	}
}
