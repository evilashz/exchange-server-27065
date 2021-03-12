using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000670 RID: 1648
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class Ex12ExRenEntryParser
	{
		// Token: 0x0600441A RID: 17434 RVA: 0x0012279A File Offset: 0x0012099A
		private Ex12ExRenEntryParser(byte[] additionalRenEntryIdsEx)
		{
			this.defaultFolderRenEx = new Dictionary<ushort, Ex12ExRenEntryParser.ExFolderEntry>();
			if (additionalRenEntryIdsEx == null)
			{
				this.entryBlob = Array<byte>.Empty;
				return;
			}
			this.entryBlob = additionalRenEntryIdsEx;
		}

		// Token: 0x0600441B RID: 17435 RVA: 0x001227C4 File Offset: 0x001209C4
		internal static Ex12ExRenEntryParser FromBytes(byte[] blob)
		{
			Ex12ExRenEntryParser ex12ExRenEntryParser = new Ex12ExRenEntryParser(blob);
			try
			{
				ex12ExRenEntryParser.Parse();
			}
			catch (FormatException)
			{
				ExTraceGlobals.DefaultFoldersTracer.TraceWarning<string>(-1L, "Ex12ExRenEntryParser::FromBytes. The blob is not correctly formated. Bytes = {0}.", (blob == null) ? "<null>" : Convert.ToBase64String(blob));
				ex12ExRenEntryParser.entryBlob = Array<byte>.Empty;
				ex12ExRenEntryParser.defaultFolderRenEx = new Dictionary<ushort, Ex12ExRenEntryParser.ExFolderEntry>();
			}
			return ex12ExRenEntryParser;
		}

		// Token: 0x0600441C RID: 17436 RVA: 0x0012282C File Offset: 0x00120A2C
		internal byte[] GetEntryId(Ex12RenEntryIdStrategy.PersistenceId persistenceId)
		{
			return this.GetExFolderEntry((ushort)persistenceId).EntryId;
		}

		// Token: 0x0600441D RID: 17437 RVA: 0x0012283C File Offset: 0x00120A3C
		internal void Insert(Ex12RenEntryIdStrategy.PersistenceId persistenceId, byte[] entryId)
		{
			Ex12ExRenEntryParser.ExFolderEntry exFolderEntry = new Ex12ExRenEntryParser.ExFolderEntry(1, entryId, 0, 0);
			if (!this.ReplaceEntryBlock(persistenceId, exFolderEntry))
			{
				this.InsertEntryBlock(persistenceId, exFolderEntry);
			}
		}

		// Token: 0x0600441E RID: 17438 RVA: 0x00122868 File Offset: 0x00120A68
		internal bool Remove(Ex12RenEntryIdStrategy.PersistenceId persistenceId)
		{
			Ex12ExRenEntryParser.ExFolderEntry exFolderEntry = null;
			if (this.defaultFolderRenEx.TryGetValue((ushort)persistenceId, out exFolderEntry))
			{
				int start = exFolderEntry.Start - 4;
				this.entryBlob = Ex12ExRenEntryParser.ReplaceBlob(this.entryBlob, start, exFolderEntry.End, Array<byte>.Empty);
				this.defaultFolderRenEx.Clear();
				this.Parse();
				return true;
			}
			return false;
		}

		// Token: 0x0600441F RID: 17439 RVA: 0x001228C1 File Offset: 0x00120AC1
		internal byte[] ToBytes()
		{
			return this.entryBlob;
		}

		// Token: 0x06004420 RID: 17440 RVA: 0x001228CC File Offset: 0x00120ACC
		[Conditional("DEBUG")]
		private static void DebugCheckReplaceBlob(byte[] newBlob, Ex12RenEntryIdStrategy.PersistenceId persistenceId, byte[] entryId)
		{
			Ex12ExRenEntryParser ex12ExRenEntryParser = new Ex12ExRenEntryParser(newBlob);
			ex12ExRenEntryParser.Parse();
			ex12ExRenEntryParser.GetEntryId(persistenceId);
			for (int i = 0; i < entryId.Length; i++)
			{
			}
		}

		// Token: 0x06004421 RID: 17441 RVA: 0x001228FC File Offset: 0x00120AFC
		private static byte[] ReplaceBlob(byte[] original, int start, int end, byte[] insertEntry)
		{
			byte[] array = new byte[original.Length - (end - start) + insertEntry.Length];
			Array.Copy(original, 0, array, 0, start);
			Array.Copy(insertEntry, 0, array, start, insertEntry.Length);
			Array.Copy(original, end, array, start + insertEntry.Length, original.Length - end);
			return array;
		}

		// Token: 0x06004422 RID: 17442 RVA: 0x00122944 File Offset: 0x00120B44
		private static void ParseEntryBlock(byte[] additionalRenEntryIdsEx, ref int index, ushort blockSize, ref Ex12ExRenEntryParser.ExFolderEntry entry)
		{
			for (;;)
			{
				int start = index;
				uint num = (uint)Ex12ExRenEntryParser.ParseUInt16(additionalRenEntryIdsEx, index);
				index += 2;
				ushort num2 = Ex12ExRenEntryParser.ParseUInt16(additionalRenEntryIdsEx, index);
				index += 2;
				blockSize -= 4;
				if (num2 > blockSize)
				{
					break;
				}
				try
				{
					if (num == 1U)
					{
						entry.EntryId = new byte[(int)num2];
						Array.Copy(additionalRenEntryIdsEx, index, entry.EntryId, 0, (int)num2);
					}
					else if (num == 2U)
					{
						entry.ElementId = Ex12ExRenEntryParser.ParseUInt16(additionalRenEntryIdsEx, index);
					}
					else
					{
						entry.Unknown = new byte[(int)num2];
						Array.Copy(additionalRenEntryIdsEx, index, entry.Unknown, 0, (int)num2);
					}
				}
				catch (ArgumentException innerException)
				{
					throw new FormatException("ParseEntryBlock", innerException);
				}
				index += (int)num2;
				blockSize -= num2;
				entry.Start = start;
				entry.End = index;
				if (blockSize <= 0)
				{
					return;
				}
			}
		}

		// Token: 0x06004423 RID: 17443 RVA: 0x00122A18 File Offset: 0x00120C18
		private static ushort ParseUInt16(byte[] bytes, int index)
		{
			ushort result;
			try
			{
				result = BitConverter.ToUInt16(bytes, index);
			}
			catch (ArgumentException innerException)
			{
				throw new FormatException("ParseUInt16", innerException);
			}
			return result;
		}

		// Token: 0x06004424 RID: 17444 RVA: 0x00122A50 File Offset: 0x00120C50
		private void Parse()
		{
			int num = 0;
			if (this.entryBlob != null && this.entryBlob.Length > 1)
			{
				for (;;)
				{
					ushort num2 = Ex12ExRenEntryParser.ParseUInt16(this.entryBlob, num);
					if (num2 == 0)
					{
						break;
					}
					Ex12ExRenEntryParser.ExFolderEntry exFolderEntry = this.GetExFolderEntry(num2);
					num += 2;
					ushort num3 = Ex12ExRenEntryParser.ParseUInt16(this.entryBlob, num);
					num += 2;
					int num4 = num;
					while (num - num4 < (int)num3)
					{
						Ex12ExRenEntryParser.ParseEntryBlock(this.entryBlob, ref num, num3, ref exFolderEntry);
					}
					if (num + 2 >= this.entryBlob.Length)
					{
						return;
					}
				}
				return;
			}
		}

		// Token: 0x06004425 RID: 17445 RVA: 0x00122ACC File Offset: 0x00120CCC
		private bool ReplaceEntryBlock(Ex12RenEntryIdStrategy.PersistenceId persistenceId, Ex12ExRenEntryParser.ExFolderEntry exFolderEntry)
		{
			Ex12ExRenEntryParser.ExFolderEntry exFolderEntry2 = null;
			if (this.defaultFolderRenEx.TryGetValue((ushort)persistenceId, out exFolderEntry2))
			{
				byte[] array = exFolderEntry.ToBytes();
				if (array.Length == exFolderEntry2.End - exFolderEntry2.Start)
				{
					exFolderEntry.Start = exFolderEntry2.Start;
					exFolderEntry.End = array.Length + exFolderEntry.Start;
					this.defaultFolderRenEx[(ushort)persistenceId] = exFolderEntry;
					this.entryBlob = Ex12ExRenEntryParser.ReplaceBlob(this.entryBlob, exFolderEntry2.Start, exFolderEntry2.End, array);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004426 RID: 17446 RVA: 0x00122B50 File Offset: 0x00120D50
		private void InsertEntryBlock(Ex12RenEntryIdStrategy.PersistenceId persistenceId, Ex12ExRenEntryParser.ExFolderEntry exFolderEntry)
		{
			this.defaultFolderRenEx[(ushort)persistenceId] = exFolderEntry;
			int num = (this.entryBlob == null) ? 0 : this.entryBlob.Length;
			int num2;
			int num3;
			byte[] array = exFolderEntry.ToBlock(persistenceId, out num2, out num3);
			this.entryBlob = Ex12ExRenEntryParser.ReplaceBlob(this.entryBlob, 0, 0, array);
			exFolderEntry.Start = num + num2;
			exFolderEntry.End = num + array.Length;
		}

		// Token: 0x06004427 RID: 17447 RVA: 0x00122BB2 File Offset: 0x00120DB2
		private Ex12ExRenEntryParser.ExFolderEntry GetExFolderEntry(ushort id)
		{
			if (id >= 32769)
			{
			}
			if (!this.defaultFolderRenEx.ContainsKey(id))
			{
				this.defaultFolderRenEx[id] = new Ex12ExRenEntryParser.ExFolderEntry();
			}
			return this.defaultFolderRenEx[id];
		}

		// Token: 0x04002535 RID: 9525
		private const int UInt16Size = 2;

		// Token: 0x04002536 RID: 9526
		private Dictionary<ushort, Ex12ExRenEntryParser.ExFolderEntry> defaultFolderRenEx;

		// Token: 0x04002537 RID: 9527
		private byte[] entryBlob;

		// Token: 0x02000671 RID: 1649
		internal enum ElementIdType
		{
			// Token: 0x04002539 RID: 9529
			EntryId = 1,
			// Token: 0x0400253A RID: 9530
			Header
		}

		// Token: 0x02000672 RID: 1650
		internal class ExFolderEntry
		{
			// Token: 0x06004428 RID: 17448 RVA: 0x00122BEF File Offset: 0x00120DEF
			internal ExFolderEntry()
			{
				this.elementId = 0;
				this.entryId = null;
				this.unknown = null;
			}

			// Token: 0x06004429 RID: 17449 RVA: 0x00122C0C File Offset: 0x00120E0C
			internal ExFolderEntry(ushort elementId, byte[] entryId, int start, int end)
			{
				this.elementId = elementId;
				this.entryId = entryId;
				this.unknown = null;
				this.start = start;
				this.end = end;
			}

			// Token: 0x170013EA RID: 5098
			// (get) Token: 0x0600442A RID: 17450 RVA: 0x00122C38 File Offset: 0x00120E38
			// (set) Token: 0x0600442B RID: 17451 RVA: 0x00122C40 File Offset: 0x00120E40
			internal int Start
			{
				get
				{
					return this.start;
				}
				set
				{
					this.start = value;
				}
			}

			// Token: 0x170013EB RID: 5099
			// (get) Token: 0x0600442C RID: 17452 RVA: 0x00122C49 File Offset: 0x00120E49
			// (set) Token: 0x0600442D RID: 17453 RVA: 0x00122C51 File Offset: 0x00120E51
			internal int End
			{
				get
				{
					return this.end;
				}
				set
				{
					this.end = value;
				}
			}

			// Token: 0x170013EC RID: 5100
			// (get) Token: 0x0600442E RID: 17454 RVA: 0x00122C5A File Offset: 0x00120E5A
			// (set) Token: 0x0600442F RID: 17455 RVA: 0x00122C62 File Offset: 0x00120E62
			internal byte[] EntryId
			{
				get
				{
					return this.entryId;
				}
				set
				{
					this.entryId = value;
				}
			}

			// Token: 0x170013ED RID: 5101
			// (get) Token: 0x06004430 RID: 17456 RVA: 0x00122C6B File Offset: 0x00120E6B
			// (set) Token: 0x06004431 RID: 17457 RVA: 0x00122C73 File Offset: 0x00120E73
			internal byte[] Unknown
			{
				get
				{
					return this.unknown;
				}
				set
				{
					this.unknown = value;
				}
			}

			// Token: 0x170013EE RID: 5102
			// (get) Token: 0x06004432 RID: 17458 RVA: 0x00122C7C File Offset: 0x00120E7C
			// (set) Token: 0x06004433 RID: 17459 RVA: 0x00122C84 File Offset: 0x00120E84
			internal ushort ElementId
			{
				get
				{
					return this.elementId;
				}
				set
				{
					this.elementId = value;
				}
			}

			// Token: 0x06004434 RID: 17460 RVA: 0x00122C90 File Offset: 0x00120E90
			internal byte[] ToBytes()
			{
				MemoryStream memoryStream = new MemoryStream();
				Marshal.SizeOf(typeof(ushort));
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					binaryWriter.Write(this.elementId);
					binaryWriter.Write((ushort)this.entryId.Length);
					binaryWriter.Write(this.entryId);
				}
				return memoryStream.ToArray();
			}

			// Token: 0x06004435 RID: 17461 RVA: 0x00122D04 File Offset: 0x00120F04
			internal byte[] ToBlock(Ex12RenEntryIdStrategy.PersistenceId persistenceId, out int entryStart, out int entryEnd)
			{
				MemoryStream memoryStream = new MemoryStream();
				int num = Marshal.SizeOf(typeof(ushort));
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					binaryWriter.Seek(2 * num, SeekOrigin.Begin);
					entryStart = (int)binaryWriter.BaseStream.Position;
					binaryWriter.Write(this.ElementId);
					binaryWriter.Write((ushort)this.EntryId.Length);
					binaryWriter.Write(this.EntryId);
					entryEnd = (int)binaryWriter.BaseStream.Position;
					int num2 = (int)binaryWriter.BaseStream.Position - 2 * num;
					binaryWriter.Seek(0, SeekOrigin.Begin);
					binaryWriter.Write((ushort)persistenceId);
					binaryWriter.Write((ushort)num2);
				}
				return memoryStream.ToArray();
			}

			// Token: 0x0400253B RID: 9531
			private ushort elementId;

			// Token: 0x0400253C RID: 9532
			private byte[] entryId;

			// Token: 0x0400253D RID: 9533
			private byte[] unknown;

			// Token: 0x0400253E RID: 9534
			private int start;

			// Token: 0x0400253F RID: 9535
			private int end;
		}
	}
}
