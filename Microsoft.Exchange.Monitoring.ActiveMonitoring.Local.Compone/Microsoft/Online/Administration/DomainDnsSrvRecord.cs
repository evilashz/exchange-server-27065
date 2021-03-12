using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003F5 RID: 1013
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "DomainDnsSrvRecord", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	public class DomainDnsSrvRecord : DomainDnsRecord
	{
		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x0600191D RID: 6429 RVA: 0x0008D6E4 File Offset: 0x0008B8E4
		// (set) Token: 0x0600191E RID: 6430 RVA: 0x0008D6EC File Offset: 0x0008B8EC
		[DataMember]
		public string NameTarget
		{
			get
			{
				return this.NameTargetField;
			}
			set
			{
				this.NameTargetField = value;
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x0600191F RID: 6431 RVA: 0x0008D6F5 File Offset: 0x0008B8F5
		// (set) Token: 0x06001920 RID: 6432 RVA: 0x0008D6FD File Offset: 0x0008B8FD
		[DataMember]
		public int? Port
		{
			get
			{
				return this.PortField;
			}
			set
			{
				this.PortField = value;
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06001921 RID: 6433 RVA: 0x0008D706 File Offset: 0x0008B906
		// (set) Token: 0x06001922 RID: 6434 RVA: 0x0008D70E File Offset: 0x0008B90E
		[DataMember]
		public int? Priority
		{
			get
			{
				return this.PriorityField;
			}
			set
			{
				this.PriorityField = value;
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06001923 RID: 6435 RVA: 0x0008D717 File Offset: 0x0008B917
		// (set) Token: 0x06001924 RID: 6436 RVA: 0x0008D71F File Offset: 0x0008B91F
		[DataMember]
		public string Protocol
		{
			get
			{
				return this.ProtocolField;
			}
			set
			{
				this.ProtocolField = value;
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06001925 RID: 6437 RVA: 0x0008D728 File Offset: 0x0008B928
		// (set) Token: 0x06001926 RID: 6438 RVA: 0x0008D730 File Offset: 0x0008B930
		[DataMember]
		public string Service
		{
			get
			{
				return this.ServiceField;
			}
			set
			{
				this.ServiceField = value;
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06001927 RID: 6439 RVA: 0x0008D739 File Offset: 0x0008B939
		// (set) Token: 0x06001928 RID: 6440 RVA: 0x0008D741 File Offset: 0x0008B941
		[DataMember]
		public int? Weight
		{
			get
			{
				return this.WeightField;
			}
			set
			{
				this.WeightField = value;
			}
		}

		// Token: 0x04001199 RID: 4505
		private string NameTargetField;

		// Token: 0x0400119A RID: 4506
		private int? PortField;

		// Token: 0x0400119B RID: 4507
		private int? PriorityField;

		// Token: 0x0400119C RID: 4508
		private string ProtocolField;

		// Token: 0x0400119D RID: 4509
		private string ServiceField;

		// Token: 0x0400119E RID: 4510
		private int? WeightField;
	}
}
