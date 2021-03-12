using System;
using System.Linq;
using System.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000DC RID: 220
	[Serializable]
	internal class FfoTenantRecipientSession : FfoRecipientSession, ITenantRecipientSession, IRecipientSession, IDirectorySession, IConfigDataProvider
	{
		// Token: 0x06000860 RID: 2144 RVA: 0x0001B039 File Offset: 0x00019239
		public FfoTenantRecipientSession(bool useConfigNC, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings) : base(useConfigNC, readOnly, consistencyMode, networkCredential, sessionSettings)
		{
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0001B048 File Offset: 0x00019248
		public FfoTenantRecipientSession(ADObjectId tenantId) : base(tenantId)
		{
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0001B051 File Offset: 0x00019251
		ADRawEntry ITenantRecipientSession.ChooseBetweenAmbiguousUsers(ADRawEntry[] entries)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0001B05A File Offset: 0x0001925A
		ADObjectId ITenantRecipientSession.ChooseBetweenAmbiguousUsers(ADObjectId user1Id, ADObjectId user2Id)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000864 RID: 2148 RVA: 0x0001B063 File Offset: 0x00019263
		public DirectoryBackendType DirectoryBackendType
		{
			get
			{
				return DirectoryBackendType.SQL;
			}
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0001B066 File Offset: 0x00019266
		ADRawEntry[] ITenantRecipientSession.FindByNetID(string netID, string organizationContext, params PropertyDefinition[] properties)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return new ADRawEntry[0];
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0001B074 File Offset: 0x00019274
		ADRawEntry[] ITenantRecipientSession.FindByNetID(string netID, params PropertyDefinition[] properties)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return new ADRawEntry[0];
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0001B082 File Offset: 0x00019282
		MiniRecipient ITenantRecipientSession.FindRecipientByNetID(NetID netId)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0001B08B File Offset: 0x0001928B
		ADRawEntry ITenantRecipientSession.FindUniqueEntryByNetID(string netID, string organizationContext, params PropertyDefinition[] properties)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0001B094 File Offset: 0x00019294
		ADRawEntry ITenantRecipientSession.FindUniqueEntryByNetID(string netID, params PropertyDefinition[] properties)
		{
			ComparisonFilter filter = new ComparisonFilter(ComparisonOperator.Equal, IADSecurityPrincipalSchema.NetID, new NetID(netID));
			return ((IConfigDataProvider)this).Find<ADUser>(filter, null, false, null).Cast<ADUser>().FirstOrDefault<ADUser>();
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0001B0C7 File Offset: 0x000192C7
		ADRawEntry ITenantRecipientSession.FindByLiveIdMemberName(string liveIdMemberName, params PropertyDefinition[] properties)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0001B0D0 File Offset: 0x000192D0
		Result<ADRawEntry>[] ITenantRecipientSession.ReadMultipleByLinkedPartnerId(LinkedPartnerGroupInformation[] entryIds, params PropertyDefinition[] properties)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return new Result<ADRawEntry>[0];
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0001B0DE File Offset: 0x000192DE
		Result<ADRawEntry>[] ITenantRecipientSession.FindByExternalDirectoryObjectIds(string[] externalDirectoryObjectIds, params PropertyDefinition[] properties)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return new Result<ADRawEntry>[0];
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x0001B0EC File Offset: 0x000192EC
		Result<ADRawEntry>[] ITenantRecipientSession.FindByExternalDirectoryObjectIds(string[] externalDirectoryObjectIds, bool includeDeletedObjects, params PropertyDefinition[] properties)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return new Result<ADRawEntry>[0];
		}
	}
}
