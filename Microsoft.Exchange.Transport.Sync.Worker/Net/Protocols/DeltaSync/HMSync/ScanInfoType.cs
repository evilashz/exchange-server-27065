using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.HMSync
{
	// Token: 0x020000A6 RID: 166
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[XmlType(TypeName = "ScanInfoType", Namespace = "HMSYNC:")]
	[Serializable]
	public class ScanInfoType
	{
		// Token: 0x1700022A RID: 554
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x00019C15 File Offset: 0x00017E15
		// (set) Token: 0x06000630 RID: 1584 RVA: 0x00019C1D File Offset: 0x00017E1D
		[XmlIgnore]
		public byte Result
		{
			get
			{
				return this.internalResult;
			}
			set
			{
				this.internalResult = value;
				this.internalResultSpecified = true;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x00019C2D File Offset: 0x00017E2D
		// (set) Token: 0x06000632 RID: 1586 RVA: 0x00019C48 File Offset: 0x00017E48
		[XmlIgnore]
		public CleanedAttachments CleanedAttachments
		{
			get
			{
				if (this.internalCleanedAttachments == null)
				{
					this.internalCleanedAttachments = new CleanedAttachments();
				}
				return this.internalCleanedAttachments;
			}
			set
			{
				this.internalCleanedAttachments = value;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x00019C51 File Offset: 0x00017E51
		// (set) Token: 0x06000634 RID: 1588 RVA: 0x00019C6C File Offset: 0x00017E6C
		[XmlIgnore]
		public InfectedAttachments InfectedAttachments
		{
			get
			{
				if (this.internalInfectedAttachments == null)
				{
					this.internalInfectedAttachments = new InfectedAttachments();
				}
				return this.internalInfectedAttachments;
			}
			set
			{
				this.internalInfectedAttachments = value;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x00019C75 File Offset: 0x00017E75
		// (set) Token: 0x06000636 RID: 1590 RVA: 0x00019C90 File Offset: 0x00017E90
		[XmlIgnore]
		public SuspiciousAttachments SuspiciousAttachments
		{
			get
			{
				if (this.internalSuspiciousAttachments == null)
				{
					this.internalSuspiciousAttachments = new SuspiciousAttachments();
				}
				return this.internalSuspiciousAttachments;
			}
			set
			{
				this.internalSuspiciousAttachments = value;
			}
		}

		// Token: 0x0400037F RID: 895
		[XmlElement(ElementName = "Result", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "unsignedByte", Namespace = "HMSYNC:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public byte internalResult;

		// Token: 0x04000380 RID: 896
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalResultSpecified;

		// Token: 0x04000381 RID: 897
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(CleanedAttachments), ElementName = "CleanedAttachments", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSYNC:")]
		public CleanedAttachments internalCleanedAttachments;

		// Token: 0x04000382 RID: 898
		[XmlElement(Type = typeof(InfectedAttachments), ElementName = "InfectedAttachments", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSYNC:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public InfectedAttachments internalInfectedAttachments;

		// Token: 0x04000383 RID: 899
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(SuspiciousAttachments), ElementName = "SuspiciousAttachments", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSYNC:")]
		public SuspiciousAttachments internalSuspiciousAttachments;
	}
}
