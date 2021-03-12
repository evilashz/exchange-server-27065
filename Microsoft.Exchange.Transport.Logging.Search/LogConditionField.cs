using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000011 RID: 17
	[XmlType("Field")]
	public class LogConditionField : LogConditionOperand
	{
		// Token: 0x0400001D RID: 29
		public string Name;
	}
}
