using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Reminders
{
	// Token: 0x02000253 RID: 595
	internal sealed class RemindersAssistantType : IEventBasedAssistantType, IAssistantType, IMailboxFilter
	{
		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x0600164A RID: 5706 RVA: 0x0007DDA3 File Offset: 0x0007BFA3
		public LocalizedString Name
		{
			get
			{
				return Strings.remindersAssistantName;
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x0600164B RID: 5707 RVA: 0x0007DDAA File Offset: 0x0007BFAA
		public string NonLocalizedName
		{
			get
			{
				return "RemindersAssistant";
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x0600164C RID: 5708 RVA: 0x0007DDB1 File Offset: 0x0007BFB1
		public MapiEventTypeFlags EventMask
		{
			get
			{
				return (MapiEventTypeFlags)(-1);
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x0600164D RID: 5709 RVA: 0x0007DDB4 File Offset: 0x0007BFB4
		public MailboxType MailboxType
		{
			get
			{
				return MailboxType.User;
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x0600164E RID: 5710 RVA: 0x0007DDB7 File Offset: 0x0007BFB7
		public bool NeedsMailboxSession
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x0600164F RID: 5711 RVA: 0x0007DDBA File Offset: 0x0007BFBA
		public bool ProcessesPublicDatabases
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06001650 RID: 5712 RVA: 0x0007DDBD File Offset: 0x0007BFBD
		public PropertyDefinition[] PreloadItemProperties
		{
			get
			{
				return RemindersAssistantType.preloadItemProperties;
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06001651 RID: 5713 RVA: 0x0007DDC4 File Offset: 0x0007BFC4
		public Guid Identity
		{
			get
			{
				return AssistantIdentity.RemindersEventBasedAssistant;
			}
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x0007DDCB File Offset: 0x0007BFCB
		public IEventBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			return new RemindersAssistant(databaseInfo, this.Name, this.NonLocalizedName);
		}

		// Token: 0x04000D1C RID: 3356
		internal const string AssistantName = "RemindersAssistant";

		// Token: 0x04000D1D RID: 3357
		private static readonly PropertyDefinition[] preloadItemProperties = new PropertyDefinition[0];
	}
}
