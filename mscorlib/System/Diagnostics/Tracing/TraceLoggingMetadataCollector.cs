using System;
using System.Collections.Generic;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000458 RID: 1112
	internal class TraceLoggingMetadataCollector
	{
		// Token: 0x06003625 RID: 13861 RVA: 0x000D02D3 File Offset: 0x000CE4D3
		internal TraceLoggingMetadataCollector()
		{
			this.impl = new TraceLoggingMetadataCollector.Impl();
		}

		// Token: 0x06003626 RID: 13862 RVA: 0x000D02F1 File Offset: 0x000CE4F1
		private TraceLoggingMetadataCollector(TraceLoggingMetadataCollector other, FieldMetadata group)
		{
			this.impl = other.impl;
			this.currentGroup = group;
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06003627 RID: 13863 RVA: 0x000D0317 File Offset: 0x000CE517
		// (set) Token: 0x06003628 RID: 13864 RVA: 0x000D031F File Offset: 0x000CE51F
		internal EventFieldTags Tags { get; set; }

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06003629 RID: 13865 RVA: 0x000D0328 File Offset: 0x000CE528
		internal int ScratchSize
		{
			get
			{
				return (int)this.impl.scratchSize;
			}
		}

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x0600362A RID: 13866 RVA: 0x000D0335 File Offset: 0x000CE535
		internal int DataCount
		{
			get
			{
				return (int)this.impl.dataCount;
			}
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x0600362B RID: 13867 RVA: 0x000D0342 File Offset: 0x000CE542
		internal int PinCount
		{
			get
			{
				return (int)this.impl.pinCount;
			}
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x0600362C RID: 13868 RVA: 0x000D034F File Offset: 0x000CE54F
		private bool BeginningBufferedArray
		{
			get
			{
				return this.bufferedArrayFieldCount == 0;
			}
		}

		// Token: 0x0600362D RID: 13869 RVA: 0x000D035C File Offset: 0x000CE55C
		public TraceLoggingMetadataCollector AddGroup(string name)
		{
			TraceLoggingMetadataCollector result = this;
			if (name != null || this.BeginningBufferedArray)
			{
				FieldMetadata fieldMetadata = new FieldMetadata(name, TraceLoggingDataType.Struct, this.Tags, this.BeginningBufferedArray);
				this.AddField(fieldMetadata);
				result = new TraceLoggingMetadataCollector(this, fieldMetadata);
			}
			return result;
		}

		// Token: 0x0600362E RID: 13870 RVA: 0x000D039C File Offset: 0x000CE59C
		public void AddScalar(string name, TraceLoggingDataType type)
		{
			TraceLoggingDataType traceLoggingDataType = type & (TraceLoggingDataType)31;
			int size;
			switch (traceLoggingDataType)
			{
			case TraceLoggingDataType.Int8:
			case TraceLoggingDataType.UInt8:
				break;
			case TraceLoggingDataType.Int16:
			case TraceLoggingDataType.UInt16:
				goto IL_6F;
			case TraceLoggingDataType.Int32:
			case TraceLoggingDataType.UInt32:
			case TraceLoggingDataType.Float:
			case TraceLoggingDataType.Boolean32:
			case TraceLoggingDataType.HexInt32:
				size = 4;
				goto IL_8B;
			case TraceLoggingDataType.Int64:
			case TraceLoggingDataType.UInt64:
			case TraceLoggingDataType.Double:
			case TraceLoggingDataType.FileTime:
			case TraceLoggingDataType.HexInt64:
				size = 8;
				goto IL_8B;
			case TraceLoggingDataType.Binary:
			case (TraceLoggingDataType)16:
			case (TraceLoggingDataType)19:
				goto IL_80;
			case TraceLoggingDataType.Guid:
			case TraceLoggingDataType.SystemTime:
				size = 16;
				goto IL_8B;
			default:
				if (traceLoggingDataType != TraceLoggingDataType.Char8)
				{
					if (traceLoggingDataType != TraceLoggingDataType.Char16)
					{
						goto IL_80;
					}
					goto IL_6F;
				}
				break;
			}
			size = 1;
			goto IL_8B;
			IL_6F:
			size = 2;
			goto IL_8B;
			IL_80:
			throw new ArgumentOutOfRangeException("type");
			IL_8B:
			this.impl.AddScalar(size);
			this.AddField(new FieldMetadata(name, type, this.Tags, this.BeginningBufferedArray));
		}

		// Token: 0x0600362F RID: 13871 RVA: 0x000D045C File Offset: 0x000CE65C
		public void AddBinary(string name, TraceLoggingDataType type)
		{
			TraceLoggingDataType traceLoggingDataType = type & (TraceLoggingDataType)31;
			if (traceLoggingDataType != TraceLoggingDataType.Binary && traceLoggingDataType - TraceLoggingDataType.CountedUtf16String > 1)
			{
				throw new ArgumentOutOfRangeException("type");
			}
			this.impl.AddScalar(2);
			this.impl.AddNonscalar();
			this.AddField(new FieldMetadata(name, type, this.Tags, this.BeginningBufferedArray));
		}

		// Token: 0x06003630 RID: 13872 RVA: 0x000D04B8 File Offset: 0x000CE6B8
		public void AddArray(string name, TraceLoggingDataType type)
		{
			TraceLoggingDataType traceLoggingDataType = type & (TraceLoggingDataType)31;
			switch (traceLoggingDataType)
			{
			case TraceLoggingDataType.Utf16String:
			case TraceLoggingDataType.MbcsString:
			case TraceLoggingDataType.Int8:
			case TraceLoggingDataType.UInt8:
			case TraceLoggingDataType.Int16:
			case TraceLoggingDataType.UInt16:
			case TraceLoggingDataType.Int32:
			case TraceLoggingDataType.UInt32:
			case TraceLoggingDataType.Int64:
			case TraceLoggingDataType.UInt64:
			case TraceLoggingDataType.Float:
			case TraceLoggingDataType.Double:
			case TraceLoggingDataType.Boolean32:
			case TraceLoggingDataType.Guid:
			case TraceLoggingDataType.FileTime:
			case TraceLoggingDataType.HexInt32:
			case TraceLoggingDataType.HexInt64:
				goto IL_7C;
			case TraceLoggingDataType.Binary:
			case (TraceLoggingDataType)16:
			case TraceLoggingDataType.SystemTime:
			case (TraceLoggingDataType)19:
				break;
			default:
				if (traceLoggingDataType == TraceLoggingDataType.Char8 || traceLoggingDataType == TraceLoggingDataType.Char16)
				{
					goto IL_7C;
				}
				break;
			}
			throw new ArgumentOutOfRangeException("type");
			IL_7C:
			if (this.BeginningBufferedArray)
			{
				throw new NotSupportedException(Environment.GetResourceString("EventSource_NotSupportedNestedArraysEnums"));
			}
			this.impl.AddScalar(2);
			this.impl.AddNonscalar();
			this.AddField(new FieldMetadata(name, type, this.Tags, true));
		}

		// Token: 0x06003631 RID: 13873 RVA: 0x000D0584 File Offset: 0x000CE784
		public void BeginBufferedArray()
		{
			if (this.bufferedArrayFieldCount >= 0)
			{
				throw new NotSupportedException(Environment.GetResourceString("EventSource_NotSupportedNestedArraysEnums"));
			}
			this.bufferedArrayFieldCount = 0;
			this.impl.BeginBuffered();
		}

		// Token: 0x06003632 RID: 13874 RVA: 0x000D05B1 File Offset: 0x000CE7B1
		public void EndBufferedArray()
		{
			if (this.bufferedArrayFieldCount != 1)
			{
				throw new InvalidOperationException(Environment.GetResourceString("EventSource_IncorrentlyAuthoredTypeInfo"));
			}
			this.bufferedArrayFieldCount = int.MinValue;
			this.impl.EndBuffered();
		}

		// Token: 0x06003633 RID: 13875 RVA: 0x000D05E4 File Offset: 0x000CE7E4
		public void AddCustom(string name, TraceLoggingDataType type, byte[] metadata)
		{
			if (this.BeginningBufferedArray)
			{
				throw new NotSupportedException(Environment.GetResourceString("EventSource_NotSupportedCustomSerializedData"));
			}
			this.impl.AddScalar(2);
			this.impl.AddNonscalar();
			this.AddField(new FieldMetadata(name, type, this.Tags, metadata));
		}

		// Token: 0x06003634 RID: 13876 RVA: 0x000D0634 File Offset: 0x000CE834
		internal byte[] GetMetadata()
		{
			int num = this.impl.Encode(null);
			byte[] array = new byte[num];
			this.impl.Encode(array);
			return array;
		}

		// Token: 0x06003635 RID: 13877 RVA: 0x000D0663 File Offset: 0x000CE863
		private void AddField(FieldMetadata fieldMetadata)
		{
			this.Tags = EventFieldTags.None;
			this.bufferedArrayFieldCount++;
			this.impl.fields.Add(fieldMetadata);
			if (this.currentGroup != null)
			{
				this.currentGroup.IncrementStructFieldCount();
			}
		}

		// Token: 0x040017E6 RID: 6118
		private readonly TraceLoggingMetadataCollector.Impl impl;

		// Token: 0x040017E7 RID: 6119
		private readonly FieldMetadata currentGroup;

		// Token: 0x040017E8 RID: 6120
		private int bufferedArrayFieldCount = int.MinValue;

		// Token: 0x02000B69 RID: 2921
		private class Impl
		{
			// Token: 0x06006B7A RID: 27514 RVA: 0x00173371 File Offset: 0x00171571
			public void AddScalar(int size)
			{
				checked
				{
					if (this.bufferNesting == 0)
					{
						if (!this.scalar)
						{
							this.dataCount += 1;
						}
						this.scalar = true;
						this.scratchSize = (short)((int)this.scratchSize + size);
					}
				}
			}

			// Token: 0x06006B7B RID: 27515 RVA: 0x001733A8 File Offset: 0x001715A8
			public void AddNonscalar()
			{
				checked
				{
					if (this.bufferNesting == 0)
					{
						this.scalar = false;
						this.pinCount += 1;
						this.dataCount += 1;
					}
				}
			}

			// Token: 0x06006B7C RID: 27516 RVA: 0x001733D7 File Offset: 0x001715D7
			public void BeginBuffered()
			{
				if (this.bufferNesting == 0)
				{
					this.AddNonscalar();
				}
				this.bufferNesting++;
			}

			// Token: 0x06006B7D RID: 27517 RVA: 0x001733F5 File Offset: 0x001715F5
			public void EndBuffered()
			{
				this.bufferNesting--;
			}

			// Token: 0x06006B7E RID: 27518 RVA: 0x00173408 File Offset: 0x00171608
			public int Encode(byte[] metadata)
			{
				int result = 0;
				foreach (FieldMetadata fieldMetadata in this.fields)
				{
					fieldMetadata.Encode(ref result, metadata);
				}
				return result;
			}

			// Token: 0x04003448 RID: 13384
			internal readonly List<FieldMetadata> fields = new List<FieldMetadata>();

			// Token: 0x04003449 RID: 13385
			internal short scratchSize;

			// Token: 0x0400344A RID: 13386
			internal sbyte dataCount;

			// Token: 0x0400344B RID: 13387
			internal sbyte pinCount;

			// Token: 0x0400344C RID: 13388
			private int bufferNesting;

			// Token: 0x0400344D RID: 13389
			private bool scalar;
		}
	}
}
