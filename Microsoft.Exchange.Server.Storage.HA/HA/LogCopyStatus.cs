using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Server.Storage.HA
{
	// Token: 0x0200001D RID: 29
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class LogCopyStatus
	{
		// Token: 0x060000DD RID: 221 RVA: 0x00006669 File Offset: 0x00004869
		internal LogCopyStatus(CopyType copyType, string nodeName, bool isCrossSite, ulong logGeneration, ulong inspectedLogGeneration, ulong replayedLogGeneration)
		{
			this.timeReceivedUTC = DateTime.UtcNow;
			this.copyType = copyType;
			this.nodeName = nodeName;
			this.isCrossSite = isCrossSite;
			this.logGeneration = logGeneration;
			this.inspectedLogGeneration = inspectedLogGeneration;
			this.replayedLogGeneration = replayedLogGeneration;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000DE RID: 222 RVA: 0x000066A9 File Offset: 0x000048A9
		internal DateTime TimeReceivedUTC
		{
			get
			{
				return this.timeReceivedUTC;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000DF RID: 223 RVA: 0x000066B1 File Offset: 0x000048B1
		internal CopyType CopyType
		{
			get
			{
				return this.copyType;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x000066B9 File Offset: 0x000048B9
		internal string NodeName
		{
			get
			{
				return this.nodeName;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x000066C1 File Offset: 0x000048C1
		internal bool IsCrossSite
		{
			get
			{
				return this.isCrossSite;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x000066C9 File Offset: 0x000048C9
		internal ulong LogGeneration
		{
			get
			{
				return this.logGeneration;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x000066D1 File Offset: 0x000048D1
		internal ulong InspectedLogGeneration
		{
			get
			{
				return this.inspectedLogGeneration;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x000066D9 File Offset: 0x000048D9
		internal ulong ReplayedLogGeneration
		{
			get
			{
				return this.replayedLogGeneration;
			}
		}

		// Token: 0x04000084 RID: 132
		private readonly DateTime timeReceivedUTC;

		// Token: 0x04000085 RID: 133
		private readonly string nodeName;

		// Token: 0x04000086 RID: 134
		private readonly bool isCrossSite;

		// Token: 0x04000087 RID: 135
		private readonly CopyType copyType;

		// Token: 0x04000088 RID: 136
		private readonly ulong logGeneration;

		// Token: 0x04000089 RID: 137
		private readonly ulong inspectedLogGeneration;

		// Token: 0x0400008A RID: 138
		private readonly ulong replayedLogGeneration;
	}
}
