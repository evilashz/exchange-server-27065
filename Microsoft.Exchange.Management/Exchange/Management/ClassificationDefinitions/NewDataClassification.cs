using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management.Automation;
using System.Xml.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x0200083B RID: 2107
	[Cmdlet("New", "DataClassification", SupportsShouldProcess = true)]
	public sealed class NewDataClassification : NewMultitenancyFixedNameSystemConfigurationObjectTask<TransportRule>
	{
		// Token: 0x17001606 RID: 5638
		// (get) Token: 0x0600491A RID: 18714 RVA: 0x0012C73A File Offset: 0x0012A93A
		// (set) Token: 0x0600491B RID: 18715 RVA: 0x0012C751 File Offset: 0x0012A951
		[ValidateLength(1, 256)]
		[Parameter(Mandatory = true, Position = 0)]
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

		// Token: 0x17001607 RID: 5639
		// (get) Token: 0x0600491C RID: 18716 RVA: 0x0012C764 File Offset: 0x0012A964
		// (set) Token: 0x0600491D RID: 18717 RVA: 0x0012C77B File Offset: 0x0012A97B
		[ValidateLength(1, 256)]
		[Parameter(Mandatory = true)]
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

		// Token: 0x17001608 RID: 5640
		// (get) Token: 0x0600491E RID: 18718 RVA: 0x0012C78E File Offset: 0x0012A98E
		// (set) Token: 0x0600491F RID: 18719 RVA: 0x0012C7A5 File Offset: 0x0012A9A5
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

		// Token: 0x17001609 RID: 5641
		// (get) Token: 0x06004920 RID: 18720 RVA: 0x0012C7B8 File Offset: 0x0012A9B8
		// (set) Token: 0x06004921 RID: 18721 RVA: 0x0012C7CF File Offset: 0x0012A9CF
		[Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
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

		// Token: 0x1700160A RID: 5642
		// (get) Token: 0x06004922 RID: 18722 RVA: 0x0012C7E2 File Offset: 0x0012A9E2
		// (set) Token: 0x06004923 RID: 18723 RVA: 0x0012C7F9 File Offset: 0x0012A9F9
		[Parameter]
		public ClassificationRuleCollectionIdParameter ClassificationRuleCollectionIdentity
		{
			get
			{
				return (ClassificationRuleCollectionIdParameter)base.Fields["ClassificationRuleCollectionIdentity"];
			}
			set
			{
				base.Fields["ClassificationRuleCollectionIdentity"] = value;
			}
		}

		// Token: 0x1700160B RID: 5643
		// (get) Token: 0x06004924 RID: 18724 RVA: 0x0012C80C File Offset: 0x0012AA0C
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewDataClassification(this.Name);
			}
		}

		// Token: 0x06004925 RID: 18725 RVA: 0x0012C819 File Offset: 0x0012AA19
		protected override void InternalValidate()
		{
			this.ClassificationRuleCollectionIdentity = ClassificationRuleCollectionIdParameter.Parse("00000000-0000-0000-0001-000000000001");
			if (this.Locale == null)
			{
				this.Locale = CultureInfo.CurrentCulture;
			}
			base.InternalValidate();
		}

		// Token: 0x06004926 RID: 18726 RVA: 0x0012C844 File Offset: 0x0012AA44
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			TransportRule transportRule = null;
			try
			{
				transportRule = this.TryGetDataObject();
				XDocument rulePackXDoc;
				if (transportRule == null)
				{
					transportRule = (TransportRule)base.PrepareDataObject();
					string rawIdentity = this.ClassificationRuleCollectionIdentity.RawIdentity;
					transportRule.SetId(ClassificationDefinitionUtils.GetClassificationRuleCollectionContainerId(base.DataSession).GetChildId(rawIdentity));
					transportRule.OrganizationId = base.CurrentOrganizationId;
					transportRule.Xml = null;
					string organizationId;
					string name;
					if (base.CurrentOrganizationId != null && base.CurrentOrganizationId.OrganizationalUnit != null)
					{
						organizationId = base.CurrentOrganizationId.OrganizationalUnit.ObjectGuid.ToString();
						name = base.CurrentOrganizationId.OrganizationalUnit.Name;
					}
					else
					{
						organizationId = base.CurrentOrgContainerId.ObjectGuid.ToString();
						name = base.CurrentOrgContainerId.DomainId.Name;
					}
					rulePackXDoc = ClassificationDefinitionUtils.CreateRuleCollectionDocumentFromTemplate(rawIdentity, organizationId, name);
				}
				else
				{
					rulePackXDoc = ClassificationDefinitionUtils.GetRuleCollectionDocumentFromTransportRule(transportRule);
				}
				this.implementation = new DataClassificationCmdletsImplementation(this);
				this.implementation.Initialize(transportRule, rulePackXDoc);
			}
			catch (LocalizedException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidOperation, null);
			}
			TaskLogger.LogExit();
			return transportRule;
		}

		// Token: 0x06004927 RID: 18727 RVA: 0x0012C978 File Offset: 0x0012AB78
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			this.implementation.DataClassificationPresentationObject.SetDefaultResource(this.Locale, this.Name, this.Description);
			this.implementation.DataClassificationPresentationObject.Fingerprints = this.Fingerprints;
			ValidationContext validationContext = new ValidationContext(ClassificationRuleCollectionOperationType.ImportOrUpdate, base.CurrentOrganizationId, false, true, (IConfigurationSession)base.DataSession, null, null, null);
			this.implementation.Save(validationContext);
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}

		// Token: 0x06004928 RID: 18728 RVA: 0x0012C9F8 File Offset: 0x0012ABF8
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter();
			TransportRule transportRule = dataObject as TransportRule;
			if (transportRule != null)
			{
				XDocument ruleCollectionDocumentFromTransportRule = ClassificationDefinitionUtils.GetRuleCollectionDocumentFromTransportRule(transportRule);
				if (ruleCollectionDocumentFromTransportRule != null)
				{
					string[] ruleIdQueries = new string[]
					{
						((DataClassificationObjectId)this.implementation.DataClassificationPresentationObject.Identity).Name
					};
					List<QueryMatchResult> list = XmlProcessingUtils.GetMatchingRulesById(ruleCollectionDocumentFromTransportRule, ruleIdQueries).ToList<QueryMatchResult>();
					if (list.Count > 0)
					{
						ClassificationRuleCollectionPresentationObject rulePackPresentationObject = ClassificationRuleCollectionPresentationObject.Create(transportRule);
						DataClassificationPresentationObject result = DataClassificationPresentationObject.Create(list[0].MatchingRuleId, list[0].MatchingRuleXElement, list[0].MatchingResourceXElement, rulePackPresentationObject);
						this.WriteResult(result);
					}
				}
			}
			else
			{
				base.WriteResult(dataObject);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06004929 RID: 18729 RVA: 0x0012CAB0 File Offset: 0x0012ACB0
		private TransportRule TryGetDataObject()
		{
			TransportRule result = null;
			try
			{
				result = (TransportRule)base.GetDataObject(this.ClassificationRuleCollectionIdentity);
			}
			catch (ManagementObjectNotFoundException)
			{
			}
			return result;
		}

		// Token: 0x04002C33 RID: 11315
		private DataClassificationCmdletsImplementation implementation;
	}
}
