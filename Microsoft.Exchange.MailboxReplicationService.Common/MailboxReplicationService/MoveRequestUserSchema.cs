using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001EA RID: 490
	internal class MoveRequestUserSchema : MailEnabledOrgPersonSchema
	{
		// Token: 0x060014C2 RID: 5314 RVA: 0x0002EAFE File Offset: 0x0002CCFE
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<ADUserSchema>();
		}

		// Token: 0x04000A4F RID: 2639
		public static readonly ADPropertyDefinition ExchangeGuid = ADMailboxRecipientSchema.ExchangeGuid;

		// Token: 0x04000A50 RID: 2640
		public static readonly ADPropertyDefinition SourceDatabase = ADUserSchema.MailboxMoveSourceMDB;

		// Token: 0x04000A51 RID: 2641
		public static readonly ADPropertyDefinition TargetDatabase = ADUserSchema.MailboxMoveTargetMDB;

		// Token: 0x04000A52 RID: 2642
		public static readonly ADPropertyDefinition SourceArchiveDatabase = ADUserSchema.MailboxMoveSourceArchiveMDB;

		// Token: 0x04000A53 RID: 2643
		public static readonly ADPropertyDefinition TargetArchiveDatabase = ADUserSchema.MailboxMoveTargetArchiveMDB;

		// Token: 0x04000A54 RID: 2644
		public static readonly ADPropertyDefinition Flags = ADUserSchema.MailboxMoveFlags;

		// Token: 0x04000A55 RID: 2645
		public static readonly ADPropertyDefinition RemoteHostName = ADUserSchema.MailboxMoveRemoteHostName;

		// Token: 0x04000A56 RID: 2646
		public static readonly ADPropertyDefinition BatchName = ADUserSchema.MailboxMoveBatchName;

		// Token: 0x04000A57 RID: 2647
		public static readonly ADPropertyDefinition Status = ADUserSchema.MailboxMoveStatus;
	}
}
