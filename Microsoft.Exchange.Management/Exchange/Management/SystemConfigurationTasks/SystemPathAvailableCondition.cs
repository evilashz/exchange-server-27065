using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Common;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B0F RID: 2831
	[Serializable]
	internal sealed class SystemPathAvailableCondition : PathOnServerCondition
	{
		// Token: 0x060064B2 RID: 25778 RVA: 0x001A46B4 File Offset: 0x001A28B4
		public SystemPathAvailableCondition(string computerName, string pathName) : base(computerName, pathName)
		{
		}

		// Token: 0x060064B3 RID: 25779 RVA: 0x001A46C0 File Offset: 0x001A28C0
		public override bool Verify()
		{
			string text = null;
			try
			{
				text = LocalLongFullPath.ParseFromPathNameAndFileName(base.PathName, "tmp.edb").PathName;
			}
			catch (ArgumentException ex)
			{
				TaskLogger.Trace("SystemPathAvailableCondition.Verify() caught exception '{0}': <Server '{1}', PathName '{2}'>", new object[]
				{
					ex.Message,
					base.ComputerName,
					base.PathName
				});
				return false;
			}
			bool flag = true;
			if (WmiWrapper.IsDirectoryExisting(base.ComputerName, text))
			{
				flag = false;
			}
			else if (WmiWrapper.IsFileExisting(base.ComputerName, text))
			{
				flag = false;
			}
			else if (WmiWrapper.IsFileExisting(base.ComputerName, base.PathName))
			{
				flag = false;
			}
			TaskLogger.Trace("SystemPathAvailableCondition.Verify() returns {0}: <Server '{1}', PathName '{2}'>", new object[]
			{
				flag,
				base.ComputerName,
				base.PathName
			});
			return flag;
		}

		// Token: 0x04003631 RID: 13873
		private const string temporarySystemFileName = "tmp.edb";
	}
}
