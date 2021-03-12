using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000009 RID: 9
	[XmlInclude(typeof(LogAndCondition))]
	[XmlInclude(typeof(LogOrCondition))]
	public abstract class LogCompoundCondition : LogCondition
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002318 File Offset: 0x00000518
		[XmlArrayItem(ElementName = "Condition")]
		public List<LogCondition> Conditions
		{
			get
			{
				return this.conditions;
			}
		}

		// Token: 0x04000019 RID: 25
		private readonly List<LogCondition> conditions = new List<LogCondition>();
	}
}
