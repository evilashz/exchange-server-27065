using System;
using System.Collections;
using System.ComponentModel;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x02000138 RID: 312
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class LocalPartCollection : ArrayList
	{
		// Token: 0x060008F5 RID: 2293 RVA: 0x0001C122 File Offset: 0x0001A322
		public string Add(string obj)
		{
			base.Add(obj);
			return obj;
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0001C12D File Offset: 0x0001A32D
		public void Insert(int index, string obj)
		{
			base.Insert(index, obj);
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0001C137 File Offset: 0x0001A337
		public void Remove(string obj)
		{
			base.Remove(obj);
		}

		// Token: 0x17000325 RID: 805
		public string this[int index]
		{
			get
			{
				return (string)base[index];
			}
			set
			{
				base[index] = value;
			}
		}
	}
}
