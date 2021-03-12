using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000006 RID: 6
	[XmlInclude(typeof(LogCompoundCondition))]
	[XmlInclude(typeof(LogBinaryOperatorCondition))]
	[XmlInclude(typeof(LogUnaryOperatorCondition))]
	[XmlInclude(typeof(LogUnaryCondition))]
	[XmlInclude(typeof(LogTrueCondition))]
	[XmlInclude(typeof(LogFalseCondition))]
	public abstract class LogCondition
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000022DC File Offset: 0x000004DC
		public static LogTrueCondition True
		{
			get
			{
				return LogCondition.trueCondition;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000022E3 File Offset: 0x000004E3
		public static LogFalseCondition False
		{
			get
			{
				return LogCondition.falseCondition;
			}
		}

		// Token: 0x04000017 RID: 23
		private static readonly LogTrueCondition trueCondition = new LogTrueCondition();

		// Token: 0x04000018 RID: 24
		private static readonly LogFalseCondition falseCondition = new LogFalseCondition();
	}
}
