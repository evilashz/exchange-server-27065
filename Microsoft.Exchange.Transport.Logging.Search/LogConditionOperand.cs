using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x0200000E RID: 14
	[XmlInclude(typeof(LogConditionField))]
	[XmlInclude(typeof(LogConditionVariable))]
	[XmlInclude(typeof(LogConditionConstant))]
	public abstract class LogConditionOperand
	{
	}
}
