using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SyncRequest
{
	// Token: 0x020001AA RID: 426
	[XmlType(TypeName = "Collections", Namespace = "AirSync:")]
	[Serializable]
	public class Collections
	{
		// Token: 0x06000BF4 RID: 3060 RVA: 0x0001E49E File Offset: 0x0001C69E
		[DispId(-4)]
		public IEnumerator GetEnumerator()
		{
			return this.CollectionCollection.GetEnumerator();
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x0001E4AB File Offset: 0x0001C6AB
		public Collection Add(Collection obj)
		{
			return this.CollectionCollection.Add(obj);
		}

		// Token: 0x17000455 RID: 1109
		[XmlIgnore]
		public Collection this[int index]
		{
			get
			{
				return this.CollectionCollection[index];
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x0001E4C7 File Offset: 0x0001C6C7
		[XmlIgnore]
		public int Count
		{
			get
			{
				return this.CollectionCollection.Count;
			}
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x0001E4D4 File Offset: 0x0001C6D4
		public void Clear()
		{
			this.CollectionCollection.Clear();
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x0001E4E4 File Offset: 0x0001C6E4
		public Collection Remove(int index)
		{
			Collection collection = this.CollectionCollection[index];
			this.CollectionCollection.Remove(collection);
			return collection;
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x0001E50B File Offset: 0x0001C70B
		public void Remove(object obj)
		{
			this.CollectionCollection.Remove(obj);
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000BFB RID: 3067 RVA: 0x0001E519 File Offset: 0x0001C719
		// (set) Token: 0x06000BFC RID: 3068 RVA: 0x0001E534 File Offset: 0x0001C734
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

		// Token: 0x040006D8 RID: 1752
		[XmlElement(Type = typeof(Collection), ElementName = "Collection", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "AirSync:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public CollectionCollection internalCollectionCollection;
	}
}
