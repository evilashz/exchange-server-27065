using System;
using System.Collections;
using System.ComponentModel;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x020000F1 RID: 241
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class LocalPartCollection : ArrayList
	{
		// Token: 0x06000761 RID: 1889 RVA: 0x0001ABBC File Offset: 0x00018DBC
		public string Add(string obj)
		{
			base.Add(obj);
			return obj;
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0001ABC7 File Offset: 0x00018DC7
		public void Insert(int index, string obj)
		{
			base.Insert(index, obj);
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0001ABD1 File Offset: 0x00018DD1
		public void Remove(string obj)
		{
			base.Remove(obj);
		}

		// Token: 0x17000292 RID: 658
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
