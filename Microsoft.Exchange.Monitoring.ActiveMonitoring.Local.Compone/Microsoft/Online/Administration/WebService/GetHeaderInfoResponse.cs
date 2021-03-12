using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000339 RID: 825
	[DataContract(Name = "GetHeaderInfoResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class GetHeaderInfoResponse : Response
	{
		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x0600159B RID: 5531 RVA: 0x0008B966 File Offset: 0x00089B66
		// (set) Token: 0x0600159C RID: 5532 RVA: 0x0008B96E File Offset: 0x00089B6E
		[DataMember]
		public ClientVersionHeader ClientVersionHeader
		{
			get
			{
				return this.ClientVersionHeaderField;
			}
			set
			{
				this.ClientVersionHeaderField = value;
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x0600159D RID: 5533 RVA: 0x0008B977 File Offset: 0x00089B77
		// (set) Token: 0x0600159E RID: 5534 RVA: 0x0008B97F File Offset: 0x00089B7F
		[DataMember]
		public string ClientVersionHeaderName
		{
			get
			{
				return this.ClientVersionHeaderNameField;
			}
			set
			{
				this.ClientVersionHeaderNameField = value;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x0600159F RID: 5535 RVA: 0x0008B988 File Offset: 0x00089B88
		// (set) Token: 0x060015A0 RID: 5536 RVA: 0x0008B990 File Offset: 0x00089B90
		[DataMember]
		public Context ContextHeader
		{
			get
			{
				return this.ContextHeaderField;
			}
			set
			{
				this.ContextHeaderField = value;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x060015A1 RID: 5537 RVA: 0x0008B999 File Offset: 0x00089B99
		// (set) Token: 0x060015A2 RID: 5538 RVA: 0x0008B9A1 File Offset: 0x00089BA1
		[DataMember]
		public string ContextHeaderName
		{
			get
			{
				return this.ContextHeaderNameField;
			}
			set
			{
				this.ContextHeaderNameField = value;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x060015A3 RID: 5539 RVA: 0x0008B9AA File Offset: 0x00089BAA
		// (set) Token: 0x060015A4 RID: 5540 RVA: 0x0008B9B2 File Offset: 0x00089BB2
		[DataMember]
		public ContractVersionHeader ContractVersionHeader
		{
			get
			{
				return this.ContractVersionHeaderField;
			}
			set
			{
				this.ContractVersionHeaderField = value;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x060015A5 RID: 5541 RVA: 0x0008B9BB File Offset: 0x00089BBB
		// (set) Token: 0x060015A6 RID: 5542 RVA: 0x0008B9C3 File Offset: 0x00089BC3
		[DataMember]
		public string ContractVersionHeaderName
		{
			get
			{
				return this.ContractVersionHeaderNameField;
			}
			set
			{
				this.ContractVersionHeaderNameField = value;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x060015A7 RID: 5543 RVA: 0x0008B9CC File Offset: 0x00089BCC
		// (set) Token: 0x060015A8 RID: 5544 RVA: 0x0008B9D4 File Offset: 0x00089BD4
		[DataMember]
		public string HeaderNameSpace
		{
			get
			{
				return this.HeaderNameSpaceField;
			}
			set
			{
				this.HeaderNameSpaceField = value;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x060015A9 RID: 5545 RVA: 0x0008B9DD File Offset: 0x00089BDD
		// (set) Token: 0x060015AA RID: 5546 RVA: 0x0008B9E5 File Offset: 0x00089BE5
		[DataMember]
		public string IdentityHeaderName
		{
			get
			{
				return this.IdentityHeaderNameField;
			}
			set
			{
				this.IdentityHeaderNameField = value;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x060015AB RID: 5547 RVA: 0x0008B9EE File Offset: 0x00089BEE
		// (set) Token: 0x060015AC RID: 5548 RVA: 0x0008B9F6 File Offset: 0x00089BF6
		[DataMember]
		public UserIdentityHeader ReturnValue
		{
			get
			{
				return this.ReturnValueField;
			}
			set
			{
				this.ReturnValueField = value;
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x060015AD RID: 5549 RVA: 0x0008B9FF File Offset: 0x00089BFF
		// (set) Token: 0x060015AE RID: 5550 RVA: 0x0008BA07 File Offset: 0x00089C07
		[DataMember]
		public TrackingHeader TrackingHeader
		{
			get
			{
				return this.TrackingHeaderField;
			}
			set
			{
				this.TrackingHeaderField = value;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x060015AF RID: 5551 RVA: 0x0008BA10 File Offset: 0x00089C10
		// (set) Token: 0x060015B0 RID: 5552 RVA: 0x0008BA18 File Offset: 0x00089C18
		[DataMember]
		public string TrackingHeaderName
		{
			get
			{
				return this.TrackingHeaderNameField;
			}
			set
			{
				this.TrackingHeaderNameField = value;
			}
		}

		// Token: 0x04000FCC RID: 4044
		private ClientVersionHeader ClientVersionHeaderField;

		// Token: 0x04000FCD RID: 4045
		private string ClientVersionHeaderNameField;

		// Token: 0x04000FCE RID: 4046
		private Context ContextHeaderField;

		// Token: 0x04000FCF RID: 4047
		private string ContextHeaderNameField;

		// Token: 0x04000FD0 RID: 4048
		private ContractVersionHeader ContractVersionHeaderField;

		// Token: 0x04000FD1 RID: 4049
		private string ContractVersionHeaderNameField;

		// Token: 0x04000FD2 RID: 4050
		private string HeaderNameSpaceField;

		// Token: 0x04000FD3 RID: 4051
		private string IdentityHeaderNameField;

		// Token: 0x04000FD4 RID: 4052
		private UserIdentityHeader ReturnValueField;

		// Token: 0x04000FD5 RID: 4053
		private TrackingHeader TrackingHeaderField;

		// Token: 0x04000FD6 RID: 4054
		private string TrackingHeaderNameField;
	}
}
