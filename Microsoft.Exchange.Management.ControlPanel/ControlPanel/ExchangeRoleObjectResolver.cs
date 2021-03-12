using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000501 RID: 1281
	public class ExchangeRoleObjectResolver : AdObjectResolver
	{
		// Token: 0x06003DAC RID: 15788 RVA: 0x000B8E5E File Offset: 0x000B705E
		internal override IDirectorySession CreateAdSession()
		{
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, base.TenantSharedConfigurationSessionSetting ?? base.TenantSessionSetting, 89, "CreateAdSession", "f:\\15.00.1497\\sources\\dev\\admin\\src\\ecp\\UsersGroups\\ExchangeRoleResolver.cs");
		}

		// Token: 0x06003DAD RID: 15789 RVA: 0x000B8E98 File Offset: 0x000B7098
		public IEnumerable<ExchangeRoleObject> ResolveObjects(IEnumerable<ADObjectId> identities)
		{
			return from row in base.ResolveObjects<ExchangeRoleObject>(identities, ExchangeRoleObject.Properties, (ADRawEntry e) => new ExchangeRoleObject(e))
			orderby row.DisplayName
			select row;
		}

		// Token: 0x0400281F RID: 10271
		internal static readonly ExchangeRoleObjectResolver Instance = new ExchangeRoleObjectResolver();
	}
}
