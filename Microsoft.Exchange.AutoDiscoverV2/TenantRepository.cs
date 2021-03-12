using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.Exchange.Autodiscover;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.AutoDiscoverV2
{
	// Token: 0x02000013 RID: 19
	[ExcludeFromCodeCoverage]
	internal class TenantRepository : ITenantRepository
	{
		// Token: 0x0600005C RID: 92 RVA: 0x00002D86 File Offset: 0x00000F86
		public TenantRepository(RequestDetailsLogger logger)
		{
			this.logger = logger;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002D98 File Offset: 0x00000F98
		public ADRecipient GetOnPremUser(SmtpAddress emailAddress)
		{
			IRecipientSession recipientSession = DirectorySessionFactory.Default.CreateRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 49, "GetOnPremUser", "f:\\15.00.1497\\sources\\dev\\autodisc\\src\\AutoDiscoverV2\\TenantRepository.cs");
			this.logger.AppendGenericInfo("GetOnPremUser", "Start Ad lookup");
			return recipientSession.FindByProxyAddress(new SmtpProxyAddress(emailAddress.Address, false));
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002DEC File Offset: 0x00000FEC
		public IAutodMiniRecipient GetNextUserFromSortedList(SmtpAddress emailAddress)
		{
			IRecipientSession recipientSession = DirectorySessionFactory.Default.CreateRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 66, "GetNextUserFromSortedList", "f:\\15.00.1497\\sources\\dev\\autodisc\\src\\AutoDiscoverV2\\TenantRepository.cs");
			this.logger.AppendGenericInfo("GetOnPremUser", "Start Ad lookup");
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				new OrFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetails, RecipientTypeDetails.UserMailbox),
					new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetails, RecipientTypeDetails.MailUser)
				}),
				new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ADUserSchema.UserPrincipalName, emailAddress.Address)
			});
			ADRawEntry[] array = recipientSession.Find(null, QueryScope.SubTree, filter, null, 1, new PropertyDefinition[]
			{
				ADUserSchema.UserPrincipalName,
				ADRecipientSchema.ExternalEmailAddress
			});
			if (array != null)
			{
				ADRawEntry adrawEntry = array.FirstOrDefault<ADRawEntry>();
				if (adrawEntry != null)
				{
					return new AutodMiniRecipient(adrawEntry);
				}
			}
			return null;
		}

		// Token: 0x04000021 RID: 33
		private readonly RequestDetailsLogger logger;
	}
}
