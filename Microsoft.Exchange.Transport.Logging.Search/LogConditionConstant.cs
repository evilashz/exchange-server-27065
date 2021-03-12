using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000010 RID: 16
	[XmlType("Constant")]
	public class LogConditionConstant : LogConditionOperand
	{
		// Token: 0x0400001C RID: 28
		public object Value;
	}
}
