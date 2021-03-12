using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000015 RID: 21
	[XmlInclude(typeof(LogComparisonCondition))]
	[XmlInclude(typeof(LogBinaryStringOperatorCondition))]
	public abstract class LogBinaryOperatorCondition : LogCondition
	{
		// Token: 0x04000020 RID: 32
		public LogConditionOperand Left;

		// Token: 0x04000021 RID: 33
		public LogConditionOperand Right;
	}
}
