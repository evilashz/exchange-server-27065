using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200018C RID: 396
	internal abstract class StatementPromptConfig : PromptConfigBase
	{
		// Token: 0x06000BB9 RID: 3001 RVA: 0x00032BD8 File Offset: 0x00030DD8
		internal StatementPromptConfig(List<PromptConfigBase> parameterPrompts, string name, string type, string conditionString, ActivityManagerConfig managerConfig) : base(name, type, conditionString, managerConfig)
		{
			this.parameterPrompts = (parameterPrompts ?? new List<PromptConfigBase>());
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000BBA RID: 3002 RVA: 0x00032BF6 File Offset: 0x00030DF6
		// (set) Token: 0x06000BBB RID: 3003 RVA: 0x00032BFE File Offset: 0x00030DFE
		protected List<PromptConfigBase> ParameterPrompts
		{
			get
			{
				return this.parameterPrompts;
			}
			set
			{
				this.parameterPrompts = value;
			}
		}

		// Token: 0x040009DF RID: 2527
		private List<PromptConfigBase> parameterPrompts;
	}
}
