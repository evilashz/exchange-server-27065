using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.RecipientDLExpansion
{
	// Token: 0x0200025F RID: 607
	internal sealed class RecipientDLExpansionEventBasedAssistantType : IEventBasedAssistantType, IAssistantType, IMailboxFilter
	{
		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x0600168B RID: 5771 RVA: 0x0008019B File Offset: 0x0007E39B
		public LocalizedString Name
		{
			get
			{
				return Strings.recipientDLExpansionAssistantName;
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x0600168C RID: 5772 RVA: 0x000801A2 File Offset: 0x0007E3A2
		public string NonLocalizedName
		{
			get
			{
				return "RecipientDLExpansionEventBasedAssistant";
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x0600168D RID: 5773 RVA: 0x000801A9 File Offset: 0x0007E3A9
		public MapiEventTypeFlags EventMask
		{
			get
			{
				return MapiEventTypeFlags.ObjectCreated | MapiEventTypeFlags.ObjectMoved | MapiEventTypeFlags.ObjectCopied;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x0600168E RID: 5774 RVA: 0x000801AD File Offset: 0x0007E3AD
		public bool NeedsMailboxSession
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x0600168F RID: 5775 RVA: 0x000801B0 File Offset: 0x0007E3B0
		public bool ProcessesPublicDatabases
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06001690 RID: 5776 RVA: 0x000801B3 File Offset: 0x0007E3B3
		public PropertyDefinition[] PreloadItemProperties
		{
			get
			{
				return RecipientDLExpansionEventBasedAssistantType.InternalPreloadItemProperties;
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06001691 RID: 5777 RVA: 0x000801BA File Offset: 0x0007E3BA
		public Guid Identity
		{
			get
			{
				return AssistantIdentity.RecipientDLExpansionEventBasedAssistant;
			}
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x000801C1 File Offset: 0x0007E3C1
		public IEventBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			return new RecipientDLExpansionEventBasedAssistant(databaseInfo, this.Name, this.NonLocalizedName);
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06001693 RID: 5779 RVA: 0x000801D5 File Offset: 0x0007E3D5
		public MailboxType MailboxType
		{
			get
			{
				return MailboxType.User;
			}
		}

		// Token: 0x04000D3E RID: 3390
		internal const string AssistantName = "RecipientDLExpansionEventBasedAssistant";

		// Token: 0x04000D3F RID: 3391
		internal static readonly PropertyDefinition[] InternalPreloadItemProperties = new PropertyDefinition[]
		{
			ItemSchema.DocumentId,
			MessageItemSchema.GroupExpansionRecipients,
			MessageItemSchema.GroupExpansionError
		};
	}
}
