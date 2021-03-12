using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x0200014B RID: 331
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[XmlType(TypeName = "AddressesType", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class AddressesType
	{
		// Token: 0x06000987 RID: 2439 RVA: 0x0001C875 File Offset: 0x0001AA75
		[DispId(-4)]
		public IEnumerator GetEnumerator()
		{
			return this.AddressCollection.GetEnumerator();
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x0001C882 File Offset: 0x0001AA82
		public string Add(string obj)
		{
			return this.AddressCollection.Add(obj);
		}

		// Token: 0x17000359 RID: 857
		[XmlIgnore]
		public string this[int index]
		{
			get
			{
				return this.AddressCollection[index];
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x0600098A RID: 2442 RVA: 0x0001C89E File Offset: 0x0001AA9E
		[XmlIgnore]
		public int Count
		{
			get
			{
				return this.AddressCollection.Count;
			}
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0001C8AB File Offset: 0x0001AAAB
		public void Clear()
		{
			this.AddressCollection.Clear();
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0001C8B8 File Offset: 0x0001AAB8
		public string Remove(int index)
		{
			string text = this.AddressCollection[index];
			this.AddressCollection.Remove(text);
			return text;
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0001C8DF File Offset: 0x0001AADF
		public void Remove(object obj)
		{
			this.AddressCollection.Remove(obj);
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x0600098E RID: 2446 RVA: 0x0001C8ED File Offset: 0x0001AAED
		// (set) Token: 0x0600098F RID: 2447 RVA: 0x0001C908 File Offset: 0x0001AB08
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

		// Token: 0x04000551 RID: 1361
		[XmlElement(Type = typeof(string), ElementName = "Address", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public AddressCollection internalAddressCollection;
	}
}
