using System;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x02000016 RID: 22
	public class SimpleBuffer : IPoolableObject
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600009E RID: 158 RVA: 0x000037A8 File Offset: 0x000019A8
		// (set) Token: 0x0600009F RID: 159 RVA: 0x000037B0 File Offset: 0x000019B0
		public byte[] Buffer { get; private set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x000037B9 File Offset: 0x000019B9
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x000037C1 File Offset: 0x000019C1
		public bool Preallocated { get; private set; }

		// Token: 0x060000A2 RID: 162 RVA: 0x000037CA File Offset: 0x000019CA
		public SimpleBuffer(int size, bool preallocated)
		{
			this.Buffer = new byte[size];
			this.Preallocated = preallocated;
		}
	}
}
