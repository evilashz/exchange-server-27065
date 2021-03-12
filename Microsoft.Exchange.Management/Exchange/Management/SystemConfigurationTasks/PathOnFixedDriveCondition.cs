using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.Common;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000932 RID: 2354
	[Serializable]
	internal sealed class PathOnFixedDriveCondition : PathOnServerCondition
	{
		// Token: 0x060053E8 RID: 21480 RVA: 0x0015AE11 File Offset: 0x00159011
		public PathOnFixedDriveCondition(string computerName, string pathName) : base(computerName, pathName)
		{
		}

		// Token: 0x060053E9 RID: 21481 RVA: 0x0015AE1C File Offset: 0x0015901C
		public override bool Verify()
		{
			bool flag = WmiWrapper.IsPathOnFixedDrive(base.ComputerName, base.PathName);
			TaskLogger.Trace("PathOnFixedDriveCondition.Verify() returns {0}: <Server '{1}', PathName '{2}'>", new object[]
			{
				flag,
				base.ComputerName,
				base.PathName
			});
			return flag;
		}
	}
}
