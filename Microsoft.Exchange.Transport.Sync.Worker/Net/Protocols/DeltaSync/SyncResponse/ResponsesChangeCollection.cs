using System;
using System.Collections;
using System.ComponentModel;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SyncResponse
{
	// Token: 0x020001B6 RID: 438
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class ResponsesChangeCollection : ArrayList
	{
		// Token: 0x06000C3E RID: 3134 RVA: 0x0001E82C File Offset: 0x0001CA2C
		public ResponsesChange Add(ResponsesChange obj)
		{
			base.Add(obj);
			return obj;
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x0001E837 File Offset: 0x0001CA37
		public ResponsesChange Add()
		{
			return this.Add(new ResponsesChange());
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x0001E844 File Offset: 0x0001CA44
		public void Insert(int index, ResponsesChange obj)
		{
			base.Insert(index, obj);
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x0001E84E File Offset: 0x0001CA4E
		public void Remove(ResponsesChange obj)
		{
			base.Remove(obj);
		}

		// Token: 0x1700046B RID: 1131
		public ResponsesChange this[int index]
		{
			get
			{
				return (ResponsesChange)base[index];
			}
			set
			{
				base[index] = value;
			}
		}
	}
}
