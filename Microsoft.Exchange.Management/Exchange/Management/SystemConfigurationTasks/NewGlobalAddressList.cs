using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020007DB RID: 2011
	[Cmdlet("New", "GlobalAddressList", SupportsShouldProcess = true, DefaultParameterSetName = "PrecannedFilter")]
	public sealed class NewGlobalAddressList : NewAddressBookBase
	{
		// Token: 0x17001554 RID: 5460
		// (get) Token: 0x0600467A RID: 18042 RVA: 0x00120C8C File Offset: 0x0011EE8C
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewGlobalAddressList(base.Name.ToString());
			}
		}

		// Token: 0x0600467B RID: 18043 RVA: 0x00120C9E File Offset: 0x0011EE9E
		protected override ADObjectId GetContainerId()
		{
			return base.CurrentOrgContainerId.GetDescendantId(GlobalAddressList.RdnGalContainerToOrganization);
		}

		// Token: 0x0600467C RID: 18044 RVA: 0x00120CB0 File Offset: 0x0011EEB0
		private void PostNewAddressBookBase()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject
			});
			if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).CmdletInfra.GlobalAddressListAttrbutes.Enabled)
			{
				bool flag = true;
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 625, "PostNewAddressBookBase", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\AddressBook\\NewAddressBook.cs");
				bool skipRangedAttributes = tenantOrTopologyConfigurationSession.SkipRangedAttributes;
				tenantOrTopologyConfigurationSession.SkipRangedAttributes = false;
				try
				{
					ExchangeConfigurationContainerWithAddressLists exchangeConfigurationContainerWithAddressLists = tenantOrTopologyConfigurationSession.GetExchangeConfigurationContainerWithAddressLists();
					try
					{
						if (exchangeConfigurationContainerWithAddressLists.LinkedAddressBookRootAttributesPresent())
						{
							AddressBookUtilities.SyncGlobalAddressLists(exchangeConfigurationContainerWithAddressLists, new Task.TaskWarningLoggingDelegate(this.WriteWarning));
							exchangeConfigurationContainerWithAddressLists.DefaultGlobalAddressList2.Add(this.DataObject.Id);
						}
						tenantOrTopologyConfigurationSession.Save(exchangeConfigurationContainerWithAddressLists);
						exchangeConfigurationContainerWithAddressLists.ResetChangeTracking();
						flag = false;
						if (!AddressBookUtilities.IsTenantAddressList(tenantOrTopologyConfigurationSession, this.DataObject.Id))
						{
							try
							{
								exchangeConfigurationContainerWithAddressLists.DefaultGlobalAddressList.Add(this.DataObject.Id);
								tenantOrTopologyConfigurationSession.Save(exchangeConfigurationContainerWithAddressLists);
							}
							catch (AdminLimitExceededException innerException)
							{
								if (!exchangeConfigurationContainerWithAddressLists.LinkedAddressBookRootAttributesPresent())
								{
									throw new ADOperationException(Strings.ErrorTooManyGALsCreated, innerException);
								}
								this.WriteWarning(Strings.WarningTooManyLegacyGALsCreated);
							}
						}
					}
					catch (DataSourceTransientException exception)
					{
						base.WriteError(exception, ErrorCategory.WriteError, this.DataObject.Id);
					}
				}
				finally
				{
					tenantOrTopologyConfigurationSession.SkipRangedAttributes = skipRangedAttributes;
					if (flag)
					{
						try
						{
							base.DataSession.Delete(this.DataObject);
						}
						catch (DataSourceTransientException ex)
						{
							TaskLogger.Trace("Exception is raised in rollback action (deleting the new Global Address List object): {0}", new object[]
							{
								ex.ToString()
							});
						}
					}
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600467D RID: 18045 RVA: 0x00120E6C File Offset: 0x0011F06C
		protected override void WriteResult(IConfigurable result)
		{
			TaskLogger.LogEnter(new object[]
			{
				result.Identity
			});
			this.PostNewAddressBookBase();
			base.WriteResult(new GlobalAddressList((AddressBookBase)result));
			TaskLogger.LogExit();
		}

		// Token: 0x17001555 RID: 5461
		// (get) Token: 0x0600467E RID: 18046 RVA: 0x00120EAB File Offset: 0x0011F0AB
		protected override string ClonableTypeName
		{
			get
			{
				return typeof(GlobalAddressList).FullName;
			}
		}

		// Token: 0x0600467F RID: 18047 RVA: 0x00120EBC File Offset: 0x0011F0BC
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return GlobalAddressList.FromDataObject((AddressBookBase)dataObject);
		}
	}
}
