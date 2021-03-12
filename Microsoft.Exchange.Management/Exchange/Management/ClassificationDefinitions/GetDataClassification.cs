using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x0200083A RID: 2106
	[Cmdlet("Get", "DataClassification", DefaultParameterSetName = "Identity")]
	[CmdletBinding(DefaultParameterSetName = "Identity")]
	public sealed class GetDataClassification : GetMultitenancySystemConfigurationObjectTask<DataClassificationIdParameter, TransportRule>
	{
		// Token: 0x17001604 RID: 5636
		// (get) Token: 0x0600490F RID: 18703 RVA: 0x0012C303 File Offset: 0x0012A503
		// (set) Token: 0x06004910 RID: 18704 RVA: 0x0012C31A File Offset: 0x0012A51A
		[Parameter(Mandatory = false, ParameterSetName = "RuleCollectionIdentity", ValueFromPipelineByPropertyName = false, ValueFromPipeline = false, Position = 0)]
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

		// Token: 0x17001605 RID: 5637
		// (get) Token: 0x06004911 RID: 18705 RVA: 0x0012C32D File Offset: 0x0012A52D
		protected override ObjectId RootId
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004912 RID: 18706 RVA: 0x0012C330 File Offset: 0x0012A530
		protected override void InternalValidate()
		{
			if (base.OptionalIdentityData != null)
			{
				base.OptionalIdentityData.ConfigurationContainerRdn = ClassificationDefinitionConstants.ClassificationDefinitionsRdn;
			}
			if (this.Identity == null && this.ClassificationRuleCollectionIdentity == null)
			{
				this.isIdentityArgumentSpecified = false;
				this.Identity = DataClassificationIdParameter.Parse("*");
			}
			if (this.Identity != null)
			{
				this.Identity.ShouldIncludeOutOfBoxCollections = true;
			}
			if (this.ClassificationRuleCollectionIdentity != null)
			{
				this.ClassificationRuleCollectionIdentity.ShouldIncludeOutOfBoxCollections = true;
			}
			base.InternalValidate();
		}

		// Token: 0x06004913 RID: 18707 RVA: 0x0012C3AC File Offset: 0x0012A5AC
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (this.ClassificationRuleCollectionIdentity != null)
			{
				LocalizedString? localizedString;
				IEnumerable<TransportRule> dataObjects = base.GetDataObjects<TransportRule>(this.ClassificationRuleCollectionIdentity, base.DataSession, this.RootId, base.OptionalIdentityData, out localizedString);
				this.writeRuleCollectionCount = 0;
				this.WriteResult<TransportRule>(dataObjects);
				if (!base.HasErrors && base.WriteObjectCount == 0U)
				{
					LocalizedString message = LocalizedString.Empty;
					if (this.writeRuleCollectionCount > 0)
					{
						message = Strings.DataClassificationRequiresHigherServerVersion(this.ClassificationRuleCollectionIdentity.ToString());
					}
					else
					{
						message = (localizedString ?? base.GetErrorMessageObjectNotFound(this.ClassificationRuleCollectionIdentity.ToString(), typeof(TransportRule).ToString(), (base.DataSession != null) ? base.DataSession.Source : null));
					}
					base.WriteError(new ManagementObjectNotFoundException(message), (ErrorCategory)1003, null);
				}
			}
			else
			{
				try
				{
					base.InternalProcessRecord();
				}
				catch (ManagementObjectNotFoundException)
				{
					if (this.isIdentityArgumentSpecified)
					{
						throw;
					}
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06004914 RID: 18708 RVA: 0x0012C4BC File Offset: 0x0012A6BC
		protected override void WriteResult<T>(IEnumerable<T> dataObjects)
		{
			TaskLogger.LogEnter();
			if (dataObjects is IEnumerable<DataClassificationPresentationObject>)
			{
				IEnumerable<DataClassificationPresentationObject> enumerable = ClassificationDefinitionUtils.FilterHigherVersionRules((IEnumerable<DataClassificationPresentationObject>)dataObjects);
				if (this.Identity != null)
				{
					enumerable = this.Identity.FilterResults(enumerable);
				}
				base.WriteResult<DataClassificationPresentationObject>(enumerable);
			}
			else
			{
				base.WriteResult<T>(dataObjects);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06004915 RID: 18709 RVA: 0x0012C50C File Offset: 0x0012A70C
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter();
			TransportRule transportRule = dataObject as TransportRule;
			if (transportRule != null)
			{
				XDocument xdocument = this.TryGetRuleCollectionDocumentFromTransportRule(transportRule);
				if (xdocument == null)
				{
					TaskLogger.LogExit();
					return;
				}
				List<DataClassificationPresentationObject> list = this.TryCreateDataClassificationPresentationObjects(transportRule, xdocument);
				if (list == null || list.Count == 0)
				{
					TaskLogger.LogExit();
					return;
				}
				this.WriteResult<DataClassificationPresentationObject>(list);
				this.writeRuleCollectionCount++;
			}
			else
			{
				base.WriteResult(dataObject);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06004916 RID: 18710 RVA: 0x0012C578 File Offset: 0x0012A778
		private XDocument TryGetRuleCollectionDocumentFromTransportRule(TransportRule transportRule)
		{
			XDocument result = null;
			try
			{
				result = ClassificationDefinitionUtils.GetRuleCollectionDocumentFromTransportRule(transportRule);
			}
			catch (InvalidOperationException)
			{
				ClassificationDefinitionsDiagnosticsReporter.Instance.WriteInvalidObjectInformation(this.GetHashCode(), transportRule.OrganizationId, transportRule.DistinguishedName);
			}
			catch (ArgumentException underlyingException)
			{
				ClassificationDefinitionsDiagnosticsReporter.Instance.WriteCorruptRulePackageDiagnosticsInformation(this.GetHashCode(), transportRule.OrganizationId, transportRule.DistinguishedName, underlyingException);
			}
			catch (AggregateException ex)
			{
				ClassificationDefinitionsDiagnosticsReporter.Instance.WriteCorruptRulePackageDiagnosticsInformation(this.GetHashCode(), transportRule.OrganizationId, transportRule.DistinguishedName, ex.Flatten());
			}
			catch (XmlException ex2)
			{
				ClassificationDefinitionsDiagnosticsReporter.Instance.WriteCorruptRulePackageDiagnosticsInformation(this.GetHashCode(), transportRule.OrganizationId, transportRule.DistinguishedName, new AggregateException(new Exception[]
				{
					ex2
				}).Flatten());
			}
			return result;
		}

		// Token: 0x06004917 RID: 18711 RVA: 0x0012C664 File Offset: 0x0012A864
		private List<DataClassificationPresentationObject> TryCreateDataClassificationPresentationObjects(TransportRule transportRuleObject, XDocument rulePackXDoc)
		{
			List<DataClassificationPresentationObject> result = null;
			try
			{
				result = GetDataClassification.CreateDataClassificationPresentationObjects(transportRuleObject, rulePackXDoc);
			}
			catch (XmlException ex)
			{
				ClassificationDefinitionsDiagnosticsReporter.Instance.WriteCorruptRulePackageDiagnosticsInformation(this.GetHashCode(), transportRuleObject.OrganizationId, transportRuleObject.DistinguishedName, new AggregateException(new Exception[]
				{
					ex
				}).Flatten());
			}
			return result;
		}

		// Token: 0x06004918 RID: 18712 RVA: 0x0012C6EC File Offset: 0x0012A8EC
		private static List<DataClassificationPresentationObject> CreateDataClassificationPresentationObjects(TransportRule transportRuleObject, XDocument rulePackXDoc)
		{
			ClassificationRuleCollectionPresentationObject rulePackPresentationObject = ClassificationRuleCollectionPresentationObject.Create(transportRuleObject, rulePackXDoc);
			IEnumerable<QueryMatchResult> matchingRulesById = XmlProcessingUtils.GetMatchingRulesById(rulePackXDoc, null);
			return (from rawDataClassificationResult in matchingRulesById
			select DataClassificationPresentationObject.Create(rawDataClassificationResult.MatchingRuleId, rawDataClassificationResult.MatchingRuleXElement, rawDataClassificationResult.MatchingResourceXElement, rulePackPresentationObject)).ToList<DataClassificationPresentationObject>();
		}

		// Token: 0x04002C31 RID: 11313
		private bool isIdentityArgumentSpecified = true;

		// Token: 0x04002C32 RID: 11314
		private int writeRuleCollectionCount;
	}
}
