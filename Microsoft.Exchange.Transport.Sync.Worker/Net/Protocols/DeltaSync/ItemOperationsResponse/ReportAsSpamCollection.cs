using System;
using System.Collections;
using System.ComponentModel;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.ItemOperationsResponse
{
	// Token: 0x020000C7 RID: 199
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class ReportAsSpamCollection : ArrayList
	{
		// Token: 0x060006B5 RID: 1717 RVA: 0x0001A2EE File Offset: 0x000184EE
		public ReportAsSpam Add(ReportAsSpam obj)
		{
			base.Add(obj);
			return obj;
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0001A2F9 File Offset: 0x000184F9
		public ReportAsSpam Add()
		{
			return this.Add(new ReportAsSpam());
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0001A306 File Offset: 0x00018506
		public void Insert(int index, ReportAsSpam obj)
		{
			base.Insert(index, obj);
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0001A310 File Offset: 0x00018510
		public void Remove(ReportAsSpam obj)
		{
			base.Remove(obj);
		}

		// Token: 0x17000256 RID: 598
		public ReportAsSpam this[int index]
		{
			get
			{
				return (ReportAsSpam)base[index];
			}
			set
			{
				base[index] = value;
			}
		}
	}
}
