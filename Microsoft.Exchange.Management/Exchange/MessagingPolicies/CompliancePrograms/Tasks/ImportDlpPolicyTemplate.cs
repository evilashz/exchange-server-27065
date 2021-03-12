using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.MessagingPolicies.CompliancePrograms.Tasks
{
	// Token: 0x02000964 RID: 2404
	[Cmdlet("Import", "DlpPolicyTemplate", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class ImportDlpPolicyTemplate : GetTaskBase<ADComplianceProgramCollection>
	{
		// Token: 0x170019B7 RID: 6583
		// (get) Token: 0x060055E7 RID: 21991 RVA: 0x001614C6 File Offset: 0x0015F6C6
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageImportDlpPolicyTemplate;
			}
		}

		// Token: 0x060055E8 RID: 21992 RVA: 0x001614CD File Offset: 0x0015F6CD
		public ImportDlpPolicyTemplate()
		{
			this.impl = new ImportDlpPolicyImpl(this);
		}

		// Token: 0x170019B8 RID: 6584
		// (get) Token: 0x060055E9 RID: 21993 RVA: 0x001614E1 File Offset: 0x0015F6E1
		// (set) Token: 0x060055EA RID: 21994 RVA: 0x001614F8 File Offset: 0x0015F6F8
		[Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
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

		// Token: 0x060055EB RID: 21995 RVA: 0x0016150B File Offset: 0x0015F70B
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(false, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 65, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\DlpPolicy\\ImportDlpPolicyTemplate.cs");
		}

		// Token: 0x060055EC RID: 21996 RVA: 0x0016152A File Offset: 0x0015F72A
		protected override void InternalProcessRecord()
		{
			this.SetupImpl();
			this.impl.ProcessRecord();
		}

		// Token: 0x060055ED RID: 21997 RVA: 0x0016153D File Offset: 0x0015F73D
		protected override void InternalValidate()
		{
			this.SetupImpl();
			this.impl.Validate();
			base.InternalValidate();
		}

		// Token: 0x060055EE RID: 21998 RVA: 0x00161556 File Offset: 0x0015F756
		private void SetupImpl()
		{
			this.impl.DataSession = base.DataSession;
			this.impl.ShouldContinue = new CmdletImplementation.ShouldContinueMethod(base.ShouldContinue);
		}

		// Token: 0x040031C9 RID: 12745
		private ImportDlpPolicyImpl impl;
	}
}
