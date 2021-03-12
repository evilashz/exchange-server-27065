using System;
using System.Globalization;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Common;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B0E RID: 2830
	[Serializable]
	internal sealed class LogLocationAvailableCondition : PathOnServerCondition
	{
		// Token: 0x060064AF RID: 25775 RVA: 0x001A4574 File Offset: 0x001A2774
		public LogLocationAvailableCondition(string computerName, string pathName, string logPrefix) : base(computerName, pathName)
		{
			this.logPrefix = logPrefix;
		}

		// Token: 0x060064B0 RID: 25776 RVA: 0x001A4588 File Offset: 0x001A2788
		public override bool Verify()
		{
			string dirName = null;
			try
			{
				dirName = LocalLongFullPath.ParseFromPathNameAndFileName(base.PathName, this.logPrefix + ".log").PathName;
			}
			catch (ArgumentException ex)
			{
				TaskLogger.Trace("LogLocationAvailableCondition.Verify() caught exception '{0}': <Server '{1}', PathName '{2}'>", new object[]
				{
					ex.Message,
					base.ComputerName,
					base.PathName
				});
				return false;
			}
			bool flag = true;
			if (WmiWrapper.IsDirectoryExisting(base.ComputerName, dirName))
			{
				flag = false;
			}
			else if (WmiWrapper.IsFileExistingInPath(base.ComputerName, base.PathName, new WmiWrapper.FileFilter(this.LogFileFilter)))
			{
				flag = false;
			}
			else if (WmiWrapper.IsFileExisting(base.ComputerName, base.PathName))
			{
				flag = false;
			}
			TaskLogger.Trace("LogLocationAvailableCondition.Verify() returns {0}: <Server '{1}', PathName '{2}'>", new object[]
			{
				flag,
				base.ComputerName,
				base.PathName
			});
			return flag;
		}

		// Token: 0x060064B1 RID: 25777 RVA: 0x001A4688 File Offset: 0x001A2888
		private bool LogFileFilter(string name, string ext)
		{
			return name.StartsWith(this.logPrefix, true, CultureInfo.InvariantCulture) && 0 == string.Compare(ext, "log", true, CultureInfo.InvariantCulture);
		}

		// Token: 0x0400362F RID: 13871
		private const string logFileExtensionWithoutPeriod = "log";

		// Token: 0x04003630 RID: 13872
		private readonly string logPrefix;
	}
}
