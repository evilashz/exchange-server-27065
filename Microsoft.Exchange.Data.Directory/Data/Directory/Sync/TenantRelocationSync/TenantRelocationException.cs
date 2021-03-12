using System;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Sync.TenantRelocationSync
{
	// Token: 0x020007F3 RID: 2035
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TenantRelocationException : Exception
	{
		// Token: 0x170023B1 RID: 9137
		// (get) Token: 0x0600649C RID: 25756 RVA: 0x0015CE44 File Offset: 0x0015B044
		// (set) Token: 0x0600649D RID: 25757 RVA: 0x0015CE4C File Offset: 0x0015B04C
		public RelocationError RelocationErrorCode { get; private set; }

		// Token: 0x0600649E RID: 25758 RVA: 0x0015CE55 File Offset: 0x0015B055
		public TenantRelocationException(RelocationError errorCode, string message, Exception innerException) : base(message, innerException)
		{
			this.RelocationErrorCode = errorCode;
		}

		// Token: 0x0600649F RID: 25759 RVA: 0x0015CE66 File Offset: 0x0015B066
		public TenantRelocationException(RelocationError errorCode, string message) : base(message)
		{
			this.RelocationErrorCode = errorCode;
		}
	}
}
