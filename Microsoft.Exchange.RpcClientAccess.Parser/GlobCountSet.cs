using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000052 RID: 82
	internal sealed class GlobCountSet : IEquatable<GlobCountSet>, IEnumerable<GlobCountRange>, IEnumerable
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00007636 File Offset: 0x00005836
		public bool IsEmpty
		{
			get
			{
				return this.ranges.Count == 0 && this.singles == null;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00007650 File Offset: 0x00005850
		public int CountRanges
		{
			get
			{
				this.MergeSingles();
				return this.ranges.Count;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000213 RID: 531 RVA: 0x00007664 File Offset: 0x00005864
		public ulong CountIds
		{
			get
			{
				this.MergeSingles();
				ulong num = 0UL;
				foreach (GlobCountRange globCountRange in this.ranges)
				{
					num += globCountRange.HighBound - globCountRange.LowBound + 1UL;
				}
				return num;
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x000076D0 File Offset: 0x000058D0
		internal GlobCountSet(ICollection<GlobCountRange> ranges)
		{
			this.ranges = new GlobCountSet.SegmentedList<GlobCountRange>(4096, ranges.Count);
			foreach (GlobCountRange range in ranges)
			{
				this.InsertRangeImpl(range);
			}
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00007738 File Offset: 0x00005938
		internal GlobCountSet()
		{
			this.ranges = new GlobCountSet.SegmentedList<GlobCountRange>(4096);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00007750 File Offset: 0x00005950
		public static GlobCountSet Parse(Reader reader)
		{
			GlobCountSet globCountSet = new GlobCountSet();
			globCountSet.InternalParse(reader);
			return globCountSet;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000776C File Offset: 0x0000596C
		public static GlobCountSet Intersect(GlobCountSet first, GlobCountSet second)
		{
			first.MergeSingles();
			second.MergeSingles();
			GlobCountSet globCountSet = null;
			int num = 0;
			int num2 = 0;
			while (num < first.ranges.Count && num2 < second.ranges.Count)
			{
				GlobCountRange globCountRange = first.ranges[num];
				GlobCountRange globCountRange2 = second.ranges[num2];
				if (globCountRange.HighBound < globCountRange2.LowBound)
				{
					num++;
				}
				else if (globCountRange.LowBound > globCountRange2.HighBound)
				{
					num2++;
				}
				else
				{
					GlobCountRange item = new GlobCountRange(Math.Max(globCountRange.LowBound, globCountRange2.LowBound), Math.Min(globCountRange.HighBound, globCountRange2.HighBound));
					if (globCountSet == null)
					{
						globCountSet = new GlobCountSet();
					}
					globCountSet.ranges.Add(item);
					if (globCountRange.HighBound < globCountRange2.HighBound)
					{
						num++;
					}
					else
					{
						num2++;
					}
				}
			}
			return globCountSet;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00007854 File Offset: 0x00005A54
		public bool Insert(GlobCountSet set)
		{
			this.MergeSingles();
			set.MergeSingles();
			bool flag = false;
			foreach (GlobCountRange range in set.ranges)
			{
				flag |= this.InsertRangeImpl(range);
			}
			return flag;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x000078BC File Offset: 0x00005ABC
		public bool Insert(ulong globCount)
		{
			if (this.singles == null && (this.ranges.Count < 10 || this.ranges[this.ranges.Count - 1].LowBound <= globCount))
			{
				return this.InsertRangeImpl(new GlobCountRange(globCount, globCount));
			}
			if (this.singles == null)
			{
				if (this.Contains(globCount))
				{
					return false;
				}
				this.singles = new GlobCountSet.SegmentedList<ulong>(8192);
			}
			this.singles.Add(globCount);
			return true;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00007941 File Offset: 0x00005B41
		public bool Insert(GlobCountRange range)
		{
			this.MergeSingles();
			return this.InsertRangeImpl(range);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00007950 File Offset: 0x00005B50
		public bool Remove(GlobCountSet set)
		{
			this.MergeSingles();
			set.MergeSingles();
			bool flag = false;
			foreach (GlobCountRange range in set.ranges)
			{
				flag |= this.RemoveRangeImpl(range);
			}
			return flag;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x000079B8 File Offset: 0x00005BB8
		public bool Remove(ulong globCount)
		{
			return this.Remove(new GlobCountRange(globCount, globCount));
		}

		// Token: 0x0600021D RID: 541 RVA: 0x000079C7 File Offset: 0x00005BC7
		public bool Remove(GlobCountRange range)
		{
			this.MergeSingles();
			return this.RemoveRangeImpl(range);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x000079D8 File Offset: 0x00005BD8
		public bool IdealPack()
		{
			if (this.IsEmpty)
			{
				return false;
			}
			this.MergeSingles();
			GlobCountRange globCountRange = this.ranges[this.ranges.Count - 1];
			if (globCountRange.LowBound == 1UL && this.ranges.Count == 1)
			{
				return false;
			}
			this.ranges = new GlobCountSet.SegmentedList<GlobCountRange>(4096);
			this.ranges.Add(new GlobCountRange(1UL, globCountRange.HighBound));
			return true;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00007A54 File Offset: 0x00005C54
		public bool Contains(ulong element)
		{
			GlobCountSet.VerifyGlobCountArgument(element, "element");
			this.MergeSingles();
			if (this.ranges.Count < 10)
			{
				for (int i = 0; i < this.ranges.Count; i++)
				{
					if (this.ranges[i].Contains(element))
					{
						return true;
					}
				}
				return false;
			}
			int num = this.ranges.BinarySearch(new GlobCountRange(element, element), GlobCountSet.GlobCountRangeLowBoundComparer.Instance);
			if (num >= 0)
			{
				return true;
			}
			int num2 = ~num - 1;
			return num2 >= 0 && element <= this.ranges[num2].HighBound;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00007AF6 File Offset: 0x00005CF6
		public GlobCountSet Clone()
		{
			this.MergeSingles();
			return new GlobCountSet(this.ranges);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00007B0C File Offset: 0x00005D0C
		public override string ToString()
		{
			this.MergeSingles();
			StringBuilder stringBuilder = new StringBuilder("{");
			for (int i = 0; i < this.ranges.Count; i++)
			{
				stringBuilder.Append(this.ranges[i]);
				if (i < this.ranges.Count - 1)
				{
					stringBuilder.Append(", ");
				}
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00007B86 File Offset: 0x00005D86
		public void Serialize(Writer writer)
		{
			this.MergeSingles();
			this.InternalSerialize(writer, 0, this.ranges.Count, 0);
			writer.WriteByte(0);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00007BAC File Offset: 0x00005DAC
		public static GlobCountSet Union(GlobCountSet first, GlobCountSet second)
		{
			first.MergeSingles();
			second.MergeSingles();
			GlobCountSet globCountSet = new GlobCountSet();
			int num = 0;
			int num2 = 0;
			GlobCountRange item;
			if (first.ranges[0].LowBound <= second.ranges[0].LowBound)
			{
				item = first.ranges[0];
				num++;
			}
			else
			{
				item = second.ranges[0];
				num2++;
			}
			while (num < first.ranges.Count || num2 < second.ranges.Count)
			{
				if (num < first.ranges.Count && item.HighBound + 1UL >= first.ranges[num].LowBound)
				{
					item = new GlobCountRange(item.LowBound, Math.Max(item.HighBound, first.ranges[num].HighBound));
					num++;
				}
				else if (num2 < second.ranges.Count && item.HighBound + 1UL >= second.ranges[num2].LowBound)
				{
					item = new GlobCountRange(item.LowBound, Math.Max(item.HighBound, second.ranges[num2].HighBound));
					num2++;
				}
				else
				{
					globCountSet.ranges.Add(item);
					if (num2 >= second.ranges.Count || (num < first.ranges.Count && first.ranges[num].LowBound <= second.ranges[num2].LowBound))
					{
						item = first.ranges[num];
						num++;
					}
					else
					{
						item = second.ranges[num2];
						num2++;
					}
				}
			}
			globCountSet.ranges.Add(item);
			return globCountSet;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00007D9C File Offset: 0x00005F9C
		public static GlobCountSet Subtract(GlobCountSet first, GlobCountSet second)
		{
			first.MergeSingles();
			second.MergeSingles();
			GlobCountSet globCountSet = null;
			int i = 0;
			int num = 0;
			GlobCountRange item = first.ranges[0];
			while (i < first.ranges.Count)
			{
				if (num < second.ranges.Count)
				{
					GlobCountRange globCountRange2;
					GlobCountRange globCountRange = globCountRange2 = second.ranges[num];
					if (globCountRange2.LowBound <= item.HighBound)
					{
						if (globCountRange.HighBound < item.LowBound)
						{
							num++;
							continue;
						}
						if (globCountRange.LowBound > item.LowBound)
						{
							GlobCountRange item2 = new GlobCountRange(item.LowBound, globCountRange.LowBound - 1UL);
							if (globCountSet == null)
							{
								globCountSet = new GlobCountSet();
							}
							globCountSet.ranges.Add(item2);
						}
						if (globCountRange.HighBound < item.HighBound)
						{
							item = new GlobCountRange(globCountRange.HighBound + 1UL, item.HighBound);
							num++;
							continue;
						}
						i++;
						if (i < first.ranges.Count)
						{
							item = first.ranges[i];
							continue;
						}
						continue;
					}
				}
				if (globCountSet == null)
				{
					globCountSet = new GlobCountSet();
				}
				globCountSet.ranges.Add(item);
				i++;
				if (i < first.ranges.Count)
				{
					item = first.ranges[i];
				}
			}
			return globCountSet;
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00007EEE File Offset: 0x000060EE
		public IEnumerator<GlobCountRange> GetEnumerator()
		{
			this.MergeSingles();
			return this.ranges.GetEnumerator();
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00007F06 File Offset: 0x00006106
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00007F10 File Offset: 0x00006110
		public bool Equals(GlobCountSet other)
		{
			if (other == null || this.CountRanges != other.CountRanges)
			{
				return false;
			}
			for (int i = 0; i < this.ranges.Count; i++)
			{
				if (!this.ranges[i].Equals(other.ranges[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00007F6C File Offset: 0x0000616C
		public override bool Equals(object obj)
		{
			GlobCountSet other = obj as GlobCountSet;
			return this.Equals(other);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00007F88 File Offset: 0x00006188
		public override int GetHashCode()
		{
			this.MergeSingles();
			int num = this.ranges.Count.GetHashCode();
			for (int i = 0; i < this.ranges.Count; i++)
			{
				num ^= this.ranges[i].GetHashCode();
			}
			return num;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00007FE3 File Offset: 0x000061E3
		internal static void VerifyGlobCountArgument(ulong globCount, string argumentName)
		{
			if (globCount == 0UL || globCount > 281474976710655UL)
			{
				throw new ArgumentOutOfRangeException(argumentName);
			}
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00007FFD File Offset: 0x000061FD
		private static bool TrySerializeRangeAsSingle(Writer writer, GlobCountRange range, int previousBytesInCommon)
		{
			if (range.LowBound == range.HighBound)
			{
				writer.WriteByte((byte)(6 - previousBytesInCommon));
				GlobCountSet.WriteBytes(writer, range.LowBound, previousBytesInCommon, 6 - previousBytesInCommon);
				return true;
			}
			return false;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000802D File Offset: 0x0000622D
		private static void SerializeRange(Writer writer, GlobCountRange range, int previousBytesInCommon)
		{
			writer.WriteByte(82);
			GlobCountSet.WriteBytes(writer, range.LowBound, previousBytesInCommon, 6 - previousBytesInCommon);
			GlobCountSet.WriteBytes(writer, range.HighBound, previousBytesInCommon, 6 - previousBytesInCommon);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000805C File Offset: 0x0000625C
		private static void EncodeRangeIntoBitmap(ref byte bitmap, GlobCountRange range, byte bitmapFirstElement)
		{
			byte b = (byte)(range.LowBound & 255UL);
			byte b2 = (byte)(range.HighBound & 255UL);
			for (int i = (int)b; i <= (int)b2; i++)
			{
				if (i != (int)bitmapFirstElement)
				{
					byte b3 = (byte)(1 << i - (int)bitmapFirstElement - 1);
					bitmap |= b3;
				}
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x000080AC File Offset: 0x000062AC
		private static int GetCountOfBytesInCommon(ulong left, ulong right, int previousBytesInCommon)
		{
			int result = previousBytesInCommon;
			int num = previousBytesInCommon;
			while (num < 6 && GlobCountSet.HasByteInCommon(num, left, right))
			{
				result = num + 1;
				num++;
			}
			return result;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x000080D6 File Offset: 0x000062D6
		private static bool HasByteInCommon(int byteIndex, ulong left, ulong right)
		{
			return (left & GlobCountSet.Masks[byteIndex]) == (right & GlobCountSet.Masks[byteIndex]);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x000080EC File Offset: 0x000062EC
		private static void WriteBytes(Writer writer, ulong globCount, int firstByteIndex, int byteCount)
		{
			for (int i = firstByteIndex; i < firstByteIndex + byteCount; i++)
			{
				byte value = (byte)(globCount >> (5 - i) * 8 & 255UL);
				writer.WriteByte(value);
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00008124 File Offset: 0x00006324
		private static GlobCountRange CreateGlobCountRangeFromBuffer(ulong lowBound, ulong highBound)
		{
			GlobCountRange result;
			try
			{
				result = new GlobCountRange(lowBound, highBound);
			}
			catch (ArgumentException ex)
			{
				throw new BufferParseException(ex.Message);
			}
			return result;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000815C File Offset: 0x0000635C
		private static ulong ReadBytes(Reader reader, ulong bytesInCommon, int countOfBytesInCommon, int bytesToRead)
		{
			ulong num = 0UL;
			for (int i = 0; i < bytesToRead; i++)
			{
				num <<= 8;
				num += (ulong)reader.ReadByte();
			}
			num <<= (6 - countOfBytesInCommon - bytesToRead) * 8;
			return num | bytesInCommon;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00008198 File Offset: 0x00006398
		private static ulong ClearBytesInCommon(ulong bytesInCommon, int countOfBytesInCommon, int bytesToClear)
		{
			ulong num = bytesInCommon;
			for (int i = countOfBytesInCommon - bytesToClear; i < countOfBytesInCommon; i++)
			{
				num &= ~GlobCountSet.Masks[i];
			}
			return num;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x000081C4 File Offset: 0x000063C4
		private void InternalSerialize(Writer writer, int startRangeIndex, int endRangeIndex, int previousBytesInCommon)
		{
			int i = startRangeIndex;
			while (i < endRangeIndex)
			{
				int num = 0;
				if (this.TrySerializeRangesWithBytesInCommon(writer, i, endRangeIndex, previousBytesInCommon, out num))
				{
					i += num;
				}
				else if (this.TrySerializeRangesAsBitmap(writer, i, endRangeIndex, previousBytesInCommon, out num))
				{
					i += num;
				}
				else
				{
					GlobCountRange range = this.ranges[i];
					if (!GlobCountSet.TrySerializeRangeAsSingle(writer, range, previousBytesInCommon))
					{
						GlobCountSet.SerializeRange(writer, range, previousBytesInCommon);
					}
					i++;
				}
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000822C File Offset: 0x0000642C
		private bool TrySerializeRangesWithBytesInCommon(Writer writer, int currentRangeIndex, int endRangeIndex, int previousBytesInCommon, out int countOfSerializedRanges)
		{
			countOfSerializedRanges = 0;
			if (previousBytesInCommon < 5 && currentRangeIndex + 1 < endRangeIndex)
			{
				int num = currentRangeIndex;
				ulong lowBound = this.ranges[currentRangeIndex].LowBound;
				int num2 = currentRangeIndex + 1;
				while (num2 < endRangeIndex && GlobCountSet.HasByteInCommon(previousBytesInCommon, lowBound, this.ranges[num2].HighBound))
				{
					num = num2;
					num2++;
				}
				if (num > currentRangeIndex)
				{
					int countOfBytesInCommon = GlobCountSet.GetCountOfBytesInCommon(lowBound, this.ranges[num].HighBound, previousBytesInCommon);
					writer.WriteByte((byte)(countOfBytesInCommon - previousBytesInCommon));
					GlobCountSet.WriteBytes(writer, lowBound, previousBytesInCommon, countOfBytesInCommon - previousBytesInCommon);
					this.InternalSerialize(writer, currentRangeIndex, num + 1, countOfBytesInCommon);
					writer.WriteByte(80);
					countOfSerializedRanges = num - currentRangeIndex + 1;
				}
			}
			return countOfSerializedRanges > 0;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x000082F4 File Offset: 0x000064F4
		private bool TrySerializeRangesAsBitmap(Writer writer, int currentRangeIndex, int endRangeIndex, int previousBytesInCommon, out int countOfSerializedRanges)
		{
			countOfSerializedRanges = 0;
			if (previousBytesInCommon == 5 && currentRangeIndex + 1 < endRangeIndex)
			{
				int num = currentRangeIndex;
				ulong lowBound = this.ranges[currentRangeIndex].LowBound;
				int num2 = currentRangeIndex + 1;
				while (num2 < endRangeIndex && this.ranges[num2].HighBound - lowBound <= 8UL)
				{
					num = num2;
					num2++;
				}
				if (num > currentRangeIndex)
				{
					byte b = (byte)(lowBound & 255UL);
					byte value = 0;
					for (int i = currentRangeIndex; i <= num; i++)
					{
						GlobCountSet.EncodeRangeIntoBitmap(ref value, this.ranges[i], b);
					}
					writer.WriteByte(66);
					writer.WriteByte(b);
					writer.WriteByte(value);
					countOfSerializedRanges = num - currentRangeIndex + 1;
				}
			}
			return countOfSerializedRanges > 0;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x000083B4 File Offset: 0x000065B4
		private void InternalParse(Reader reader)
		{
			ulong bytesInCommon = 0UL;
			int num = 0;
			Stack<int> stack = new Stack<int>(5);
			byte b;
			for (;;)
			{
				b = reader.ReadByte();
				byte b2 = b;
				switch (b2)
				{
				case 0:
					goto IL_5D;
				case 1:
				case 2:
				case 3:
				case 4:
				case 5:
				case 6:
					if ((int)b + num > 6)
					{
						goto Block_5;
					}
					if ((int)b + num < 6)
					{
						bytesInCommon = GlobCountSet.ReadBytes(reader, bytesInCommon, num, (int)b);
						stack.Push((int)b);
						num += (int)b;
					}
					else
					{
						ulong num2 = GlobCountSet.ReadBytes(reader, bytesInCommon, num, (int)b);
						GlobCountRange range = GlobCountSet.CreateGlobCountRangeFromBuffer(num2, num2);
						this.InsertRangeImpl(range);
					}
					break;
				default:
				{
					if (b2 != 66)
					{
						switch (b2)
						{
						case 80:
						{
							if (stack.Count < 1)
							{
								goto Block_10;
							}
							int num3 = stack.Pop();
							bytesInCommon = GlobCountSet.ClearBytesInCommon(bytesInCommon, num, num3);
							num -= num3;
							continue;
						}
						case 82:
						{
							ulong lowBound = GlobCountSet.ReadBytes(reader, bytesInCommon, num, 6 - num);
							ulong highBound = GlobCountSet.ReadBytes(reader, bytesInCommon, num, 6 - num);
							GlobCountRange range2 = GlobCountSet.CreateGlobCountRangeFromBuffer(lowBound, highBound);
							this.InsertRangeImpl(range2);
							continue;
						}
						}
						goto Block_3;
					}
					if (num != 5)
					{
						goto Block_7;
					}
					ulong num4 = GlobCountSet.ReadBytes(reader, bytesInCommon, num, 1);
					this.InsertRangeImpl(GlobCountSet.CreateGlobCountRangeFromBuffer(num4, num4));
					byte b3 = reader.ReadByte();
					for (int i = 0; i < 8; i++)
					{
						byte b4 = (byte)(1 << i);
						if ((b3 & b4) != 0)
						{
							ulong num5 = num4 + (ulong)((long)i) + 1UL;
							this.InsertRangeImpl(GlobCountSet.CreateGlobCountRangeFromBuffer(num5, num5));
						}
					}
					break;
				}
				}
			}
			Block_3:
			goto IL_1A8;
			IL_5D:
			if (num == 0)
			{
				return;
			}
			throw new BufferParseException("End of GlobCountSet found prematurely, while there are still bytes in common that have not been 'popped'.");
			Block_5:
			throw new BufferParseException(string.Format("Too many bytes specified. We already have {0} bytes in common, and we found {1} more bytes being specified.", num, b));
			Block_7:
			throw new BufferParseException("Found a bitmap before we have 5 bytes in common.");
			Block_10:
			throw new BufferParseException("Found a pop instruction, and there is no bytes in common pushed.");
			IL_1A8:
			throw new BufferParseException(string.Format("Expected: 0 to 6, 'B', 'P' or 'R'. Found: {0}.", b));
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00008580 File Offset: 0x00006780
		private bool InsertRangeImpl(GlobCountRange range)
		{
			int num = this.ranges.Count - 1;
			if (this.ranges.Count >= 10 && range.HighBound < this.ranges[this.ranges.Count - 1].LowBound)
			{
				int num2 = this.ranges.BinarySearch(range, GlobCountSet.GlobCountRangeHighBoundComparer.Instance);
				if (num2 >= 0)
				{
					num = num2;
				}
				else
				{
					num = ~num2;
					if (num == this.ranges.Count)
					{
						num--;
					}
				}
			}
			int i = num;
			while (i >= 0)
			{
				GlobCountRange globCountRange = this.ranges[i];
				if (globCountRange.HighBound + 1UL < range.LowBound)
				{
					this.ranges.Insert(i + 1, range);
					return true;
				}
				if (globCountRange.LowBound <= range.LowBound)
				{
					if (globCountRange.HighBound < range.HighBound)
					{
						this.ranges[i] = new GlobCountRange(globCountRange.LowBound, range.HighBound);
						return true;
					}
					return false;
				}
				else if (globCountRange.LowBound > range.HighBound + 1UL)
				{
					i--;
				}
				else
				{
					ulong lowBound = Math.Min(globCountRange.LowBound, range.LowBound);
					ulong highBound = Math.Max(globCountRange.HighBound, range.HighBound);
					range = new GlobCountRange(lowBound, highBound);
					this.ranges.RemoveAt(i);
					i--;
				}
			}
			this.ranges.Insert(0, range);
			return true;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x000086F4 File Offset: 0x000068F4
		private bool RemoveRangeImpl(GlobCountRange range)
		{
			bool result = false;
			int num = 0;
			if (this.ranges.Count >= 10 && range.LowBound > this.ranges[0].HighBound)
			{
				int num2 = this.ranges.BinarySearch(range, GlobCountSet.GlobCountRangeLowBoundComparer.Instance);
				if (num2 >= 0)
				{
					num = num2;
				}
				else
				{
					num = ~num2 - 1;
					if (num < 0)
					{
						num++;
					}
				}
			}
			int i = num;
			while (i < this.ranges.Count)
			{
				GlobCountRange globCountRange = this.ranges[i];
				if (globCountRange.HighBound < range.LowBound)
				{
					i++;
				}
				else
				{
					if (globCountRange.LowBound > range.HighBound)
					{
						break;
					}
					result = true;
					if (range.HighBound >= globCountRange.HighBound)
					{
						if (range.LowBound <= globCountRange.LowBound)
						{
							this.ranges.RemoveAt(i);
						}
						else
						{
							this.ranges[i] = new GlobCountRange(globCountRange.LowBound, range.LowBound - 1UL);
							i++;
						}
					}
					else
					{
						if (range.LowBound <= globCountRange.LowBound)
						{
							this.ranges[i] = new GlobCountRange(range.HighBound + 1UL, globCountRange.HighBound);
							break;
						}
						this.ranges[i] = new GlobCountRange(range.HighBound + 1UL, globCountRange.HighBound);
						this.ranges.Insert(i, new GlobCountRange(globCountRange.LowBound, range.LowBound - 1UL));
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00008881 File Offset: 0x00006A81
		private void MergeSingles()
		{
			if (this.singles != null)
			{
				this.DoMergeSingles();
			}
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00008894 File Offset: 0x00006A94
		private void DoMergeSingles()
		{
			if (this.singles.Count <= 1)
			{
				foreach (ulong num in this.singles)
				{
					this.InsertRangeImpl(new GlobCountRange(num, num));
				}
				this.singles = null;
				return;
			}
			this.singles.Sort();
			if (this.ranges.Count == 0 || this.ranges[this.ranges.Count - 1].LowBound <= this.singles[0])
			{
				using (GlobCountSet.SegmentedList<ulong>.Enumerator enumerator2 = this.singles.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						ulong num2 = enumerator2.Current;
						this.InsertRangeImpl(new GlobCountRange(num2, num2));
					}
					goto IL_212;
				}
			}
			GlobCountSet.SegmentedList<GlobCountRange> segmentedList = this.ranges;
			this.ranges = new GlobCountSet.SegmentedList<GlobCountRange>(4096);
			int num3 = 0;
			int i = 0;
			while (num3 < segmentedList.Count && i < this.singles.Count)
			{
				ulong num4 = this.singles[i];
				GlobCountRange globCountRange = new GlobCountRange(num4, num4);
				int num5 = segmentedList.BinarySearch(globCountRange, GlobCountSet.GlobCountRangeLowBoundComparer.Instance);
				if (num5 >= 0)
				{
					i++;
				}
				else
				{
					num5 = ~num5;
					if (num3 < num5)
					{
						if (this.ranges.Count != 0)
						{
							this.InsertRangeImpl(segmentedList[num3]);
							num3++;
						}
						if (num3 < num5)
						{
							this.ranges.AppendFrom(segmentedList, num3, num5 - num3);
							num3 = num5;
						}
					}
					this.InsertRangeImpl(globCountRange);
					i++;
				}
			}
			if (num3 < segmentedList.Count)
			{
				this.InsertRangeImpl(segmentedList[num3]);
				num3++;
				if (num3 < segmentedList.Count)
				{
					this.ranges.AppendFrom(segmentedList, num3, segmentedList.Count - num3);
				}
			}
			else
			{
				while (i < this.singles.Count)
				{
					ulong num6 = this.singles[i];
					GlobCountRange range = new GlobCountRange(num6, num6);
					this.InsertRangeImpl(range);
					i++;
				}
			}
			IL_212:
			this.singles = null;
		}

		// Token: 0x04000104 RID: 260
		private const int GlobCountSize = 6;

		// Token: 0x04000105 RID: 261
		private const int RangesSegmentSize = 4096;

		// Token: 0x04000106 RID: 262
		private const int SinglesSegmentSize = 8192;

		// Token: 0x04000107 RID: 263
		private const ulong MaxGlobCount = 281474976710655UL;

		// Token: 0x04000108 RID: 264
		private static readonly ulong[] Masks = new ulong[]
		{
			280375465082880UL,
			1095216660480UL,
			4278190080UL,
			16711680UL,
			65280UL,
			255UL
		};

		// Token: 0x04000109 RID: 265
		private GlobCountSet.SegmentedList<GlobCountRange> ranges;

		// Token: 0x0400010A RID: 266
		private GlobCountSet.SegmentedList<ulong> singles;

		// Token: 0x02000053 RID: 83
		private class SegmentedList<T> : ICollection<T>, IEnumerable<T>, IEnumerable
		{
			// Token: 0x0600023D RID: 573 RVA: 0x00008B20 File Offset: 0x00006D20
			public SegmentedList(int segmentSize) : this(segmentSize, 0)
			{
			}

			// Token: 0x0600023E RID: 574 RVA: 0x00008B2C File Offset: 0x00006D2C
			public SegmentedList(int segmentSize, int initialCapacity)
			{
				this.segmentSize = segmentSize;
				this.offsetMask = segmentSize - 1;
				this.segmentShift = 0;
				while ((segmentSize >>= 1) != 0)
				{
					this.segmentShift++;
				}
				if (initialCapacity > 0)
				{
					this.items = new T[initialCapacity + this.segmentSize - 1 >> this.segmentShift][];
					initialCapacity = Math.Min(this.segmentSize, initialCapacity);
					this.items[0] = new T[initialCapacity];
					this.capacity = initialCapacity;
				}
			}

			// Token: 0x17000085 RID: 133
			// (get) Token: 0x0600023F RID: 575 RVA: 0x00008BB4 File Offset: 0x00006DB4
			public int Count
			{
				get
				{
					return this.count;
				}
			}

			// Token: 0x17000086 RID: 134
			// (get) Token: 0x06000240 RID: 576 RVA: 0x00008BBC File Offset: 0x00006DBC
			bool ICollection<!0>.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000087 RID: 135
			public T this[int index]
			{
				get
				{
					return this.items[index >> this.segmentShift][index & this.offsetMask];
				}
				set
				{
					this.items[index >> this.segmentShift][index & this.offsetMask] = value;
				}
			}

			// Token: 0x06000243 RID: 579 RVA: 0x00008C04 File Offset: 0x00006E04
			public void Add(T item)
			{
				if (this.count == this.capacity)
				{
					this.EnsureCapacity(this.count + 1);
				}
				this.items[this.count >> this.segmentShift][this.count & this.offsetMask] = item;
				this.count++;
			}

			// Token: 0x06000244 RID: 580 RVA: 0x00008C68 File Offset: 0x00006E68
			public void Insert(int index, T item)
			{
				if (this.count == this.capacity)
				{
					this.EnsureCapacity(this.count + 1);
				}
				if (index < this.count)
				{
					this.AddRoomForElement(index);
				}
				this.count++;
				this.items[index >> this.segmentShift][index & this.offsetMask] = item;
			}

			// Token: 0x06000245 RID: 581 RVA: 0x00008CCF File Offset: 0x00006ECF
			public void RemoveAt(int index)
			{
				if (index < this.count)
				{
					this.RemoveRoomForElement(index);
				}
				this.count--;
			}

			// Token: 0x06000246 RID: 582 RVA: 0x00008CF0 File Offset: 0x00006EF0
			public int BinarySearch(T item, IComparer<T> comparer)
			{
				int i = 0;
				int num = this.count - 1;
				while (i <= num)
				{
					int num2 = i + (num - i >> 1);
					int num3 = comparer.Compare(this.items[num2 >> this.segmentShift][num2 & this.offsetMask], item);
					if (num3 == 0)
					{
						return num2;
					}
					if (num3 < 0)
					{
						i = num2 + 1;
					}
					else
					{
						num = num2 - 1;
					}
				}
				return ~i;
			}

			// Token: 0x06000247 RID: 583 RVA: 0x00008D52 File Offset: 0x00006F52
			public void Sort()
			{
				this.QuickSort(0, this.count - 1, Comparer<T>.Default);
			}

			// Token: 0x06000248 RID: 584 RVA: 0x00008D68 File Offset: 0x00006F68
			public void AppendFrom(GlobCountSet.SegmentedList<T> from, int index, int count)
			{
				if (count > 0)
				{
					int num = this.count + count;
					if (this.capacity < num)
					{
						this.EnsureCapacity(num);
					}
					do
					{
						int num2 = index / from.segmentSize;
						int num3 = index % from.segmentSize;
						int val = from.segmentSize - num3;
						int num4 = this.count >> this.segmentShift;
						int num5 = this.count & this.offsetMask;
						int val2 = this.segmentSize - num5;
						int num6 = Math.Min(count, Math.Min(val, val2));
						Array.Copy(from.items[num2], num3, this.items[num4], num5, num6);
						index += num6;
						count -= num6;
						this.count += num6;
					}
					while (count != 0);
				}
			}

			// Token: 0x06000249 RID: 585 RVA: 0x00008E28 File Offset: 0x00007028
			public GlobCountSet.SegmentedList<T>.Enumerator GetEnumerator()
			{
				return new GlobCountSet.SegmentedList<T>.Enumerator(this);
			}

			// Token: 0x0600024A RID: 586 RVA: 0x00008E30 File Offset: 0x00007030
			IEnumerator<T> IEnumerable<!0>.GetEnumerator()
			{
				return new GlobCountSet.SegmentedList<T>.Enumerator(this);
			}

			// Token: 0x0600024B RID: 587 RVA: 0x00008E3D File Offset: 0x0000703D
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new GlobCountSet.SegmentedList<T>.Enumerator(this);
			}

			// Token: 0x0600024C RID: 588 RVA: 0x00008E4A File Offset: 0x0000704A
			void ICollection<!0>.Clear()
			{
				throw new NotImplementedException("This method of ICollection is not implemented");
			}

			// Token: 0x0600024D RID: 589 RVA: 0x00008E56 File Offset: 0x00007056
			bool ICollection<!0>.Contains(T item)
			{
				throw new NotImplementedException("This method of ICollection is not implemented");
			}

			// Token: 0x0600024E RID: 590 RVA: 0x00008E62 File Offset: 0x00007062
			void ICollection<!0>.CopyTo(T[] array, int arrayIndex)
			{
				throw new NotImplementedException("This method of ICollection is not implemented");
			}

			// Token: 0x0600024F RID: 591 RVA: 0x00008E6E File Offset: 0x0000706E
			bool ICollection<!0>.Remove(T item)
			{
				throw new NotImplementedException("This method of ICollection is not implemented");
			}

			// Token: 0x06000250 RID: 592 RVA: 0x00008E7C File Offset: 0x0000707C
			private void AddRoomForElement(int index)
			{
				int num = index >> this.segmentShift;
				int num2 = this.count >> this.segmentShift;
				int num3 = index & this.offsetMask;
				int num4 = this.count & this.offsetMask;
				if (num == num2)
				{
					Array.Copy(this.items[num], num3, this.items[num], num3 + 1, num4 - num3);
					return;
				}
				T t = this.items[num][this.segmentSize - 1];
				Array.Copy(this.items[num], num3, this.items[num], num3 + 1, this.segmentSize - num3 - 1);
				for (int i = num + 1; i < num2; i++)
				{
					T t2 = this.items[i][this.segmentSize - 1];
					Array.Copy(this.items[i], 0, this.items[i], 1, this.segmentSize - 1);
					this.items[i][0] = t;
					t = t2;
				}
				Array.Copy(this.items[num2], 0, this.items[num2], 1, num4);
				this.items[num2][0] = t;
			}

			// Token: 0x06000251 RID: 593 RVA: 0x00008FA0 File Offset: 0x000071A0
			private void RemoveRoomForElement(int index)
			{
				int num = index >> this.segmentShift;
				int num2 = this.count - 1 >> this.segmentShift;
				int num3 = index & this.offsetMask;
				int num4 = this.count - 1 & this.offsetMask;
				if (num == num2)
				{
					Array.Copy(this.items[num], num3 + 1, this.items[num], num3, num4 - num3);
					return;
				}
				Array.Copy(this.items[num], num3 + 1, this.items[num], num3, this.segmentSize - num3 - 1);
				for (int i = num + 1; i < num2; i++)
				{
					this.items[i - 1][this.segmentSize - 1] = this.items[i][0];
					Array.Copy(this.items[i], 1, this.items[i], 0, this.segmentSize - 1);
				}
				this.items[num2 - 1][this.segmentSize - 1] = this.items[num2][0];
				Array.Copy(this.items[num2], 1, this.items[num2], 0, num4);
			}

			// Token: 0x06000252 RID: 594 RVA: 0x000090C0 File Offset: 0x000072C0
			private void EnsureCapacity(int minCapacity)
			{
				if (this.capacity < this.segmentSize)
				{
					if (this.items == null)
					{
						this.items = new T[minCapacity + this.segmentSize - 1 >> this.segmentShift][];
					}
					int i = this.segmentSize;
					if (minCapacity < this.segmentSize)
					{
						for (i = ((this.capacity == 0) ? 2 : (this.capacity * 2)); i < minCapacity; i *= 2)
						{
						}
						i = Math.Min(i, this.segmentSize);
					}
					T[] array = new T[i];
					if (this.count > 0)
					{
						Array.Copy(this.items[0], 0, array, 0, this.count);
					}
					this.items[0] = array;
					this.capacity = array.Length;
				}
				if (this.capacity < minCapacity)
				{
					int num = this.capacity >> this.segmentShift;
					int num2 = minCapacity + this.segmentSize - 1 >> this.segmentShift;
					if (num2 > this.items.Length)
					{
						int j;
						for (j = this.items.Length * 2; j < num2; j *= 2)
						{
						}
						T[][] destinationArray = new T[j][];
						Array.Copy(this.items, 0, destinationArray, 0, num);
						this.items = destinationArray;
					}
					for (int k = num; k < num2; k++)
					{
						this.items[k] = new T[this.segmentSize];
						this.capacity += this.segmentSize;
					}
				}
			}

			// Token: 0x06000253 RID: 595 RVA: 0x00009228 File Offset: 0x00007428
			private void SwapIfGreaterWithItems(IComparer<T> comparer, int a, int b)
			{
				if (a != b && comparer.Compare(this.items[a >> this.segmentShift][a & this.offsetMask], this.items[b >> this.segmentShift][b & this.offsetMask]) > 0)
				{
					T t = this.items[a >> this.segmentShift][a & this.offsetMask];
					this.items[a >> this.segmentShift][a & this.offsetMask] = this.items[b >> this.segmentShift][b & this.offsetMask];
					this.items[b >> this.segmentShift][b & this.offsetMask] = t;
				}
			}

			// Token: 0x06000254 RID: 596 RVA: 0x00009304 File Offset: 0x00007504
			private void QuickSort(int left, int right, IComparer<T> comparer)
			{
				do
				{
					int num = left;
					int num2 = right;
					int num3 = num + (num2 - num >> 1);
					this.SwapIfGreaterWithItems(comparer, num, num3);
					this.SwapIfGreaterWithItems(comparer, num, num2);
					this.SwapIfGreaterWithItems(comparer, num3, num2);
					T t = this.items[num3 >> this.segmentShift][num3 & this.offsetMask];
					for (;;)
					{
						if (comparer.Compare(this.items[num >> this.segmentShift][num & this.offsetMask], t) >= 0)
						{
							while (comparer.Compare(t, this.items[num2 >> this.segmentShift][num2 & this.offsetMask]) < 0)
							{
								num2--;
							}
							if (num > num2)
							{
								break;
							}
							if (num < num2)
							{
								T t2 = this.items[num >> this.segmentShift][num & this.offsetMask];
								this.items[num >> this.segmentShift][num & this.offsetMask] = this.items[num2 >> this.segmentShift][num2 & this.offsetMask];
								this.items[num2 >> this.segmentShift][num2 & this.offsetMask] = t2;
							}
							num++;
							num2--;
							if (num > num2)
							{
								break;
							}
						}
						else
						{
							num++;
						}
					}
					if (num2 - left <= right - num)
					{
						if (left < num2)
						{
							this.QuickSort(left, num2, comparer);
						}
						left = num;
					}
					else
					{
						if (num < right)
						{
							this.QuickSort(num, right, comparer);
						}
						right = num2;
					}
				}
				while (left < right);
			}

			// Token: 0x0400010B RID: 267
			private readonly int segmentSize;

			// Token: 0x0400010C RID: 268
			private readonly int segmentShift;

			// Token: 0x0400010D RID: 269
			private readonly int offsetMask;

			// Token: 0x0400010E RID: 270
			private int capacity;

			// Token: 0x0400010F RID: 271
			private int count;

			// Token: 0x04000110 RID: 272
			private T[][] items;

			// Token: 0x02000054 RID: 84
			public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
			{
				// Token: 0x06000255 RID: 597 RVA: 0x00009484 File Offset: 0x00007684
				internal Enumerator(GlobCountSet.SegmentedList<T> list)
				{
					this.list = list;
					this.index = -1;
				}

				// Token: 0x06000256 RID: 598 RVA: 0x00009494 File Offset: 0x00007694
				public void Dispose()
				{
				}

				// Token: 0x06000257 RID: 599 RVA: 0x00009496 File Offset: 0x00007696
				public bool MoveNext()
				{
					if (this.index < this.list.count - 1)
					{
						this.index++;
						return true;
					}
					this.index = -1;
					return false;
				}

				// Token: 0x17000088 RID: 136
				// (get) Token: 0x06000258 RID: 600 RVA: 0x000094C5 File Offset: 0x000076C5
				public T Current
				{
					get
					{
						return this.list[this.index];
					}
				}

				// Token: 0x17000089 RID: 137
				// (get) Token: 0x06000259 RID: 601 RVA: 0x000094D8 File Offset: 0x000076D8
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x0600025A RID: 602 RVA: 0x000094E5 File Offset: 0x000076E5
				void IEnumerator.Reset()
				{
					this.index = -1;
				}

				// Token: 0x04000111 RID: 273
				private GlobCountSet.SegmentedList<T> list;

				// Token: 0x04000112 RID: 274
				private int index;
			}
		}

		// Token: 0x02000055 RID: 85
		private class GlobCountRangeLowBoundComparer : IComparer<GlobCountRange>
		{
			// Token: 0x0600025B RID: 603 RVA: 0x000094F0 File Offset: 0x000076F0
			public int Compare(GlobCountRange x, GlobCountRange y)
			{
				return x.LowBound.CompareTo(y.LowBound);
			}

			// Token: 0x04000113 RID: 275
			public static readonly GlobCountSet.GlobCountRangeLowBoundComparer Instance = new GlobCountSet.GlobCountRangeLowBoundComparer();
		}

		// Token: 0x02000056 RID: 86
		private class GlobCountRangeHighBoundComparer : IComparer<GlobCountRange>
		{
			// Token: 0x0600025E RID: 606 RVA: 0x00009528 File Offset: 0x00007728
			public int Compare(GlobCountRange x, GlobCountRange y)
			{
				return x.HighBound.CompareTo(y.HighBound);
			}

			// Token: 0x04000114 RID: 276
			public static readonly GlobCountSet.GlobCountRangeHighBoundComparer Instance = new GlobCountSet.GlobCountRangeHighBoundComparer();
		}
	}
}
