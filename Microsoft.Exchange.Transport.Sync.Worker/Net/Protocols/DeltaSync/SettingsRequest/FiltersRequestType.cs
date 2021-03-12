using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x02000116 RID: 278
	[XmlType(TypeName = "FiltersRequestType", Namespace = "HMSETTINGS:")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class FiltersRequestType
	{
		// Token: 0x06000834 RID: 2100 RVA: 0x0001B6F5 File Offset: 0x000198F5
		[DispId(-4)]
		public IEnumerator GetEnumerator()
		{
			return this.FilterCollection.GetEnumerator();
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0001B702 File Offset: 0x00019902
		public FiltersRequestTypeFilter Add(FiltersRequestTypeFilter obj)
		{
			return this.FilterCollection.Add(obj);
		}

		// Token: 0x170002DC RID: 732
		[XmlIgnore]
		public FiltersRequestTypeFilter this[int index]
		{
			get
			{
				return this.FilterCollection[index];
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000837 RID: 2103 RVA: 0x0001B71E File Offset: 0x0001991E
		[XmlIgnore]
		public int Count
		{
			get
			{
				return this.FilterCollection.Count;
			}
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x0001B72B File Offset: 0x0001992B
		public void Clear()
		{
			this.FilterCollection.Clear();
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x0001B738 File Offset: 0x00019938
		public FiltersRequestTypeFilter Remove(int index)
		{
			FiltersRequestTypeFilter filtersRequestTypeFilter = this.FilterCollection[index];
			this.FilterCollection.Remove(filtersRequestTypeFilter);
			return filtersRequestTypeFilter;
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x0001B75F File Offset: 0x0001995F
		public void Remove(object obj)
		{
			this.FilterCollection.Remove(obj);
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x0001B76D File Offset: 0x0001996D
		// (set) Token: 0x0600083C RID: 2108 RVA: 0x0001B788 File Offset: 0x00019988
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

		// Token: 0x04000479 RID: 1145
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(FiltersRequestTypeFilter), ElementName = "Filter", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public FiltersRequestTypeFilterCollection internalFilterCollection;
	}
}
