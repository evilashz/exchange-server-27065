using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000359 RID: 857
	public class SecurityPrincipalObjectResolver : AdObjectResolver
	{
		// Token: 0x06002FBB RID: 12219 RVA: 0x00091660 File Offset: 0x0008F860
		public IEnumerable<SecurityPrincipalRow> ResolveObjects(IEnumerable<ADObjectId> identities)
		{
			return from row in base.ResolveObjects<SecurityPrincipalRow>(identities, SecurityPrincipalRow.Properties, (ADRawEntry e) => new SecurityPrincipalRow(e))
			orderby row.Name
			select row;
		}

		// Token: 0x06002FBC RID: 12220 RVA: 0x000916B8 File Offset: 0x0008F8B8
		internal override IDirectorySession CreateAdSession()
		{
			return DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.PartiallyConsistent, base.TenantSessionSetting, 124, "CreateAdSession", "f:\\15.00.1497\\sources\\dev\\admin\\src\\ecp\\Pickers\\SecurityPrincipalResolver.cs");
		}

		// Token: 0x04002318 RID: 8984
		internal static readonly SecurityPrincipalObjectResolver Instance = new SecurityPrincipalObjectResolver();
	}
}
