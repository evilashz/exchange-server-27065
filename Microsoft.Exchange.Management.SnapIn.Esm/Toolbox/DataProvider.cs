using System;
using Microsoft.Exchange.Management.SystemManager;

namespace Microsoft.Exchange.Management.SnapIn.Esm.Toolbox
{
	// Token: 0x02000007 RID: 7
	public abstract class DataProvider
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000022A2 File Offset: 0x000004A2
		public DataList<Tool> Tools
		{
			get
			{
				return this.tools;
			}
		}

		// Token: 0x06000009 RID: 9
		public abstract void Query();

		// Token: 0x040001A8 RID: 424
		private DataList<Tool> tools = new DataList<Tool>();
	}
}
