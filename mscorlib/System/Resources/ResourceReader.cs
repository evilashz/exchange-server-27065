using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Text;

namespace System.Resources
{
	// Token: 0x0200036C RID: 876
	[ComVisible(true)]
	public sealed class ResourceReader : IResourceReader, IEnumerable, IDisposable
	{
		// Token: 0x06002C26 RID: 11302 RVA: 0x000A7094 File Offset: 0x000A5294
		[SecuritySafeCritical]
		public ResourceReader(string fileName)
		{
			this._resCache = new Dictionary<string, ResourceLocator>(FastResourceComparer.Default);
			this._store = new BinaryReader(new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.RandomAccess, Path.GetFileName(fileName), false), Encoding.UTF8);
			try
			{
				this.ReadResources();
			}
			catch
			{
				this._store.Close();
				throw;
			}
		}

		// Token: 0x06002C27 RID: 11303 RVA: 0x000A7108 File Offset: 0x000A5308
		[SecurityCritical]
		public ResourceReader(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_StreamNotReadable"));
			}
			this._resCache = new Dictionary<string, ResourceLocator>(FastResourceComparer.Default);
			this._store = new BinaryReader(stream, Encoding.UTF8);
			this._ums = (stream as UnmanagedMemoryStream);
			this.ReadResources();
		}

		// Token: 0x06002C28 RID: 11304 RVA: 0x000A7174 File Offset: 0x000A5374
		[SecurityCritical]
		internal ResourceReader(Stream stream, Dictionary<string, ResourceLocator> resCache)
		{
			this._resCache = resCache;
			this._store = new BinaryReader(stream, Encoding.UTF8);
			this._ums = (stream as UnmanagedMemoryStream);
			this.ReadResources();
		}

		// Token: 0x06002C29 RID: 11305 RVA: 0x000A71A6 File Offset: 0x000A53A6
		public void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x06002C2A RID: 11306 RVA: 0x000A71AF File Offset: 0x000A53AF
		public void Dispose()
		{
			this.Close();
		}

		// Token: 0x06002C2B RID: 11307 RVA: 0x000A71B8 File Offset: 0x000A53B8
		[SecuritySafeCritical]
		private void Dispose(bool disposing)
		{
			if (this._store != null)
			{
				this._resCache = null;
				if (disposing)
				{
					BinaryReader store = this._store;
					this._store = null;
					if (store != null)
					{
						store.Close();
					}
				}
				this._store = null;
				this._namePositions = null;
				this._nameHashes = null;
				this._ums = null;
				this._namePositionsPtr = null;
				this._nameHashesPtr = null;
			}
		}

		// Token: 0x06002C2C RID: 11308 RVA: 0x000A721C File Offset: 0x000A541C
		[SecurityCritical]
		internal unsafe static int ReadUnalignedI4(int* p)
		{
			return (int)(*(byte*)p) | (int)((byte*)p)[1] << 8 | (int)((byte*)p)[2] << 16 | (int)((byte*)p)[3] << 24;
		}

		// Token: 0x06002C2D RID: 11309 RVA: 0x000A7244 File Offset: 0x000A5444
		private void SkipInt32()
		{
			this._store.BaseStream.Seek(4L, SeekOrigin.Current);
		}

		// Token: 0x06002C2E RID: 11310 RVA: 0x000A725C File Offset: 0x000A545C
		private void SkipString()
		{
			int num = this._store.Read7BitEncodedInt();
			if (num < 0)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_NegativeStringLength"));
			}
			this._store.BaseStream.Seek((long)num, SeekOrigin.Current);
		}

		// Token: 0x06002C2F RID: 11311 RVA: 0x000A729D File Offset: 0x000A549D
		[SecuritySafeCritical]
		private int GetNameHash(int index)
		{
			if (this._ums == null)
			{
				return this._nameHashes[index];
			}
			return ResourceReader.ReadUnalignedI4(this._nameHashesPtr + index);
		}

		// Token: 0x06002C30 RID: 11312 RVA: 0x000A72C4 File Offset: 0x000A54C4
		[SecuritySafeCritical]
		private int GetNamePosition(int index)
		{
			int num;
			if (this._ums == null)
			{
				num = this._namePositions[index];
			}
			else
			{
				num = ResourceReader.ReadUnalignedI4(this._namePositionsPtr + index);
			}
			if (num < 0 || (long)num > this._dataSectionOffset - this._nameSectionOffset)
			{
				throw new FormatException(Environment.GetResourceString("BadImageFormat_ResourcesNameInvalidOffset", new object[]
				{
					num
				}));
			}
			return num;
		}

		// Token: 0x06002C31 RID: 11313 RVA: 0x000A732B File Offset: 0x000A552B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06002C32 RID: 11314 RVA: 0x000A7333 File Offset: 0x000A5533
		public IDictionaryEnumerator GetEnumerator()
		{
			if (this._resCache == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("ResourceReaderIsClosed"));
			}
			return new ResourceReader.ResourceEnumerator(this);
		}

		// Token: 0x06002C33 RID: 11315 RVA: 0x000A7353 File Offset: 0x000A5553
		internal ResourceReader.ResourceEnumerator GetEnumeratorInternal()
		{
			return new ResourceReader.ResourceEnumerator(this);
		}

		// Token: 0x06002C34 RID: 11316 RVA: 0x000A735C File Offset: 0x000A555C
		internal int FindPosForResource(string name)
		{
			int num = FastResourceComparer.HashFunction(name);
			int i = 0;
			int num2 = this._numResources - 1;
			int num3 = -1;
			bool flag = false;
			while (i <= num2)
			{
				num3 = i + num2 >> 1;
				int nameHash = this.GetNameHash(num3);
				int num4;
				if (nameHash == num)
				{
					num4 = 0;
				}
				else if (nameHash < num)
				{
					num4 = -1;
				}
				else
				{
					num4 = 1;
				}
				if (num4 == 0)
				{
					flag = true;
					break;
				}
				if (num4 < 0)
				{
					i = num3 + 1;
				}
				else
				{
					num2 = num3 - 1;
				}
			}
			if (!flag)
			{
				return -1;
			}
			if (i != num3)
			{
				i = num3;
				while (i > 0 && this.GetNameHash(i - 1) == num)
				{
					i--;
				}
			}
			if (num2 != num3)
			{
				num2 = num3;
				while (num2 < this._numResources - 1 && this.GetNameHash(num2 + 1) == num)
				{
					num2++;
				}
			}
			lock (this)
			{
				int j = i;
				while (j <= num2)
				{
					this._store.BaseStream.Seek(this._nameSectionOffset + (long)this.GetNamePosition(j), SeekOrigin.Begin);
					if (this.CompareStringEqualsName(name))
					{
						int num5 = this._store.ReadInt32();
						if (num5 < 0 || (long)num5 >= this._store.BaseStream.Length - this._dataSectionOffset)
						{
							throw new FormatException(Environment.GetResourceString("BadImageFormat_ResourcesDataInvalidOffset", new object[]
							{
								num5
							}));
						}
						return num5;
					}
					else
					{
						j++;
					}
				}
			}
			return -1;
		}

		// Token: 0x06002C35 RID: 11317 RVA: 0x000A74D0 File Offset: 0x000A56D0
		[SecuritySafeCritical]
		private unsafe bool CompareStringEqualsName(string name)
		{
			int num = this._store.Read7BitEncodedInt();
			if (num < 0)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_NegativeStringLength"));
			}
			if (this._ums == null)
			{
				byte[] array = new byte[num];
				int num2;
				for (int i = num; i > 0; i -= num2)
				{
					num2 = this._store.Read(array, num - i, i);
					if (num2 == 0)
					{
						throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourceNameCorrupted"));
					}
				}
				return FastResourceComparer.CompareOrdinal(array, num / 2, name) == 0;
			}
			byte* positionPointer = this._ums.PositionPointer;
			this._ums.Seek((long)num, SeekOrigin.Current);
			if (this._ums.Position > this._ums.Length)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesNameTooLong"));
			}
			return FastResourceComparer.CompareOrdinal(positionPointer, num, name) == 0;
		}

		// Token: 0x06002C36 RID: 11318 RVA: 0x000A759C File Offset: 0x000A579C
		[SecurityCritical]
		private unsafe string AllocateStringForNameIndex(int index, out int dataOffset)
		{
			long num = (long)this.GetNamePosition(index);
			int num2;
			byte[] array;
			lock (this)
			{
				this._store.BaseStream.Seek(num + this._nameSectionOffset, SeekOrigin.Begin);
				num2 = this._store.Read7BitEncodedInt();
				if (num2 < 0)
				{
					throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_NegativeStringLength"));
				}
				if (this._ums != null)
				{
					if (this._ums.Position > this._ums.Length - (long)num2)
					{
						throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesIndexTooLong", new object[]
						{
							index
						}));
					}
					char* positionPointer = (char*)this._ums.PositionPointer;
					string result = new string(positionPointer, 0, num2 / 2);
					this._ums.Position += (long)num2;
					dataOffset = this._store.ReadInt32();
					if (dataOffset < 0 || (long)dataOffset >= this._store.BaseStream.Length - this._dataSectionOffset)
					{
						throw new FormatException(Environment.GetResourceString("BadImageFormat_ResourcesDataInvalidOffset", new object[]
						{
							dataOffset
						}));
					}
					return result;
				}
				else
				{
					array = new byte[num2];
					int num3;
					for (int i = num2; i > 0; i -= num3)
					{
						num3 = this._store.Read(array, num2 - i, i);
						if (num3 == 0)
						{
							throw new EndOfStreamException(Environment.GetResourceString("BadImageFormat_ResourceNameCorrupted_NameIndex", new object[]
							{
								index
							}));
						}
					}
					dataOffset = this._store.ReadInt32();
					if (dataOffset < 0 || (long)dataOffset >= this._store.BaseStream.Length - this._dataSectionOffset)
					{
						throw new FormatException(Environment.GetResourceString("BadImageFormat_ResourcesDataInvalidOffset", new object[]
						{
							dataOffset
						}));
					}
				}
			}
			return Encoding.Unicode.GetString(array, 0, num2);
		}

		// Token: 0x06002C37 RID: 11319 RVA: 0x000A779C File Offset: 0x000A599C
		private object GetValueForNameIndex(int index)
		{
			long num = (long)this.GetNamePosition(index);
			object result;
			lock (this)
			{
				this._store.BaseStream.Seek(num + this._nameSectionOffset, SeekOrigin.Begin);
				this.SkipString();
				int num2 = this._store.ReadInt32();
				if (num2 < 0 || (long)num2 >= this._store.BaseStream.Length - this._dataSectionOffset)
				{
					throw new FormatException(Environment.GetResourceString("BadImageFormat_ResourcesDataInvalidOffset", new object[]
					{
						num2
					}));
				}
				if (this._version == 1)
				{
					result = this.LoadObjectV1(num2);
				}
				else
				{
					ResourceTypeCode resourceTypeCode;
					result = this.LoadObjectV2(num2, out resourceTypeCode);
				}
			}
			return result;
		}

		// Token: 0x06002C38 RID: 11320 RVA: 0x000A7868 File Offset: 0x000A5A68
		internal string LoadString(int pos)
		{
			this._store.BaseStream.Seek(this._dataSectionOffset + (long)pos, SeekOrigin.Begin);
			string result = null;
			int num = this._store.Read7BitEncodedInt();
			if (this._version == 1)
			{
				if (num == -1)
				{
					return null;
				}
				if (this.FindType(num) != typeof(string))
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResourceNotString_Type", new object[]
					{
						this.FindType(num).FullName
					}));
				}
				result = this._store.ReadString();
			}
			else
			{
				ResourceTypeCode resourceTypeCode = (ResourceTypeCode)num;
				if (resourceTypeCode != ResourceTypeCode.String && resourceTypeCode != ResourceTypeCode.Null)
				{
					string text;
					if (resourceTypeCode < ResourceTypeCode.StartOfUserTypes)
					{
						text = resourceTypeCode.ToString();
					}
					else
					{
						text = this.FindType(resourceTypeCode - ResourceTypeCode.StartOfUserTypes).FullName;
					}
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResourceNotString_Type", new object[]
					{
						text
					}));
				}
				if (resourceTypeCode == ResourceTypeCode.String)
				{
					result = this._store.ReadString();
				}
			}
			return result;
		}

		// Token: 0x06002C39 RID: 11321 RVA: 0x000A7954 File Offset: 0x000A5B54
		internal object LoadObject(int pos)
		{
			if (this._version == 1)
			{
				return this.LoadObjectV1(pos);
			}
			ResourceTypeCode resourceTypeCode;
			return this.LoadObjectV2(pos, out resourceTypeCode);
		}

		// Token: 0x06002C3A RID: 11322 RVA: 0x000A797C File Offset: 0x000A5B7C
		internal object LoadObject(int pos, out ResourceTypeCode typeCode)
		{
			if (this._version == 1)
			{
				object obj = this.LoadObjectV1(pos);
				typeCode = ((obj is string) ? ResourceTypeCode.String : ResourceTypeCode.StartOfUserTypes);
				return obj;
			}
			return this.LoadObjectV2(pos, out typeCode);
		}

		// Token: 0x06002C3B RID: 11323 RVA: 0x000A79B4 File Offset: 0x000A5BB4
		internal object LoadObjectV1(int pos)
		{
			object result;
			try
			{
				result = this._LoadObjectV1(pos);
			}
			catch (EndOfStreamException inner)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_TypeMismatch"), inner);
			}
			catch (ArgumentOutOfRangeException inner2)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_TypeMismatch"), inner2);
			}
			return result;
		}

		// Token: 0x06002C3C RID: 11324 RVA: 0x000A7A0C File Offset: 0x000A5C0C
		[SecuritySafeCritical]
		private object _LoadObjectV1(int pos)
		{
			this._store.BaseStream.Seek(this._dataSectionOffset + (long)pos, SeekOrigin.Begin);
			int num = this._store.Read7BitEncodedInt();
			if (num == -1)
			{
				return null;
			}
			RuntimeType left = this.FindType(num);
			if (left == typeof(string))
			{
				return this._store.ReadString();
			}
			if (left == typeof(int))
			{
				return this._store.ReadInt32();
			}
			if (left == typeof(byte))
			{
				return this._store.ReadByte();
			}
			if (left == typeof(sbyte))
			{
				return this._store.ReadSByte();
			}
			if (left == typeof(short))
			{
				return this._store.ReadInt16();
			}
			if (left == typeof(long))
			{
				return this._store.ReadInt64();
			}
			if (left == typeof(ushort))
			{
				return this._store.ReadUInt16();
			}
			if (left == typeof(uint))
			{
				return this._store.ReadUInt32();
			}
			if (left == typeof(ulong))
			{
				return this._store.ReadUInt64();
			}
			if (left == typeof(float))
			{
				return this._store.ReadSingle();
			}
			if (left == typeof(double))
			{
				return this._store.ReadDouble();
			}
			if (left == typeof(DateTime))
			{
				return new DateTime(this._store.ReadInt64());
			}
			if (left == typeof(TimeSpan))
			{
				return new TimeSpan(this._store.ReadInt64());
			}
			if (left == typeof(decimal))
			{
				int[] array = new int[4];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this._store.ReadInt32();
				}
				return new decimal(array);
			}
			return this.DeserializeObject(num);
		}

		// Token: 0x06002C3D RID: 11325 RVA: 0x000A7C64 File Offset: 0x000A5E64
		internal object LoadObjectV2(int pos, out ResourceTypeCode typeCode)
		{
			object result;
			try
			{
				result = this._LoadObjectV2(pos, out typeCode);
			}
			catch (EndOfStreamException inner)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_TypeMismatch"), inner);
			}
			catch (ArgumentOutOfRangeException inner2)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_TypeMismatch"), inner2);
			}
			return result;
		}

		// Token: 0x06002C3E RID: 11326 RVA: 0x000A7CC0 File Offset: 0x000A5EC0
		[SecuritySafeCritical]
		private object _LoadObjectV2(int pos, out ResourceTypeCode typeCode)
		{
			this._store.BaseStream.Seek(this._dataSectionOffset + (long)pos, SeekOrigin.Begin);
			typeCode = (ResourceTypeCode)this._store.Read7BitEncodedInt();
			switch (typeCode)
			{
			case ResourceTypeCode.Null:
				return null;
			case ResourceTypeCode.String:
				return this._store.ReadString();
			case ResourceTypeCode.Boolean:
				return this._store.ReadBoolean();
			case ResourceTypeCode.Char:
				return (char)this._store.ReadUInt16();
			case ResourceTypeCode.Byte:
				return this._store.ReadByte();
			case ResourceTypeCode.SByte:
				return this._store.ReadSByte();
			case ResourceTypeCode.Int16:
				return this._store.ReadInt16();
			case ResourceTypeCode.UInt16:
				return this._store.ReadUInt16();
			case ResourceTypeCode.Int32:
				return this._store.ReadInt32();
			case ResourceTypeCode.UInt32:
				return this._store.ReadUInt32();
			case ResourceTypeCode.Int64:
				return this._store.ReadInt64();
			case ResourceTypeCode.UInt64:
				return this._store.ReadUInt64();
			case ResourceTypeCode.Single:
				return this._store.ReadSingle();
			case ResourceTypeCode.Double:
				return this._store.ReadDouble();
			case ResourceTypeCode.Decimal:
				return this._store.ReadDecimal();
			case ResourceTypeCode.DateTime:
			{
				long dateData = this._store.ReadInt64();
				return DateTime.FromBinary(dateData);
			}
			case ResourceTypeCode.TimeSpan:
			{
				long ticks = this._store.ReadInt64();
				return new TimeSpan(ticks);
			}
			case ResourceTypeCode.ByteArray:
			{
				int num = this._store.ReadInt32();
				if (num < 0)
				{
					throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourceDataLengthInvalid", new object[]
					{
						num
					}));
				}
				if (this._ums == null)
				{
					if ((long)num > this._store.BaseStream.Length)
					{
						throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourceDataLengthInvalid", new object[]
						{
							num
						}));
					}
					return this._store.ReadBytes(num);
				}
				else
				{
					if ((long)num > this._ums.Length - this._ums.Position)
					{
						throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourceDataLengthInvalid", new object[]
						{
							num
						}));
					}
					byte[] array = new byte[num];
					int num2 = this._ums.Read(array, 0, num);
					return array;
				}
				break;
			}
			case ResourceTypeCode.Stream:
			{
				int num3 = this._store.ReadInt32();
				if (num3 < 0)
				{
					throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourceDataLengthInvalid", new object[]
					{
						num3
					}));
				}
				if (this._ums == null)
				{
					byte[] array2 = this._store.ReadBytes(num3);
					return new PinnedBufferMemoryStream(array2);
				}
				if ((long)num3 > this._ums.Length - this._ums.Position)
				{
					throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourceDataLengthInvalid", new object[]
					{
						num3
					}));
				}
				return new UnmanagedMemoryStream(this._ums.PositionPointer, (long)num3, (long)num3, FileAccess.Read, true);
			}
			}
			if (typeCode < ResourceTypeCode.StartOfUserTypes)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_TypeMismatch"));
			}
			int typeIndex = typeCode - ResourceTypeCode.StartOfUserTypes;
			return this.DeserializeObject(typeIndex);
		}

		// Token: 0x06002C3F RID: 11327 RVA: 0x000A8048 File Offset: 0x000A6248
		[SecurityCritical]
		private object DeserializeObject(int typeIndex)
		{
			RuntimeType runtimeType = this.FindType(typeIndex);
			if (this._safeToDeserialize == null)
			{
				this.InitSafeToDeserializeArray();
			}
			object obj;
			if (this._safeToDeserialize[typeIndex])
			{
				this._objFormatter.Binder = this._typeLimitingBinder;
				this._typeLimitingBinder.ExpectingToDeserialize(runtimeType);
				obj = this._objFormatter.UnsafeDeserialize(this._store.BaseStream, null);
			}
			else
			{
				this._objFormatter.Binder = null;
				obj = this._objFormatter.Deserialize(this._store.BaseStream);
			}
			if (obj.GetType() != runtimeType)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResType&SerBlobMismatch", new object[]
				{
					runtimeType.FullName,
					obj.GetType().FullName
				}));
			}
			return obj;
		}

		// Token: 0x06002C40 RID: 11328 RVA: 0x000A810C File Offset: 0x000A630C
		[SecurityCritical]
		private void ReadResources()
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.File | StreamingContextStates.Persistence));
			this._typeLimitingBinder = new ResourceReader.TypeLimitingDeserializationBinder();
			binaryFormatter.Binder = this._typeLimitingBinder;
			this._objFormatter = binaryFormatter;
			try
			{
				this._ReadResources();
			}
			catch (EndOfStreamException inner)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"), inner);
			}
			catch (IndexOutOfRangeException inner2)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"), inner2);
			}
		}

		// Token: 0x06002C41 RID: 11329 RVA: 0x000A8190 File Offset: 0x000A6390
		[SecurityCritical]
		private unsafe void _ReadResources()
		{
			int num = this._store.ReadInt32();
			if (num != ResourceManager.MagicNumber)
			{
				throw new ArgumentException(Environment.GetResourceString("Resources_StreamNotValid"));
			}
			int num2 = this._store.ReadInt32();
			int num3 = this._store.ReadInt32();
			if (num3 < 0 || num2 < 0)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"));
			}
			if (num2 > 1)
			{
				this._store.BaseStream.Seek((long)num3, SeekOrigin.Current);
			}
			else
			{
				string text = this._store.ReadString();
				AssemblyName asmName = new AssemblyName(ResourceManager.MscorlibName);
				if (!ResourceManager.CompareNames(text, ResourceManager.ResReaderTypeName, asmName))
				{
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_WrongResourceReader_Type", new object[]
					{
						text
					}));
				}
				this.SkipString();
			}
			int num4 = this._store.ReadInt32();
			if (num4 != 2 && num4 != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_ResourceFileUnsupportedVersion", new object[]
				{
					2,
					num4
				}));
			}
			this._version = num4;
			this._numResources = this._store.ReadInt32();
			if (this._numResources < 0)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"));
			}
			int num5 = this._store.ReadInt32();
			if (num5 < 0)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"));
			}
			this._typeTable = new RuntimeType[num5];
			this._typeNamePositions = new int[num5];
			for (int i = 0; i < num5; i++)
			{
				this._typeNamePositions[i] = (int)this._store.BaseStream.Position;
				this.SkipString();
			}
			long position = this._store.BaseStream.Position;
			int num6 = (int)position & 7;
			if (num6 != 0)
			{
				for (int j = 0; j < 8 - num6; j++)
				{
					this._store.ReadByte();
				}
			}
			if (this._ums == null)
			{
				this._nameHashes = new int[this._numResources];
				for (int k = 0; k < this._numResources; k++)
				{
					this._nameHashes[k] = this._store.ReadInt32();
				}
			}
			else
			{
				if (((long)this._numResources & (long)((ulong)-536870912)) != 0L)
				{
					throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"));
				}
				int num7 = 4 * this._numResources;
				this._nameHashesPtr = (int*)this._ums.PositionPointer;
				this._ums.Seek((long)num7, SeekOrigin.Current);
				byte* positionPointer = this._ums.PositionPointer;
			}
			if (this._ums == null)
			{
				this._namePositions = new int[this._numResources];
				for (int l = 0; l < this._numResources; l++)
				{
					int num8 = this._store.ReadInt32();
					if (num8 < 0)
					{
						throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"));
					}
					this._namePositions[l] = num8;
				}
			}
			else
			{
				if (((long)this._numResources & (long)((ulong)-536870912)) != 0L)
				{
					throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"));
				}
				int num9 = 4 * this._numResources;
				this._namePositionsPtr = (int*)this._ums.PositionPointer;
				this._ums.Seek((long)num9, SeekOrigin.Current);
				byte* positionPointer2 = this._ums.PositionPointer;
			}
			this._dataSectionOffset = (long)this._store.ReadInt32();
			if (this._dataSectionOffset < 0L)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"));
			}
			this._nameSectionOffset = this._store.BaseStream.Position;
			if (this._dataSectionOffset < this._nameSectionOffset)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"));
			}
		}

		// Token: 0x06002C42 RID: 11330 RVA: 0x000A851C File Offset: 0x000A671C
		private RuntimeType FindType(int typeIndex)
		{
			if (typeIndex < 0 || typeIndex >= this._typeTable.Length)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_InvalidType"));
			}
			if (this._typeTable[typeIndex] == null)
			{
				long position = this._store.BaseStream.Position;
				try
				{
					this._store.BaseStream.Position = (long)this._typeNamePositions[typeIndex];
					string typeName = this._store.ReadString();
					this._typeTable[typeIndex] = (RuntimeType)Type.GetType(typeName, true);
				}
				finally
				{
					this._store.BaseStream.Position = position;
				}
			}
			return this._typeTable[typeIndex];
		}

		// Token: 0x06002C43 RID: 11331 RVA: 0x000A85D0 File Offset: 0x000A67D0
		[SecurityCritical]
		private void InitSafeToDeserializeArray()
		{
			this._safeToDeserialize = new bool[this._typeTable.Length];
			int i = 0;
			while (i < this._typeTable.Length)
			{
				long position = this._store.BaseStream.Position;
				string text;
				try
				{
					this._store.BaseStream.Position = (long)this._typeNamePositions[i];
					text = this._store.ReadString();
				}
				finally
				{
					this._store.BaseStream.Position = position;
				}
				RuntimeType runtimeType = (RuntimeType)Type.GetType(text, false);
				if (runtimeType == null)
				{
					AssemblyName assemblyName = null;
					string typeName = text;
					goto IL_E5;
				}
				if (!(runtimeType.BaseType == typeof(Enum)))
				{
					string typeName = runtimeType.FullName;
					AssemblyName assemblyName = new AssemblyName();
					RuntimeAssembly runtimeAssembly = (RuntimeAssembly)runtimeType.Assembly;
					assemblyName.Init(runtimeAssembly.GetSimpleName(), runtimeAssembly.GetPublicKey(), null, null, runtimeAssembly.GetLocale(), AssemblyHashAlgorithm.None, AssemblyVersionCompatibility.SameMachine, null, AssemblyNameFlags.PublicKey, null);
					goto IL_E5;
				}
				this._safeToDeserialize[i] = true;
				IL_11B:
				i++;
				continue;
				IL_E5:
				foreach (string asmTypeName in ResourceReader.TypesSafeForDeserialization)
				{
					AssemblyName assemblyName;
					string typeName;
					if (ResourceManager.CompareNames(asmTypeName, typeName, assemblyName))
					{
						this._safeToDeserialize[i] = true;
					}
				}
				goto IL_11B;
			}
		}

		// Token: 0x06002C44 RID: 11332 RVA: 0x000A871C File Offset: 0x000A691C
		public void GetResourceData(string resourceName, out string resourceType, out byte[] resourceData)
		{
			if (resourceName == null)
			{
				throw new ArgumentNullException("resourceName");
			}
			if (this._resCache == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("ResourceReaderIsClosed"));
			}
			int[] array = new int[this._numResources];
			int num = this.FindPosForResource(resourceName);
			if (num == -1)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_ResourceNameNotExist", new object[]
				{
					resourceName
				}));
			}
			lock (this)
			{
				for (int i = 0; i < this._numResources; i++)
				{
					this._store.BaseStream.Position = this._nameSectionOffset + (long)this.GetNamePosition(i);
					int num2 = this._store.Read7BitEncodedInt();
					if (num2 < 0)
					{
						throw new FormatException(Environment.GetResourceString("BadImageFormat_ResourcesNameInvalidOffset", new object[]
						{
							num2
						}));
					}
					this._store.BaseStream.Position += (long)num2;
					int num3 = this._store.ReadInt32();
					if (num3 < 0 || (long)num3 >= this._store.BaseStream.Length - this._dataSectionOffset)
					{
						throw new FormatException(Environment.GetResourceString("BadImageFormat_ResourcesDataInvalidOffset", new object[]
						{
							num3
						}));
					}
					array[i] = num3;
				}
				Array.Sort<int>(array);
				int num4 = Array.BinarySearch<int>(array, num);
				long num5 = (num4 < this._numResources - 1) ? ((long)array[num4 + 1] + this._dataSectionOffset) : this._store.BaseStream.Length;
				int num6 = (int)(num5 - ((long)num + this._dataSectionOffset));
				this._store.BaseStream.Position = this._dataSectionOffset + (long)num;
				ResourceTypeCode resourceTypeCode = (ResourceTypeCode)this._store.Read7BitEncodedInt();
				if (resourceTypeCode < ResourceTypeCode.Null || resourceTypeCode >= ResourceTypeCode.StartOfUserTypes + this._typeTable.Length)
				{
					throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_InvalidType"));
				}
				resourceType = this.TypeNameFromTypeCode(resourceTypeCode);
				num6 -= (int)(this._store.BaseStream.Position - (this._dataSectionOffset + (long)num));
				byte[] array2 = this._store.ReadBytes(num6);
				if (array2.Length != num6)
				{
					throw new FormatException(Environment.GetResourceString("BadImageFormat_ResourceNameCorrupted"));
				}
				resourceData = array2;
			}
		}

		// Token: 0x06002C45 RID: 11333 RVA: 0x000A897C File Offset: 0x000A6B7C
		private string TypeNameFromTypeCode(ResourceTypeCode typeCode)
		{
			if (typeCode < ResourceTypeCode.StartOfUserTypes)
			{
				return "ResourceTypeCode." + typeCode.ToString();
			}
			int num = typeCode - ResourceTypeCode.StartOfUserTypes;
			long position = this._store.BaseStream.Position;
			string result;
			try
			{
				this._store.BaseStream.Position = (long)this._typeNamePositions[num];
				result = this._store.ReadString();
			}
			finally
			{
				this._store.BaseStream.Position = position;
			}
			return result;
		}

		// Token: 0x0400119E RID: 4510
		private const int DefaultFileStreamBufferSize = 4096;

		// Token: 0x0400119F RID: 4511
		private BinaryReader _store;

		// Token: 0x040011A0 RID: 4512
		internal Dictionary<string, ResourceLocator> _resCache;

		// Token: 0x040011A1 RID: 4513
		private long _nameSectionOffset;

		// Token: 0x040011A2 RID: 4514
		private long _dataSectionOffset;

		// Token: 0x040011A3 RID: 4515
		private int[] _nameHashes;

		// Token: 0x040011A4 RID: 4516
		[SecurityCritical]
		private unsafe int* _nameHashesPtr;

		// Token: 0x040011A5 RID: 4517
		private int[] _namePositions;

		// Token: 0x040011A6 RID: 4518
		[SecurityCritical]
		private unsafe int* _namePositionsPtr;

		// Token: 0x040011A7 RID: 4519
		private RuntimeType[] _typeTable;

		// Token: 0x040011A8 RID: 4520
		private int[] _typeNamePositions;

		// Token: 0x040011A9 RID: 4521
		private BinaryFormatter _objFormatter;

		// Token: 0x040011AA RID: 4522
		private int _numResources;

		// Token: 0x040011AB RID: 4523
		private UnmanagedMemoryStream _ums;

		// Token: 0x040011AC RID: 4524
		private int _version;

		// Token: 0x040011AD RID: 4525
		private bool[] _safeToDeserialize;

		// Token: 0x040011AE RID: 4526
		private ResourceReader.TypeLimitingDeserializationBinder _typeLimitingBinder;

		// Token: 0x040011AF RID: 4527
		private static readonly string[] TypesSafeForDeserialization = new string[]
		{
			"System.String[], mscorlib, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
			"System.DateTime[], mscorlib, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
			"System.Drawing.Bitmap, System.Drawing, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
			"System.Drawing.Imaging.Metafile, System.Drawing, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
			"System.Drawing.Point, System.Drawing, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
			"System.Drawing.PointF, System.Drawing, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
			"System.Drawing.Size, System.Drawing, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
			"System.Drawing.SizeF, System.Drawing, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
			"System.Drawing.Font, System.Drawing, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
			"System.Drawing.Icon, System.Drawing, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
			"System.Drawing.Color, System.Drawing, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
			"System.Windows.Forms.Cursor, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089",
			"System.Windows.Forms.Padding, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089",
			"System.Windows.Forms.LinkArea, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089",
			"System.Windows.Forms.ImageListStreamer, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089",
			"System.Windows.Forms.ListViewGroup, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089",
			"System.Windows.Forms.ListViewItem, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089",
			"System.Windows.Forms.ListViewItem+ListViewSubItem, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089",
			"System.Windows.Forms.ListViewItem+ListViewSubItem+SubItemStyle, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089",
			"System.Windows.Forms.OwnerDrawPropertyBag, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089",
			"System.Windows.Forms.TreeNode, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089"
		};

		// Token: 0x02000B2F RID: 2863
		internal sealed class TypeLimitingDeserializationBinder : SerializationBinder
		{
			// Token: 0x1700122D RID: 4653
			// (get) Token: 0x06006AED RID: 27373 RVA: 0x00171364 File Offset: 0x0016F564
			// (set) Token: 0x06006AEE RID: 27374 RVA: 0x0017136C File Offset: 0x0016F56C
			internal ObjectReader ObjectReader
			{
				get
				{
					return this._objectReader;
				}
				set
				{
					this._objectReader = value;
				}
			}

			// Token: 0x06006AEF RID: 27375 RVA: 0x00171375 File Offset: 0x0016F575
			internal void ExpectingToDeserialize(RuntimeType type)
			{
				this._typeToDeserialize = type;
			}

			// Token: 0x06006AF0 RID: 27376 RVA: 0x00171380 File Offset: 0x0016F580
			[SecuritySafeCritical]
			public override Type BindToType(string assemblyName, string typeName)
			{
				AssemblyName asmName = new AssemblyName(assemblyName);
				bool flag = false;
				foreach (string asmTypeName in ResourceReader.TypesSafeForDeserialization)
				{
					if (ResourceManager.CompareNames(asmTypeName, typeName, asmName))
					{
						flag = true;
						break;
					}
				}
				Type type = this.ObjectReader.FastBindToType(assemblyName, typeName);
				if (type.IsEnum)
				{
					flag = true;
				}
				if (flag)
				{
					return null;
				}
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResType&SerBlobMismatch", new object[]
				{
					this._typeToDeserialize.FullName,
					typeName
				}));
			}

			// Token: 0x0400335E RID: 13150
			private RuntimeType _typeToDeserialize;

			// Token: 0x0400335F RID: 13151
			private ObjectReader _objectReader;
		}

		// Token: 0x02000B30 RID: 2864
		internal sealed class ResourceEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06006AF2 RID: 27378 RVA: 0x0017140F File Offset: 0x0016F60F
			internal ResourceEnumerator(ResourceReader reader)
			{
				this._currentName = -1;
				this._reader = reader;
				this._dataPosition = -2;
			}

			// Token: 0x06006AF3 RID: 27379 RVA: 0x00171430 File Offset: 0x0016F630
			public bool MoveNext()
			{
				if (this._currentName == this._reader._numResources - 1 || this._currentName == -2147483648)
				{
					this._currentIsValid = false;
					this._currentName = int.MinValue;
					return false;
				}
				this._currentIsValid = true;
				this._currentName++;
				return true;
			}

			// Token: 0x1700122E RID: 4654
			// (get) Token: 0x06006AF4 RID: 27380 RVA: 0x0017148C File Offset: 0x0016F68C
			public object Key
			{
				[SecuritySafeCritical]
				get
				{
					if (this._currentName == -2147483648)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
					}
					if (!this._currentIsValid)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
					}
					if (this._reader._resCache == null)
					{
						throw new InvalidOperationException(Environment.GetResourceString("ResourceReaderIsClosed"));
					}
					return this._reader.AllocateStringForNameIndex(this._currentName, out this._dataPosition);
				}
			}

			// Token: 0x1700122F RID: 4655
			// (get) Token: 0x06006AF5 RID: 27381 RVA: 0x00171502 File Offset: 0x0016F702
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x17001230 RID: 4656
			// (get) Token: 0x06006AF6 RID: 27382 RVA: 0x0017150F File Offset: 0x0016F70F
			internal int DataPosition
			{
				get
				{
					return this._dataPosition;
				}
			}

			// Token: 0x17001231 RID: 4657
			// (get) Token: 0x06006AF7 RID: 27383 RVA: 0x00171518 File Offset: 0x0016F718
			public DictionaryEntry Entry
			{
				[SecuritySafeCritical]
				get
				{
					if (this._currentName == -2147483648)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
					}
					if (!this._currentIsValid)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
					}
					if (this._reader._resCache == null)
					{
						throw new InvalidOperationException(Environment.GetResourceString("ResourceReaderIsClosed"));
					}
					object obj = null;
					ResourceReader reader = this._reader;
					string key;
					lock (reader)
					{
						Dictionary<string, ResourceLocator> resCache = this._reader._resCache;
						lock (resCache)
						{
							key = this._reader.AllocateStringForNameIndex(this._currentName, out this._dataPosition);
							ResourceLocator resourceLocator;
							if (this._reader._resCache.TryGetValue(key, out resourceLocator))
							{
								obj = resourceLocator.Value;
							}
							if (obj == null)
							{
								if (this._dataPosition == -1)
								{
									obj = this._reader.GetValueForNameIndex(this._currentName);
								}
								else
								{
									obj = this._reader.LoadObject(this._dataPosition);
								}
							}
						}
					}
					return new DictionaryEntry(key, obj);
				}
			}

			// Token: 0x17001232 RID: 4658
			// (get) Token: 0x06006AF8 RID: 27384 RVA: 0x00171648 File Offset: 0x0016F848
			public object Value
			{
				get
				{
					if (this._currentName == -2147483648)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
					}
					if (!this._currentIsValid)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
					}
					if (this._reader._resCache == null)
					{
						throw new InvalidOperationException(Environment.GetResourceString("ResourceReaderIsClosed"));
					}
					return this._reader.GetValueForNameIndex(this._currentName);
				}
			}

			// Token: 0x06006AF9 RID: 27385 RVA: 0x001716B8 File Offset: 0x0016F8B8
			public void Reset()
			{
				if (this._reader._resCache == null)
				{
					throw new InvalidOperationException(Environment.GetResourceString("ResourceReaderIsClosed"));
				}
				this._currentIsValid = false;
				this._currentName = -1;
			}

			// Token: 0x04003360 RID: 13152
			private const int ENUM_DONE = -2147483648;

			// Token: 0x04003361 RID: 13153
			private const int ENUM_NOT_STARTED = -1;

			// Token: 0x04003362 RID: 13154
			private ResourceReader _reader;

			// Token: 0x04003363 RID: 13155
			private bool _currentIsValid;

			// Token: 0x04003364 RID: 13156
			private int _currentName;

			// Token: 0x04003365 RID: 13157
			private int _dataPosition;
		}
	}
}
