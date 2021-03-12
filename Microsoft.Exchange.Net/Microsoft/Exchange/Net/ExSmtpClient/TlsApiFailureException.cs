using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Net.ExSmtpClient
{
	// Token: 0x020000EA RID: 234
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class TlsApiFailureException : LocalizedException
	{
		// Token: 0x06000621 RID: 1569 RVA: 0x00015F7F File Offset: 0x0001417F
		public TlsApiFailureException(string error) : base(NetException.TlsApiFailureException(error))
		{
			this.error = error;
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00015F94 File Offset: 0x00014194
		public TlsApiFailureException(string error, Exception innerException) : base(NetException.TlsApiFailureException(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00015FAA File Offset: 0x000141AA
		protected TlsApiFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00015FD4 File Offset: 0x000141D4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x00015FEF File Offset: 0x000141EF
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x040004FA RID: 1274
		private readonly string error;
	}
}
