using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001B8 RID: 440
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("DisasterRecovery", "ExsetdataAtom")]
	public sealed class DisasterRecoveryExsetdataAtom : ManageExsetdataAtom
	{
		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06000F63 RID: 3939 RVA: 0x000442F2 File Offset: 0x000424F2
		// (set) Token: 0x06000F64 RID: 3940 RVA: 0x00044309 File Offset: 0x00042509
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

		// Token: 0x06000F65 RID: 3941 RVA: 0x00044321 File Offset: 0x00042521
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.DisasterRecoveryAtom(this.AtomName);
			TaskLogger.LogExit();
		}
	}
}
