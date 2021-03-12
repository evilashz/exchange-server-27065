using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000269 RID: 617
	public class RetentionPolicyTagObjectResolver : AdObjectResolver
	{
		// Token: 0x0600295E RID: 10590 RVA: 0x000826F0 File Offset: 0x000808F0
		public IEnumerable<RetentionPolicyTagResolverRow> ResolveObjects(IEnumerable<ADObjectId> identities)
		{
			IConfigurationSession session = (IConfigurationSession)this.CreateAdSession();
			return from row in base.ResolveObjects<RetentionPolicyTagResolverRow>(identities, RetentionPolicyTagResolverRow.Properties, (ADRawEntry e) => new RetentionPolicyTagResolverRow(e)
			{
				ContentSettings = this.GetELCContentSettings(session, e.Id).FirstOrDefault<ElcContentSettings>()
			})
			orderby row.Name
			select row;
		}

		// Token: 0x0600295F RID: 10591 RVA: 0x00082755 File Offset: 0x00080955
		internal override IDirectorySession CreateAdSession()
		{
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, base.TenantSharedConfigurationSessionSetting ?? base.TenantSessionSetting, 186, "CreateAdSession", "f:\\15.00.1497\\sources\\dev\\admin\\src\\ecp\\Organize\\RetentionPolicyTagResolver.cs");
		}

		// Token: 0x06002960 RID: 10592 RVA: 0x00082784 File Offset: 0x00080984
		internal ADPagedReader<ElcContentSettings> GetELCContentSettings(IConfigurationSession session, ADObjectId identity)
		{
			return session.FindPaged<ElcContentSettings>(identity, QueryScope.SubTree, null, null, 0);
		}

		// Token: 0x040020D3 RID: 8403
		internal static readonly RetentionPolicyTagObjectResolver Instance = new RetentionPolicyTagObjectResolver();
	}
}
