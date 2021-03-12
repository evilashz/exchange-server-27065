using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.MessagingPolicies.CompliancePrograms.Tasks
{
	// Token: 0x0200096F RID: 2415
	[Cmdlet("Import", "DlpPolicyCollection", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class ImportDlpPolicyCollection : GetMultitenancySystemConfigurationObjectTask<DlpPolicyIdParameter, ADComplianceProgram>
	{
		// Token: 0x06005640 RID: 22080 RVA: 0x001627F7 File Offset: 0x001609F7
		public ImportDlpPolicyCollection()
		{
			this.impl = new ImportDlpPolicyCollectionImpl(this);
		}

		// Token: 0x170019C8 RID: 6600
		// (get) Token: 0x06005641 RID: 22081 RVA: 0x0016280B File Offset: 0x00160A0B
		// (set) Token: 0x06005642 RID: 22082 RVA: 0x00162822 File Offset: 0x00160A22
		[Parameter(Mandatory = true, Position = 0)]
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

		// Token: 0x170019C9 RID: 6601
		// (get) Token: 0x06005643 RID: 22083 RVA: 0x00162835 File Offset: 0x00160A35
		// (set) Token: 0x06005644 RID: 22084 RVA: 0x0016285B File Offset: 0x00160A5B
		[Parameter(Mandatory = false)]
		public SwitchParameter Force
		{
			get
			{
				return (SwitchParameter)(base.Fields["Force"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Force"] = value;
			}
		}

		// Token: 0x170019CA RID: 6602
		// (get) Token: 0x06005645 RID: 22085 RVA: 0x00162873 File Offset: 0x00160A73
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageImportDlpPolicy;
			}
		}

		// Token: 0x06005646 RID: 22086 RVA: 0x0016287A File Offset: 0x00160A7A
		protected override void InternalProcessRecord()
		{
			this.SetupImpl();
			this.impl.ProcessRecord();
		}

		// Token: 0x06005647 RID: 22087 RVA: 0x0016288D File Offset: 0x00160A8D
		protected override void InternalValidate()
		{
			this.SetupImpl();
			this.impl.Validate();
			base.InternalValidate();
		}

		// Token: 0x06005648 RID: 22088 RVA: 0x001628A6 File Offset: 0x00160AA6
		private void SetupImpl()
		{
			this.impl.DataSession = new MessagingPoliciesSyncLogDataSession(base.DataSession, null, null);
			this.impl.ShouldContinue = new CmdletImplementation.ShouldContinueMethod(base.ShouldContinue);
		}

		// Token: 0x040031DE RID: 12766
		private ImportDlpPolicyCollectionImpl impl;
	}
}
