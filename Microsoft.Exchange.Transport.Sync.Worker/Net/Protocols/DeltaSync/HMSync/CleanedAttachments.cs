using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.HMSync
{
	// Token: 0x020000A7 RID: 167
	[XmlType(TypeName = "CleanedAttachments", Namespace = "HMSYNC:")]
	[Serializable]
	public class CleanedAttachments
	{
		// Token: 0x06000638 RID: 1592 RVA: 0x00019CA1 File Offset: 0x00017EA1
		[DispId(-4)]
		public IEnumerator GetEnumerator()
		{
			return this.AttachmentCollection.GetEnumerator();
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00019CAE File Offset: 0x00017EAE
		public Attachment Add(Attachment obj)
		{
			return this.AttachmentCollection.Add(obj);
		}

		// Token: 0x1700022E RID: 558
		[XmlIgnore]
		public Attachment this[int index]
		{
			get
			{
				return this.AttachmentCollection[index];
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x00019CCA File Offset: 0x00017ECA
		[XmlIgnore]
		public int Count
		{
			get
			{
				return this.AttachmentCollection.Count;
			}
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00019CD7 File Offset: 0x00017ED7
		public void Clear()
		{
			this.AttachmentCollection.Clear();
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00019CE4 File Offset: 0x00017EE4
		public Attachment Remove(int index)
		{
			Attachment attachment = this.AttachmentCollection[index];
			this.AttachmentCollection.Remove(attachment);
			return attachment;
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x00019D0B File Offset: 0x00017F0B
		public void Remove(object obj)
		{
			this.AttachmentCollection.Remove(obj);
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x00019D19 File Offset: 0x00017F19
		// (set) Token: 0x06000640 RID: 1600 RVA: 0x00019D34 File Offset: 0x00017F34
		[XmlIgnore]
		public AttachmentCollection AttachmentCollection
		{
			get
			{
				if (this.internalAttachmentCollection == null)
				{
					this.internalAttachmentCollection = new AttachmentCollection();
				}
				return this.internalAttachmentCollection;
			}
			set
			{
				this.internalAttachmentCollection = value;
			}
		}

		// Token: 0x04000384 RID: 900
		[XmlElement(Type = typeof(Attachment), ElementName = "Attachment", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSYNC:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public AttachmentCollection internalAttachmentCollection;
	}
}
