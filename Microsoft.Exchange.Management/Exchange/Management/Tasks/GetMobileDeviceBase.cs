using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.AirSync;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.AirSync;
using Microsoft.Exchange.Management.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200006C RID: 108
	public class GetMobileDeviceBase<TIdentity, TDataObject> : GetMultitenancySystemConfigurationObjectTask<TIdentity, TDataObject> where TIdentity : IIdentityParameter where TDataObject : ADObject, new()
	{
		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0000DACE File Offset: 0x0000BCCE
		// (set) Token: 0x0600034A RID: 842 RVA: 0x0000DAD6 File Offset: 0x0000BCD6
		[Parameter]
		public Unlimited<uint> ResultSize
		{
			get
			{
				return base.InternalResultSize;
			}
			set
			{
				base.InternalResultSize = value;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0000DADF File Offset: 0x0000BCDF
		// (set) Token: 0x0600034C RID: 844 RVA: 0x0000DAF6 File Offset: 0x0000BCF6
		[Parameter(Mandatory = true, ParameterSetName = "Mailbox", ValueFromPipeline = true)]
		[ValidateNotNullOrEmpty]
		public MailboxIdParameter Mailbox
		{
			get
			{
				return (MailboxIdParameter)base.Fields["Mailbox"];
			}
			set
			{
				base.Fields["Mailbox"] = value;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600034D RID: 845 RVA: 0x0000DB09 File Offset: 0x0000BD09
		// (set) Token: 0x0600034E RID: 846 RVA: 0x0000DB20 File Offset: 0x0000BD20
		[Parameter(Mandatory = false)]
		public OrganizationalUnitIdParameter OrganizationalUnit
		{
			get
			{
				return (OrganizationalUnitIdParameter)base.Fields["OrganizationalUnit"];
			}
			set
			{
				base.Fields["OrganizationalUnit"] = value;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600034F RID: 847 RVA: 0x0000DB33 File Offset: 0x0000BD33
		// (set) Token: 0x06000350 RID: 848 RVA: 0x0000DB59 File Offset: 0x0000BD59
		[Parameter(Mandatory = false)]
		public SwitchParameter ActiveSync
		{
			get
			{
				return (SwitchParameter)(base.Fields["ActiveSync"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ActiveSync"] = value;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000351 RID: 849 RVA: 0x0000DB71 File Offset: 0x0000BD71
		// (set) Token: 0x06000352 RID: 850 RVA: 0x0000DB97 File Offset: 0x0000BD97
		[Parameter(Mandatory = false)]
		public SwitchParameter OWAforDevices
		{
			get
			{
				return (SwitchParameter)(base.Fields["OWAforDevices"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["OWAforDevices"] = value;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000353 RID: 851 RVA: 0x0000DBAF File Offset: 0x0000BDAF
		// (set) Token: 0x06000354 RID: 852 RVA: 0x0000DBC6 File Offset: 0x0000BDC6
		[Parameter]
		public string SortBy
		{
			get
			{
				return (string)base.Fields["SortBy"];
			}
			set
			{
				base.Fields["SortBy"] = (string.IsNullOrEmpty(value) ? null : value);
				this.internalSortBy = QueryHelper.GetSortBy(this.SortBy, GetMobileDeviceBase<TIdentity, TDataObject>.SortPropertiesArray);
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000355 RID: 853 RVA: 0x0000DBFA File Offset: 0x0000BDFA
		// (set) Token: 0x06000356 RID: 854 RVA: 0x0000DC14 File Offset: 0x0000BE14
		[Parameter]
		[ValidateNotNullOrEmpty]
		public string Filter
		{
			get
			{
				return (string)base.Fields["Filter"];
			}
			set
			{
				MonadFilter monadFilter = new MonadFilter(value, this, GetMobileDeviceBase<TIdentity, TDataObject>.FilterableObjectSchema);
				this.inputFilter = monadFilter.InnerFilter;
				if (!this.IsFilteringByDeviceAccess())
				{
					base.OptionalIdentityData.AdditionalFilter = monadFilter.InnerFilter;
				}
				base.Fields["Filter"] = value;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0000DC64 File Offset: 0x0000BE64
		// (set) Token: 0x06000358 RID: 856 RVA: 0x0000DC8A File Offset: 0x0000BE8A
		[Parameter(Mandatory = false)]
		public SwitchParameter Monitoring
		{
			get
			{
				return (SwitchParameter)(base.Fields["Monitoring"] ?? false);
			}
			set
			{
				base.Fields["Monitoring"] = value;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0000DCA2 File Offset: 0x0000BEA2
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600035A RID: 858 RVA: 0x0000DCA8 File Offset: 0x0000BEA8
		protected override ObjectId RootId
		{
			get
			{
				if (MobileDeviceTaskHelper.IsRunningUnderMyOptionsRole(this, base.TenantGlobalCatalogSession, base.SessionSettings))
				{
					ADObjectId result;
					if (!base.TryGetExecutingUserId(out result))
					{
						throw new ExecutingUserPropertyNotFoundException("executingUserid");
					}
					return result;
				}
				else
				{
					if (this.user != null)
					{
						return this.user.Id;
					}
					if (this.organizationalUnit != null)
					{
						return this.organizationalUnit.Id;
					}
					if (base.CurrentOrganizationId != null && base.CurrentOrganizationId != OrganizationId.ForestWideOrgId)
					{
						return base.CurrentOrganizationId.OrganizationalUnit;
					}
					return base.RootId;
				}
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600035B RID: 859 RVA: 0x0000DD3A File Offset: 0x0000BF3A
		protected override QueryFilter InternalFilter
		{
			get
			{
				return this.ConstructQueryFilterWithCustomFilter(this.IsFilteringByDeviceAccess() ? null : this.inputFilter);
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600035C RID: 860 RVA: 0x0000DD53 File Offset: 0x0000BF53
		protected override SortBy InternalSortBy
		{
			get
			{
				return this.internalSortBy;
			}
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000DD5C File Offset: 0x0000BF5C
		protected override IConfigDataProvider CreateSession()
		{
			IConfigurationSession configurationSession;
			if (!MobileDeviceTaskHelper.IsRunningUnderMyOptionsRole(this, base.TenantGlobalCatalogSession, base.SessionSettings))
			{
				configurationSession = (IConfigurationSession)base.CreateSession();
			}
			else
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
				configurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, sessionSettings, 296, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\AirSync\\GetMobileDevice.cs");
			}
			configurationSession.UseConfigNC = false;
			configurationSession.UseGlobalCatalog = (base.DomainController == null && base.ServerSettings.ViewEntireForest);
			return configurationSession;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000DDF0 File Offset: 0x0000BFF0
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			this.FindUser();
			this.FindOrganizationalUnit();
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000DE04 File Offset: 0x0000C004
		protected override void WriteResult(IConfigurable dataObject)
		{
			MobileDevice mobileDevice = dataObject as MobileDevice;
			if (!this.ShouldShowMobileDevice(mobileDevice))
			{
				return;
			}
			ADObjectId orgContainerId = this.ConfigurationSession.GetOrgContainerId();
			for (ADObjectId adobjectId = mobileDevice.Id; adobjectId != null; adobjectId = adobjectId.Parent)
			{
				if (ADObjectId.Equals(adobjectId, orgContainerId))
				{
					return;
				}
			}
			bool flag = string.Equals("EASProbeDeviceType", mobileDevice.DeviceType, StringComparison.OrdinalIgnoreCase);
			if (this.Monitoring)
			{
				if (!flag)
				{
					return;
				}
			}
			else
			{
				if (flag)
				{
					return;
				}
				DeviceAccessState deviceAccessState = DeviceAccessState.Unknown;
				DeviceAccessStateReason deviceAccessStateReason = DeviceAccessStateReason.Unknown;
				ADObjectId deviceAccessControlRule = null;
				bool flag2 = false;
				if (mobileDevice != null && mobileDevice.OrganizationId != OrganizationId.ForestWideOrgId && (mobileDevice.DeviceAccessStateReason != DeviceAccessStateReason.Individual || mobileDevice.DeviceAccessState != DeviceAccessState.Blocked) && mobileDevice.DeviceAccessStateReason != DeviceAccessStateReason.Policy && mobileDevice.DeviceAccessStateReason < DeviceAccessStateReason.UserAgentsChanges)
				{
					Command.DetermineDeviceAccessState(this.LoadAbq(OrganizationId.ForestWideOrgId), mobileDevice.DeviceType, mobileDevice.DeviceModel, mobileDevice.DeviceUserAgent, mobileDevice.DeviceOS, out deviceAccessState, out deviceAccessStateReason, out deviceAccessControlRule);
					if (deviceAccessState == DeviceAccessState.Blocked)
					{
						mobileDevice.DeviceAccessState = deviceAccessState;
						mobileDevice.DeviceAccessStateReason = deviceAccessStateReason;
						mobileDevice.DeviceAccessControlRule = deviceAccessControlRule;
						flag2 = true;
						if (this.IsFilteringByDeviceAccess() && !this.IsInFilter(mobileDevice))
						{
							return;
						}
					}
				}
				if (!flag2 && mobileDevice != null && mobileDevice.DeviceAccessState != DeviceAccessState.DeviceDiscovery && mobileDevice.DeviceAccessStateReason != DeviceAccessStateReason.Individual && mobileDevice.DeviceAccessStateReason != DeviceAccessStateReason.Upgrade && mobileDevice.DeviceAccessStateReason != DeviceAccessStateReason.Policy && mobileDevice.DeviceAccessStateReason < DeviceAccessStateReason.UserAgentsChanges)
				{
					Command.DetermineDeviceAccessState(this.LoadAbq(mobileDevice.OrganizationId), mobileDevice.DeviceType, mobileDevice.DeviceModel, mobileDevice.DeviceUserAgent, mobileDevice.DeviceOS, out deviceAccessState, out deviceAccessStateReason, out deviceAccessControlRule);
					mobileDevice.DeviceAccessState = deviceAccessState;
					mobileDevice.DeviceAccessStateReason = deviceAccessStateReason;
					mobileDevice.DeviceAccessControlRule = deviceAccessControlRule;
				}
			}
			if (base.Fields["ActiveSync"] == null && base.Fields["OWAforDevices"] == null)
			{
				base.Fields["ActiveSync"] = new SwitchParameter(true);
				base.Fields["OWAforDevices"] = new SwitchParameter(true);
			}
			else if (base.Fields["ActiveSync"] == null)
			{
				if (this.OWAforDevices == false)
				{
					base.Fields["ActiveSync"] = new SwitchParameter(true);
				}
			}
			else if (base.Fields["OWAforDevices"] == null && this.ActiveSync == false)
			{
				base.Fields["OWAforDevices"] = new SwitchParameter(true);
			}
			if ((!this.ActiveSync || mobileDevice.ClientType != MobileClientType.EAS) && (!this.OWAforDevices || mobileDevice.ClientType != MobileClientType.MOWA))
			{
				return;
			}
			if (mobileDevice.ClientType == MobileClientType.MOWA)
			{
				MobileDevice mobileDevice2 = mobileDevice;
				string format = Strings.MOWADeviceTypePrefix;
				string arg;
				if ((arg = mobileDevice.DeviceType) == null)
				{
					arg = (mobileDevice.DeviceModel ?? mobileDevice.DeviceOS);
				}
				mobileDevice2.DeviceType = string.Format(format, arg);
			}
			if (this.IsFilteringByDeviceAccess() && !this.IsInFilter(mobileDevice))
			{
				return;
			}
			base.WriteResult(dataObject);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000E0F0 File Offset: 0x0000C2F0
		private bool IsInFilter(MobileDevice device)
		{
			if (this.inputFilter == null)
			{
				throw new InvalidOperationException("The inputFilter should not be null in this case");
			}
			if (device != null)
			{
				try
				{
					return OpathFilterEvaluator.FilterMatches(this.inputFilter, device);
				}
				catch (FilterOnlyAttributesException)
				{
					return true;
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000E13C File Offset: 0x0000C33C
		private void FindUser()
		{
			if (this.Mailbox == null)
			{
				return;
			}
			this.Mailbox.SearchWithDisplayName = false;
			IEnumerable<ADRecipient> objects = this.Mailbox.GetObjects<ADRecipient>(null, base.TenantGlobalCatalogSession);
			using (IEnumerator<ADRecipient> enumerator = objects.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					this.user = (enumerator.Current as ADUser);
					if (enumerator.MoveNext())
					{
						base.WriteError(new RecipientNotUniqueException(this.Mailbox.ToString()), ErrorCategory.InvalidArgument, null);
					}
					if (this.user == null || (this.user.RecipientType != RecipientType.UserMailbox && this.user.RecipientType != RecipientType.MailUser))
					{
						base.WriteError(new RecipientNotValidException(this.Mailbox.ToString()), ErrorCategory.InvalidArgument, null);
					}
					base.CurrentOrganizationId = this.user.OrganizationId;
				}
				else
				{
					base.WriteError(new RecipientNotFoundException(this.Mailbox.ToString()), ErrorCategory.InvalidArgument, null);
				}
			}
			if (this.user == null || (int)this.user.ExchangeVersion.ExchangeBuild.Major > Server.CurrentExchangeMajorVersion || (int)this.user.ExchangeVersion.ExchangeBuild.Major < Server.Exchange2009MajorVersion)
			{
				base.WriteError(new TaskNotSupportedOnVersionException(base.CommandRuntime.ToString(), Server.CurrentExchangeMajorVersion), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000E294 File Offset: 0x0000C494
		private void FindOrganizationalUnit()
		{
			if (this.OrganizationalUnit == null)
			{
				return;
			}
			bool useConfigNC = this.ConfigurationSession.UseConfigNC;
			bool useGlobalCatalog = this.ConfigurationSession.UseGlobalCatalog;
			this.ConfigurationSession.UseConfigNC = false;
			if (string.IsNullOrEmpty(this.ConfigurationSession.DomainController))
			{
				this.ConfigurationSession.UseGlobalCatalog = true;
			}
			try
			{
				this.organizationalUnit = (ExchangeOrganizationalUnit)base.GetDataObject<ExchangeOrganizationalUnit>(this.OrganizationalUnit, this.ConfigurationSession, (base.CurrentOrganizationId != null) ? base.CurrentOrganizationId.OrganizationalUnit : null, new LocalizedString?(Strings.ErrorOrganizationalUnitNotFound(this.OrganizationalUnit.ToString())), new LocalizedString?(Strings.ErrorOrganizationalUnitNotUnique(this.OrganizationalUnit.ToString())));
				RecipientTaskHelper.IsOrgnizationalUnitInOrganization(this.ConfigurationSession, base.CurrentOrganizationId, this.organizationalUnit, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError));
			}
			finally
			{
				this.ConfigurationSession.UseConfigNC = useConfigNC;
				this.ConfigurationSession.UseGlobalCatalog = useGlobalCatalog;
			}
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000E3AC File Offset: 0x0000C5AC
		private OrganizationSettingsData LoadAbq(OrganizationId organizationId)
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, organizationId, organizationId, false);
			IConfigurationSession configurationSession = this.CreateConfigurationSession(sessionSettings);
			ActiveSyncOrganizationSettings[] array = configurationSession.Find<ActiveSyncOrganizationSettings>(configurationSession.GetOrgContainerId(), QueryScope.SubTree, null, null, 2);
			if (array == null || array.Length == 0)
			{
				base.WriteError(new NoActiveSyncOrganizationSettingsException(organizationId.ToString()), ErrorCategory.InvalidArgument, null);
			}
			OrganizationSettingsData organizationSettingsData = new OrganizationSettingsData(array[0], configurationSession);
			this.organizationSettings[organizationId] = organizationSettingsData;
			return organizationSettingsData;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000E414 File Offset: 0x0000C614
		private QueryFilter ConstructQueryFilterWithCustomFilter(QueryFilter customFilter)
		{
			List<QueryFilter> list = new List<QueryFilter>(3);
			QueryFilter internalFilter = base.InternalFilter;
			if (internalFilter != null)
			{
				list.Add(internalFilter);
			}
			if (customFilter != null)
			{
				list.Add(customFilter);
			}
			switch (list.Count)
			{
			case 0:
				return null;
			case 1:
				return list[0];
			default:
				return new AndFilter(list.ToArray());
			}
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000E470 File Offset: 0x0000C670
		private bool IsFilteringByDeviceAccess()
		{
			return !string.IsNullOrEmpty(this.Filter) && (this.Filter.IndexOf(MobileDeviceSchema.DeviceAccessState.Name, StringComparison.InvariantCultureIgnoreCase) >= 0 || this.Filter.IndexOf(MobileDeviceSchema.DeviceAccessStateReason.Name, StringComparison.InvariantCultureIgnoreCase) >= 0 || this.Filter.IndexOf(MobileDeviceSchema.DeviceAccessControlRule.Name, StringComparison.InvariantCultureIgnoreCase) >= 0);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000E4DC File Offset: 0x0000C6DC
		private bool ShouldShowMobileDevice(MobileDevice mobileDevice)
		{
			return mobileDevice != null && !mobileDevice.MaximumSupportedExchangeObjectVersion.IsOlderThan(mobileDevice.ExchangeVersion) && 0 > mobileDevice.Id.DistinguishedName.IndexOf("Soft Deleted Objects", StringComparison.OrdinalIgnoreCase) && 0 > mobileDevice.Id.Rdn.EscapedName.IndexOf("-", StringComparison.OrdinalIgnoreCase) && 0 > mobileDevice.Id.Parent.Rdn.EscapedName.IndexOf("-", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x040001F9 RID: 505
		private static readonly PropertyDefinition[] SortPropertiesArray = new PropertyDefinition[]
		{
			MobileDeviceSchema.FriendlyName,
			MobileDeviceSchema.DeviceId,
			MobileDeviceSchema.DeviceImei,
			MobileDeviceSchema.DeviceMobileOperator,
			MobileDeviceSchema.DeviceOS,
			MobileDeviceSchema.DeviceOSLanguage,
			MobileDeviceSchema.DeviceTelephoneNumber,
			MobileDeviceSchema.DeviceType,
			MobileDeviceSchema.DeviceUserAgent,
			MobileDeviceSchema.DeviceModel,
			MobileDeviceSchema.FirstSyncTime,
			MobileDeviceSchema.UserDisplayName,
			MobileDeviceSchema.DeviceAccessState,
			MobileDeviceSchema.DeviceAccessStateReason,
			MobileDeviceSchema.DeviceAccessControlRule,
			MobileDeviceSchema.ClientVersion
		};

		// Token: 0x040001FA RID: 506
		private static readonly MobileDeviceSchema FilterableObjectSchema = ObjectSchema.GetInstance<MobileDeviceSchema>();

		// Token: 0x040001FB RID: 507
		private ADUser user;

		// Token: 0x040001FC RID: 508
		private ExchangeOrganizationalUnit organizationalUnit;

		// Token: 0x040001FD RID: 509
		private QueryFilter inputFilter;

		// Token: 0x040001FE RID: 510
		private SortBy internalSortBy;

		// Token: 0x040001FF RID: 511
		private Dictionary<OrganizationId, OrganizationSettingsData> organizationSettings = new Dictionary<OrganizationId, OrganizationSettingsData>(2);
	}
}
