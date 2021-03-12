using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000248 RID: 584
	internal class ProxyHeaderValue
	{
		// Token: 0x06000F67 RID: 3943 RVA: 0x0004BD18 File Offset: 0x00049F18
		public ProxyHeaderValue(ProxyHeaderType proxyHeaderType, byte[] value)
		{
			this.proxyHeaderType = proxyHeaderType;
			this.value = value;
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000F68 RID: 3944 RVA: 0x0004BD2E File Offset: 0x00049F2E
		public byte[] Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000F69 RID: 3945 RVA: 0x0004BD36 File Offset: 0x00049F36
		public ProxyHeaderType ProxyHeaderType
		{
			get
			{
				return this.proxyHeaderType;
			}
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x0004BD3E File Offset: 0x00049F3E
		public void ValidateSize()
		{
			if (this.value != null && this.value.Length > ProxyHeaderValue.MaxSizeLimit)
			{
				throw new InvalidProxySecurityContextException();
			}
		}

		// Token: 0x04000BB7 RID: 2999
		internal static readonly int MaxSizeLimit = 3145728;

		// Token: 0x04000BB8 RID: 3000
		private ProxyHeaderType proxyHeaderType;

		// Token: 0x04000BB9 RID: 3001
		private byte[] value;
	}
}
