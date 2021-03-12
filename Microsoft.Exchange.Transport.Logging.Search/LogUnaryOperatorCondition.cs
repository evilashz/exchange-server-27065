using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x0200001D RID: 29
	[XmlInclude(typeof(LogIsNullOrEmptyCondition))]
	public abstract class LogUnaryOperatorCondition : LogCondition
	{
		// Token: 0x0400002C RID: 44
		public LogConditionOperand Operand;
	}
}
