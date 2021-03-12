using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200007A RID: 122
	internal class BadItemLog : ObjectLog<BadItemData>
	{
		// Token: 0x06000543 RID: 1347 RVA: 0x0001EBC9 File Offset: 0x0001CDC9
		private BadItemLog() : base(new BadItemLog.BadItemLogSchema(), new SimpleObjectLogConfiguration("BadItem", "BadItemLogEnabled", "BadItemLogMaxDirSize", "BadItemLogMaxFileSize"))
		{
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0001EBF0 File Offset: 0x0001CDF0
		public static void Write(Guid requestGuid, BadMessageRec item)
		{
			BadItemData objectToLog = new BadItemData(requestGuid, item);
			BadItemLog.instance.LogObject(objectToLog);
		}

		// Token: 0x04000225 RID: 549
		private static BadItemLog instance = new BadItemLog();

		// Token: 0x0200007B RID: 123
		private class BadItemLogSchema : ObjectLogSchema
		{
			// Token: 0x17000148 RID: 328
			// (get) Token: 0x06000546 RID: 1350 RVA: 0x0001EC1C File Offset: 0x0001CE1C
			public override string Software
			{
				get
				{
					return "Microsoft Exchange Mailbox Replication Service";
				}
			}

			// Token: 0x17000149 RID: 329
			// (get) Token: 0x06000547 RID: 1351 RVA: 0x0001EC23 File Offset: 0x0001CE23
			public override string LogType
			{
				get
				{
					return "BadItem Log";
				}
			}

			// Token: 0x04000226 RID: 550
			public static readonly ObjectLogSimplePropertyDefinition<BadItemData> RequestGuid = new ObjectLogSimplePropertyDefinition<BadItemData>("RequestGuid", (BadItemData d) => d.RequestGuid);

			// Token: 0x04000227 RID: 551
			public static readonly ObjectLogSimplePropertyDefinition<BadItemData> BadItemKind = new ObjectLogSimplePropertyDefinition<BadItemData>("BadItemKind", (BadItemData d) => d.Kind.ToString());

			// Token: 0x04000228 RID: 552
			public static readonly ObjectLogSimplePropertyDefinition<BadItemData> EntryId = new ObjectLogSimplePropertyDefinition<BadItemData>("EntryId", (BadItemData d) => TraceUtils.DumpEntryId(d.EntryId));

			// Token: 0x04000229 RID: 553
			public static readonly ObjectLogSimplePropertyDefinition<BadItemData> FolderId = new ObjectLogSimplePropertyDefinition<BadItemData>("FolderId", (BadItemData d) => TraceUtils.DumpEntryId(d.EntryId));

			// Token: 0x0400022A RID: 554
			public static readonly ObjectLogSimplePropertyDefinition<BadItemData> WKFType = new ObjectLogSimplePropertyDefinition<BadItemData>("WKFType", (BadItemData d) => d.WKFType.ToString());

			// Token: 0x0400022B RID: 555
			public static readonly ObjectLogSimplePropertyDefinition<BadItemData> MessageClass = new ObjectLogSimplePropertyDefinition<BadItemData>("MessageClass", (BadItemData d) => d.MessageClass);

			// Token: 0x0400022C RID: 556
			public static readonly ObjectLogSimplePropertyDefinition<BadItemData> MessageSize = new ObjectLogSimplePropertyDefinition<BadItemData>("MessageSize", delegate(BadItemData d)
			{
				if (d.MessageSize == null)
				{
					return string.Empty;
				}
				return d.MessageSize.Value.ToString();
			});

			// Token: 0x0400022D RID: 557
			public static readonly ObjectLogSimplePropertyDefinition<BadItemData> DateSent = new ObjectLogSimplePropertyDefinition<BadItemData>("DateSent", delegate(BadItemData d)
			{
				if (d.DateSent == null)
				{
					return string.Empty;
				}
				return d.DateSent.Value.ToString();
			});

			// Token: 0x0400022E RID: 558
			public static readonly ObjectLogSimplePropertyDefinition<BadItemData> DateReceived = new ObjectLogSimplePropertyDefinition<BadItemData>("DateReceived", delegate(BadItemData d)
			{
				if (d.DateReceived == null)
				{
					return string.Empty;
				}
				return d.DateReceived.Value.ToString();
			});

			// Token: 0x0400022F RID: 559
			public static readonly ObjectLogSimplePropertyDefinition<BadItemData> FailureMessage = new ObjectLogSimplePropertyDefinition<BadItemData>("FailureMessage", delegate(BadItemData d)
			{
				if (d.FailureMessage == null)
				{
					return string.Empty;
				}
				return CommonUtils.FullExceptionMessage(d.FailureMessage).ToString();
			});

			// Token: 0x04000230 RID: 560
			public static readonly ObjectLogSimplePropertyDefinition<BadItemData> Category = new ObjectLogSimplePropertyDefinition<BadItemData>("Category", (BadItemData d) => d.Category);

			// Token: 0x04000231 RID: 561
			public static readonly ObjectLogSimplePropertyDefinition<BadItemData> CallStackHash = new ObjectLogSimplePropertyDefinition<BadItemData>("CallStackHash", (BadItemData d) => d.CallStackHash);
		}
	}
}
