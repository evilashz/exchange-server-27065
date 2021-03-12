using System;
using System.Collections;
using System.Management.Automation.Runspaces;
using Microsoft.Exchange.Management.ControlPanel;
using Microsoft.PowerShell.HostingTools;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000140 RID: 320
	public interface IPSCommandWrapper
	{
		// Token: 0x17001A5E RID: 6750
		// (get) Token: 0x06002121 RID: 8481
		CommandCollection Commands { get; }

		// Token: 0x06002122 RID: 8482
		PowerShellResults<O> Invoke<O>(RunspaceMediator runspaceMediator, IEnumerable pipelineInput, WebServiceParameters parameters, CmdletActivity activity, bool isGetListAsync);

		// Token: 0x06002123 RID: 8483
		IPSCommandWrapper AddCommand(string commandText);

		// Token: 0x06002124 RID: 8484
		IPSCommandWrapper AddCommand(Command command);

		// Token: 0x06002125 RID: 8485
		IPSCommandWrapper AddParameter(string name);

		// Token: 0x06002126 RID: 8486
		IPSCommandWrapper AddParameter(string name, object value);
	}
}
