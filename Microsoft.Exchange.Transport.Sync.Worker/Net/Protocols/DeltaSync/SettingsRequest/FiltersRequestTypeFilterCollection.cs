using System;
using System.Collections;
using System.ComponentModel;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x020000F0 RID: 240
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class FiltersRequestTypeFilterCollection : ArrayList
	{
		// Token: 0x0600075A RID: 1882 RVA: 0x0001AB71 File Offset: 0x00018D71
		public FiltersRequestTypeFilter Add(FiltersRequestTypeFilter obj)
		{
			base.Add(obj);
			return obj;
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0001AB7C File Offset: 0x00018D7C
		public FiltersRequestTypeFilter Add()
		{
			return this.Add(new FiltersRequestTypeFilter());
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0001AB89 File Offset: 0x00018D89
		public void Insert(int index, FiltersRequestTypeFilter obj)
		{
			base.Insert(index, obj);
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0001AB93 File Offset: 0x00018D93
		public void Remove(FiltersRequestTypeFilter obj)
		{
			base.Remove(obj);
		}

		// Token: 0x17000291 RID: 657
		public FiltersRequestTypeFilter this[int index]
		{
			get
			{
				return (FiltersRequestTypeFilter)base[index];
			}
			set
			{
				base[index] = value;
			}
		}
	}
}
