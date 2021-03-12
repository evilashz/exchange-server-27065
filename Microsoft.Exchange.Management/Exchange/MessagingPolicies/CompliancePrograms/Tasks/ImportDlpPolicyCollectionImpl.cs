using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.MessagingPolicies.CompliancePrograms.Tasks
{
	// Token: 0x02000970 RID: 2416
	internal class ImportDlpPolicyCollectionImpl : CmdletImplementation
	{
		// Token: 0x06005649 RID: 22089 RVA: 0x001628D7 File Offset: 0x00160AD7
		public ImportDlpPolicyCollectionImpl(ImportDlpPolicyCollection dataObject)
		{
			this.dataObject = dataObject;
		}

		// Token: 0x0600564A RID: 22090 RVA: 0x001628E6 File Offset: 0x00160AE6
		public override void Validate()
		{
			if (this.dataObject.FileData == null)
			{
				this.dataObject.WriteError(new ArgumentException(Strings.ImportDlpPolicyFileDataIsNull), ErrorCategory.InvalidArgument, "FileData");
			}
		}

		// Token: 0x0600564B RID: 22091 RVA: 0x00162918 File Offset: 0x00160B18
		public override void ProcessRecord()
		{
			if (!this.dataObject.Force && !base.ShouldContinue(Strings.PromptToOverwriteDlpPoliciesOnImport))
			{
				return;
			}
			try
			{
				ADRuleStorageManager adruleStorageManager = new ADRuleStorageManager(Utils.RuleCollectionNameFromRole(), base.DataSession);
				adruleStorageManager.LoadRuleCollection();
				foreach (TransportRuleHandle transportRuleHandle in adruleStorageManager.GetRuleHandles())
				{
					Guid guid;
					if (transportRuleHandle.Rule.TryGetDlpPolicyId(out guid))
					{
						base.DataSession.Delete(transportRuleHandle.AdRule);
					}
				}
				DlpUtils.GetInstalledTenantDlpPolicies(base.DataSession).ToList<ADComplianceProgram>().ForEach(new Action<ADComplianceProgram>(base.DataSession.Delete));
				List<DlpPolicyMetaData> list = DlpUtils.LoadDlpPolicyInstances(this.dataObject.FileData).ToList<DlpPolicyMetaData>();
				foreach (DlpPolicyMetaData dlpPolicy in list)
				{
					IEnumerable<PSObject> enumerable;
					DlpUtils.AddTenantDlpPolicy(base.DataSession, dlpPolicy, Utils.GetOrganizationParameterValue(this.dataObject.Fields), out enumerable);
				}
			}
			catch (Exception ex)
			{
				if (!this.IsKnownException(ex))
				{
					throw;
				}
				this.dataObject.WriteError(ex, ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x0600564C RID: 22092 RVA: 0x00162A9C File Offset: 0x00160C9C
		private bool IsKnownException(Exception e)
		{
			return ImportDlpPolicyCollectionImpl.KnownExceptions.Any((Type exceptionType) => exceptionType.IsInstanceOfType(e));
		}

		// Token: 0x040031DF RID: 12767
		private ImportDlpPolicyCollection dataObject;

		// Token: 0x040031E0 RID: 12768
		private static readonly Type[] KnownExceptions = new Type[]
		{
			typeof(DirectoryNotFoundException),
			typeof(IOException),
			typeof(DlpPolicyParsingException),
			typeof(DlpPolicyScriptExecutionException)
		};
	}
}
