using System;
using System.Configuration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Hygiene.Data.DataProvider
{
	// Token: 0x020000B6 RID: 182
	internal class EnvironmentStrategy
	{
		// Token: 0x06000605 RID: 1541 RVA: 0x00014043 File Offset: 0x00012243
		public virtual bool IsForefrontForOffice()
		{
			return DatacenterRegistry.IsForefrontForOffice();
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0001404A File Offset: 0x0001224A
		public virtual bool IsForefrontDALOverrideUseSQL()
		{
			return Convert.ToBoolean(ConfigurationManager.AppSettings["DALUseLocalDB"]);
		}
	}
}
