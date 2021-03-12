using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x02000154 RID: 340
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[XmlType(TypeName = "DomainsType", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class DomainsType
	{
		// Token: 0x060009C9 RID: 2505 RVA: 0x0001CC4E File Offset: 0x0001AE4E
		[DispId(-4)]
		public IEnumerator GetEnumerator()
		{
			return this.DomainCollection.GetEnumerator();
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0001CC5B File Offset: 0x0001AE5B
		public string Add(string obj)
		{
			return this.DomainCollection.Add(obj);
		}

		// Token: 0x17000371 RID: 881
		[XmlIgnore]
		public string this[int index]
		{
			get
			{
				return this.DomainCollection[index];
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x060009CC RID: 2508 RVA: 0x0001CC77 File Offset: 0x0001AE77
		[XmlIgnore]
		public int Count
		{
			get
			{
				return this.DomainCollection.Count;
			}
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0001CC84 File Offset: 0x0001AE84
		public void Clear()
		{
			this.DomainCollection.Clear();
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x0001CC94 File Offset: 0x0001AE94
		public string Remove(int index)
		{
			string text = this.DomainCollection[index];
			this.DomainCollection.Remove(text);
			return text;
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x0001CCBB File Offset: 0x0001AEBB
		public void Remove(object obj)
		{
			this.DomainCollection.Remove(obj);
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x060009D0 RID: 2512 RVA: 0x0001CCC9 File Offset: 0x0001AEC9
		// (set) Token: 0x060009D1 RID: 2513 RVA: 0x0001CCE4 File Offset: 0x0001AEE4
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

		// Token: 0x04000568 RID: 1384
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(string), ElementName = "Domain", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "HMSETTINGS:")]
		public DomainCollection internalDomainCollection;
	}
}
