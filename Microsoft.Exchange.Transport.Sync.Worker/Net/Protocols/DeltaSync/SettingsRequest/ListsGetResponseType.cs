using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x0200011C RID: 284
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[XmlType(TypeName = "ListsGetResponseType", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class ListsGetResponseType
	{
		// Token: 0x06000859 RID: 2137 RVA: 0x0001B8FE File Offset: 0x00019AFE
		[DispId(-4)]
		public IEnumerator GetEnumerator()
		{
			return this.ListCollection.GetEnumerator();
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0001B90B File Offset: 0x00019B0B
		public ListsGetResponseTypeList Add(ListsGetResponseTypeList obj)
		{
			return this.ListCollection.Add(obj);
		}

		// Token: 0x170002EA RID: 746
		[XmlIgnore]
		public ListsGetResponseTypeList this[int index]
		{
			get
			{
				return this.ListCollection[index];
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x0600085C RID: 2140 RVA: 0x0001B927 File Offset: 0x00019B27
		[XmlIgnore]
		public int Count
		{
			get
			{
				return this.ListCollection.Count;
			}
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0001B934 File Offset: 0x00019B34
		public void Clear()
		{
			this.ListCollection.Clear();
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0001B944 File Offset: 0x00019B44
		public ListsGetResponseTypeList Remove(int index)
		{
			ListsGetResponseTypeList listsGetResponseTypeList = this.ListCollection[index];
			this.ListCollection.Remove(listsGetResponseTypeList);
			return listsGetResponseTypeList;
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0001B96B File Offset: 0x00019B6B
		public void Remove(object obj)
		{
			this.ListCollection.Remove(obj);
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000860 RID: 2144 RVA: 0x0001B979 File Offset: 0x00019B79
		// (set) Token: 0x06000861 RID: 2145 RVA: 0x0001B994 File Offset: 0x00019B94
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

		// Token: 0x0400048A RID: 1162
		[XmlElement(Type = typeof(ListsGetResponseTypeList), ElementName = "List", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ListsGetResponseTypeListCollection internalListCollection;
	}
}
