using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.ItemOperationsRequest
{
	// Token: 0x020000BD RID: 189
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[XmlType(TypeName = "ReportAsSpamType", Namespace = "ItemOperations:")]
	[Serializable]
	public class ReportAsSpamType : ItemOpsBaseType
	{
		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x0001A11A File Offset: 0x0001831A
		// (set) Token: 0x06000697 RID: 1687 RVA: 0x0001A135 File Offset: 0x00018335
		[XmlIgnore]
		public MessageType MessageType
		{
			get
			{
				if (this.internalMessageType == null)
				{
					this.internalMessageType = new MessageType();
				}
				return this.internalMessageType;
			}
			set
			{
				this.internalMessageType = value;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000698 RID: 1688 RVA: 0x0001A13E File Offset: 0x0001833E
		// (set) Token: 0x06000699 RID: 1689 RVA: 0x0001A159 File Offset: 0x00018359
		[XmlIgnore]
		public ReportAsSpamTypeOptions Options
		{
			get
			{
				if (this.internalOptions == null)
				{
					this.internalOptions = new ReportAsSpamTypeOptions();
				}
				return this.internalOptions;
			}
			set
			{
				this.internalOptions = value;
			}
		}

		// Token: 0x040003A2 RID: 930
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(MessageType), ElementName = "MessageType", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "ItemOperations:")]
		public MessageType internalMessageType;

		// Token: 0x040003A3 RID: 931
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(ReportAsSpamTypeOptions), ElementName = "Options", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "ItemOperations:")]
		public ReportAsSpamTypeOptions internalOptions;
	}
}
