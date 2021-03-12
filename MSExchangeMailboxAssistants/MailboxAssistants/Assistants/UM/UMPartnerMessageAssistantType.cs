using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.UM
{
	// Token: 0x02000110 RID: 272
	internal sealed class UMPartnerMessageAssistantType : IEventBasedAssistantType, IAssistantType
	{
		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x00047ED2 File Offset: 0x000460D2
		public LocalizedString Name
		{
			get
			{
				return Strings.umPartnerMessageName;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x00047ED9 File Offset: 0x000460D9
		public string NonLocalizedName
		{
			get
			{
				return "UMPartnerMessageAssistant";
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000B18 RID: 2840 RVA: 0x00047EE0 File Offset: 0x000460E0
		public MapiEventTypeFlags EventMask
		{
			get
			{
				return MapiEventTypeFlags.NewMail;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x00047EE3 File Offset: 0x000460E3
		public bool NeedsMailboxSession
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000B1A RID: 2842 RVA: 0x00047EE6 File Offset: 0x000460E6
		public bool ProcessesPublicDatabases
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x00047EEC File Offset: 0x000460EC
		public PropertyDefinition[] PreloadItemProperties
		{
			get
			{
				return new PropertyDefinition[]
				{
					ItemSchema.Id,
					ItemSchema.InternetMessageId,
					MessageItemSchema.XMsExchangeUMPartnerStatus,
					MessageItemSchema.XMsExchangeUMPartnerContent
				};
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000B1C RID: 2844 RVA: 0x00047F21 File Offset: 0x00046121
		public Guid Identity
		{
			get
			{
				return AssistantIdentity.UmPartnerMessageAssistant;
			}
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x00047F28 File Offset: 0x00046128
		public IEventBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			return new UMPartnerMessageAssistant(databaseInfo, this.Name, this.NonLocalizedName);
		}

		// Token: 0x04000702 RID: 1794
		internal const string AssistantName = "UMPartnerMessageAssistant";
	}
}
