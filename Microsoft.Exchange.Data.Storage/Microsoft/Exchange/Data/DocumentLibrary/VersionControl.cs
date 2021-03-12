using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006F3 RID: 1779
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class VersionControl
	{
		// Token: 0x06004689 RID: 18057 RVA: 0x0012C86C File Offset: 0x0012AA6C
		internal VersionControl(bool isCheckedOut, string checkedOutTo, int versionId)
		{
			this.isCheckedOut = isCheckedOut;
			this.checkedOutTo = checkedOutTo;
			this.versionId = versionId;
		}

		// Token: 0x0600468A RID: 18058 RVA: 0x0012C88C File Offset: 0x0012AA8C
		public override bool Equals(object obj)
		{
			VersionControl versionControl = obj as VersionControl;
			return versionControl != null && (this.CheckedOutTo == versionControl.CheckedOutTo && this.IsCheckedOut == versionControl.IsCheckedOut) && this.TipVersion == versionControl.TipVersion;
		}

		// Token: 0x0600468B RID: 18059 RVA: 0x0012C8D8 File Offset: 0x0012AAD8
		public override int GetHashCode()
		{
			return this.CheckedOutTo.GetHashCode() + this.TipVersion.GetHashCode();
		}

		// Token: 0x17001485 RID: 5253
		// (get) Token: 0x0600468C RID: 18060 RVA: 0x0012C8FF File Offset: 0x0012AAFF
		public bool IsCheckedOut
		{
			get
			{
				return this.isCheckedOut;
			}
		}

		// Token: 0x17001486 RID: 5254
		// (get) Token: 0x0600468D RID: 18061 RVA: 0x0012C907 File Offset: 0x0012AB07
		public string CheckedOutTo
		{
			get
			{
				return this.checkedOutTo;
			}
		}

		// Token: 0x17001487 RID: 5255
		// (get) Token: 0x0600468E RID: 18062 RVA: 0x0012C90F File Offset: 0x0012AB0F
		public int TipVersion
		{
			get
			{
				return this.versionId;
			}
		}

		// Token: 0x0400269D RID: 9885
		private readonly bool isCheckedOut;

		// Token: 0x0400269E RID: 9886
		private readonly string checkedOutTo;

		// Token: 0x0400269F RID: 9887
		private readonly int versionId;
	}
}
