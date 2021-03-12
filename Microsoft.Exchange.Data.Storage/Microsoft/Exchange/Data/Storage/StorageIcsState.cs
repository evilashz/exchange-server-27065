using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000842 RID: 2114
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct StorageIcsState
	{
		// Token: 0x06004E70 RID: 20080 RVA: 0x00148884 File Offset: 0x00146A84
		public StorageIcsState(byte[] stateIdsetGiven, byte[] stateCnsetSeen, byte[] stateCnsetSeenFAI, byte[] stateCnsetRead)
		{
			this.stateIdsetGiven = stateIdsetGiven;
			this.stateCnsetSeen = stateCnsetSeen;
			this.stateCnsetSeenFAI = stateCnsetSeenFAI;
			this.stateCnsetRead = stateCnsetRead;
		}

		// Token: 0x17001637 RID: 5687
		// (get) Token: 0x06004E71 RID: 20081 RVA: 0x001488A3 File Offset: 0x00146AA3
		// (set) Token: 0x06004E72 RID: 20082 RVA: 0x001488B9 File Offset: 0x00146AB9
		public byte[] StateIdsetGiven
		{
			get
			{
				if (this.stateIdsetGiven == null)
				{
					return StorageIcsState.stateEmpty;
				}
				return this.stateIdsetGiven;
			}
			set
			{
				this.stateIdsetGiven = value;
			}
		}

		// Token: 0x17001638 RID: 5688
		// (get) Token: 0x06004E73 RID: 20083 RVA: 0x001488C2 File Offset: 0x00146AC2
		// (set) Token: 0x06004E74 RID: 20084 RVA: 0x001488D8 File Offset: 0x00146AD8
		public byte[] StateCnsetSeen
		{
			get
			{
				if (this.stateCnsetSeen == null)
				{
					return StorageIcsState.stateEmpty;
				}
				return this.stateCnsetSeen;
			}
			set
			{
				this.stateCnsetSeen = value;
			}
		}

		// Token: 0x17001639 RID: 5689
		// (get) Token: 0x06004E75 RID: 20085 RVA: 0x001488E1 File Offset: 0x00146AE1
		// (set) Token: 0x06004E76 RID: 20086 RVA: 0x001488F7 File Offset: 0x00146AF7
		public byte[] StateCnsetSeenFAI
		{
			get
			{
				if (this.stateCnsetSeenFAI == null)
				{
					return StorageIcsState.stateEmpty;
				}
				return this.stateCnsetSeenFAI;
			}
			set
			{
				this.stateCnsetSeenFAI = value;
			}
		}

		// Token: 0x1700163A RID: 5690
		// (get) Token: 0x06004E77 RID: 20087 RVA: 0x00148900 File Offset: 0x00146B00
		// (set) Token: 0x06004E78 RID: 20088 RVA: 0x00148916 File Offset: 0x00146B16
		public byte[] StateCnsetRead
		{
			get
			{
				if (this.stateCnsetRead == null)
				{
					return StorageIcsState.stateEmpty;
				}
				return this.stateCnsetRead;
			}
			set
			{
				this.stateCnsetRead = value;
			}
		}

		// Token: 0x04002ACC RID: 10956
		private static readonly byte[] stateEmpty = Array<byte>.Empty;

		// Token: 0x04002ACD RID: 10957
		private byte[] stateIdsetGiven;

		// Token: 0x04002ACE RID: 10958
		private byte[] stateCnsetSeen;

		// Token: 0x04002ACF RID: 10959
		private byte[] stateCnsetSeenFAI;

		// Token: 0x04002AD0 RID: 10960
		private byte[] stateCnsetRead;
	}
}
