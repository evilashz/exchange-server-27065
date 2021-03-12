using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001ED RID: 493
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("Install", "ExsetdataAtom")]
	public sealed class InstallExsetdataAtom : ManageExsetdataAtom
	{
		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x060010D8 RID: 4312 RVA: 0x0004A634 File Offset: 0x00048834
		// (set) Token: 0x060010D9 RID: 4313 RVA: 0x0004A64B File Offset: 0x0004884B
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

		// Token: 0x060010DA RID: 4314 RVA: 0x0004A663 File Offset: 0x00048863
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InstallAtom(this.AtomName);
			TaskLogger.LogExit();
		}
	}
}
