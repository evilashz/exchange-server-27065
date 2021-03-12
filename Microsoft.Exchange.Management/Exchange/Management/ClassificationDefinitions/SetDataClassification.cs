using System;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x0200083E RID: 2110
	[Cmdlet("Set", "DataClassification", SupportsShouldProcess = true)]
	public sealed class SetDataClassification : SetSystemConfigurationObjectTask<DataClassificationIdParameter, TransportRule>
	{
		// Token: 0x1700160F RID: 5647
		// (get) Token: 0x06004941 RID: 18753 RVA: 0x0012D3CD File Offset: 0x0012B5CD
		// (set) Token: 0x06004942 RID: 18754 RVA: 0x0012D3E4 File Offset: 0x0012B5E4
		[Parameter]
		public string Name
		{
			get
			{
				return (string)base.Fields["Name"];
			}
			set
			{
				base.Fields["Name"] = value;
			}
		}

		// Token: 0x17001610 RID: 5648
		// (get) Token: 0x06004943 RID: 18755 RVA: 0x0012D3F7 File Offset: 0x0012B5F7
		// (set) Token: 0x06004944 RID: 18756 RVA: 0x0012D40E File Offset: 0x0012B60E
		[Parameter]
		public string Description
		{
			get
			{
				return (string)base.Fields["Description"];
			}
			set
			{
				base.Fields["Description"] = value;
			}
		}

		// Token: 0x17001611 RID: 5649
		// (get) Token: 0x06004945 RID: 18757 RVA: 0x0012D421 File Offset: 0x0012B621
		// (set) Token: 0x06004946 RID: 18758 RVA: 0x0012D438 File Offset: 0x0012B638
		[Parameter]
		[ValidateNotNull]
		public CultureInfo Locale
		{
			get
			{
				return (CultureInfo)base.Fields["Locale"];
			}
			set
			{
				base.Fields["Locale"] = value;
			}
		}

		// Token: 0x17001612 RID: 5650
		// (get) Token: 0x06004947 RID: 18759 RVA: 0x0012D44B File Offset: 0x0012B64B
		// (set) Token: 0x06004948 RID: 18760 RVA: 0x0012D471 File Offset: 0x0012B671
		[Parameter]
		public SwitchParameter IsDefault
		{
			get
			{
				return (SwitchParameter)(base.Fields["IsDefault"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["IsDefault"] = value;
			}
		}

		// Token: 0x17001613 RID: 5651
		// (get) Token: 0x06004949 RID: 18761 RVA: 0x0012D489 File Offset: 0x0012B689
		// (set) Token: 0x0600494A RID: 18762 RVA: 0x0012D4A0 File Offset: 0x0012B6A0
		[Parameter]
		public MultiValuedProperty<Fingerprint> Fingerprints
		{
			get
			{
				return (MultiValuedProperty<Fingerprint>)base.Fields["Fingerprints"];
			}
			set
			{
				base.Fields["Fingerprints"] = value;
			}
		}

		// Token: 0x0600494B RID: 18763 RVA: 0x0012D4B3 File Offset: 0x0012B6B3
		public override object GetDynamicParameters()
		{
			return null;
		}

		// Token: 0x0600494C RID: 18764 RVA: 0x0012D4B8 File Offset: 0x0012B6B8
		protected override void InternalValidate()
		{
			if (base.OptionalIdentityData != null)
			{
				base.OptionalIdentityData.ConfigurationContainerRdn = ClassificationDefinitionConstants.ClassificationDefinitionsRdn;
			}
			if (base.UserSpecifiedParameters.Contains("Locale") && !base.UserSpecifiedParameters.Contains("Name") && !base.UserSpecifiedParameters.Contains("Description") && !base.UserSpecifiedParameters.Contains("IsDefault"))
			{
				base.WriteError(new ErrorMissingNameOrDescriptionOrIsDefaultParametersException(), ErrorCategory.InvalidOperation, null);
			}
			if (this.Locale == null)
			{
				this.Locale = CultureInfo.CurrentCulture;
			}
			this.ValidateParameterValueContraints("Name", this.Name);
			this.ValidateParameterValueContraints("Description", this.Description);
			base.InternalValidate();
		}

		// Token: 0x0600494D RID: 18765 RVA: 0x0012D570 File Offset: 0x0012B770
		protected override IConfigurable ResolveDataObject()
		{
			TaskLogger.LogEnter();
			this.implementation = new DataClassificationCmdletsImplementation(this);
			TransportRule transportRule = this.implementation.Initialize(base.DataSession, this.Identity, base.OptionalIdentityData);
			if (TaskHelper.ShouldUnderscopeDataSessionToOrganization((IDirectorySession)base.DataSession, transportRule))
			{
				base.UnderscopeDataSession(transportRule.OrganizationId);
				base.CurrentOrganizationId = transportRule.OrganizationId;
			}
			TaskLogger.LogExit();
			return transportRule;
		}

		// Token: 0x0600494E RID: 18766 RVA: 0x0012D5E0 File Offset: 0x0012B7E0
		protected override void InternalProcessRecord()
		{
			if (this.IsDefault || this.Locale.Equals(this.implementation.DataClassificationPresentationObject.DefaultCulture))
			{
				if (!base.UserSpecifiedParameters.Contains("Name") && this.implementation.DataClassificationPresentationObject.AllLocalizedNames.ContainsKey(this.Locale))
				{
					this.Name = this.implementation.DataClassificationPresentationObject.AllLocalizedNames[this.Locale];
				}
				if (!base.UserSpecifiedParameters.Contains("Description") && this.implementation.DataClassificationPresentationObject.AllLocalizedDescriptions.ContainsKey(this.Locale))
				{
					this.Description = this.implementation.DataClassificationPresentationObject.AllLocalizedDescriptions[this.Locale];
				}
				if (string.IsNullOrEmpty(this.Name) || string.IsNullOrEmpty(this.Description))
				{
					base.WriteError(new InvalidNameOrDescriptionForDefaultLocaleException(), ErrorCategory.InvalidOperation, null);
				}
				this.implementation.DataClassificationPresentationObject.SetDefaultResource(this.Locale, this.Name, this.Description);
			}
			else
			{
				if (base.Fields.IsModified("Name"))
				{
					this.implementation.DataClassificationPresentationObject.SetLocalizedName(this.Locale, this.Name);
				}
				if (base.Fields.IsModified("Description"))
				{
					this.implementation.DataClassificationPresentationObject.SetLocalizedDescription(this.Locale, this.Description);
				}
			}
			if (base.Fields.IsModified("Fingerprints"))
			{
				this.implementation.DataClassificationPresentationObject.Fingerprints = this.Fingerprints;
			}
			ValidationContext validationContext = new ValidationContext(ClassificationRuleCollectionOperationType.Update, base.CurrentOrganizationId, false, true, (IConfigurationSession)base.DataSession, this.DataObject, null, null);
			this.implementation.Save(validationContext);
			if (this.IsObjectStateChanged())
			{
				base.InternalProcessRecord();
				return;
			}
			bool flag = false;
			if (!base.TryGetVariableValue<bool>("ExchangeDisableNotChangedWarning", out flag) && !flag)
			{
				this.WriteWarning(Strings.WarningForceMessageWithId(this.Identity.ToString()));
			}
		}

		// Token: 0x0600494F RID: 18767 RVA: 0x0012D7F0 File Offset: 0x0012B9F0
		private void ValidateParameterValueContraints(string parameterName, string parameterValue)
		{
			int num = 256;
			if (!string.IsNullOrEmpty(parameterValue) && parameterValue.Length > num)
			{
				base.WriteError(new ErrorInvalidNameOrDescriptionParametersException(parameterName, parameterValue.Length, num), ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x04002C3E RID: 11326
		private DataClassificationCmdletsImplementation implementation;
	}
}
