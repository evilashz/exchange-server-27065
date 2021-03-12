using System;
using System.Collections;
using System.ComponentModel;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SyncResponse
{
	// Token: 0x020001B8 RID: 440
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class AddCollection : ArrayList
	{
		// Token: 0x06000C4C RID: 3148 RVA: 0x0001E8C2 File Offset: 0x0001CAC2
		public Add Add(Add obj)
		{
			base.Add(obj);
			return obj;
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x0001E8CD File Offset: 0x0001CACD
		public Add Add()
		{
			return this.Add(new Add());
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x0001E8DA File Offset: 0x0001CADA
		public void Insert(int index, Add obj)
		{
			base.Insert(index, obj);
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x0001E8E4 File Offset: 0x0001CAE4
		public void Remove(Add obj)
		{
			base.Remove(obj);
		}

		// Token: 0x1700046D RID: 1133
		public Add this[int index]
		{
			get
			{
				return (Add)base[index];
			}
			set
			{
				base[index] = value;
			}
		}
	}
}
