using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.HA.DirectoryServices
{
	// Token: 0x0200001D RID: 29
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ADSessionFactory
	{
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00004629 File Offset: 0x00002829
		private static IADSessionFactory Default
		{
			get
			{
				if (ADSessionFactory.defaultInstance == null)
				{
					ADSessionFactory.defaultInstance = new ADSessionWrapperFactoryImpl();
				}
				return ADSessionFactory.defaultInstance;
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00004641 File Offset: 0x00002841
		internal static void SetTestADSessionFactory(IADSessionFactory testADSessionFactory)
		{
			ADSessionFactory.defaultInstance = testADSessionFactory;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00004649 File Offset: 0x00002849
		public static IADToplogyConfigurationSession CreateIgnoreInvalidRootOrgSession(bool readOnly = true)
		{
			return ADSessionFactory.Default.CreateIgnoreInvalidRootOrgSession(readOnly);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00004656 File Offset: 0x00002856
		public static IADToplogyConfigurationSession CreatePartiallyConsistentRootOrgSession(bool readOnly = true)
		{
			return ADSessionFactory.Default.CreatePartiallyConsistentRootOrgSession(readOnly);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00004663 File Offset: 0x00002863
		public static IADToplogyConfigurationSession CreateFullyConsistentRootOrgSession(bool readOnly = true)
		{
			return ADSessionFactory.Default.CreateFullyConsistentRootOrgSession(readOnly);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00004670 File Offset: 0x00002870
		public static IADRootOrganizationRecipientSession CreateIgnoreInvalidRootOrgRecipientSession()
		{
			return ADSessionFactory.Default.CreateIgnoreInvalidRootOrgRecipientSession();
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000467C File Offset: 0x0000287C
		public static IADToplogyConfigurationSession CreateWrapper(ITopologyConfigurationSession session)
		{
			return ADTopologyConfigurationSessionWrapper.CreateWrapper(session);
		}

		// Token: 0x0400008C RID: 140
		private static IADSessionFactory defaultInstance;
	}
}
