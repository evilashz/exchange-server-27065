using System;

namespace Microsoft.Exchange.Data.Transport.Email
{
	// Token: 0x02000105 RID: 261
	internal class TnefRecipient
	{
		// Token: 0x060007F6 RID: 2038 RVA: 0x0001D0E5 File Offset: 0x0001B2E5
		internal TnefRecipient(PureTnefMessage tnefMessage, int originalIndex, string displayName, string smtpAddress, string nativeAddress, string nativeAddressType)
		{
			this.tnefMessage = tnefMessage;
			this.originalIndex = originalIndex;
			this.displayName = displayName;
			this.smtpAddress = smtpAddress;
			this.nativeAddress = nativeAddress;
			this.nativeAddressType = nativeAddressType;
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060007F7 RID: 2039 RVA: 0x0001D11A File Offset: 0x0001B31A
		// (set) Token: 0x060007F8 RID: 2040 RVA: 0x0001D122 File Offset: 0x0001B322
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
			set
			{
				this.displayName = value;
				this.SetDirty();
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x0001D131 File Offset: 0x0001B331
		// (set) Token: 0x060007FA RID: 2042 RVA: 0x0001D139 File Offset: 0x0001B339
		public string SmtpAddress
		{
			get
			{
				return this.smtpAddress;
			}
			set
			{
				this.smtpAddress = value;
				this.SetDirty();
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060007FB RID: 2043 RVA: 0x0001D148 File Offset: 0x0001B348
		// (set) Token: 0x060007FC RID: 2044 RVA: 0x0001D150 File Offset: 0x0001B350
		public string NativeAddress
		{
			get
			{
				return this.nativeAddress;
			}
			set
			{
				this.nativeAddress = value;
				this.SetDirty();
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060007FD RID: 2045 RVA: 0x0001D15F File Offset: 0x0001B35F
		// (set) Token: 0x060007FE RID: 2046 RVA: 0x0001D167 File Offset: 0x0001B367
		public string NativeAddressType
		{
			get
			{
				return this.nativeAddressType;
			}
			set
			{
				this.nativeAddressType = value;
				this.SetDirty();
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060007FF RID: 2047 RVA: 0x0001D176 File Offset: 0x0001B376
		// (set) Token: 0x06000800 RID: 2048 RVA: 0x0001D17E File Offset: 0x0001B37E
		internal int OriginalIndex
		{
			get
			{
				return this.originalIndex;
			}
			set
			{
				this.originalIndex = value;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000801 RID: 2049 RVA: 0x0001D187 File Offset: 0x0001B387
		// (set) Token: 0x06000802 RID: 2050 RVA: 0x0001D18F File Offset: 0x0001B38F
		internal PureTnefMessage TnefMessage
		{
			get
			{
				return this.tnefMessage;
			}
			set
			{
				this.tnefMessage = value;
			}
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x0001D198 File Offset: 0x0001B398
		private void SetDirty()
		{
			if (this.tnefMessage != null)
			{
				this.tnefMessage.SetDirty(this);
			}
		}

		// Token: 0x0400045E RID: 1118
		private PureTnefMessage tnefMessage;

		// Token: 0x0400045F RID: 1119
		private string displayName;

		// Token: 0x04000460 RID: 1120
		private string smtpAddress;

		// Token: 0x04000461 RID: 1121
		private string nativeAddress;

		// Token: 0x04000462 RID: 1122
		private string nativeAddressType;

		// Token: 0x04000463 RID: 1123
		private int originalIndex;
	}
}
