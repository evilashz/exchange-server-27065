using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000019 RID: 25
	[XmlInclude(typeof(LogStringEndsWithCondition))]
	[XmlInclude(typeof(LogStringContainsCondition))]
	[XmlInclude(typeof(LogStringStartsWithCondition))]
	public abstract class LogBinaryStringOperatorCondition : LogBinaryOperatorCondition
	{
		// Token: 0x0400002B RID: 43
		public bool IgnoreCase;
	}
}
