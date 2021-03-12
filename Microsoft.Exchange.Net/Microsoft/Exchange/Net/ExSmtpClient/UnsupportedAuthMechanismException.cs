using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Net.ExSmtpClient
{
	// Token: 0x020000ED RID: 237
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UnsupportedAuthMechanismException : LocalizedException
	{
		// Token: 0x06000631 RID: 1585 RVA: 0x0001613E File Offset: 0x0001433E
		public UnsupportedAuthMechanismException(string authMechanism) : base(NetException.UnsupportedAuthMechanismException(authMechanism))
		{
			this.authMechanism = authMechanism;
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00016153 File Offset: 0x00014353
		public UnsupportedAuthMechanismException(string authMechanism, Exception innerException) : base(NetException.UnsupportedAuthMechanismException(authMechanism), innerException)
		{
			this.authMechanism = authMechanism;
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x00016169 File Offset: 0x00014369
		protected UnsupportedAuthMechanismException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.authMechanism = (string)info.GetValue("authMechanism", typeof(string));
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x00016193 File Offset: 0x00014393
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("authMechanism", this.authMechanism);
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x000161AE File Offset: 0x000143AE
		public string AuthMechanism
		{
			get
			{
				return this.authMechanism;
			}
		}

		// Token: 0x040004FE RID: 1278
		private readonly string authMechanism;
	}
}
