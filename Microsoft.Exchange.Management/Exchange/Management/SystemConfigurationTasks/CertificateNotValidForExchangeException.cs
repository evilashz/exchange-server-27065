using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F52 RID: 3922
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CertificateNotValidForExchangeException : LocalizedException
	{
		// Token: 0x0600AB8F RID: 43919 RVA: 0x0028F328 File Offset: 0x0028D528
		public CertificateNotValidForExchangeException(string thumbprint, string reason) : base(Strings.CertificateNotValidForExchange(thumbprint, reason))
		{
			this.thumbprint = thumbprint;
			this.reason = reason;
		}

		// Token: 0x0600AB90 RID: 43920 RVA: 0x0028F345 File Offset: 0x0028D545
		public CertificateNotValidForExchangeException(string thumbprint, string reason, Exception innerException) : base(Strings.CertificateNotValidForExchange(thumbprint, reason), innerException)
		{
			this.thumbprint = thumbprint;
			this.reason = reason;
		}

		// Token: 0x0600AB91 RID: 43921 RVA: 0x0028F364 File Offset: 0x0028D564
		protected CertificateNotValidForExchangeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.thumbprint = (string)info.GetValue("thumbprint", typeof(string));
			this.reason = (string)info.GetValue("reason", typeof(string));
		}

		// Token: 0x0600AB92 RID: 43922 RVA: 0x0028F3B9 File Offset: 0x0028D5B9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("thumbprint", this.thumbprint);
			info.AddValue("reason", this.reason);
		}

		// Token: 0x17003764 RID: 14180
		// (get) Token: 0x0600AB93 RID: 43923 RVA: 0x0028F3E5 File Offset: 0x0028D5E5
		public string Thumbprint
		{
			get
			{
				return this.thumbprint;
			}
		}

		// Token: 0x17003765 RID: 14181
		// (get) Token: 0x0600AB94 RID: 43924 RVA: 0x0028F3ED File Offset: 0x0028D5ED
		public string Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x040060CA RID: 24778
		private readonly string thumbprint;

		// Token: 0x040060CB RID: 24779
		private readonly string reason;
	}
}
