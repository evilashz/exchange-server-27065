using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000008 RID: 8
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class BinaryDeserializer : DisposeTrackableBase
	{
		// Token: 0x0600001D RID: 29 RVA: 0x00002684 File Offset: 0x00000884
		public BinaryDeserializer(byte[] buffer)
		{
			if (buffer != null)
			{
				this.stream = new MemoryStream(buffer);
			}
			else
			{
				this.stream = new MemoryStream();
			}
			this.stream.Position = 0L;
			this.reader = new BinaryReader(this.stream);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000026D4 File Offset: 0x000008D4
		public BinaryDeserializer(ArraySegment<byte> segment)
		{
			this.stream = new MemoryStream(segment.Array, segment.Offset, segment.Count);
			this.stream.Position = 0L;
			this.reader = new BinaryReader(this.stream);
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002725 File Offset: 0x00000925
		public BinaryReader Reader
		{
			get
			{
				return this.reader;
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000272D File Offset: 0x0000092D
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.stream != null)
				{
					this.stream.Dispose();
					this.stream = null;
				}
				if (this.reader != null)
				{
					this.reader.Dispose();
					this.reader = null;
				}
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002766 File Offset: 0x00000966
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<BinaryDeserializer>(this);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002770 File Offset: 0x00000970
		public T[] ReadArray<T>(Func<BinaryDeserializer, T> deserializeOneElement)
		{
			int num = this.ReadInt();
			if (num < 0 || (long)num > this.stream.Length - this.stream.Position || num > 20000)
			{
				MapiExceptionHelper.ThrowIfError("Invalid serialized array size", -2147024809);
			}
			T[] array = new T[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = deserializeOneElement(this);
			}
			return array;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000027DC File Offset: 0x000009DC
		public int ReadInt()
		{
			return this.reader.ReadInt32();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000027E9 File Offset: 0x000009E9
		public Guid ReadGuid()
		{
			return new Guid(this.reader.ReadBytes(16));
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000027FD File Offset: 0x000009FD
		public string ReadString()
		{
			return this.reader.ReadString();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000280A File Offset: 0x00000A0A
		public ulong ReadUInt64()
		{
			return this.reader.ReadUInt64();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002818 File Offset: 0x00000A18
		public byte[] ReadBytes()
		{
			int num = this.ReadInt();
			if (num < 0 || (long)num > this.stream.Length - this.stream.Position)
			{
				MapiExceptionHelper.ThrowIfError("Invalid serialized byte array size", -2147024809);
			}
			return this.reader.ReadBytes(num);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002870 File Offset: 0x00000A70
		public PropValue ReadPropValue()
		{
			PropTag propTag = (PropTag)this.ReadInt();
			object value = null;
			PropType propType = propTag.ValueType();
			if (propType <= PropType.String)
			{
				if (propType <= PropType.Boolean)
				{
					if (propType == PropType.Int)
					{
						value = this.ReadInt();
						goto IL_151;
					}
					switch (propType)
					{
					case PropType.Error:
						value = this.ReadInt();
						goto IL_151;
					case PropType.Boolean:
						value = (this.ReadInt() != 0);
						goto IL_151;
					}
				}
				else
				{
					if (propType == PropType.Long)
					{
						value = (long)this.ReadUInt64();
						goto IL_151;
					}
					switch (propType)
					{
					case PropType.AnsiString:
					case PropType.String:
						value = this.ReadString();
						goto IL_151;
					}
				}
			}
			else if (propType <= PropType.Guid)
			{
				if (propType == PropType.SysTime)
				{
					long longValue = (long)this.ReadUInt64();
					value = PropValue.LongAsDateTime(longValue);
					goto IL_151;
				}
				if (propType == PropType.Guid)
				{
					value = this.ReadGuid();
					goto IL_151;
				}
			}
			else
			{
				if (propType == PropType.Binary)
				{
					value = this.ReadBytes();
					goto IL_151;
				}
				switch (propType)
				{
				case PropType.AnsiStringArray:
				case PropType.StringArray:
					value = this.ReadArray<string>((BinaryDeserializer binaryDeserializer) => binaryDeserializer.ReadString());
					goto IL_151;
				}
			}
			MapiExceptionHelper.ThrowIfError(string.Format("Unable to deserialize PropValue type {0}", propTag.ValueType()), -2147221246);
			IL_151:
			return new PropValue(propTag, value);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000029DD File Offset: 0x00000BDD
		public PropValue[] ReadPropValues()
		{
			return this.ReadArray<PropValue>((BinaryDeserializer deserializer) => deserializer.ReadPropValue());
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002A04 File Offset: 0x00000C04
		public static void Deserialize(byte[] request, BinaryDeserializer.DeserializeDelegate del)
		{
			if (request == null)
			{
				MapiExceptionHelper.ThrowIfError("Invalid serialized input data", -2147024809);
			}
			try
			{
				using (BinaryDeserializer binaryDeserializer = new BinaryDeserializer(request))
				{
					del(binaryDeserializer);
				}
			}
			catch (EndOfStreamException)
			{
				MapiExceptionHelper.ThrowIfError("Invalid serialized input data", -2147024809);
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002A70 File Offset: 0x00000C70
		public void Deserialize(BinaryDeserializer.DeserializeDelegate del)
		{
			try
			{
				del(this);
			}
			catch (EndOfStreamException)
			{
				MapiExceptionHelper.ThrowIfError("Invalid serialized input data", -2147024809);
			}
		}

		// Token: 0x0400001B RID: 27
		private MemoryStream stream;

		// Token: 0x0400001C RID: 28
		private BinaryReader reader;

		// Token: 0x02000009 RID: 9
		// (Invoke) Token: 0x0600002F RID: 47
		public delegate void DeserializeDelegate(BinaryDeserializer deserializer);
	}
}
