using System;
using System.Collections;
using System.ComponentModel;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x0200013A RID: 314
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class ListsRequestTypeListCollection : ArrayList
	{
		// Token: 0x06000901 RID: 2305 RVA: 0x0001C19E File Offset: 0x0001A39E
		public ListsRequestTypeList Add(ListsRequestTypeList obj)
		{
			base.Add(obj);
			return obj;
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0001C1A9 File Offset: 0x0001A3A9
		public ListsRequestTypeList Add()
		{
			return this.Add(new ListsRequestTypeList());
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0001C1B6 File Offset: 0x0001A3B6
		public void Insert(int index, ListsRequestTypeList obj)
		{
			base.Insert(index, obj);
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0001C1C0 File Offset: 0x0001A3C0
		public void Remove(ListsRequestTypeList obj)
		{
			base.Remove(obj);
		}

		// Token: 0x17000327 RID: 807
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
