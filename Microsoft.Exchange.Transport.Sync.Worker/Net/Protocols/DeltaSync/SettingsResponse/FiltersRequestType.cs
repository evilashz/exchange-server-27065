using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x0200014C RID: 332
	[XmlType(TypeName = "FiltersRequestType", Namespace = "HMSETTINGS:")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class FiltersRequestType
	{
		// Token: 0x06000991 RID: 2449 RVA: 0x0001C919 File Offset: 0x0001AB19
		[DispId(-4)]
		public IEnumerator GetEnumerator()
		{
			return this.FilterCollection.GetEnumerator();
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0001C926 File Offset: 0x0001AB26
		public FiltersRequestTypeFilter Add(FiltersRequestTypeFilter obj)
		{
			return this.FilterCollection.Add(obj);
		}

		// Token: 0x1700035C RID: 860
		[XmlIgnore]
		public FiltersRequestTypeFilter this[int index]
		{
			get
			{
				return this.FilterCollection[index];
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x0001C942 File Offset: 0x0001AB42
		[XmlIgnore]
		public int Count
		{
			get
			{
				return this.FilterCollection.Count;
			}
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0001C94F File Offset: 0x0001AB4F
		public void Clear()
		{
			this.FilterCollection.Clear();
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0001C95C File Offset: 0x0001AB5C
		public FiltersRequestTypeFilter Remove(int index)
		{
			FiltersRequestTypeFilter filtersRequestTypeFilter = this.FilterCollection[index];
			this.FilterCollection.Remove(filtersRequestTypeFilter);
			return filtersRequestTypeFilter;
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0001C983 File Offset: 0x0001AB83
		public void Remove(object obj)
		{
			this.FilterCollection.Remove(obj);
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x0001C991 File Offset: 0x0001AB91
		// (set) Token: 0x06000999 RID: 2457 RVA: 0x0001C9AC File Offset: 0x0001ABAC
		[XmlIgnore]
		public FiltersRequestTypeFilterCollection FilterCollection
		{
			get
			{
				if (this.internalFilterCollection == null)
				{
					this.internalFilterCollection = new FiltersRequestTypeFilterCollection();
				}
				return this.internalFilterCollection;
			}
			set
			{
				this.internalFilterCollection = value;
			}
		}

		// Token: 0x04000552 RID: 1362
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(FiltersRequestTypeFilter), ElementName = "Filter", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public FiltersRequestTypeFilterCollection internalFilterCollection;
	}
}
