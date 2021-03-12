using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.SnapIn;
using Microsoft.Exchange.ManagementGUI;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000C5 RID: 197
	public abstract class ObjectPicker : ObjectPickerBase
	{
		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x00016984 File Offset: 0x00014B84
		public static string ObjectName
		{
			get
			{
				return ADObjectSchema.Name.Name;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x00016990 File Offset: 0x00014B90
		internal static string ObjectClass
		{
			get
			{
				return ADObjectSchema.ObjectClass.Name;
			}
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0001699C File Offset: 0x00014B9C
		static ObjectPicker()
		{
			IconLibrary.IconReferenceCollection icons = ObjectPicker.ObjectClassIconLibrary.Icons;
			icons.Add("AddressListPicker", Icons.AddressList);
			icons.Add("AcceptedDomainPicker", Icons.AcceptedDomain);
			icons.Add("RemoteDomainPicker", Icons.RemoteDomain);
			icons.Add("UMAutoAttendantPicker", Icons.AutoAttendant);
			icons.Add("DAGNetworkPicker", Icons.DAGNetwork);
			icons.Add("UMDialPlanPicker", Icons.DialPlan);
			icons.Add("msExchDynamicDistributionList", Icons.DynamicDL);
			icons.Add("ElcMailboxPolicyPicker", Icons.ELCMailboxPolicy);
			icons.Add("ExchangeServerPicker", Icons.ExchangeServer);
			icons.Add("MailboxDatabasePicker", Icons.MailboxDatabase);
			icons.Add("MobileMailboxPolicyPicker", Icons.AirSyncMailboxPolicy);
			icons.Add("OabVirtualDirectoryPicker", Icons.OfflineAddressBookDistributionPoint);
			icons.Add("OfflineAddressBookPicker", Icons.OfflineAddressList);
			icons.Add("PublicFolderDatabasePicker", Icons.PublicFolderDatabase);
			icons.Add("UMMailboxPolicyPicker", Icons.UMMailboxPolicy);
			icons.Add("DatabaseAvailabilityGroupPicker", Icons.DatabaseAvailabilityGroup);
			icons.Add("OwaMailboxPolicyPicker", Icons.OWAMailboxPolicy);
			icons.Add("MailboxServerPicker", Icons.MailboxServer);
			icons.Add(typeof(ADOwaVirtualDirectory).Name, Icons.OWAVirtualDirectory);
			icons.Add(typeof(ADEcpVirtualDirectory).Name, Icons.EcpVirtualDirectory);
			icons.Add(typeof(ADWebServicesVirtualDirectory).Name, Icons.OWAVirtualDirectory);
			icons.Add(typeof(ADMobileVirtualDirectory).Name, Icons.AirSyncVirtualDirectory);
			icons.Add(typeof(ADAutodiscoverVirtualDirectory).Name, Icons.OWAVirtualDirectory);
			icons.Add(typeof(ADOabVirtualDirectory).Name, Icons.OfflineAddressBookDistributionPoint);
			icons.Add(CertificateIconType.ValidCertificate, Icons.CertificateValid);
			icons.Add(CertificateIconType.InvalidCertificate, Icons.CertificateInValid);
			icons.Add(CertificateIconType.CertificateRequest, Icons.CertificateRequest);
			icons.Add(CertificateIconType.AboutToExpiredCertificate, Icons.CertificateAboutToExpire);
			icons.Add(ElcFolderType.All, Icons.ELCFoldersAllMailboxFolders);
			icons.Add(ElcFolderType.Calendar, Icons.ELCFoldersCalendar);
			icons.Add(ElcFolderType.Contacts, Icons.ELCFoldersContacts);
			icons.Add(ElcFolderType.DeletedItems, Icons.ELCFoldersDeletedItems);
			icons.Add(ElcFolderType.Drafts, Icons.ELCFoldersDrafts);
			icons.Add(ElcFolderType.Inbox, Icons.ELCFoldersInbox);
			icons.Add(ElcFolderType.Journal, Icons.ELCFoldersJournal);
			icons.Add(ElcFolderType.JunkEmail, Icons.ELCFoldersJunkEmail);
			icons.Add(ElcFolderType.Notes, Icons.ELCFoldersNotes);
			icons.Add(ElcFolderType.ManagedCustomFolder, Icons.ELCFoldersCustomOrgFolder);
			icons.Add(ElcFolderType.Outbox, Icons.ELCFoldersOutbox);
			icons.Add(ElcFolderType.SentItems, Icons.ELCFoldersSentItems);
			icons.Add(ElcFolderType.Tasks, Icons.ELCFoldersTasks);
			icons.Add(ElcFolderType.RssSubscriptions, Icons.ELCFoldersRSSSubscriptions);
			icons.Add(ElcFolderType.SyncIssues, Icons.ELCFoldersSyncIssues);
			icons.Add(ElcFolderType.ConversationHistory, Icons.ELCFoldersConversationHistory);
			icons.Add(RecipientTypeDetails.UserMailbox, Icons.Mailbox);
			icons.Add(RecipientTypeDetails.LegacyMailbox, Icons.LegacyMailbox);
			icons.Add(RecipientTypeDetails.LinkedMailbox, Icons.LinkedMailbox);
			icons.Add(RecipientTypeDetails.SharedMailbox, Icons.SharedMailbox);
			icons.Add(RecipientTypeDetails.TeamMailbox, Icons.SharedMailbox);
			icons.Add(RecipientTypeDetails.EquipmentMailbox, Icons.EquipmentMailbox);
			icons.Add(RecipientTypeDetails.RoomMailbox, Icons.ConferenceRoomMailbox);
			icons.Add(RecipientTypeDetails.MailForestContact, Icons.MailForestContact);
			icons.Add(RecipientTypeDetails.MailUser, Icons.MailUser);
			icons.Add(RecipientTypeDetails.PublicFolder, Icons.MailEnabledPublicFolder);
			icons.Add(RecipientTypeDetails.MailContact, Icons.MailEnabledContact);
			icons.Add(RecipientTypeDetails.MailUniversalDistributionGroup, Icons.DistributionGroup);
			icons.Add(RecipientTypeDetails.MailUniversalSecurityGroup, Icons.MailEnabledUniversalSecurityGroup);
			icons.Add(RecipientTypeDetails.MailNonUniversalGroup, Icons.MailEnabledNonUniversalGroup);
			icons.Add(RecipientTypeDetails.DynamicDistributionGroup, Icons.DynamicDL);
			icons.Add(RecipientTypeDetails.DiscoveryMailbox, Icons.DiscoveryMailbox);
			icons.Add(RecipientTypeDetails.User, Icons.User);
			icons.Add(RecipientTypeDetails.DisabledUser, Icons.UserDisabled);
			icons.Add(RecipientTypeDetails.Contact, Icons.Contact);
			icons.Add(RecipientTypeDetails.UniversalDistributionGroup, Icons.UniversalDistributionGroup);
			icons.Add(RecipientTypeDetails.UniversalSecurityGroup, Icons.UniversalSecurityGroup);
			icons.Add(RecipientTypeDetails.RoleGroup, Icons.UniversalSecurityGroup);
			icons.Add(RecipientTypeDetails.MicrosoftExchange, Icons.MicrosoftExchange);
			icons.Add((RecipientTypeDetails)((ulong)int.MinValue), Icons.RemoteMailbox);
			icons.Add(RecipientTypeDetails.RemoteRoomMailbox, Icons.RemoteMailbox);
			icons.Add(RecipientTypeDetails.RemoteEquipmentMailbox, Icons.RemoteMailbox);
			icons.Add(RecipientTypeDetails.RemoteSharedMailbox, Icons.RemoteMailbox);
			icons.Add(RecipientTypeDetails.RemoteTeamMailbox, Icons.RemoteMailbox);
			icons.Add(GatewayStatus.Enabled, Icons.IPGateway);
			icons.Add(GatewayStatus.Disabled, Icons.IPGatewayDisabled);
			icons.Add(GatewayStatus.NoNewCalls, Icons.IPGatewayDisabled);
			icons.Add(RecipientTypeDetails.NonUniversalGroup, Icons.NonUniversalGroup);
			icons.Add(RecipientTypeDetails.None, Icons.RecipientTypeDetailsNone);
			icons.Add(SecurityPrincipalType.Group, Icons.UniversalSecurityGroup);
			icons.Add(SecurityPrincipalType.Computer, Icons.ExchangeServer);
			icons.Add(SecurityPrincipalType.WellknownSecurityPrincipal, Icons.SecurityPrincipal);
			icons.Add("organizationalUnit", Icons.OrganizationalUnit);
			icons.Add("container", Icons.OrganizationalUnit);
			icons.Add("builtinDomain", Icons.OrganizationalUnit);
			icons.Add("msExchSystemObjectsContainer", Icons.OrganizationalUnit);
			icons.Add("DeletedUser", Icons.DeletedUser);
			icons.Add("domainDNS", Icons.Domain);
			icons.Add("SharingPolicyPicker", Icons.FederatedSharingMailboxSetting);
			icons.Add("RetentionPolicyPicker", Icons.RetentionPolicy);
			icons.Add("RetentionPolicyTagPicker", Icons.RetentionPolicyTag);
			icons.Add("RoleAssignmentPolicyPicker", Icons.RoleAssignmentPolicy);
			icons.Add(ItemLoadStatus.Loading, Icons.Loading);
			icons.Add(ItemLoadStatus.Failed, Icons.Error);
			icons.Add("ArchivedMailbox", Icons.ArchiveMailbox);
			icons.Add("MailboxPlanPicker", Icons.MailboxPlan);
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x000170B8 File Offset: 0x000152B8
		internal ResultsLoaderProfile ObjectPickerProfile
		{
			get
			{
				return this.objectPickerProfile;
			}
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x000170C0 File Offset: 0x000152C0
		protected ObjectPicker() : this(null)
		{
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x000170CC File Offset: 0x000152CC
		protected ObjectPicker(ResultsLoaderProfile profile)
		{
			this.ResetUseTreeViewForm();
			this.ResetScopeSupportingLevel();
			if (profile != null)
			{
				this.UpdateResultsLoaderProfile(profile);
				this.objectPickerProfile = profile;
				this.scopeSupportingLevel = this.objectPickerProfile.ScopeSupportingLevel;
				this.useTreeViewForm = this.objectPickerProfile.UseTreeViewForm;
			}
			this.DataTableLoader = this.CreateDataTableLoader();
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00017160 File Offset: 0x00015360
		private void UpdateResultsLoaderProfile(ResultsLoaderProfile profile)
		{
			ResultsColumnProfile[] displayedColumnCollection = profile.DisplayedColumnCollection;
			for (int i = 0; i < displayedColumnCollection.Length; i++)
			{
				ResultsColumnProfile displayColumnProfile = displayedColumnCollection[i];
				DataColumn dataColumn = profile.DataTable.Columns.OfType<DataColumn>().First((DataColumn column) => column.ColumnName == displayColumnProfile.Name);
				if (dataColumn.DataType != typeof(string))
				{
					string text = dataColumn.ColumnName + "_FilterSupport_";
					DataColumn dataColumn2 = new DataColumn(text, typeof(string));
					dataColumn2.ExtendedProperties["LambdaExpression"] = string.Format("{0}=>WinformsHelper.ConvertValueToString(@0.Table.Columns[\"{0}\"], @0[\"{0}\"])", dataColumn.ColumnName);
					profile.DataTable.Columns.Add(dataColumn2);
					displayColumnProfile.Name = text;
					displayColumnProfile.SortMode = SortMode.DelegateColumn;
					dataColumn2.ExtendedProperties["DelegateColumnName"] = dataColumn.ColumnName;
				}
			}
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00017268 File Offset: 0x00015468
		protected ObjectPicker(string profileName) : this(ObjectPicker.ProfileLoader.GetProfile(profileName))
		{
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0001727B File Offset: 0x0001547B
		protected ObjectPicker(IResultsLoaderConfiguration config) : this(config.BuildResultsLoaderProfile())
		{
		}

		// Token: 0x06000658 RID: 1624
		protected abstract DataTableLoader CreateDataTableLoader();

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000659 RID: 1625
		public abstract string ObjectClassDisplayName { get; }

		// Token: 0x0600065A RID: 1626
		public abstract void PerformQuery(object rootId, string searchText);

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x00017289 File Offset: 0x00015489
		protected virtual bool DefaultUseTreeViewForm
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x0001728C File Offset: 0x0001548C
		protected virtual ScopeSupportingLevel DefaultScopeSupportingLevel
		{
			get
			{
				return ScopeSupportingLevel.NoScoping;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x0001728F File Offset: 0x0001548F
		protected virtual string DefaultCaption
		{
			get
			{
				return Strings.DefaultCaption(this.ObjectClassDisplayName);
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x000172A1 File Offset: 0x000154A1
		protected virtual string DefaultNoResultsLabelText
		{
			get
			{
				return Strings.NoItemsToSelect;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x000172AD File Offset: 0x000154AD
		public virtual string IdentityProperty
		{
			get
			{
				return ObjectPicker.ObjectName;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x000172B4 File Offset: 0x000154B4
		public virtual string ImageProperty
		{
			get
			{
				return ObjectPicker.ObjectClass;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x000172BB File Offset: 0x000154BB
		public virtual string NameProperty
		{
			get
			{
				return ObjectPicker.ObjectName;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000662 RID: 1634 RVA: 0x000172C2 File Offset: 0x000154C2
		public virtual string DefaultSortProperty
		{
			get
			{
				return this.NameProperty;
			}
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x000172CC File Offset: 0x000154CC
		public virtual ExchangeColumnHeader[] CreateColumnHeaders()
		{
			return new List<ExchangeColumnHeader>
			{
				new ExchangeColumnHeader(ObjectPicker.ObjectName, Strings.Name)
			}.ToArray();
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00017300 File Offset: 0x00015500
		public override DataTable CreateResultsDataTable()
		{
			DataTable dataTable = base.CreateResultsDataTable();
			dataTable.Columns.Add(ObjectPicker.ObjectClass, typeof(string));
			DataColumn dataColumn = dataTable.Columns.Add(ObjectPicker.ObjectName);
			dataColumn.Unique = true;
			dataTable.PrimaryKey = new DataColumn[]
			{
				dataColumn
			};
			return dataTable;
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0001735C File Offset: 0x0001555C
		protected override DataTable GetSelectedObjects(IntPtr hwndOwner)
		{
			this.ResetScopeSetting();
			DataTable result;
			using (Form form = this.CreateObjectPickerForm())
			{
				if (base.Container != null)
				{
					base.Container.Add(form, form.Name + form.GetHashCode());
				}
				IUIService iuiservice = (IUIService)this.GetService(typeof(IUIService));
				if (iuiservice == null)
				{
					iuiservice = new UIService(new Win32Window(hwndOwner));
				}
				DataTable dataTable = null;
				if (DialogResult.OK == iuiservice.ShowDialog(form))
				{
					DataTable selectedObjects = ((ISelectedObjectsProvider)form).SelectedObjects;
					if (this.ObjectPickerProfile == null)
					{
						dataTable = ObjectPicker.RemoveNonRequiredColumns(selectedObjects);
					}
					else
					{
						dataTable = selectedObjects;
					}
				}
				if (base.Container != null)
				{
					base.Container.Remove(form);
				}
				result = dataTable;
			}
			return result;
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00017428 File Offset: 0x00015628
		internal virtual DataTableLoader CreateDataTableLoaderForResolver()
		{
			DataTableLoader dataTableLoader = this.CreateDataTableLoader();
			if (dataTableLoader.Table != null && this.ObjectPickerProfile == null)
			{
				this.MarkNonOptionalColumnsAsRequiredColumn(dataTableLoader.Table);
				ObjectPicker.RemoveNonRequiredColumns(dataTableLoader.Table);
			}
			if (dataTableLoader.RefreshArgument != null)
			{
				dataTableLoader.RefreshArgument = (ICloneable)dataTableLoader.ResultsLoaderProfile.CloneWithSharedInputTable();
			}
			return dataTableLoader;
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x00017483 File Offset: 0x00015683
		// (set) Token: 0x06000668 RID: 1640 RVA: 0x0001748C File Offset: 0x0001568C
		[Browsable(false)]
		public DataTableLoader DataTableLoader
		{
			get
			{
				return this.dataTableLoader;
			}
			protected set
			{
				if (this.DataTableLoader != value)
				{
					this.dataTableLoader = value;
					if (this.DataTableLoader != null)
					{
						if (this.DataTableLoader.Table == null)
						{
							this.DataTableLoader.Table = this.CreateResultsDataTable();
						}
						if (this.ObjectPickerProfile == null)
						{
							this.MarkNonOptionalColumnsAsRequiredColumn(this.DataTableLoader.Table);
						}
					}
				}
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x000174E8 File Offset: 0x000156E8
		// (set) Token: 0x0600066A RID: 1642 RVA: 0x000174F0 File Offset: 0x000156F0
		[DefaultValue(true)]
		public virtual bool ShowListItemIcon
		{
			get
			{
				return this.showListItemIcon;
			}
			set
			{
				this.showListItemIcon = value;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x000174F9 File Offset: 0x000156F9
		// (set) Token: 0x0600066C RID: 1644 RVA: 0x00017501 File Offset: 0x00015701
		[DefaultValue(true)]
		public bool SupportSearch
		{
			get
			{
				return this.supportSearch;
			}
			set
			{
				this.supportSearch = value;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x0001750A File Offset: 0x0001570A
		internal bool SupportModifyScope
		{
			get
			{
				return this.scopeSupportingLevel != ScopeSupportingLevel.NoScoping;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x00017518 File Offset: 0x00015718
		internal bool ShouldScopingWithinDefaultDomainScope
		{
			get
			{
				return this.ScopeSupportingLevel == ScopeSupportingLevel.WithinDefaultScope && !ADServerSettingsSingleton.GetInstance().ADServerSettings.ForestViewEnabled;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x00017537 File Offset: 0x00015737
		// (set) Token: 0x06000670 RID: 1648 RVA: 0x0001753F File Offset: 0x0001573F
		public ScopeSupportingLevel ScopeSupportingLevel
		{
			get
			{
				return this.scopeSupportingLevel;
			}
			set
			{
				this.scopeSupportingLevel = value;
				if (this.ObjectPickerProfile != null)
				{
					this.ObjectPickerProfile.ScopeSupportingLevel = value;
				}
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x0001755C File Offset: 0x0001575C
		// (set) Token: 0x06000672 RID: 1650 RVA: 0x00017564 File Offset: 0x00015764
		[DefaultValue(null)]
		public ScopeSettings DefaultScopeSettings
		{
			get
			{
				return this.defaultScopeSettings;
			}
			set
			{
				this.defaultScopeSettings = value;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x00017570 File Offset: 0x00015770
		[DefaultValue(null)]
		public ScopeSettings ScopeSettings
		{
			get
			{
				if (this.scopeSettings == null)
				{
					if (this.DefaultScopeSettings != null)
					{
						this.scopeSettings = new ScopeSettings();
						this.scopeSettings.CopyFrom(this.DefaultScopeSettings);
					}
					else
					{
						this.scopeSettings = new ScopeSettings(ADServerSettingsSingleton.GetInstance().ADServerSettings);
					}
				}
				return this.scopeSettings;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x000175C6 File Offset: 0x000157C6
		// (set) Token: 0x06000675 RID: 1653 RVA: 0x000175CE File Offset: 0x000157CE
		public bool UseTreeViewForm
		{
			get
			{
				return this.useTreeViewForm;
			}
			set
			{
				this.useTreeViewForm = value;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x000175D7 File Offset: 0x000157D7
		// (set) Token: 0x06000677 RID: 1655 RVA: 0x000175DF File Offset: 0x000157DF
		[DefaultValue(true)]
		public bool CanSelectRootObject
		{
			get
			{
				return this.canSelectRootObject;
			}
			set
			{
				this.canSelectRootObject = value;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x000175E8 File Offset: 0x000157E8
		// (set) Token: 0x06000679 RID: 1657 RVA: 0x00017604 File Offset: 0x00015804
		public string Caption
		{
			get
			{
				if (string.IsNullOrEmpty(this.caption))
				{
					return this.DefaultCaption;
				}
				return this.caption;
			}
			set
			{
				this.caption = value;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x0001760D File Offset: 0x0001580D
		// (set) Token: 0x0600067B RID: 1659 RVA: 0x00017629 File Offset: 0x00015829
		public string NoResultsLabelText
		{
			get
			{
				if (string.IsNullOrEmpty(this.noResultsLabelText))
				{
					return this.DefaultNoResultsLabelText;
				}
				return this.noResultsLabelText;
			}
			set
			{
				this.noResultsLabelText = value;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x00017632 File Offset: 0x00015832
		public static IconLibrary ObjectClassIconLibrary
		{
			get
			{
				return ObjectPicker.objectClassIconLibrary;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x00017639 File Offset: 0x00015839
		// (set) Token: 0x0600067E RID: 1662 RVA: 0x00017655 File Offset: 0x00015855
		public string HelpTopic
		{
			get
			{
				if (this.helpTopic == null)
				{
					this.helpTopic = this.DefaultHelpTopic;
				}
				return this.helpTopic;
			}
			set
			{
				value = (value ?? "");
				this.helpTopic = value;
			}
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0001766A File Offset: 0x0001586A
		private bool ShouldSerializeHelpTopic()
		{
			return this.HelpTopic != this.DefaultHelpTopic;
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0001767D File Offset: 0x0001587D
		private void ResetHelpTopic()
		{
			this.HelpTopic = this.DefaultHelpTopic;
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x0001768B File Offset: 0x0001588B
		protected virtual string DefaultHelpTopic
		{
			get
			{
				if (this.ObjectPickerProfile == null)
				{
					return base.GetType().FullName;
				}
				return this.ObjectPickerProfile.HelpTopic;
			}
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x000176AC File Offset: 0x000158AC
		private bool ShouldSerializeUseTreeViewForm()
		{
			return this.UseTreeViewForm != this.DefaultUseTreeViewForm;
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x000176BF File Offset: 0x000158BF
		private void ResetUseTreeViewForm()
		{
			this.UseTreeViewForm = this.DefaultUseTreeViewForm;
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x000176CD File Offset: 0x000158CD
		private bool ShouldSerializeScopeSupportingLevel()
		{
			return this.ScopeSupportingLevel != this.DefaultScopeSupportingLevel;
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x000176E0 File Offset: 0x000158E0
		private void ResetScopeSupportingLevel()
		{
			this.ScopeSupportingLevel = this.DefaultScopeSupportingLevel;
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x000176EE File Offset: 0x000158EE
		private bool ShouldSerializeCaption()
		{
			return this.Caption != this.DefaultCaption;
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x00017701 File Offset: 0x00015901
		private void ResetCaption()
		{
			this.Caption = this.DefaultCaption;
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0001770F File Offset: 0x0001590F
		private bool ShouldSerializeNoResultsLabelText()
		{
			return this.NoResultsLabelText != this.DefaultNoResultsLabelText;
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x00017722 File Offset: 0x00015922
		private void ResetNoResultsLabelText()
		{
			this.NoResultsLabelText = this.DefaultNoResultsLabelText;
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x00017730 File Offset: 0x00015930
		private void ResetScopeSetting()
		{
			this.scopeSettings = null;
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0001773C File Offset: 0x0001593C
		private Form CreateObjectPickerForm()
		{
			Form result;
			if (this.UseTreeViewForm)
			{
				result = new TreeViewObjectPickerForm(this);
			}
			else
			{
				result = new ObjectPickerForm(this);
			}
			return result;
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x00017764 File Offset: 0x00015964
		private void MarkNonOptionalColumnsAsRequiredColumn(DataTable dataTable)
		{
			bool[] array = new bool[dataTable.Columns.Count];
			foreach (ExchangeColumnHeader exchangeColumnHeader in this.CreateColumnHeaders())
			{
				if (!exchangeColumnHeader.Default && dataTable.Columns.Contains(exchangeColumnHeader.Name))
				{
					int ordinal = dataTable.Columns[exchangeColumnHeader.Name].Ordinal;
					array[ordinal] = true;
				}
			}
			foreach (object obj in dataTable.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				if (!array[dataColumn.Ordinal])
				{
					ObjectPicker.SetIsRequiredDataColumnFlag(dataColumn, true);
				}
			}
			foreach (DataColumn column in dataTable.PrimaryKey)
			{
				ObjectPicker.SetIsRequiredDataColumnFlag(column, true);
			}
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00017860 File Offset: 0x00015A60
		private static DataTable RemoveNonRequiredColumns(DataTable table)
		{
			for (int i = table.Columns.Count - 1; i >= 0; i--)
			{
				DataColumn column = table.Columns[i];
				if (!ObjectPicker.GetIsRequiredDataColumnFlag(column))
				{
					table.Columns.Remove(column);
				}
			}
			return table;
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x000178A7 File Offset: 0x00015AA7
		internal static bool GetIsRequiredDataColumnFlag(DataColumn column)
		{
			if (column == null)
			{
				throw new ArgumentNullException("column");
			}
			return column.ExtendedProperties.ContainsKey(ObjectPicker.RequiredDataColumnFlag);
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x000178C7 File Offset: 0x00015AC7
		internal static void SetIsRequiredDataColumnFlag(DataColumn column, bool isRequiredDataColumn)
		{
			if (column == null)
			{
				throw new ArgumentNullException("column");
			}
			if (ObjectPicker.GetIsRequiredDataColumnFlag(column) != isRequiredDataColumn)
			{
				if (isRequiredDataColumn)
				{
					column.ExtendedProperties.Add(ObjectPicker.RequiredDataColumnFlag, null);
					return;
				}
				column.ExtendedProperties.Remove(ObjectPicker.RequiredDataColumnFlag);
			}
		}

		// Token: 0x04000212 RID: 530
		internal const string FilterKeyName = "_FilterSupport_";

		// Token: 0x04000213 RID: 531
		private static readonly string RequiredDataColumnFlag = "required";

		// Token: 0x04000214 RID: 532
		private bool useTreeViewForm;

		// Token: 0x04000215 RID: 533
		private bool showListItemIcon = true;

		// Token: 0x04000216 RID: 534
		private bool supportSearch = true;

		// Token: 0x04000217 RID: 535
		private string caption;

		// Token: 0x04000218 RID: 536
		private string noResultsLabelText;

		// Token: 0x04000219 RID: 537
		private DataTableLoader dataTableLoader;

		// Token: 0x0400021A RID: 538
		private ScopeSupportingLevel scopeSupportingLevel;

		// Token: 0x0400021B RID: 539
		private ScopeSettings scopeSettings;

		// Token: 0x0400021C RID: 540
		private ResultsLoaderProfile objectPickerProfile;

		// Token: 0x0400021D RID: 541
		private static ObjectPickerProfileLoader ProfileLoader = new ObjectPickerProfileLoader();

		// Token: 0x0400021E RID: 542
		private ScopeSettings defaultScopeSettings;

		// Token: 0x0400021F RID: 543
		private bool canSelectRootObject = true;

		// Token: 0x04000220 RID: 544
		private static IconLibrary objectClassIconLibrary = new IconLibrary();

		// Token: 0x04000221 RID: 545
		private string helpTopic;
	}
}
