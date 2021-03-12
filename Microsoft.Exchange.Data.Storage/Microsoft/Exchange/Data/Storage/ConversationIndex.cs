using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200089E RID: 2206
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct ConversationIndex : IEquatable<ConversationIndex>
	{
		// Token: 0x0600527E RID: 21118 RVA: 0x00158EE4 File Offset: 0x001570E4
		private ConversationIndex(byte[] bytes)
		{
			Util.ThrowOnNullArgument(bytes, "bytes");
			this.bytes = bytes;
			this.readonlyBytes = null;
			this.isParsed = false;
			this.guid = Guid.Empty;
			this.components = null;
			this.readonlyComponents = null;
			this.hasHash = false;
			this.hashCode = 0;
		}

		// Token: 0x0600527F RID: 21119 RVA: 0x00158F38 File Offset: 0x00157138
		public static ConversationIndex CreateNew()
		{
			return ConversationIndex.Create(Guid.NewGuid());
		}

		// Token: 0x06005280 RID: 21120 RVA: 0x00158F44 File Offset: 0x00157144
		public static ConversationIndex Create(IList<byte> bytes)
		{
			Util.ThrowOnNullArgument(bytes, "bytes");
			if (!ConversationIndex.IsValidConversationIndex(bytes))
			{
				throw new ArgumentException("bytes doesn't correspond to a valid conversation index");
			}
			return new ConversationIndex(ConversationIndex.CloneBytes(bytes));
		}

		// Token: 0x06005281 RID: 21121 RVA: 0x00158F6F File Offset: 0x0015716F
		public static ConversationId RetrieveConversationId(IList<byte> conversationIndexInBytes)
		{
			return ConversationId.Create(ConversationIndex.Create(conversationIndexInBytes));
		}

		// Token: 0x06005282 RID: 21122 RVA: 0x00158F7C File Offset: 0x0015717C
		public static bool TryCreate(IList<byte> bytes, out ConversationIndex conversationIndex)
		{
			conversationIndex = ConversationIndex.Empty;
			if (bytes == null || !ConversationIndex.IsValidConversationIndex(bytes))
			{
				return false;
			}
			conversationIndex = new ConversationIndex(ConversationIndex.CloneBytes(bytes));
			return true;
		}

		// Token: 0x06005283 RID: 21123 RVA: 0x00158FA8 File Offset: 0x001571A8
		public static ConversationIndex Create(ConversationId conversationId)
		{
			Util.ThrowOnNullArgument(conversationId, "conversationId");
			return ConversationIndex.Create(new Guid(conversationId.GetBytes()));
		}

		// Token: 0x06005284 RID: 21124 RVA: 0x00158FC5 File Offset: 0x001571C5
		public static ConversationIndex Create(Guid guid)
		{
			return ConversationIndex.Create(guid, ExDateTime.UtcNow);
		}

		// Token: 0x06005285 RID: 21125 RVA: 0x00158FD4 File Offset: 0x001571D4
		public static ConversationIndex Create(Guid guid, ExDateTime header)
		{
			byte[] array = new byte[22];
			array[0] = 1;
			long num = ((DateTime)header.ToUtc()).ToFileTime();
			int num2 = 56;
			for (int i = 1; i < 6; i++)
			{
				array[i] = (byte)(num >> num2 & 255L);
				num2 -= 8;
			}
			byte[] array2 = guid.ToByteArray();
			for (int j = 0; j < array2.Length; j++)
			{
				array[6 + j] = array2[j];
			}
			return new ConversationIndex(array);
		}

		// Token: 0x06005286 RID: 21126 RVA: 0x00159054 File Offset: 0x00157254
		public static ConversationIndex CreateFromParent(Guid conversationIdGuid, ConversationIndex parentIndex)
		{
			return ConversationIndex.CreateFromParent(parentIndex.bytes, ExDateTime.UtcNow).UpdateGuid(conversationIdGuid);
		}

		// Token: 0x06005287 RID: 21127 RVA: 0x0015907B File Offset: 0x0015727B
		public static ConversationIndex CreateFromParent(IList<byte> parentBytes)
		{
			return ConversationIndex.CreateFromParent(parentBytes, ExDateTime.UtcNow);
		}

		// Token: 0x06005288 RID: 21128 RVA: 0x00159088 File Offset: 0x00157288
		public static ConversationIndex CreateFromParent(IList<byte> parentBytes, ExDateTime messageTime)
		{
			if (parentBytes == null || !ConversationIndex.IsValidConversationIndex(parentBytes))
			{
				return ConversationIndex.CreateNew();
			}
			int count = parentBytes.Count;
			byte[] array = new byte[count + 5];
			parentBytes.CopyTo(array, 0);
			ulong num = (ulong)(messageTime.ToFileTime() & -65536L);
			ulong lastFileTime = ConversationIndex.GetLastFileTime(parentBytes);
			if (num > lastFileTime)
			{
				num -= lastFileTime;
			}
			else
			{
				num = lastFileTime - num;
			}
			uint num2;
			if ((num >> 32 & 16646144UL) == 0UL)
			{
				num2 = (uint)(num >> 32 & 131071UL);
				num2 <<= 14;
				num2 |= (uint)((num & (ulong)-262144) >> 18);
				array[count] = (byte)((num2 & 4278190080U) >> 24);
			}
			else
			{
				num2 = (uint)(num >> 32 & 4194303UL);
				num2 <<= 9;
				num2 |= (uint)((num & (ulong)-8388608) >> 23);
				array[count] = (byte)((num2 & 2130706432U) >> 24 | 128U);
			}
			array[count + 1] = (byte)((num2 & 16711680U) >> 16);
			array[count + 2] = (byte)((num2 & 65280U) >> 8);
			array[count + 3] = (byte)(num2 & 255U);
			array[count + 4] = (byte)(messageTime.UtcTicks & 255L);
			return new ConversationIndex(array);
		}

		// Token: 0x06005289 RID: 21129 RVA: 0x001591AF File Offset: 0x001573AF
		public static bool operator ==(ConversationIndex index1, ConversationIndex index2)
		{
			return index1.Equals(index2);
		}

		// Token: 0x0600528A RID: 21130 RVA: 0x001591B9 File Offset: 0x001573B9
		public static bool operator !=(ConversationIndex index1, ConversationIndex index2)
		{
			return !index1.Equals(index2);
		}

		// Token: 0x0600528B RID: 21131 RVA: 0x001591C6 File Offset: 0x001573C6
		public static bool CheckStageValue(ConversationIndex.FixupStage fixupStage, ConversationIndex.FixupStage expectedStage)
		{
			if (expectedStage == ConversationIndex.FixupStage.Unknown)
			{
				return fixupStage == ConversationIndex.FixupStage.Unknown;
			}
			return (fixupStage & expectedStage) == expectedStage;
		}

		// Token: 0x0600528C RID: 21132 RVA: 0x001591D8 File Offset: 0x001573D8
		public static bool WasMessageEverProcessed(ICorePropertyBag propertyBag)
		{
			return propertyBag.GetValueAsNullable<bool>(ItemSchema.ConversationIndexTracking) != null;
		}

		// Token: 0x0600528D RID: 21133 RVA: 0x001591F8 File Offset: 0x001573F8
		private static byte[] CloneBytes(IList<byte> bytes)
		{
			byte[] array = new byte[bytes.Count];
			bytes.CopyTo(array, 0);
			return array;
		}

		// Token: 0x0600528E RID: 21134 RVA: 0x0015921C File Offset: 0x0015741C
		private static byte[] ExtractBytes(byte[] bytes, int start, int count)
		{
			Util.ThrowOnNullArgument(bytes, "bytes");
			if (start < 0)
			{
				throw new ArgumentException("start must be > 0");
			}
			if (count <= 0)
			{
				throw new ArgumentException("count must be >= 0");
			}
			byte[] array = new byte[count];
			Array.Copy(bytes, start, array, 0, count);
			return array;
		}

		// Token: 0x0600528F RID: 21135 RVA: 0x00159264 File Offset: 0x00157464
		private static bool IsValidConversationIndex(IList<byte> conversationIndex)
		{
			return conversationIndex.Count >= 22 && (conversationIndex.Count - 22) % 5 == 0;
		}

		// Token: 0x06005290 RID: 21136 RVA: 0x00159280 File Offset: 0x00157480
		private static ulong GetLastFileTime(IList<byte> parentBytes)
		{
			ulong num = 0UL;
			for (int i = 1; i < 6; i++)
			{
				num |= (ulong)parentBytes[i];
				num <<= 8;
			}
			num <<= 8;
			for (int j = 22; j < parentBytes.Count; j += 5)
			{
				ulong num2;
				if ((parentBytes[j] & 128) == 128)
				{
					num2 = (ulong)((int)(parentBytes[j] & 127) << 15 | (int)parentBytes[j + 1] << 7 | parentBytes[j + 2] >> 1);
					num2 <<= 32;
					num2 |= (ulong)((int)parentBytes[j + 2] << 31 | (int)parentBytes[j + 3] << 23);
				}
				else
				{
					num2 = (ulong)((int)(parentBytes[j] & 127) << 10 | (int)parentBytes[j + 1] << 2 | parentBytes[j + 2] >> 6);
					num2 <<= 32;
					num2 |= (ulong)((int)parentBytes[j + 2] << 26 | (int)parentBytes[j + 3] << 18);
				}
				num += num2;
			}
			return num;
		}

		// Token: 0x06005291 RID: 21137 RVA: 0x00159378 File Offset: 0x00157578
		public static bool IsFixUpCreatingNewConversation(ConversationIndex.FixupStage fixupStage)
		{
			ConversationIndex.FixupStage fixupStage2 = ConversationIndex.FixupStage.H3 | ConversationIndex.FixupStage.H4 | ConversationIndex.FixupStage.H8 | ConversationIndex.FixupStage.H11 | ConversationIndex.FixupStage.H12 | ConversationIndex.FixupStage.SC;
			return ConversationIndex.CheckStageValue(fixupStage2, fixupStage);
		}

		// Token: 0x06005292 RID: 21138 RVA: 0x00159392 File Offset: 0x00157592
		public static bool IsFixupAddingOutOfOrderMessageToConversation(ConversationIndex.FixupStage fixupStage)
		{
			return ConversationIndex.CheckStageValue(fixupStage, ConversationIndex.FixupStage.H6);
		}

		// Token: 0x06005293 RID: 21139 RVA: 0x0015939C File Offset: 0x0015759C
		public static bool CompareTopics(string incomingTopic, string foundTopic)
		{
			return (string.IsNullOrEmpty(foundTopic) && string.IsNullOrEmpty(incomingTopic)) || (foundTopic != null && incomingTopic != null && 0 == string.Compare(incomingTopic, foundTopic, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x06005294 RID: 21140 RVA: 0x001593D6 File Offset: 0x001575D6
		public ConversationIndex UpdateGuid(Guid guid)
		{
			return this.UpdateGuid(guid.ToByteArray());
		}

		// Token: 0x06005295 RID: 21141 RVA: 0x001593E5 File Offset: 0x001575E5
		public ConversationIndex UpdateGuid(ConversationId conversationId)
		{
			return this.UpdateGuid(conversationId.GetBytes());
		}

		// Token: 0x06005296 RID: 21142 RVA: 0x001593F4 File Offset: 0x001575F4
		public ConversationIndex UpdateHeader(byte[] header)
		{
			Util.ThrowOnNullOrEmptyArgument(header, "header");
			if (header.Length != 5)
			{
				throw new ArgumentException("header must be 5 bytes long");
			}
			byte[] destinationArray = ConversationIndex.CloneBytes(this.bytes);
			Array.Copy(header, 0, destinationArray, 1, 5);
			return new ConversationIndex(destinationArray);
		}

		// Token: 0x170016FD RID: 5885
		// (get) Token: 0x06005297 RID: 21143 RVA: 0x00159439 File Offset: 0x00157639
		public IList<byte> Bytes
		{
			get
			{
				if (this.readonlyBytes == null)
				{
					this.readonlyBytes = new System.Collections.ObjectModel.ReadOnlyCollection<byte>(this.bytes);
				}
				return this.readonlyBytes;
			}
		}

		// Token: 0x06005298 RID: 21144 RVA: 0x0015945A File Offset: 0x0015765A
		public byte[] ToByteArray()
		{
			return ConversationIndex.CloneBytes(this.bytes);
		}

		// Token: 0x170016FE RID: 5886
		// (get) Token: 0x06005299 RID: 21145 RVA: 0x00159467 File Offset: 0x00157667
		public IList<byte[]> Components
		{
			get
			{
				this.EnsureParsed();
				return this.readonlyComponents;
			}
		}

		// Token: 0x170016FF RID: 5887
		// (get) Token: 0x0600529A RID: 21146 RVA: 0x00159475 File Offset: 0x00157675
		public byte[] Header
		{
			get
			{
				return this.Components[0];
			}
		}

		// Token: 0x17001700 RID: 5888
		// (get) Token: 0x0600529B RID: 21147 RVA: 0x00159483 File Offset: 0x00157683
		public Guid Guid
		{
			get
			{
				this.EnsureParsed();
				return this.guid;
			}
		}

		// Token: 0x0600529C RID: 21148 RVA: 0x00159491 File Offset: 0x00157691
		public bool IsParentOf(ConversationIndex childIndex)
		{
			Util.ThrowOnNullArgument(childIndex, "childIndex");
			return childIndex.bytes.Length == this.bytes.Length + 5 && this.IsAncestorOf(childIndex);
		}

		// Token: 0x0600529D RID: 21149 RVA: 0x001594C4 File Offset: 0x001576C4
		public bool IsAncestorOf(ConversationIndex childIndex)
		{
			Util.ThrowOnNullArgument(childIndex, "childIndex");
			if (this.Guid.CompareTo(childIndex.Guid) != 0)
			{
				return false;
			}
			int num = this.bytes.Length;
			if (childIndex.bytes.Length < num + 5)
			{
				return false;
			}
			for (int i = 0; i < num; i++)
			{
				if (this.bytes[i] != childIndex.bytes[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600529E RID: 21150 RVA: 0x00159538 File Offset: 0x00157738
		private ConversationIndex UpdateGuid(byte[] guidBytes)
		{
			byte[] destinationArray = ConversationIndex.CloneBytes(this.bytes);
			Array.Copy(guidBytes, 0, destinationArray, 6, 16);
			return new ConversationIndex(destinationArray);
		}

		// Token: 0x0600529F RID: 21151 RVA: 0x00159564 File Offset: 0x00157764
		private void EnsureParsed()
		{
			if (this.isParsed)
			{
				return;
			}
			int num = (this.bytes.Length - 16 - 1) / 5;
			this.components = new List<byte[]>(num);
			this.components.Add(ConversationIndex.ExtractBytes(this.bytes, 1, 5));
			this.guid = new Guid(ConversationIndex.ExtractBytes(this.bytes, 6, 16));
			for (int i = 1; i < num; i++)
			{
				this.components.Add(ConversationIndex.ExtractBytes(this.bytes, 22 + (i - 1) * 5, 5));
			}
			this.readonlyComponents = new System.Collections.ObjectModel.ReadOnlyCollection<byte[]>(this.components);
			this.isParsed = true;
		}

		// Token: 0x060052A0 RID: 21152 RVA: 0x0015960C File Offset: 0x0015780C
		public override bool Equals(object o)
		{
			if (!(o is ConversationIndex))
			{
				return false;
			}
			ConversationIndex o2 = (ConversationIndex)o;
			return this.Equals(o2);
		}

		// Token: 0x060052A1 RID: 21153 RVA: 0x00159631 File Offset: 0x00157831
		public bool Equals(ConversationIndex o)
		{
			return ArrayComparer<byte>.Comparer.Equals(this.bytes, o.bytes);
		}

		// Token: 0x060052A2 RID: 21154 RVA: 0x0015964C File Offset: 0x0015784C
		public override int GetHashCode()
		{
			if (!this.hasHash)
			{
				int num = 0;
				int num2 = this.bytes.Length;
				for (int i = 0; i < num2; i++)
				{
					num ^= (int)this.bytes[i] << 8 * (i & 3);
				}
				this.hashCode = num;
				this.hasHash = true;
			}
			return this.hashCode;
		}

		// Token: 0x060052A3 RID: 21155 RVA: 0x001596A0 File Offset: 0x001578A0
		public override string ToString()
		{
			return GlobalObjectId.ByteArrayToHexString(this.bytes);
		}

		// Token: 0x060052A4 RID: 21156 RVA: 0x001596B0 File Offset: 0x001578B0
		public static ConversationIndex GenerateFromPhoneNumber(string number)
		{
			E164Number e164Number;
			if (!E164Number.TryParse(number, out e164Number))
			{
				return ConversationIndex.Empty;
			}
			int num = "472e2878-19b1-4ac1-a21a-".Length + 12;
			StringBuilder stringBuilder = new StringBuilder(num);
			stringBuilder.Append("472e2878-19b1-4ac1-a21a-");
			if (e164Number.SignificantNumber.Length <= 12)
			{
				stringBuilder.Append(e164Number.SignificantNumber);
			}
			else
			{
				stringBuilder.Append(e164Number.SignificantNumber.Substring(e164Number.SignificantNumber.Length - 12));
			}
			while (stringBuilder.Length < num)
			{
				stringBuilder.Append('f');
			}
			return ConversationIndex.Create(new Guid(stringBuilder.ToString()));
		}

		// Token: 0x060052A5 RID: 21157 RVA: 0x00159750 File Offset: 0x00157950
		// Note: this type is marked as 'beforefieldinit'.
		static ConversationIndex()
		{
			byte[] array = new byte[22];
			array[0] = 1;
			ConversationIndex.Empty = new ConversationIndex(array);
		}

		// Token: 0x04002CE1 RID: 11489
		private const int GuidLength = 16;

		// Token: 0x04002CE2 RID: 11490
		private const int ComponentLength = 5;

		// Token: 0x04002CE3 RID: 11491
		private const int HeaderLength = 22;

		// Token: 0x04002CE4 RID: 11492
		private const byte Reserved = 1;

		// Token: 0x04002CE5 RID: 11493
		private const int CountedPhoneNumberLength = 12;

		// Token: 0x04002CE6 RID: 11494
		private const string SmsConversationIndexGuidPrefix = "472e2878-19b1-4ac1-a21a-";

		// Token: 0x04002CE7 RID: 11495
		public static readonly ConversationIndex Empty;

		// Token: 0x04002CE8 RID: 11496
		private readonly byte[] bytes;

		// Token: 0x04002CE9 RID: 11497
		private System.Collections.ObjectModel.ReadOnlyCollection<byte> readonlyBytes;

		// Token: 0x04002CEA RID: 11498
		private bool isParsed;

		// Token: 0x04002CEB RID: 11499
		private Guid guid;

		// Token: 0x04002CEC RID: 11500
		private List<byte[]> components;

		// Token: 0x04002CED RID: 11501
		private System.Collections.ObjectModel.ReadOnlyCollection<byte[]> readonlyComponents;

		// Token: 0x04002CEE RID: 11502
		private bool hasHash;

		// Token: 0x04002CEF RID: 11503
		private int hashCode;

		// Token: 0x0200089F RID: 2207
		[Flags]
		public enum FixupStage
		{
			// Token: 0x04002CF1 RID: 11505
			Unknown = 0,
			// Token: 0x04002CF2 RID: 11506
			H1 = 1,
			// Token: 0x04002CF3 RID: 11507
			H2 = 2,
			// Token: 0x04002CF4 RID: 11508
			H3 = 4,
			// Token: 0x04002CF5 RID: 11509
			H4 = 8,
			// Token: 0x04002CF6 RID: 11510
			H5 = 16,
			// Token: 0x04002CF7 RID: 11511
			H6 = 32,
			// Token: 0x04002CF8 RID: 11512
			H7 = 64,
			// Token: 0x04002CF9 RID: 11513
			H8 = 128,
			// Token: 0x04002CFA RID: 11514
			H9 = 256,
			// Token: 0x04002CFB RID: 11515
			H10 = 512,
			// Token: 0x04002CFC RID: 11516
			H11 = 1024,
			// Token: 0x04002CFD RID: 11517
			H12 = 2048,
			// Token: 0x04002CFE RID: 11518
			H13 = 4096,
			// Token: 0x04002CFF RID: 11519
			H14 = 8192,
			// Token: 0x04002D00 RID: 11520
			Error = 262144,
			// Token: 0x04002D01 RID: 11521
			M1 = 524288,
			// Token: 0x04002D02 RID: 11522
			M2 = 1048576,
			// Token: 0x04002D03 RID: 11523
			M3 = 2097152,
			// Token: 0x04002D04 RID: 11524
			M4 = 4194304,
			// Token: 0x04002D05 RID: 11525
			L1 = 67108864,
			// Token: 0x04002D06 RID: 11526
			S1 = 134217728,
			// Token: 0x04002D07 RID: 11527
			S2 = 268435456,
			// Token: 0x04002D08 RID: 11528
			SC = 536870912,
			// Token: 0x04002D09 RID: 11529
			TC = 1073741824
		}
	}
}
