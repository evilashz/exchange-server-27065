using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000394 RID: 916
	[KnownType(typeof(LicenseQuotaExceededException))]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "InvalidUserLicenseException", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[KnownType(typeof(InvalidSubscriptionStatusException))]
	public class InvalidUserLicenseException : InvalidParameterException
	{
		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x0600167E RID: 5758 RVA: 0x0008C0C2 File Offset: 0x0008A2C2
		// (set) Token: 0x0600167F RID: 5759 RVA: 0x0008C0CA File Offset: 0x0008A2CA
		[DataMember]
		public string Account
		{
			get
			{
				return this.AccountField;
			}
			set
			{
				this.AccountField = value;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06001680 RID: 5760 RVA: 0x0008C0D3 File Offset: 0x0008A2D3
		// (set) Token: 0x06001681 RID: 5761 RVA: 0x0008C0DB File Offset: 0x0008A2DB
		[DataMember]
		public string Sku
		{
			get
			{
				return this.SkuField;
			}
			set
			{
				this.SkuField = value;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001682 RID: 5762 RVA: 0x0008C0E4 File Offset: 0x0008A2E4
		// (set) Token: 0x06001683 RID: 5763 RVA: 0x0008C0EC File Offset: 0x0008A2EC
		[DataMember]
		public Guid? SubscriptionId
		{
			get
			{
				return this.SubscriptionIdField;
			}
			set
			{
				this.SubscriptionIdField = value;
			}
		}

		// Token: 0x04001010 RID: 4112
		private string AccountField;

		// Token: 0x04001011 RID: 4113
		private string SkuField;

		// Token: 0x04001012 RID: 4114
		private Guid? SubscriptionIdField;
	}
}
