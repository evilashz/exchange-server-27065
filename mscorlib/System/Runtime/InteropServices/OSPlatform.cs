using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200097D RID: 2429
	public struct OSPlatform : IEquatable<OSPlatform>
	{
		// Token: 0x17001101 RID: 4353
		// (get) Token: 0x060061DE RID: 25054 RVA: 0x0014CC64 File Offset: 0x0014AE64
		public static OSPlatform Linux { get; } = new OSPlatform("LINUX");

		// Token: 0x17001102 RID: 4354
		// (get) Token: 0x060061DF RID: 25055 RVA: 0x0014CC6B File Offset: 0x0014AE6B
		public static OSPlatform OSX { get; } = new OSPlatform("OSX");

		// Token: 0x17001103 RID: 4355
		// (get) Token: 0x060061E0 RID: 25056 RVA: 0x0014CC72 File Offset: 0x0014AE72
		public static OSPlatform Windows { get; } = new OSPlatform("WINDOWS");

		// Token: 0x060061E1 RID: 25057 RVA: 0x0014CC79 File Offset: 0x0014AE79
		private OSPlatform(string osPlatform)
		{
			if (osPlatform == null)
			{
				throw new ArgumentNullException("osPlatform");
			}
			if (osPlatform.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyValue"), "osPlatform");
			}
			this._osPlatform = osPlatform;
		}

		// Token: 0x060061E2 RID: 25058 RVA: 0x0014CCAD File Offset: 0x0014AEAD
		public static OSPlatform Create(string osPlatform)
		{
			return new OSPlatform(osPlatform);
		}

		// Token: 0x060061E3 RID: 25059 RVA: 0x0014CCB5 File Offset: 0x0014AEB5
		public bool Equals(OSPlatform other)
		{
			return this.Equals(other._osPlatform);
		}

		// Token: 0x060061E4 RID: 25060 RVA: 0x0014CCC3 File Offset: 0x0014AEC3
		internal bool Equals(string other)
		{
			return string.Equals(this._osPlatform, other, StringComparison.Ordinal);
		}

		// Token: 0x060061E5 RID: 25061 RVA: 0x0014CCD2 File Offset: 0x0014AED2
		public override bool Equals(object obj)
		{
			return obj is OSPlatform && this.Equals((OSPlatform)obj);
		}

		// Token: 0x060061E6 RID: 25062 RVA: 0x0014CCEA File Offset: 0x0014AEEA
		public override int GetHashCode()
		{
			if (this._osPlatform != null)
			{
				return this._osPlatform.GetHashCode();
			}
			return 0;
		}

		// Token: 0x060061E7 RID: 25063 RVA: 0x0014CD01 File Offset: 0x0014AF01
		public override string ToString()
		{
			return this._osPlatform ?? string.Empty;
		}

		// Token: 0x060061E8 RID: 25064 RVA: 0x0014CD12 File Offset: 0x0014AF12
		public static bool operator ==(OSPlatform left, OSPlatform right)
		{
			return left.Equals(right);
		}

		// Token: 0x060061E9 RID: 25065 RVA: 0x0014CD1C File Offset: 0x0014AF1C
		public static bool operator !=(OSPlatform left, OSPlatform right)
		{
			return !(left == right);
		}

		// Token: 0x04002BEF RID: 11247
		private readonly string _osPlatform;
	}
}
