using System;
using System.Collections;
using System.ComponentModel;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x020000EF RID: 239
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class FilterCollection : ArrayList
	{
		// Token: 0x06000753 RID: 1875 RVA: 0x0001AB26 File Offset: 0x00018D26
		public Filter Add(Filter obj)
		{
			base.Add(obj);
			return obj;
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x0001AB31 File Offset: 0x00018D31
		public Filter Add()
		{
			return this.Add(new Filter());
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0001AB3E File Offset: 0x00018D3E
		public void Insert(int index, Filter obj)
		{
			base.Insert(index, obj);
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0001AB48 File Offset: 0x00018D48
		public void Remove(Filter obj)
		{
			base.Remove(obj);
		}

		// Token: 0x17000290 RID: 656
		public Filter this[int index]
		{
			get
			{
				return (Filter)base[index];
			}
			set
			{
				base[index] = value;
			}
		}
	}
}
