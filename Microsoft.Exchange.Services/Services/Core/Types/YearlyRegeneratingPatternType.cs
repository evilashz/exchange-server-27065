using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000625 RID: 1573
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "YearlyRegeneration")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class YearlyRegeneratingPatternType : RegeneratingPatternBaseType
	{
	}
}
