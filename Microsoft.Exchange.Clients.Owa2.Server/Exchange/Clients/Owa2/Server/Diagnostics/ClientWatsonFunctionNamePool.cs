using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x02000432 RID: 1074
	internal sealed class ClientWatsonFunctionNamePool
	{
		// Token: 0x060024A9 RID: 9385 RVA: 0x00085104 File Offset: 0x00083304
		public int GetOrAddFunctionNameIndex(string name)
		{
			int count;
			if (!this.pool.TryGetValue(name, out count))
			{
				count = this.pool.Count;
				this.pool.Add(name, count);
			}
			return count;
		}

		// Token: 0x060024AA RID: 9386 RVA: 0x0008513C File Offset: 0x0008333C
		public string[] ToArray()
		{
			string[] array = new string[this.pool.Count];
			this.pool.Keys.CopyTo(array, 0);
			return array;
		}

		// Token: 0x0400142A RID: 5162
		private readonly Dictionary<string, int> pool = new Dictionary<string, int>();
	}
}
