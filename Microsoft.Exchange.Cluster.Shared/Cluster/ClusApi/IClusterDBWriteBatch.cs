using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x0200001F RID: 31
	public interface IClusterDBWriteBatch : IDisposable
	{
		// Token: 0x06000127 RID: 295
		void CreateOrOpenKey(string keyName);

		// Token: 0x06000128 RID: 296
		void DeleteKey(string keyName);

		// Token: 0x06000129 RID: 297
		void SetValue(string valueName, string value);

		// Token: 0x0600012A RID: 298
		void SetValue(string valueName, IEnumerable<string> value);

		// Token: 0x0600012B RID: 299
		void SetValue(string valueName, int value);

		// Token: 0x0600012C RID: 300
		void SetValue(string valueName, long value);

		// Token: 0x0600012D RID: 301
		void DeleteValue(string valueName);

		// Token: 0x0600012E RID: 302
		void Execute();
	}
}
