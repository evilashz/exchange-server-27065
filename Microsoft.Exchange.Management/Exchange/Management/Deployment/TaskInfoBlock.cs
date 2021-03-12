using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000184 RID: 388
	[ClassAccessLevel(AccessLevel.Consumer)]
	public abstract class TaskInfoBlock
	{
		// Token: 0x06000E80 RID: 3712 RVA: 0x00041A4E File Offset: 0x0003FC4E
		public TaskInfoBlock()
		{
			this.useInstallTasks = false;
			this.isFatal = true;
			this.weight = 1;
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000E81 RID: 3713 RVA: 0x00041A6B File Offset: 0x0003FC6B
		// (set) Token: 0x06000E82 RID: 3714 RVA: 0x00041A81 File Offset: 0x0003FC81
		[XmlAttribute]
		public string DescriptionId
		{
			get
			{
				if (this.descriptionId != null)
				{
					return this.descriptionId;
				}
				return string.Empty;
			}
			set
			{
				this.descriptionId = value;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000E83 RID: 3715 RVA: 0x00041A8A File Offset: 0x0003FC8A
		// (set) Token: 0x06000E84 RID: 3716 RVA: 0x00041A92 File Offset: 0x0003FC92
		[XmlAttribute]
		public bool UseInstallTasks
		{
			get
			{
				return this.useInstallTasks;
			}
			set
			{
				this.useInstallTasks = value;
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000E85 RID: 3717 RVA: 0x00041A9B File Offset: 0x0003FC9B
		// (set) Token: 0x06000E86 RID: 3718 RVA: 0x00041AA3 File Offset: 0x0003FCA3
		[XmlAttribute]
		public int Weight
		{
			get
			{
				return this.weight;
			}
			set
			{
				this.weight = value;
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000E87 RID: 3719 RVA: 0x00041AAC File Offset: 0x0003FCAC
		// (set) Token: 0x06000E88 RID: 3720 RVA: 0x00041AB4 File Offset: 0x0003FCB4
		[XmlAttribute]
		public bool IsFatal
		{
			get
			{
				return this.isFatal;
			}
			set
			{
				this.isFatal = value;
			}
		}

		// Token: 0x06000E89 RID: 3721
		internal abstract string GetTask(InstallationCircumstances circumstance);

		// Token: 0x040006CC RID: 1740
		private string descriptionId;

		// Token: 0x040006CD RID: 1741
		private bool useInstallTasks;

		// Token: 0x040006CE RID: 1742
		private int weight;

		// Token: 0x040006CF RID: 1743
		private bool isFatal;
	}
}
