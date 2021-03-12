using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.Common;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000934 RID: 2356
	[Serializable]
	internal sealed class DirectoryNotExistCondition : PathOnServerCondition
	{
		// Token: 0x060053EC RID: 21484 RVA: 0x0015AEC4 File Offset: 0x001590C4
		public DirectoryNotExistCondition(string computerName, string directoryName) : base(computerName, directoryName)
		{
		}

		// Token: 0x060053ED RID: 21485 RVA: 0x0015AED0 File Offset: 0x001590D0
		public override bool Verify()
		{
			bool flag = !WmiWrapper.IsDirectoryExisting(base.ComputerName, base.PathName);
			TaskLogger.Trace("DirectoryNotExistCondition.Verify() returns {0}: <Server '{1}', DirectoryName '{2}'>", new object[]
			{
				flag,
				base.ComputerName,
				base.PathName
			});
			return flag;
		}
	}
}
