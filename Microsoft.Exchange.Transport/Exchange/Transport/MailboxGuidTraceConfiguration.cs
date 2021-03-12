using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000079 RID: 121
	internal class MailboxGuidTraceConfiguration : TraceConfigurationBase
	{
		// Token: 0x06000380 RID: 896 RVA: 0x0000FA6C File Offset: 0x0000DC6C
		public override void OnLoad()
		{
			this.filteredMailboxs = MailboxGuidTraceConfiguration.GetGuidFilterList(this.exTraceConfiguration, "MailboxGuid");
			this.filteredMDBs = MailboxGuidTraceConfiguration.GetGuidFilterList(this.exTraceConfiguration, "MailboxDatabaseGuid");
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0000FA9A File Offset: 0x0000DC9A
		public List<Guid> FilteredMailboxs
		{
			get
			{
				return this.filteredMailboxs;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000382 RID: 898 RVA: 0x0000FAA2 File Offset: 0x0000DCA2
		public List<Guid> FilteredMDBs
		{
			get
			{
				return this.filteredMDBs;
			}
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000FAAC File Offset: 0x0000DCAC
		private static List<Guid> GetGuidFilterList(ExTraceConfiguration configuration, string filterKey)
		{
			List<Guid> list = new List<Guid>();
			List<string> list2;
			if (configuration.CustomParameters.TryGetValue(filterKey, out list2))
			{
				foreach (string g in list2)
				{
					try
					{
						Guid item = new Guid(g);
						list.Add(item);
					}
					catch (FormatException)
					{
					}
				}
			}
			return list;
		}

		// Token: 0x040001F7 RID: 503
		public const string MailboxGuidFilterKey = "MailboxGuid";

		// Token: 0x040001F8 RID: 504
		public const string MailboxDatabaseGuidFilterKey = "MailboxDatabaseGuid";

		// Token: 0x040001F9 RID: 505
		private List<Guid> filteredMDBs;

		// Token: 0x040001FA RID: 506
		private List<Guid> filteredMailboxs;
	}
}
