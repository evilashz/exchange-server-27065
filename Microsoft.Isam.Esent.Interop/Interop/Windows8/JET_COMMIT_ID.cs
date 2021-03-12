using System;
using System.Globalization;

namespace Microsoft.Isam.Esent.Interop.Windows8
{
	// Token: 0x02000278 RID: 632
	public class JET_COMMIT_ID : IComparable<JET_COMMIT_ID>, IEquatable<JET_COMMIT_ID>
	{
		// Token: 0x06000B2C RID: 2860 RVA: 0x00016BF5 File Offset: 0x00014DF5
		internal JET_COMMIT_ID(NATIVE_COMMIT_ID native)
		{
			this.signLog = new JET_SIGNATURE(native.signLog);
			this.commitId = native.commitId;
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x00016C1C File Offset: 0x00014E1C
		public static bool operator <(JET_COMMIT_ID lhs, JET_COMMIT_ID rhs)
		{
			return lhs.CompareTo(rhs) < 0;
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x00016C28 File Offset: 0x00014E28
		public static bool operator >(JET_COMMIT_ID lhs, JET_COMMIT_ID rhs)
		{
			return lhs.CompareTo(rhs) > 0;
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x00016C34 File Offset: 0x00014E34
		public static bool operator <=(JET_COMMIT_ID lhs, JET_COMMIT_ID rhs)
		{
			return lhs.CompareTo(rhs) <= 0;
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x00016C43 File Offset: 0x00014E43
		public static bool operator >=(JET_COMMIT_ID lhs, JET_COMMIT_ID rhs)
		{
			return lhs.CompareTo(rhs) >= 0;
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x00016C52 File Offset: 0x00014E52
		public static bool operator ==(JET_COMMIT_ID lhs, JET_COMMIT_ID rhs)
		{
			return lhs.CompareTo(rhs) == 0;
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x00016C5E File Offset: 0x00014E5E
		public static bool operator !=(JET_COMMIT_ID lhs, JET_COMMIT_ID rhs)
		{
			return lhs.CompareTo(rhs) != 0;
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x00016C70 File Offset: 0x00014E70
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_COMMIT_ID({0}:{1}", new object[]
			{
				this.signLog,
				this.commitId
			});
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x00016CB0 File Offset: 0x00014EB0
		public int CompareTo(JET_COMMIT_ID other)
		{
			if (other == null)
			{
				if (this.commitId <= 0L)
				{
					return 0;
				}
				return 1;
			}
			else
			{
				if (this.signLog != other.signLog)
				{
					throw new ArgumentException("The commit-ids belong to different log-streams");
				}
				return this.commitId.CompareTo(other.commitId);
			}
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x00016D00 File Offset: 0x00014F00
		public bool Equals(JET_COMMIT_ID other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x00016D0C File Offset: 0x00014F0C
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.CompareTo((JET_COMMIT_ID)obj) == 0;
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x00016D38 File Offset: 0x00014F38
		public override int GetHashCode()
		{
			return this.commitId.GetHashCode() ^ this.signLog.GetHashCode();
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x00016D68 File Offset: 0x00014F68
		internal NATIVE_COMMIT_ID GetNativeCommitId()
		{
			return new NATIVE_COMMIT_ID
			{
				signLog = this.signLog.GetNativeSignature(),
				commitId = this.commitId
			};
		}

		// Token: 0x040004E0 RID: 1248
		private readonly JET_SIGNATURE signLog;

		// Token: 0x040004E1 RID: 1249
		private readonly long commitId;
	}
}
