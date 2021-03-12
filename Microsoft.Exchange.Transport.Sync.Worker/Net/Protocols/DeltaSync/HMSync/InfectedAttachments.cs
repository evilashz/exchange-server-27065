using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.HMSync
{
	// Token: 0x020000AA RID: 170
	[XmlType(TypeName = "InfectedAttachments", Namespace = "HMSYNC:")]
	[Serializable]
	public class InfectedAttachments
	{
		// Token: 0x06000646 RID: 1606 RVA: 0x00019D79 File Offset: 0x00017F79
		[DispId(-4)]
		public IEnumerator GetEnumerator()
		{
			return this.AttachmentCollection.GetEnumerator();
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x00019D86 File Offset: 0x00017F86
		public AttachmentVirusInfoType Add(AttachmentVirusInfoType obj)
		{
			return this.AttachmentCollection.Add(obj);
		}

		// Token: 0x17000232 RID: 562
		[XmlIgnore]
		public AttachmentVirusInfoType this[int index]
		{
			get
			{
				return this.AttachmentCollection[index];
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x00019DA2 File Offset: 0x00017FA2
		[XmlIgnore]
		public int Count
		{
			get
			{
				return this.AttachmentCollection.Count;
			}
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00019DAF File Offset: 0x00017FAF
		public void Clear()
		{
			this.AttachmentCollection.Clear();
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x00019DBC File Offset: 0x00017FBC
		public AttachmentVirusInfoType Remove(int index)
		{
			AttachmentVirusInfoType attachmentVirusInfoType = this.AttachmentCollection[index];
			this.AttachmentCollection.Remove(attachmentVirusInfoType);
			return attachmentVirusInfoType;
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00019DE3 File Offset: 0x00017FE3
		public void Remove(object obj)
		{
			this.AttachmentCollection.Remove(obj);
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x00019DF1 File Offset: 0x00017FF1
		// (set) Token: 0x0600064E RID: 1614 RVA: 0x00019E0C File Offset: 0x0001800C
		[XmlIgnore]
		public AttachmentVirusInfoTypeCollection AttachmentCollection
		{
			get
			{
				if (this.internalAttachmentCollection == null)
				{
					this.internalAttachmentCollection = new AttachmentVirusInfoTypeCollection();
				}
				return this.internalAttachmentCollection;
			}
			set
			{
				this.internalAttachmentCollection = value;
			}
		}

		// Token: 0x04000386 RID: 902
		[XmlElement(Type = typeof(AttachmentVirusInfoType), ElementName = "Attachment", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSYNC:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public AttachmentVirusInfoTypeCollection internalAttachmentCollection;
	}
}
