using System;
using System.Collections;
using System.ComponentModel;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SyncRequest
{
	// Token: 0x020001A6 RID: 422
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class ChangeCollection : ArrayList
	{
		// Token: 0x06000BA8 RID: 2984 RVA: 0x0001E0A1 File Offset: 0x0001C2A1
		public Change Add(Change obj)
		{
			base.Add(obj);
			return obj;
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x0001E0AC File Offset: 0x0001C2AC
		public Change Add()
		{
			return this.Add(new Change());
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x0001E0B9 File Offset: 0x0001C2B9
		public void Insert(int index, Change obj)
		{
			base.Insert(index, obj);
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x0001E0C3 File Offset: 0x0001C2C3
		public void Remove(Change obj)
		{
			base.Remove(obj);
		}

		// Token: 0x17000435 RID: 1077
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
