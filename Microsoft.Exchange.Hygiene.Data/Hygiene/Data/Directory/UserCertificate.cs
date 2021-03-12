using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x02000114 RID: 276
	internal class UserCertificate : ConfigurablePropertyBag
	{
		// Token: 0x06000AB5 RID: 2741 RVA: 0x0002123C File Offset: 0x0001F43C
		public UserCertificate(Guid userId, string certSubjectName, string certIssuerName)
		{
			if (userId.Equals(Guid.Empty))
			{
				throw new ArgumentException("userId");
			}
			if (string.IsNullOrWhiteSpace(certSubjectName))
			{
				throw new ArgumentNullException("certSubjectName");
			}
			if (certSubjectName.Length >= 1024)
			{
				throw new ArgumentException("Certificate Subject Name cannot be greater than or equal to 1024 characters");
			}
			if (string.IsNullOrWhiteSpace(certIssuerName))
			{
				throw new ArgumentNullException("certIssuerName");
			}
			if (certIssuerName.Length >= 1024)
			{
				throw new ArgumentException("Certificate Issuer Name cannot be greater than or equal to 1024 characters");
			}
			this.UserId = userId;
			this.CertificateSubjectName = certSubjectName;
			this.CertificateIssuerName = certIssuerName;
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x000212D3 File Offset: 0x0001F4D3
		// (set) Token: 0x06000AB7 RID: 2743 RVA: 0x000212E5 File Offset: 0x0001F4E5
		public Guid UserId
		{
			get
			{
				return (Guid)this[UserCertificate.UserIdProp];
			}
			private set
			{
				this[UserCertificate.UserIdProp] = value;
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x000212F8 File Offset: 0x0001F4F8
		// (set) Token: 0x06000AB9 RID: 2745 RVA: 0x0002130A File Offset: 0x0001F50A
		public string CertificateSubjectName
		{
			get
			{
				return this[UserCertificate.CertificateSubjectNameProp] as string;
			}
			private set
			{
				this[UserCertificate.CertificateSubjectNameProp] = value;
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000ABA RID: 2746 RVA: 0x00021318 File Offset: 0x0001F518
		// (set) Token: 0x06000ABB RID: 2747 RVA: 0x0002132A File Offset: 0x0001F52A
		public string CertificateIssuerName
		{
			get
			{
				return this[UserCertificate.CertificateIssuerNameProp] as string;
			}
			private set
			{
				this[UserCertificate.CertificateIssuerNameProp] = value;
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x00021338 File Offset: 0x0001F538
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId((Guid)this[UserCertificate.UserIdProp] + (string)this[UserCertificate.CertificateIssuerNameProp]);
			}
		}

		// Token: 0x04000574 RID: 1396
		internal static readonly HygienePropertyDefinition UserIdProp = ADMiniUserSchema.UserIdProp;

		// Token: 0x04000575 RID: 1397
		internal static readonly HygienePropertyDefinition UserCertificateIdProp = new HygienePropertyDefinition("userCertificateId", typeof(Guid));

		// Token: 0x04000576 RID: 1398
		internal static readonly HygienePropertyDefinition CertificateIdProp = new HygienePropertyDefinition("CertificateId", typeof(Guid));

		// Token: 0x04000577 RID: 1399
		internal static readonly HygienePropertyDefinition CertificateSubjectNameProp = new HygienePropertyDefinition("certificateSubjectName", typeof(string));

		// Token: 0x04000578 RID: 1400
		internal static readonly HygienePropertyDefinition CertificateIssuerNameProp = new HygienePropertyDefinition("certificateIssuerName", typeof(string));
	}
}
