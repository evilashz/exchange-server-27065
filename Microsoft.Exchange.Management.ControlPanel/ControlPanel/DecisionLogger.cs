using System;
using System.Collections;
using System.IO;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000378 RID: 888
	internal class DecisionLogger : IEnumerable
	{
		// Token: 0x0600303E RID: 12350 RVA: 0x000932DC File Offset: 0x000914DC
		public DecisionLogger(TextWriter decisionLog)
		{
			this.decisionLog = decisionLog;
			this.Decision = true;
		}

		// Token: 0x0600303F RID: 12351 RVA: 0x000932F2 File Offset: 0x000914F2
		public void Add(bool precondition, LocalizedString failureLog)
		{
			this.Decision = (this.Decision && precondition);
			if (!precondition)
			{
				this.decisionLog.Write('\t');
				this.decisionLog.WriteLine(failureLog);
			}
		}

		// Token: 0x17001F2C RID: 7980
		// (get) Token: 0x06003040 RID: 12352 RVA: 0x00093323 File Offset: 0x00091523
		// (set) Token: 0x06003041 RID: 12353 RVA: 0x0009332B File Offset: 0x0009152B
		public bool Decision { get; private set; }

		// Token: 0x06003042 RID: 12354 RVA: 0x00093334 File Offset: 0x00091534
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04002357 RID: 9047
		private TextWriter decisionLog;
	}
}
