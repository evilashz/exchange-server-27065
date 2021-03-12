using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Hygiene.Data.DataProvider;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x020001C7 RID: 455
	internal class MobileDeviceSession
	{
		// Token: 0x0600132B RID: 4907 RVA: 0x00039F1E File Offset: 0x0003811E
		public MobileDeviceSession()
		{
			this.dataProvider = ConfigDataProviderFactory.Default.Create(DatabaseType.Mtrt);
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x00039F37 File Offset: 0x00038137
		public IEnumerable<T> GetDashboardSummary<T>(QueryFilter filter) where T : DeviceSnapshot, new()
		{
			return this.dataProvider.Find<T>(filter, null, false, null).Cast<T>();
		}

		// Token: 0x04000933 RID: 2355
		private readonly IConfigDataProvider dataProvider;
	}
}
