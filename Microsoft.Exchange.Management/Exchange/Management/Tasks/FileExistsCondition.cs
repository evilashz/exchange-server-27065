using System;
using System.IO;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020000EA RID: 234
	[Serializable]
	internal class FileExistsCondition : Condition
	{
		// Token: 0x060006F1 RID: 1777 RVA: 0x0001CBF4 File Offset: 0x0001ADF4
		public FileExistsCondition(string fileName)
		{
			this.fileName = fileName;
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0001CC04 File Offset: 0x0001AE04
		public override bool Verify()
		{
			TaskLogger.LogEnter();
			if (this.FileName == null || string.Empty == this.FileName)
			{
				throw new ConditionInitializationException("FileName", this);
			}
			bool result = File.Exists(this.FileName);
			TaskLogger.LogExit();
			return result;
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x0001CC4E File Offset: 0x0001AE4E
		// (set) Token: 0x060006F4 RID: 1780 RVA: 0x0001CC56 File Offset: 0x0001AE56
		public string FileName
		{
			get
			{
				return this.fileName;
			}
			set
			{
				this.fileName = value;
			}
		}

		// Token: 0x0400034C RID: 844
		private string fileName;
	}
}
