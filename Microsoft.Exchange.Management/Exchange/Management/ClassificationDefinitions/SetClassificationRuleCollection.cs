using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management.Automation;
using System.Text;
using System.Xml.Linq;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x0200084A RID: 2122
	[Cmdlet("Set", "ClassificationRuleCollection", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class SetClassificationRuleCollection : SetClassificationRuleCollectionBase
	{
		// Token: 0x17001621 RID: 5665
		// (get) Token: 0x060049AB RID: 18859 RVA: 0x0012F089 File Offset: 0x0012D289
		// (set) Token: 0x060049AC RID: 18860 RVA: 0x0012F0A0 File Offset: 0x0012D2A0
		[Parameter(ParameterSetName = "Identity", Mandatory = true, ValueFromPipeline = true, Position = 0)]
		public byte[] FileData
		{
			get
			{
				return (byte[])base.Fields["FileData"];
			}
			set
			{
				base.Fields["FileData"] = value;
			}
		}

		// Token: 0x17001622 RID: 5666
		// (get) Token: 0x060049AD RID: 18861 RVA: 0x0012F0B3 File Offset: 0x0012D2B3
		// (set) Token: 0x060049AE RID: 18862 RVA: 0x0012F0D9 File Offset: 0x0012D2D9
		[Parameter(ParameterSetName = "Identity", Mandatory = false, ValueFromPipeline = false)]
		public SwitchParameter OutOfBoxCollection
		{
			get
			{
				return (SwitchParameter)(base.Fields["OutOfBoxCollection"] ?? false);
			}
			set
			{
				base.Fields["OutOfBoxCollection"] = value;
			}
		}

		// Token: 0x17001623 RID: 5667
		// (get) Token: 0x060049AF RID: 18863 RVA: 0x0012F0F1 File Offset: 0x0012D2F1
		// (set) Token: 0x060049B0 RID: 18864 RVA: 0x0012F108 File Offset: 0x0012D308
		[Parameter(Mandatory = false, ValueFromPipeline = false)]
		[ValidateNotNullOrEmpty]
		public OrganizationIdParameter Organization
		{
			get
			{
				return (OrganizationIdParameter)base.Fields["Organization"];
			}
			set
			{
				base.Fields["Organization"] = value;
			}
		}

		// Token: 0x17001624 RID: 5668
		// (get) Token: 0x060049B1 RID: 18865 RVA: 0x0012F11B File Offset: 0x0012D31B
		// (set) Token: 0x060049B2 RID: 18866 RVA: 0x0012F123 File Offset: 0x0012D323
		private new ClassificationRuleCollectionIdParameter Identity
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

		// Token: 0x17001625 RID: 5669
		// (get) Token: 0x060049B3 RID: 18867 RVA: 0x0012F12C File Offset: 0x0012D32C
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetClassificationRuleCollection(this.localizedName);
			}
		}

		// Token: 0x060049B4 RID: 18868 RVA: 0x0012F139 File Offset: 0x0012D339
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			base.CurrentOrganizationId = this.ResolveCurrentOrganization();
		}

		// Token: 0x060049B5 RID: 18869 RVA: 0x0012F150 File Offset: 0x0012D350
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			byte[] array = null;
			byte[] array2 = null;
			bool flag = false;
			try
			{
				flag = RulePackageDecrypter.DecryptRulePackage(this.FileData, out array, out array2);
			}
			catch (Exception innerException)
			{
				base.WriteError(new ClassificationRuleCollectionDecryptionException(innerException), ErrorCategory.InvalidData, null);
			}
			XDocument xdocument = this.ValidateAgainstSchema(flag ? array : this.FileData);
			this.SetIdentityParameter(xdocument);
			if (base.OptionalIdentityData != null)
			{
				base.OptionalIdentityData.ConfigurationContainerRdn = ClassificationDefinitionConstants.ClassificationDefinitionsRdn;
			}
			this.DataObject = (TransportRule)this.ResolveDataObject();
			if (base.HasErrors)
			{
				return;
			}
			ExAssert.RetailAssert(this.DataObject != null, "DataObject must not be null at this point.");
			if (VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled)
			{
				this.ValidateOperationScope();
			}
			string s = this.ValidateAgainstBusinessRulesAndReadMetadata(xdocument);
			this.FileData = (flag ? array2 : Encoding.Unicode.GetBytes(s));
			base.InternalValidate();
			TaskLogger.LogExit();
		}

		// Token: 0x060049B6 RID: 18870 RVA: 0x0012F24C File Offset: 0x0012D44C
		protected override void StampChangesOn(IConfigurable dataObject)
		{
			TaskLogger.LogEnter();
			TransportRule transportRule = (TransportRule)dataObject;
			byte[] replicationSignature = null;
			try
			{
				replicationSignature = ClassificationRuleCollectionValidationUtils.PackAndValidateCompressedRulePackage(this.FileData, this.validationContext);
			}
			catch (LocalizedException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidData, null);
			}
			transportRule.AdminDisplayName = this.defaultName;
			transportRule.ReplicationSignature = replicationSignature;
			base.StampChangesOn(dataObject);
			TaskLogger.LogEnter();
		}

		// Token: 0x060049B7 RID: 18871 RVA: 0x0012F2B8 File Offset: 0x0012D4B8
		private OrganizationId ResolveCurrentOrganization()
		{
			if (!object.ReferenceEquals(this.Organization, null))
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, null, sessionSettings, 306, "ResolveCurrentOrganization", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\transport\\ClassificationDefinitions\\SetClassificationRuleCollection.cs");
				tenantOrTopologyConfigurationSession.UseConfigNC = false;
				ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(this.Organization, tenantOrTopologyConfigurationSession, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.Organization.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.Organization.ToString())));
				return adorganizationalUnit.OrganizationId;
			}
			return base.ExecutingUserOrganizationId;
		}

		// Token: 0x060049B8 RID: 18872 RVA: 0x0012F36C File Offset: 0x0012D56C
		private XDocument ValidateAgainstSchema(byte[] rulePackageRawData)
		{
			XDocument result = null;
			try
			{
				result = XmlProcessingUtils.ValidateRulePackageXmlContentsLite(rulePackageRawData);
			}
			catch (LocalizedException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidData, null);
			}
			return result;
		}

		// Token: 0x060049B9 RID: 18873 RVA: 0x0012F3A4 File Offset: 0x0012D5A4
		private void ValidateOperationScope()
		{
			bool flag = this.OutOfBoxCollection;
			bool flag2 = OrganizationId.ForestWideOrgId.Equals(this.DataObject.OrganizationId);
			if (flag && !flag2)
			{
				this.WriteWarning(Strings.ClassificationRuleCollectionIllegalScopedSetOperation);
				this.OutOfBoxCollection = false;
				return;
			}
			if (!flag && flag2)
			{
				base.WriteError(new ClassificationRuleCollectionIllegalScopeException(Strings.ClassificationRuleCollectionIllegalScopedSetOobOperation), ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x060049BA RID: 18874 RVA: 0x0012F40C File Offset: 0x0012D60C
		private string ValidateAgainstBusinessRulesAndReadMetadata(XDocument rulePackXDoc)
		{
			ExAssert.RetailAssert(this.rulePackageIdentifier != null && this.DataObject != null, "Business rules validation in Set-ClassificationRuleCollection must take place after the DataObject resolution");
			string result = string.Empty;
			try
			{
				this.validationContext = new ValidationContext(ClassificationRuleCollectionOperationType.Update, base.CurrentOrganizationId, VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled && this.OutOfBoxCollection, false, (IConfigurationSession)base.DataSession, this.DataObject, null, null);
				result = ClassificationRuleCollectionValidationUtils.ValidateRulePackageContents(this.validationContext, rulePackXDoc);
				ClassificationRuleCollectionLocalizableDetails classificationRuleCollectionLocalizableDetails = XmlProcessingUtils.ReadDefaultRulePackageMetadata(rulePackXDoc);
				this.defaultName = classificationRuleCollectionLocalizableDetails.Name;
				ClassificationRuleCollectionLocalizableDetails classificationRuleCollectionLocalizableDetails2 = XmlProcessingUtils.ReadRulePackageMetadata(rulePackXDoc, CultureInfo.CurrentCulture);
				this.localizedName = ((classificationRuleCollectionLocalizableDetails2 != null && classificationRuleCollectionLocalizableDetails2.Name != null) ? classificationRuleCollectionLocalizableDetails2.Name : this.defaultName);
				if (VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled && OrganizationId.ForestWideOrgId.Equals(((IDirectorySession)base.DataSession).SessionSettings.CurrentOrganizationId))
				{
					this.WriteWarning(Strings.ClassificationRuleCollectionIneffectiveSharingViolationCheck);
				}
				SortedSet<string> sortedSet = new SortedSet<string>();
				IList<string> deletedRulesInUse = ClassificationRuleCollectionValidationUtils.GetDeletedRulesInUse(base.DataSession, this.DataObject, sortedSet, rulePackXDoc);
				if (deletedRulesInUse.Count > 0)
				{
					LocalizedString message = Strings.ClassificationRuleCollectionSharingViolationSetOperationVerbose(this.localizedName ?? this.rulePackageIdentifier, string.Join(Strings.ClassificationRuleCollectionOffendingListSeparator, deletedRulesInUse), string.Join(Strings.ClassificationRuleCollectionOffendingListSeparator, sortedSet));
					throw ClassificationDefinitionUtils.PopulateExceptionSource<ClassificationRuleCollectionSharingViolationException, IList<string>>(new ClassificationRuleCollectionSharingViolationException(message), deletedRulesInUse);
				}
			}
			catch (ClassificationRuleCollectionSharingViolationException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidOperation, null);
			}
			catch (ClassificationRuleCollectionInternalValidationException ex)
			{
				base.WriteError(ex, (-2147287038 == ex.Error) ? ErrorCategory.ObjectNotFound : ErrorCategory.InvalidResult, null);
			}
			catch (ClassificationRuleCollectionTimeoutException exception2)
			{
				base.WriteError(exception2, ErrorCategory.OperationTimeout, null);
			}
			catch (LocalizedException exception3)
			{
				base.WriteError(exception3, ErrorCategory.InvalidData, null);
			}
			return result;
		}

		// Token: 0x060049BB RID: 18875 RVA: 0x0012F640 File Offset: 0x0012D840
		private void SetIdentityParameter(XDocument rulePackXDoc)
		{
			this.rulePackageIdentifier = XmlProcessingUtils.GetRulePackId(rulePackXDoc);
			this.Identity = ClassificationRuleCollectionIdParameter.Parse(this.rulePackageIdentifier);
		}

		// Token: 0x04002C5A RID: 11354
		private string rulePackageIdentifier;

		// Token: 0x04002C5B RID: 11355
		private string defaultName;

		// Token: 0x04002C5C RID: 11356
		private string localizedName;

		// Token: 0x04002C5D RID: 11357
		private ValidationContext validationContext;
	}
}
