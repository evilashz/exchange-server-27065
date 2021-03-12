using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000442 RID: 1090
	[Cmdlet("remove", "MobileDeviceMailboxPolicy", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveMobilePolicy : RemoveMailboxPolicyBase<MobileMailboxPolicy>
	{
		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x060026A1 RID: 9889 RVA: 0x00098E5C File Offset: 0x0009705C
		// (set) Token: 0x060026A2 RID: 9890 RVA: 0x00098E64 File Offset: 0x00097064
		[Parameter(Mandatory = false)]
		public SwitchParameter Force
		{
			get
			{
				return this.force;
			}
			set
			{
				this.force = value;
			}
		}

		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x060026A3 RID: 9891 RVA: 0x00098E6D File Offset: 0x0009706D
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if (base.DataObject.IsDefault)
				{
					return Strings.ConfirmationMessageRemoveDefaultMobileMailboxPolicy(this.Identity.ToString());
				}
				return Strings.ConfirmationMessageRemoveMobileMailboxPolicy(this.Identity.ToString());
			}
		}

		// Token: 0x060026A4 RID: 9892 RVA: 0x00098E9D File Offset: 0x0009709D
		protected override bool HandleRemoveWithAssociatedUsers()
		{
			return this.Force || base.ShouldContinue(Strings.WarningRemovePolicyWithAssociatedUsers(base.DataObject.Name));
		}

		// Token: 0x04001D7F RID: 7551
		private SwitchParameter force;
	}
}
