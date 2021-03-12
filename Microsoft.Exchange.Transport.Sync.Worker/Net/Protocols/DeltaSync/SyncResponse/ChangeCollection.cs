using System;
using System.Collections;
using System.ComponentModel;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SyncResponse
{
	// Token: 0x020001B3 RID: 435
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class ChangeCollection : ArrayList
	{
		// Token: 0x06000C29 RID: 3113 RVA: 0x0001E74B File Offset: 0x0001C94B
		public Change Add(Change obj)
		{
			base.Add(obj);
			return obj;
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x0001E756 File Offset: 0x0001C956
		public Change Add()
		{
			return this.Add(new Change());
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x0001E763 File Offset: 0x0001C963
		public void Insert(int index, Change obj)
		{
			base.Insert(index, obj);
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x0001E76D File Offset: 0x0001C96D
		public void Remove(Change obj)
		{
			base.Remove(obj);
		}

		// Token: 0x17000468 RID: 1128
		public Change this[int index]
		{
			get
			{
				return (Change)base[index];
			}
			set
			{
				base[index] = value;
			}
		}
	}
}
