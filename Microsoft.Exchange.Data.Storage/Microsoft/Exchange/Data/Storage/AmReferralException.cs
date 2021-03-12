using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000D3 RID: 211
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmReferralException : AmServerException
	{
		// Token: 0x0600129D RID: 4765 RVA: 0x00067F59 File Offset: 0x00066159
		public AmReferralException(string referredServer) : base(ServerStrings.AmReferralException(referredServer))
		{
			this.referredServer = referredServer;
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x00067F73 File Offset: 0x00066173
		public AmReferralException(string referredServer, Exception innerException) : base(ServerStrings.AmReferralException(referredServer), innerException)
		{
			this.referredServer = referredServer;
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x00067F8E File Offset: 0x0006618E
		protected AmReferralException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.referredServer = (string)info.GetValue("referredServer", typeof(string));
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x00067FB8 File Offset: 0x000661B8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("referredServer", this.referredServer);
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x060012A1 RID: 4769 RVA: 0x00067FD3 File Offset: 0x000661D3
		public string ReferredServer
		{
			get
			{
				return this.referredServer;
			}
		}

		// Token: 0x04000963 RID: 2403
		private readonly string referredServer;
	}
}
