using System;
using System.IO;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Search.Core.Common
{
	// Token: 0x02000080 RID: 128
	internal sealed class SerializableStreamProperty : SerializableProperty<Stream>
	{
		// Token: 0x0600033F RID: 831 RVA: 0x0000ADDC File Offset: 0x00008FDC
		internal SerializableStreamProperty(SerializablePropertyId id, byte[] bytes) : base(id, new MemoryStream(bytes, false))
		{
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000ADEC File Offset: 0x00008FEC
		internal SerializableStreamProperty(BinaryReader reader)
		{
			this.reader = reader;
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0000ADFB File Offset: 0x00008FFB
		public override SerializablePropertyType Type
		{
			get
			{
				return SerializablePropertyType.Stream;
			}
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000AE00 File Offset: 0x00009000
		public void CopyTo(Stream outputStream)
		{
			Util.ThrowOnNullArgument(outputStream, "outputStream");
			if (this.reader == null)
			{
				throw new InvalidOperationException("No reader is initialized");
			}
			int num = this.reader.ReadInt32();
			BufferPool bufferPool = BufferPoolCollection.AutoCleanupCollection.Acquire(BufferPoolCollection.BufferSize.Size4K);
			byte[] array = bufferPool.Acquire();
			try
			{
				int num2;
				for (int i = num; i > 0; i -= num2)
				{
					num2 = this.reader.Read(array, 0, Math.Min(i, array.Length));
					if (num2 == 0)
					{
						throw new EndOfStreamException("unexpected end of stream");
					}
					outputStream.Write(array, 0, num2);
				}
			}
			finally
			{
				bufferPool.Release(array);
			}
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000AEA4 File Offset: 0x000090A4
		protected override void SerializeValue(BinaryWriter writer)
		{
			writer.Write((int)base.PropertyValue.Length);
			BufferPool bufferPool = BufferPoolCollection.AutoCleanupCollection.Acquire(BufferPoolCollection.BufferSize.Size4K);
			byte[] array = bufferPool.Acquire();
			try
			{
				int count;
				while ((count = base.PropertyValue.Read(array, 0, array.Length)) != 0)
				{
					writer.Write(array, 0, count);
				}
			}
			finally
			{
				bufferPool.Release(array);
			}
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000AF10 File Offset: 0x00009110
		protected override void DeserializeValue(BinaryReader reader)
		{
		}

		// Token: 0x04000171 RID: 369
		private readonly BinaryReader reader;
	}
}
