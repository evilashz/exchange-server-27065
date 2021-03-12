using System;
using System.Collections.Generic;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200089D RID: 2205
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class ConversationId : StoreId, IEquatable<ConversationId>, IComparable
	{
		// Token: 0x06005270 RID: 21104 RVA: 0x00158D44 File Offset: 0x00156F44
		private ConversationId(byte[] bytes)
		{
			Util.ThrowOnNullArgument(bytes, "bytes");
			if (bytes.Length != 16)
			{
				throw new CorruptDataException(ServerStrings.ExInvalidIdFormat);
			}
			this.bytes = bytes;
			this.hashCode = 0;
		}

		// Token: 0x06005271 RID: 21105 RVA: 0x00158D77 File Offset: 0x00156F77
		public static ConversationId Create(string base64String)
		{
			return new ConversationId(StoreId.Base64ToByteArray(base64String));
		}

		// Token: 0x06005272 RID: 21106 RVA: 0x00158D84 File Offset: 0x00156F84
		public static ConversationId Create(byte[] bytes)
		{
			return new ConversationId(ConversationId.CloneBytes(bytes));
		}

		// Token: 0x06005273 RID: 21107 RVA: 0x00158D91 File Offset: 0x00156F91
		public static ConversationId Create(Guid guid)
		{
			return new ConversationId(guid.ToByteArray());
		}

		// Token: 0x06005274 RID: 21108 RVA: 0x00158DA0 File Offset: 0x00156FA0
		public static ConversationId Create(ConversationIndex index)
		{
			return new ConversationId(index.Guid.ToByteArray());
		}

		// Token: 0x06005275 RID: 21109 RVA: 0x00158DC1 File Offset: 0x00156FC1
		public override string ToBase64String()
		{
			return Convert.ToBase64String(this.bytes);
		}

		// Token: 0x06005276 RID: 21110 RVA: 0x00158DCE File Offset: 0x00156FCE
		public override byte[] GetBytes()
		{
			return ConversationId.CloneBytes(this.bytes);
		}

		// Token: 0x06005277 RID: 21111 RVA: 0x00158DDC File Offset: 0x00156FDC
		public override bool Equals(object o)
		{
			ConversationId o2 = o as ConversationId;
			return this.Equals(o2);
		}

		// Token: 0x06005278 RID: 21112 RVA: 0x00158DF8 File Offset: 0x00156FF8
		public override bool Equals(StoreId o)
		{
			ConversationId o2 = o as ConversationId;
			return this.Equals(o2);
		}

		// Token: 0x06005279 RID: 21113 RVA: 0x00158E13 File Offset: 0x00157013
		public bool Equals(ConversationId o)
		{
			return o != null && ArrayComparer<byte>.Comparer.Equals(this.bytes, o.bytes);
		}

		// Token: 0x0600527A RID: 21114 RVA: 0x00158E30 File Offset: 0x00157030
		public override int GetHashCode()
		{
			this.hashCode = 0;
			for (int i = 0; i < this.bytes.Length; i++)
			{
				this.hashCode ^= (int)this.bytes[i] << 8 * (i % 4);
			}
			return this.hashCode;
		}

		// Token: 0x0600527B RID: 21115 RVA: 0x00158E7B File Offset: 0x0015707B
		public override string ToString()
		{
			return GlobalObjectId.ByteArrayToHexString(this.bytes);
		}

		// Token: 0x0600527C RID: 21116 RVA: 0x00158E88 File Offset: 0x00157088
		private static byte[] CloneBytes(IList<byte> bytes)
		{
			byte[] array = new byte[bytes.Count];
			bytes.CopyTo(array, 0);
			return array;
		}

		// Token: 0x0600527D RID: 21117 RVA: 0x00158EAC File Offset: 0x001570AC
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			ConversationId conversationId = obj as ConversationId;
			if (obj == null)
			{
				throw new ArgumentException();
			}
			return ArrayComparer<byte>.Comparer.Compare(this.bytes, conversationId.bytes);
		}

		// Token: 0x04002CDF RID: 11487
		private byte[] bytes;

		// Token: 0x04002CE0 RID: 11488
		private int hashCode;
	}
}
