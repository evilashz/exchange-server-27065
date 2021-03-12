using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000018 RID: 24
	[XmlType("StringCompare")]
	public class LogStringComparisonCondition : LogComparisonCondition
	{
		// Token: 0x0400002A RID: 42
		public bool IgnoreCase;
	}
}
