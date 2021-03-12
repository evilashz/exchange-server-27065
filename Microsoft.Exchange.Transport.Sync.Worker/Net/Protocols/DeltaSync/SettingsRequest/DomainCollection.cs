using System;
using System.Collections;
using System.ComponentModel;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x020000F5 RID: 245
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class DomainCollection : ArrayList
	{
		// Token: 0x0600077B RID: 1915 RVA: 0x0001ACCE File Offset: 0x00018ECE
		public string Add(string obj)
		{
			base.Add(obj);
			return obj;
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0001ACD9 File Offset: 0x00018ED9
		public void Insert(int index, string obj)
		{
			base.Insert(index, obj);
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0001ACE3 File Offset: 0x00018EE3
		public void Remove(string obj)
		{
			base.Remove(obj);
		}

		// Token: 0x17000296 RID: 662
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
