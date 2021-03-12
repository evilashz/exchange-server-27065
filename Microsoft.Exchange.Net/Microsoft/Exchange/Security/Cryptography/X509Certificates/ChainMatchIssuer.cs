using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Security.Cryptography.X509Certificates
{
	// Token: 0x02000A87 RID: 2695
	internal class ChainMatchIssuer
	{
		// Token: 0x06003A3B RID: 14907 RVA: 0x00094BF1 File Offset: 0x00092DF1
		private ChainMatchIssuer()
		{
		}

		// Token: 0x06003A3C RID: 14908 RVA: 0x00094BF9 File Offset: 0x00092DF9
		protected ChainMatchIssuer(ChainMatchIssuer.Operator type, Oid[] oids)
		{
			this.type = type;
			this.usages = oids;
		}

		// Token: 0x17000E7D RID: 3709
		// (get) Token: 0x06003A3D RID: 14909 RVA: 0x00094C0F File Offset: 0x00092E0F
		// (set) Token: 0x06003A3E RID: 14910 RVA: 0x00094C17 File Offset: 0x00092E17
		public Oid[] Usages
		{
			get
			{
				return this.usages;
			}
			set
			{
				this.usages = value;
			}
		}

		// Token: 0x06003A3F RID: 14911 RVA: 0x00094C20 File Offset: 0x00092E20
		internal CapiNativeMethods.CertUsageMatch GetCertUsageMatch(ref SafeHGlobalHandle bytes)
		{
			CapiNativeMethods.CryptoApiBlob usage = new CapiNativeMethods.CryptoApiBlob(0U, bytes);
			if (this.Usages != null && this.Usages.Length > 0)
			{
				bytes = this.GetBytes();
				usage = new CapiNativeMethods.CryptoApiBlob((uint)this.Usages.Length, bytes);
			}
			return new CapiNativeMethods.CertUsageMatch((CapiNativeMethods.CertUsageMatch.Operator)this.type, usage);
		}

		// Token: 0x06003A40 RID: 14912 RVA: 0x00094C70 File Offset: 0x00092E70
		private SafeHGlobalHandle GetBytes()
		{
			if (this.usages == null || this.usages.Length == 0)
			{
				return SafeHGlobalHandle.InvalidHandle;
			}
			int num = Marshal.SizeOf(typeof(IntPtr));
			int num2 = num * this.usages.Length;
			foreach (Oid oid in this.usages)
			{
				num2 += Encoding.ASCII.GetByteCount(oid.Value) + 1;
			}
			SafeHGlobalHandle safeHGlobalHandle = NativeMethods.AllocHGlobal(num2);
			IntPtr intPtr = safeHGlobalHandle.DangerousGetHandle();
			IntPtr intPtr2 = (IntPtr)((long)intPtr + (long)(num * this.usages.Length));
			foreach (Oid oid2 in this.usages)
			{
				byte[] bytes = Encoding.ASCII.GetBytes(oid2.Value);
				Marshal.WriteIntPtr(intPtr, intPtr2);
				Marshal.Copy(bytes, 0, intPtr2, bytes.Length);
				Marshal.WriteByte(intPtr2, bytes.Length, 0);
				intPtr = (IntPtr)((long)intPtr + (long)num);
				intPtr2 = (IntPtr)((long)intPtr2 + (long)bytes.Length + 1L);
			}
			return safeHGlobalHandle;
		}

		// Token: 0x04003271 RID: 12913
		private ChainMatchIssuer.Operator type;

		// Token: 0x04003272 RID: 12914
		private Oid[] usages;

		// Token: 0x02000A88 RID: 2696
		protected enum Operator : uint
		{
			// Token: 0x04003274 RID: 12916
			And,
			// Token: 0x04003275 RID: 12917
			Or
		}
	}
}
