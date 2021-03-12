using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.HA.DirectoryServices
{
	// Token: 0x0200001F RID: 31
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ADSessionWrapperFactoryImpl : IADSessionFactory
	{
		// Token: 0x06000184 RID: 388 RVA: 0x00004684 File Offset: 0x00002884
		public IADToplogyConfigurationSession CreateIgnoreInvalidRootOrgSession(bool readOnly)
		{
			return this.CreateSession(readOnly, ConsistencyMode.IgnoreInvalid);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000468E File Offset: 0x0000288E
		public IADToplogyConfigurationSession CreatePartiallyConsistentRootOrgSession(bool readOnly)
		{
			return this.CreateSession(readOnly, ConsistencyMode.PartiallyConsistent);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00004698 File Offset: 0x00002898
		public IADToplogyConfigurationSession CreateFullyConsistentRootOrgSession(bool readOnly)
		{
			return this.CreateSession(readOnly, ConsistencyMode.FullyConsistent);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000046A4 File Offset: 0x000028A4
		public IADRootOrganizationRecipientSession CreateIgnoreInvalidRootOrgRecipientSession()
		{
			IRootOrganizationRecipientSession session = DirectorySessionFactory.Default.CreateRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 226, "CreateIgnoreInvalidRootOrgRecipientSession", "f:\\15.00.1497\\sources\\dev\\data\\src\\HA\\DirectoryServices\\ADSessionFactory.cs");
			return ADRootOrganizationRecipientSessionWrapper.CreateWrapper(session);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000046D8 File Offset: 0x000028D8
		private IADToplogyConfigurationSession CreateSession(bool readOnly, ConsistencyMode consistencyMode)
		{
			ITopologyConfigurationSession session = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(readOnly, consistencyMode, ADSessionSettings.FromRootOrgScopeSet(), 239, "CreateSession", "f:\\15.00.1497\\sources\\dev\\data\\src\\HA\\DirectoryServices\\ADSessionFactory.cs");
			return ADSessionFactory.CreateWrapper(session);
		}
	}
}
