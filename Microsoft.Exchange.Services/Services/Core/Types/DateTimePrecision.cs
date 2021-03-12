using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000DBF RID: 3519
	[XmlRoot(ElementName = "DateTimePrecision", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlType(TypeName = "DateTimePrecisionType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum DateTimePrecision
	{
		// Token: 0x04003191 RID: 12689
		Seconds,
		// Token: 0x04003192 RID: 12690
		Milliseconds
	}
}
