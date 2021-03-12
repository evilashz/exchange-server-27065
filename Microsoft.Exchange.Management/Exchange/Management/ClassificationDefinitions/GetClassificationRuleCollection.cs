using System;
using System.Management.Automation;
using System.Xml;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x02000839 RID: 2105
	[Cmdlet("Get", "ClassificationRuleCollection", DefaultParameterSetName = "Identity")]
	public sealed class GetClassificationRuleCollection : GetMultitenancySystemConfigurationObjectTask<ClassificationRuleCollectionIdParameter, TransportRule>
	{
		// Token: 0x17001603 RID: 5635
		// (get) Token: 0x0600490A RID: 18698 RVA: 0x0012C148 File Offset: 0x0012A348
		protected override ObjectId RootId
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600490B RID: 18699 RVA: 0x0012C14C File Offset: 0x0012A34C
		protected override void InternalValidate()
		{
			if (base.OptionalIdentityData != null)
			{
				base.OptionalIdentityData.ConfigurationContainerRdn = ClassificationDefinitionConstants.ClassificationDefinitionsRdn;
			}
			if (this.Identity == null)
			{
				this.isIdentityArgumentSpecified = false;
				this.Identity = ClassificationRuleCollectionIdParameter.Parse("*");
			}
			this.Identity.ShouldIncludeOutOfBoxCollections = true;
			base.InternalValidate();
		}

		// Token: 0x0600490C RID: 18700 RVA: 0x0012C1A4 File Offset: 0x0012A3A4
		protected override void InternalProcessRecord()
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

		// Token: 0x0600490D RID: 18701 RVA: 0x0012C1D8 File Offset: 0x0012A3D8
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter();
			TransportRule transportRule = (TransportRule)dataObject;
			ClassificationRuleCollectionPresentationObject dataObject2;
			try
			{
				dataObject2 = ExportableClassificationRuleCollectionPresentationObject.Create(transportRule);
			}
			catch (InvalidOperationException)
			{
				ClassificationDefinitionsDiagnosticsReporter.Instance.WriteInvalidObjectInformation(this.GetHashCode(), transportRule.OrganizationId, transportRule.DistinguishedName);
				TaskLogger.LogExit();
				return;
			}
			catch (ArgumentException underlyingException)
			{
				ClassificationDefinitionsDiagnosticsReporter.Instance.WriteCorruptRulePackageDiagnosticsInformation(this.GetHashCode(), transportRule.OrganizationId, transportRule.DistinguishedName, underlyingException);
				TaskLogger.LogExit();
				return;
			}
			catch (AggregateException ex)
			{
				ClassificationDefinitionsDiagnosticsReporter.Instance.WriteCorruptRulePackageDiagnosticsInformation(this.GetHashCode(), transportRule.OrganizationId, transportRule.DistinguishedName, ex.Flatten());
				TaskLogger.LogExit();
				return;
			}
			catch (XmlException ex2)
			{
				ClassificationDefinitionsDiagnosticsReporter.Instance.WriteCorruptRulePackageDiagnosticsInformation(this.GetHashCode(), transportRule.OrganizationId, transportRule.DistinguishedName, new AggregateException(new Exception[]
				{
					ex2
				}).Flatten());
				TaskLogger.LogExit();
				return;
			}
			base.WriteResult(dataObject2);
			TaskLogger.LogExit();
		}

		// Token: 0x04002C30 RID: 11312
		private bool isIdentityArgumentSpecified = true;
	}
}
