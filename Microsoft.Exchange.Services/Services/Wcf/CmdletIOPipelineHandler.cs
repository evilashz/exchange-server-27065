using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000984 RID: 2436
	internal class CmdletIOPipelineHandler : TaskIOPipelineBase
	{
		// Token: 0x17000FAF RID: 4015
		// (get) Token: 0x060045B5 RID: 17845 RVA: 0x000F4CD2 File Offset: 0x000F2ED2
		// (set) Token: 0x060045B6 RID: 17846 RVA: 0x000F4CDA File Offset: 0x000F2EDA
		public string UserPrompt { get; private set; }

		// Token: 0x060045B7 RID: 17847 RVA: 0x000F4CE3 File Offset: 0x000F2EE3
		public override bool ShouldContinue(string query, string caption, ref bool yesToAll, ref bool noToAll, out bool? output)
		{
			output = new bool?(yesToAll);
			if (!yesToAll && !noToAll)
			{
				this.UserPrompt = query;
			}
			return false;
		}
	}
}
