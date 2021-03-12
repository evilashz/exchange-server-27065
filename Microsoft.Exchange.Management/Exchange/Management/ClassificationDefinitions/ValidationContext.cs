using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x02000853 RID: 2131
	internal class ValidationContext
	{
		// Token: 0x1700162B RID: 5675
		// (get) Token: 0x060049EE RID: 18926 RVA: 0x00130571 File Offset: 0x0012E771
		// (set) Token: 0x060049EF RID: 18927 RVA: 0x00130579 File Offset: 0x0012E779
		internal ClassificationRuleCollectionOperationType OperationType { get; private set; }

		// Token: 0x1700162C RID: 5676
		// (get) Token: 0x060049F0 RID: 18928 RVA: 0x00130582 File Offset: 0x0012E782
		// (set) Token: 0x060049F1 RID: 18929 RVA: 0x0013058A File Offset: 0x0012E78A
		internal bool IsPayloadOobRuleCollection { get; private set; }

		// Token: 0x1700162D RID: 5677
		// (get) Token: 0x060049F2 RID: 18930 RVA: 0x00130593 File Offset: 0x0012E793
		// (set) Token: 0x060049F3 RID: 18931 RVA: 0x0013059B File Offset: 0x0012E79B
		internal bool IsPayloadFingerprintsRuleCollection { get; private set; }

		// Token: 0x1700162E RID: 5678
		// (get) Token: 0x060049F4 RID: 18932 RVA: 0x001305A4 File Offset: 0x0012E7A4
		// (set) Token: 0x060049F5 RID: 18933 RVA: 0x001305AC File Offset: 0x0012E7AC
		internal OrganizationId CurrentOrganizationId { get; private set; }

		// Token: 0x1700162F RID: 5679
		// (get) Token: 0x060049F6 RID: 18934 RVA: 0x001305B5 File Offset: 0x0012E7B5
		internal DataClassificationConfig DcValidationConfig
		{
			get
			{
				if (this.organizationValidationConfig == null && !this.organizationValidationConfigRead)
				{
					this.organizationValidationConfig = this.classificationsDataReader.GetDataClassificationConfig(this.CurrentOrganizationId, this.dataSession);
					this.organizationValidationConfigRead = true;
				}
				return this.organizationValidationConfig;
			}
		}

		// Token: 0x17001630 RID: 5680
		// (get) Token: 0x060049F7 RID: 18935 RVA: 0x001305F1 File Offset: 0x0012E7F1
		// (set) Token: 0x060049F8 RID: 18936 RVA: 0x001305F9 File Offset: 0x0012E7F9
		internal TransportRule ExistingRulePackDataObject { get; private set; }

		// Token: 0x17001631 RID: 5681
		// (get) Token: 0x060049F9 RID: 18937 RVA: 0x00130602 File Offset: 0x0012E802
		// (set) Token: 0x060049FA RID: 18938 RVA: 0x0013060A File Offset: 0x0012E80A
		internal string ValidatedRuleCollectionDocument { get; set; }

		// Token: 0x060049FB RID: 18939 RVA: 0x00130614 File Offset: 0x0012E814
		internal Dictionary<string, HashSet<string>> GetAllExistingClassificationIdentifiers(Func<TransportRule, bool> inclusiveFilter = null)
		{
			OrganizationId currentOrganizationId = this.CurrentOrganizationId;
			IConfigurationSession openedDataSession = this.dataSession;
			IClassificationDefinitionsDataReader dataReader = this.classificationsDataReader;
			IClassificationDefinitionsDiagnosticsReporter classificationDefinitionsDiagnosticsReporter = this.classificationDiagnosticsReporter;
			return DlpUtils.GetAllClassificationIdentifiers(currentOrganizationId, openedDataSession, inclusiveFilter, null, dataReader, classificationDefinitionsDiagnosticsReporter);
		}

		// Token: 0x060049FC RID: 18940 RVA: 0x00130644 File Offset: 0x0012E844
		internal Version GetExistingRulePackVersion()
		{
			if (this.existingrulePackXDocument == null)
			{
				return null;
			}
			return XmlProcessingUtils.GetRulePackVersion(this.existingrulePackXDocument);
		}

		// Token: 0x060049FD RID: 18941 RVA: 0x0013065B File Offset: 0x0012E85B
		internal ISet<string> GetRuleIdentifiersFromExistingRulePack()
		{
			if (this.existingrulePackXDocument == null)
			{
				return null;
			}
			return new HashSet<string>(XmlProcessingUtils.GetAllRuleIds(this.existingrulePackXDocument), ClassificationDefinitionConstants.RuleIdComparer);
		}

		// Token: 0x060049FE RID: 18942 RVA: 0x0013067C File Offset: 0x0012E87C
		private void InitializeExistingRulePack(TransportRule existingRulePack)
		{
			this.ExistingRulePackDataObject = existingRulePack;
			if (ClassificationRuleCollectionOperationType.Update == this.OperationType)
			{
				ExAssert.RetailAssert(existingRulePack != null, "ValidationContext constructor must check that there's an existing rule pack data object for update operation.");
			}
			if (this.OperationType == ClassificationRuleCollectionOperationType.Import || existingRulePack == null)
			{
				return;
			}
			XDocument xdocument = null;
			try
			{
				xdocument = ClassificationDefinitionUtils.GetRuleCollectionDocumentFromTransportRule(existingRulePack);
			}
			catch (InvalidOperationException)
			{
				this.classificationDiagnosticsReporter.WriteInvalidObjectInformation(this.GetHashCode(), existingRulePack.OrganizationId, existingRulePack.DistinguishedName);
			}
			catch (ArgumentException underlyingException)
			{
				this.classificationDiagnosticsReporter.WriteCorruptRulePackageDiagnosticsInformation(this.GetHashCode(), existingRulePack.OrganizationId, existingRulePack.DistinguishedName, underlyingException);
			}
			catch (AggregateException ex)
			{
				this.classificationDiagnosticsReporter.WriteCorruptRulePackageDiagnosticsInformation(this.GetHashCode(), existingRulePack.OrganizationId, existingRulePack.DistinguishedName, ex.Flatten());
			}
			catch (XmlException ex2)
			{
				this.classificationDiagnosticsReporter.WriteCorruptRulePackageDiagnosticsInformation(this.GetHashCode(), existingRulePack.OrganizationId, existingRulePack.DistinguishedName, new AggregateException(new Exception[]
				{
					ex2
				}).Flatten());
			}
			this.existingrulePackXDocument = xdocument;
		}

		// Token: 0x060049FF RID: 18943 RVA: 0x001307A4 File Offset: 0x0012E9A4
		internal ValidationContext(ClassificationRuleCollectionOperationType operationType, OrganizationId currentOrganizationId, bool isPayloadOobRuleCollection, bool isPayloadFingerprintsRuleCollection, IConfigurationSession currentDataSession, IConfigurable existingDataObject = null, IClassificationDefinitionsDataReader dataReader = null, IClassificationDefinitionsDiagnosticsReporter diagnosticsReporter = null)
		{
			if (object.ReferenceEquals(null, currentOrganizationId))
			{
				throw new ArgumentNullException("currentOrganizationId");
			}
			TransportRule transportRule = existingDataObject as TransportRule;
			if (ClassificationRuleCollectionOperationType.Update == operationType && transportRule == null)
			{
				throw new ArgumentException("existingDataObject");
			}
			this.dataSession = currentDataSession;
			this.classificationsDataReader = (dataReader ?? ClassificationDefinitionsDataReader.DefaultInstance);
			this.classificationDiagnosticsReporter = (diagnosticsReporter ?? ClassificationDefinitionsDiagnosticsReporter.Instance);
			this.organizationValidationConfigRead = false;
			this.organizationValidationConfig = null;
			this.OperationType = operationType;
			this.CurrentOrganizationId = currentOrganizationId;
			this.IsPayloadOobRuleCollection = isPayloadOobRuleCollection;
			this.IsPayloadFingerprintsRuleCollection = isPayloadFingerprintsRuleCollection;
			this.InitializeExistingRulePack(transportRule);
			this.ValidatedRuleCollectionDocument = null;
		}

		// Token: 0x04002C84 RID: 11396
		private bool organizationValidationConfigRead;

		// Token: 0x04002C85 RID: 11397
		private DataClassificationConfig organizationValidationConfig;

		// Token: 0x04002C86 RID: 11398
		private readonly IConfigurationSession dataSession;

		// Token: 0x04002C87 RID: 11399
		private readonly IClassificationDefinitionsDataReader classificationsDataReader;

		// Token: 0x04002C88 RID: 11400
		private readonly IClassificationDefinitionsDiagnosticsReporter classificationDiagnosticsReporter;

		// Token: 0x04002C89 RID: 11401
		private XDocument existingrulePackXDocument;
	}
}
