using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x0200082F RID: 2095
	internal sealed class DataClassificationCmdletsImplementation
	{
		// Token: 0x170015E7 RID: 5607
		// (get) Token: 0x06004897 RID: 18583 RVA: 0x00129CD0 File Offset: 0x00127ED0
		// (set) Token: 0x06004898 RID: 18584 RVA: 0x00129CD8 File Offset: 0x00127ED8
		public DataClassificationPresentationObject DataClassificationPresentationObject { get; private set; }

		// Token: 0x06004899 RID: 18585 RVA: 0x00129CE1 File Offset: 0x00127EE1
		public DataClassificationCmdletsImplementation(Task task)
		{
			ArgumentValidator.ThrowIfNull("task", task);
			this.task = task;
		}

		// Token: 0x0600489A RID: 18586 RVA: 0x00129CFC File Offset: 0x00127EFC
		public TransportRule Initialize(IConfigDataProvider dataSession, DataClassificationIdParameter identity, OptionalIdentityData optionalData)
		{
			ArgumentValidator.ThrowIfNull("dataSession", dataSession);
			ArgumentValidator.ThrowIfNull("identity", identity);
			identity.ShouldIncludeOutOfBoxCollections = false;
			this.task.WriteVerbose(TaskVerboseStringHelper.GetFindByIdParameterVerboseString(identity, dataSession, typeof(TransportRule), null));
			IEnumerable<TransportRule> enumerable = null;
			try
			{
				LocalizedString? localizedString;
				enumerable = identity.GetObjects<TransportRule>(null, dataSession, optionalData, out localizedString);
			}
			finally
			{
				this.task.WriteVerbose(TaskVerboseStringHelper.GetSourceVerboseString(dataSession));
			}
			string[] array = new string[]
			{
				identity.DataClassificationIdentity
			};
			List<QueryMatchResult> list = new List<QueryMatchResult>();
			foreach (TransportRule transportRule in enumerable)
			{
				XDocument rulePackXDoc;
				if (this.TryParseADRulePack(transportRule, out rulePackXDoc) && XmlProcessingUtils.IsFingerprintRuleCollection(rulePackXDoc))
				{
					List<QueryMatchResult> list2 = XmlProcessingUtils.GetMatchingRulesById(rulePackXDoc, array).ToList<QueryMatchResult>();
					if (list2.Count == 0)
					{
						list2 = XmlProcessingUtils.GetMatchingRulesByName(rulePackXDoc, array, NameMatchingOptions.InvariantNameOrLocalizedNameMatch, true).ToList<QueryMatchResult>();
					}
					list.AddRange(list2);
					if (list.Count == 1)
					{
						this.adRulePack = transportRule;
						this.ruleXElement = list[0].MatchingRuleXElement;
						this.ruleResourceXElement = list[0].MatchingResourceXElement;
						this.rulePackXDocument = this.ruleXElement.Document;
						ClassificationRuleCollectionPresentationObject rulePackPresentationObject = ClassificationRuleCollectionPresentationObject.Create(this.adRulePack, this.rulePackXDocument);
						this.DataClassificationPresentationObject = DataClassificationPresentationObject.Create(list[0].MatchingRuleId, list[0].MatchingRuleXElement, list[0].MatchingResourceXElement, rulePackPresentationObject);
					}
					else if (list.Count > 1)
					{
						break;
					}
				}
			}
			if (list.Count <= 0)
			{
				throw new ManagementObjectNotFoundException(Strings.ErrorCannotFindFingerprintDataClassification(identity.ToString()));
			}
			if (list.Count > 1)
			{
				throw new ManagementObjectAmbiguousException(Strings.ErrorManagementObjectAmbiguous(identity.ToString()));
			}
			return this.adRulePack;
		}

		// Token: 0x0600489B RID: 18587 RVA: 0x00129F04 File Offset: 0x00128104
		public TransportRule Initialize(TransportRule adRulePack, XDocument rulePackXDoc)
		{
			ArgumentValidator.ThrowIfNull("adRulepack", adRulePack);
			ArgumentValidator.ThrowIfNull("rulePackXDoc", rulePackXDoc);
			this.adRulePack = adRulePack;
			this.rulePackXDocument = rulePackXDoc;
			ClassificationRuleCollectionPresentationObject rulePackPresentationObject = ClassificationRuleCollectionPresentationObject.Create(this.adRulePack, this.rulePackXDocument);
			this.DataClassificationPresentationObject = DataClassificationPresentationObject.Create(rulePackPresentationObject);
			return this.adRulePack;
		}

		// Token: 0x0600489C RID: 18588 RVA: 0x00129F5C File Offset: 0x0012815C
		private byte[] GetCompressedFileData(ValidationContext validationContext)
		{
			try
			{
				string text = ClassificationRuleCollectionValidationUtils.ValidateRulePackageContents(validationContext, this.rulePackXDocument);
				byte[] uncompressedSerializedRulePackageData = null;
				if (!string.IsNullOrEmpty(text))
				{
					uncompressedSerializedRulePackageData = Encoding.Unicode.GetBytes(text);
				}
				return ClassificationRuleCollectionValidationUtils.PackAndValidateCompressedRulePackage(uncompressedSerializedRulePackageData, validationContext);
			}
			catch (ClassificationRuleCollectionSharingViolationException exception)
			{
				this.task.WriteError(exception, ErrorCategory.InvalidOperation, null);
			}
			catch (ClassificationRuleCollectionInternalValidationException ex)
			{
				this.task.WriteError(ex, (-2147287038 == ex.Error) ? ErrorCategory.ObjectNotFound : ErrorCategory.InvalidResult, null);
			}
			catch (ClassificationRuleCollectionTimeoutException exception2)
			{
				this.task.WriteError(exception2, ErrorCategory.OperationTimeout, null);
			}
			catch (LocalizedException exception3)
			{
				this.task.WriteError(exception3, ErrorCategory.InvalidData, null);
			}
			return null;
		}

		// Token: 0x0600489D RID: 18589 RVA: 0x0012A02C File Offset: 0x0012822C
		private void InternalSave(ValidationContext validationContext, bool force)
		{
			ArgumentValidator.ThrowIfNull("validationContext", validationContext);
			bool flag = XmlProcessingUtils.OptimizeRulePackXDoc(this.rulePackXDocument, validationContext.DcValidationConfig);
			if (force || flag || this.DataClassificationPresentationObject.IsDirty)
			{
				XElement rulePackageMetadataElement = XmlProcessingUtils.GetRulePackageMetadataElement(this.rulePackXDocument);
				ClassificationRuleCollectionLocalizableDetails classificationRuleCollectionLocalizableDetails = XmlProcessingUtils.ReadDefaultRulePackageMetadata(rulePackageMetadataElement);
				this.adRulePack.AdminDisplayName = classificationRuleCollectionLocalizableDetails.Name;
				this.adRulePack.ReplicationSignature = this.GetCompressedFileData(validationContext);
			}
		}

		// Token: 0x0600489E RID: 18590 RVA: 0x0012A0A0 File Offset: 0x001282A0
		public void Save(ValidationContext validationContext)
		{
			try
			{
				this.DataClassificationPresentationObject.Save(this.rulePackXDocument);
				this.InternalSave(validationContext, false);
				List<string> list = new List<string>();
				List<string> list2 = new List<string>();
				foreach (Fingerprint fingerprint in this.DataClassificationPresentationObject.Fingerprints)
				{
					string a = string.IsNullOrEmpty(fingerprint.Description) ? string.Empty : fingerprint.Description;
					string b = string.IsNullOrEmpty(fingerprint.ActualDescription) ? string.Empty : fingerprint.ActualDescription;
					if (!string.Equals(a, b))
					{
						list.Add(fingerprint.Description);
						list2.Add(fingerprint.ActualDescription);
					}
				}
				if (list.Count > 0)
				{
					this.task.WriteWarning(Strings.WarningReuseExistingFingerprints(string.Join(Strings.ClassificationRuleCollectionOffendingListSeparator, list), string.Join(Strings.ClassificationRuleCollectionOffendingListSeparator, list2)));
				}
			}
			catch (LocalizedException exception)
			{
				this.task.WriteError(exception, ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x0600489F RID: 18591 RVA: 0x0012A1CC File Offset: 0x001283CC
		public bool Delete(ValidationContext validationContext)
		{
			ArgumentValidator.ThrowIfNull("validationContext", validationContext);
			try
			{
				this.ruleXElement.Remove();
				this.ruleResourceXElement.Remove();
				if (XmlProcessingUtils.GetAllRuleIds(this.rulePackXDocument).Count <= 0)
				{
					return true;
				}
				this.InternalSave(validationContext, true);
				return false;
			}
			catch (LocalizedException exception)
			{
				this.task.WriteError(exception, ErrorCategory.InvalidOperation, null);
			}
			return false;
		}

		// Token: 0x060048A0 RID: 18592 RVA: 0x0012A244 File Offset: 0x00128444
		private bool TryParseADRulePack(TransportRule adRulePack, out XDocument rulePackXDocument)
		{
			ArgumentValidator.ThrowIfNull("adRulePack", adRulePack);
			rulePackXDocument = null;
			Exception ex = null;
			try
			{
				rulePackXDocument = ClassificationDefinitionUtils.GetRuleCollectionDocumentFromTransportRule(adRulePack);
			}
			catch (ArgumentException ex2)
			{
				ex = ex2;
			}
			catch (AggregateException ex3)
			{
				ex = ex3;
			}
			catch (XmlException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				this.task.WriteWarning(Strings.WarningInvalidRuleCollectionADObject(adRulePack.Identity.ToString(), ex.Message));
			}
			return rulePackXDocument != null;
		}

		// Token: 0x04002C04 RID: 11268
		private Task task;

		// Token: 0x04002C05 RID: 11269
		private TransportRule adRulePack;

		// Token: 0x04002C06 RID: 11270
		private XDocument rulePackXDocument;

		// Token: 0x04002C07 RID: 11271
		private XElement ruleXElement;

		// Token: 0x04002C08 RID: 11272
		private XElement ruleResourceXElement;
	}
}
