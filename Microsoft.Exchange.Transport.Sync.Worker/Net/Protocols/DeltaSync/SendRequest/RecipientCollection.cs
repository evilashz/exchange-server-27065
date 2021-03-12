using System;
using System.Collections;
using System.ComponentModel;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SendRequest
{
	// Token: 0x020000D4 RID: 212
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class RecipientCollection : ArrayList
	{
		// Token: 0x06000709 RID: 1801 RVA: 0x0001A706 File Offset: 0x00018906
		public string Add(string obj)
		{
			base.Add(obj);
			return obj;
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0001A711 File Offset: 0x00018911
		public void Insert(int index, string obj)
		{
			base.Insert(index, obj);
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0001A71B File Offset: 0x0001891B
		public void Remove(string obj)
		{
			base.Remove(obj);
		}

		// Token: 0x17000276 RID: 630
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
