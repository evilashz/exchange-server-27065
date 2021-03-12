using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage.RightsManagement;
using Microsoft.Exchange.Diagnostics;
using Microsoft.RightsManagementServices.Core;
using Microsoft.RightsManagementServices.Provider;

namespace Microsoft.Exchange.Data.Storage.OfflineRms
{
	// Token: 0x02000ABE RID: 2750
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class DirectoryServiceProvider : IDirectoryServiceProvider
	{
		// Token: 0x06006430 RID: 25648 RVA: 0x001A82EB File Offset: 0x001A64EB
		public DirectoryServiceProvider()
		{
		}

		// Token: 0x06006431 RID: 25649 RVA: 0x001A82F3 File Offset: 0x001A64F3
		public DirectoryServiceProvider(RmsClientManagerContext clientContext)
		{
			if (clientContext == null)
			{
				throw new ArgumentNullException("clientContext");
			}
			this.clientContext = clientContext;
		}

		// Token: 0x06006432 RID: 25650 RVA: 0x001A8310 File Offset: 0x001A6510
		public bool EvaluateIdentity(string userAddress, string[] targetAddresses, out string matchedAddress)
		{
			if (string.IsNullOrEmpty(userAddress))
			{
				throw new ArgumentNullException("userAddress");
			}
			if (targetAddresses == null)
			{
				throw new ArgumentNullException("targetAddresses");
			}
			matchedAddress = null;
			string recipientAddress = DirectoryServiceProvider.RmsIdentityAddressToSmtpAddress(userAddress);
			try
			{
				foreach (string text in targetAddresses)
				{
					if (!string.IsNullOrEmpty(text))
					{
						string groupAddress = DirectoryServiceProvider.RmsIdentityAddressToSmtpAddress(text);
						if (ServerManager.IsMemberOf(this.clientContext, recipientAddress, groupAddress))
						{
							matchedAddress = text;
							return true;
						}
					}
				}
			}
			catch (TransientException ex)
			{
				throw new DirectoryServiceProviderException(false, "Failed to look up " + userAddress, ex);
			}
			catch (ADOperationException ex2)
			{
				throw new DirectoryServiceProviderException(false, "Failed to look up " + userAddress, ex2);
			}
			return false;
		}

		// Token: 0x06006433 RID: 25651 RVA: 0x001A83D8 File Offset: 0x001A65D8
		public bool UserMatchesAnyoneIdentity(string userAddress)
		{
			if (string.IsNullOrEmpty(userAddress))
			{
				throw new ArgumentNullException("userAddress");
			}
			return DirectoryServiceProvider.IsUserMemberOfTenant(DirectoryServiceProvider.RmsIdentityAddressToSmtpAddress(userAddress), this.clientContext.OrgId);
		}

		// Token: 0x06006434 RID: 25652 RVA: 0x001A8404 File Offset: 0x001A6604
		private static bool IsUserMemberOfTenant(string userAddress, OrganizationId organizationId)
		{
			bool result;
			try
			{
				result = ServerManager.IsUserMemberOfTenant(userAddress, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId));
			}
			catch (NonUniqueRecipientException ex)
			{
				throw new DirectoryServiceProviderException(false, "User email address is not unique: " + userAddress, ex);
			}
			return result;
		}

		// Token: 0x06006435 RID: 25653 RVA: 0x001A8448 File Offset: 0x001A6648
		private static string RmsIdentityAddressToSmtpAddress(string rmsAddress)
		{
			rmsAddress = rmsAddress.Trim();
			if (rmsAddress.IndexOf("mail=", StringComparison.OrdinalIgnoreCase) == 0)
			{
				rmsAddress = rmsAddress.Substring("mail=".Length);
			}
			return rmsAddress.Trim();
		}

		// Token: 0x040038D6 RID: 14550
		private readonly RmsClientManagerContext clientContext;
	}
}
