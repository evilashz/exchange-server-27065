using System;
using System.Collections;
using System.ComponentModel;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.ItemOperationsResponse
{
	// Token: 0x020000C8 RID: 200
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class ReportAsNotSpamCollection : ArrayList
	{
		// Token: 0x060006BC RID: 1724 RVA: 0x0001A339 File Offset: 0x00018539
		public ReportAsNotSpam Add(ReportAsNotSpam obj)
		{
			base.Add(obj);
			return obj;
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0001A344 File Offset: 0x00018544
		public ReportAsNotSpam Add()
		{
			return this.Add(new ReportAsNotSpam());
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0001A351 File Offset: 0x00018551
		public void Insert(int index, ReportAsNotSpam obj)
		{
			base.Insert(index, obj);
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0001A35B File Offset: 0x0001855B
		public void Remove(ReportAsNotSpam obj)
		{
			base.Remove(obj);
		}

		// Token: 0x17000257 RID: 599
		public ReportAsNotSpam this[int index]
		{
			get
			{
				return (ReportAsNotSpam)base[index];
			}
			set
			{
				base[index] = value;
			}
		}
	}
}
