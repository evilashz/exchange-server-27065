using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000178 RID: 376
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("BuildToBuildUpgrade", "ExsetdataAtom")]
	public sealed class BuildToBuildUpgradeExsetdataAtom : ManageExsetdataAtom
	{
		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000E0E RID: 3598 RVA: 0x00040493 File Offset: 0x0003E693
		// (set) Token: 0x06000E0F RID: 3599 RVA: 0x000404AA File Offset: 0x0003E6AA
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

		// Token: 0x06000E10 RID: 3600 RVA: 0x000404C2 File Offset: 0x0003E6C2
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.BuildToBuildUpgradeAtom(this.AtomName);
			TaskLogger.LogExit();
		}
	}
}
