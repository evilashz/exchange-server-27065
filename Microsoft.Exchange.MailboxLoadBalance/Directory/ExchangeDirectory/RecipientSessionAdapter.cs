using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance.Directory.ExchangeDirectory
{
	// Token: 0x02000085 RID: 133
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class RecipientSessionAdapter
	{
		// Token: 0x060004FA RID: 1274
		public abstract IEnumerable<LoadBalancingMiniRecipient> FindAllUsersLinkedToDatabase(ADObjectId databaseId);

		// Token: 0x060004FB RID: 1275
		public abstract LoadBalancingMiniRecipient FindRecipient(DirectoryIdentity identity);

		// Token: 0x060004FC RID: 1276
		public abstract Guid GetExternalDirectoryOrganizationId(LoadBalancingMiniRecipient recipient);

		// Token: 0x04000195 RID: 405
		public static readonly Hookable<RecipientSessionAdapter> Instance = Hookable<RecipientSessionAdapter>.Create(true, new RecipientSessionAdapter.ADDriverRecipientSessionAdapter());

		// Token: 0x02000086 RID: 134
		private sealed class ADDriverRecipientSessionAdapter : RecipientSessionAdapter
		{
			// Token: 0x060004FF RID: 1279 RVA: 0x0000C9C2 File Offset: 0x0000ABC2
			public override IEnumerable<LoadBalancingMiniRecipient> FindAllUsersLinkedToDatabase(ADObjectId databaseId)
			{
				return PartitionDataAggregator.FindAllUsersLinkedToDatabase<LoadBalancingMiniRecipient>(databaseId);
			}

			// Token: 0x06000500 RID: 1280 RVA: 0x0000C9CC File Offset: 0x0000ABCC
			public override Guid GetExternalDirectoryOrganizationId(LoadBalancingMiniRecipient recipient)
			{
				if (recipient.OrganizationId == OrganizationId.ForestWideOrgId)
				{
					return TenantPartitionHint.ExternalDirectoryOrganizationIdForRootOrg;
				}
				string text;
				try
				{
					text = recipient.OrganizationId.ToExternalDirectoryOrganizationId();
				}
				catch (InvalidOperationException ex)
				{
					string extraData = string.Format("OrgId: {0}, recipientGuid: {1}, database: {2}", recipient.OrganizationId.GetTenantGuid(), recipient.ExchangeObjectId, recipient.Database);
					ExWatson.SendReport(ex, ReportOptions.None, extraData);
					throw new InvalidOrganizationException(recipient.OrganizationId.ToString(), ex);
				}
				Guid result;
				if (!Guid.TryParse(text, out result))
				{
					throw new InvalidOrganizationIdentityException(string.Format("{0}", recipient.OrganizationId), text);
				}
				return result;
			}

			// Token: 0x06000501 RID: 1281 RVA: 0x0000CA78 File Offset: 0x0000AC78
			public override LoadBalancingMiniRecipient FindRecipient(DirectoryIdentity identity)
			{
				if (identity == null)
				{
					throw new ArgumentNullException("identity", "DirectoryIdentity should not be null");
				}
				ADSessionSettings sessionSettings;
				if (identity.OrganizationId == Guid.Empty)
				{
					sessionSettings = ADSessionSettings.FromRootOrgScopeSet();
				}
				else
				{
					sessionSettings = ADSessionSettings.FromExternalDirectoryOrganizationId(identity.OrganizationId);
				}
				IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, sessionSettings, 129, "FindRecipient", "f:\\15.00.1497\\sources\\dev\\mrs\\src\\MailboxLoadBalance\\Directory\\ExchangeDirectory\\RecipientSessionAdapter.cs");
				QueryFilter queryFilter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Id, identity.ADObjectId);
				QueryFilter queryFilter2 = new ComparisonFilter(ComparisonOperator.Equal, MiniRecipientSchema.ArchiveGuid, identity.Guid);
				QueryFilter queryFilter3 = new ComparisonFilter(ComparisonOperator.Equal, MiniRecipientSchema.ExchangeGuid, identity.Guid);
				QueryFilter queryFilter4 = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ExchangeObjectId, identity.Guid);
				QueryFilter filter = QueryFilter.OrTogether(new QueryFilter[]
				{
					queryFilter,
					queryFilter2,
					queryFilter3,
					queryFilter4
				});
				LoadBalancingMiniRecipient[] array = tenantOrRootOrgRecipientSession.Find<LoadBalancingMiniRecipient>(null, QueryScope.SubTree, filter, null, 2);
				if (array == null || array.Length == 0)
				{
					throw new RecipientNotFoundException(identity.ToString());
				}
				if (array.Length > 1)
				{
					throw new MultipleRecipientFoundException(identity.ToString());
				}
				return array[0];
			}
		}
	}
}
