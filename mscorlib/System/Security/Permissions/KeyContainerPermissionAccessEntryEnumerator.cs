using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x020002EA RID: 746
	[ComVisible(true)]
	[Serializable]
	public sealed class KeyContainerPermissionAccessEntryEnumerator : IEnumerator
	{
		// Token: 0x060026B8 RID: 9912 RVA: 0x0008C35A File Offset: 0x0008A55A
		private KeyContainerPermissionAccessEntryEnumerator()
		{
		}

		// Token: 0x060026B9 RID: 9913 RVA: 0x0008C362 File Offset: 0x0008A562
		internal KeyContainerPermissionAccessEntryEnumerator(KeyContainerPermissionAccessEntryCollection entries)
		{
			this.m_entries = entries;
			this.m_current = -1;
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x060026BA RID: 9914 RVA: 0x0008C378 File Offset: 0x0008A578
		public KeyContainerPermissionAccessEntry Current
		{
			get
			{
				return this.m_entries[this.m_current];
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x060026BB RID: 9915 RVA: 0x0008C38B File Offset: 0x0008A58B
		object IEnumerator.Current
		{
			get
			{
				return this.m_entries[this.m_current];
			}
		}

		// Token: 0x060026BC RID: 9916 RVA: 0x0008C39E File Offset: 0x0008A59E
		public bool MoveNext()
		{
			if (this.m_current == this.m_entries.Count - 1)
			{
				return false;
			}
			this.m_current++;
			return true;
		}

		// Token: 0x060026BD RID: 9917 RVA: 0x0008C3C6 File Offset: 0x0008A5C6
		public void Reset()
		{
			this.m_current = -1;
		}

		// Token: 0x04000EB6 RID: 3766
		private KeyContainerPermissionAccessEntryCollection m_entries;

		// Token: 0x04000EB7 RID: 3767
		private int m_current;
	}
}
