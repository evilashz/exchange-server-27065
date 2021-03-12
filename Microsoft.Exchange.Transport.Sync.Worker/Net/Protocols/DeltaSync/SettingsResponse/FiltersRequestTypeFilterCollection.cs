using System;
using System.Collections;
using System.ComponentModel;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x02000137 RID: 311
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class FiltersRequestTypeFilterCollection : ArrayList
	{
		// Token: 0x060008EE RID: 2286 RVA: 0x0001C0D7 File Offset: 0x0001A2D7
		public FiltersRequestTypeFilter Add(FiltersRequestTypeFilter obj)
		{
			base.Add(obj);
			return obj;
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x0001C0E2 File Offset: 0x0001A2E2
		public FiltersRequestTypeFilter Add()
		{
			return this.Add(new FiltersRequestTypeFilter());
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0001C0EF File Offset: 0x0001A2EF
		public void Insert(int index, FiltersRequestTypeFilter obj)
		{
			base.Insert(index, obj);
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0001C0F9 File Offset: 0x0001A2F9
		public void Remove(FiltersRequestTypeFilter obj)
		{
			base.Remove(obj);
		}

		// Token: 0x17000324 RID: 804
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
