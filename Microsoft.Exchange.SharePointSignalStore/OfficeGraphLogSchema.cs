using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.SharePointSignalStore
{
	// Token: 0x0200000A RID: 10
	internal static class OfficeGraphLogSchema
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000021CD File Offset: 0x000003CD
		public static CsvTable Schema
		{
			get
			{
				return OfficeGraphLogSchema.schema;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000021D4 File Offset: 0x000003D4
		public static string LogPrefix
		{
			get
			{
				return "OFFICEGRAPH";
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000021DB File Offset: 0x000003DB
		public static List<Version> SupportedVersionsInAscendingOrder
		{
			get
			{
				return OfficeGraphLogSchema.supportedVersionsInAscendingOrder;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000021E2 File Offset: 0x000003E2
		public static Version DefaultVersion
		{
			get
			{
				return OfficeGraphLogSchema.E15Version;
			}
		}

		// Token: 0x04000011 RID: 17
		public const string TimeStamp = "TimeStamp";

		// Token: 0x04000012 RID: 18
		public const string SignalType = "SignalType";

		// Token: 0x04000013 RID: 19
		public const string Signal = "Signal";

		// Token: 0x04000014 RID: 20
		public const string OrganizationId = "OrganizationId";

		// Token: 0x04000015 RID: 21
		public const string SharePointUrl = "SharePointUrl";

		// Token: 0x04000016 RID: 22
		public static readonly Version E15Version = new Version(15, 0, 0, 0);

		// Token: 0x04000017 RID: 23
		private static readonly List<Version> supportedVersionsInAscendingOrder = new List<Version>
		{
			OfficeGraphLogSchema.E15Version
		};

		// Token: 0x04000018 RID: 24
		private static readonly CsvTable schema = new CsvTable(new CsvField[]
		{
			new CsvField("TimeStamp", typeof(DateTime), OfficeGraphLogSchema.E15Version),
			new CsvField("SignalType", typeof(string), OfficeGraphLogSchema.E15Version),
			new CsvField("Signal", typeof(string), OfficeGraphLogSchema.E15Version),
			new CsvField("OrganizationId", typeof(string), OfficeGraphLogSchema.E15Version),
			new CsvField("SharePointUrl", typeof(string), OfficeGraphLogSchema.E15Version)
		});
	}
}
