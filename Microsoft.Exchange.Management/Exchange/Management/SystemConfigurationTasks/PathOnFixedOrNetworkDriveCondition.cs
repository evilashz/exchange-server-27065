using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.Common;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000931 RID: 2353
	[Serializable]
	internal sealed class PathOnFixedOrNetworkDriveCondition : PathOnServerCondition
	{
		// Token: 0x060053E6 RID: 21478 RVA: 0x0015ADB8 File Offset: 0x00158FB8
		public PathOnFixedOrNetworkDriveCondition(string computerName, string pathName) : base(computerName, pathName)
		{
		}

		// Token: 0x060053E7 RID: 21479 RVA: 0x0015ADC4 File Offset: 0x00158FC4
		public override bool Verify()
		{
			bool flag = WmiWrapper.IsPathOnFixedOrNetworkDrive(base.ComputerName, base.PathName);
			TaskLogger.Trace("PathOnFixedOrNetworkDriveCondition.Verify() returns {0}: <Server '{1}', PathName '{2}'>", new object[]
			{
				flag,
				base.ComputerName,
				base.PathName
			});
			return flag;
		}
	}
}
