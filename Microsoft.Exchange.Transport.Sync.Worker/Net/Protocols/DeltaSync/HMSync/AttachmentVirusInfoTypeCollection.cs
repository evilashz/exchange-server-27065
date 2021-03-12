using System;
using System.Collections;
using System.ComponentModel;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.HMSync
{
	// Token: 0x020000A1 RID: 161
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class AttachmentVirusInfoTypeCollection : ArrayList
	{
		// Token: 0x0600060A RID: 1546 RVA: 0x00019A47 File Offset: 0x00017C47
		public AttachmentVirusInfoType Add(AttachmentVirusInfoType obj)
		{
			base.Add(obj);
			return obj;
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00019A52 File Offset: 0x00017C52
		public AttachmentVirusInfoType Add()
		{
			return this.Add(new AttachmentVirusInfoType());
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00019A5F File Offset: 0x00017C5F
		public void Insert(int index, AttachmentVirusInfoType obj)
		{
			base.Insert(index, obj);
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00019A69 File Offset: 0x00017C69
		public void Remove(AttachmentVirusInfoType obj)
		{
			base.Remove(obj);
		}

		// Token: 0x17000221 RID: 545
		public AttachmentVirusInfoType this[int index]
		{
			get
			{
				return (AttachmentVirusInfoType)base[index];
			}
			set
			{
				base[index] = value;
			}
		}
	}
}
