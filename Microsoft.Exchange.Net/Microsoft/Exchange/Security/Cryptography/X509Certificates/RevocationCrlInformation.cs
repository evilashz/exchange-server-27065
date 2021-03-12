using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Security.Cryptography.X509Certificates
{
	// Token: 0x02000AF5 RID: 2805
	internal class RevocationCrlInformation
	{
		// Token: 0x06003C49 RID: 15433 RVA: 0x0009C947 File Offset: 0x0009AB47
		private RevocationCrlInformation(RevocationCrlInformation.CertRevocationCrlInfo item)
		{
			this.deltaEntry = item.IsDeltaCRLEntry;
			this.baseCRLContext = item.BaseCRLList;
			this.deltaCRLContext = item.DeltaCRLList;
		}

		// Token: 0x17000EED RID: 3821
		// (get) Token: 0x06003C4A RID: 15434 RVA: 0x0009C976 File Offset: 0x0009AB76
		public bool IsDelta
		{
			get
			{
				return this.deltaEntry;
			}
		}

		// Token: 0x17000EEE RID: 3822
		// (get) Token: 0x06003C4B RID: 15435 RVA: 0x0009C97E File Offset: 0x0009AB7E
		public CertificateRevocationList BaseCRL
		{
			get
			{
				return this.baseCRLContext;
			}
		}

		// Token: 0x17000EEF RID: 3823
		// (get) Token: 0x06003C4C RID: 15436 RVA: 0x0009C986 File Offset: 0x0009AB86
		public CertificateRevocationList DeltaCRL
		{
			get
			{
				return this.deltaCRLContext;
			}
		}

		// Token: 0x06003C4D RID: 15437 RVA: 0x0009C98E File Offset: 0x0009AB8E
		internal static RevocationCrlInformation Create(IntPtr bytes)
		{
			return new RevocationCrlInformation((RevocationCrlInformation.CertRevocationCrlInfo)Marshal.PtrToStructure(bytes, typeof(RevocationCrlInformation.CertRevocationCrlInfo)));
		}

		// Token: 0x04003516 RID: 13590
		private bool deltaEntry;

		// Token: 0x04003517 RID: 13591
		private CertificateRevocationList baseCRLContext;

		// Token: 0x04003518 RID: 13592
		private CertificateRevocationList deltaCRLContext;

		// Token: 0x02000AF6 RID: 2806
		private struct CertRevocationCrlInfo
		{
			// Token: 0x17000EF0 RID: 3824
			// (get) Token: 0x06003C4E RID: 15438 RVA: 0x0009C9AA File Offset: 0x0009ABAA
			public bool IsDeltaCRLEntry
			{
				get
				{
					return this.deltaCrlEntry;
				}
			}

			// Token: 0x17000EF1 RID: 3825
			// (get) Token: 0x06003C4F RID: 15439 RVA: 0x0009C9B2 File Offset: 0x0009ABB2
			public CertificateRevocationList BaseCRLList
			{
				get
				{
					if (!(this.baseCRLContext != IntPtr.Zero))
					{
						return null;
					}
					return CertificateRevocationList.Create(this.baseCRLContext);
				}
			}

			// Token: 0x17000EF2 RID: 3826
			// (get) Token: 0x06003C50 RID: 15440 RVA: 0x0009C9D3 File Offset: 0x0009ABD3
			public CertificateRevocationList DeltaCRLList
			{
				get
				{
					if (!(this.deltaCRLContext != IntPtr.Zero))
					{
						return null;
					}
					return CertificateRevocationList.Create(this.deltaCRLContext);
				}
			}

			// Token: 0x04003519 RID: 13593
			private uint size;

			// Token: 0x0400351A RID: 13594
			private IntPtr baseCRLContext;

			// Token: 0x0400351B RID: 13595
			private IntPtr deltaCRLContext;

			// Token: 0x0400351C RID: 13596
			private IntPtr crlEntry;

			// Token: 0x0400351D RID: 13597
			[MarshalAs(UnmanagedType.Bool)]
			private bool deltaCrlEntry;
		}
	}
}
