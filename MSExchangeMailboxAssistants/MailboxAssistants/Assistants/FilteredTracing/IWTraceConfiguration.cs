using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.FilteredTracing
{
	// Token: 0x02000009 RID: 9
	internal class IWTraceConfiguration
	{
		// Token: 0x06000050 RID: 80 RVA: 0x00003994 File Offset: 0x00001B94
		private IWTraceConfiguration()
		{
			this.exTraceConfiguration = ExTraceConfiguration.Instance;
			this.exTraceConfigVersion = this.exTraceConfiguration.Version;
			this.filteredMailboxs = IWTraceConfiguration.GetGuidFilterList(this.exTraceConfiguration, "MailboxGuid");
			this.filteredMDBs = IWTraceConfiguration.GetGuidFilterList(this.exTraceConfiguration, "MailboxDatabaseGuid");
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000051 RID: 81 RVA: 0x000039F0 File Offset: 0x00001BF0
		public static IWTraceConfiguration Instance
		{
			get
			{
				if (IWTraceConfiguration.instance.exTraceConfigVersion != ExTraceConfiguration.Instance.Version)
				{
					lock (IWTraceConfiguration.staticSyncObject)
					{
						if (IWTraceConfiguration.instance.exTraceConfigVersion != ExTraceConfiguration.Instance.Version)
						{
							IWTraceConfiguration.instance = new IWTraceConfiguration();
						}
					}
				}
				return IWTraceConfiguration.instance;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00003A64 File Offset: 0x00001C64
		public List<Guid> FilteredMailboxs
		{
			get
			{
				return this.filteredMailboxs;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00003A6C File Offset: 0x00001C6C
		public List<Guid> FilteredMDBs
		{
			get
			{
				return this.filteredMDBs;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00003A74 File Offset: 0x00001C74
		public bool FilteredTracingEnabled
		{
			get
			{
				return this.exTraceConfiguration.PerThreadTracingConfigured;
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003A84 File Offset: 0x00001C84
		private static List<Guid> GetGuidFilterList(ExTraceConfiguration configuration, string filterKey)
		{
			List<Guid> list = new List<Guid>();
			List<string> list2;
			if (configuration.CustomParameters.TryGetValue(filterKey, out list2))
			{
				foreach (string g in list2)
				{
					Guid item;
					if (GuidHelper.TryParseGuid(g, out item))
					{
						list.Add(item);
					}
				}
			}
			return list;
		}

		// Token: 0x04000048 RID: 72
		public const string MailboxGuidFilterKey = "MailboxGuid";

		// Token: 0x04000049 RID: 73
		public const string MailboxDatabaseGuidFilterKey = "MailboxDatabaseGuid";

		// Token: 0x0400004A RID: 74
		private static object staticSyncObject = new object();

		// Token: 0x0400004B RID: 75
		private static IWTraceConfiguration instance = new IWTraceConfiguration();

		// Token: 0x0400004C RID: 76
		private ExTraceConfiguration exTraceConfiguration;

		// Token: 0x0400004D RID: 77
		private int exTraceConfigVersion;

		// Token: 0x0400004E RID: 78
		private List<Guid> filteredMDBs;

		// Token: 0x0400004F RID: 79
		private List<Guid> filteredMailboxs;
	}
}
