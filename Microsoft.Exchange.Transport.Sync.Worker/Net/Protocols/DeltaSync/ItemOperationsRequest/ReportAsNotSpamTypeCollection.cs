using System;
using System.Collections;
using System.ComponentModel;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.ItemOperationsRequest
{
	// Token: 0x020000B5 RID: 181
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class ReportAsNotSpamTypeCollection : ArrayList
	{
		// Token: 0x06000674 RID: 1652 RVA: 0x00019FA9 File Offset: 0x000181A9
		public ReportAsNotSpamType Add(ReportAsNotSpamType obj)
		{
			base.Add(obj);
			return obj;
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00019FB4 File Offset: 0x000181B4
		public ReportAsNotSpamType Add()
		{
			return this.Add(new ReportAsNotSpamType());
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x00019FC1 File Offset: 0x000181C1
		public void Insert(int index, ReportAsNotSpamType obj)
		{
			base.Insert(index, obj);
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00019FCB File Offset: 0x000181CB
		public void Remove(ReportAsNotSpamType obj)
		{
			base.Remove(obj);
		}

		// Token: 0x17000242 RID: 578
		public ReportAsNotSpamType this[int index]
		{
			get
			{
				return (ReportAsNotSpamType)base[index];
			}
			set
			{
				base[index] = value;
			}
		}
	}
}
