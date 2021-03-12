using System;
using System.Collections;
using System.ComponentModel;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x02000135 RID: 309
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class ListsGetResponseTypeListCollection : ArrayList
	{
		// Token: 0x060008E0 RID: 2272 RVA: 0x0001C041 File Offset: 0x0001A241
		public ListsGetResponseTypeList Add(ListsGetResponseTypeList obj)
		{
			base.Add(obj);
			return obj;
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x0001C04C File Offset: 0x0001A24C
		public ListsGetResponseTypeList Add()
		{
			return this.Add(new ListsGetResponseTypeList());
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0001C059 File Offset: 0x0001A259
		public void Insert(int index, ListsGetResponseTypeList obj)
		{
			base.Insert(index, obj);
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0001C063 File Offset: 0x0001A263
		public void Remove(ListsGetResponseTypeList obj)
		{
			base.Remove(obj);
		}

		// Token: 0x17000322 RID: 802
		public ListsGetResponseTypeList this[int index]
		{
			get
			{
				return (ListsGetResponseTypeList)base[index];
			}
			set
			{
				base[index] = value;
			}
		}
	}
}
