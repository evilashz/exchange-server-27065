using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003F4 RID: 1012
	[DataContract(Name = "DomainDnsRecord", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	[KnownType(typeof(DomainDnsTxtRecord))]
	[KnownType(typeof(DomainDnsMXRecord))]
	[KnownType(typeof(DomainDnsNullRecord))]
	[KnownType(typeof(DomainDnsSrvRecord))]
	[KnownType(typeof(DomainDnsCnameRecord))]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class DomainDnsRecord : IExtensibleDataObject
	{
		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06001910 RID: 6416 RVA: 0x0008D676 File Offset: 0x0008B876
		// (set) Token: 0x06001911 RID: 6417 RVA: 0x0008D67E File Offset: 0x0008B87E
		public ExtensionDataObject ExtensionData
		{
			get
			{
				return this.extensionDataField;
			}
			set
			{
				this.extensionDataField = value;
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06001912 RID: 6418 RVA: 0x0008D687 File Offset: 0x0008B887
		// (set) Token: 0x06001913 RID: 6419 RVA: 0x0008D68F File Offset: 0x0008B88F
		[DataMember]
		public DomainCapabilities? Capability
		{
			get
			{
				return this.CapabilityField;
			}
			set
			{
				this.CapabilityField = value;
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06001914 RID: 6420 RVA: 0x0008D698 File Offset: 0x0008B898
		// (set) Token: 0x06001915 RID: 6421 RVA: 0x0008D6A0 File Offset: 0x0008B8A0
		[DataMember]
		public bool? IsOptional
		{
			get
			{
				return this.IsOptionalField;
			}
			set
			{
				this.IsOptionalField = value;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06001916 RID: 6422 RVA: 0x0008D6A9 File Offset: 0x0008B8A9
		// (set) Token: 0x06001917 RID: 6423 RVA: 0x0008D6B1 File Offset: 0x0008B8B1
		[DataMember]
		public string Label
		{
			get
			{
				return this.LabelField;
			}
			set
			{
				this.LabelField = value;
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06001918 RID: 6424 RVA: 0x0008D6BA File Offset: 0x0008B8BA
		// (set) Token: 0x06001919 RID: 6425 RVA: 0x0008D6C2 File Offset: 0x0008B8C2
		[DataMember]
		public Guid? ObjectId
		{
			get
			{
				return this.ObjectIdField;
			}
			set
			{
				this.ObjectIdField = value;
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x0600191A RID: 6426 RVA: 0x0008D6CB File Offset: 0x0008B8CB
		// (set) Token: 0x0600191B RID: 6427 RVA: 0x0008D6D3 File Offset: 0x0008B8D3
		[DataMember]
		public int? Ttl
		{
			get
			{
				return this.TtlField;
			}
			set
			{
				this.TtlField = value;
			}
		}

		// Token: 0x04001193 RID: 4499
		private ExtensionDataObject extensionDataField;

		// Token: 0x04001194 RID: 4500
		private DomainCapabilities? CapabilityField;

		// Token: 0x04001195 RID: 4501
		private bool? IsOptionalField;

		// Token: 0x04001196 RID: 4502
		private string LabelField;

		// Token: 0x04001197 RID: 4503
		private Guid? ObjectIdField;

		// Token: 0x04001198 RID: 4504
		private int? TtlField;
	}
}
