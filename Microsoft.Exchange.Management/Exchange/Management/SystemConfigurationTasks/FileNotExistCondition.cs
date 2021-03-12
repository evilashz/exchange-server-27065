using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.Common;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000933 RID: 2355
	[Serializable]
	internal sealed class FileNotExistCondition : PathOnServerCondition
	{
		// Token: 0x060053EA RID: 21482 RVA: 0x0015AE69 File Offset: 0x00159069
		public FileNotExistCondition(string computerName, string fileName) : base(computerName, fileName)
		{
		}

		// Token: 0x060053EB RID: 21483 RVA: 0x0015AE74 File Offset: 0x00159074
		public override bool Verify()
		{
			bool flag = !WmiWrapper.IsFileExisting(base.ComputerName, base.PathName);
			TaskLogger.Trace("FileNotExistCondition.Verify() returns {0}: <Server '{1}', FileName '{2}'>", new object[]
			{
				flag,
				base.ComputerName,
				base.PathName
			});
			return flag;
		}
	}
}
