using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SyncResponse
{
	// Token: 0x020001BA RID: 442
	[XmlType(TypeName = "Collections", Namespace = "AirSync:")]
	[Serializable]
	public class Collections
	{
		// Token: 0x06000C5C RID: 3164 RVA: 0x0001E999 File Offset: 0x0001CB99
		[DispId(-4)]
		public IEnumerator GetEnumerator()
		{
			return this.CollectionCollection.GetEnumerator();
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x0001E9A6 File Offset: 0x0001CBA6
		public Collection Add(Collection obj)
		{
			return this.CollectionCollection.Add(obj);
		}

		// Token: 0x17000472 RID: 1138
		[XmlIgnore]
		public Collection this[int index]
		{
			get
			{
				return this.CollectionCollection[index];
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06000C5F RID: 3167 RVA: 0x0001E9C2 File Offset: 0x0001CBC2
		[XmlIgnore]
		public int Count
		{
			get
			{
				return this.CollectionCollection.Count;
			}
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x0001E9CF File Offset: 0x0001CBCF
		public void Clear()
		{
			this.CollectionCollection.Clear();
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x0001E9DC File Offset: 0x0001CBDC
		public Collection Remove(int index)
		{
			Collection collection = this.CollectionCollection[index];
			this.CollectionCollection.Remove(collection);
			return collection;
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x0001EA03 File Offset: 0x0001CC03
		public void Remove(object obj)
		{
			this.CollectionCollection.Remove(obj);
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06000C63 RID: 3171 RVA: 0x0001EA11 File Offset: 0x0001CC11
		// (set) Token: 0x06000C64 RID: 3172 RVA: 0x0001EA2C File Offset: 0x0001CC2C
		[XmlIgnore]
		public CollectionCollection CollectionCollection
		{
			get
			{
				if (this.internalCollectionCollection == null)
				{
					this.internalCollectionCollection = new CollectionCollection();
				}
				return this.internalCollectionCollection;
			}
			set
			{
				this.internalCollectionCollection = value;
			}
		}

		// Token: 0x040006EF RID: 1775
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(Collection), ElementName = "Collection", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "AirSync:")]
		public CollectionCollection internalCollectionCollection;
	}
}
