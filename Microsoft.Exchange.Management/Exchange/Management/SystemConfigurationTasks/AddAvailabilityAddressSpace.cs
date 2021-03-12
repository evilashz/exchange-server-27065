using System;
using System.Management.Automation;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020007F0 RID: 2032
	[Cmdlet("Add", "AvailabilityAddressSpace", SupportsShouldProcess = true)]
	public sealed class AddAvailabilityAddressSpace : NewMultitenancyFixedNameSystemConfigurationObjectTask<AvailabilityAddressSpace>
	{
		// Token: 0x17001577 RID: 5495
		// (get) Token: 0x060046F2 RID: 18162 RVA: 0x0012354C File Offset: 0x0012174C
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageAddAvailabilityAddressSpace(this.ForestName.ToString(), this.AccessMethod.ToString());
			}
		}

		// Token: 0x17001578 RID: 5496
		// (get) Token: 0x060046F3 RID: 18163 RVA: 0x0012356E File Offset: 0x0012176E
		// (set) Token: 0x060046F4 RID: 18164 RVA: 0x0012357B File Offset: 0x0012177B
		[Parameter(Mandatory = true)]
		[ValidateNotNullOrEmpty]
		public string ForestName
		{
			get
			{
				return this.DataObject.ForestName;
			}
			set
			{
				this.DataObject.ForestName = value;
			}
		}

		// Token: 0x17001579 RID: 5497
		// (get) Token: 0x060046F5 RID: 18165 RVA: 0x00123589 File Offset: 0x00121789
		// (set) Token: 0x060046F6 RID: 18166 RVA: 0x00123596 File Offset: 0x00121796
		[Parameter(Mandatory = true)]
		public AvailabilityAccessMethod AccessMethod
		{
			get
			{
				return this.DataObject.AccessMethod;
			}
			set
			{
				this.DataObject.AccessMethod = value;
			}
		}

		// Token: 0x1700157A RID: 5498
		// (get) Token: 0x060046F7 RID: 18167 RVA: 0x001235A4 File Offset: 0x001217A4
		// (set) Token: 0x060046F8 RID: 18168 RVA: 0x001235B1 File Offset: 0x001217B1
		[Parameter(Mandatory = false)]
		public bool UseServiceAccount
		{
			get
			{
				return this.DataObject.UseServiceAccount;
			}
			set
			{
				this.DataObject.UseServiceAccount = value;
			}
		}

		// Token: 0x1700157B RID: 5499
		// (get) Token: 0x060046F9 RID: 18169 RVA: 0x001235BF File Offset: 0x001217BF
		// (set) Token: 0x060046FA RID: 18170 RVA: 0x001235D6 File Offset: 0x001217D6
		[Parameter(Mandatory = false)]
		public PSCredential Credentials
		{
			get
			{
				return (PSCredential)base.Fields["Credentials"];
			}
			set
			{
				base.Fields["Credentials"] = value;
			}
		}

		// Token: 0x1700157C RID: 5500
		// (get) Token: 0x060046FB RID: 18171 RVA: 0x001235E9 File Offset: 0x001217E9
		// (set) Token: 0x060046FC RID: 18172 RVA: 0x001235F6 File Offset: 0x001217F6
		[Parameter(Mandatory = false)]
		public Uri ProxyUrl
		{
			get
			{
				return this.DataObject.ProxyUrl;
			}
			set
			{
				this.DataObject.ProxyUrl = value;
			}
		}

		// Token: 0x1700157D RID: 5501
		// (get) Token: 0x060046FD RID: 18173 RVA: 0x00123604 File Offset: 0x00121804
		// (set) Token: 0x060046FE RID: 18174 RVA: 0x00123611 File Offset: 0x00121811
		[Parameter(Mandatory = false)]
		public Uri TargetAutodiscoverEpr
		{
			get
			{
				return this.DataObject.TargetAutodiscoverEpr;
			}
			set
			{
				this.DataObject.TargetAutodiscoverEpr = value;
			}
		}

		// Token: 0x060046FF RID: 18175 RVA: 0x00123620 File Offset: 0x00121820
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			SharedConfigurationTaskHelper.VerifyIsNotTinyTenant(base.CurrentOrgState, new Task.ErrorLoggerDelegate(base.WriteError));
			IConfigurationSession session = (IConfigurationSession)base.DataSession;
			if (Datacenter.IsMultiTenancyEnabled())
			{
				if (this.AccessMethod != AvailabilityAccessMethod.OrgWideFB)
				{
					base.ThrowTerminatingError(new InvalidAvailabilityAccessMethodException(), ErrorCategory.InvalidOperation, this.ForestName);
				}
				if (this.TargetAutodiscoverEpr == null)
				{
					base.ThrowTerminatingError(new AvailabilityAddressSpaceInvalidTargetAutodiscoverEprException(), ErrorCategory.InvalidOperation, this.ForestName);
				}
			}
			if (this.AccessMethod == AvailabilityAccessMethod.OrgWideFBBasic && this.TargetAutodiscoverEpr == null)
			{
				base.ThrowTerminatingError(new AvailabilityAddressSpaceInvalidTargetAutodiscoverEprException(), ErrorCategory.InvalidOperation, this.ForestName);
			}
			this.DataObject.SetId(session, this.ForestName);
			PSCredential credentials = this.Credentials;
			if (credentials != null)
			{
				this.DataObject.UserName = credentials.UserName;
				this.DataObject.SetPassword(credentials.Password);
			}
			base.InternalValidate();
			if (base.HasErrors)
			{
				TaskLogger.LogExit();
				return;
			}
			TaskLogger.LogExit();
		}

		// Token: 0x04002B0A RID: 11018
		private const string propCredentials = "Credentials";
	}
}
