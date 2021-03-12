using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Exchange.Security.Cryptography.X509Certificates
{
	// Token: 0x02000AEF RID: 2799
	internal class CertificateRevocationList
	{
		// Token: 0x06003C30 RID: 15408 RVA: 0x0009C567 File Offset: 0x0009A767
		private CertificateRevocationList(CertificateRevocationList.CrlContext item)
		{
			this.certEncodingType = item.CertEncodingType;
			this.rawData = item.CrlRawData;
			this.store = item.Store;
		}

		// Token: 0x17000EDE RID: 3806
		// (get) Token: 0x06003C31 RID: 15409 RVA: 0x0009C596 File Offset: 0x0009A796
		public int CertEncodingType
		{
			get
			{
				return this.certEncodingType;
			}
		}

		// Token: 0x17000EDF RID: 3807
		// (get) Token: 0x06003C32 RID: 15410 RVA: 0x0009C59E File Offset: 0x0009A79E
		public byte[] CrlRawData
		{
			get
			{
				return this.rawData;
			}
		}

		// Token: 0x17000EE0 RID: 3808
		// (get) Token: 0x06003C33 RID: 15411 RVA: 0x0009C5A6 File Offset: 0x0009A7A6
		public X509Store Store
		{
			get
			{
				return this.store;
			}
		}

		// Token: 0x06003C34 RID: 15412 RVA: 0x0009C5AE File Offset: 0x0009A7AE
		internal static CertificateRevocationList Create(IntPtr bytes)
		{
			return new CertificateRevocationList((CertificateRevocationList.CrlContext)Marshal.PtrToStructure(bytes, typeof(CertificateRevocationList.CrlContext)));
		}

		// Token: 0x040034FE RID: 13566
		private int certEncodingType;

		// Token: 0x040034FF RID: 13567
		private byte[] rawData;

		// Token: 0x04003500 RID: 13568
		private X509Store store;

		// Token: 0x02000AF0 RID: 2800
		private struct CrlContext
		{
			// Token: 0x17000EE1 RID: 3809
			// (get) Token: 0x06003C35 RID: 15413 RVA: 0x0009C5CA File Offset: 0x0009A7CA
			public int CertEncodingType
			{
				get
				{
					return this.certEncodingType;
				}
			}

			// Token: 0x17000EE2 RID: 3810
			// (get) Token: 0x06003C36 RID: 15414 RVA: 0x0009C5D4 File Offset: 0x0009A7D4
			public byte[] CrlRawData
			{
				get
				{
					if (this.crlEncodedSize <= 0)
					{
						return null;
					}
					byte[] array = new byte[this.crlEncodedSize];
					Marshal.Copy(this.crlEncoded, array, 0, this.crlEncodedSize);
					return array;
				}
			}

			// Token: 0x17000EE3 RID: 3811
			// (get) Token: 0x06003C37 RID: 15415 RVA: 0x0009C60C File Offset: 0x0009A80C
			public X509Store Store
			{
				get
				{
					if (!(this.certStore != IntPtr.Zero))
					{
						return null;
					}
					return new X509Store(this.certStore);
				}
			}

			// Token: 0x04003501 RID: 13569
			private int certEncodingType;

			// Token: 0x04003502 RID: 13570
			private IntPtr crlEncoded;

			// Token: 0x04003503 RID: 13571
			private int crlEncodedSize;

			// Token: 0x04003504 RID: 13572
			private IntPtr crlInfo;

			// Token: 0x04003505 RID: 13573
			private IntPtr certStore;
		}
	}
}
