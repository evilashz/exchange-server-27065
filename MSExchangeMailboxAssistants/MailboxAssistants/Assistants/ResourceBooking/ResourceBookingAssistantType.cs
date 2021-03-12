using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ResourceBooking
{
	// Token: 0x0200012A RID: 298
	internal sealed class ResourceBookingAssistantType : IEventBasedAssistantType, IAssistantType
	{
		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000BFD RID: 3069 RVA: 0x0004E22E File Offset: 0x0004C42E
		public LocalizedString Name
		{
			get
			{
				return Strings.resName;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000BFE RID: 3070 RVA: 0x0004E235 File Offset: 0x0004C435
		public string NonLocalizedName
		{
			get
			{
				return "ResourceBookingAssistant";
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000BFF RID: 3071 RVA: 0x0004E23C File Offset: 0x0004C43C
		public MapiEventTypeFlags EventMask
		{
			get
			{
				return (MapiEventTypeFlags)(-1);
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000C00 RID: 3072 RVA: 0x0004E23F File Offset: 0x0004C43F
		public bool NeedsMailboxSession
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000C01 RID: 3073 RVA: 0x0004E242 File Offset: 0x0004C442
		public bool ProcessesPublicDatabases
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x0004E245 File Offset: 0x0004C445
		public PropertyDefinition[] PreloadItemProperties
		{
			get
			{
				return ResourceBookingAssistantType.preloadItemProperties;
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000C03 RID: 3075 RVA: 0x0004E24C File Offset: 0x0004C44C
		public Guid Identity
		{
			get
			{
				return AssistantIdentity.ResourceBookingAssistant;
			}
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x0004E253 File Offset: 0x0004C453
		public IEventBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			return new ResourceBookingAssistant(databaseInfo, this.Name, this.NonLocalizedName);
		}

		// Token: 0x0400076A RID: 1898
		internal const string AssistantName = "ResourceBookingAssistant";

		// Token: 0x0400076B RID: 1899
		private static readonly PropertyDefinition[] preloadItemProperties = new PropertyDefinition[]
		{
			ItemSchema.ReminderIsSet,
			MessageItemSchema.ReceivedRepresentingEmailAddress,
			MessageItemSchema.MessageLocaleId
		};
	}
}
