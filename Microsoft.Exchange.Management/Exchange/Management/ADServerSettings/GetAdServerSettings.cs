using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Management.ADServerSettings
{
	// Token: 0x0200005B RID: 91
	[OutputType(new Type[]
	{
		typeof(RunspaceServerSettingsPresentationObject)
	})]
	[Cmdlet("Get", "AdServerSettings")]
	public sealed class GetAdServerSettings : Task
	{
		// Token: 0x0600024B RID: 587 RVA: 0x0000A032 File Offset: 0x00008232
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			if (base.ServerSettings != null)
			{
				base.WriteObject(new RunspaceServerSettingsPresentationObject((RunspaceServerSettings)base.ServerSettings));
			}
		}
	}
}
