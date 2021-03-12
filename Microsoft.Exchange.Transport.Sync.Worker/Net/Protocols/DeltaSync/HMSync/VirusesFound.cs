using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.HMSync
{
	// Token: 0x020000A5 RID: 165
	[XmlType(TypeName = "VirusesFound", Namespace = "HMSYNC:")]
	[Serializable]
	public class VirusesFound
	{
		// Token: 0x06000625 RID: 1573 RVA: 0x00019B70 File Offset: 0x00017D70
		[DispId(-4)]
		public IEnumerator GetEnumerator()
		{
			return this.NameCollection.GetEnumerator();
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00019B7D File Offset: 0x00017D7D
		public string Add(string obj)
		{
			return this.NameCollection.Add(obj);
		}

		// Token: 0x17000227 RID: 551
		[XmlIgnore]
		public string this[int index]
		{
			get
			{
				return this.NameCollection[index];
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x00019B99 File Offset: 0x00017D99
		[XmlIgnore]
		public int Count
		{
			get
			{
				return this.NameCollection.Count;
			}
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00019BA6 File Offset: 0x00017DA6
		public void Clear()
		{
			this.NameCollection.Clear();
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x00019BB4 File Offset: 0x00017DB4
		public string Remove(int index)
		{
			string text = this.NameCollection[index];
			this.NameCollection.Remove(text);
			return text;
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x00019BDB File Offset: 0x00017DDB
		public void Remove(object obj)
		{
			this.NameCollection.Remove(obj);
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x00019BE9 File Offset: 0x00017DE9
		// (set) Token: 0x0600062D RID: 1581 RVA: 0x00019C04 File Offset: 0x00017E04
		[XmlIgnore]
		public NameCollection NameCollection
		{
			get
			{
				if (this.internalNameCollection == null)
				{
					this.internalNameCollection = new NameCollection();
				}
				return this.internalNameCollection;
			}
			set
			{
				this.internalNameCollection = value;
			}
		}

		// Token: 0x0400037E RID: 894
		[XmlElement(Type = typeof(string), ElementName = "Name", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "HMSYNC:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public NameCollection internalNameCollection;
	}
}
