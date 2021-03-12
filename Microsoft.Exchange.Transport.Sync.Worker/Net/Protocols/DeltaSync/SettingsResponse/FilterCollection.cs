using System;
using System.Collections;
using System.ComponentModel;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x02000136 RID: 310
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class FilterCollection : ArrayList
	{
		// Token: 0x060008E7 RID: 2279 RVA: 0x0001C08C File Offset: 0x0001A28C
		public Filter Add(Filter obj)
		{
			base.Add(obj);
			return obj;
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0001C097 File Offset: 0x0001A297
		public Filter Add()
		{
			return this.Add(new Filter());
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x0001C0A4 File Offset: 0x0001A2A4
		public void Insert(int index, Filter obj)
		{
			base.Insert(index, obj);
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0001C0AE File Offset: 0x0001A2AE
		public void Remove(Filter obj)
		{
			base.Remove(obj);
		}

		// Token: 0x17000323 RID: 803
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
