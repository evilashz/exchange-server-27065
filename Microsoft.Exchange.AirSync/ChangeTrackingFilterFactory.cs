using System;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200004E RID: 78
	internal class ChangeTrackingFilterFactory
	{
		// Token: 0x060004AA RID: 1194 RVA: 0x0001D110 File Offset: 0x0001B310
		internal static ChangeTrackingFilter CreateFilter(string type, int version)
		{
			if (version >= 120 && type == "Calendar")
			{
				return new ChangeTrackingFilterFactory.V12CalendarFilter();
			}
			if (version >= 140 && type == "Email")
			{
				return new ChangeTrackingFilterFactory.V14EmailFilter();
			}
			if (version >= 120 && type == "Email")
			{
				return new ChangeTrackingFilterFactory.V12EmailFilter();
			}
			if (version <= 25 && type == "Email")
			{
				return new ChangeTrackingFilterFactory.V25EmailFilter();
			}
			if (type == "RecipientInfoCache")
			{
				return new ChangeTrackingFilterFactory.V14RecipientInfoCacheFilter();
			}
			return new ChangeTrackingFilterFactory.AllNodesFilter();
		}

		// Token: 0x0200004F RID: 79
		internal class AllNodesFilter : ChangeTrackingFilter
		{
			// Token: 0x060004AC RID: 1196 RVA: 0x0001D1A0 File Offset: 0x0001B3A0
			internal AllNodesFilter() : base(new ChangeTrackingNode[]
			{
				ChangeTrackingNode.AllNodes
			}, false)
			{
			}
		}

		// Token: 0x02000050 RID: 80
		internal class V12CalendarFilter : ChangeTrackingFilter
		{
			// Token: 0x060004AD RID: 1197 RVA: 0x0001D1C4 File Offset: 0x0001B3C4
			internal V12CalendarFilter() : base(new ChangeTrackingNode[]
			{
				ChangeTrackingNode.AllOtherNodes,
				new ChangeTrackingNode("Calendar:", "DtStamp"),
				new ChangeTrackingNode("Calendar:", "Attendees")
			}, true)
			{
			}
		}

		// Token: 0x02000051 RID: 81
		internal class V14EmailFilter : ChangeTrackingFilter
		{
			// Token: 0x060004AE RID: 1198 RVA: 0x0001D20C File Offset: 0x0001B40C
			internal V14EmailFilter() : base(new ChangeTrackingNode[]
			{
				ChangeTrackingNode.AllOtherNodes,
				new ChangeTrackingNode("Email:", "Read"),
				new ChangeTrackingNode("Email:", "Flag"),
				new ChangeTrackingNode("Email2:", "UmUserNotes"),
				new ChangeTrackingNode("Email2:", "LastVerbExecuted"),
				new ChangeTrackingNode("Email2:", "LastVerbExecutionTime"),
				new ChangeTrackingNode("Email:", "Categories")
			}, true)
			{
			}
		}

		// Token: 0x02000052 RID: 82
		internal class V14RecipientInfoCacheFilter : ChangeTrackingFilter
		{
			// Token: 0x060004AF RID: 1199 RVA: 0x0001D29C File Offset: 0x0001B49C
			internal V14RecipientInfoCacheFilter() : base(new ChangeTrackingNode[]
			{
				ChangeTrackingNode.AllOtherNodes,
				new ChangeTrackingNode("Contacts:", "Email1Address"),
				new ChangeTrackingNode("Contacts:", "FileAs"),
				new ChangeTrackingNode("Contacts:", "Alias"),
				new ChangeTrackingNode("Contacts:", "WeightedRank")
			}, true)
			{
			}
		}

		// Token: 0x02000053 RID: 83
		internal class V12EmailFilter : ChangeTrackingFilter
		{
			// Token: 0x060004B0 RID: 1200 RVA: 0x0001D308 File Offset: 0x0001B508
			internal V12EmailFilter() : base(new ChangeTrackingNode[]
			{
				ChangeTrackingNode.AllOtherNodes,
				new ChangeTrackingNode("Email:", "Read"),
				new ChangeTrackingNode("Email:", "Flag")
			}, true)
			{
			}
		}

		// Token: 0x02000054 RID: 84
		internal class V25EmailFilter : ChangeTrackingFilter
		{
			// Token: 0x060004B1 RID: 1201 RVA: 0x0001D350 File Offset: 0x0001B550
			internal V25EmailFilter() : base(new ChangeTrackingNode[]
			{
				ChangeTrackingNode.AllOtherNodes,
				new ChangeTrackingNode("Email:", "Read")
			}, true)
			{
			}
		}
	}
}
