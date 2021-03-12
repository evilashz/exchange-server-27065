using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.Common;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000936 RID: 2358
	[Serializable]
	internal sealed class InformationStoreServiceRunningCondition : Condition
	{
		// Token: 0x060053F1 RID: 21489 RVA: 0x0015AF40 File Offset: 0x00159140
		public InformationStoreServiceRunningCondition(string computerName)
		{
			this.computerName = computerName;
		}

		// Token: 0x060053F2 RID: 21490 RVA: 0x0015AF50 File Offset: 0x00159150
		public override bool Verify()
		{
			bool flag = WmiWrapper.IsServiceRunning(this.computerName, "MSExchangeIS");
			TaskLogger.Trace("InformationStoreServiceRunningCondition.Verify() returns {0}: <Server '{1}'>", new object[]
			{
				flag,
				this.computerName
			});
			return flag;
		}

		// Token: 0x04003108 RID: 12552
		private readonly string computerName;
	}
}
