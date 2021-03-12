using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x0200000C RID: 12
	[XmlInclude(typeof(LogQuantifierCondition))]
	[XmlInclude(typeof(LogNotCondition))]
	public abstract class LogUnaryCondition : LogCondition
	{
		// Token: 0x0400001A RID: 26
		public LogCondition Condition;
	}
}
