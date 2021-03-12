using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x0200011F RID: 287
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[XmlType(TypeName = "LocalPartsType", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class LocalPartsType
	{
		// Token: 0x06000876 RID: 2166 RVA: 0x0001BAD1 File Offset: 0x00019CD1
		[DispId(-4)]
		public IEnumerator GetEnumerator()
		{
			return this.LocalPartCollection.GetEnumerator();
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0001BADE File Offset: 0x00019CDE
		public string Add(string obj)
		{
			return this.LocalPartCollection.Add(obj);
		}

		// Token: 0x170002F4 RID: 756
		[XmlIgnore]
		public string this[int index]
		{
			get
			{
				return this.LocalPartCollection[index];
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000879 RID: 2169 RVA: 0x0001BAFA File Offset: 0x00019CFA
		[XmlIgnore]
		public int Count
		{
			get
			{
				return this.LocalPartCollection.Count;
			}
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0001BB07 File Offset: 0x00019D07
		public void Clear()
		{
			this.LocalPartCollection.Clear();
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0001BB14 File Offset: 0x00019D14
		public string Remove(int index)
		{
			string text = this.LocalPartCollection[index];
			this.LocalPartCollection.Remove(text);
			return text;
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0001BB3B File Offset: 0x00019D3B
		public void Remove(object obj)
		{
			this.LocalPartCollection.Remove(obj);
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x0600087D RID: 2173 RVA: 0x0001BB49 File Offset: 0x00019D49
		// (set) Token: 0x0600087E RID: 2174 RVA: 0x0001BB64 File Offset: 0x00019D64
		[XmlIgnore]
		public LocalPartCollection LocalPartCollection
		{
			get
			{
				if (this.internalLocalPartCollection == null)
				{
					this.internalLocalPartCollection = new LocalPartCollection();
				}
				return this.internalLocalPartCollection;
			}
			set
			{
				this.internalLocalPartCollection = value;
			}
		}

		// Token: 0x04000490 RID: 1168
		[XmlElement(Type = typeof(string), ElementName = "LocalPart", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public LocalPartCollection internalLocalPartCollection;
	}
}
