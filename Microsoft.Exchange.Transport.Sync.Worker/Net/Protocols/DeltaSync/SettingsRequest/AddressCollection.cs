using System;
using System.Collections;
using System.ComponentModel;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x020000F2 RID: 242
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class AddressCollection : ArrayList
	{
		// Token: 0x06000767 RID: 1895 RVA: 0x0001ABFA File Offset: 0x00018DFA
		public string Add(string obj)
		{
			base.Add(obj);
			return obj;
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0001AC05 File Offset: 0x00018E05
		public void Insert(int index, string obj)
		{
			base.Insert(index, obj);
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0001AC0F File Offset: 0x00018E0F
		public void Remove(string obj)
		{
			base.Remove(obj);
		}

		// Token: 0x17000293 RID: 659
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
