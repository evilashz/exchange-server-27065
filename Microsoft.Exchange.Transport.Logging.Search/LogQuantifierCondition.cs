using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000012 RID: 18
	[XmlInclude(typeof(LogForEveryCondition))]
	[XmlInclude(typeof(LogForAnyCondition))]
	public abstract class LogQuantifierCondition : LogUnaryCondition
	{
		// Token: 0x0400001E RID: 30
		public LogConditionField Field;

		// Token: 0x0400001F RID: 31
		public LogConditionVariable Variable;
	}
}
