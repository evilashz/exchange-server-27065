using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001028 RID: 4136
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TlsSenderCertificateNameMatchesFfoFDSmtpCertificateException : LocalizedException
	{
		// Token: 0x0600AF82 RID: 44930 RVA: 0x002947C4 File Offset: 0x002929C4
		public TlsSenderCertificateNameMatchesFfoFDSmtpCertificateException(string certificate) : base(Strings.TlsSenderCertificateNameMatchesFfoFDSmtpCertificateId(certificate))
		{
			this.certificate = certificate;
		}

		// Token: 0x0600AF83 RID: 44931 RVA: 0x002947D9 File Offset: 0x002929D9
		public TlsSenderCertificateNameMatchesFfoFDSmtpCertificateException(string certificate, Exception innerException) : base(Strings.TlsSenderCertificateNameMatchesFfoFDSmtpCertificateId(certificate), innerException)
		{
			this.certificate = certificate;
		}

		// Token: 0x0600AF84 RID: 44932 RVA: 0x002947EF File Offset: 0x002929EF
		protected TlsSenderCertificateNameMatchesFfoFDSmtpCertificateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.certificate = (string)info.GetValue("certificate", typeof(string));
		}

		// Token: 0x0600AF85 RID: 44933 RVA: 0x00294819 File Offset: 0x00292A19
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("certificate", this.certificate);
		}

		// Token: 0x170037FF RID: 14335
		// (get) Token: 0x0600AF86 RID: 44934 RVA: 0x00294834 File Offset: 0x00292A34
		public string Certificate
		{
			get
			{
				return this.certificate;
			}
		}

		// Token: 0x04006165 RID: 24933
		private readonly string certificate;
	}
}
