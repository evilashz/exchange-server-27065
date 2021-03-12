using System;
using System.ServiceModel;
using Microsoft.Exchange.Clients.Common;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200010B RID: 267
	public abstract class OwaExtendedErrorCodeException : FaultException
	{
		// Token: 0x0600099C RID: 2460 RVA: 0x00022A26 File Offset: 0x00020C26
		public OwaExtendedErrorCodeException(OwaExtendedErrorCode extendedErrorCode, string message, string user, FaultCode faultCode) : this(extendedErrorCode, message, user, faultCode, null)
		{
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x00022A34 File Offset: 0x00020C34
		public OwaExtendedErrorCodeException(OwaExtendedErrorCode extendedErrorCode, string message, string user, FaultCode faultCode, string errorData) : base(message, faultCode)
		{
			this.extendedErrorCode = extendedErrorCode;
			this.user = user;
			this.errorData = errorData;
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x00022A55 File Offset: 0x00020C55
		public OwaExtendedErrorCode ExtendedErrorCode
		{
			get
			{
				return this.extendedErrorCode;
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x0600099F RID: 2463 RVA: 0x00022A5D File Offset: 0x00020C5D
		public string User
		{
			get
			{
				return this.user;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x060009A0 RID: 2464 RVA: 0x00022A65 File Offset: 0x00020C65
		public string ErrorData
		{
			get
			{
				return this.errorData;
			}
		}

		// Token: 0x04000698 RID: 1688
		private readonly OwaExtendedErrorCode extendedErrorCode;

		// Token: 0x04000699 RID: 1689
		private readonly string user;

		// Token: 0x0400069A RID: 1690
		private readonly string errorData;
	}
}
