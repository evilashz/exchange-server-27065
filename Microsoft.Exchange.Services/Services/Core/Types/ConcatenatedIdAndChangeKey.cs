using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000003 RID: 3
	internal struct ConcatenatedIdAndChangeKey
	{
		// Token: 0x06000002 RID: 2 RVA: 0x000020D8 File Offset: 0x000002D8
		public ConcatenatedIdAndChangeKey(string id, string changeKey)
		{
			this.id = id;
			this.changeKey = changeKey;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020E8 File Offset: 0x000002E8
		public string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020F0 File Offset: 0x000002F0
		public string ChangeKey
		{
			get
			{
				return this.changeKey;
			}
		}

		// Token: 0x04000009 RID: 9
		private string id;

		// Token: 0x0400000A RID: 10
		private string changeKey;
	}
}
