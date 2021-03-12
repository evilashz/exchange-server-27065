using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002DE RID: 734
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CertificateLoadErrorException : MailboxReplicationPermanentException
	{
		// Token: 0x06002416 RID: 9238 RVA: 0x0004F806 File Offset: 0x0004DA06
		public CertificateLoadErrorException(string certificateName, string errorMessage) : base(MrsStrings.CertificateLoadError(certificateName, errorMessage))
		{
			this.certificateName = certificateName;
			this.errorMessage = errorMessage;
		}

		// Token: 0x06002417 RID: 9239 RVA: 0x0004F823 File Offset: 0x0004DA23
		public CertificateLoadErrorException(string certificateName, string errorMessage, Exception innerException) : base(MrsStrings.CertificateLoadError(certificateName, errorMessage), innerException)
		{
			this.certificateName = certificateName;
			this.errorMessage = errorMessage;
		}

		// Token: 0x06002418 RID: 9240 RVA: 0x0004F844 File Offset: 0x0004DA44
		protected CertificateLoadErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.certificateName = (string)info.GetValue("certificateName", typeof(string));
			this.errorMessage = (string)info.GetValue("errorMessage", typeof(string));
		}

		// Token: 0x06002419 RID: 9241 RVA: 0x0004F899 File Offset: 0x0004DA99
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("certificateName", this.certificateName);
			info.AddValue("errorMessage", this.errorMessage);
		}

		// Token: 0x17000D34 RID: 3380
		// (get) Token: 0x0600241A RID: 9242 RVA: 0x0004F8C5 File Offset: 0x0004DAC5
		public string CertificateName
		{
			get
			{
				return this.certificateName;
			}
		}

		// Token: 0x17000D35 RID: 3381
		// (get) Token: 0x0600241B RID: 9243 RVA: 0x0004F8CD File Offset: 0x0004DACD
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x04000FE7 RID: 4071
		private readonly string certificateName;

		// Token: 0x04000FE8 RID: 4072
		private readonly string errorMessage;
	}
}
