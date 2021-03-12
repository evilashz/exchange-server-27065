using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x0200015B RID: 347
	[XmlType(TypeName = "Addresses", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class Addresses
	{
		// Token: 0x06000A2E RID: 2606 RVA: 0x0001D171 File Offset: 0x0001B371
		[DispId(-4)]
		public IEnumerator GetEnumerator()
		{
			return this.AddressCollection.GetEnumerator();
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0001D17E File Offset: 0x0001B37E
		public string Add(string obj)
		{
			return this.AddressCollection.Add(obj);
		}

		// Token: 0x1700039D RID: 925
		[XmlIgnore]
		public string this[int index]
		{
			get
			{
				return this.AddressCollection[index];
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000A31 RID: 2609 RVA: 0x0001D19A File Offset: 0x0001B39A
		[XmlIgnore]
		public int Count
		{
			get
			{
				return this.AddressCollection.Count;
			}
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0001D1A7 File Offset: 0x0001B3A7
		public void Clear()
		{
			this.AddressCollection.Clear();
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0001D1B4 File Offset: 0x0001B3B4
		public string Remove(int index)
		{
			string text = this.AddressCollection[index];
			this.AddressCollection.Remove(text);
			return text;
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0001D1DB File Offset: 0x0001B3DB
		public void Remove(object obj)
		{
			this.AddressCollection.Remove(obj);
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000A35 RID: 2613 RVA: 0x0001D1E9 File Offset: 0x0001B3E9
		// (set) Token: 0x06000A36 RID: 2614 RVA: 0x0001D204 File Offset: 0x0001B404
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

		// Token: 0x040005AD RID: 1453
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(string), ElementName = "Address", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "HMSETTINGS:")]
		public AddressCollection internalAddressCollection;
	}
}
