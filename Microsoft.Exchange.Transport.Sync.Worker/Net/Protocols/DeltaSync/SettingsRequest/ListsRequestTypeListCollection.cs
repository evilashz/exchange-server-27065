using System;
using System.Collections;
using System.ComponentModel;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x020000F3 RID: 243
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class ListsRequestTypeListCollection : ArrayList
	{
		// Token: 0x0600076D RID: 1901 RVA: 0x0001AC38 File Offset: 0x00018E38
		public ListsRequestTypeList Add(ListsRequestTypeList obj)
		{
			base.Add(obj);
			return obj;
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0001AC43 File Offset: 0x00018E43
		public ListsRequestTypeList Add()
		{
			return this.Add(new ListsRequestTypeList());
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0001AC50 File Offset: 0x00018E50
		public void Insert(int index, ListsRequestTypeList obj)
		{
			base.Insert(index, obj);
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0001AC5A File Offset: 0x00018E5A
		public void Remove(ListsRequestTypeList obj)
		{
			base.Remove(obj);
		}

		// Token: 0x17000294 RID: 660
		public ListsRequestTypeList this[int index]
		{
			get
			{
				return (ListsRequestTypeList)base[index];
			}
			set
			{
				base[index] = value;
			}
		}
	}
}
