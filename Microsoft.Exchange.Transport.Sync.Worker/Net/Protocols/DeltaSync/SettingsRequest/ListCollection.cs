using System;
using System.Collections;
using System.ComponentModel;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x020000F4 RID: 244
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class ListCollection : ArrayList
	{
		// Token: 0x06000774 RID: 1908 RVA: 0x0001AC83 File Offset: 0x00018E83
		public List Add(List obj)
		{
			base.Add(obj);
			return obj;
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0001AC8E File Offset: 0x00018E8E
		public List Add()
		{
			return this.Add(new List());
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0001AC9B File Offset: 0x00018E9B
		public void Insert(int index, List obj)
		{
			base.Insert(index, obj);
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0001ACA5 File Offset: 0x00018EA5
		public void Remove(List obj)
		{
			base.Remove(obj);
		}

		// Token: 0x17000295 RID: 661
		public List this[int index]
		{
			get
			{
				return (List)base[index];
			}
			set
			{
				base[index] = value;
			}
		}
	}
}
