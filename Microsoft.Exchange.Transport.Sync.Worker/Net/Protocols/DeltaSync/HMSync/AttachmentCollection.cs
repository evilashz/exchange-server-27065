using System;
using System.Collections;
using System.ComponentModel;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.HMSync
{
	// Token: 0x020000A3 RID: 163
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class AttachmentCollection : ArrayList
	{
		// Token: 0x06000617 RID: 1559 RVA: 0x00019AD0 File Offset: 0x00017CD0
		public Attachment Add(Attachment obj)
		{
			base.Add(obj);
			return obj;
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00019ADB File Offset: 0x00017CDB
		public Attachment Add()
		{
			return this.Add(new Attachment());
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00019AE8 File Offset: 0x00017CE8
		public void Insert(int index, Attachment obj)
		{
			base.Insert(index, obj);
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00019AF2 File Offset: 0x00017CF2
		public void Remove(Attachment obj)
		{
			base.Remove(obj);
		}

		// Token: 0x17000223 RID: 547
		public Attachment this[int index]
		{
			get
			{
				return (Attachment)base[index];
			}
			set
			{
				base[index] = value;
			}
		}
	}
}
