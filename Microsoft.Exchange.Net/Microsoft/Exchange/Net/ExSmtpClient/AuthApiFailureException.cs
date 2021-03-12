using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Net.ExSmtpClient
{
	// Token: 0x020000E4 RID: 228
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class AuthApiFailureException : LocalizedException
	{
		// Token: 0x06000605 RID: 1541 RVA: 0x00015D41 File Offset: 0x00013F41
		public AuthApiFailureException(string error) : base(NetException.AuthApiFailureException(error))
		{
			this.error = error;
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00015D56 File Offset: 0x00013F56
		public AuthApiFailureException(string error, Exception innerException) : base(NetException.AuthApiFailureException(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00015D6C File Offset: 0x00013F6C
		protected AuthApiFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00015D96 File Offset: 0x00013F96
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000609 RID: 1545 RVA: 0x00015DB1 File Offset: 0x00013FB1
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x040004F6 RID: 1270
		private readonly string error;
	}
}
