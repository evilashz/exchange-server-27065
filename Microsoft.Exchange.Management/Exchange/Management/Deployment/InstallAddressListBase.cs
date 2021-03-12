using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001DB RID: 475
	[ClassAccessLevel(AccessLevel.Consumer)]
	public abstract class InstallAddressListBase : InstallAddressBookContainer
	{
		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06001067 RID: 4199 RVA: 0x00048C02 File Offset: 0x00046E02
		protected override ADObjectId RdnContainerToOrganization
		{
			get
			{
				return AddressList.RdnAlContainerToOrganization;
			}
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x00048C0C File Offset: 0x00046E0C
		protected void PostExchange(ADObjectId alContainer)
		{
			IConfigurationSession configurationSession = base.CreateGlobalWritableConfigSession();
			bool skipRangedAttributes = configurationSession.SkipRangedAttributes;
			configurationSession.SkipRangedAttributes = true;
			try
			{
				ExchangeConfigurationContainerWithAddressLists exchangeConfigurationContainerWithAddressLists = configurationSession.GetExchangeConfigurationContainerWithAddressLists();
				if (exchangeConfigurationContainerWithAddressLists.LinkedAddressBookRootAttributesPresent())
				{
					exchangeConfigurationContainerWithAddressLists.AddressBookRoots2.Add(alContainer);
				}
				base.WriteVerbose(TaskVerboseStringHelper.GetSaveObjectVerboseString(exchangeConfigurationContainerWithAddressLists, configurationSession, typeof(ExchangeConfigurationContainer)));
				configurationSession.Save(exchangeConfigurationContainerWithAddressLists);
				exchangeConfigurationContainerWithAddressLists.ResetChangeTracking();
				if (!AddressBookUtilities.IsTenantAddressList(configurationSession, alContainer))
				{
					exchangeConfigurationContainerWithAddressLists.AddressBookRoots.Add(alContainer);
					base.WriteVerbose(TaskVerboseStringHelper.GetSaveObjectVerboseString(exchangeConfigurationContainerWithAddressLists, configurationSession, typeof(ExchangeConfigurationContainer)));
					configurationSession.Save(exchangeConfigurationContainerWithAddressLists);
				}
			}
			finally
			{
				configurationSession.SkipRangedAttributes = skipRangedAttributes;
				base.WriteVerbose(TaskVerboseStringHelper.GetSourceVerboseString(configurationSession));
			}
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x00048CC8 File Offset: 0x00046EC8
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || CannedAddressListsFilterHelper.IsKnownException(exception);
		}
	}
}
