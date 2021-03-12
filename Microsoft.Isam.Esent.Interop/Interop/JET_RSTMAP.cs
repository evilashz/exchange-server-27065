using System;
using System.Diagnostics;
using System.Globalization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002C1 RID: 705
	[Serializable]
	public class JET_RSTMAP : IContentEquatable<JET_RSTMAP>, IDeepCloneable<JET_RSTMAP>
	{
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000CC5 RID: 3269 RVA: 0x00019A0E File Offset: 0x00017C0E
		// (set) Token: 0x06000CC6 RID: 3270 RVA: 0x00019A16 File Offset: 0x00017C16
		public string szDatabaseName
		{
			[DebuggerStepThrough]
			get
			{
				return this.databaseName;
			}
			set
			{
				this.databaseName = value;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000CC7 RID: 3271 RVA: 0x00019A1F File Offset: 0x00017C1F
		// (set) Token: 0x06000CC8 RID: 3272 RVA: 0x00019A27 File Offset: 0x00017C27
		public string szNewDatabaseName
		{
			[DebuggerStepThrough]
			get
			{
				return this.newDatabaseName;
			}
			set
			{
				this.newDatabaseName = value;
			}
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x00019A30 File Offset: 0x00017C30
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_RSTINFO(szDatabaseName={0},szNewDatabaseName={1})", new object[]
			{
				this.szDatabaseName,
				this.szNewDatabaseName
			});
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x00019A66 File Offset: 0x00017C66
		public bool ContentEquals(JET_RSTMAP other)
		{
			return other != null && string.Equals(this.szDatabaseName, other.szDatabaseName, StringComparison.OrdinalIgnoreCase) && string.Equals(this.szNewDatabaseName, other.szNewDatabaseName, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x00019A95 File Offset: 0x00017C95
		public JET_RSTMAP DeepClone()
		{
			return (JET_RSTMAP)base.MemberwiseClone();
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x00019AA4 File Offset: 0x00017CA4
		internal NATIVE_RSTMAP GetNativeRstmap()
		{
			return new NATIVE_RSTMAP
			{
				szDatabaseName = LibraryHelpers.MarshalStringToHGlobalUni(this.szDatabaseName),
				szNewDatabaseName = LibraryHelpers.MarshalStringToHGlobalUni(this.szNewDatabaseName)
			};
		}

		// Token: 0x04000835 RID: 2101
		private string databaseName;

		// Token: 0x04000836 RID: 2102
		private string newDatabaseName;
	}
}
