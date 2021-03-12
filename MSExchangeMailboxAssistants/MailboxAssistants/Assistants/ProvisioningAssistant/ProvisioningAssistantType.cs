using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ProvisioningAssistant
{
	// Token: 0x02000130 RID: 304
	internal sealed class ProvisioningAssistantType : IEventBasedAssistantType, IAssistantType
	{
		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000C2C RID: 3116 RVA: 0x0004F476 File Offset: 0x0004D676
		public LocalizedString Name
		{
			get
			{
				return Strings.provisioningAssistantName;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000C2D RID: 3117 RVA: 0x0004F47D File Offset: 0x0004D67D
		public string NonLocalizedName
		{
			get
			{
				return "ProvisioningAssistant";
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000C2E RID: 3118 RVA: 0x0004F484 File Offset: 0x0004D684
		public MapiEventTypeFlags EventMask
		{
			get
			{
				return (MapiEventTypeFlags)(-1);
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000C2F RID: 3119 RVA: 0x0004F487 File Offset: 0x0004D687
		public bool NeedsMailboxSession
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x0004F48A File Offset: 0x0004D68A
		public bool ProcessesPublicDatabases
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x0004F48D File Offset: 0x0004D68D
		public PropertyDefinition[] PreloadItemProperties
		{
			get
			{
				return new PropertyDefinition[0];
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000C32 RID: 3122 RVA: 0x0004F495 File Offset: 0x0004D695
		public Guid Identity
		{
			get
			{
				return AssistantIdentity.ProvisioningAssistant;
			}
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x0004F49C File Offset: 0x0004D69C
		public IEventBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			return new ProvisioningAssistant(databaseInfo, this.Name, this.NonLocalizedName);
		}

		// Token: 0x04000790 RID: 1936
		internal const string AssistantName = "ProvisioningAssistant";
	}
}
