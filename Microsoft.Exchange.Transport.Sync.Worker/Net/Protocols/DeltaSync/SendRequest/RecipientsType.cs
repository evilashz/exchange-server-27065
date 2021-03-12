using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SendRequest
{
	// Token: 0x020000D5 RID: 213
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[XmlType(TypeName = "RecipientsType", Namespace = "Send:")]
	[Serializable]
	public class RecipientsType
	{
		// Token: 0x0600070F RID: 1807 RVA: 0x0001A744 File Offset: 0x00018944
		[DispId(-4)]
		public IEnumerator GetEnumerator()
		{
			return this.RecipientCollection.GetEnumerator();
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0001A751 File Offset: 0x00018951
		public string Add(string obj)
		{
			return this.RecipientCollection.Add(obj);
		}

		// Token: 0x17000277 RID: 631
		[XmlIgnore]
		public string this[int index]
		{
			get
			{
				return this.RecipientCollection[index];
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x0001A76D File Offset: 0x0001896D
		[XmlIgnore]
		public int Count
		{
			get
			{
				return this.RecipientCollection.Count;
			}
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0001A77A File Offset: 0x0001897A
		public void Clear()
		{
			this.RecipientCollection.Clear();
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0001A788 File Offset: 0x00018988
		public string Remove(int index)
		{
			string text = this.RecipientCollection[index];
			this.RecipientCollection.Remove(text);
			return text;
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0001A7AF File Offset: 0x000189AF
		public void Remove(object obj)
		{
			this.RecipientCollection.Remove(obj);
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x0001A7BD File Offset: 0x000189BD
		// (set) Token: 0x06000717 RID: 1815 RVA: 0x0001A7D8 File Offset: 0x000189D8
		[XmlIgnore]
		public RecipientCollection RecipientCollection
		{
			get
			{
				if (this.internalRecipientCollection == null)
				{
					this.internalRecipientCollection = new RecipientCollection();
				}
				return this.internalRecipientCollection;
			}
			set
			{
				this.internalRecipientCollection = value;
			}
		}

		// Token: 0x040003D3 RID: 979
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(string), ElementName = "Recipient", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "Send:")]
		public RecipientCollection internalRecipientCollection;
	}
}
