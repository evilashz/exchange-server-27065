using System;
using System.Collections;
using System.ComponentModel;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.HMSync
{
	// Token: 0x020000A2 RID: 162
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class NameCollection : ArrayList
	{
		// Token: 0x06000611 RID: 1553 RVA: 0x00019A92 File Offset: 0x00017C92
		public string Add(string obj)
		{
			base.Add(obj);
			return obj;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00019A9D File Offset: 0x00017C9D
		public void Insert(int index, string obj)
		{
			base.Insert(index, obj);
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00019AA7 File Offset: 0x00017CA7
		public void Remove(string obj)
		{
			base.Remove(obj);
		}

		// Token: 0x17000222 RID: 546
		public string this[int index]
		{
			get
			{
				return (string)base[index];
			}
			set
			{
				base[index] = value;
			}
		}
	}
}
