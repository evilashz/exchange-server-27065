using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MessagingPolicies.UnJournalAgent
{
	// Token: 0x02000022 RID: 34
	internal static class AgentStrings
	{
		// Token: 0x060000B3 RID: 179 RVA: 0x000077CC File Offset: 0x000059CC
		static AgentStrings()
		{
			AgentStrings.stringIDs.Add(4023179196U, "PermanentErrorOther");
			AgentStrings.stringIDs.Add(2314169302U, "NoRecipientsResolvedMsg");
			AgentStrings.stringIDs.Add(3020404937U, "UnexpectedJournalMessageFormatMsg");
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00007844 File Offset: 0x00005A44
		public static LocalizedString DefectiveJournalNoRecipients(string recipientList)
		{
			return new LocalizedString("DefectiveJournalNoRecipients", AgentStrings.ResourceManager, new object[]
			{
				recipientList
			});
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000786C File Offset: 0x00005A6C
		public static LocalizedString InvalidEnvelopeJournalMessageMissingReport(string messageid)
		{
			return new LocalizedString("InvalidEnvelopeJournalMessageMissingReport", AgentStrings.ResourceManager, new object[]
			{
				messageid
			});
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00007894 File Offset: 0x00005A94
		public static LocalizedString PermanentErrorOther
		{
			get
			{
				return new LocalizedString("PermanentErrorOther", AgentStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000078AC File Offset: 0x00005AAC
		public static LocalizedString InvalidEnvelopeJournalMessageMissingEmbedded(string messageid)
		{
			return new LocalizedString("InvalidEnvelopeJournalMessageMissingEmbedded", AgentStrings.ResourceManager, new object[]
			{
				messageid
			});
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000078D4 File Offset: 0x00005AD4
		public static LocalizedString InvalidEnvelopeJournalMessageAttachment(string messageid)
		{
			return new LocalizedString("InvalidEnvelopeJournalMessageAttachment", AgentStrings.ResourceManager, new object[]
			{
				messageid
			});
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000078FC File Offset: 0x00005AFC
		public static LocalizedString InvalidEnvelopeJournalVersionForExtraction(string version)
		{
			return new LocalizedString("InvalidEnvelopeJournalVersionForExtraction", AgentStrings.ResourceManager, new object[]
			{
				version
			});
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00007924 File Offset: 0x00005B24
		public static LocalizedString NoRecipientsResolvedMsg
		{
			get
			{
				return new LocalizedString("NoRecipientsResolvedMsg", AgentStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000793C File Offset: 0x00005B3C
		public static LocalizedString UnProvisionedRecipientsMsg(string recipientList)
		{
			return new LocalizedString("UnProvisionedRecipientsMsg", AgentStrings.ResourceManager, new object[]
			{
				recipientList
			});
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00007964 File Offset: 0x00005B64
		public static LocalizedString UnexpectedJournalMessageFormatMsg
		{
			get
			{
				return new LocalizedString("UnexpectedJournalMessageFormatMsg", AgentStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000797C File Offset: 0x00005B7C
		public static LocalizedString InvalidEnvelopeJournalMessageMissingSender(string messageid)
		{
			return new LocalizedString("InvalidEnvelopeJournalMessageMissingSender", AgentStrings.ResourceManager, new object[]
			{
				messageid
			});
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000079A4 File Offset: 0x00005BA4
		public static LocalizedString InvalidEnvelopeJournalMessagesInvalidFormat(string messageid, string error)
		{
			return new LocalizedString("InvalidEnvelopeJournalMessagesInvalidFormat", AgentStrings.ResourceManager, new object[]
			{
				messageid,
				error
			});
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000079D0 File Offset: 0x00005BD0
		public static LocalizedString DefectiveJournalWithRecipients(string recipientList)
		{
			return new LocalizedString("DefectiveJournalWithRecipients", AgentStrings.ResourceManager, new object[]
			{
				recipientList
			});
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000079F8 File Offset: 0x00005BF8
		public static LocalizedString InvalidEnvelopeJournalMessageMissingRequiredMessageId(string messageid)
		{
			return new LocalizedString("InvalidEnvelopeJournalMessageMissingRequiredMessageId", AgentStrings.ResourceManager, new object[]
			{
				messageid
			});
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00007A20 File Offset: 0x00005C20
		public static LocalizedString GetLocalizedString(AgentStrings.IDs key)
		{
			return new LocalizedString(AgentStrings.stringIDs[(uint)key], AgentStrings.ResourceManager, new object[0]);
		}

		// Token: 0x040000F9 RID: 249
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(3);

		// Token: 0x040000FA RID: 250
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.MessagingPolicies.UnJournalAgent.AgentStrings", typeof(AgentStrings).GetTypeInfo().Assembly);

		// Token: 0x02000023 RID: 35
		public enum IDs : uint
		{
			// Token: 0x040000FC RID: 252
			PermanentErrorOther = 4023179196U,
			// Token: 0x040000FD RID: 253
			NoRecipientsResolvedMsg = 2314169302U,
			// Token: 0x040000FE RID: 254
			UnexpectedJournalMessageFormatMsg = 3020404937U
		}

		// Token: 0x02000024 RID: 36
		private enum ParamIDs
		{
			// Token: 0x04000100 RID: 256
			DefectiveJournalNoRecipients,
			// Token: 0x04000101 RID: 257
			InvalidEnvelopeJournalMessageMissingReport,
			// Token: 0x04000102 RID: 258
			InvalidEnvelopeJournalMessageMissingEmbedded,
			// Token: 0x04000103 RID: 259
			InvalidEnvelopeJournalMessageAttachment,
			// Token: 0x04000104 RID: 260
			InvalidEnvelopeJournalVersionForExtraction,
			// Token: 0x04000105 RID: 261
			UnProvisionedRecipientsMsg,
			// Token: 0x04000106 RID: 262
			InvalidEnvelopeJournalMessageMissingSender,
			// Token: 0x04000107 RID: 263
			InvalidEnvelopeJournalMessagesInvalidFormat,
			// Token: 0x04000108 RID: 264
			DefectiveJournalWithRecipients,
			// Token: 0x04000109 RID: 265
			InvalidEnvelopeJournalMessageMissingRequiredMessageId
		}
	}
}
