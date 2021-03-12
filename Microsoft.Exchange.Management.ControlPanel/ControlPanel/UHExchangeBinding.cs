using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004A1 RID: 1185
	[DataContract]
	public class UHExchangeBinding
	{
		// Token: 0x06003AF0 RID: 15088 RVA: 0x000B2697 File Offset: 0x000B0897
		public UHExchangeBinding(BindingMetadata exchangeBinding)
		{
			ArgumentValidator.ThrowIfNull("exchangeBinding", exchangeBinding);
			this.PrimarySmtpAddress = exchangeBinding.Name;
			this.displayName = exchangeBinding.DisplayName;
		}

		// Token: 0x1700234A RID: 9034
		// (get) Token: 0x06003AF1 RID: 15089 RVA: 0x000B26C2 File Offset: 0x000B08C2
		// (set) Token: 0x06003AF2 RID: 15090 RVA: 0x000B26CA File Offset: 0x000B08CA
		[DataMember]
		public string PrimarySmtpAddress
		{
			get
			{
				return this.primarySmtpAddress;
			}
			set
			{
				this.primarySmtpAddress = value;
			}
		}

		// Token: 0x1700234B RID: 9035
		// (get) Token: 0x06003AF3 RID: 15091 RVA: 0x000B26D3 File Offset: 0x000B08D3
		// (set) Token: 0x06003AF4 RID: 15092 RVA: 0x000B26DB File Offset: 0x000B08DB
		[DataMember]
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
			set
			{
				this.displayName = value;
			}
		}

		// Token: 0x0400273F RID: 10047
		private string primarySmtpAddress;

		// Token: 0x04002740 RID: 10048
		private string displayName;
	}
}
