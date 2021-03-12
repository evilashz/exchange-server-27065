using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000017 RID: 23
	[XmlInclude(typeof(LogStringComparisonCondition))]
	[XmlType("Compare")]
	public class LogComparisonCondition : LogBinaryOperatorCondition
	{
		// Token: 0x04000029 RID: 41
		public LogComparisonOperator Operator;
	}
}
