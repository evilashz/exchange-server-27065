using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020000EE RID: 238
	[Cmdlet("start", "SetupProcess", SupportsShouldProcess = true)]
	[LocDescription(Strings.IDs.StartSetupProcess)]
	public class RunProcess : RunProcessBase
	{
		// Token: 0x1700026A RID: 618
		// (get) Token: 0x0600070D RID: 1805 RVA: 0x0001D130 File Offset: 0x0001B330
		// (set) Token: 0x0600070E RID: 1806 RVA: 0x0001D147 File Offset: 0x0001B347
		[Parameter(Mandatory = true)]
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

		// Token: 0x0600070F RID: 1807 RVA: 0x0001D15A File Offset: 0x0001B35A
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.ExeName = this.Name;
			base.InternalValidate();
			TaskLogger.LogExit();
		}

		// Token: 0x04000356 RID: 854
		private const string ExeNameParameter = "Name";
	}
}
