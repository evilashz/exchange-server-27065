using System;
using System.Text;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200041C RID: 1052
	internal class FieldMetadata
	{
		// Token: 0x0600351F RID: 13599 RVA: 0x000CE006 File Offset: 0x000CC206
		public FieldMetadata(string name, TraceLoggingDataType type, EventFieldTags tags, bool variableCount) : this(name, type, tags, variableCount ? 64 : 0, 0, null)
		{
		}

		// Token: 0x06003520 RID: 13600 RVA: 0x000CE01C File Offset: 0x000CC21C
		public FieldMetadata(string name, TraceLoggingDataType type, EventFieldTags tags, ushort fixedCount) : this(name, type, tags, 32, fixedCount, null)
		{
		}

		// Token: 0x06003521 RID: 13601 RVA: 0x000CE02C File Offset: 0x000CC22C
		public FieldMetadata(string name, TraceLoggingDataType type, EventFieldTags tags, byte[] custom) : this(name, type, tags, 96, checked((ushort)((custom == null) ? 0 : custom.Length)), custom)
		{
		}

		// Token: 0x06003522 RID: 13602 RVA: 0x000CE048 File Offset: 0x000CC248
		private FieldMetadata(string name, TraceLoggingDataType dataType, EventFieldTags tags, byte countFlags, ushort fixedCount = 0, byte[] custom = null)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name", "This usually means that the object passed to Write is of a type that does not support being used as the top-level object in an event, e.g. a primitive or built-in type.");
			}
			Statics.CheckName(name);
			int num = (int)(dataType & (TraceLoggingDataType)31);
			this.name = name;
			this.nameSize = Encoding.UTF8.GetByteCount(this.name) + 1;
			this.inType = (byte)(num | (int)countFlags);
			this.outType = (byte)(dataType >> 8 & (TraceLoggingDataType)127);
			this.tags = tags;
			this.fixedCount = fixedCount;
			this.custom = custom;
			if (countFlags != 0)
			{
				if (num == 0)
				{
					throw new NotSupportedException(Environment.GetResourceString("EventSource_NotSupportedArrayOfNil"));
				}
				if (num == 14)
				{
					throw new NotSupportedException(Environment.GetResourceString("EventSource_NotSupportedArrayOfBinary"));
				}
				if (num == 1 || num == 2)
				{
					throw new NotSupportedException(Environment.GetResourceString("EventSource_NotSupportedArrayOfNullTerminatedString"));
				}
			}
			if ((this.tags & (EventFieldTags)268435455) != EventFieldTags.None)
			{
				this.outType |= 128;
			}
			if (this.outType != 0)
			{
				this.inType |= 128;
			}
		}

		// Token: 0x06003523 RID: 13603 RVA: 0x000CE147 File Offset: 0x000CC347
		public void IncrementStructFieldCount()
		{
			this.inType |= 128;
			this.outType += 1;
			if ((this.outType & 127) == 0)
			{
				throw new NotSupportedException(Environment.GetResourceString("EventSource_TooManyFields"));
			}
		}

		// Token: 0x06003524 RID: 13604 RVA: 0x000CE188 File Offset: 0x000CC388
		public void Encode(ref int pos, byte[] metadata)
		{
			if (metadata != null)
			{
				Encoding.UTF8.GetBytes(this.name, 0, this.name.Length, metadata, pos);
			}
			pos += this.nameSize;
			if (metadata != null)
			{
				metadata[pos] = this.inType;
			}
			pos++;
			if ((this.inType & 128) != 0)
			{
				if (metadata != null)
				{
					metadata[pos] = this.outType;
				}
				pos++;
				if ((this.outType & 128) != 0)
				{
					Statics.EncodeTags((int)this.tags, ref pos, metadata);
				}
			}
			if ((this.inType & 32) != 0)
			{
				if (metadata != null)
				{
					metadata[pos] = (byte)this.fixedCount;
					metadata[pos + 1] = (byte)(this.fixedCount >> 8);
				}
				pos += 2;
				if (96 == (this.inType & 96) && this.fixedCount != 0)
				{
					if (metadata != null)
					{
						Buffer.BlockCopy(this.custom, 0, metadata, pos, (int)this.fixedCount);
					}
					pos += (int)this.fixedCount;
				}
			}
		}

		// Token: 0x04001787 RID: 6023
		private readonly string name;

		// Token: 0x04001788 RID: 6024
		private readonly int nameSize;

		// Token: 0x04001789 RID: 6025
		private readonly EventFieldTags tags;

		// Token: 0x0400178A RID: 6026
		private readonly byte[] custom;

		// Token: 0x0400178B RID: 6027
		private readonly ushort fixedCount;

		// Token: 0x0400178C RID: 6028
		private byte inType;

		// Token: 0x0400178D RID: 6029
		private byte outType;
	}
}
