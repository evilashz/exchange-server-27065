using System;
using System.Collections;
using System.ComponentModel;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SyncResponse
{
	// Token: 0x020001B7 RID: 439
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class ResponsesDeleteCollection : ArrayList
	{
		// Token: 0x06000C45 RID: 3141 RVA: 0x0001E877 File Offset: 0x0001CA77
		public ResponsesDelete Add(ResponsesDelete obj)
		{
			base.Add(obj);
			return obj;
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x0001E882 File Offset: 0x0001CA82
		public ResponsesDelete Add()
		{
			return this.Add(new ResponsesDelete());
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x0001E88F File Offset: 0x0001CA8F
		public void Insert(int index, ResponsesDelete obj)
		{
			base.Insert(index, obj);
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x0001E899 File Offset: 0x0001CA99
		public void Remove(ResponsesDelete obj)
		{
			base.Remove(obj);
		}

		// Token: 0x1700046C RID: 1132
		public ResponsesDelete this[int index]
		{
			get
			{
				return (ResponsesDelete)base[index];
			}
			set
			{
				base[index] = value;
			}
		}
	}
}
