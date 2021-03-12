using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200092F RID: 2351
	[Serializable]
	internal abstract class PathCondition : Condition
	{
		// Token: 0x060053E0 RID: 21472 RVA: 0x0015AD77 File Offset: 0x00158F77
		protected PathCondition(string pathName)
		{
			this.PathName = pathName;
		}

		// Token: 0x170018FE RID: 6398
		// (get) Token: 0x060053E1 RID: 21473 RVA: 0x0015AD86 File Offset: 0x00158F86
		// (set) Token: 0x060053E2 RID: 21474 RVA: 0x0015AD8E File Offset: 0x00158F8E
		protected string PathName
		{
			get
			{
				return this.pathName;
			}
			set
			{
				this.pathName = value;
			}
		}

		// Token: 0x04003105 RID: 12549
		private string pathName;
	}
}
