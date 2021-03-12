using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace Microsoft.Exchange.Security.Cryptography.X509Certificates
{
	// Token: 0x02000AF8 RID: 2808
	internal class RevocationInformation
	{
		// Token: 0x06003C51 RID: 15441 RVA: 0x0009C9F4 File Offset: 0x0009ABF4
		private RevocationInformation(RevocationInformation.CertRevocationInfo item)
		{
			this.status = item.Status;
			this.oid = item.RevocationOid;
			this.expires = item.Expires;
			this.crlInfo = item.CrlInfo;
		}

		// Token: 0x17000EF3 RID: 3827
		// (get) Token: 0x06003C52 RID: 15442 RVA: 0x0009CA30 File Offset: 0x0009AC30
		public RevocationStatus Status
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x17000EF4 RID: 3828
		// (get) Token: 0x06003C53 RID: 15443 RVA: 0x0009CA38 File Offset: 0x0009AC38
		public Oid Oid
		{
			get
			{
				if (!string.IsNullOrEmpty(this.oid))
				{
					return new Oid(this.oid);
				}
				return null;
			}
		}

		// Token: 0x17000EF5 RID: 3829
		// (get) Token: 0x06003C54 RID: 15444 RVA: 0x0009CA54 File Offset: 0x0009AC54
		public DateTime Expires
		{
			get
			{
				return this.expires;
			}
		}

		// Token: 0x17000EF6 RID: 3830
		// (get) Token: 0x06003C55 RID: 15445 RVA: 0x0009CA5C File Offset: 0x0009AC5C
		public RevocationCrlInformation CRLInformation
		{
			get
			{
				return this.crlInfo;
			}
		}

		// Token: 0x06003C56 RID: 15446 RVA: 0x0009CA64 File Offset: 0x0009AC64
		internal static RevocationInformation Create(IntPtr bytes)
		{
			RevocationInformation.CertRevocationInfo item = (RevocationInformation.CertRevocationInfo)Marshal.PtrToStructure(bytes, typeof(RevocationInformation.CertRevocationInfo));
			return new RevocationInformation(item);
		}

		// Token: 0x04003522 RID: 13602
		private RevocationStatus status;

		// Token: 0x04003523 RID: 13603
		private string oid;

		// Token: 0x04003524 RID: 13604
		private DateTime expires;

		// Token: 0x04003525 RID: 13605
		private RevocationCrlInformation crlInfo;

		// Token: 0x02000AF9 RID: 2809
		private struct CertRevocationInfo
		{
			// Token: 0x17000EF7 RID: 3831
			// (get) Token: 0x06003C57 RID: 15447 RVA: 0x0009CA8D File Offset: 0x0009AC8D
			public RevocationStatus Status
			{
				get
				{
					return this.status;
				}
			}

			// Token: 0x17000EF8 RID: 3832
			// (get) Token: 0x06003C58 RID: 15448 RVA: 0x0009CA95 File Offset: 0x0009AC95
			public string RevocationOid
			{
				get
				{
					return this.revocationOid;
				}
			}

			// Token: 0x17000EF9 RID: 3833
			// (get) Token: 0x06003C59 RID: 15449 RVA: 0x0009CA9D File Offset: 0x0009AC9D
			public DateTime Expires
			{
				get
				{
					if (!this.hasFreshnessTime)
					{
						return DateTime.UtcNow;
					}
					return DateTime.UtcNow + TimeSpan.FromSeconds((double)this.freshnessTime);
				}
			}

			// Token: 0x17000EFA RID: 3834
			// (get) Token: 0x06003C5A RID: 15450 RVA: 0x0009CAC3 File Offset: 0x0009ACC3
			public RevocationCrlInformation CrlInfo
			{
				get
				{
					if (!(this.crlInfo != IntPtr.Zero))
					{
						return null;
					}
					return RevocationCrlInformation.Create(this.crlInfo);
				}
			}

			// Token: 0x04003526 RID: 13606
			private uint size;

			// Token: 0x04003527 RID: 13607
			private RevocationStatus status;

			// Token: 0x04003528 RID: 13608
			[MarshalAs(UnmanagedType.LPStr)]
			private string revocationOid;

			// Token: 0x04003529 RID: 13609
			private IntPtr oidSpecificInfo;

			// Token: 0x0400352A RID: 13610
			[MarshalAs(UnmanagedType.Bool)]
			private bool hasFreshnessTime;

			// Token: 0x0400352B RID: 13611
			private int freshnessTime;

			// Token: 0x0400352C RID: 13612
			private IntPtr crlInfo;
		}
	}
}
