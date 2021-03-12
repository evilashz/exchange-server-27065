using System;

namespace Microsoft.Office.Datacenter.WorkerTaskFramework
{
	// Token: 0x02000003 RID: 3
	[Serializable]
	public class PropertyInformation
	{
		// Token: 0x06000004 RID: 4 RVA: 0x0000210E File Offset: 0x0000030E
		public PropertyInformation(string name, string description, bool isMandatory = false)
		{
			this.name = name;
			this.Description = description;
			this.IsMandatory = isMandatory;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000212B File Offset: 0x0000032B
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002133 File Offset: 0x00000333
		// (set) Token: 0x06000007 RID: 7 RVA: 0x0000213B File Offset: 0x0000033B
		public string Description { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002144 File Offset: 0x00000344
		// (set) Token: 0x06000009 RID: 9 RVA: 0x0000214C File Offset: 0x0000034C
		public bool IsMandatory { get; set; }

		// Token: 0x04000003 RID: 3
		private readonly string name;
	}
}
