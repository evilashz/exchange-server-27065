using System;
using System.Collections;
using System.ComponentModel;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SyncRequest
{
	// Token: 0x020001A4 RID: 420
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class CollectionCollection : ArrayList
	{
		// Token: 0x06000B9A RID: 2970 RVA: 0x0001E00B File Offset: 0x0001C20B
		public Collection Add(Collection obj)
		{
			base.Add(obj);
			return obj;
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x0001E016 File Offset: 0x0001C216
		public Collection Add()
		{
			return this.Add(new Collection());
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x0001E023 File Offset: 0x0001C223
		public void Insert(int index, Collection obj)
		{
			base.Insert(index, obj);
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x0001E02D File Offset: 0x0001C22D
		public void Remove(Collection obj)
		{
			base.Remove(obj);
		}

		// Token: 0x17000433 RID: 1075
		public Collection this[int index]
		{
			get
			{
				return (Collection)base[index];
			}
			set
			{
				base[index] = value;
			}
		}
	}
}
