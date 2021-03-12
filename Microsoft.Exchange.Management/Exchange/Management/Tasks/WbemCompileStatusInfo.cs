using System;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020000F8 RID: 248
	internal struct WbemCompileStatusInfo
	{
		// Token: 0x06000755 RID: 1877 RVA: 0x0001F702 File Offset: 0x0001D902
		internal void InitializeStatusInfo()
		{
			this.phaseError = 0;
			this.hresult = 0;
			this.objectNum = 0;
			this.firstLine = 0;
			this.lastLine = 0;
			this.outFlags = 0;
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000756 RID: 1878 RVA: 0x0001F72E File Offset: 0x0001D92E
		internal int PhaseError
		{
			get
			{
				return this.phaseError;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000757 RID: 1879 RVA: 0x0001F736 File Offset: 0x0001D936
		internal int HResult
		{
			get
			{
				return this.hresult;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x0001F73E File Offset: 0x0001D93E
		internal int FirstLine
		{
			get
			{
				return this.firstLine;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x0001F746 File Offset: 0x0001D946
		internal int LastLine
		{
			get
			{
				return this.lastLine;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x0600075A RID: 1882 RVA: 0x0001F74E File Offset: 0x0001D94E
		internal int OutFlags
		{
			get
			{
				return this.outFlags;
			}
		}

		// Token: 0x04000376 RID: 886
		private int phaseError;

		// Token: 0x04000377 RID: 887
		private int hresult;

		// Token: 0x04000378 RID: 888
		private int objectNum;

		// Token: 0x04000379 RID: 889
		private int firstLine;

		// Token: 0x0400037A RID: 890
		private int lastLine;

		// Token: 0x0400037B RID: 891
		private int outFlags;
	}
}
