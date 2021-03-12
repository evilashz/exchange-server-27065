using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000023 RID: 35
	public class CrashOnLIDException : Exception
	{
		// Token: 0x060002C4 RID: 708 RVA: 0x00007B16 File Offset: 0x00005D16
		public CrashOnLIDException(LID lid) : base(string.Format("Crash on LID: {0}", lid.Value))
		{
			this.lid = lid;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00007B3B File Offset: 0x00005D3B
		public CrashOnLIDException(LID lid, Exception innerException) : base(string.Format("Crash on LID: {0}", lid.Value), innerException)
		{
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x00007B5A File Offset: 0x00005D5A
		public LID LID
		{
			get
			{
				return this.lid;
			}
		}

		// Token: 0x040003F2 RID: 1010
		private LID lid;
	}
}
