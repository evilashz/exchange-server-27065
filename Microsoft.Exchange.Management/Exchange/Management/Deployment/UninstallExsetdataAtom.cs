using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000279 RID: 633
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("Uninstall", "ExsetdataAtom")]
	public sealed class UninstallExsetdataAtom : ManageExsetdataAtom
	{
		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x0600176D RID: 5997 RVA: 0x00063685 File Offset: 0x00061885
		// (set) Token: 0x0600176E RID: 5998 RVA: 0x0006369C File Offset: 0x0006189C
		[Parameter(Mandatory = true)]
		public AtomID AtomName
		{
			get
			{
				return (AtomID)base.Fields["AtomName"];
			}
			set
			{
				base.Fields["AtomName"] = value;
			}
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x000636B4 File Offset: 0x000618B4
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.UninstallAtom(this.AtomName);
			TaskLogger.LogExit();
		}
	}
}
