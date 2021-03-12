using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Approval;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Approval
{
	// Token: 0x0200012E RID: 302
	internal sealed class ApprovalAssistantType : IEventBasedAssistantType, IAssistantType, IMailboxFilter
	{
		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000C20 RID: 3104 RVA: 0x0004F274 File Offset: 0x0004D474
		public LocalizedString Name
		{
			get
			{
				return Strings.approvalName;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000C21 RID: 3105 RVA: 0x0004F27B File Offset: 0x0004D47B
		public string NonLocalizedName
		{
			get
			{
				return "ApprovalAssistant";
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000C22 RID: 3106 RVA: 0x0004F282 File Offset: 0x0004D482
		public MapiEventTypeFlags EventMask
		{
			get
			{
				return (MapiEventTypeFlags)(-1);
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000C23 RID: 3107 RVA: 0x0004F285 File Offset: 0x0004D485
		public MailboxType MailboxType
		{
			get
			{
				return MailboxType.Arbitration;
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x0004F288 File Offset: 0x0004D488
		public bool NeedsMailboxSession
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000C25 RID: 3109 RVA: 0x0004F28B File Offset: 0x0004D48B
		public bool ProcessesPublicDatabases
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000C26 RID: 3110 RVA: 0x0004F28E File Offset: 0x0004D48E
		public PropertyDefinition[] PreloadItemProperties
		{
			get
			{
				return ApprovalProcessor.ApprovalProperties;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000C27 RID: 3111 RVA: 0x0004F295 File Offset: 0x0004D495
		public Guid Identity
		{
			get
			{
				return AssistantIdentity.ApprovalAssistant;
			}
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x0004F29C File Offset: 0x0004D49C
		public IEventBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			return new ApprovalAssistant(databaseInfo, this.Name, this.NonLocalizedName);
		}

		// Token: 0x04000786 RID: 1926
		internal const string AssistantName = "ApprovalAssistant";
	}
}
