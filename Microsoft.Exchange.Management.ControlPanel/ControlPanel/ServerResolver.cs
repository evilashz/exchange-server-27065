using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.ControlPanel.DBMgmt;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200010D RID: 269
	public class ServerResolver : AdObjectResolver, IServerResolver
	{
		// Token: 0x06001FA8 RID: 8104 RVA: 0x0005F7F9 File Offset: 0x0005D9F9
		private ServerResolver()
		{
		}

		// Token: 0x17001A1E RID: 6686
		// (get) Token: 0x06001FA9 RID: 8105 RVA: 0x0005F801 File Offset: 0x0005DA01
		// (set) Token: 0x06001FAA RID: 8106 RVA: 0x0005F808 File Offset: 0x0005DA08
		internal static IServerResolver Instance { get; set; } = new ServerResolver();

		// Token: 0x06001FAB RID: 8107 RVA: 0x0005F820 File Offset: 0x0005DA20
		public IEnumerable<ServerResolverRow> ResolveObjects(IEnumerable<ADObjectId> identities)
		{
			return from row in base.ResolveObjects<ServerResolverRow>(identities, ServerResolverRow.Properties, (ADRawEntry e) => new ServerResolverRow(e))
			orderby row.DisplayName
			select row;
		}

		// Token: 0x06001FAC RID: 8108 RVA: 0x0005F878 File Offset: 0x0005DA78
		internal override IDirectorySession CreateAdSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(true, ConsistencyMode.PartiallyConsistent, base.TenantSessionSetting, 199, "CreateAdSession", "f:\\15.00.1497\\sources\\dev\\admin\\src\\ecp\\DBMgmt\\ServerResolver.cs");
		}
	}
}
