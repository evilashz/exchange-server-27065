using System;
using System.Collections;
using System.ComponentModel;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SyncRequest
{
	// Token: 0x020001A7 RID: 423
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class AddCollection : ArrayList
	{
		// Token: 0x06000BAF RID: 2991 RVA: 0x0001E0EC File Offset: 0x0001C2EC
		public Add Add(Add obj)
		{
			base.Add(obj);
			return obj;
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0001E0F7 File Offset: 0x0001C2F7
		public Add Add()
		{
			return this.Add(new Add());
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0001E104 File Offset: 0x0001C304
		public void Insert(int index, Add obj)
		{
			base.Insert(index, obj);
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0001E10E File Offset: 0x0001C30E
		public void Remove(Add obj)
		{
			base.Remove(obj);
		}

		// Token: 0x17000436 RID: 1078
		public Add this[int index]
		{
			get
			{
				return (Add)base[index];
			}
			set
			{
				base[index] = value;
			}
		}
	}
}
