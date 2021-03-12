using System;
using System.Collections;
using System.ComponentModel;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x020000EE RID: 238
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class ListsGetResponseTypeListCollection : ArrayList
	{
		// Token: 0x0600074C RID: 1868 RVA: 0x0001AADB File Offset: 0x00018CDB
		public ListsGetResponseTypeList Add(ListsGetResponseTypeList obj)
		{
			base.Add(obj);
			return obj;
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0001AAE6 File Offset: 0x00018CE6
		public ListsGetResponseTypeList Add()
		{
			return this.Add(new ListsGetResponseTypeList());
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0001AAF3 File Offset: 0x00018CF3
		public void Insert(int index, ListsGetResponseTypeList obj)
		{
			base.Insert(index, obj);
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0001AAFD File Offset: 0x00018CFD
		public void Remove(ListsGetResponseTypeList obj)
		{
			base.Remove(obj);
		}

		// Token: 0x1700028F RID: 655
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
