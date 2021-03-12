using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.OAB
{
	// Token: 0x02000142 RID: 322
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class BinaryReaderExtensions
	{
		// Token: 0x06000D38 RID: 3384 RVA: 0x00037AF0 File Offset: 0x00035CF0
		public static ushort ReadUInt16(this BinaryReader reader, string elementName)
		{
			return BinaryReaderExtensions.ReadAndHandleExceptions<ushort>(() => reader.ReadUInt16(), reader.BaseStream.Position, elementName);
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x00037B44 File Offset: 0x00035D44
		public static uint ReadUInt32(this BinaryReader reader, string elementName)
		{
			return BinaryReaderExtensions.ReadAndHandleExceptions<uint>(() => reader.ReadUInt32(), reader.BaseStream.Position, elementName);
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x00037B98 File Offset: 0x00035D98
		public static ulong ReadUInt64(this BinaryReader reader, string elementName)
		{
			return BinaryReaderExtensions.ReadAndHandleExceptions<ulong>(() => reader.ReadUInt64(), reader.BaseStream.Position, elementName);
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x00037BEC File Offset: 0x00035DEC
		public static byte ReadByte(this BinaryReader reader, string elementName)
		{
			return BinaryReaderExtensions.ReadAndHandleExceptions<byte>(() => reader.ReadByte(), reader.BaseStream.Position, elementName);
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x00037C28 File Offset: 0x00035E28
		public static Guid ReadGuid(this BinaryReader reader, string elementName)
		{
			byte[] b = reader.ReadBytes(16, elementName);
			return new Guid(b);
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x00037C60 File Offset: 0x00035E60
		public static byte[] ReadBytes(this BinaryReader reader, int count, string elementName)
		{
			long position = reader.BaseStream.Position;
			byte[] array = BinaryReaderExtensions.ReadAndHandleExceptions<byte[]>(() => reader.ReadBytes(count), position, elementName);
			if (array.Length < count)
			{
				throw new InvalidDataException(string.Format("Unable to completely read '{0}' element from stream at position {1}, only {2} were read instead of {3}", new object[]
				{
					elementName,
					position,
					array.Length,
					count
				}));
			}
			return array;
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x00037CF0 File Offset: 0x00035EF0
		private static T ReadAndHandleExceptions<T>(Func<T> read, long position, string elementName)
		{
			T result;
			try
			{
				result = read();
			}
			catch (EndOfStreamException innerException)
			{
				throw new InvalidDataException(string.Format("Unexpected end of stream when reading '{0}' element from stream at position {1}", elementName, position), innerException);
			}
			return result;
		}
	}
}
