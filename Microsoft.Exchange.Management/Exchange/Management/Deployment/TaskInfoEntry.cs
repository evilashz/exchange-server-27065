using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000185 RID: 389
	[ClassAccessLevel(AccessLevel.Consumer)]
	public abstract class TaskInfoEntry
	{
		// Token: 0x06000E8A RID: 3722 RVA: 0x00041ABD File Offset: 0x0003FCBD
		public TaskInfoEntry()
		{
			this.usePrimaryTask = false;
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000E8B RID: 3723 RVA: 0x00041ACC File Offset: 0x0003FCCC
		// (set) Token: 0x06000E8C RID: 3724 RVA: 0x00041AD4 File Offset: 0x0003FCD4
		internal bool UsePrimaryTask
		{
			get
			{
				return this.usePrimaryTask;
			}
			set
			{
				this.usePrimaryTask = value;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000E8D RID: 3725 RVA: 0x00041ADD File Offset: 0x0003FCDD
		// (set) Token: 0x06000E8E RID: 3726 RVA: 0x00041AF3 File Offset: 0x0003FCF3
		[XmlText]
		public string Task
		{
			get
			{
				if (this.task != null)
				{
					return this.task;
				}
				return string.Empty;
			}
			set
			{
				this.task = value;
			}
		}

		// Token: 0x040006D0 RID: 1744
		private bool usePrimaryTask;

		// Token: 0x040006D1 RID: 1745
		private string task;
	}
}
