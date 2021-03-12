using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x0200001B RID: 27
	internal class UserADRawEntryAnchorMailbox : ArchiveSupportedAnchorMailbox
	{
		// Token: 0x060000C2 RID: 194 RVA: 0x000054B8 File Offset: 0x000036B8
		public UserADRawEntryAnchorMailbox(ADRawEntry activeDirectoryRawEntry, IRequestContext requestContext) : base(AnchorSource.UserADRawEntry, (activeDirectoryRawEntry != null && activeDirectoryRawEntry.Id != null) ? activeDirectoryRawEntry.Id.DistinguishedName : null, requestContext)
		{
			this.activeDirectoryRawEntry = activeDirectoryRawEntry;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000054E2 File Offset: 0x000036E2
		public override string GetOrganizationNameForLogging()
		{
			return ((OrganizationId)this.activeDirectoryRawEntry[ADObjectSchema.OrganizationId]).GetFriendlyName();
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000054FE File Offset: 0x000036FE
		protected override ADRawEntry LoadADRawEntry()
		{
			return this.activeDirectoryRawEntry;
		}

		// Token: 0x0400004C RID: 76
		private ADRawEntry activeDirectoryRawEntry;
	}
}
