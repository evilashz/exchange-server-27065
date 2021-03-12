using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser
{
	// Token: 0x0200017F RID: 383
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class FastTransferWriter : BaseObject, IFastTransferDataInterface, IDisposable
	{
		// Token: 0x06000772 RID: 1906 RVA: 0x0001A108 File Offset: 0x00018308
		internal FastTransferWriter(ArraySegment<byte> buffer)
		{
			this.buffer = buffer;
			this.writer = new BufferWriter(this.buffer);
			this.overflowWriter = new BufferWriter(this.overflowBuffer);
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000773 RID: 1907 RVA: 0x0001A159 File Offset: 0x00018359
		public int AvailableBufferSize
		{
			get
			{
				if (!this.OverflowOccurred && !this.forceBufferFull)
				{
					return (int)(this.writer.AvailableSpace + 1U);
				}
				return 0;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x0001A17A File Offset: 0x0001837A
		public bool IsBufferFull
		{
			get
			{
				return this.AvailableBufferSize == 0 || this.forceBufferFull;
			}
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0001A18C File Offset: 0x0001838C
		public void ForceBufferFull()
		{
			this.forceBufferFull = true;
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0001A198 File Offset: 0x00018398
		public int CopyFrom(Stream source, int count)
		{
			int num = Math.Min(this.AvailableBufferSize, count);
			int num2 = Math.Min(num, (int)this.writer.AvailableSpace);
			int num3 = this.writer.CopyFrom(source, num2);
			if (num3 == num2 && num > num2)
			{
				num3 += this.overflowWriter.CopyFrom(source, num - num2);
			}
			return num3;
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000777 RID: 1911 RVA: 0x0001A1ED File Offset: 0x000183ED
		public int Position
		{
			get
			{
				return (int)this.writer.Position;
			}
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0001A1FC File Offset: 0x000183FC
		public ArraySegment<byte> GetOverflowBytes()
		{
			if (this.OverflowOccurred)
			{
				return this.overflowBuffer.SubSegment(0, (int)this.overflowWriter.Position);
			}
			return default(ArraySegment<byte>);
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0001A23C File Offset: 0x0001843C
		public void PutMarker(PropertyTag marker)
		{
			if (!marker.IsMarker)
			{
				throw new ArgumentException("Should be a marker", "marker");
			}
			uint atom = FastTransferWriter.DenormalizeTag(null, marker);
			this.WriteAtom<uint>(delegate(Writer writer, uint value)
			{
				writer.WriteUInt32(value);
			}, atom);
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0001A298 File Offset: 0x00018498
		public void WritePropertyInfo(FastTransferDownloadContext context, PropertyTag propertyTag, NamedProperty namedProperty)
		{
			uint key = FastTransferWriter.DenormalizeTag(context, propertyTag);
			this.WriteAtom<KeyValuePair<uint, NamedProperty>>(delegate(Writer writer, KeyValuePair<uint, NamedProperty> value)
			{
				FastTransferWriter.PropertyInfoWriter(writer, value);
			}, new KeyValuePair<uint, NamedProperty>(key, namedProperty));
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0001A2E0 File Offset: 0x000184E0
		public void WriteLength(uint length)
		{
			this.WriteAtom<uint>(delegate(Writer writer, uint value)
			{
				writer.WriteUInt32(value);
			}, length);
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0001A320 File Offset: 0x00018520
		public void SerializeFixedSizeValue(PropertyValue propertyValue)
		{
			PropertyType propertyType = propertyValue.PropertyTag.PropertyType;
			if (propertyType == PropertyType.Bool)
			{
				this.WriteAtom<short>(delegate(Writer writer, short value)
				{
					writer.WriteInt16(value);
				}, propertyValue.GetValueAssert<bool>() ? 1 : 0);
				return;
			}
			this.WriteAtom<PropertyValue>(delegate(Writer writer, PropertyValue pv)
			{
				writer.WritePropertyValueWithoutTag(pv, CTSGlobals.AsciiEncoding, WireFormatStyle.Rop);
			}, propertyValue);
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0001A398 File Offset: 0x00018598
		public void SerializeVariableSizeValue(byte[] data, int startIndex, int sizeToWriteOut)
		{
			if (sizeToWriteOut > this.AvailableBufferSize)
			{
				throw new ArgumentOutOfRangeException("sizeToWriteOut");
			}
			int num = Math.Min(sizeToWriteOut, (int)this.writer.AvailableSpace);
			this.writer.WriteBytesSegment(new ArraySegment<byte>(data, startIndex, num));
			if (num < sizeToWriteOut)
			{
				this.overflowWriter.WriteBytesSegment(new ArraySegment<byte>(data, startIndex + num, sizeToWriteOut - num));
			}
			this.atomsWrittenSinceLastChangeOfState++;
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0001A407 File Offset: 0x00018607
		public bool TryWriteOverflow(ref ArraySegment<byte> overflowBytes)
		{
			if (overflowBytes.Count == 0)
			{
				return true;
			}
			if ((long)overflowBytes.Count <= (long)((ulong)this.writer.AvailableSpace))
			{
				this.writer.WriteBytesSegment(overflowBytes);
				overflowBytes = default(ArraySegment<byte>);
				return true;
			}
			return false;
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x0001A444 File Offset: 0x00018644
		public override string ToString()
		{
			return string.Format("FX Writer: {0}/{1}, overflow size = {2}", this.writer.Position, this.buffer.Count, this.overflowWriter.Position);
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0001A48E File Offset: 0x0001868E
		[DebuggerStepThrough]
		public void NotifyCanSplitBuffers()
		{
			this.atomsWrittenSinceLastChangeOfState = 0;
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0001A497 File Offset: 0x00018697
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<FastTransferWriter>(this);
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0001A49F File Offset: 0x0001869F
		protected override void InternalDispose()
		{
			Util.DisposeIfPresent(this.writer);
			Util.DisposeIfPresent(this.overflowWriter);
			base.InternalDispose();
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0001A4BD File Offset: 0x000186BD
		private static uint DenormalizeTag(FastTransferDownloadContext context, PropertyTag propertyTag)
		{
			if (context == null)
			{
				return FastTransferWriter.DenormalizeTag(true, false, propertyTag);
			}
			return FastTransferWriter.DenormalizeTag(context.UseCpidOrUnicode, context.UseCpid, propertyTag);
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0001A4E0 File Offset: 0x000186E0
		internal static uint DenormalizeTag(bool useCpidOrUnicode, bool useCpid, PropertyTag propertyTag)
		{
			uint num = propertyTag;
			if (PropertyTag.IsStringPropertyType(propertyTag.ElementPropertyType))
			{
				num &= 4294930432U;
				if (useCpidOrUnicode)
				{
					if (useCpid)
					{
						num |= 33968U;
					}
					else
					{
						num |= 31U;
					}
				}
				else
				{
					num |= 30U;
				}
			}
			return num;
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0001A528 File Offset: 0x00018728
		private static void PropertyInfoWriter(Writer writer, KeyValuePair<uint, NamedProperty> propertyInfo)
		{
			uint key = propertyInfo.Key;
			NamedProperty value = propertyInfo.Value;
			writer.WriteUInt32(key);
			if (value != null)
			{
				writer.WriteGuid(value.Guid);
				writer.WriteByte((byte)value.Kind);
				if (value.Kind == NamedPropertyKind.String)
				{
					writer.WriteUnicodeString(value.Name, StringFlags.IncludeNull);
					return;
				}
				if (value.Kind == NamedPropertyKind.Id)
				{
					writer.WriteUInt32(value.Id);
				}
			}
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0001A594 File Offset: 0x00018794
		private void WriteAtom<TAtom>(Action<Writer, TAtom> writeDelegate, TAtom atom)
		{
			if (writeDelegate.Target != null)
			{
				throw new ArgumentException("Writer should be a static delegate for perf reasons");
			}
			bool flag = true;
			if (this.writer.AvailableSpace < 536U)
			{
				using (CountWriter countWriter = new CountWriter())
				{
					writeDelegate(countWriter, atom);
					if ((ulong)this.writer.AvailableSpace < (ulong)countWriter.Position)
					{
						flag = false;
					}
				}
			}
			if (flag)
			{
				writeDelegate(this.writer, atom);
			}
			else
			{
				writeDelegate(this.overflowWriter, atom);
			}
			this.atomsWrittenSinceLastChangeOfState++;
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000787 RID: 1927 RVA: 0x0001A638 File Offset: 0x00018838
		private bool OverflowOccurred
		{
			get
			{
				return this.overflowWriter.Position > 0L;
			}
		}

		// Token: 0x040003B0 RID: 944
		private const int MaxOutputBufferBytes = 32767;

		// Token: 0x040003B1 RID: 945
		private const int OverflowLimit = 536;

		// Token: 0x040003B2 RID: 946
		private const int Teaser = 1;

		// Token: 0x040003B3 RID: 947
		private const int PerformanceConstantMaxBuffersPerProcessor = 5;

		// Token: 0x040003B4 RID: 948
		private readonly ArraySegment<byte> buffer;

		// Token: 0x040003B5 RID: 949
		private readonly BufferWriter writer;

		// Token: 0x040003B6 RID: 950
		private readonly ArraySegment<byte> overflowBuffer = new ArraySegment<byte>(new byte[536]);

		// Token: 0x040003B7 RID: 951
		private readonly BufferWriter overflowWriter;

		// Token: 0x040003B8 RID: 952
		private bool forceBufferFull;

		// Token: 0x040003B9 RID: 953
		private int atomsWrittenSinceLastChangeOfState;
	}
}
