using System;
using System.Text;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x020000A1 RID: 161
	public struct TenantHint
	{
		// Token: 0x060007BE RID: 1982 RVA: 0x00015B5F File Offset: 0x00013D5F
		public TenantHint(byte[] tenantHintBlob)
		{
			this.tenantHintBlob = tenantHintBlob;
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060007BF RID: 1983 RVA: 0x00015B68 File Offset: 0x00013D68
		public static TenantHint Empty
		{
			get
			{
				return new TenantHint(TenantHint.emptyBlob);
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060007C0 RID: 1984 RVA: 0x00015B74 File Offset: 0x00013D74
		public static TenantHint RootOrg
		{
			get
			{
				return new TenantHint(TenantHint.rootOrgBlob);
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x00015B80 File Offset: 0x00013D80
		public static byte[] RootOrgBlob
		{
			get
			{
				return TenantHint.rootOrgBlob;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060007C2 RID: 1986 RVA: 0x00015B87 File Offset: 0x00013D87
		public bool IsEmpty
		{
			get
			{
				return this.tenantHintBlob == null || this.tenantHintBlob.Length == 0;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x00015B9E File Offset: 0x00013D9E
		public bool IsRootOrg
		{
			get
			{
				return this.IsEmpty || ValueHelper.ArraysEqual<byte>(this.tenantHintBlob, TenantHint.rootOrgBlob);
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x00015BBA File Offset: 0x00013DBA
		public byte[] TenantHintBlob
		{
			get
			{
				return this.tenantHintBlob;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060007C5 RID: 1989 RVA: 0x00015BC2 File Offset: 0x00013DC2
		public int TenantHintBlobSize
		{
			get
			{
				if (this.tenantHintBlob != null)
				{
					return this.tenantHintBlob.Length;
				}
				return 0;
			}
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00015BD8 File Offset: 0x00013DD8
		public override string ToString()
		{
			if (this.IsEmpty)
			{
				return "<Empty>";
			}
			StringBuilder stringBuilder = new StringBuilder(this.TenantHintBlobSize * 2);
			foreach (byte b in this.TenantHintBlob)
			{
				stringBuilder.Append(b.ToString("X2"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040006E8 RID: 1768
		internal const int MaxTenantHintBlobSize = 128;

		// Token: 0x040006E9 RID: 1769
		private static readonly byte[] rootOrgBlob = new byte[16];

		// Token: 0x040006EA RID: 1770
		private static readonly byte[] emptyBlob = new byte[0];

		// Token: 0x040006EB RID: 1771
		private byte[] tenantHintBlob;
	}
}
