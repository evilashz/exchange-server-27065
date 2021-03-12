using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x02000106 RID: 262
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[XmlType(TypeName = "FiltersResponseType", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class FiltersResponseType
	{
		// Token: 0x060007C8 RID: 1992 RVA: 0x0001B116 File Offset: 0x00019316
		[DispId(-4)]
		public IEnumerator GetEnumerator()
		{
			return this.FilterCollection.GetEnumerator();
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x0001B123 File Offset: 0x00019323
		public Filter Add(Filter obj)
		{
			return this.FilterCollection.Add(obj);
		}

		// Token: 0x170002B1 RID: 689
		[XmlIgnore]
		public Filter this[int index]
		{
			get
			{
				return this.FilterCollection[index];
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x060007CB RID: 1995 RVA: 0x0001B13F File Offset: 0x0001933F
		[XmlIgnore]
		public int Count
		{
			get
			{
				return this.FilterCollection.Count;
			}
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x0001B14C File Offset: 0x0001934C
		public void Clear()
		{
			this.FilterCollection.Clear();
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0001B15C File Offset: 0x0001935C
		public Filter Remove(int index)
		{
			Filter filter = this.FilterCollection[index];
			this.FilterCollection.Remove(filter);
			return filter;
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0001B183 File Offset: 0x00019383
		public void Remove(object obj)
		{
			this.FilterCollection.Remove(obj);
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x060007CF RID: 1999 RVA: 0x0001B191 File Offset: 0x00019391
		// (set) Token: 0x060007D0 RID: 2000 RVA: 0x0001B1AC File Offset: 0x000193AC
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

		// Token: 0x04000440 RID: 1088
		[XmlElement(Type = typeof(Filter), ElementName = "Filter", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public FilterCollection internalFilterCollection;
	}
}
