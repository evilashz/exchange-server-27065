using System;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000282 RID: 642
	[Cmdlet("Update", "ServiceExecutable")]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class UpdateServiceExecutable : ManageServiceBase
	{
		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x0600178D RID: 6029 RVA: 0x00063CCC File Offset: 0x00061ECC
		// (set) Token: 0x0600178E RID: 6030 RVA: 0x00063CE3 File Offset: 0x00061EE3
		[Parameter(Mandatory = true)]
		public string ServiceName
		{
			get
			{
				return (string)base.Fields["ServiceName"];
			}
			set
			{
				base.Fields["ServiceName"] = value;
			}
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x0600178F RID: 6031 RVA: 0x00063CF6 File Offset: 0x00061EF6
		// (set) Token: 0x06001790 RID: 6032 RVA: 0x00063D0D File Offset: 0x00061F0D
		[Parameter(Mandatory = true)]
		public string Executable
		{
			get
			{
				return (string)base.Fields["Executable"];
			}
			set
			{
				base.Fields["Executable"] = value;
			}
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x00063D20 File Offset: 0x00061F20
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			string executablePath = "\"" + Path.Combine(ConfigurationContext.Setup.BinPath, this.Executable) + "\"";
			base.UpdateExecutable(this.ServiceName, executablePath);
			TaskLogger.LogExit();
		}
	}
}
