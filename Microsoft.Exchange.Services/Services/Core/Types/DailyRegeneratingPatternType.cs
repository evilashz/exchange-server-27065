using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000628 RID: 1576
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "DailyRegeneration")]
	[Serializable]
	public class DailyRegeneratingPatternType : RegeneratingPatternBaseType
	{
	}
}
