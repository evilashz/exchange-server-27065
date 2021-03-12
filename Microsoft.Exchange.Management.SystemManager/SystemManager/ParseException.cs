using System;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000081 RID: 129
	public sealed class ParseException : Exception
	{
		// Token: 0x0600045A RID: 1114 RVA: 0x00010344 File Offset: 0x0000E544
		public ParseException(string message, int position) : base(message)
		{
			this.position = position;
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x00010354 File Offset: 0x0000E554
		public int Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0001035C File Offset: 0x0000E55C
		public override string ToString()
		{
			return string.Format("{0} (at index {1})", this.Message, this.position);
		}

		// Token: 0x04000122 RID: 290
		private int position;
	}
}
