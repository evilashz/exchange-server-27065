using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002C7 RID: 711
	[Serializable]
	[StructLayout(LayoutKind.Auto)]
	public struct JET_SIGNATURE : IEquatable<JET_SIGNATURE>
	{
		// Token: 0x06000CEE RID: 3310 RVA: 0x00019F3C File Offset: 0x0001813C
		internal JET_SIGNATURE(int random, DateTime? time, string computerName)
		{
			this.ulRandom = (uint)random;
			this.logtimeCreate = ((time != null) ? new JET_LOGTIME(time.Value) : default(JET_LOGTIME));
			this.szComputerName = computerName;
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x00019F7D File Offset: 0x0001817D
		internal JET_SIGNATURE(NATIVE_SIGNATURE native)
		{
			this.ulRandom = native.ulRandom;
			this.logtimeCreate = native.logtimeCreate;
			this.szComputerName = native.szComputerName;
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x00019FA6 File Offset: 0x000181A6
		public static bool operator ==(JET_SIGNATURE lhs, JET_SIGNATURE rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x00019FB0 File Offset: 0x000181B0
		public static bool operator !=(JET_SIGNATURE lhs, JET_SIGNATURE rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x00019FBC File Offset: 0x000181BC
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_SIGNATURE({0}:{1}:{2})", new object[]
			{
				this.ulRandom,
				this.logtimeCreate.ToDateTime(),
				this.szComputerName
			});
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x0001A00D File Offset: 0x0001820D
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((JET_SIGNATURE)obj);
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x0001A040 File Offset: 0x00018240
		public override int GetHashCode()
		{
			return this.ulRandom.GetHashCode() ^ this.logtimeCreate.GetHashCode() ^ ((this.szComputerName == null) ? -1 : this.szComputerName.GetHashCode());
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x0001A088 File Offset: 0x00018288
		public bool Equals(JET_SIGNATURE other)
		{
			bool flag = (string.IsNullOrEmpty(this.szComputerName) && string.IsNullOrEmpty(other.szComputerName)) || (!string.IsNullOrEmpty(this.szComputerName) && !string.IsNullOrEmpty(other.szComputerName) && this.szComputerName == other.szComputerName);
			return flag && this.ulRandom == other.ulRandom && this.logtimeCreate == other.logtimeCreate;
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x0001A10C File Offset: 0x0001830C
		internal NATIVE_SIGNATURE GetNativeSignature()
		{
			return new NATIVE_SIGNATURE
			{
				ulRandom = this.ulRandom,
				szComputerName = this.szComputerName,
				logtimeCreate = this.logtimeCreate
			};
		}

		// Token: 0x04000850 RID: 2128
		internal readonly uint ulRandom;

		// Token: 0x04000851 RID: 2129
		internal readonly JET_LOGTIME logtimeCreate;

		// Token: 0x04000852 RID: 2130
		private readonly string szComputerName;
	}
}
