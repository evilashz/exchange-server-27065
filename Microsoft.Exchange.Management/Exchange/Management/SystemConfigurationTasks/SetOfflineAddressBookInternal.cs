using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020001CB RID: 459
	public class SetOfflineAddressBookInternal : SetSystemConfigurationObjectTask<OfflineAddressBookIdParameter, OfflineAddressBook>
	{
		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06000FF2 RID: 4082 RVA: 0x000475FA File Offset: 0x000457FA
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetOfflineAddressBook(this.Identity.ToString());
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06000FF3 RID: 4083 RVA: 0x0004760C File Offset: 0x0004580C
		// (set) Token: 0x06000FF4 RID: 4084 RVA: 0x00047623 File Offset: 0x00045823
		[Parameter]
		public MailboxIdParameter GeneratingMailbox
		{
			get
			{
				return (MailboxIdParameter)base.Fields["GeneratingMailbox"];
			}
			set
			{
				base.Fields["GeneratingMailbox"] = value;
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06000FF5 RID: 4085 RVA: 0x00047636 File Offset: 0x00045836
		// (set) Token: 0x06000FF6 RID: 4086 RVA: 0x0004764D File Offset: 0x0004584D
		[Parameter]
		[ValidateNotNullOrEmpty]
		public AddressBookBaseIdParameter[] AddressLists
		{
			get
			{
				return (AddressBookBaseIdParameter[])base.Fields["AddressLists"];
			}
			set
			{
				base.Fields["AddressLists"] = value;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06000FF7 RID: 4087 RVA: 0x00047660 File Offset: 0x00045860
		// (set) Token: 0x06000FF8 RID: 4088 RVA: 0x00047677 File Offset: 0x00045877
		[Parameter]
		public VirtualDirectoryIdParameter[] VirtualDirectories
		{
			get
			{
				return (VirtualDirectoryIdParameter[])base.Fields["VirtualDirectories"];
			}
			set
			{
				base.Fields["VirtualDirectories"] = value;
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06000FF9 RID: 4089 RVA: 0x0004768A File Offset: 0x0004588A
		// (set) Token: 0x06000FFA RID: 4090 RVA: 0x000476B0 File Offset: 0x000458B0
		[Parameter]
		public SwitchParameter ApplyMandatoryProperties
		{
			get
			{
				return (SwitchParameter)(base.Fields["ApplyMandatoryProperties"] ?? false);
			}
			set
			{
				base.Fields["ApplyMandatoryProperties"] = value;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06000FFB RID: 4091 RVA: 0x000476C8 File Offset: 0x000458C8
		// (set) Token: 0x06000FFC RID: 4092 RVA: 0x000476EE File Offset: 0x000458EE
		[Parameter]
		public SwitchParameter UseDefaultAttributes
		{
			get
			{
				return (SwitchParameter)(base.Fields["UseDefaultAttributes"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["UseDefaultAttributes"] = value;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06000FFD RID: 4093 RVA: 0x00047706 File Offset: 0x00045906
		protected override bool ExchangeVersionUpgradeSupported
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x0004770C File Offset: 0x0004590C
		protected override bool ShouldUpgradeExchangeVersion(ADObject adObject)
		{
			return this.ApplyMandatoryProperties.IsPresent;
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x00047728 File Offset: 0x00045928
		protected override void UpgradeExchangeVersion(ADObject adObject)
		{
			if (!this.DataObject.IsE15OrLater())
			{
				return;
			}
			OfflineAddressBook offlineAddressBook = (OfflineAddressBook)adObject;
			Server server = (Server)base.GetDataObject<Server>(new ServerIdParameter(offlineAddressBook.Server), base.GlobalConfigSession, null, new LocalizedString?(Strings.ErrorServerNotFound(offlineAddressBook.Server.Name.ToString())), new LocalizedString?(Strings.ErrorServerNotUnique(offlineAddressBook.Server.Name.ToString())));
			if (server.IsE14OrLater)
			{
				offlineAddressBook.ExchangeVersion = ExchangeObjectVersion.Exchange2010;
				return;
			}
			if (server.IsExchange2007OrLater)
			{
				offlineAddressBook.ExchangeVersion = ExchangeObjectVersion.Exchange2007;
				return;
			}
			offlineAddressBook.ExchangeVersion = ExchangeObjectVersion.Exchange2003;
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x000477D0 File Offset: 0x000459D0
		protected override void StampChangesOn(IConfigurable dataObject)
		{
			bool flag = ((OfflineAddressBook)dataObject).Versions.Contains(OfflineAddressBookVersion.Version1);
			this.isOriginalPfDistributionEnabled = ((OfflineAddressBook)dataObject).PublicFolderDistributionEnabled;
			base.StampChangesOn(dataObject);
			bool flag2 = ((OfflineAddressBook)dataObject).Versions.Contains(OfflineAddressBookVersion.Version1);
			this.isPresentPfDistributionEnabled = ((OfflineAddressBook)dataObject).PublicFolderDistributionEnabled;
			OfflineAddressBook offlineAddressBook = (OfflineAddressBook)dataObject;
			for (int i = 0; i < SetOfflineAddressBookInternal.propertiesCannotBeSet.GetLength(0); i++)
			{
				if (offlineAddressBook.IsModified(SetOfflineAddressBookInternal.propertiesCannotBeSet[i, 0]))
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorSpecifiedPropertyCannotBeSet((SetOfflineAddressBookInternal.propertiesCannotBeSet[i, 1] ?? SetOfflineAddressBookInternal.propertiesCannotBeSet[i, 0]).ToString())), ErrorCategory.InvalidOperation, this.Identity);
				}
			}
			if (!flag && flag2)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorSetVersion1OfExchange12OAB), ErrorCategory.InvalidOperation, offlineAddressBook.Identity);
			}
			if (this.GeneratingMailbox != null)
			{
				if (offlineAddressBook.ExchangeVersion.IsOlderThan(ExchangeObjectVersion.Exchange2012))
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorLegacyOABDoesNotSupportLinkedMailbox), ErrorCategory.InvalidOperation, this.Identity);
				}
				offlineAddressBook.GeneratingMailbox = OfflineAddressBookTaskUtility.ValidateGeneratingMailbox(base.TenantGlobalCatalogSession, this.GeneratingMailbox, new OfflineAddressBookTaskUtility.GetUniqueObject(base.GetDataObject<ADUser>), offlineAddressBook, new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			if (this.AddressLists != null && this.AddressLists.Length > 0)
			{
				offlineAddressBook.AddressLists = OfflineAddressBookTaskUtility.ValidateAddressBook(base.DataSession, this.AddressLists, new OfflineAddressBookTaskUtility.GetUniqueObject(base.GetDataObject<AddressBookBase>), offlineAddressBook, new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			else if (offlineAddressBook.IsModified(OfflineAddressBookSchema.AddressLists))
			{
				List<AddressBookBaseIdParameter> list = new List<AddressBookBaseIdParameter>();
				if (offlineAddressBook.AddressLists != null && offlineAddressBook.AddressLists.Added.Length != 0)
				{
					foreach (ADObjectId adObjectId in offlineAddressBook.AddressLists.Added)
					{
						list.Add(new AddressBookBaseIdParameter(adObjectId));
					}
					OfflineAddressBookTaskUtility.ValidateAddressBook(base.DataSession, list.ToArray(), new OfflineAddressBookTaskUtility.GetUniqueObject(base.GetDataObject<AddressBookBase>), offlineAddressBook, new Task.TaskErrorLoggingDelegate(base.WriteError));
				}
			}
			if (base.Fields.IsModified("VirtualDirectories"))
			{
				if (this.VirtualDirectories != null && this.VirtualDirectories.Length > 0)
				{
					offlineAddressBook.VirtualDirectories = OfflineAddressBookTaskUtility.ValidateVirtualDirectory(base.GlobalConfigSession, this.VirtualDirectories, new OfflineAddressBookTaskUtility.GetUniqueObject(base.GetDataObject<ADOabVirtualDirectory>), offlineAddressBook, new Task.TaskErrorLoggingDelegate(base.WriteError));
				}
				else
				{
					offlineAddressBook.VirtualDirectories = null;
				}
			}
			else if (offlineAddressBook.IsModified(OfflineAddressBookSchema.VirtualDirectories))
			{
				if (offlineAddressBook.VirtualDirectories != null && offlineAddressBook.VirtualDirectories.Added.Length > 0)
				{
					List<VirtualDirectoryIdParameter> list2 = new List<VirtualDirectoryIdParameter>();
					foreach (ADObjectId adObjectId2 in offlineAddressBook.VirtualDirectories.Added)
					{
						list2.Add(new VirtualDirectoryIdParameter(adObjectId2));
					}
					OfflineAddressBookTaskUtility.ValidateVirtualDirectory(base.GlobalConfigSession, list2.ToArray(), new OfflineAddressBookTaskUtility.GetUniqueObject(base.GetDataObject<ADOabVirtualDirectory>), offlineAddressBook, new Task.TaskErrorLoggingDelegate(base.WriteError));
				}
				else
				{
					offlineAddressBook.VirtualDirectories = null;
				}
			}
			bool flag3 = false;
			if (this.isOriginalPfDistributionEnabled ^ this.isPresentPfDistributionEnabled)
			{
				if (offlineAddressBook.PublicFolderDatabase != null)
				{
					IEnumerable<PublicFolderDatabase> objects = new DatabaseIdParameter(offlineAddressBook.PublicFolderDatabase)
					{
						AllowLegacy = true
					}.GetObjects<PublicFolderDatabase>(null, base.GlobalConfigSession);
					using (IEnumerator<PublicFolderDatabase> enumerator = objects.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							this.publicFolderDatabase = enumerator.Current;
							if (enumerator.MoveNext())
							{
								this.publicFolderDatabase = null;
							}
							else
							{
								flag3 = true;
							}
						}
					}
				}
				if (!flag3)
				{
					OfflineAddressBookTaskUtility.ValidatePublicFolderInfrastructure(base.GlobalConfigSession, new Task.TaskErrorLoggingDelegate(base.WriteError), new Task.TaskWarningLoggingDelegate(this.WriteWarning), this.isPresentPfDistributionEnabled);
				}
			}
			if (this.UseDefaultAttributes.IsPresent)
			{
				if (offlineAddressBook.ExchangeVersion.IsOlderThan(ExchangeObjectVersion.Exchange2010))
				{
					base.WriteError(new InvalidOperationException(Strings.CannotUseDefaultAttributesForExchange2007OAB), ErrorCategory.InvalidOperation, this.Identity);
				}
				offlineAddressBook.ConfiguredAttributes = OfflineAddressBookMapiProperty.DefaultOABPropertyList;
			}
			if (!this.isOriginalPfDistributionEnabled && this.isPresentPfDistributionEnabled && !flag3)
			{
				this.publicFolderDatabase = OfflineAddressBookTaskUtility.FindPublicFolderDatabase(base.GlobalConfigSession, offlineAddressBook.Server, new Task.TaskErrorLoggingDelegate(base.WriteError));
				offlineAddressBook.PublicFolderDatabase = (ADObjectId)this.publicFolderDatabase.Identity;
			}
			if (offlineAddressBook.IsChanged(OfflineAddressBookSchema.VirtualDirectories) || this.isOriginalPfDistributionEnabled != this.isPresentPfDistributionEnabled)
			{
				OfflineAddressBookTaskUtility.WarnForNoDistribution(offlineAddressBook, new Task.TaskWarningLoggingDelegate(this.WriteWarning));
			}
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x00047C94 File Offset: 0x00045E94
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (this.DataObject.IsChanged(OfflineAddressBookSchema.IsDefault) && !this.DataObject.IsDefault)
			{
				base.WriteError(new InvalidOperationException(Strings.CannotResetDefaultOAB), ErrorCategory.InvalidOperation, this.Identity);
			}
			if (this.DataObject.IsModified(OfflineAddressBookSchema.ConfiguredAttributes))
			{
				try
				{
					this.DataObject.UpdateRawMapiAttributes(false);
				}
				catch (ArgumentException exception)
				{
					base.WriteError(exception, ErrorCategory.InvalidArgument, this.DataObject);
				}
			}
			this.ThrowErrorIfUnsupportedParameterIsSpecified();
			TaskLogger.LogExit();
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x00047D34 File Offset: 0x00045F34
		protected override void InternalProcessRecord()
		{
			OfflineAddressBook offlineAddressBook = null;
			if (this.DataObject.IsChanged(OfflineAddressBookSchema.IsDefault) && this.DataObject.IsDefault)
			{
				offlineAddressBook = OfflineAddressBookTaskUtility.ResetOldDefaultOab(base.DataSession, new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			bool flag = false;
			try
			{
				base.InternalProcessRecord();
				flag = true;
				if (!this.isOriginalPfDistributionEnabled && this.isPresentPfDistributionEnabled)
				{
					this.WriteWarning(Strings.DoNotMoveImmediately(this.DataObject.Name));
					OfflineAddressBookTaskUtility.DoMaintenanceTask(this.publicFolderDatabase, base.DomainController, new Task.TaskWarningLoggingDelegate(this.WriteWarning));
				}
			}
			finally
			{
				if (!flag && offlineAddressBook != null)
				{
					offlineAddressBook.IsDefault = true;
					try
					{
						base.DataSession.Save(offlineAddressBook);
					}
					catch (DataSourceTransientException exception)
					{
						this.WriteError(exception, ErrorCategory.WriteError, null, false);
					}
				}
			}
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x00047E18 File Offset: 0x00046018
		private void ThrowErrorIfUnsupportedParameterIsSpecified()
		{
			if (!this.DataObject.IsE15OrLater())
			{
				return;
			}
			if (this.ApplyMandatoryProperties.IsPresent)
			{
				this.WriteError(new InvalidOperationException(Strings.CannotSpecifyApplyMandatoryPropertiesParameterWithE15OrLaterOab), ErrorCategory.InvalidOperation, this.Identity, true);
			}
			foreach (ADPropertyDefinition adpropertyDefinition in SetOfflineAddressBookInternal.ParametersUnsupportedForOAB15OrLater)
			{
				if (this.DataObject.IsModified(adpropertyDefinition))
				{
					this.WriteError(new InvalidOperationException(Strings.CannotSpecifyParameterWithE15OrLaterOab(adpropertyDefinition.Name)), ErrorCategory.InvalidOperation, this.Identity, true);
				}
			}
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x00047EAC File Offset: 0x000460AC
		// Note: this type is marked as 'beforefieldinit'.
		static SetOfflineAddressBookInternal()
		{
			ADPropertyDefinition[,] array = new ADPropertyDefinition[3, 2];
			array[0, 0] = ADConfigurationObjectSchema.AdminDisplayName;
			array[1, 0] = OfflineAddressBookSchema.Server;
			array[2, 0] = OfflineAddressBookSchema.PublicFolderDatabase;
			SetOfflineAddressBookInternal.propertiesCannotBeSet = array;
		}

		// Token: 0x0400075D RID: 1885
		private static readonly ADPropertyDefinition[] ParametersUnsupportedForOAB15OrLater = new ADPropertyDefinition[]
		{
			OfflineAddressBookSchema.Schedule,
			OfflineAddressBookSchema.Versions
		};

		// Token: 0x0400075E RID: 1886
		private static readonly ADPropertyDefinition[,] propertiesCannotBeSet;

		// Token: 0x0400075F RID: 1887
		private bool isOriginalPfDistributionEnabled;

		// Token: 0x04000760 RID: 1888
		private bool isPresentPfDistributionEnabled;

		// Token: 0x04000761 RID: 1889
		private PublicFolderDatabase publicFolderDatabase;
	}
}
