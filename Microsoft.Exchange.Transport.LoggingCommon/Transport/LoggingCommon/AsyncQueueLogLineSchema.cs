using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.LoggingCommon
{
	// Token: 0x02000003 RID: 3
	internal static class AsyncQueueLogLineSchema
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002389 File Offset: 0x00000589
		public static int TimeStampFieldIndex
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4 RVA: 0x0000238C File Offset: 0x0000058C
		public static CsvTable DefaultSchema
		{
			get
			{
				return AsyncQueueLogLineSchema.schema;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002393 File Offset: 0x00000593
		public static Version DefaultVersion
		{
			get
			{
				return AsyncQueueLogLineSchema.E15Version;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000239C File Offset: 0x0000059C
		public static List<Version> SupportedVersionsInAscendingOrder
		{
			get
			{
				return new List<Version>
				{
					AsyncQueueLogLineSchema.E15Version
				};
			}
		}

		// Token: 0x04000006 RID: 6
		internal const string Timestamp = "Timestamp";

		// Token: 0x04000007 RID: 7
		internal const string OrganizationalUnitRoot = "OrganizationalUnitRoot";

		// Token: 0x04000008 RID: 8
		internal const string StepTransactionId = "StepTransactionId";

		// Token: 0x04000009 RID: 9
		internal const string ProcessStartDatetime = "ProcessStartDatetime";

		// Token: 0x0400000A RID: 10
		internal const string PrimaryProperties = "PrimaryProperties";

		// Token: 0x0400000B RID: 11
		internal const string ExtendedProperties = "ExtendedProperties";

		// Token: 0x0400000C RID: 12
		private static readonly Version E15Version = new Version(15, 0, 0, 0);

		// Token: 0x0400000D RID: 13
		private static readonly CsvTable schema = new CsvTable(new CsvField[]
		{
			new CsvField("Timestamp", typeof(DateTime), AsyncQueueLogLineSchema.E15Version),
			new CsvField("OrganizationalUnitRoot", typeof(string), true, AsyncQueueLogLineSchema.E15Version),
			new CsvField("StepTransactionId", typeof(string), true, AsyncQueueLogLineSchema.E15Version),
			new CsvField("ProcessStartDatetime", typeof(DateTime), true, AsyncQueueLogLineSchema.E15Version),
			new CsvField("PrimaryProperties", typeof(KeyValuePair<string, object>[]), AsyncQueueLogLineSchema.E15Version),
			new CsvField("ExtendedProperties", typeof(KeyValuePair<string, object>[]), AsyncQueueLogLineSchema.E15Version)
		});
	}
}
