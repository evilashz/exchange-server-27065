using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x02000155 RID: 341
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[XmlType(TypeName = "LocalPartsType", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class LocalPartsType
	{
		// Token: 0x060009D3 RID: 2515 RVA: 0x0001CCF5 File Offset: 0x0001AEF5
		[DispId(-4)]
		public IEnumerator GetEnumerator()
		{
			return this.LocalPartCollection.GetEnumerator();
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x0001CD02 File Offset: 0x0001AF02
		public string Add(string obj)
		{
			return this.LocalPartCollection.Add(obj);
		}

		// Token: 0x17000374 RID: 884
		[XmlIgnore]
		public string this[int index]
		{
			get
			{
				return this.LocalPartCollection[index];
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x0001CD1E File Offset: 0x0001AF1E
		[XmlIgnore]
		public int Count
		{
			get
			{
				return this.LocalPartCollection.Count;
			}
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0001CD2B File Offset: 0x0001AF2B
		public void Clear()
		{
			this.LocalPartCollection.Clear();
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0001CD38 File Offset: 0x0001AF38
		public string Remove(int index)
		{
			string text = this.LocalPartCollection[index];
			this.LocalPartCollection.Remove(text);
			return text;
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0001CD5F File Offset: 0x0001AF5F
		public void Remove(object obj)
		{
			this.LocalPartCollection.Remove(obj);
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x060009DA RID: 2522 RVA: 0x0001CD6D File Offset: 0x0001AF6D
		// (set) Token: 0x060009DB RID: 2523 RVA: 0x0001CD88 File Offset: 0x0001AF88
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

		// Token: 0x04000569 RID: 1385
		[XmlElement(Type = typeof(string), ElementName = "LocalPart", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public LocalPartCollection internalLocalPartCollection;
	}
}
