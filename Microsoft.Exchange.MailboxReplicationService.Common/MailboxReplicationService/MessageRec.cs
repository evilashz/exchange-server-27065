using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000042 RID: 66
	[DataContract]
	internal sealed class MessageRec
	{
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000325 RID: 805 RVA: 0x00005AB2 File Offset: 0x00003CB2
		// (set) Token: 0x06000326 RID: 806 RVA: 0x00005ABA File Offset: 0x00003CBA
		[DataMember(IsRequired = true)]
		public byte[] EntryId
		{
			get
			{
				return this.entryId;
			}
			set
			{
				this.entryId = value;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000327 RID: 807 RVA: 0x00005AC3 File Offset: 0x00003CC3
		// (set) Token: 0x06000328 RID: 808 RVA: 0x00005ACB File Offset: 0x00003CCB
		[DataMember(EmitDefaultValue = false)]
		public byte[] FolderId
		{
			get
			{
				return this.folderId;
			}
			set
			{
				this.folderId = value;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000329 RID: 809 RVA: 0x00005AD4 File Offset: 0x00003CD4
		// (set) Token: 0x0600032A RID: 810 RVA: 0x00005ADE File Offset: 0x00003CDE
		[DataMember(IsRequired = true, Name = "Flags")]
		public MessageRecFlags LegacyFlags
		{
			get
			{
				return (MessageRecFlags)(this.flags & MsgRecFlags.AllLegacy);
			}
			set
			{
				this.flags |= (MsgRecFlags)value;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600032B RID: 811 RVA: 0x00005AEE File Offset: 0x00003CEE
		// (set) Token: 0x0600032C RID: 812 RVA: 0x00005AF6 File Offset: 0x00003CF6
		[DataMember(EmitDefaultValue = false)]
		public int FlagsInt
		{
			get
			{
				return (int)this.flags;
			}
			set
			{
				this.flags |= (MsgRecFlags)value;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600032D RID: 813 RVA: 0x00005B06 File Offset: 0x00003D06
		// (set) Token: 0x0600032E RID: 814 RVA: 0x00005B0E File Offset: 0x00003D0E
		[DataMember(EmitDefaultValue = false)]
		public DateTime CreationTimestamp
		{
			get
			{
				return this.creationTimestamp;
			}
			set
			{
				this.creationTimestamp = value;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600032F RID: 815 RVA: 0x00005B17 File Offset: 0x00003D17
		// (set) Token: 0x06000330 RID: 816 RVA: 0x00005B1F File Offset: 0x00003D1F
		[DataMember(EmitDefaultValue = false)]
		public int MessageSize
		{
			get
			{
				return this.messageSize;
			}
			set
			{
				this.messageSize = value;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000331 RID: 817 RVA: 0x00005B28 File Offset: 0x00003D28
		// (set) Token: 0x06000332 RID: 818 RVA: 0x00005B30 File Offset: 0x00003D30
		[DataMember(EmitDefaultValue = false)]
		public PropValueData[] AdditionalProps
		{
			get
			{
				return this.additionalProps;
			}
			set
			{
				this.additionalProps = value;
			}
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00005B39 File Offset: 0x00003D39
		public MessageRec()
		{
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00005B41 File Offset: 0x00003D41
		public MessageRec(byte[] entryId, byte[] folderId, DateTime creationTimestamp, int messageSize, MsgRecFlags flags, PropValueData[] additionalProps)
		{
			this.entryId = entryId;
			this.folderId = folderId;
			this.creationTimestamp = creationTimestamp;
			this.messageSize = messageSize;
			this.additionalProps = additionalProps;
			this.flags = flags;
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000335 RID: 821 RVA: 0x00005B76 File Offset: 0x00003D76
		// (set) Token: 0x06000336 RID: 822 RVA: 0x00005B7E File Offset: 0x00003D7E
		public MsgRecFlags Flags
		{
			get
			{
				return this.flags;
			}
			set
			{
				this.flags = value;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000337 RID: 823 RVA: 0x00005B87 File Offset: 0x00003D87
		public bool IsDeleted
		{
			get
			{
				return this.flags.HasFlag(MsgRecFlags.Deleted);
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000338 RID: 824 RVA: 0x00005B9F File Offset: 0x00003D9F
		public bool IsNew
		{
			get
			{
				return this.flags.HasFlag(MsgRecFlags.New);
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000339 RID: 825 RVA: 0x00005BB7 File Offset: 0x00003DB7
		public bool IsFAI
		{
			get
			{
				return this.flags.HasFlag(MsgRecFlags.Associated);
			}
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00005BD0 File Offset: 0x00003DD0
		public int CompareTo(MessageRecSortBy sortBy, DateTime creationTimestamp, byte[] folderId, byte[] entryId)
		{
			int num = this.CreationTimestamp.CompareTo(creationTimestamp);
			if (num == 0)
			{
				num = ArrayComparer<byte>.Comparer.Compare(this.FolderId, folderId);
				if (num == 0)
				{
					num = ArrayComparer<byte>.Comparer.Compare(this.EntryId, entryId);
				}
			}
			if (sortBy == MessageRecSortBy.DescendingTimeStamp)
			{
				num = -num;
			}
			return num;
		}

		// Token: 0x17000131 RID: 305
		public object this[PropTag ptag]
		{
			get
			{
				if (this.additionalProps != null)
				{
					foreach (PropValueData propValueData in this.additionalProps)
					{
						if (propValueData.PropTag == (int)ptag)
						{
							return propValueData.Value;
						}
					}
				}
				return null;
			}
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00005C64 File Offset: 0x00003E64
		[Conditional("DEBUG")]
		private void Validate()
		{
			if (this.flags.HasFlag(MsgRecFlags.New))
			{
				this.flags.HasFlag(MsgRecFlags.Deleted);
			}
			if (this.flags.HasFlag(MsgRecFlags.Regular))
			{
				this.flags.HasFlag(MsgRecFlags.Associated);
			}
		}

		// Token: 0x04000275 RID: 629
		private byte[] entryId;

		// Token: 0x04000276 RID: 630
		private byte[] folderId;

		// Token: 0x04000277 RID: 631
		private DateTime creationTimestamp;

		// Token: 0x04000278 RID: 632
		private int messageSize;

		// Token: 0x04000279 RID: 633
		private MsgRecFlags flags;

		// Token: 0x0400027A RID: 634
		private PropValueData[] additionalProps;
	}
}
