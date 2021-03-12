using System;
using System.Collections;
using System.ComponentModel;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.HMMail
{
	// Token: 0x02000096 RID: 150
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class ReminderDateCollection : ArrayList
	{
		// Token: 0x060005DD RID: 1501 RVA: 0x0001984C File Offset: 0x00017A4C
		public string Add(string obj)
		{
			base.Add(obj);
			return obj;
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00019857 File Offset: 0x00017A57
		public void Insert(int index, string obj)
		{
			base.Insert(index, obj);
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x00019861 File Offset: 0x00017A61
		public void Remove(string obj)
		{
			base.Remove(obj);
		}

		// Token: 0x17000213 RID: 531
		public string this[int index]
		{
			get
			{
				return (string)base[index];
			}
			set
			{
				base[index] = value;
			}
		}
	}
}
