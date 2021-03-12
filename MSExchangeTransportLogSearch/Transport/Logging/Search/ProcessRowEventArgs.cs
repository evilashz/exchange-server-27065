using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000021 RID: 33
	internal class ProcessRowEventArgs : EventArgs
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00004D5E File Offset: 0x00002F5E
		public ReadOnlyRow Row
		{
			get
			{
				return this.readOnlyRow;
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004D66 File Offset: 0x00002F66
		public ProcessRowEventArgs(ReadOnlyRow readOnlyRow)
		{
			this.readOnlyRow = readOnlyRow;
		}

		// Token: 0x04000053 RID: 83
		private ReadOnlyRow readOnlyRow;
	}
}
