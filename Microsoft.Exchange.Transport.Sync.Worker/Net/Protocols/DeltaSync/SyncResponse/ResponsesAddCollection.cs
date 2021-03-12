using System;
using System.Collections;
using System.ComponentModel;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SyncResponse
{
	// Token: 0x020001B4 RID: 436
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class ResponsesAddCollection : ArrayList
	{
		// Token: 0x06000C30 RID: 3120 RVA: 0x0001E796 File Offset: 0x0001C996
		public ResponsesAdd Add(ResponsesAdd obj)
		{
			base.Add(obj);
			return obj;
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x0001E7A1 File Offset: 0x0001C9A1
		public ResponsesAdd Add()
		{
			return this.Add(new ResponsesAdd());
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x0001E7AE File Offset: 0x0001C9AE
		public void Insert(int index, ResponsesAdd obj)
		{
			base.Insert(index, obj);
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x0001E7B8 File Offset: 0x0001C9B8
		public void Remove(ResponsesAdd obj)
		{
			base.Remove(obj);
		}

		// Token: 0x17000469 RID: 1129
		public ResponsesAdd this[int index]
		{
			get
			{
				return (ResponsesAdd)base[index];
			}
			set
			{
				base[index] = value;
			}
		}
	}
}
