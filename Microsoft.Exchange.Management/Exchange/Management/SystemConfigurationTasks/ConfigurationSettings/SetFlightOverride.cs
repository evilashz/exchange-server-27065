using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks.ConfigurationSettings
{
	// Token: 0x02000949 RID: 2377
	[Cmdlet("Set", "FlightOverride", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class SetFlightOverride : SetOverrideBase
	{
		// Token: 0x17001966 RID: 6502
		// (get) Token: 0x060054E4 RID: 21732 RVA: 0x0015E8C8 File Offset: 0x0015CAC8
		// (set) Token: 0x060054E5 RID: 21733 RVA: 0x0015E8D0 File Offset: 0x0015CAD0
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public new Version MaxVersion
		{
			get
			{
				return base.MaxVersion;
			}
			set
			{
				base.MaxVersion = value;
			}
		}

		// Token: 0x17001967 RID: 6503
		// (get) Token: 0x060054E6 RID: 21734 RVA: 0x0015E8D9 File Offset: 0x0015CAD9
		// (set) Token: 0x060054E7 RID: 21735 RVA: 0x0015E8EA File Offset: 0x0015CAEA
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public new Version FixVersion
		{
			get
			{
				return base.FixVersion ?? new Version();
			}
			set
			{
				base.FixVersion = value;
			}
		}

		// Token: 0x17001968 RID: 6504
		// (get) Token: 0x060054E8 RID: 21736 RVA: 0x0015E8F3 File Offset: 0x0015CAF3
		protected override bool IsFlight
		{
			get
			{
				return true;
			}
		}
	}
}
