using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001029 RID: 4137
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TlsSenderCertificateNameMatchesServiceProviderCertificateException : LocalizedException
	{
		// Token: 0x0600AF87 RID: 44935 RVA: 0x0029483C File Offset: 0x00292A3C
		public TlsSenderCertificateNameMatchesServiceProviderCertificateException(string certificate) : base(Strings.TlsSenderCertificateNameMatchesServiceProviderCertificateId(certificate))
		{
			this.certificate = certificate;
		}

		// Token: 0x0600AF88 RID: 44936 RVA: 0x00294851 File Offset: 0x00292A51
		public TlsSenderCertificateNameMatchesServiceProviderCertificateException(string certificate, Exception innerException) : base(Strings.TlsSenderCertificateNameMatchesServiceProviderCertificateId(certificate), innerException)
		{
			this.certificate = certificate;
		}

		// Token: 0x0600AF89 RID: 44937 RVA: 0x00294867 File Offset: 0x00292A67
		protected TlsSenderCertificateNameMatchesServiceProviderCertificateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.certificate = (string)info.GetValue("certificate", typeof(string));
		}

		// Token: 0x0600AF8A RID: 44938 RVA: 0x00294891 File Offset: 0x00292A91
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("certificate", this.certificate);
		}

		// Token: 0x17003800 RID: 14336
		// (get) Token: 0x0600AF8B RID: 44939 RVA: 0x002948AC File Offset: 0x00292AAC
		public string Certificate
		{
			get
			{
				return this.certificate;
			}
		}

		// Token: 0x04006166 RID: 24934
		private readonly string certificate;
	}
}
