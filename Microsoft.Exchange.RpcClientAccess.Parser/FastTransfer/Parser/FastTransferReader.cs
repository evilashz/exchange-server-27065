using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser
{
	// Token: 0x02000171 RID: 369
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class FastTransferReader : BaseObject, IFastTransferReader, IFastTransferDataInterface, IDisposable
	{
		// Token: 0x06000718 RID: 1816 RVA: 0x00018FB8 File Offset: 0x000171B8
		internal FastTransferReader(ArraySegment<byte> buffer)
		{
			this.reader = Reader.CreateBufferReader(buffer);
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x00018FCC File Offset: 0x000171CC
		internal FastTransferReader(Reader reader)
		{
			this.reader = reader;
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x00018FDB File Offset: 0x000171DB
		public bool IsDataAvailable
		{
			get
			{
				return this.AvailableBufferSize > 0;
			}
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x00018FE8 File Offset: 0x000171E8
		public bool TryPeekMarker(out PropertyTag propertyTag)
		{
			if (this.AvailableBufferSize >= 4)
			{
				int num;
				propertyTag = new PropertyTag(FastTransferReader.NormalizeTag(this.reader.PeekUInt32(0L), out num));
				return propertyTag.IsMarker;
			}
			throw new BufferParseException(string.Format("The buffer has residual data, which cannot be parsed. Residual data size = {0}.", this.AvailableBufferSize));
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x00019040 File Offset: 0x00017240
		public void ReadMarker(PropertyTag expectedMarker)
		{
			if (!expectedMarker.IsMarker)
			{
				throw new ArgumentException("Should be a marker", "expectedMarker");
			}
			int num;
			PropertyTag propertyTag = new PropertyTag(FastTransferReader.NormalizeTag(this.reader.ReadUInt32(), out num));
			this.OnAtomRead();
			if (propertyTag != expectedMarker)
			{
				throw new RopExecutionException(string.Format("Unexpected marker found: {0}. Expected: {1}", propertyTag, expectedMarker), ErrorCode.FxUnexpectedMarker);
			}
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x000190B0 File Offset: 0x000172B0
		public PropertyTag ReadPropertyInfo(out NamedProperty namedProperty, out int codePage)
		{
			PropertyTag propertyTag = new PropertyTag(FastTransferReader.NormalizeTag(this.reader.ReadUInt32(), out codePage));
			this.OnAtomRead();
			if (propertyTag.IsMarker && !propertyTag.IsMetaProperty)
			{
				throw new RopExecutionException(string.Format("Marker was not expected: {0}", propertyTag), ErrorCode.FxUnexpectedMarker);
			}
			if (propertyTag.IsNamedProperty)
			{
				Guid guid = this.reader.ReadGuid();
				NamedPropertyKind namedPropertyKind = (NamedPropertyKind)this.reader.ReadByte();
				switch (namedPropertyKind)
				{
				case NamedPropertyKind.Id:
				{
					uint id = this.reader.ReadUInt32();
					namedProperty = new NamedProperty(guid, id);
					break;
				}
				case NamedPropertyKind.String:
				{
					string name = this.reader.ReadUnicodeString(StringFlags.IncludeNull);
					namedProperty = new NamedProperty(guid, name);
					break;
				}
				default:
					throw new BufferParseException(string.Format("Unrecognized Kind \"{0}\" for the named property {1}.", namedPropertyKind, propertyTag));
				}
			}
			else
			{
				namedProperty = null;
			}
			return propertyTag;
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00019190 File Offset: 0x00017390
		public PropertyValue ReadAndParseFixedSizeValue(PropertyTag propertyTag)
		{
			PropertyType propertyType = propertyTag.PropertyType;
			PropertyValue result;
			if (propertyType == PropertyType.Bool)
			{
				result = new PropertyValue(propertyTag, 0 != this.reader.ReadInt16());
			}
			else
			{
				result = this.reader.ReadPropertyValueForTag(propertyTag, WireFormatStyle.Rop);
			}
			this.OnAtomRead();
			return result;
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x000191E0 File Offset: 0x000173E0
		public int ReadLength(int maxValue)
		{
			int num = this.reader.ReadInt32();
			this.OnAtomRead();
			if (num < 0 || num > maxValue)
			{
				throw new RopExecutionException(string.Format("The length is not in the range. Length = {0}, maxValue = {1}.", num, maxValue), (ErrorCode)2147746565U);
			}
			return num;
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x0001922C File Offset: 0x0001742C
		public ArraySegment<byte> ReadVariableSizeValue(int maxToRead)
		{
			if (maxToRead < 0)
			{
				throw new ArgumentOutOfRangeException("sizeToRead");
			}
			int count = Math.Min(this.AvailableBufferSize, maxToRead);
			this.atomsReadSinceLastChangeOfState++;
			return this.reader.ReadArraySegment((uint)count);
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0001926F File Offset: 0x0001746F
		public void NotifyCanSplitBuffers()
		{
			this.atomsReadSinceLastChangeOfState = 0;
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00019278 File Offset: 0x00017478
		public override string ToString()
		{
			return string.Format("FX Reader: {0}/{1}", this.reader.Position, this.reader.Length);
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x000192A4 File Offset: 0x000174A4
		protected override void InternalDispose()
		{
			this.reader.Dispose();
			base.InternalDispose();
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x000192B8 File Offset: 0x000174B8
		internal static uint NormalizeTag(uint rawPropertyTag, out int codePage)
		{
			uint num = rawPropertyTag;
			codePage = 4095;
			if ((rawPropertyTag & 32768U) != 0U)
			{
				codePage = CodePagePropertyTypeTranslator.PropertyTagEncodedCodePageToCodePage((int)(rawPropertyTag & (rawPropertyTag & 4095U)));
				num = (rawPropertyTag & 4294930432U);
				if ((long)codePage == 1200L)
				{
					num |= 31U;
				}
				else
				{
					num |= 30U;
				}
			}
			return num;
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00019307 File Offset: 0x00017507
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<FastTransferReader>(this);
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000726 RID: 1830 RVA: 0x0001930F File Offset: 0x0001750F
		private int AvailableBufferSize
		{
			get
			{
				return (int)(this.reader.Length - this.reader.Position);
			}
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x00019329 File Offset: 0x00017529
		private void OnAtomRead()
		{
			this.atomsReadSinceLastChangeOfState++;
			if (this.atomsReadSinceLastChangeOfState > 1)
			{
				throw new InvalidOperationException("At most one atom can be read between changes of states");
			}
		}

		// Token: 0x0400038F RID: 911
		private readonly Reader reader;

		// Token: 0x04000390 RID: 912
		private int atomsReadSinceLastChangeOfState;
	}
}
