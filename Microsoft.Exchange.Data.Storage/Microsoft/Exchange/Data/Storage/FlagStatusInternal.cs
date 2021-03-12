using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000077 RID: 119
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class FlagStatusInternal
	{
		// Token: 0x0600086A RID: 2154 RVA: 0x0003FE62 File Offset: 0x0003E062
		internal FlagStatusInternal()
		{
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600086B RID: 2155 RVA: 0x0003FE6A File Offset: 0x0003E06A
		// (set) Token: 0x0600086C RID: 2156 RVA: 0x0003FE72 File Offset: 0x0003E072
		internal StoreObjectId ExistingItemObjectId
		{
			get
			{
				return this.existingItemObjectId;
			}
			set
			{
				this.existingItemObjectId = value;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x0600086D RID: 2157 RVA: 0x0003FE7B File Offset: 0x0003E07B
		// (set) Token: 0x0600086E RID: 2158 RVA: 0x0003FE83 File Offset: 0x0003E083
		internal StoreObjectId ParentId
		{
			get
			{
				return this.parentId;
			}
			set
			{
				this.parentId = value;
			}
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0003FE8C File Offset: 0x0003E08C
		internal bool IsDirty()
		{
			return this.markMsgFlagsToSet != 0 || this.markMsgStatusToSet != 0;
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0003FEA4 File Offset: 0x0003E0A4
		private static int GetSetReadFlagsBitMask()
		{
			return 513;
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0003FEAC File Offset: 0x0003E0AC
		internal int GetSetReadFlag()
		{
			int num = this.markMsgFlagsToSet & FlagStatusInternal.GetSetReadFlagsBitMask();
			if (num == 0)
			{
				return -1;
			}
			SetReadFlags setReadFlags = SetReadFlags.None;
			if ((num & 1) != 0)
			{
				if ((this.msgFlags & 1) != 0)
				{
					setReadFlags = setReadFlags;
				}
				else
				{
					setReadFlags |= SetReadFlags.ClearRead;
				}
			}
			if ((num & 512) != 0 && (this.msgFlags & 512) != 0)
			{
				setReadFlags |= SetReadFlags.SuppressReceipt;
			}
			return (int)setReadFlags;
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x0003FF00 File Offset: 0x0003E100
		internal bool? TryGetValue(PropertyDefinition propertyDefinition, int flag)
		{
			if (propertyDefinition.Equals(InternalSchema.Flags))
			{
				if ((this.markMsgFlagsForRead & flag) == 0)
				{
					return null;
				}
				return new bool?((this.msgFlags & flag) != 0);
			}
			else
			{
				if (propertyDefinition != InternalSchema.MessageStatus)
				{
					throw new InvalidOperationException();
				}
				if ((this.markMsgStatusForRead & flag) == 0)
				{
					return null;
				}
				return new bool?((this.msgStatus & flag) != 0);
			}
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x0003FF77 File Offset: 0x0003E177
		internal bool GetDirtyStatusBits(out int bitsSet, out int bitsClear)
		{
			bitsSet = 0;
			bitsClear = 0;
			if (this.markMsgStatusToSet == 0)
			{
				return false;
			}
			bitsSet = (this.markMsgStatusToSet & this.msgStatus);
			bitsClear = (this.markMsgStatusToSet & ~this.msgStatus);
			return true;
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x0003FFAC File Offset: 0x0003E1AC
		internal bool GetNonReadFlagsBits(out int bitsSet, out int bitsClear)
		{
			bitsSet = 0;
			bitsClear = 0;
			int num = this.markMsgFlagsToSet & ~FlagStatusInternal.GetSetReadFlagsBitMask();
			if (num == 0)
			{
				return false;
			}
			bitsSet = (num & this.msgFlags);
			bitsClear = (num & ~this.msgFlags);
			return true;
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x0003FFE8 File Offset: 0x0003E1E8
		internal void ClearFlagsForSet(PropertyDefinition propertyDefinition)
		{
			MessageFlagsProperty messageFlagsProperty = propertyDefinition as MessageFlagsProperty;
			if (messageFlagsProperty.NativeProperty == InternalSchema.Flags)
			{
				this.markMsgFlagsToSet &= ~messageFlagsProperty.Flag;
				return;
			}
			if (messageFlagsProperty.NativeProperty == InternalSchema.MessageStatus)
			{
				this.markMsgStatusToSet &= ~messageFlagsProperty.Flag;
			}
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x00040040 File Offset: 0x0003E240
		internal void SetFlagsPropertyOnItem(PropertyDefinition propertyDefinition, int flag, bool value)
		{
			if (propertyDefinition != InternalSchema.Flags)
			{
				if (propertyDefinition == InternalSchema.MessageStatus)
				{
					this.markMsgStatusToSet |= flag;
					this.markMsgStatusForRead |= flag;
					if (value)
					{
						this.msgStatus |= flag;
						return;
					}
					this.msgStatus &= ~flag;
				}
				return;
			}
			this.markMsgFlagsToSet |= flag;
			this.markMsgFlagsForRead |= flag;
			if (value)
			{
				this.msgFlags |= flag;
				return;
			}
			this.msgFlags &= ~flag;
		}

		// Token: 0x04000231 RID: 561
		private StoreObjectId existingItemObjectId;

		// Token: 0x04000232 RID: 562
		private StoreObjectId parentId;

		// Token: 0x04000233 RID: 563
		private int markMsgFlagsToSet;

		// Token: 0x04000234 RID: 564
		private int markMsgFlagsForRead;

		// Token: 0x04000235 RID: 565
		private int msgFlags;

		// Token: 0x04000236 RID: 566
		private int markMsgStatusToSet;

		// Token: 0x04000237 RID: 567
		private int markMsgStatusForRead;

		// Token: 0x04000238 RID: 568
		private int msgStatus;
	}
}
