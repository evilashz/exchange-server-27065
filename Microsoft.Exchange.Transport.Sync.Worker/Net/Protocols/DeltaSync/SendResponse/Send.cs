using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Exchange.Net.Protocols.DeltaSync.HMSync;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SendResponse
{
	// Token: 0x020000DC RID: 220
	[XmlRoot(ElementName = "Send", Namespace = "Send:", IsNullable = false)]
	[Serializable]
	public class Send
	{
		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000730 RID: 1840 RVA: 0x0001A942 File Offset: 0x00018B42
		// (set) Token: 0x06000731 RID: 1841 RVA: 0x0001A94A File Offset: 0x00018B4A
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

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x0001A95A File Offset: 0x00018B5A
		// (set) Token: 0x06000733 RID: 1843 RVA: 0x0001A975 File Offset: 0x00018B75
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

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x0001A97E File Offset: 0x00018B7E
		// (set) Token: 0x06000735 RID: 1845 RVA: 0x0001A999 File Offset: 0x00018B99
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

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x0001A9A2 File Offset: 0x00018BA2
		// (set) Token: 0x06000737 RID: 1847 RVA: 0x0001A9BD File Offset: 0x00018BBD
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

		// Token: 0x040003DE RID: 990
		[XmlElement(ElementName = "Status", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "Send:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int internalStatus;

		// Token: 0x040003DF RID: 991
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalStatusSpecified;

		// Token: 0x040003E0 RID: 992
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(Fault), ElementName = "Fault", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSYNC:")]
		public Fault internalFault;

		// Token: 0x040003E1 RID: 993
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(AuthPolicy), ElementName = "AuthPolicy", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSYNC:")]
		public AuthPolicy internalAuthPolicy;

		// Token: 0x040003E2 RID: 994
		[XmlElement(Type = typeof(Responses), ElementName = "Responses", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "Send:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Responses internalResponses;
	}
}
