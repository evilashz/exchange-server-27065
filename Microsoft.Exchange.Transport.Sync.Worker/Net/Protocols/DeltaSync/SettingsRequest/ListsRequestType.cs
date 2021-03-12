using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x02000104 RID: 260
	[XmlType(TypeName = "ListsRequestType", Namespace = "HMSETTINGS:")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class ListsRequestType
	{
		// Token: 0x060007B5 RID: 1973 RVA: 0x0001AFEA File Offset: 0x000191EA
		[DispId(-4)]
		public IEnumerator GetEnumerator()
		{
			return this.ListCollection.GetEnumerator();
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x0001AFF7 File Offset: 0x000191F7
		public ListsRequestTypeList Add(ListsRequestTypeList obj)
		{
			return this.ListCollection.Add(obj);
		}

		// Token: 0x170002AA RID: 682
		[XmlIgnore]
		public ListsRequestTypeList this[int index]
		{
			get
			{
				return this.ListCollection[index];
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x0001B013 File Offset: 0x00019213
		[XmlIgnore]
		public int Count
		{
			get
			{
				return this.ListCollection.Count;
			}
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0001B020 File Offset: 0x00019220
		public void Clear()
		{
			this.ListCollection.Clear();
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0001B030 File Offset: 0x00019230
		public ListsRequestTypeList Remove(int index)
		{
			ListsRequestTypeList listsRequestTypeList = this.ListCollection[index];
			this.ListCollection.Remove(listsRequestTypeList);
			return listsRequestTypeList;
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0001B057 File Offset: 0x00019257
		public void Remove(object obj)
		{
			this.ListCollection.Remove(obj);
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x0001B065 File Offset: 0x00019265
		// (set) Token: 0x060007BD RID: 1981 RVA: 0x0001B080 File Offset: 0x00019280
		[XmlIgnore]
		public ListsRequestTypeListCollection ListCollection
		{
			get
			{
				if (this.internalListCollection == null)
				{
					this.internalListCollection = new ListsRequestTypeListCollection();
				}
				return this.internalListCollection;
			}
			set
			{
				this.internalListCollection = value;
			}
		}

		// Token: 0x0400043B RID: 1083
		[XmlElement(Type = typeof(ListsRequestTypeList), ElementName = "List", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ListsRequestTypeListCollection internalListCollection;
	}
}
