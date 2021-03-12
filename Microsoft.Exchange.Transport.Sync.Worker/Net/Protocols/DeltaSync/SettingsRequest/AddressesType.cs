using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x02000115 RID: 277
	[XmlType(TypeName = "AddressesType", Namespace = "HMSETTINGS:")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class AddressesType
	{
		// Token: 0x0600082A RID: 2090 RVA: 0x0001B650 File Offset: 0x00019850
		[DispId(-4)]
		public IEnumerator GetEnumerator()
		{
			return this.AddressCollection.GetEnumerator();
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x0001B65D File Offset: 0x0001985D
		public string Add(string obj)
		{
			return this.AddressCollection.Add(obj);
		}

		// Token: 0x170002D9 RID: 729
		[XmlIgnore]
		public string this[int index]
		{
			get
			{
				return this.AddressCollection[index];
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x0600082D RID: 2093 RVA: 0x0001B679 File Offset: 0x00019879
		[XmlIgnore]
		public int Count
		{
			get
			{
				return this.AddressCollection.Count;
			}
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x0001B686 File Offset: 0x00019886
		public void Clear()
		{
			this.AddressCollection.Clear();
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x0001B694 File Offset: 0x00019894
		public string Remove(int index)
		{
			string text = this.AddressCollection[index];
			this.AddressCollection.Remove(text);
			return text;
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0001B6BB File Offset: 0x000198BB
		public void Remove(object obj)
		{
			this.AddressCollection.Remove(obj);
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000831 RID: 2097 RVA: 0x0001B6C9 File Offset: 0x000198C9
		// (set) Token: 0x06000832 RID: 2098 RVA: 0x0001B6E4 File Offset: 0x000198E4
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

		// Token: 0x04000478 RID: 1144
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(string), ElementName = "Address", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "HMSETTINGS:")]
		public AddressCollection internalAddressCollection;
	}
}
