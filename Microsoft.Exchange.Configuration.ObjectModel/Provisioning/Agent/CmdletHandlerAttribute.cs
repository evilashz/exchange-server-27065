using System;

namespace Microsoft.Exchange.Provisioning.Agent
{
	// Token: 0x02000202 RID: 514
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class CmdletHandlerAttribute : Attribute
	{
		// Token: 0x060011F6 RID: 4598 RVA: 0x000378A7 File Offset: 0x00035AA7
		public CmdletHandlerAttribute(string taskName) : this()
		{
			this.taskName = taskName;
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x000378B6 File Offset: 0x00035AB6
		public CmdletHandlerAttribute()
		{
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x060011F8 RID: 4600 RVA: 0x000378BE File Offset: 0x00035ABE
		// (set) Token: 0x060011F9 RID: 4601 RVA: 0x000378C6 File Offset: 0x00035AC6
		public string TaskName
		{
			get
			{
				return this.taskName;
			}
			set
			{
				this.taskName = value;
			}
		}

		// Token: 0x04000442 RID: 1090
		private string taskName;
	}
}
