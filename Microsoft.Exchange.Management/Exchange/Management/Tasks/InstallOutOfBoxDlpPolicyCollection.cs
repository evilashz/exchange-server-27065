using System;
using System.IO;
using System.Management.Automation;
using System.Reflection;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MessagingPolicies.CompliancePrograms.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002D8 RID: 728
	[Cmdlet("Install", "OutOfBoxDlpPolicyCollection", SupportsShouldProcess = true)]
	public sealed class InstallOutOfBoxDlpPolicyCollection : NewFixedNameSystemConfigurationObjectTask<ADComplianceProgramCollection>
	{
		// Token: 0x0600195B RID: 6491 RVA: 0x00071487 File Offset: 0x0006F687
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 49, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\DirectorySetup\\InstallOutOfBoxDlpPolicyCollection.cs");
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x000714B4 File Offset: 0x0006F6B4
		protected override IConfigurable PrepareDataObject()
		{
			ADComplianceProgramCollection adcomplianceProgramCollection = (ADComplianceProgramCollection)base.PrepareDataObject();
			adcomplianceProgramCollection.Name = DlpUtils.OutOfBoxDlpPoliciesCollectionName;
			ADObjectId adobjectId = base.RootOrgContainerId;
			adobjectId = adobjectId.GetChildId("Transport Settings");
			adobjectId = adobjectId.GetChildId("Rules");
			adobjectId = adobjectId.GetChildId(adcomplianceProgramCollection.Name);
			adcomplianceProgramCollection.SetId(adobjectId);
			return adcomplianceProgramCollection;
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x0007150C File Offset: 0x0006F70C
		protected override void InternalProcessRecord()
		{
			try
			{
				if (base.DataSession.Read<ADComplianceProgramCollection>(this.DataObject.Id) == null)
				{
					base.InternalProcessRecord();
				}
			}
			catch (ADObjectAlreadyExistsException)
			{
			}
			using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(InstallOutOfBoxDlpPolicyCollection.DlpPolicyTemplatesXmlResourceId))
			{
				DlpUtils.DeleteOutOfBoxDlpPolicies(base.DataSession);
				DlpUtils.SaveOutOfBoxDlpTemplates(base.DataSession, DlpPolicyParser.ParseDlpPolicyTemplates(manifestResourceStream));
			}
		}

		// Token: 0x04000B08 RID: 2824
		private static string DlpPolicyTemplatesXmlResourceId = "OutOfBoxDlpPolicyTemplates.xml";
	}
}
