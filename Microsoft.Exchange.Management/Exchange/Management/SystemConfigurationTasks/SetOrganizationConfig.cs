using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Provisioning;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AEE RID: 2798
	[Cmdlet("Set", "OrganizationConfig", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetOrganizationConfig : BaseOrganization
	{
		// Token: 0x17001E1F RID: 7711
		// (get) Token: 0x0600634D RID: 25421 RVA: 0x0019F02B File Offset: 0x0019D22B
		// (set) Token: 0x0600634E RID: 25422 RVA: 0x0019F033 File Offset: 0x0019D233
		[Parameter(Mandatory = false, ParameterSetName = "Identity", Position = 0)]
		public new OrganizationIdParameter Identity
		{
			get
			{
				return base.Identity;
			}
			set
			{
				base.Identity = value;
			}
		}

		// Token: 0x17001E20 RID: 7712
		// (get) Token: 0x0600634F RID: 25423 RVA: 0x0019F03C File Offset: 0x0019D23C
		// (set) Token: 0x06006350 RID: 25424 RVA: 0x0019F044 File Offset: 0x0019D244
		[Parameter(Mandatory = false)]
		public new AccountPartitionIdParameter AccountPartition
		{
			get
			{
				return base.AccountPartition;
			}
			set
			{
				base.AccountPartition = value;
			}
		}

		// Token: 0x17001E21 RID: 7713
		// (get) Token: 0x06006351 RID: 25425 RVA: 0x0019F04D File Offset: 0x0019D24D
		// (set) Token: 0x06006352 RID: 25426 RVA: 0x0019F064 File Offset: 0x0019D264
		[Parameter(Mandatory = false)]
		public RecipientIdParameter MicrosoftExchangeRecipientReplyRecipient
		{
			get
			{
				return (RecipientIdParameter)base.Fields[OrganizationSchema.MicrosoftExchangeRecipientReplyRecipient];
			}
			set
			{
				base.Fields[OrganizationSchema.MicrosoftExchangeRecipientReplyRecipient] = value;
			}
		}

		// Token: 0x17001E22 RID: 7714
		// (get) Token: 0x06006353 RID: 25427 RVA: 0x0019F077 File Offset: 0x0019D277
		// (set) Token: 0x06006354 RID: 25428 RVA: 0x0019F08E File Offset: 0x0019D28E
		[Parameter(Mandatory = false)]
		public UserContactGroupIdParameter HierarchicalAddressBookRoot
		{
			get
			{
				return (UserContactGroupIdParameter)base.Fields[OrganizationSchema.HABRootDepartmentLink];
			}
			set
			{
				base.Fields[OrganizationSchema.HABRootDepartmentLink] = value;
			}
		}

		// Token: 0x17001E23 RID: 7715
		// (get) Token: 0x06006355 RID: 25429 RVA: 0x0019F0A1 File Offset: 0x0019D2A1
		// (set) Token: 0x06006356 RID: 25430 RVA: 0x0019F0B8 File Offset: 0x0019D2B8
		[Parameter(Mandatory = false)]
		public OrganizationalUnitIdParameter DistributionGroupDefaultOU
		{
			get
			{
				return (OrganizationalUnitIdParameter)base.Fields[OrganizationSchema.DistributionGroupDefaultOU];
			}
			set
			{
				base.Fields[OrganizationSchema.DistributionGroupDefaultOU] = value;
			}
		}

		// Token: 0x17001E24 RID: 7716
		// (get) Token: 0x06006357 RID: 25431 RVA: 0x0019F0CB File Offset: 0x0019D2CB
		// (set) Token: 0x06006358 RID: 25432 RVA: 0x0019F0E2 File Offset: 0x0019D2E2
		[Parameter(Mandatory = false)]
		[ValidateCount(0, 64)]
		public MultiValuedProperty<RecipientIdParameter> ExchangeNotificationRecipients
		{
			get
			{
				return (MultiValuedProperty<RecipientIdParameter>)base.Fields["ExchangeNotificationRecipients"];
			}
			set
			{
				base.Fields["ExchangeNotificationRecipients"] = value;
			}
		}

		// Token: 0x17001E25 RID: 7717
		// (get) Token: 0x06006359 RID: 25433 RVA: 0x0019F0F5 File Offset: 0x0019D2F5
		// (set) Token: 0x0600635A RID: 25434 RVA: 0x0019F10C File Offset: 0x0019D30C
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<MailboxOrMailUserIdParameter> RemotePublicFolderMailboxes
		{
			get
			{
				return (MultiValuedProperty<MailboxOrMailUserIdParameter>)base.Fields[OrganizationSchema.RemotePublicFolderMailboxes];
			}
			set
			{
				base.Fields[OrganizationSchema.RemotePublicFolderMailboxes] = value;
			}
		}

		// Token: 0x0600635B RID: 25435 RVA: 0x0019F120 File Offset: 0x0019D320
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			Organization organization = (Organization)this.GetDynamicParameters();
			if (organization.MicrosoftExchangeRecipientEmailAddresses.Changed && organization.IsChanged(OrganizationSchema.MicrosoftExchangeRecipientPrimarySmtpAddress))
			{
				base.ThrowTerminatingError(new InvalidOperationException(Strings.ErrorPrimarySmtpAndEmailAddressesSpecified), ErrorCategory.InvalidOperation, null);
			}
			if (organization.IsModified(OrganizationSchema.EwsApplicationAccessPolicy))
			{
				if (!organization.EwsAllowListSpecified && !organization.EwsBlockListSpecified)
				{
					organization.EwsExceptions = null;
				}
			}
			else
			{
				if (organization.EwsAllowListSpecified)
				{
					organization.EwsApplicationAccessPolicy = new EwsApplicationAccessPolicy?(EwsApplicationAccessPolicy.EnforceAllowList);
				}
				if (organization.EwsBlockListSpecified)
				{
					organization.EwsApplicationAccessPolicy = new EwsApplicationAccessPolicy?(EwsApplicationAccessPolicy.EnforceBlockList);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600635C RID: 25436 RVA: 0x0019F1C8 File Offset: 0x0019D3C8
		protected override void InternalStateReset()
		{
			TaskLogger.LogEnter();
			base.InternalStateReset();
			ADSessionSettings sessionSettings;
			if (this.AccountPartition != null)
			{
				PartitionId partitionId = RecipientTaskHelper.ResolvePartitionId(this.AccountPartition, new Task.TaskErrorLoggingDelegate(base.WriteError));
				sessionSettings = ADSessionSettings.FromAccountPartitionRootOrgScopeSet(partitionId);
			}
			else
			{
				sessionSettings = ADSessionSettings.RescopeToSubtree(base.OrgWideSessionSettings);
			}
			this.recipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, sessionSettings, 176, "InternalStateReset", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\organization\\SetOrganization.cs");
			this.recipientSession.UseConfigNC = true;
			TaskLogger.LogExit();
		}

		// Token: 0x0600635D RID: 25437 RVA: 0x0019F254 File Offset: 0x0019D454
		protected override void ResolveLocalSecondaryIdentities()
		{
			base.ResolveLocalSecondaryIdentities();
			Organization organization = (Organization)this.GetDynamicParameters();
			if (base.Fields.IsModified(OrganizationSchema.MicrosoftExchangeRecipientReplyRecipient))
			{
				if (this.MicrosoftExchangeRecipientReplyRecipient != null)
				{
					ADRecipient adrecipient = (ADRecipient)base.GetDataObject<ADRecipient>(this.MicrosoftExchangeRecipientReplyRecipient, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorRecipientNotFound(this.MicrosoftExchangeRecipientReplyRecipient.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(this.MicrosoftExchangeRecipientReplyRecipient.ToString())));
					organization.MicrosoftExchangeRecipientReplyRecipient = (ADObjectId)adrecipient.Identity;
				}
				else
				{
					organization.MicrosoftExchangeRecipientReplyRecipient = null;
				}
			}
			if (base.Fields.IsModified(OrganizationSchema.HABRootDepartmentLink))
			{
				if (this.HierarchicalAddressBookRoot != null)
				{
					ADRecipient adrecipient2 = (ADRecipient)base.GetDataObject<ADRecipient>(this.HierarchicalAddressBookRoot, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorRecipientNotFound(this.HierarchicalAddressBookRoot.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(this.HierarchicalAddressBookRoot.ToString())));
					organization.HierarchicalAddressBookRoot = (ADObjectId)adrecipient2.Identity;
				}
				else
				{
					organization.HierarchicalAddressBookRoot = null;
				}
			}
			if (base.Fields.IsModified(OrganizationSchema.DistributionGroupDefaultOU))
			{
				if (this.DistributionGroupDefaultOU != null)
				{
					ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(this.DistributionGroupDefaultOU, this.ConfigurationSession, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.DistributionGroupDefaultOU.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.DistributionGroupDefaultOU.ToString())));
					organization.DistributionGroupDefaultOU = (ADObjectId)adorganizationalUnit.Identity;
					return;
				}
				organization.DistributionGroupDefaultOU = null;
			}
		}

		// Token: 0x0600635E RID: 25438 RVA: 0x0019F3D8 File Offset: 0x0019D5D8
		protected override void StampChangesOn(IConfigurable dataObject)
		{
			TaskLogger.LogEnter();
			Organization organization = (Organization)dataObject;
			this.mergedCollection = new MultiValuedProperty<OrganizationSummaryEntry>();
			foreach (OrganizationSummaryEntry organizationSummaryEntry in organization.OrganizationSummary)
			{
				if (organizationSummaryEntry.NumberOfFields >= 3 || OrganizationSummaryEntry.IsValidKeyForCurrentAndPreviousRelease(organizationSummaryEntry.Key))
				{
					this.mergedCollection.Add(organizationSummaryEntry);
				}
			}
			this.microsoftExchangeRecipient = MailboxTaskHelper.FindMicrosoftExchangeRecipient(this.recipientSession, (IConfigurationSession)base.DataSession);
			if (this.microsoftExchangeRecipient != null)
			{
				organization.MicrosoftExchangeRecipientEmailAddresses = this.microsoftExchangeRecipient.EmailAddresses;
				organization.MicrosoftExchangeRecipientReplyRecipient = this.microsoftExchangeRecipient.ForwardingAddress;
				organization.MicrosoftExchangeRecipientEmailAddressPolicyEnabled = this.microsoftExchangeRecipient.EmailAddressPolicyEnabled;
				organization.MicrosoftExchangeRecipientPrimarySmtpAddress = this.microsoftExchangeRecipient.PrimarySmtpAddress;
				organization.ResetChangeTracking();
			}
			if (base.Fields.IsModified("ExchangeNotificationRecipients"))
			{
				this.SetMultiValuedProperty<RecipientIdParameter, SmtpAddress>(this.ExchangeNotificationRecipients, organization.ExchangeNotificationRecipients, new SetOrganizationConfig.Resolver<RecipientIdParameter, SmtpAddress>(this.ResolveRecipients));
			}
			if (base.Fields.IsModified(OrganizationSchema.RemotePublicFolderMailboxes))
			{
				this.SetMultiValuedProperty<MailboxOrMailUserIdParameter, ADObjectId>(this.RemotePublicFolderMailboxes, organization.RemotePublicFolderMailboxes, new SetOrganizationConfig.Resolver<MailboxOrMailUserIdParameter, ADObjectId>(this.ResolveRemotePublicFolderMailboxes));
			}
			base.StampChangesOn(dataObject);
			TaskLogger.LogExit();
		}

		// Token: 0x0600635F RID: 25439 RVA: 0x0019F538 File Offset: 0x0019D738
		private void SetMultiValuedProperty<T, V>(MultiValuedProperty<T> inputValues, MultiValuedProperty<V> existingValues, SetOrganizationConfig.Resolver<T, V> resolver)
		{
			if (inputValues == null)
			{
				existingValues.Clear();
				return;
			}
			if (!inputValues.IsChangesOnlyCopy)
			{
				existingValues.Clear();
				using (IEnumerator<V> enumerator = resolver(inputValues).Distinct<V>().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						V item = enumerator.Current;
						existingValues.Add(item);
					}
					return;
				}
			}
			HashSet<V> first = new HashSet<V>(existingValues);
			IEnumerable<V> second = resolver(inputValues.Added.Cast<T>());
			IEnumerable<V> second2 = resolver(inputValues.Removed.Cast<T>());
			existingValues.Clear();
			foreach (V item2 in first.Union(second).Except(second2))
			{
				existingValues.Add(item2);
			}
		}

		// Token: 0x06006360 RID: 25440 RVA: 0x0019F824 File Offset: 0x0019DA24
		private IEnumerable<ADObjectId> ResolveRemotePublicFolderMailboxes(IEnumerable<MailboxOrMailUserIdParameter> adUsers)
		{
			foreach (MailboxOrMailUserIdParameter adUser in adUsers)
			{
				ADUser adObject = (ADUser)base.GetDataObject<ADUser>(adUser, base.TenantGlobalCatalogSession, this.RootId, new LocalizedString?(Strings.ErrorRecipientNotFound(adUser.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(adUser.ToString())));
				yield return (ADObjectId)adObject.Identity;
			}
			yield break;
		}

		// Token: 0x06006361 RID: 25441 RVA: 0x0019F848 File Offset: 0x0019DA48
		private void MergeAdded(MultiValuedProperty<OrganizationSummaryEntry> summaryInfo)
		{
			foreach (OrganizationSummaryEntry organizationSummaryEntry in summaryInfo.Added)
			{
				if (!organizationSummaryEntry.Key.Equals(OrganizationSummaryEntry.SummaryInfoUpdateDate) && OrganizationSummaryEntry.IsValidKeyForCurrentRelease(organizationSummaryEntry.Key))
				{
					OrganizationSummaryEntry organizationSummaryEntry2 = null;
					if (this.FindInMergedCollection(organizationSummaryEntry, out organizationSummaryEntry2))
					{
						this.mergedCollection.Remove(organizationSummaryEntry2);
						this.mergedCollection.Add(new OrganizationSummaryEntry(organizationSummaryEntry2.Key, organizationSummaryEntry.HasError ? organizationSummaryEntry2.Value : organizationSummaryEntry.Value, organizationSummaryEntry.HasError));
					}
					else if (!organizationSummaryEntry.HasError)
					{
						this.mergedCollection.Add(organizationSummaryEntry.Clone());
					}
				}
			}
		}

		// Token: 0x06006362 RID: 25442 RVA: 0x0019F900 File Offset: 0x0019DB00
		private void MergeRemoved(MultiValuedProperty<OrganizationSummaryEntry> summaryInfo)
		{
			foreach (OrganizationSummaryEntry organizationSummaryEntry in summaryInfo.Removed)
			{
				OrganizationSummaryEntry item;
				if (!organizationSummaryEntry.Key.Equals(OrganizationSummaryEntry.SummaryInfoUpdateDate) && OrganizationSummaryEntry.IsValidKeyForCurrentRelease(organizationSummaryEntry.Key) && this.FindInMergedCollection(organizationSummaryEntry, out item))
				{
					this.mergedCollection.Remove(item);
				}
			}
		}

		// Token: 0x06006363 RID: 25443 RVA: 0x0019F964 File Offset: 0x0019DB64
		private bool FindInMergedCollection(OrganizationSummaryEntry entry, out OrganizationSummaryEntry foundEntry)
		{
			foundEntry = null;
			foreach (OrganizationSummaryEntry organizationSummaryEntry in this.mergedCollection)
			{
				if (organizationSummaryEntry.Key.Equals(entry.Key))
				{
					foundEntry = organizationSummaryEntry;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006364 RID: 25444 RVA: 0x0019F9D0 File Offset: 0x0019DBD0
		private void UpdateUploadDate()
		{
			OrganizationSummaryEntry organizationSummaryEntry = new OrganizationSummaryEntry(OrganizationSummaryEntry.SummaryInfoUpdateDate, DateTime.UtcNow.ToString(), false);
			OrganizationSummaryEntry item;
			if (this.FindInMergedCollection(organizationSummaryEntry, out item))
			{
				this.mergedCollection.Remove(item);
			}
			this.mergedCollection.Add(organizationSummaryEntry);
		}

		// Token: 0x06006365 RID: 25445 RVA: 0x0019FA20 File Offset: 0x0019DC20
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (this.DataObject.IsChanged(OrganizationSchema.CustomerFeedbackEnabled) && this.DataObject.CustomerFeedbackEnabled == null)
			{
				base.WriteError(new InvalidOperationException(Strings.CustomerFeedbackEnabledError), ErrorCategory.InvalidOperation, null);
			}
			if (this.DataObject.IsChanged(OrganizationSchema.OrganizationSummary))
			{
				MultiValuedProperty<OrganizationSummaryEntry> organizationSummary = this.DataObject.OrganizationSummary;
				this.MergeAdded(organizationSummary);
				this.MergeRemoved(organizationSummary);
				if (this.mergedCollection.Changed)
				{
					this.UpdateUploadDate();
				}
				this.DataObject.OrganizationSummary = this.mergedCollection;
			}
			Organization organization = (Organization)this.GetDynamicParameters();
			if (organization.EwsAllowListSpecified && organization.EwsBlockListSpecified)
			{
				base.ThrowTerminatingError(new ArgumentException(Strings.ErrorEwsAllowListAndEwsBlockListSpecified), ErrorCategory.InvalidArgument, null);
			}
			if (organization.IsModified(OrganizationSchema.EwsApplicationAccessPolicy))
			{
				if (organization.EwsApplicationAccessPolicy == EwsApplicationAccessPolicy.EnforceAllowList && organization.EwsBlockListSpecified)
				{
					base.ThrowTerminatingError(new ArgumentException(Strings.ErrorEwsEnforceAllowListAndEwsBlockListSpecified), ErrorCategory.InvalidArgument, null);
				}
				if (organization.EwsApplicationAccessPolicy == EwsApplicationAccessPolicy.EnforceBlockList && organization.EwsAllowListSpecified)
				{
					base.ThrowTerminatingError(new ArgumentException(Strings.ErrorEwsEnforceBlockListAndEwsAllowListSpecified), ErrorCategory.InvalidArgument, null);
				}
			}
			if (organization.IsModified(OrganizationSchema.DefaultPublicFolderMailbox))
			{
				Organization orgContainer = ((IConfigurationSession)base.DataSession).GetOrgContainer();
				PublicFolderInformation defaultPublicFolderMailbox = orgContainer.DefaultPublicFolderMailbox;
				if (!defaultPublicFolderMailbox.CanUpdate)
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorCannotUpdatePublicFolderHierarchyInformation), ExchangeErrorCategory.Client, null);
				}
				if (defaultPublicFolderMailbox.HierarchyMailboxGuid != Guid.Empty)
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorPublicFolderHierarchyAlreadyProvisioned), ExchangeErrorCategory.Client, null);
				}
			}
			if (this.DataObject.IsChanged(OrganizationSchema.TenantRelocationsAllowed) && !this.DataObject.TenantRelocationsAllowed)
			{
				string ridMasterName = ForestTenantRelocationsCache.GetRidMasterName(this.DataObject.OrganizationId.PartitionId);
				ITenantConfigurationSession tenantConfigurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(ridMasterName, true, ConsistencyMode.PartiallyConsistent, null, ADSessionSettings.FromAllTenantsPartitionId(this.DataObject.OrganizationId.PartitionId ?? PartitionId.LocalForest), 564, "InternalValidate", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\organization\\SetOrganization.cs");
				TenantRelocationRequest[] array = tenantConfigurationSession.Find<TenantRelocationRequest>(null, QueryScope.SubTree, new OrFilter(new QueryFilter[]
				{
					TenantRelocationRequest.TenantRelocationRequestFilter,
					TenantRelocationRequest.TenantRelocationLandingFilter
				}), null, 1);
				if (array.Length > 0)
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorTenantRelocationInProgress(array[0].Name)), ErrorCategory.InvalidOperation, null);
				}
			}
		}

		// Token: 0x06006366 RID: 25446 RVA: 0x0019FCB8 File Offset: 0x0019DEB8
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			Organization organization = (Organization)base.PrepareDataObject();
			this.isMicrosoftExchangeRecipientChanged = (organization.IsChanged(OrganizationSchema.MicrosoftExchangeRecipientEmailAddresses) || organization.IsChanged(OrganizationSchema.MicrosoftExchangeRecipientEmailAddressPolicyEnabled) || organization.IsChanged(OrganizationSchema.MicrosoftExchangeRecipientPrimarySmtpAddress) || organization.IsChanged(OrganizationSchema.MicrosoftExchangeRecipientReplyRecipient));
			if (this.isMicrosoftExchangeRecipientChanged && this.microsoftExchangeRecipient == null)
			{
				this.WriteWarning(Strings.ErrorMicrosoftExchangeRecipientNotFound);
			}
			if (this.microsoftExchangeRecipient != null)
			{
				this.microsoftExchangeRecipient.DisplayName = ADMicrosoftExchangeRecipient.DefaultDisplayName;
				this.microsoftExchangeRecipient.DeliverToMailboxAndForward = false;
				if (organization.IsChanged(OrganizationSchema.MicrosoftExchangeRecipientEmailAddresses))
				{
					this.microsoftExchangeRecipient.EmailAddresses.CopyChangesFrom(organization.MicrosoftExchangeRecipientEmailAddresses);
					if (Datacenter.IsMultiTenancyEnabled())
					{
						RecipientTaskHelper.ValidateSmtpAddress(this.ConfigurationSession, this.microsoftExchangeRecipient.EmailAddresses, this.microsoftExchangeRecipient, new Task.ErrorLoggerDelegate(base.WriteError), base.ProvisioningCache);
					}
					ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, this.microsoftExchangeRecipient.OrganizationId, base.CurrentOrganizationId, false);
					IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.TenantGlobalCatalogSession.DomainController, true, ConsistencyMode.PartiallyConsistent, base.TenantGlobalCatalogSession.NetworkCredential, sessionSettings, 632, "PrepareDataObject", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\organization\\SetOrganization.cs");
					RecipientTaskHelper.ValidateEmailAddressErrorOut(tenantOrRootOrgRecipientSession, this.microsoftExchangeRecipient.EmailAddresses, this.microsoftExchangeRecipient, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerReThrowDelegate(this.WriteError));
					if (this.microsoftExchangeRecipient.EmailAddresses.Count > 0)
					{
						RecipientTaskHelper.ValidateEmailAddress(base.TenantGlobalCatalogSession, this.microsoftExchangeRecipient.EmailAddresses, this.microsoftExchangeRecipient, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
					}
				}
				if (organization.IsChanged(OrganizationSchema.MicrosoftExchangeRecipientPrimarySmtpAddress))
				{
					this.microsoftExchangeRecipient.PrimarySmtpAddress = organization.MicrosoftExchangeRecipientPrimarySmtpAddress;
				}
				if (organization.IsChanged(OrganizationSchema.MicrosoftExchangeRecipientReplyRecipient))
				{
					this.microsoftExchangeRecipient.ForwardingAddress = organization.MicrosoftExchangeRecipientReplyRecipient;
				}
				if (organization.IsChanged(OrganizationSchema.MicrosoftExchangeRecipientEmailAddressPolicyEnabled))
				{
					this.microsoftExchangeRecipient.EmailAddressPolicyEnabled = organization.MicrosoftExchangeRecipientEmailAddressPolicyEnabled;
				}
				ValidationError[] array = this.microsoftExchangeRecipient.Validate();
				for (int i = 0; i < array.Length; i++)
				{
					this.WriteError(new DataValidationException(array[i]), ErrorCategory.InvalidData, null, i == array.Length - 1);
				}
				if (!ProvisioningLayer.Disabled)
				{
					if (base.IsProvisioningLayerAvailable)
					{
						ProvisioningLayer.UpdateAffectedIConfigurable(this, RecipientTaskHelper.ConvertRecipientToPresentationObject(this.microsoftExchangeRecipient), false);
					}
					else
					{
						base.WriteError(new InvalidOperationException(Strings.ErrorNoProvisioningHandlerAvailable), ErrorCategory.InvalidOperation, null);
					}
				}
				organization.MicrosoftExchangeRecipientEmailAddresses = this.microsoftExchangeRecipient.EmailAddresses;
				organization.MicrosoftExchangeRecipientPrimarySmtpAddress = this.microsoftExchangeRecipient.PrimarySmtpAddress;
				organization.MicrosoftExchangeRecipientEmailAddressPolicyEnabled = this.microsoftExchangeRecipient.EmailAddressPolicyEnabled;
			}
			if (organization.IsChanged(OrganizationSchema.AdfsAuthenticationRawConfiguration) && AdfsAuthenticationConfig.Validate((string)organization[OrganizationSchema.AdfsAuthenticationRawConfiguration]))
			{
				this.WriteWarning(Strings.NeedIisRestartWarning);
			}
			TaskLogger.LogExit();
			return organization;
		}

		// Token: 0x06006367 RID: 25447 RVA: 0x0019FFA0 File Offset: 0x0019E1A0
		protected override void ProvisioningUpdateConfigurationObject()
		{
		}

		// Token: 0x06006368 RID: 25448 RVA: 0x0019FFA4 File Offset: 0x0019E1A4
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (this.microsoftExchangeRecipient != null && this.microsoftExchangeRecipient.ObjectState != ObjectState.Unchanged)
			{
				this.recipientSession.Save(this.microsoftExchangeRecipient);
			}
			if (this.DataObject.IsModified(OrganizationSchema.CustomerFeedbackEnabled))
			{
				IConfigurationSession configurationSession = (IConfigurationSession)base.DataSession;
				IEnumerable<Server> enumerable = configurationSession.FindAllPaged<Server>();
				if (enumerable != null)
				{
					foreach (Server server in enumerable)
					{
						if (server.IsE14OrLater)
						{
							server.CustomerFeedbackEnabled = this.DataObject.CustomerFeedbackEnabled;
							configurationSession.Save(server);
						}
					}
				}
			}
			if (this.DataObject.IsModified(OrganizationSchema.HABRootDepartmentLink) && VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled)
			{
				ADOrganizationConfig dataObject = this.DataObject;
				ADObjectId hierarchicalAddressBookRoot = dataObject.HierarchicalAddressBookRoot;
				dataObject.HierarchicalAddressBookRoot = null;
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(false, ConsistencyMode.PartiallyConsistent, this.ConfigurationSession.SessionSettings, 756, "InternalProcessRecord", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\organization\\SetOrganization.cs");
				ADObjectId organizationalUnit = dataObject.OrganizationId.OrganizationalUnit;
				ExchangeOrganizationalUnit exchangeOrganizationalUnit = (ExchangeOrganizationalUnit)base.GetDataObject<ExchangeOrganizationalUnit>(new OrganizationalUnitIdParameter(organizationalUnit), tenantOrTopologyConfigurationSession, null, new LocalizedString?(Strings.ErrorOrganizationalUnitNotFound(organizationalUnit.ToString())), new LocalizedString?(Strings.ErrorOrganizationalUnitNotUnique(organizationalUnit.ToString())));
				exchangeOrganizationalUnit.HierarchicalAddressBookRoot = hierarchicalAddressBookRoot;
				tenantOrTopologyConfigurationSession.Save(exchangeOrganizationalUnit);
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}

		// Token: 0x06006369 RID: 25449 RVA: 0x001A0134 File Offset: 0x0019E334
		private IEnumerable<SmtpAddress> ResolveRecipients(IEnumerable<RecipientIdParameter> recipients)
		{
			LinkedList<SmtpAddress> linkedList = new LinkedList<SmtpAddress>();
			foreach (RecipientIdParameter recipientIdParameter in recipients)
			{
				if (SmtpAddress.IsValidSmtpAddress(recipientIdParameter.RawIdentity))
				{
					linkedList.AddLast(SmtpAddress.Parse(recipientIdParameter.RawIdentity));
				}
				else
				{
					ADRecipient adrecipient = null;
					IEnumerable<ADRecipient> objects = recipientIdParameter.GetObjects<ADRecipient>(null, base.TenantGlobalCatalogSession);
					using (IEnumerator<ADRecipient> enumerator2 = objects.GetEnumerator())
					{
						if (!enumerator2.MoveNext())
						{
							this.WriteError(new NoRecipientsForRecipientIdException(recipientIdParameter.ToString()), (ErrorCategory)1000, this.DataObject, true);
						}
						adrecipient = enumerator2.Current;
						if (enumerator2.MoveNext())
						{
							this.WriteError(new MoreThanOneRecipientForRecipientIdException(recipientIdParameter.ToString()), (ErrorCategory)1000, this.DataObject, true);
						}
					}
					if (SmtpAddress.Empty.Equals(adrecipient.PrimarySmtpAddress))
					{
						this.WriteError(new NoSmtpAddressForRecipientIdException(recipientIdParameter.ToString()), (ErrorCategory)1000, this.DataObject, true);
					}
					linkedList.AddLast(adrecipient.PrimarySmtpAddress);
				}
			}
			return linkedList;
		}

		// Token: 0x040035EB RID: 13803
		private ADMicrosoftExchangeRecipient microsoftExchangeRecipient;

		// Token: 0x040035EC RID: 13804
		private bool isMicrosoftExchangeRecipientChanged;

		// Token: 0x040035ED RID: 13805
		private IRecipientSession recipientSession;

		// Token: 0x040035EE RID: 13806
		private MultiValuedProperty<OrganizationSummaryEntry> mergedCollection;

		// Token: 0x02000AEF RID: 2799
		// (Invoke) Token: 0x0600636C RID: 25452
		private delegate IEnumerable<V> Resolver<T, V>(IEnumerable<T> inputValues);
	}
}
