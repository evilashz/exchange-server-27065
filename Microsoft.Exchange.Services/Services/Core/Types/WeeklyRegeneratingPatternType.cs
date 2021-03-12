using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000627 RID: 1575
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "WeeklyRegeneration")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class WeeklyRegeneratingPatternType : RegeneratingPatternBaseType
	{
	}
}
