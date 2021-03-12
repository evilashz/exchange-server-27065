using System;
using System.Collections;
using System.ComponentModel;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SyncResponse
{
	// Token: 0x020001B2 RID: 434
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class CollectionCollection : ArrayList
	{
		// Token: 0x06000C22 RID: 3106 RVA: 0x0001E700 File Offset: 0x0001C900
		public Collection Add(Collection obj)
		{
			base.Add(obj);
			return obj;
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x0001E70B File Offset: 0x0001C90B
		public Collection Add()
		{
			return this.Add(new Collection());
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x0001E718 File Offset: 0x0001C918
		public void Insert(int index, Collection obj)
		{
			base.Insert(index, obj);
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x0001E722 File Offset: 0x0001C922
		public void Remove(Collection obj)
		{
			base.Remove(obj);
		}

		// Token: 0x17000467 RID: 1127
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
