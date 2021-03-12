using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000E6 RID: 230
	internal sealed class ASTraceConfiguration
	{
		// Token: 0x060005FB RID: 1531 RVA: 0x0001A15C File Offset: 0x0001835C
		private ASTraceConfiguration()
		{
			this.exTraceConfiguration = ExTraceConfiguration.Instance;
			this.exTraceConfigVersion = this.exTraceConfiguration.Version;
			this.filteredMailboxes = ASTraceConfiguration.GetStringFilterList(this.exTraceConfiguration, "UserDN");
			this.filteredRequesters = ASTraceConfiguration.GetStringFilterList(this.exTraceConfiguration, "WindowsIdentity");
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x0001A1B8 File Offset: 0x000183B8
		public static ASTraceConfiguration Instance
		{
			get
			{
				if (ASTraceConfiguration.instance.exTraceConfigVersion != ExTraceConfiguration.Instance.Version)
				{
					lock (ASTraceConfiguration.staticSyncObject)
					{
						if (ASTraceConfiguration.instance.exTraceConfigVersion != ExTraceConfiguration.Instance.Version)
						{
							ASTraceConfiguration.instance = new ASTraceConfiguration();
						}
					}
				}
				return ASTraceConfiguration.instance;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x0001A22C File Offset: 0x0001842C
		public List<string> FilteredMailboxes
		{
			get
			{
				return this.filteredMailboxes;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x0001A234 File Offset: 0x00018434
		public List<string> FilteredRequesters
		{
			get
			{
				return this.filteredRequesters;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x0001A23C File Offset: 0x0001843C
		public bool FilteredTracingEnabled
		{
			get
			{
				return this.exTraceConfiguration.PerThreadTracingConfigured;
			}
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0001A24C File Offset: 0x0001844C
		private static List<string> GetStringFilterList(ExTraceConfiguration configuration, string filterKey)
		{
			List<string> result;
			if (!configuration.CustomParameters.TryGetValue(filterKey, out result))
			{
				return new List<string>();
			}
			return result;
		}

		// Token: 0x04000371 RID: 881
		public const string MailboxDNFilterKey = "UserDN";

		// Token: 0x04000372 RID: 882
		public const string RequesterWindowsIndentityFilterKey = "WindowsIdentity";

		// Token: 0x04000373 RID: 883
		private static object staticSyncObject = new object();

		// Token: 0x04000374 RID: 884
		private static ASTraceConfiguration instance = new ASTraceConfiguration();

		// Token: 0x04000375 RID: 885
		private ExTraceConfiguration exTraceConfiguration;

		// Token: 0x04000376 RID: 886
		private int exTraceConfigVersion;

		// Token: 0x04000377 RID: 887
		private List<string> filteredMailboxes;

		// Token: 0x04000378 RID: 888
		private List<string> filteredRequesters;
	}
}
