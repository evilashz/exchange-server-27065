using System;
using System.Collections;
using System.ComponentModel;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SyncResponse
{
	// Token: 0x020001B5 RID: 437
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class DeleteCollection : ArrayList
	{
		// Token: 0x06000C37 RID: 3127 RVA: 0x0001E7E1 File Offset: 0x0001C9E1
		public Delete Add(Delete obj)
		{
			base.Add(obj);
			return obj;
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x0001E7EC File Offset: 0x0001C9EC
		public Delete Add()
		{
			return this.Add(new Delete());
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x0001E7F9 File Offset: 0x0001C9F9
		public void Insert(int index, Delete obj)
		{
			base.Insert(index, obj);
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x0001E803 File Offset: 0x0001CA03
		public void Remove(Delete obj)
		{
			base.Remove(obj);
		}

		// Token: 0x1700046A RID: 1130
		public Delete this[int index]
		{
			get
			{
				return (Delete)base[index];
			}
			set
			{
				base[index] = value;
			}
		}
	}
}
