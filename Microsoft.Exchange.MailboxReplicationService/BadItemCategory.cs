using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000065 RID: 101
	internal abstract class BadItemCategory
	{
		// Token: 0x06000501 RID: 1281 RVA: 0x0001E210 File Offset: 0x0001C410
		public BadItemCategory(string name, string configSettingName)
		{
			this.Name = name;
			this.ConfigSettingName = configSettingName;
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x0001E226 File Offset: 0x0001C426
		// (set) Token: 0x06000503 RID: 1283 RVA: 0x0001E22E File Offset: 0x0001C42E
		public string Name { get; private set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x0001E237 File Offset: 0x0001C437
		// (set) Token: 0x06000505 RID: 1285 RVA: 0x0001E23F File Offset: 0x0001C43F
		private protected string ConfigSettingName { protected get; private set; }

		// Token: 0x06000506 RID: 1286 RVA: 0x0001E248 File Offset: 0x0001C448
		public virtual int GetLimit()
		{
			return ConfigBase<MRSConfigSchema>.GetConfig<int>(this.ConfigSettingName);
		}

		// Token: 0x06000507 RID: 1287
		public abstract bool IsMatch(BadMessageRec message, TestIntegration testIntegration);
	}
}
