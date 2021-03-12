using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Exchange.Net.Protocols.DeltaSync.HMSync;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SyncResponse
{
	// Token: 0x020001B9 RID: 441
	[XmlRoot(ElementName = "Sync", Namespace = "AirSync:", IsNullable = false)]
	[Serializable]
	public class Sync
	{
		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06000C53 RID: 3155 RVA: 0x0001E90D File Offset: 0x0001CB0D
		// (set) Token: 0x06000C54 RID: 3156 RVA: 0x0001E915 File Offset: 0x0001CB15
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

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06000C55 RID: 3157 RVA: 0x0001E925 File Offset: 0x0001CB25
		// (set) Token: 0x06000C56 RID: 3158 RVA: 0x0001E940 File Offset: 0x0001CB40
		[XmlIgnore]
		public Fault Fault
		{
			get
			{
				if (this.internalFault == null)
				{
					this.internalFault = new Fault();
				}
				return this.internalFault;
			}
			set
			{
				this.internalFault = value;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06000C57 RID: 3159 RVA: 0x0001E949 File Offset: 0x0001CB49
		// (set) Token: 0x06000C58 RID: 3160 RVA: 0x0001E964 File Offset: 0x0001CB64
		[XmlIgnore]
		public AuthPolicy AuthPolicy
		{
			get
			{
				if (this.internalAuthPolicy == null)
				{
					this.internalAuthPolicy = new AuthPolicy();
				}
				return this.internalAuthPolicy;
			}
			set
			{
				this.internalAuthPolicy = value;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06000C59 RID: 3161 RVA: 0x0001E96D File Offset: 0x0001CB6D
		// (set) Token: 0x06000C5A RID: 3162 RVA: 0x0001E988 File Offset: 0x0001CB88
		[XmlIgnore]
		public Collections Collections
		{
			get
			{
				if (this.internalCollections == null)
				{
					this.internalCollections = new Collections();
				}
				return this.internalCollections;
			}
			set
			{
				this.internalCollections = value;
			}
		}

		// Token: 0x040006EA RID: 1770
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "Status", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSYNC:")]
		public int internalStatus;

		// Token: 0x040006EB RID: 1771
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalStatusSpecified;

		// Token: 0x040006EC RID: 1772
		[XmlElement(Type = typeof(Fault), ElementName = "Fault", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSYNC:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Fault internalFault;

		// Token: 0x040006ED RID: 1773
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(AuthPolicy), ElementName = "AuthPolicy", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSYNC:")]
		public AuthPolicy internalAuthPolicy;

		// Token: 0x040006EE RID: 1774
		[XmlElement(Type = typeof(Collections), ElementName = "Collections", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "AirSync:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Collections internalCollections;
	}
}
