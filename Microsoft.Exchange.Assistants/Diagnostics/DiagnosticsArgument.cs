using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Assistants.Diagnostics
{
	// Token: 0x02000098 RID: 152
	internal class DiagnosticsArgument : DiagnosableArgument
	{
		// Token: 0x06000486 RID: 1158 RVA: 0x0001853E File Offset: 0x0001673E
		public DiagnosticsArgument(string argument)
		{
			base.Initialize(argument);
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00018550 File Offset: 0x00016750
		protected override void InitializeSchema(Dictionary<string, Type> schema)
		{
			schema["summary"] = typeof(bool);
			schema["running"] = typeof(bool);
			schema["queued"] = typeof(bool);
			schema["history"] = typeof(bool);
			schema["assistant"] = typeof(string);
			schema["database"] = typeof(string);
			schema["mailboxprocessor"] = typeof(bool);
			schema["lockedmailboxes"] = typeof(bool);
			schema["mailboxprocessorscantime"] = typeof(bool);
			schema["publicfolder"] = typeof(bool);
			schema["split"] = typeof(bool);
			schema["recent"] = typeof(bool);
			schema["old"] = typeof(bool);
			schema["mailbox"] = typeof(Guid);
		}

		// Token: 0x04000295 RID: 661
		public const string DiagnosticsComponentName = "MailboxAssistants";

		// Token: 0x04000296 RID: 662
		public const string SummaryArgument = "summary";

		// Token: 0x04000297 RID: 663
		public const string RunningArgument = "running";

		// Token: 0x04000298 RID: 664
		public const string QueuedArgument = "queued";

		// Token: 0x04000299 RID: 665
		public const string AssistantArgument = "assistant";

		// Token: 0x0400029A RID: 666
		public const string DatabaseArgument = "database";

		// Token: 0x0400029B RID: 667
		public const string WindowJobHistoryArgument = "history";

		// Token: 0x0400029C RID: 668
		public const string MailboxProcessorAssistantArgument = "mailboxprocessor";

		// Token: 0x0400029D RID: 669
		public const string MailboxProcessorAssistantLockedMailboxArgument = "lockedmailboxes";

		// Token: 0x0400029E RID: 670
		public const string MailboxProcessorAssistantScanTime = "mailboxprocessorscantime";

		// Token: 0x0400029F RID: 671
		public const string MailboxProcessorAssistantXmlRoot = "MailboxProcessorAssistant";

		// Token: 0x040002A0 RID: 672
		public const string MailboxProcessorLastScanXmlElem = "MailboxProcessorLastScan";

		// Token: 0x040002A1 RID: 673
		public const string MailboxDatabaseXmlElem = "MailboxDatabase";

		// Token: 0x040002A2 RID: 674
		public const string MailboxDatabaseLastScanXmlElem = "LastScan";

		// Token: 0x040002A3 RID: 675
		public const string MailboxLockedDetectorXmlElem = "MailboxLockedDetector";

		// Token: 0x040002A4 RID: 676
		public const string MailboxLockedXmlElem = "LockedMailbox";

		// Token: 0x040002A5 RID: 677
		public const string MailboxGuidXmlElem = "MailboxGuid";

		// Token: 0x040002A6 RID: 678
		public const string MailboxLockedCounterXmlElem = "LockedDetectionCounter";

		// Token: 0x040002A7 RID: 679
		public const string PublicFolderAssistantArgument = "publicfolder";

		// Token: 0x040002A8 RID: 680
		public const string PublicFolderSplitArgument = "split";

		// Token: 0x040002A9 RID: 681
		public const string PublicFolderRecentSplitArgument = "recent";

		// Token: 0x040002AA RID: 682
		public const string PublicFolderOldSplitArgument = "old";

		// Token: 0x040002AB RID: 683
		public const string PublicFolderSplitMailboxArgument = "mailbox";

		// Token: 0x040002AC RID: 684
		public const string PublicFolderAssistantXmlRoot = "PublicFolderAssistant";

		// Token: 0x040002AD RID: 685
		public const string PublicFolderSplitXmlRoot = "PublicFolderSplit";

		// Token: 0x040002AE RID: 686
		public const string PublicFolderSplitStateXmlElement = "PublicFolderSplitState";

		// Token: 0x040002AF RID: 687
		public const string PublicFolderSplitDateXmlElement = "PublicFolderSplitDate";

		// Token: 0x040002B0 RID: 688
		public const string PublicFolderSplitStateValueXmlElement = "SplitState";

		// Token: 0x040002B1 RID: 689
		public const string PublicFolderSplitDateValueXmlElement = "SplitDate";

		// Token: 0x040002B2 RID: 690
		public const string PublicFolderMailboxGuidXmlElement = "MailboxGuid";
	}
}
