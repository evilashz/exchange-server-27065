using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x02000142 RID: 322
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[XmlType(TypeName = "FiltersResponseType", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class FiltersResponseType
	{
		// Token: 0x0600093B RID: 2363 RVA: 0x0001C4AA File Offset: 0x0001A6AA
		[DispId(-4)]
		public IEnumerator GetEnumerator()
		{
			return this.FilterCollection.GetEnumerator();
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x0001C4B7 File Offset: 0x0001A6B7
		public Filter Add(Filter obj)
		{
			return this.FilterCollection.Add(obj);
		}

		// Token: 0x17000339 RID: 825
		[XmlIgnore]
		public Filter this[int index]
		{
			get
			{
				return this.FilterCollection[index];
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x0001C4D3 File Offset: 0x0001A6D3
		[XmlIgnore]
		public int Count
		{
			get
			{
				return this.FilterCollection.Count;
			}
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x0001C4E0 File Offset: 0x0001A6E0
		public void Clear()
		{
			this.FilterCollection.Clear();
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0001C4F0 File Offset: 0x0001A6F0
		public Filter Remove(int index)
		{
			Filter filter = this.FilterCollection[index];
			this.FilterCollection.Remove(filter);
			return filter;
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x0001C517 File Offset: 0x0001A717
		public void Remove(object obj)
		{
			this.FilterCollection.Remove(obj);
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x0001C525 File Offset: 0x0001A725
		// (set) Token: 0x06000943 RID: 2371 RVA: 0x0001C540 File Offset: 0x0001A740
		[XmlIgnore]
		public FilterCollection FilterCollection
		{
			get
			{
				if (this.internalFilterCollection == null)
				{
					this.internalFilterCollection = new FilterCollection();
				}
				return this.internalFilterCollection;
			}
			set
			{
				this.internalFilterCollection = value;
			}
		}

		// Token: 0x0400051F RID: 1311
		[XmlElement(Type = typeof(Filter), ElementName = "Filter", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public FilterCollection internalFilterCollection;
	}
}
