using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000F2 RID: 242
	internal class PartnerCertificate : ConfigurablePropertyBag
	{
		// Token: 0x06000970 RID: 2416 RVA: 0x0001D661 File Offset: 0x0001B861
		public PartnerCertificate()
		{
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x0001D669 File Offset: 0x0001B869
		internal PartnerCertificate(string callerId, string certSubjectName)
		{
			if (string.IsNullOrEmpty(callerId))
			{
				throw new ArgumentNullException("callerId");
			}
			if (string.IsNullOrEmpty(certSubjectName))
			{
				throw new ArgumentNullException("certSubjectName");
			}
			this.PartnerCallerId = callerId;
			this.CertificateSubjectName = certSubjectName;
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x0001D6A5 File Offset: 0x0001B8A5
		internal PartnerCertificate(string callerId, string partnerName, string certSubjectName) : this(callerId, certSubjectName)
		{
			if (string.IsNullOrEmpty(partnerName))
			{
				throw new ArgumentNullException("partnerName");
			}
			this.PartnerName = partnerName;
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000973 RID: 2419 RVA: 0x0001D6C9 File Offset: 0x0001B8C9
		// (set) Token: 0x06000974 RID: 2420 RVA: 0x0001D6DB File Offset: 0x0001B8DB
		public string PartnerCallerId
		{
			get
			{
				return this[PartnerCertificate.PartnerCallerIdDef] as string;
			}
			internal set
			{
				this[PartnerCertificate.PartnerCallerIdDef] = value;
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x0001D6E9 File Offset: 0x0001B8E9
		// (set) Token: 0x06000976 RID: 2422 RVA: 0x0001D6FB File Offset: 0x0001B8FB
		public string PartnerName
		{
			get
			{
				return this[PartnerCertificate.PartnerNameDef] as string;
			}
			internal set
			{
				this[PartnerCertificate.PartnerNameDef] = value;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000977 RID: 2423 RVA: 0x0001D709 File Offset: 0x0001B909
		// (set) Token: 0x06000978 RID: 2424 RVA: 0x0001D71B File Offset: 0x0001B91B
		public string CertificateSubjectName
		{
			get
			{
				return this[PartnerCertificate.CertificateSubjectNameDef] as string;
			}
			internal set
			{
				this[PartnerCertificate.CertificateSubjectNameDef] = value;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x0001D729 File Offset: 0x0001B929
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId((string)this[PartnerCertificate.PartnerCallerIdDef] + (string)this[PartnerCertificate.CertificateSubjectNameDef]);
			}
		}

		// Token: 0x04000501 RID: 1281
		internal static readonly HygienePropertyDefinition PartnerCallerIdDef = new HygienePropertyDefinition("callerId", typeof(string));

		// Token: 0x04000502 RID: 1282
		internal static readonly HygienePropertyDefinition PartnerCertificateIdDef = new HygienePropertyDefinition("partnerCertificateId", typeof(Guid));

		// Token: 0x04000503 RID: 1283
		internal static readonly HygienePropertyDefinition PartnerIdDef = new HygienePropertyDefinition("partnerId", typeof(Guid));

		// Token: 0x04000504 RID: 1284
		internal static readonly HygienePropertyDefinition PartnerNameDef = new HygienePropertyDefinition("partnerName", typeof(string));

		// Token: 0x04000505 RID: 1285
		internal static readonly HygienePropertyDefinition CertificateIdDef = new HygienePropertyDefinition("certificateId", typeof(Guid));

		// Token: 0x04000506 RID: 1286
		internal static readonly HygienePropertyDefinition CertificateSubjectNameDef = new HygienePropertyDefinition("certificateSubjectName", typeof(string));
	}
}
