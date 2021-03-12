using System;
using System.Collections;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.MessagingPolicies.CompliancePrograms.Tasks
{
	// Token: 0x02000966 RID: 2406
	[Cmdlet("New", "DlpPolicy", SupportsShouldProcess = true)]
	public sealed class NewDlpPolicy : NewMultitenancyFixedNameSystemConfigurationObjectTask<ADComplianceProgram>
	{
		// Token: 0x170019B9 RID: 6585
		// (get) Token: 0x060055F4 RID: 22004 RVA: 0x00161799 File Offset: 0x0015F999
		// (set) Token: 0x060055F5 RID: 22005 RVA: 0x001617B0 File Offset: 0x0015F9B0
		[Parameter(Mandatory = false)]
		public string Template
		{
			get
			{
				return (string)base.Fields["Template"];
			}
			set
			{
				base.Fields["Template"] = value;
			}
		}

		// Token: 0x170019BA RID: 6586
		// (get) Token: 0x060055F6 RID: 22006 RVA: 0x001617C3 File Offset: 0x0015F9C3
		// (set) Token: 0x060055F7 RID: 22007 RVA: 0x001617DA File Offset: 0x0015F9DA
		[Parameter(Mandatory = false)]
		public RuleState State
		{
			get
			{
				return (RuleState)base.Fields["State"];
			}
			set
			{
				base.Fields["State"] = value;
			}
		}

		// Token: 0x170019BB RID: 6587
		// (get) Token: 0x060055F8 RID: 22008 RVA: 0x001617F2 File Offset: 0x0015F9F2
		// (set) Token: 0x060055F9 RID: 22009 RVA: 0x00161809 File Offset: 0x0015FA09
		[Parameter(Mandatory = false)]
		public RuleMode Mode
		{
			get
			{
				return (RuleMode)base.Fields["Mode"];
			}
			set
			{
				base.Fields["Mode"] = value;
			}
		}

		// Token: 0x170019BC RID: 6588
		// (get) Token: 0x060055FA RID: 22010 RVA: 0x00161821 File Offset: 0x0015FA21
		// (set) Token: 0x060055FB RID: 22011 RVA: 0x00161838 File Offset: 0x0015FA38
		[Parameter(Mandatory = false)]
		public byte[] TemplateData
		{
			get
			{
				return (byte[])base.Fields["TemplateData"];
			}
			set
			{
				base.Fields["TemplateData"] = value;
			}
		}

		// Token: 0x170019BD RID: 6589
		// (get) Token: 0x060055FC RID: 22012 RVA: 0x0016184B File Offset: 0x0015FA4B
		// (set) Token: 0x060055FD RID: 22013 RVA: 0x00161862 File Offset: 0x0015FA62
		[Parameter(Mandatory = false)]
		public Hashtable Parameters
		{
			get
			{
				return (Hashtable)base.Fields["Parameters"];
			}
			set
			{
				base.Fields["Parameters"] = value;
			}
		}

		// Token: 0x170019BE RID: 6590
		// (get) Token: 0x060055FE RID: 22014 RVA: 0x00161875 File Offset: 0x0015FA75
		// (set) Token: 0x060055FF RID: 22015 RVA: 0x0016188C File Offset: 0x0015FA8C
		[Parameter(Mandatory = false, Position = 0)]
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

		// Token: 0x170019BF RID: 6591
		// (get) Token: 0x06005600 RID: 22016 RVA: 0x0016189F File Offset: 0x0015FA9F
		// (set) Token: 0x06005601 RID: 22017 RVA: 0x001618B6 File Offset: 0x0015FAB6
		[Parameter(Mandatory = false)]
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

		// Token: 0x06005602 RID: 22018 RVA: 0x001618C9 File Offset: 0x0015FAC9
		public NewDlpPolicy()
		{
			this.impl = new NewDlpPolicyImpl(this);
		}

		// Token: 0x06005603 RID: 22019 RVA: 0x001618DD File Offset: 0x0015FADD
		public OrganizationId ResolveOrganization()
		{
			return this.ResolveCurrentOrganization();
		}

		// Token: 0x170019C0 RID: 6592
		// (get) Token: 0x06005604 RID: 22020 RVA: 0x001618E5 File Offset: 0x0015FAE5
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageInstallDlpPolicy(this.Name);
			}
		}

		// Token: 0x06005605 RID: 22021 RVA: 0x001618F2 File Offset: 0x0015FAF2
		protected override void InternalProcessRecord()
		{
			this.SetupImpl();
			this.impl.ProcessRecord();
		}

		// Token: 0x06005606 RID: 22022 RVA: 0x00161908 File Offset: 0x0015FB08
		protected override void InternalValidate()
		{
			this.DataObject = (ADComplianceProgram)this.PrepareDataObject();
			if (this.Name != null)
			{
				this.DataObject.SetId(base.DataSession as IConfigurationSession, this.Name);
			}
			this.SetupImpl();
			this.impl.Validate();
		}

		// Token: 0x06005607 RID: 22023 RVA: 0x0016195B File Offset: 0x0015FB5B
		private void SetupImpl()
		{
			this.impl.DataSession = new MessagingPoliciesSyncLogDataSession(base.DataSession, null, null);
			this.impl.ShouldContinue = new CmdletImplementation.ShouldContinueMethod(base.ShouldContinue);
		}

		// Token: 0x040031CD RID: 12749
		private NewDlpPolicyImpl impl;

		// Token: 0x040031CE RID: 12750
		internal static readonly string DefaultVersion = "15.00.0002.000";

		// Token: 0x02000967 RID: 2407
		// (Invoke) Token: 0x0600560A RID: 22026
		public delegate void WarningWriterDelegate(string text);
	}
}
