using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Xml.Linq;
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
	// Token: 0x02000840 RID: 2112
	[Cmdlet("New", "ClassificationRuleCollection", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = "ArbitraryCollection")]
	public sealed class NewClassificationRuleCollection : NewMultitenancyFixedNameSystemConfigurationObjectTask<TransportRule>
	{
		// Token: 0x17001614 RID: 5652
		// (get) Token: 0x06004952 RID: 18770 RVA: 0x0012D831 File Offset: 0x0012BA31
		// (set) Token: 0x06004953 RID: 18771 RVA: 0x0012D848 File Offset: 0x0012BA48
		[Parameter(ParameterSetName = "ArbitraryCollection", Mandatory = true, ValueFromPipeline = true, Position = 0)]
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

		// Token: 0x17001615 RID: 5653
		// (get) Token: 0x06004954 RID: 18772 RVA: 0x0012D85B File Offset: 0x0012BA5B
		// (set) Token: 0x06004955 RID: 18773 RVA: 0x0012D881 File Offset: 0x0012BA81
		[Parameter(ParameterSetName = "ArbitraryCollection", Mandatory = false, ValueFromPipeline = false)]
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

		// Token: 0x17001616 RID: 5654
		// (get) Token: 0x06004956 RID: 18774 RVA: 0x0012D899 File Offset: 0x0012BA99
		// (set) Token: 0x06004957 RID: 18775 RVA: 0x0012D8BF File Offset: 0x0012BABF
		[Parameter(ParameterSetName = "OutOfBoxInstall", Mandatory = true)]
		public SwitchParameter InstallDefaultCollection
		{
			get
			{
				return (SwitchParameter)(base.Fields["InstallDefaultCollection"] ?? false);
			}
			set
			{
				base.Fields["InstallDefaultCollection"] = value;
			}
		}

		// Token: 0x17001617 RID: 5655
		// (get) Token: 0x06004958 RID: 18776 RVA: 0x0012D8D7 File Offset: 0x0012BAD7
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if (this.FileData != null)
				{
					return Strings.ConfirmationMessageNewClassificationRuleCollection(this.localizedName);
				}
				return LocalizedString.Empty;
			}
		}

		// Token: 0x06004959 RID: 18777 RVA: 0x0012D8F4 File Offset: 0x0012BAF4
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			TransportRule transportRule;
			if (this.InstallDefaultCollection && this.existingRulePack != null)
			{
				transportRule = this.existingRulePack;
			}
			else
			{
				transportRule = (TransportRule)base.PrepareDataObject();
				transportRule.SetId(ClassificationDefinitionUtils.GetClassificationRuleCollectionContainerId(base.DataSession).GetChildId(this.rulePackageIdentifier));
				transportRule.OrganizationId = base.CurrentOrganizationId;
			}
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
			transportRule.Xml = null;
			transportRule.ReplicationSignature = replicationSignature;
			TaskLogger.LogExit();
			return transportRule;
		}

		// Token: 0x0600495A RID: 18778 RVA: 0x0012D9AC File Offset: 0x0012BBAC
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			if (this.InstallDefaultCollection)
			{
				using (Stream stream = ClassificationDefinitionUtils.LoadStreamFromEmbeddedResource("DefaultClassificationDefinitions.xml"))
				{
					byte[] array = new byte[stream.Length];
					stream.Read(array, 0, Convert.ToInt32(stream.Length));
					this.FileData = array;
					goto IL_71;
				}
			}
			if (VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled)
			{
				this.ValidateOperationScope();
			}
			IL_71:
			byte[] rulePackageRawData = null;
			byte[] array2 = null;
			try
			{
				this.isEncrypted = RulePackageDecrypter.DecryptRulePackage(this.FileData, out rulePackageRawData, out array2);
			}
			catch (Exception innerException)
			{
				base.WriteError(new ClassificationRuleCollectionDecryptionException(innerException), ErrorCategory.InvalidData, null);
			}
			if (this.isEncrypted)
			{
				ExAssert.RetailAssert(!this.InstallDefaultCollection, "Installation of encrypted default OOB rule pack is not supported due to versioning!");
				string text = this.ValidateAndReadMetadata(rulePackageRawData);
				this.FileData = ((text == null) ? null : array2);
			}
			else
			{
				string text2 = this.ValidateAndReadMetadata(this.FileData);
				this.FileData = ((text2 == null) ? null : Encoding.Unicode.GetBytes(text2));
			}
			if (this.FileData != null)
			{
				base.InternalValidate();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600495B RID: 18779 RVA: 0x0012DAE8 File Offset: 0x0012BCE8
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (this.FileData == null)
			{
				return;
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}

		// Token: 0x0600495C RID: 18780 RVA: 0x0012DB04 File Offset: 0x0012BD04
		protected override void WriteResult(IConfigurable result)
		{
			TaskLogger.LogEnter(new object[]
			{
				result.Identity
			});
			IConfigurable result2 = ClassificationRuleCollectionPresentationObject.Create((TransportRule)result, this.rulePackVersion, this.rulePackageDetailsElement, this.isEncrypted);
			base.WriteResult(result2);
			TaskLogger.LogExit();
		}

		// Token: 0x0600495D RID: 18781 RVA: 0x0012DB54 File Offset: 0x0012BD54
		private void ValidateOperationScope()
		{
			ExAssert.RetailAssert(!this.InstallDefaultCollection, "Shouldn't validate New-ClassificationRuleCollection scope when installing default rule collection");
			bool flag = this.OutOfBoxCollection;
			bool flag2 = OrganizationId.ForestWideOrgId.Equals(base.CurrentOrganizationId);
			if (flag && !flag2)
			{
				base.WriteError(new ClassificationRuleCollectionIllegalScopeException(Strings.ClassificationRuleCollectionIllegalScopedNewOobOperation), ErrorCategory.InvalidOperation, null);
				return;
			}
			if (!flag && flag2)
			{
				base.WriteError(new ClassificationRuleCollectionIllegalScopeException(Strings.ClassificationRuleCollectionIllegalScopedNewOperation), ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x0600495E RID: 18782 RVA: 0x0012DBDC File Offset: 0x0012BDDC
		private string ValidateAndReadMetadata(byte[] rulePackageRawData)
		{
			string result = null;
			try
			{
				XDocument xdocument = XmlProcessingUtils.ValidateRulePackageXmlContentsLite(rulePackageRawData);
				this.rulePackageIdentifier = XmlProcessingUtils.GetRulePackId(xdocument);
				this.rulePackVersion = (this.InstallDefaultCollection ? XmlProcessingUtils.SetRulePackVersionFromAssemblyFileVersion(xdocument) : XmlProcessingUtils.GetRulePackVersion(xdocument));
				ClassificationRuleCollectionIdParameter classificationRuleCollectionIdParameter = ClassificationRuleCollectionIdParameter.Parse("*");
				classificationRuleCollectionIdParameter.ShouldIncludeOutOfBoxCollections = true;
				List<TransportRule> list = base.GetDataObjects<TransportRule>(classificationRuleCollectionIdParameter, base.DataSession, ClassificationDefinitionUtils.GetClassificationRuleCollectionContainerId(base.DataSession)).ToList<TransportRule>();
				this.existingRulePack = list.FirstOrDefault((TransportRule transportRule) => transportRule.Name.Equals(this.rulePackageIdentifier, StringComparison.OrdinalIgnoreCase));
				this.validationContext = new ValidationContext(this.InstallDefaultCollection ? ClassificationRuleCollectionOperationType.ImportOrUpdate : ClassificationRuleCollectionOperationType.Import, base.CurrentOrganizationId, this.InstallDefaultCollection || (VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled && this.OutOfBoxCollection), false, (IConfigurationSession)base.DataSession, this.existingRulePack, null, null);
				if (this.validationContext.DcValidationConfig != null && list.Count >= this.validationContext.DcValidationConfig.MaxRulePackages)
				{
					base.WriteError(new ClassificationRuleCollectionNumberExceedLimit(this.validationContext.DcValidationConfig.MaxRulePackages), ErrorCategory.InvalidOperation, null);
				}
				result = ClassificationRuleCollectionValidationUtils.ValidateRulePackageContents(this.validationContext, xdocument);
				this.rulePackageDetailsElement = XmlProcessingUtils.GetRulePackageMetadataElement(xdocument);
				ClassificationRuleCollectionLocalizableDetails classificationRuleCollectionLocalizableDetails = XmlProcessingUtils.ReadDefaultRulePackageMetadata(this.rulePackageDetailsElement);
				this.defaultName = classificationRuleCollectionLocalizableDetails.Name;
				ClassificationRuleCollectionLocalizableDetails classificationRuleCollectionLocalizableDetails2 = XmlProcessingUtils.ReadRulePackageMetadata(this.rulePackageDetailsElement, CultureInfo.CurrentCulture);
				this.localizedName = ((classificationRuleCollectionLocalizableDetails2 != null && classificationRuleCollectionLocalizableDetails2.Name != null) ? classificationRuleCollectionLocalizableDetails2.Name : this.defaultName);
			}
			catch (ClassificationRuleCollectionVersionValidationException ex)
			{
				this.WriteWarning(ex.LocalizedString);
			}
			catch (ClassificationRuleCollectionAlreadyExistsException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidOperation, null);
			}
			catch (ClassificationRuleCollectionInternalValidationException ex2)
			{
				base.WriteError(ex2, (-2147287038 == ex2.Error) ? ErrorCategory.ObjectNotFound : ErrorCategory.InvalidResult, null);
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

		// Token: 0x04002C3F RID: 11327
		private string rulePackageIdentifier;

		// Token: 0x04002C40 RID: 11328
		private Version rulePackVersion;

		// Token: 0x04002C41 RID: 11329
		private XElement rulePackageDetailsElement;

		// Token: 0x04002C42 RID: 11330
		private string defaultName;

		// Token: 0x04002C43 RID: 11331
		private string localizedName;

		// Token: 0x04002C44 RID: 11332
		private bool isEncrypted;

		// Token: 0x04002C45 RID: 11333
		private ValidationContext validationContext;

		// Token: 0x04002C46 RID: 11334
		private TransportRule existingRulePack;
	}
}
