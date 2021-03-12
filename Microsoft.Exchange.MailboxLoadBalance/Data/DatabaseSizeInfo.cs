using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance.Data
{
	// Token: 0x02000044 RID: 68
	[ClassAccessLevel(AccessLevel.Implementation)]
	[DataContract]
	internal class DatabaseSizeInfo
	{
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000274 RID: 628 RVA: 0x000081A0 File Offset: 0x000063A0
		// (set) Token: 0x06000275 RID: 629 RVA: 0x000081AD File Offset: 0x000063AD
		[IgnoreDataMember]
		public ByteQuantifiedSize AvailableWhitespace
		{
			get
			{
				return ByteQuantifiedSize.FromBytes(this.avaialbleWhitespaceBytes);
			}
			set
			{
				this.avaialbleWhitespaceBytes = value.ToBytes();
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000276 RID: 630 RVA: 0x000081BC File Offset: 0x000063BC
		// (set) Token: 0x06000277 RID: 631 RVA: 0x000081C9 File Offset: 0x000063C9
		[IgnoreDataMember]
		public ByteQuantifiedSize CurrentPhysicalSize
		{
			get
			{
				return ByteQuantifiedSize.FromBytes(this.currentSizeBytes);
			}
			set
			{
				this.currentSizeBytes = value.ToBytes();
			}
		}

		// Token: 0x040000B2 RID: 178
		[DataMember]
		private ulong avaialbleWhitespaceBytes;

		// Token: 0x040000B3 RID: 179
		[DataMember]
		private ulong currentSizeBytes;
	}
}
