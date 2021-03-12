using System;
using System.Security.Principal;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000116 RID: 278
	internal static class DirectoryAccessorExtension
	{
		// Token: 0x06000916 RID: 2326 RVA: 0x0003BC44 File Offset: 0x00039E44
		public static MiniRecipient[] GetSlaveAccounts(this IExecuter directoryAccessor, SecurityIdentifier masterAccountSid, OrganizationId tenantOrganizationId, PropertyDefinition[] miniRecipientProperties)
		{
			MiniRecipient[] miniRecipients = null;
			directoryAccessor.Execute(delegate
			{
				IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(tenantOrganizationId), 40, "GetSlaveAccounts", "f:\\15.00.1497\\sources\\dev\\Security\\src\\Authentication\\TokenMunger\\DirectoryAccessorExtension.cs");
				QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, MiniRecipientSchema.MasterAccountSid, masterAccountSid);
				miniRecipients = tenantOrRootOrgRecipientSession.FindMiniRecipient(null, QueryScope.SubTree, filter, null, 2, miniRecipientProperties);
			});
			return miniRecipients;
		}
	}
}
