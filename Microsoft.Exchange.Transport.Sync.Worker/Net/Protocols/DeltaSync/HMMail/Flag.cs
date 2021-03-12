using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.HMMail
{
	// Token: 0x0200009D RID: 157
	[XmlRoot(ElementName = "Flag", Namespace = "HMMAIL:", IsNullable = false)]
	[Serializable]
	public class Flag
	{
		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x00019952 File Offset: 0x00017B52
		// (set) Token: 0x060005FA RID: 1530 RVA: 0x0001996D File Offset: 0x00017B6D
		[XmlIgnore]
		public StateCollection StateCollection
		{
			get
			{
				if (this.internalStateCollection == null)
				{
					this.internalStateCollection = new StateCollection();
				}
				return this.internalStateCollection;
			}
			set
			{
				this.internalStateCollection = value;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x00019976 File Offset: 0x00017B76
		// (set) Token: 0x060005FC RID: 1532 RVA: 0x00019991 File Offset: 0x00017B91
		[XmlIgnore]
		public stringWithCharSetTypeCollection TitleCollection
		{
			get
			{
				if (this.internalTitleCollection == null)
				{
					this.internalTitleCollection = new stringWithCharSetTypeCollection();
				}
				return this.internalTitleCollection;
			}
			set
			{
				this.internalTitleCollection = value;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x0001999A File Offset: 0x00017B9A
		// (set) Token: 0x060005FE RID: 1534 RVA: 0x000199B5 File Offset: 0x00017BB5
		[XmlIgnore]
		public ReminderDateCollection ReminderDateCollection
		{
			get
			{
				if (this.internalReminderDateCollection == null)
				{
					this.internalReminderDateCollection = new ReminderDateCollection();
				}
				return this.internalReminderDateCollection;
			}
			set
			{
				this.internalReminderDateCollection = value;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x000199BE File Offset: 0x00017BBE
		// (set) Token: 0x06000600 RID: 1536 RVA: 0x000199D9 File Offset: 0x00017BD9
		[XmlIgnore]
		public CompletedCollection CompletedCollection
		{
			get
			{
				if (this.internalCompletedCollection == null)
				{
					this.internalCompletedCollection = new CompletedCollection();
				}
				return this.internalCompletedCollection;
			}
			set
			{
				this.internalCompletedCollection = value;
			}
		}

		// Token: 0x04000371 RID: 881
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(byte), ElementName = "State", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "unsignedByte", Namespace = "HMMAIL:")]
		public StateCollection internalStateCollection;

		// Token: 0x04000372 RID: 882
		[XmlElement(Type = typeof(stringWithCharSetType), ElementName = "Title", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMMAIL:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public stringWithCharSetTypeCollection internalTitleCollection;

		// Token: 0x04000373 RID: 883
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(string), ElementName = "ReminderDate", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "HMMAIL:")]
		public ReminderDateCollection internalReminderDateCollection;

		// Token: 0x04000374 RID: 884
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(bitType), ElementName = "Completed", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMMAIL:")]
		public CompletedCollection internalCompletedCollection;
	}
}
