using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200050E RID: 1294
	public class MailboxPlanResolver : AdObjectResolver
	{
		// Token: 0x06003E0D RID: 15885 RVA: 0x000BA6CC File Offset: 0x000B88CC
		public IEnumerable<MailboxPlanResolverRow> ResolveObjects(IEnumerable<ADObjectId> identities)
		{
			return from row in base.ResolveObjects<MailboxPlanResolverRow>(identities, MailboxPlanResolverRow.Properties, (ADRawEntry e) => new MailboxPlanResolverRow(e))
			orderby row.DisplayName
			select row;
		}

		// Token: 0x06003E0E RID: 15886 RVA: 0x000BA724 File Offset: 0x000B8924
		internal override IDirectorySession CreateAdSession()
		{
			return DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.PartiallyConsistent, base.TenantSessionSetting, 106, "CreateAdSession", "f:\\15.00.1497\\sources\\dev\\admin\\src\\ecp\\UsersGroups\\MailboxPlanResolver.cs");
		}

		// Token: 0x0400284F RID: 10319
		internal static readonly MailboxPlanResolver Instance = new MailboxPlanResolver();
	}
}
