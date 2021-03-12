using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000916 RID: 2326
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class ParticipantEntryId
	{
		// Token: 0x06005728 RID: 22312 RVA: 0x00166B27 File Offset: 0x00164D27
		internal ParticipantEntryId()
		{
		}

		// Token: 0x17001840 RID: 6208
		// (get) Token: 0x06005729 RID: 22313 RVA: 0x00166B30 File Offset: 0x00164D30
		internal virtual bool? IsDL
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600572A RID: 22314 RVA: 0x00166B48 File Offset: 0x00164D48
		public static ParticipantEntryId TryFromEntryId(byte[] bytes)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			ParticipantEntryId result;
			using (ParticipantEntryId.Reader reader = new ParticipantEntryId.Reader(bytes))
			{
				result = ParticipantEntryId.TryFromEntryId(reader);
			}
			return result;
		}

		// Token: 0x0600572B RID: 22315 RVA: 0x00166B90 File Offset: 0x00164D90
		public static ParticipantEntryId FromParticipant(Participant participant, ParticipantEntryIdConsumer consumer)
		{
			if (participant == null)
			{
				throw new ArgumentNullException("participant");
			}
			EnumValidator.ThrowIfInvalid<ParticipantEntryIdConsumer>(consumer);
			ParticipantEntryId participantEntryId = null;
			foreach (ParticipantEntryId.TryFromParticipantDelegate tryFromParticipantDelegate in ParticipantEntryId.tryFromParticipantChain)
			{
				participantEntryId = tryFromParticipantDelegate(participant, consumer);
				if (participantEntryId != null)
				{
					break;
				}
			}
			return participantEntryId;
		}

		// Token: 0x0600572C RID: 22316 RVA: 0x00166BE0 File Offset: 0x00164DE0
		public byte[] ToByteArray()
		{
			byte[] bytes;
			using (ParticipantEntryId.Writer writer = new ParticipantEntryId.Writer())
			{
				this.Serialize(writer);
				bytes = writer.GetBytes();
			}
			return bytes;
		}

		// Token: 0x0600572D RID: 22317 RVA: 0x00166C70 File Offset: 0x00164E70
		internal static ParticipantEntryId FromEntryId(byte[] bytes)
		{
			return ParticipantEntryId.TranslateExceptions<ParticipantEntryId>(delegate
			{
				ParticipantEntryId result;
				using (ParticipantEntryId.Reader reader = new ParticipantEntryId.Reader(bytes))
				{
					result = ParticipantEntryId.FromEntryId(null, reader);
				}
				return result;
			});
		}

		// Token: 0x0600572E RID: 22318 RVA: 0x00166CE4 File Offset: 0x00164EE4
		internal static IList<ParticipantEntryId> FromFlatEntryList(byte[] bytes)
		{
			return ParticipantEntryId.TranslateExceptions<IList<ParticipantEntryId>>(delegate
			{
				IList<ParticipantEntryId> result;
				using (ParticipantEntryId.Reader reader = new ParticipantEntryId.Reader(bytes))
				{
					result = ParticipantEntryId.FromFlatEntryList(reader);
				}
				return result;
			});
		}

		// Token: 0x0600572F RID: 22319 RVA: 0x00166E24 File Offset: 0x00165024
		internal static byte[] ToFlatEntryList(ICollection<ParticipantEntryId> entryIds)
		{
			return ParticipantEntryId.TranslateExceptions<byte[]>(delegate
			{
				byte[] bytes;
				using (ParticipantEntryId.Writer writer = new ParticipantEntryId.Writer())
				{
					writer.Write(entryIds.Count);
					ParticipantEntryId.Bookmark bookmark = writer.PlaceBookmark<int>(new ParticipantEntryId.WriterMethod<int>(writer.Write), 0);
					ParticipantEntryId.Bookmark currentBookmark = writer.CurrentBookmark;
					foreach (ParticipantEntryId participantEntryId in entryIds)
					{
						if (participantEntryId == null)
						{
							throw new ArgumentNullException("entryIds");
						}
						ParticipantEntryId.Bookmark bookmark2 = writer.PlaceBookmark<int>(new ParticipantEntryId.WriterMethod<int>(writer.Write), 0);
						ParticipantEntryId.Bookmark currentBookmark2 = writer.CurrentBookmark;
						participantEntryId.Serialize(writer);
						int num = (int)(writer.CurrentBookmark - currentBookmark2);
						writer.WriteBookmark<int>(bookmark2, num);
						writer.WritePadding(num, 4);
					}
					writer.WriteBookmark<int>(bookmark, (int)(writer.CurrentBookmark - currentBookmark));
					bytes = writer.GetBytes();
				}
				return bytes;
			});
		}

		// Token: 0x06005730 RID: 22320
		internal abstract IEnumerable<PropValue> GetParticipantProperties();

		// Token: 0x06005731 RID: 22321 RVA: 0x00166E4F File Offset: 0x0016504F
		internal virtual ParticipantOrigin GetParticipantOrigin()
		{
			return null;
		}

		// Token: 0x06005732 RID: 22322 RVA: 0x00166E54 File Offset: 0x00165054
		protected static T TranslateExceptions<T>(ParticipantEntryId.SimpleAction<T> action)
		{
			T result;
			try
			{
				result = action();
			}
			catch (ArgumentException innerException)
			{
				throw new CorruptDataException(ServerStrings.ExInvalidParticipantEntryId, innerException);
			}
			catch (EndOfStreamException innerException2)
			{
				throw new CorruptDataException(ServerStrings.ExInvalidParticipantEntryId, innerException2);
			}
			return result;
		}

		// Token: 0x06005733 RID: 22323
		protected abstract void Serialize(ParticipantEntryId.Writer writer);

		// Token: 0x06005734 RID: 22324 RVA: 0x00166EA4 File Offset: 0x001650A4
		private static ParticipantEntryId FromEntryId(ParticipantEntryId.WabEntryFlag? wrapperFlags, ParticipantEntryId.Reader reader)
		{
			Guid a = reader.ReadEntryHeader();
			if (a == ParticipantEntryId.OneOffProviderGuid)
			{
				using (ParticipantEntryId.Reader reader2 = reader.TearRest())
				{
					return new OneOffParticipantEntryId(reader2);
				}
			}
			if (a == ParticipantEntryId.WabProviderGuid)
			{
				ParticipantEntryId.WabEntryFlag wabEntryFlag = (ParticipantEntryId.WabEntryFlag)reader.ReadByte();
				switch ((byte)(wabEntryFlag & ParticipantEntryId.WabEntryFlag.ObjectTypeMask))
				{
				case 0:
				case 5:
				case 6:
					break;
				case 1:
				case 2:
					goto IL_D0;
				case 3:
				case 4:
					using (ParticipantEntryId.Reader reader3 = reader.TearRest())
					{
						return new StoreParticipantEntryId(wabEntryFlag, reader3);
					}
					break;
				default:
					goto IL_D0;
				}
				if (wrapperFlags != null)
				{
					throw new CorruptDataException(ServerStrings.ExInvalidParticipantEntryId);
				}
				using (ParticipantEntryId.Reader reader4 = reader.TearRest())
				{
					return ParticipantEntryId.FromEntryId(new ParticipantEntryId.WabEntryFlag?(wabEntryFlag), reader4);
				}
				IL_D0:
				throw new NotSupportedException(ServerStrings.ExInvalidWABObjectType(wabEntryFlag & ParticipantEntryId.WabEntryFlag.ObjectTypeMask));
			}
			if (a == ParticipantEntryId.ExchangeProviderGuid)
			{
				using (ParticipantEntryId.Reader reader5 = reader.TearRest())
				{
					return new ADParticipantEntryId(wrapperFlags, reader5);
				}
			}
			if (a == ParticipantEntryId.OlabProviderGuid)
			{
				using (ParticipantEntryId.Reader reader6 = reader.TearRest())
				{
					return new StoreParticipantEntryId(reader6);
				}
			}
			throw new NotSupportedException(ServerStrings.ExUnsupportedABProvider(a.ToString(), string.Empty));
		}

		// Token: 0x06005735 RID: 22325 RVA: 0x00167058 File Offset: 0x00165258
		private static IList<ParticipantEntryId> FromFlatEntryList(ParticipantEntryId.Reader reader)
		{
			int num = reader.ReadInt32();
			reader.ReadInt32();
			IList<ParticipantEntryId> list = new List<ParticipantEntryId>();
			for (int i = 0; i < num; i++)
			{
				int num2 = reader.ReadInt32();
				list.Add(ParticipantEntryId.TryFromEntryId(reader.TearNext(num2)));
				if (reader.BytesRemaining != 0)
				{
					reader.ReadPadding(num2, 4);
				}
			}
			reader.EnsureEnd();
			return list;
		}

		// Token: 0x06005736 RID: 22326 RVA: 0x001670E4 File Offset: 0x001652E4
		private static ParticipantEntryId TryFromEntryId(ParticipantEntryId.Reader reader)
		{
			try
			{
				return ParticipantEntryId.TranslateExceptions<ParticipantEntryId>(() => ParticipantEntryId.FromEntryId(null, reader));
			}
			catch (NotSupportedException)
			{
			}
			catch (CorruptDataException)
			{
			}
			reader.BaseStream.Position = 0L;
			return new UnrecognizedParticipantEntryId(reader.ReadBytes(reader.BytesRemaining));
		}

		// Token: 0x04002E80 RID: 11904
		protected static readonly Guid ExchangeProviderGuid = new Guid("c840a7dc-42c0-1a10-b4b9-08002b2fe182");

		// Token: 0x04002E81 RID: 11905
		protected static readonly Guid OlabProviderGuid = new Guid("0aaa42fe-c718-101a-e885-0b651c240000");

		// Token: 0x04002E82 RID: 11906
		protected static readonly Guid OneOffProviderGuid = new Guid("a41f2b81-a3be-1910-9d6e-00dd010f5402");

		// Token: 0x04002E83 RID: 11907
		protected static readonly Guid WabProviderGuid = new Guid("d3ad91c0-9d51-11cf-a4a9-00aa0047faa4");

		// Token: 0x04002E84 RID: 11908
		private static ParticipantEntryId.TryFromParticipantDelegate[] tryFromParticipantChain = new ParticipantEntryId.TryFromParticipantDelegate[]
		{
			new ParticipantEntryId.TryFromParticipantDelegate(StoreParticipantEntryId.TryFromParticipant),
			new ParticipantEntryId.TryFromParticipantDelegate(ADParticipantEntryId.TryFromParticipant),
			new ParticipantEntryId.TryFromParticipantDelegate(OneOffParticipantEntryId.TryFromParticipant)
		};

		// Token: 0x02000917 RID: 2327
		[Flags]
		protected internal enum WabEntryFlag : byte
		{
			// Token: 0x04002E86 RID: 11910
			Envelope = 0,
			// Token: 0x04002E87 RID: 11911
			WabMember = 2,
			// Token: 0x04002E88 RID: 11912
			ContactPerson = 3,
			// Token: 0x04002E89 RID: 11913
			ContactDL = 4,
			// Token: 0x04002E8A RID: 11914
			DirectoryPerson = 5,
			// Token: 0x04002E8B RID: 11915
			DirectoryDL = 6,
			// Token: 0x04002E8C RID: 11916
			BusinessFax = 0,
			// Token: 0x04002E8D RID: 11917
			HomeFax = 16,
			// Token: 0x04002E8E RID: 11918
			OtherFax = 32,
			// Token: 0x04002E8F RID: 11919
			NoEmailIndex = 48,
			// Token: 0x04002E90 RID: 11920
			EmailIndex1 = 64,
			// Token: 0x04002E91 RID: 11921
			EmailIndex2 = 80,
			// Token: 0x04002E92 RID: 11922
			EmailIndex3 = 96,
			// Token: 0x04002E93 RID: 11923
			Outlook = 128,
			// Token: 0x04002E94 RID: 11924
			EmailIndexMask = 112,
			// Token: 0x04002E95 RID: 11925
			ObjectTypeMask = 15
		}

		// Token: 0x02000918 RID: 2328
		// (Invoke) Token: 0x06005739 RID: 22329
		public delegate void WriterMethod<T>(T value);

		// Token: 0x02000919 RID: 2329
		// (Invoke) Token: 0x0600573D RID: 22333
		protected delegate ParticipantEntryId TryFromParticipantDelegate(Participant participant, ParticipantEntryIdConsumer consumer);

		// Token: 0x0200091A RID: 2330
		// (Invoke) Token: 0x06005741 RID: 22337
		protected delegate T SimpleAction<T>();

		// Token: 0x0200091B RID: 2331
		protected internal struct Bookmark
		{
			// Token: 0x06005744 RID: 22340 RVA: 0x001671EF File Offset: 0x001653EF
			public static long operator -(ParticipantEntryId.Bookmark end, ParticipantEntryId.Bookmark start)
			{
				return end.Position - start.Position;
			}

			// Token: 0x04002E96 RID: 11926
			public long Position;

			// Token: 0x04002E97 RID: 11927
			public long Size;

			// Token: 0x04002E98 RID: 11928
			public Delegate WriterMethod;
		}

		// Token: 0x0200091C RID: 2332
		[StructLayout(LayoutKind.Explicit)]
		internal struct LTId
		{
			// Token: 0x04002E99 RID: 11929
			public const int Size = 24;

			// Token: 0x04002E9A RID: 11930
			[FieldOffset(0)]
			public Guid Guid;

			// Token: 0x04002E9B RID: 11931
			[FixedBuffer(typeof(byte), 6)]
			[FieldOffset(16)]
			public ParticipantEntryId.LTId.<GlobCnt>e__FixedBuffer0 GlobCnt;

			// Token: 0x04002E9C RID: 11932
			[FieldOffset(22)]
			public ushort Level;

			// Token: 0x04002E9D RID: 11933
			[FixedBuffer(typeof(byte), 24)]
			[FieldOffset(0)]
			private ParticipantEntryId.LTId.<bytes>e__FixedBuffer1 bytes;

			// Token: 0x04002E9E RID: 11934
			[FieldOffset(16)]
			public ulong GlobCntLong;

			// Token: 0x0200091D RID: 2333
			[UnsafeValueType]
			[CompilerGenerated]
			[StructLayout(LayoutKind.Sequential, Size = 6)]
			public struct <GlobCnt>e__FixedBuffer0
			{
				// Token: 0x04002E9F RID: 11935
				public byte FixedElementField;
			}

			// Token: 0x0200091E RID: 2334
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 24)]
			public struct <bytes>e__FixedBuffer1
			{
				// Token: 0x04002EA0 RID: 11936
				public byte FixedElementField;
			}
		}

		// Token: 0x0200091F RID: 2335
		internal struct LTEntryId
		{
			// Token: 0x04002EA1 RID: 11937
			public const int Size = 70;

			// Token: 0x04002EA2 RID: 11938
			public uint ABFlags;

			// Token: 0x04002EA3 RID: 11939
			public Guid StoreGuid;

			// Token: 0x04002EA4 RID: 11940
			public ushort Eit;

			// Token: 0x04002EA5 RID: 11941
			public ParticipantEntryId.LTId FolderId;

			// Token: 0x04002EA6 RID: 11942
			public ParticipantEntryId.LTId MessageId;
		}

		// Token: 0x02000920 RID: 2336
		protected internal class Reader : BinaryReader
		{
			// Token: 0x06005745 RID: 22341 RVA: 0x00167200 File Offset: 0x00165400
			public Reader(byte[] bytes) : this(bytes, 0, bytes.Length)
			{
			}

			// Token: 0x17001841 RID: 6209
			// (get) Token: 0x06005746 RID: 22342 RVA: 0x0016720D File Offset: 0x0016540D
			public int BytesRemaining
			{
				get
				{
					return (int)(this.BaseStream.Length - this.BaseStream.Position);
				}
			}

			// Token: 0x17001842 RID: 6210
			// (get) Token: 0x06005747 RID: 22343 RVA: 0x00167227 File Offset: 0x00165427
			public bool IsEnd
			{
				get
				{
					return this.BytesRemaining == 0;
				}
			}

			// Token: 0x17001843 RID: 6211
			// (get) Token: 0x06005748 RID: 22344 RVA: 0x00167232 File Offset: 0x00165432
			protected int BufferPointer
			{
				get
				{
					return this.Origin + (int)this.BaseStream.Position;
				}
			}

			// Token: 0x06005749 RID: 22345 RVA: 0x00167247 File Offset: 0x00165447
			public void EnsureEnd()
			{
				if (this.BytesRemaining != 0)
				{
					throw new EndOfStreamException(ServerStrings.ExInvalidParticipantEntryId);
				}
			}

			// Token: 0x0600574A RID: 22346 RVA: 0x00167261 File Offset: 0x00165461
			public Guid ReadEntryHeader()
			{
				if (this.ReadUInt32() != 0U)
				{
					throw new CorruptDataException(ServerStrings.ExInvalidParticipantEntryId);
				}
				return this.ReadGuid();
			}

			// Token: 0x0600574B RID: 22347 RVA: 0x0016727C File Offset: 0x0016547C
			public byte[] ReadExactBytes(int count)
			{
				byte[] array = this.ReadBytes(count);
				if (array.Length != count)
				{
					throw new EndOfStreamException(ServerStrings.ExInvalidParticipantEntryId);
				}
				return array;
			}

			// Token: 0x0600574C RID: 22348 RVA: 0x001672A8 File Offset: 0x001654A8
			public Guid ReadGuid()
			{
				return new Guid(this.ReadExactBytes(sizeof(Guid)));
			}

			// Token: 0x0600574D RID: 22349 RVA: 0x001672BC File Offset: 0x001654BC
			public ParticipantEntryId.LTEntryId ReadLTEntryId()
			{
				ParticipantEntryId.LTEntryId result = default(ParticipantEntryId.LTEntryId);
				result.ABFlags = this.ReadUInt32();
				result.StoreGuid = this.ReadGuid();
				if (this.BytesRemaining != 4)
				{
					result.Eit = this.ReadUInt16();
					result.FolderId = this.ReadLTId();
					result.MessageId = this.ReadLTId();
					return result;
				}
				throw new NotSupportedException(ServerStrings.ExUnsupportedABProvider("PST", string.Empty));
			}

			// Token: 0x0600574E RID: 22350 RVA: 0x00167338 File Offset: 0x00165538
			public ParticipantEntryId.LTId ReadLTId()
			{
				return new ParticipantEntryId.LTId
				{
					Guid = this.ReadGuid(),
					GlobCntLong = this.ReadUInt64()
				};
			}

			// Token: 0x0600574F RID: 22351 RVA: 0x00167368 File Offset: 0x00165568
			public void ReadPadding(int size, int granularity)
			{
				int num = size % granularity;
				if (num != 0)
				{
					this.ReadExactBytes(granularity - num);
				}
			}

			// Token: 0x06005750 RID: 22352 RVA: 0x00167388 File Offset: 0x00165588
			public string ReadZString(Encoding encoding)
			{
				char[] chars = encoding.GetChars(this.GetBuffer(), this.BufferPointer, this.BytesRemaining);
				int num = ParticipantEntryId.Reader.IndexOf(chars, '\0');
				if (num != -1)
				{
					string result = new string(chars, 0, num);
					this.BaseStream.Position += (long)encoding.GetByteCount(chars, 0, num + 1);
					return result;
				}
				throw new CorruptDataException(ServerStrings.ExInvalidParticipantEntryId);
			}

			// Token: 0x06005751 RID: 22353 RVA: 0x001673F0 File Offset: 0x001655F0
			public ParticipantEntryId.Reader TearNext(int count)
			{
				ParticipantEntryId.Reader result = new ParticipantEntryId.Reader(this, (int)this.BaseStream.Position, count);
				this.BaseStream.Position += (long)count;
				return result;
			}

			// Token: 0x06005752 RID: 22354 RVA: 0x00167426 File Offset: 0x00165626
			public ParticipantEntryId.Reader TearRest()
			{
				return this.TearNext(this.BytesRemaining);
			}

			// Token: 0x06005753 RID: 22355 RVA: 0x00167434 File Offset: 0x00165634
			private static int IndexOf(char[] array, char ch)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] == ch)
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x06005754 RID: 22356 RVA: 0x00167458 File Offset: 0x00165658
			public Reader(byte[] bytes, int index, int count) : base(new MemoryStream(bytes, index, count, false, true))
			{
			}

			// Token: 0x06005755 RID: 22357 RVA: 0x0016746A File Offset: 0x0016566A
			public Reader(ParticipantEntryId.Reader parentReader, int index, int count) : this(((MemoryStream)parentReader.BaseStream).GetBuffer(), index + parentReader.Origin, count)
			{
				this.Origin = index + parentReader.Origin;
			}

			// Token: 0x06005756 RID: 22358 RVA: 0x00167499 File Offset: 0x00165699
			protected byte[] GetBuffer()
			{
				return ((MemoryStream)this.BaseStream).GetBuffer();
			}

			// Token: 0x04002EA7 RID: 11943
			protected readonly int Origin;
		}

		// Token: 0x02000921 RID: 2337
		protected internal class Writer : BinaryWriter
		{
			// Token: 0x06005757 RID: 22359 RVA: 0x001674AB File Offset: 0x001656AB
			public Writer() : base(new MemoryStream())
			{
			}

			// Token: 0x17001844 RID: 6212
			// (get) Token: 0x06005758 RID: 22360 RVA: 0x001674B8 File Offset: 0x001656B8
			public ParticipantEntryId.Bookmark CurrentBookmark
			{
				get
				{
					ParticipantEntryId.Bookmark result;
					result.Position = this.Position;
					result.Size = 0L;
					result.WriterMethod = null;
					return result;
				}
			}

			// Token: 0x17001845 RID: 6213
			// (get) Token: 0x06005759 RID: 22361 RVA: 0x001674E4 File Offset: 0x001656E4
			public long Position
			{
				get
				{
					this.Flush();
					return this.BaseStream.Position;
				}
			}

			// Token: 0x0600575A RID: 22362 RVA: 0x001674F7 File Offset: 0x001656F7
			public byte[] GetBytes()
			{
				this.Flush();
				return ((MemoryStream)this.BaseStream).ToArray();
			}

			// Token: 0x0600575B RID: 22363 RVA: 0x00167510 File Offset: 0x00165710
			public ParticipantEntryId.Bookmark PlaceBookmark<T>(ParticipantEntryId.WriterMethod<T> writerMethod, T initialValue)
			{
				ParticipantEntryId.Bookmark result;
				result.Position = this.Position;
				writerMethod(initialValue);
				result.Size = this.BaseStream.Position - result.Position;
				result.WriterMethod = writerMethod;
				return result;
			}

			// Token: 0x0600575C RID: 22364 RVA: 0x00167554 File Offset: 0x00165754
			public void WriteEntryHeader(Guid providerGuid)
			{
				this.Write(0U);
				this.Write(providerGuid);
			}

			// Token: 0x0600575D RID: 22365 RVA: 0x00167564 File Offset: 0x00165764
			public void Write(Guid guid)
			{
				byte[] array = guid.ToByteArray();
				this.Write(array, 0, array.Length);
			}

			// Token: 0x0600575E RID: 22366 RVA: 0x00167584 File Offset: 0x00165784
			public void Write(ParticipantEntryId.LTEntryId ltEntryId)
			{
				this.Write(ltEntryId.ABFlags);
				this.Write(ltEntryId.StoreGuid);
				this.Write(ltEntryId.Eit);
				this.Write(ltEntryId.FolderId);
				this.Write(ltEntryId.MessageId);
			}

			// Token: 0x0600575F RID: 22367 RVA: 0x001675D2 File Offset: 0x001657D2
			public void Write(ParticipantEntryId.LTId ltid)
			{
				this.Write(ltid.Guid);
				this.Write(ltid.GlobCntLong);
			}

			// Token: 0x06005760 RID: 22368 RVA: 0x001675F0 File Offset: 0x001657F0
			public void WriteBookmark<T>(ParticipantEntryId.Bookmark bookmark, T value)
			{
				long position = this.Position;
				this.BaseStream.Position = bookmark.Position;
				((ParticipantEntryId.WriterMethod<T>)bookmark.WriterMethod)(value);
				this.BaseStream.Position = position;
			}

			// Token: 0x06005761 RID: 22369 RVA: 0x00167634 File Offset: 0x00165834
			public void WritePadding(int size, int granularity)
			{
				int num = size % granularity;
				if (num != 0)
				{
					this.Write(new byte[granularity - num]);
				}
			}

			// Token: 0x06005762 RID: 22370 RVA: 0x00167658 File Offset: 0x00165858
			public void WriteZString(string value, Encoding encoding)
			{
				byte[] bytes = encoding.GetBytes(value + '\0');
				this.Write(bytes, 0, bytes.Length);
			}
		}
	}
}
