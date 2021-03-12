using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x02000140 RID: 320
	[XmlType(TypeName = "ListsRequestType", Namespace = "HMSETTINGS:")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class ListsRequestType
	{
		// Token: 0x06000928 RID: 2344 RVA: 0x0001C37F File Offset: 0x0001A57F
		[DispId(-4)]
		public IEnumerator GetEnumerator()
		{
			return this.ListCollection.GetEnumerator();
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x0001C38C File Offset: 0x0001A58C
		public ListsRequestTypeList Add(ListsRequestTypeList obj)
		{
			return this.ListCollection.Add(obj);
		}

		// Token: 0x17000332 RID: 818
		[XmlIgnore]
		public ListsRequestTypeList this[int index]
		{
			get
			{
				return this.ListCollection[index];
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x0001C3A8 File Offset: 0x0001A5A8
		[XmlIgnore]
		public int Count
		{
			get
			{
				return this.ListCollection.Count;
			}
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0001C3B5 File Offset: 0x0001A5B5
		public void Clear()
		{
			this.ListCollection.Clear();
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x0001C3C4 File Offset: 0x0001A5C4
		public ListsRequestTypeList Remove(int index)
		{
			ListsRequestTypeList listsRequestTypeList = this.ListCollection[index];
			this.ListCollection.Remove(listsRequestTypeList);
			return listsRequestTypeList;
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x0001C3EB File Offset: 0x0001A5EB
		public void Remove(object obj)
		{
			this.ListCollection.Remove(obj);
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x0001C3F9 File Offset: 0x0001A5F9
		// (set) Token: 0x06000930 RID: 2352 RVA: 0x0001C414 File Offset: 0x0001A614
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

		// Token: 0x0400051A RID: 1306
		[XmlElement(Type = typeof(ListsRequestTypeList), ElementName = "List", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ListsRequestTypeListCollection internalListCollection;
	}
}
