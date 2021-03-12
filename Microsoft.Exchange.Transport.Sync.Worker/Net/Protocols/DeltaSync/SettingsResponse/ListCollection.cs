using System;
using System.Collections;
using System.ComponentModel;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x0200013B RID: 315
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class ListCollection : ArrayList
	{
		// Token: 0x06000908 RID: 2312 RVA: 0x0001C1E9 File Offset: 0x0001A3E9
		public List Add(List obj)
		{
			base.Add(obj);
			return obj;
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x0001C1F4 File Offset: 0x0001A3F4
		public List Add()
		{
			return this.Add(new List());
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x0001C201 File Offset: 0x0001A401
		public void Insert(int index, List obj)
		{
			base.Insert(index, obj);
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x0001C20B File Offset: 0x0001A40B
		public void Remove(List obj)
		{
			base.Remove(obj);
		}

		// Token: 0x17000328 RID: 808
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
