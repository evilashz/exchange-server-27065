using System;
using System.Collections;
using System.ComponentModel;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x02000139 RID: 313
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class AddressCollection : ArrayList
	{
		// Token: 0x060008FB RID: 2299 RVA: 0x0001C160 File Offset: 0x0001A360
		public string Add(string obj)
		{
			base.Add(obj);
			return obj;
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0001C16B File Offset: 0x0001A36B
		public void Insert(int index, string obj)
		{
			base.Insert(index, obj);
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0001C175 File Offset: 0x0001A375
		public void Remove(string obj)
		{
			base.Remove(obj);
		}

		// Token: 0x17000326 RID: 806
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
