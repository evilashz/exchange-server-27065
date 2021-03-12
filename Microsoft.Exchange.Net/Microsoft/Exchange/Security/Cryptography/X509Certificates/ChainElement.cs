using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Exchange.Security.Cryptography.X509Certificates
{
	// Token: 0x02000AF1 RID: 2801
	internal class ChainElement
	{
		// Token: 0x06003C38 RID: 15416 RVA: 0x0009C62D File Offset: 0x0009A82D
		private ChainElement(ChainElement.CertChainElement item)
		{
			this.trust = item.TrustStatus;
			this.extendedErrorInfo = item.ExtendedErrorInfo;
			this.certificate = item.Certificate;
			this.revocationInfo = item.RevocationInformation;
		}

		// Token: 0x17000EE4 RID: 3812
		// (get) Token: 0x06003C39 RID: 15417 RVA: 0x0009C669 File Offset: 0x0009A869
		public X509Certificate2 Certificate
		{
			get
			{
				return this.certificate;
			}
		}

		// Token: 0x17000EE5 RID: 3813
		// (get) Token: 0x06003C3A RID: 15418 RVA: 0x0009C671 File Offset: 0x0009A871
		public TrustStatus Status
		{
			get
			{
				return this.trust.error;
			}
		}

		// Token: 0x17000EE6 RID: 3814
		// (get) Token: 0x06003C3B RID: 15419 RVA: 0x0009C67E File Offset: 0x0009A87E
		public string ExtendedError
		{
			get
			{
				return this.extendedErrorInfo;
			}
		}

		// Token: 0x17000EE7 RID: 3815
		// (get) Token: 0x06003C3C RID: 15420 RVA: 0x0009C686 File Offset: 0x0009A886
		public TrustInformation TrustInformation
		{
			get
			{
				return this.trust.information;
			}
		}

		// Token: 0x17000EE8 RID: 3816
		// (get) Token: 0x06003C3D RID: 15421 RVA: 0x0009C693 File Offset: 0x0009A893
		public RevocationInformation RevocationInfo
		{
			get
			{
				return this.revocationInfo;
			}
		}

		// Token: 0x06003C3E RID: 15422 RVA: 0x0009C69B File Offset: 0x0009A89B
		internal static ChainElement Create(IntPtr bytes)
		{
			return new ChainElement((ChainElement.CertChainElement)Marshal.PtrToStructure(bytes, typeof(ChainElement.CertChainElement)));
		}

		// Token: 0x06003C3F RID: 15423 RVA: 0x0009C6B8 File Offset: 0x0009A8B8
		internal static bool IsSelfSigned(IntPtr bytes)
		{
			return (((ChainElement.CertChainElement)Marshal.PtrToStructure(bytes, typeof(ChainElement.CertChainElement))).TrustStatus.information & TrustInformation.IsSelfSigned) != TrustInformation.None;
		}

		// Token: 0x04003506 RID: 13574
		private CapiNativeMethods.CertTrustStatus trust;

		// Token: 0x04003507 RID: 13575
		private X509Certificate2 certificate;

		// Token: 0x04003508 RID: 13576
		private string extendedErrorInfo;

		// Token: 0x04003509 RID: 13577
		private RevocationInformation revocationInfo;

		// Token: 0x02000AF2 RID: 2802
		private struct CertChainElement
		{
			// Token: 0x17000EE9 RID: 3817
			// (get) Token: 0x06003C40 RID: 15424 RVA: 0x0009C6EF File Offset: 0x0009A8EF
			public X509Certificate2 Certificate
			{
				get
				{
					return new X509Certificate2(this.certContext);
				}
			}

			// Token: 0x17000EEA RID: 3818
			// (get) Token: 0x06003C41 RID: 15425 RVA: 0x0009C6FC File Offset: 0x0009A8FC
			public RevocationInformation RevocationInformation
			{
				get
				{
					if (!(this.revocationInfo != IntPtr.Zero))
					{
						return null;
					}
					return RevocationInformation.Create(this.revocationInfo);
				}
			}

			// Token: 0x17000EEB RID: 3819
			// (get) Token: 0x06003C42 RID: 15426 RVA: 0x0009C71D File Offset: 0x0009A91D
			public string ExtendedErrorInfo
			{
				get
				{
					return this.extendedErrorInfo;
				}
			}

			// Token: 0x17000EEC RID: 3820
			// (get) Token: 0x06003C43 RID: 15427 RVA: 0x0009C725 File Offset: 0x0009A925
			public CapiNativeMethods.CertTrustStatus TrustStatus
			{
				get
				{
					return this.trustStatus;
				}
			}

			// Token: 0x0400350A RID: 13578
			private uint size;

			// Token: 0x0400350B RID: 13579
			private IntPtr certContext;

			// Token: 0x0400350C RID: 13580
			private CapiNativeMethods.CertTrustStatus trustStatus;

			// Token: 0x0400350D RID: 13581
			private IntPtr revocationInfo;

			// Token: 0x0400350E RID: 13582
			private IntPtr issuanceUsage;

			// Token: 0x0400350F RID: 13583
			private IntPtr applicationUsage;

			// Token: 0x04003510 RID: 13584
			[MarshalAs(UnmanagedType.LPWStr)]
			private string extendedErrorInfo;
		}
	}
}
