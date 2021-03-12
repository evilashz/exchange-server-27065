using System;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000930 RID: 2352
	[Serializable]
	internal abstract class PathOnServerCondition : PathCondition
	{
		// Token: 0x060053E3 RID: 21475 RVA: 0x0015AD97 File Offset: 0x00158F97
		protected PathOnServerCondition(string computerName, string pathName) : base(pathName)
		{
			this.ComputerName = computerName;
		}

		// Token: 0x170018FF RID: 6399
		// (get) Token: 0x060053E4 RID: 21476 RVA: 0x0015ADA7 File Offset: 0x00158FA7
		// (set) Token: 0x060053E5 RID: 21477 RVA: 0x0015ADAF File Offset: 0x00158FAF
		protected string ComputerName
		{
			get
			{
				return this.computerName;
			}
			set
			{
				this.computerName = value;
			}
		}

		// Token: 0x04003106 RID: 12550
		private string computerName;
	}
}
