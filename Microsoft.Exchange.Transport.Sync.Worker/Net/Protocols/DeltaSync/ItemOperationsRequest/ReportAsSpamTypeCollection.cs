using System;
using System.Collections;
using System.ComponentModel;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.ItemOperationsRequest
{
	// Token: 0x020000B6 RID: 182
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class ReportAsSpamTypeCollection : ArrayList
	{
		// Token: 0x0600067B RID: 1659 RVA: 0x00019FF4 File Offset: 0x000181F4
		public ReportAsSpamType Add(ReportAsSpamType obj)
		{
			base.Add(obj);
			return obj;
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00019FFF File Offset: 0x000181FF
		public ReportAsSpamType Add()
		{
			return this.Add(new ReportAsSpamType());
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0001A00C File Offset: 0x0001820C
		public void Insert(int index, ReportAsSpamType obj)
		{
			base.Insert(index, obj);
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0001A016 File Offset: 0x00018216
		public void Remove(ReportAsSpamType obj)
		{
			base.Remove(obj);
		}

		// Token: 0x17000243 RID: 579
		public ReportAsSpamType this[int index]
		{
			get
			{
				return (ReportAsSpamType)base[index];
			}
			set
			{
				base[index] = value;
			}
		}
	}
}
