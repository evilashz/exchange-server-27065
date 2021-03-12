using System;
using System.Diagnostics;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020008A9 RID: 2217
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConversationIndexTrackingEx
	{
		// Token: 0x060052D9 RID: 21209 RVA: 0x0015A0AE File Offset: 0x001582AE
		private ConversationIndexTrackingEx()
		{
			this.traceBuilder = new StringBuilder(0);
			this.traceOverflow = false;
		}

		// Token: 0x060052DA RID: 21210 RVA: 0x0015A0C9 File Offset: 0x001582C9
		public static ConversationIndexTrackingEx Create()
		{
			return new ConversationIndexTrackingEx();
		}

		// Token: 0x060052DB RID: 21211 RVA: 0x0015A0D0 File Offset: 0x001582D0
		public void Trace(string key, string value)
		{
			Util.ThrowOnNullArgument(key, "key");
			this.Trace(string.Format("{0}={1}", key, value ?? "<null>"));
		}

		// Token: 0x060052DC RID: 21212 RVA: 0x0015A0F8 File Offset: 0x001582F8
		public void Trace(ConversationIndex index)
		{
			Util.ThrowOnNullArgument(index, "index");
			StringBuilder stringBuilder = new StringBuilder(0);
			stringBuilder.Append("[");
			stringBuilder.Append(string.Format("{0}={1}", "CID", index.Guid));
			stringBuilder.Append(";");
			stringBuilder.Append(string.Format("{0}={1}", "IDXHEAD", GlobalObjectId.ByteArrayToHexString(index.Header)));
			stringBuilder.Append(";");
			stringBuilder.Append(string.Format("{0}={1}", "IDXCOUNT", index.Components.Count));
			stringBuilder.Append("]");
			this.Trace("II", stringBuilder.ToString());
		}

		// Token: 0x060052DD RID: 21213 RVA: 0x0015A1C8 File Offset: 0x001583C8
		public void TraceVersionAndHeuristics(string fixupStage)
		{
			this.Trace(string.Format("Version={0}, Stage={1}", ConversationIndexTrackingEx.GetBuildVersion(), fixupStage));
		}

		// Token: 0x060052DE RID: 21214 RVA: 0x0015A1E0 File Offset: 0x001583E0
		internal static ServerVersion GetBuildVersion()
		{
			if (ConversationIndexTrackingEx.buildVersion == null)
			{
				FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(Util.GetAssemblyLocation());
				ConversationIndexTrackingEx.buildVersion = new ServerVersion(1879048192 | (versionInfo.FileMajorPart & 63) << 22 | (versionInfo.FileMinorPart & 63) << 16 | 32768 | (versionInfo.FileBuildPart & 32767));
			}
			return ConversationIndexTrackingEx.buildVersion;
		}

		// Token: 0x060052DF RID: 21215 RVA: 0x0015A248 File Offset: 0x00158448
		public override string ToString()
		{
			string text = this.traceBuilder.ToString();
			if (this.traceOverflow)
			{
				text = text.Substring(0, 1024 - "...".Length);
				text += "...";
			}
			return text;
		}

		// Token: 0x060052E0 RID: 21216 RVA: 0x0015A290 File Offset: 0x00158490
		private void Trace(string value)
		{
			if (this.traceOverflow)
			{
				return;
			}
			if (this.traceBuilder.Length >= 1024)
			{
				this.traceOverflow = true;
				return;
			}
			if (this.traceBuilder.Length > 0)
			{
				this.traceBuilder.Append(";");
			}
			this.traceBuilder.Append(value);
			if (this.traceBuilder.Length > 1024)
			{
				this.traceOverflow = true;
			}
		}

		// Token: 0x04002D22 RID: 11554
		private const int MaxTraceLength = 1024;

		// Token: 0x04002D23 RID: 11555
		private const string Ellipsis = "...";

		// Token: 0x04002D24 RID: 11556
		public const string BodyTagInMilliSeconds = "BT";

		// Token: 0x04002D25 RID: 11557
		public const string SearchByMessageIdInMilliSeconds = "SBMID";

		// Token: 0x04002D26 RID: 11558
		public const string SearchByConversationIdInMilliSeconds = "SBCID";

		// Token: 0x04002D27 RID: 11559
		public const string SearchByTopicInMilliSeconds = "SBT";

		// Token: 0x04002D28 RID: 11560
		public const string SearchBySMSConversationIdInMilliSeconds = "SBSMSCID";

		// Token: 0x04002D29 RID: 11561
		public const string FixupInMilliSeconds = "FIXUP";

		// Token: 0x04002D2A RID: 11562
		public const string IncomingIndex = "II";

		// Token: 0x04002D2B RID: 11563
		public const string Stage1 = "S1";

		// Token: 0x04002D2C RID: 11564
		public const string Stage2 = "S2";

		// Token: 0x04002D2D RID: 11565
		public const string Stage3 = "S3";

		// Token: 0x04002D2E RID: 11566
		public const string TopicHashAdded = "THA";

		// Token: 0x04002D2F RID: 11567
		private const string ConversationId = "CID";

		// Token: 0x04002D30 RID: 11568
		private const string ConversationIndexHeader = "IDXHEAD";

		// Token: 0x04002D31 RID: 11569
		private const string ConversationIndexComponentsCount = "IDXCOUNT";

		// Token: 0x04002D32 RID: 11570
		private const string TraceSeparator = ";";

		// Token: 0x04002D33 RID: 11571
		private const string traceFormat = "{0}={1}";

		// Token: 0x04002D34 RID: 11572
		private static ServerVersion buildVersion;

		// Token: 0x04002D35 RID: 11573
		private StringBuilder traceBuilder;

		// Token: 0x04002D36 RID: 11574
		private bool traceOverflow;
	}
}
