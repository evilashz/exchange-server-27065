using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x02000125 RID: 293
	[XmlType(TypeName = "Addresses", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class Addresses
	{
		// Token: 0x060008D1 RID: 2257 RVA: 0x0001BF4D File Offset: 0x0001A14D
		[DispId(-4)]
		public IEnumerator GetEnumerator()
		{
			return this.AddressCollection.GetEnumerator();
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0001BF5A File Offset: 0x0001A15A
		public string Add(string obj)
		{
			return this.AddressCollection.Add(obj);
		}

		// Token: 0x1700031D RID: 797
		[XmlIgnore]
		public string this[int index]
		{
			get
			{
				return this.AddressCollection[index];
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x0001BF76 File Offset: 0x0001A176
		[XmlIgnore]
		public int Count
		{
			get
			{
				return this.AddressCollection.Count;
			}
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0001BF83 File Offset: 0x0001A183
		public void Clear()
		{
			this.AddressCollection.Clear();
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0001BF90 File Offset: 0x0001A190
		public string Remove(int index)
		{
			string text = this.AddressCollection[index];
			this.AddressCollection.Remove(text);
			return text;
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0001BFB7 File Offset: 0x0001A1B7
		public void Remove(object obj)
		{
			this.AddressCollection.Remove(obj);
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x060008D8 RID: 2264 RVA: 0x0001BFC5 File Offset: 0x0001A1C5
		// (set) Token: 0x060008D9 RID: 2265 RVA: 0x0001BFE0 File Offset: 0x0001A1E0
		[XmlIgnore]
		public AddressCollection AddressCollection
		{
			get
			{
				if (this.internalAddressCollection == null)
				{
					this.internalAddressCollection = new AddressCollection();
				}
				return this.internalAddressCollection;
			}
			set
			{
				this.internalAddressCollection = value;
			}
		}

		// Token: 0x040004D4 RID: 1236
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(string), ElementName = "Address", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "HMSETTINGS:")]
		public AddressCollection internalAddressCollection;
	}
}
