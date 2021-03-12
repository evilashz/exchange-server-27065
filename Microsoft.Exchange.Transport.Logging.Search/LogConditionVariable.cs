using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x0200000F RID: 15
	[XmlType("Variable")]
	public class LogConditionVariable : LogConditionOperand
	{
		// Token: 0x0400001B RID: 27
		public string Name;
	}
}
