using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000246 RID: 582
	[Cmdlet("Set", "EnvironmentVariable")]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	public sealed class SetEnvironmentVariable : ManageEnvironmentVariable
	{
		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x060015C4 RID: 5572 RVA: 0x0005B81D File Offset: 0x00059A1D
		// (set) Token: 0x060015C5 RID: 5573 RVA: 0x0005B834 File Offset: 0x00059A34
		[Parameter(Mandatory = true)]
		public string Value
		{
			get
			{
				return (string)base.Fields["Value"];
			}
			set
			{
				base.Fields["Value"] = value;
			}
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x0005B847 File Offset: 0x00059A47
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.SetVariable(base.Name, this.Value, base.Target);
			TaskLogger.LogExit();
		}
	}
}
