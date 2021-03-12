using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Security.Cryptography.X509Certificates
{
	// Token: 0x02000AFA RID: 2810
	internal class CertificateChain
	{
		// Token: 0x06003C5B RID: 15451 RVA: 0x0009CAE4 File Offset: 0x0009ACE4
		private CertificateChain(CertificateChain.CertSimpleChain item)
		{
			this.trust = item.Status;
			this.expires = item.Expires;
			this.elements = item.Elements;
		}

		// Token: 0x17000EFB RID: 3835
		// (get) Token: 0x06003C5C RID: 15452 RVA: 0x0009CB13 File Offset: 0x0009AD13
		public TrustStatus Status
		{
			get
			{
				return this.trust.error;
			}
		}

		// Token: 0x17000EFC RID: 3836
		// (get) Token: 0x06003C5D RID: 15453 RVA: 0x0009CB20 File Offset: 0x0009AD20
		public TrustInformation TrustInformation
		{
			get
			{
				return this.trust.information;
			}
		}

		// Token: 0x17000EFD RID: 3837
		// (get) Token: 0x06003C5E RID: 15454 RVA: 0x0009CB2D File Offset: 0x0009AD2D
		public IList<ChainElement> Elements
		{
			get
			{
				return this.elements;
			}
		}

		// Token: 0x17000EFE RID: 3838
		// (get) Token: 0x06003C5F RID: 15455 RVA: 0x0009CB35 File Offset: 0x0009AD35
		public DateTime Expires
		{
			get
			{
				return this.expires;
			}
		}

		// Token: 0x06003C60 RID: 15456 RVA: 0x0009CB40 File Offset: 0x0009AD40
		internal static bool IsSelfSigned(IntPtr bytes)
		{
			CertificateChain.CertSimpleChain certSimpleChain = (CertificateChain.CertSimpleChain)Marshal.PtrToStructure(bytes, typeof(CertificateChain.CertSimpleChain));
			if (certSimpleChain.ElementCount != 1)
			{
				return false;
			}
			IntPtr bytes2 = Marshal.ReadIntPtr(certSimpleChain.RawElements);
			return ChainElement.IsSelfSigned(bytes2);
		}

		// Token: 0x06003C61 RID: 15457 RVA: 0x0009CB82 File Offset: 0x0009AD82
		internal static CertificateChain Create(IntPtr bytes)
		{
			return new CertificateChain((CertificateChain.CertSimpleChain)Marshal.PtrToStructure(bytes, typeof(CertificateChain.CertSimpleChain)));
		}

		// Token: 0x0400352D RID: 13613
		private CapiNativeMethods.CertTrustStatus trust;

		// Token: 0x0400352E RID: 13614
		private IList<ChainElement> elements;

		// Token: 0x0400352F RID: 13615
		private DateTime expires;

		// Token: 0x02000AFB RID: 2811
		private struct CertSimpleChain
		{
			// Token: 0x17000EFF RID: 3839
			// (get) Token: 0x06003C62 RID: 15458 RVA: 0x0009CB9E File Offset: 0x0009AD9E
			public CapiNativeMethods.CertTrustStatus Status
			{
				get
				{
					return this.trustStatus;
				}
			}

			// Token: 0x17000F00 RID: 3840
			// (get) Token: 0x06003C63 RID: 15459 RVA: 0x0009CBA6 File Offset: 0x0009ADA6
			public DateTime Expires
			{
				get
				{
					if (!this.hasRevocationFreshnessTime)
					{
						return DateTime.UtcNow;
					}
					return DateTime.UtcNow + TimeSpan.FromSeconds(this.revocationFreshnessTime);
				}
			}

			// Token: 0x17000F01 RID: 3841
			// (get) Token: 0x06003C64 RID: 15460 RVA: 0x0009CBCD File Offset: 0x0009ADCD
			public int ElementCount
			{
				get
				{
					return this.elements;
				}
			}

			// Token: 0x17000F02 RID: 3842
			// (get) Token: 0x06003C65 RID: 15461 RVA: 0x0009CBD5 File Offset: 0x0009ADD5
			public IntPtr RawElements
			{
				get
				{
					return this.elementData;
				}
			}

			// Token: 0x17000F03 RID: 3843
			// (get) Token: 0x06003C66 RID: 15462 RVA: 0x0009CBE0 File Offset: 0x0009ADE0
			public IList<ChainElement> Elements
			{
				get
				{
					if (this.elements <= 0)
					{
						return null;
					}
					List<ChainElement> list = new List<ChainElement>(this.elements);
					for (int i = 0; i < this.elements; i++)
					{
						IntPtr bytes = Marshal.ReadIntPtr(this.elementData, i * Marshal.SizeOf(typeof(IntPtr)));
						list.Add(ChainElement.Create(bytes));
					}
					return list.AsReadOnly();
				}
			}

			// Token: 0x17000F04 RID: 3844
			// (get) Token: 0x06003C67 RID: 15463 RVA: 0x0009CC44 File Offset: 0x0009AE44
			public IntPtr TrustList
			{
				get
				{
					return this.trustListInfo;
				}
			}

			// Token: 0x04003530 RID: 13616
			private uint size;

			// Token: 0x04003531 RID: 13617
			private CapiNativeMethods.CertTrustStatus trustStatus;

			// Token: 0x04003532 RID: 13618
			private int elements;

			// Token: 0x04003533 RID: 13619
			private IntPtr elementData;

			// Token: 0x04003534 RID: 13620
			private IntPtr trustListInfo;

			// Token: 0x04003535 RID: 13621
			[MarshalAs(UnmanagedType.Bool)]
			private bool hasRevocationFreshnessTime;

			// Token: 0x04003536 RID: 13622
			private uint revocationFreshnessTime;
		}
	}
}
