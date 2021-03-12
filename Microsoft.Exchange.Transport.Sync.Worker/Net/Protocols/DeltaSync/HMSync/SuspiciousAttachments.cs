using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.HMSync
{
	// Token: 0x020000AB RID: 171
	[XmlType(TypeName = "SuspiciousAttachments", Namespace = "HMSYNC:")]
	[Serializable]
	public class SuspiciousAttachments
	{
		// Token: 0x06000650 RID: 1616 RVA: 0x00019E1D File Offset: 0x0001801D
		[DispId(-4)]
		public IEnumerator GetEnumerator()
		{
			return this.AttachmentCollection.GetEnumerator();
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00019E2A File Offset: 0x0001802A
		public AttachmentVirusInfoType Add(AttachmentVirusInfoType obj)
		{
			return this.AttachmentCollection.Add(obj);
		}

		// Token: 0x17000235 RID: 565
		[XmlIgnore]
		public AttachmentVirusInfoType this[int index]
		{
			get
			{
				return this.AttachmentCollection[index];
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x00019E46 File Offset: 0x00018046
		[XmlIgnore]
		public int Count
		{
			get
			{
				return this.AttachmentCollection.Count;
			}
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x00019E53 File Offset: 0x00018053
		public void Clear()
		{
			this.AttachmentCollection.Clear();
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00019E60 File Offset: 0x00018060
		public AttachmentVirusInfoType Remove(int index)
		{
			AttachmentVirusInfoType attachmentVirusInfoType = this.AttachmentCollection[index];
			this.AttachmentCollection.Remove(attachmentVirusInfoType);
			return attachmentVirusInfoType;
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00019E87 File Offset: 0x00018087
		public void Remove(object obj)
		{
			this.AttachmentCollection.Remove(obj);
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x00019E95 File Offset: 0x00018095
		// (set) Token: 0x06000658 RID: 1624 RVA: 0x00019EB0 File Offset: 0x000180B0
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

		// Token: 0x04000387 RID: 903
		[XmlElement(Type = typeof(AttachmentVirusInfoType), ElementName = "Attachment", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSYNC:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public AttachmentVirusInfoTypeCollection internalAttachmentCollection;
	}
}
