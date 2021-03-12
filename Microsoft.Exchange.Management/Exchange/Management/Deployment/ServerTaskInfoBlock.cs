using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000187 RID: 391
	[ClassAccessLevel(AccessLevel.Consumer)]
	public class ServerTaskInfoBlock : TaskInfoBlock
	{
		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000E98 RID: 3736 RVA: 0x00041B50 File Offset: 0x0003FD50
		// (set) Token: 0x06000E99 RID: 3737 RVA: 0x00041B6B File Offset: 0x0003FD6B
		public ServerTaskInfoEntry Standalone
		{
			get
			{
				if (this.standalone == null)
				{
					this.standalone = new ServerTaskInfoEntry();
				}
				return this.standalone;
			}
			set
			{
				this.standalone = value;
			}
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x00041B74 File Offset: 0x0003FD74
		internal override string GetTask(InstallationCircumstances circumstance)
		{
			switch (circumstance)
			{
			case InstallationCircumstances.Standalone:
				return this.Standalone.Task;
			}
			return string.Empty;
		}

		// Token: 0x040006D2 RID: 1746
		private ServerTaskInfoEntry standalone;
	}
}
