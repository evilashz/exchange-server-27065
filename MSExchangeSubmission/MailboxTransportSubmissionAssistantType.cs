using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxTransport.Submission
{
	// Token: 0x02000007 RID: 7
	internal sealed class MailboxTransportSubmissionAssistantType : IEventBasedAssistantType, IAssistantType
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00004136 File Offset: 0x00002336
		public LocalizedString Name
		{
			get
			{
				return Strings.MailboxTransportSubmissionAssistantName;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000043 RID: 67 RVA: 0x0000413D File Offset: 0x0000233D
		public string NonLocalizedName
		{
			get
			{
				return "MailboxTransportSubmissionAssistant";
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00004144 File Offset: 0x00002344
		public MapiEventTypeFlags EventMask
		{
			get
			{
				return MapiEventTypeFlags.MailSubmitted;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000045 RID: 69 RVA: 0x0000414B File Offset: 0x0000234B
		public bool NeedsMailboxSession
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000046 RID: 70 RVA: 0x0000414E File Offset: 0x0000234E
		public bool ProcessesPublicDatabases
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00004151 File Offset: 0x00002351
		public PropertyDefinition[] PreloadItemProperties
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00004154 File Offset: 0x00002354
		public Guid Identity
		{
			get
			{
				return MailboxTransportSubmissionAssistant.MailboxTransportSubmissionServiceComponentGuid;
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000415C File Offset: 0x0000235C
		public IEventBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			MailboxTransportSubmissionAssistant mailboxTransportSubmissionAssistant = new MailboxTransportSubmissionAssistant(databaseInfo, this.Name, this.NonLocalizedName);
			lock (MailboxTransportSubmissionService.MailboxTransportSubmissionAssistantInstances)
			{
				if (MailboxTransportSubmissionService.MailboxTransportSubmissionAssistantInstances.Count < 100)
				{
					MailboxTransportSubmissionService.MailboxTransportSubmissionAssistantInstances.TryAdd(mailboxTransportSubmissionAssistant);
				}
			}
			return mailboxTransportSubmissionAssistant;
		}

		// Token: 0x04000055 RID: 85
		internal const string AssistantName = "MailboxTransportSubmissionAssistant";
	}
}
