using System;
using System.Collections;
using System.ComponentModel;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SyncRequest
{
	// Token: 0x020001A5 RID: 421
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class DeleteCollection : ArrayList
	{
		// Token: 0x06000BA1 RID: 2977 RVA: 0x0001E056 File Offset: 0x0001C256
		public Delete Add(Delete obj)
		{
			base.Add(obj);
			return obj;
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x0001E061 File Offset: 0x0001C261
		public Delete Add()
		{
			return this.Add(new Delete());
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x0001E06E File Offset: 0x0001C26E
		public void Insert(int index, Delete obj)
		{
			base.Insert(index, obj);
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0001E078 File Offset: 0x0001C278
		public void Remove(Delete obj)
		{
			base.Remove(obj);
		}

		// Token: 0x17000434 RID: 1076
		public Delete this[int index]
		{
			get
			{
				return (Delete)base[index];
			}
			set
			{
				base[index] = value;
			}
		}
	}
}
