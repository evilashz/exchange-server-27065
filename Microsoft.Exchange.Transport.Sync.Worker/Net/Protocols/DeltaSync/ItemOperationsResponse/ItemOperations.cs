using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.ItemOperationsResponse
{
	// Token: 0x020000CA RID: 202
	[XmlRoot(ElementName = "ItemOperations", Namespace = "ItemOperations:", IsNullable = false)]
	[Serializable]
	public class ItemOperations
	{
		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x0001A3A4 File Offset: 0x000185A4
		// (set) Token: 0x060006C7 RID: 1735 RVA: 0x0001A3AC File Offset: 0x000185AC
		[XmlIgnore]
		public int Status
		{
			get
			{
				return this.internalStatus;
			}
			set
			{
				this.internalStatus = value;
				this.internalStatusSpecified = true;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x0001A3BC File Offset: 0x000185BC
		// (set) Token: 0x060006C9 RID: 1737 RVA: 0x0001A3D7 File Offset: 0x000185D7
		[XmlIgnore]
		public ItemOperationsFault Fault
		{
			get
			{
				if (this.internalFault == null)
				{
					this.internalFault = new ItemOperationsFault();
				}
				return this.internalFault;
			}
			set
			{
				this.internalFault = value;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x0001A3E0 File Offset: 0x000185E0
		// (set) Token: 0x060006CB RID: 1739 RVA: 0x0001A3FB File Offset: 0x000185FB
		[XmlIgnore]
		public ItemOperationsAuthPolicy AuthPolicy
		{
			get
			{
				if (this.internalAuthPolicy == null)
				{
					this.internalAuthPolicy = new ItemOperationsAuthPolicy();
				}
				return this.internalAuthPolicy;
			}
			set
			{
				this.internalAuthPolicy = value;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x0001A404 File Offset: 0x00018604
		// (set) Token: 0x060006CD RID: 1741 RVA: 0x0001A41F File Offset: 0x0001861F
		[XmlIgnore]
		public Responses Responses
		{
			get
			{
				if (this.internalResponses == null)
				{
					this.internalResponses = new Responses();
				}
				return this.internalResponses;
			}
			set
			{
				this.internalResponses = value;
			}
		}

		// Token: 0x040003B0 RID: 944
		[XmlElement(ElementName = "Status", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "ItemOperations:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int internalStatus;

		// Token: 0x040003B1 RID: 945
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalStatusSpecified;

		// Token: 0x040003B2 RID: 946
		[XmlElement(Type = typeof(ItemOperationsFault), ElementName = "Fault", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "ItemOperations:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ItemOperationsFault internalFault;

		// Token: 0x040003B3 RID: 947
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(ItemOperationsAuthPolicy), ElementName = "AuthPolicy", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "ItemOperations:")]
		public ItemOperationsAuthPolicy internalAuthPolicy;

		// Token: 0x040003B4 RID: 948
		[XmlElement(Type = typeof(Responses), ElementName = "Responses", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "ItemOperations:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Responses internalResponses;
	}
}
