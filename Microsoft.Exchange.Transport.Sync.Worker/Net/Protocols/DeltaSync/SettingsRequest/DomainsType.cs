using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x0200011E RID: 286
	[XmlType(TypeName = "DomainsType", Namespace = "HMSETTINGS:")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class DomainsType
	{
		// Token: 0x0600086C RID: 2156 RVA: 0x0001BA2A File Offset: 0x00019C2A
		[DispId(-4)]
		public IEnumerator GetEnumerator()
		{
			return this.DomainCollection.GetEnumerator();
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x0001BA37 File Offset: 0x00019C37
		public string Add(string obj)
		{
			return this.DomainCollection.Add(obj);
		}

		// Token: 0x170002F1 RID: 753
		[XmlIgnore]
		public string this[int index]
		{
			get
			{
				return this.DomainCollection[index];
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x0600086F RID: 2159 RVA: 0x0001BA53 File Offset: 0x00019C53
		[XmlIgnore]
		public int Count
		{
			get
			{
				return this.DomainCollection.Count;
			}
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0001BA60 File Offset: 0x00019C60
		public void Clear()
		{
			this.DomainCollection.Clear();
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0001BA70 File Offset: 0x00019C70
		public string Remove(int index)
		{
			string text = this.DomainCollection[index];
			this.DomainCollection.Remove(text);
			return text;
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x0001BA97 File Offset: 0x00019C97
		public void Remove(object obj)
		{
			this.DomainCollection.Remove(obj);
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000873 RID: 2163 RVA: 0x0001BAA5 File Offset: 0x00019CA5
		// (set) Token: 0x06000874 RID: 2164 RVA: 0x0001BAC0 File Offset: 0x00019CC0
		[XmlIgnore]
		public DomainCollection DomainCollection
		{
			get
			{
				if (this.internalDomainCollection == null)
				{
					this.internalDomainCollection = new DomainCollection();
				}
				return this.internalDomainCollection;
			}
			set
			{
				this.internalDomainCollection = value;
			}
		}

		// Token: 0x0400048F RID: 1167
		[XmlElement(Type = typeof(string), ElementName = "Domain", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DomainCollection internalDomainCollection;
	}
}
