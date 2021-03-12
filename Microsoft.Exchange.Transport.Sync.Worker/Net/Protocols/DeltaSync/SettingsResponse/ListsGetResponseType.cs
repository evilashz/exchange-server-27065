using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x02000152 RID: 338
	[XmlType(TypeName = "ListsGetResponseType", Namespace = "HMSETTINGS:")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class ListsGetResponseType
	{
		// Token: 0x060009B6 RID: 2486 RVA: 0x0001CB22 File Offset: 0x0001AD22
		[DispId(-4)]
		public IEnumerator GetEnumerator()
		{
			return this.ListCollection.GetEnumerator();
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0001CB2F File Offset: 0x0001AD2F
		public ListsGetResponseTypeList Add(ListsGetResponseTypeList obj)
		{
			return this.ListCollection.Add(obj);
		}

		// Token: 0x1700036A RID: 874
		[XmlIgnore]
		public ListsGetResponseTypeList this[int index]
		{
			get
			{
				return this.ListCollection[index];
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x060009B9 RID: 2489 RVA: 0x0001CB4B File Offset: 0x0001AD4B
		[XmlIgnore]
		public int Count
		{
			get
			{
				return this.ListCollection.Count;
			}
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x0001CB58 File Offset: 0x0001AD58
		public void Clear()
		{
			this.ListCollection.Clear();
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x0001CB68 File Offset: 0x0001AD68
		public ListsGetResponseTypeList Remove(int index)
		{
			ListsGetResponseTypeList listsGetResponseTypeList = this.ListCollection[index];
			this.ListCollection.Remove(listsGetResponseTypeList);
			return listsGetResponseTypeList;
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0001CB8F File Offset: 0x0001AD8F
		public void Remove(object obj)
		{
			this.ListCollection.Remove(obj);
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x060009BD RID: 2493 RVA: 0x0001CB9D File Offset: 0x0001AD9D
		// (set) Token: 0x060009BE RID: 2494 RVA: 0x0001CBB8 File Offset: 0x0001ADB8
		[XmlIgnore]
		public ListsGetResponseTypeListCollection ListCollection
		{
			get
			{
				if (this.internalListCollection == null)
				{
					this.internalListCollection = new ListsGetResponseTypeListCollection();
				}
				return this.internalListCollection;
			}
			set
			{
				this.internalListCollection = value;
			}
		}

		// Token: 0x04000563 RID: 1379
		[XmlElement(Type = typeof(ListsGetResponseTypeList), ElementName = "List", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ListsGetResponseTypeListCollection internalListCollection;
	}
}
