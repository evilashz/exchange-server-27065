using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Conversations
{
	// Token: 0x02000027 RID: 39
	internal sealed class ConversationsAssistantType : IEventBasedAssistantType, IAssistantType
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000118 RID: 280 RVA: 0x000071D4 File Offset: 0x000053D4
		public LocalizedString Name
		{
			get
			{
				return Strings.conversationsAssistantName;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000119 RID: 281 RVA: 0x000071DB File Offset: 0x000053DB
		public string NonLocalizedName
		{
			get
			{
				return "ConversationsAssistant";
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600011A RID: 282 RVA: 0x000071E2 File Offset: 0x000053E2
		public MapiEventTypeFlags EventMask
		{
			get
			{
				return (MapiEventTypeFlags)(-1);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600011B RID: 283 RVA: 0x000071E5 File Offset: 0x000053E5
		public bool NeedsMailboxSession
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600011C RID: 284 RVA: 0x000071E8 File Offset: 0x000053E8
		public bool ProcessesPublicDatabases
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600011D RID: 285 RVA: 0x000071EB File Offset: 0x000053EB
		public PropertyDefinition[] PreloadItemProperties
		{
			get
			{
				return ConversationsAssistantType.preloadItemProperties;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600011E RID: 286 RVA: 0x000071F2 File Offset: 0x000053F2
		public Guid Identity
		{
			get
			{
				return AssistantIdentity.ConversationsAssistant;
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000071F9 File Offset: 0x000053F9
		public IEventBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			return new ConversationsAssistant(databaseInfo, this.Name, this.NonLocalizedName);
		}

		// Token: 0x0400011B RID: 283
		internal const string AssistantName = "ConversationsAssistant";

		// Token: 0x0400011C RID: 284
		private static readonly PropertyDefinition[] preloadItemProperties = new PropertyDefinition[]
		{
			ItemSchema.ConversationId,
			ItemSchema.Id
		};
	}
}
