using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200015B RID: 347
	public class RunResult
	{
		// Token: 0x17001A79 RID: 6777
		// (get) Token: 0x060021AF RID: 8623 RVA: 0x00065631 File Offset: 0x00063831
		public List<string> DataObjectes
		{
			get
			{
				return this.dataObjects;
			}
		}

		// Token: 0x17001A7A RID: 6778
		// (get) Token: 0x060021B0 RID: 8624 RVA: 0x00065639 File Offset: 0x00063839
		// (set) Token: 0x060021B1 RID: 8625 RVA: 0x00065641 File Offset: 0x00063841
		public bool ErrorOccur { get; set; }

		// Token: 0x04001D3B RID: 7483
		private List<string> dataObjects = new List<string>();
	}
}
