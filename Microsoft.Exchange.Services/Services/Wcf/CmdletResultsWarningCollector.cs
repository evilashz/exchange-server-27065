using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000985 RID: 2437
	internal class CmdletResultsWarningCollector : TaskIOPipelineBase
	{
		// Token: 0x060045B9 RID: 17849 RVA: 0x000F4D0C File Offset: 0x000F2F0C
		public override bool WriteWarning(LocalizedString input, string helperUrl, out LocalizedString output)
		{
			output = input;
			if (this.capturedWarnings == null)
			{
				this.capturedWarnings = new Dictionary<int, List<string>>();
			}
			if (!this.capturedWarnings.ContainsKey(this.currentResultIndex))
			{
				this.capturedWarnings[this.currentResultIndex] = new List<string>();
			}
			this.capturedWarnings[this.currentResultIndex].Add(input);
			return true;
		}

		// Token: 0x060045BA RID: 17850 RVA: 0x000F4D79 File Offset: 0x000F2F79
		public override bool WriteObject(object input, out object output)
		{
			this.currentResultIndex++;
			return base.WriteObject(input, out output);
		}

		// Token: 0x060045BB RID: 17851 RVA: 0x000F4D91 File Offset: 0x000F2F91
		internal string[] GetWarningMessagesForResult(int resultIndex)
		{
			if (this.capturedWarnings != null && this.capturedWarnings.ContainsKey(resultIndex))
			{
				return this.capturedWarnings[resultIndex].ToArray();
			}
			return null;
		}

		// Token: 0x04002877 RID: 10359
		private int currentResultIndex;

		// Token: 0x04002878 RID: 10360
		private Dictionary<int, List<string>> capturedWarnings;
	}
}
