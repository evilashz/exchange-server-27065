using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.MessagingPolicies.CompliancePrograms.Tasks
{
	// Token: 0x02000971 RID: 2417
	[Cmdlet("Export", "DlpPolicyCollection", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class ExportDlpPolicyCollection : GetMultitenancySystemConfigurationObjectTask<DlpPolicyIdParameter, ADComplianceProgram>
	{
		// Token: 0x170019CB RID: 6603
		// (get) Token: 0x0600564E RID: 22094 RVA: 0x00162B1A File Offset: 0x00160D1A
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageExportDlpPolicyCollection;
			}
		}

		// Token: 0x170019CC RID: 6604
		// (get) Token: 0x0600564F RID: 22095 RVA: 0x00162B24 File Offset: 0x00160D24
		protected override ObjectId RootId
		{
			get
			{
				IConfigurationSession configurationSession = (IConfigurationSession)base.DataSession;
				if (configurationSession == null)
				{
					return null;
				}
				return configurationSession.GetOrgContainerId().GetChildId("Transport Settings").GetChildId("Rules").GetChildId(DlpUtils.TenantDlpPoliciesCollectionName);
			}
		}

		// Token: 0x06005650 RID: 22096 RVA: 0x00162B74 File Offset: 0x00160D74
		protected override void WriteResult<T>(IEnumerable<T> dataObjects)
		{
			IList<DlpPolicyMetaData> list = (from dataObject in (IEnumerable<ADComplianceProgram>)dataObjects
			select DlpPolicyParser.ParseDlpPolicyInstance(dataObject.TransportRulesXml)).ToList<DlpPolicyMetaData>();
			foreach (DlpPolicyMetaData dlpPolicyMetaData in list)
			{
				dlpPolicyMetaData.PolicyCommands = DlpUtils.GetEtrsForDlpPolicy(dlpPolicyMetaData.ImmutableId, base.DataSession);
			}
			this.WriteResult(new BinaryFileDataObject
			{
				FileData = DlpPolicyParser.SerializeDlpPolicyInstances(list)
			});
		}
	}
}
