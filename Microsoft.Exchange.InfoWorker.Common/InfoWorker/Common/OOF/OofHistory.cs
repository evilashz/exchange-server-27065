using System;
using System.IO;
using System.Threading;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.OOF;
using Microsoft.Mapi;

namespace Microsoft.Exchange.InfoWorker.Common.OOF
{
	// Token: 0x0200003A RID: 58
	internal static class OofHistory
	{
		// Token: 0x06000117 RID: 279 RVA: 0x00007364 File Offset: 0x00005564
		internal static void ClearOofHistory(MailboxSession itemStore, bool oofEnabled)
		{
			MapiFolder mapiFolder = null;
			MapiFolder mapiFolder2 = null;
			MapiStream mapiStream = null;
			bool flag = false;
			byte[] initialBytes = oofEnabled ? OofHistory.InitialBytesOofStateOn : OofHistory.InitialBytesOofStateOff;
			try
			{
				if (OofHistory.TryOpenFolder(itemStore, out mapiFolder, out mapiFolder2))
				{
					long num = (long)itemStore.GetHashCode();
					bool flag2;
					mapiStream = OofHistory.OpenAndLockStream(mapiFolder2, initialBytes, num, out flag2);
					flag = true;
					if (!flag2)
					{
						byte[] array = new byte[2];
						if (2 != mapiStream.Read(array, 0, 2))
						{
							OofHistory.ResetOofHistoryProperty(mapiStream, initialBytes, num);
						}
						else
						{
							bool flag3 = 0 != array[1];
							if (flag3 == oofEnabled)
							{
								OofHistory.Tracer.TraceDebug<bool>(num, "Current OOF state {0} matches in OOF history and mailbox properties, clear is not executed.", oofEnabled);
							}
							else
							{
								OofHistory.ResetOofHistoryProperty(mapiStream, initialBytes, num);
							}
						}
					}
				}
			}
			finally
			{
				if (mapiStream != null)
				{
					try
					{
						if (flag)
						{
							mapiStream.UnlockRegion(0L, 1L, 1);
						}
					}
					finally
					{
						mapiStream.Dispose();
					}
				}
				if (mapiFolder != null)
				{
					mapiFolder.Dispose();
					mapiFolder = null;
				}
				if (mapiFolder2 != null)
				{
					mapiFolder2.Dispose();
					mapiFolder2 = null;
				}
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000745C File Offset: 0x0000565C
		internal static void RemoveOofHistoryEntriesWithProperty(MailboxSession itemStore, bool oofEnabled, OofHistory.PropId propId, byte[] propValue)
		{
			MapiFolder mapiFolder = null;
			MapiFolder mapiFolder2 = null;
			MapiStream mapiStream = null;
			bool flag = false;
			byte[] initialBytes = oofEnabled ? OofHistory.InitialBytesOofStateOn : OofHistory.InitialBytesOofStateOff;
			try
			{
				if (OofHistory.TryOpenFolder(itemStore, out mapiFolder, out mapiFolder2))
				{
					long hashCode = (long)itemStore.GetHashCode();
					bool flag2;
					mapiStream = OofHistory.OpenAndLockStream(mapiFolder2, initialBytes, hashCode, out flag2);
					flag = true;
					if (!flag2)
					{
						int num;
						if (OofHistory.TryGetStreamLengthForEntryRemoval(hashCode, mapiStream, out num))
						{
							byte[] buffer;
							if (OofHistory.TryReadAllFromStream(hashCode, mapiStream, num, out buffer))
							{
								int num2 = OofHistory.RemoveEntriesWithProperty(num, buffer, propId, propValue, hashCode);
								if (num2 < num)
								{
									mapiStream.Seek(0L, SeekOrigin.Begin);
									mapiStream.Write(buffer, 0, num2);
									mapiStream.SetLength((long)num2);
								}
							}
						}
					}
				}
			}
			finally
			{
				if (mapiStream != null)
				{
					try
					{
						if (flag)
						{
							mapiStream.UnlockRegion(0L, 1L, 1);
						}
					}
					finally
					{
						mapiStream.Dispose();
					}
				}
				if (mapiFolder != null)
				{
					mapiFolder.Dispose();
					mapiFolder = null;
				}
				if (mapiFolder2 != null)
				{
					mapiFolder2.Dispose();
					mapiFolder2 = null;
				}
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00007558 File Offset: 0x00005758
		private static bool TryOpenFolder(MailboxSession itemStore, out MapiFolder nonIpmSubtreeFolder, out MapiFolder oofHistoryFolder)
		{
			MapiStore _ContainedMapiStore = itemStore.__ContainedMapiStore;
			nonIpmSubtreeFolder = _ContainedMapiStore.GetNonIpmSubtreeFolder();
			oofHistoryFolder = null;
			if (nonIpmSubtreeFolder == null)
			{
				OofHistory.Tracer.TraceError((long)itemStore.GetHashCode(), "Unable to open the non ipm subtree folder, OOF history operation is not executed.");
				return false;
			}
			try
			{
				oofHistoryFolder = nonIpmSubtreeFolder.OpenSubFolderByName("Freebusy Data");
			}
			catch (MapiExceptionNotFound)
			{
				OofHistory.Tracer.TraceError<string>((long)itemStore.GetHashCode(), "Unable to open the OOF history folder {0}, OOF history operation is not executed.", "Freebusy Data");
				return false;
			}
			return true;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000075D8 File Offset: 0x000057D8
		private static MapiStream OpenAndLockStream(MapiFolder oofHistoryFolder, byte[] initialBytes, long hashCode, out bool isNew)
		{
			MapiStream mapiStream = null;
			isNew = false;
			OpenPropertyFlags openPropertyFlags = OpenPropertyFlags.Modify | OpenPropertyFlags.DeferredErrors;
			try
			{
				mapiStream = oofHistoryFolder.OpenStream(PropTag.OofHistory, openPropertyFlags);
				OofHistory.LockStreamWithRetry(mapiStream, hashCode);
			}
			catch (MapiExceptionNotFound)
			{
				if (mapiStream != null)
				{
					mapiStream.Dispose();
					mapiStream = null;
				}
				OofHistory.Tracer.TraceDebug(hashCode, "OOF history property does not exist, trying to open with create flag.");
				openPropertyFlags |= OpenPropertyFlags.Create;
				mapiStream = oofHistoryFolder.OpenStream(PropTag.OofHistory, openPropertyFlags);
				OofHistory.LockStreamWithRetry(mapiStream, hashCode);
				OofHistory.ResetOofHistoryProperty(mapiStream, initialBytes, hashCode);
				isNew = true;
			}
			return mapiStream;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00007658 File Offset: 0x00005858
		private static void LockStreamWithRetry(MapiStream oofHistoryStream, long hashCode)
		{
			for (int i = 0; i < 6; i++)
			{
				try
				{
					oofHistoryStream.LockRegion(0L, 1L, 1);
					break;
				}
				catch (MapiExceptionLockViolation arg)
				{
					if (i == 5)
					{
						throw;
					}
					OofHistory.Tracer.TraceDebug<int, int, MapiExceptionLockViolation>(hashCode, "Failed to lock on attempt {0}, sleeping {1} milliseconds. Exception encountered is {2}.", i, 100, arg);
					Thread.Sleep(100);
				}
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000076B4 File Offset: 0x000058B4
		private static void ResetOofHistoryProperty(MapiStream oofHistoryStream, byte[] initialBytes, long hashCode)
		{
			OofHistory.Tracer.TraceDebug(hashCode, "Resetting OOF history to initial values.");
			if (!oofHistoryStream.CanWrite)
			{
				throw new InvalidOperationException("OOF history property stream is not writable.");
			}
			if (!oofHistoryStream.CanSeek)
			{
				throw new InvalidOperationException("OOF history property stream is not seekable.");
			}
			if (0L != oofHistoryStream.Position)
			{
				oofHistoryStream.Seek(0L, SeekOrigin.Begin);
			}
			oofHistoryStream.Write(initialBytes, 0, 6);
			if (oofHistoryStream.Length > 6L)
			{
				oofHistoryStream.SetLength(6L);
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00007726 File Offset: 0x00005926
		private static void HandleInsufficientDataCorruption(long hashCode, string message)
		{
			OofHistory.Tracer.TraceError<string>(hashCode, "OOF history insufficient data corruption detected: {0}", message);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000773C File Offset: 0x0000593C
		private static bool TryGetStreamLengthForEntryRemoval(long hashCode, Stream oofHistoryStream, out int historyLength)
		{
			historyLength = 0;
			long length = oofHistoryStream.Length;
			if (7L > length)
			{
				OofHistory.Tracer.TraceDebug(hashCode, "OOF history length is less than 7, it doesn't contain the specified property.");
				return false;
			}
			try
			{
				historyLength = Convert.ToInt32(length);
			}
			catch (OverflowException)
			{
				OofHistory.Tracer.TraceError<long, int>(hashCode, "OOF history data length {0} exceeded the maximum supported length {1}.", length, int.MaxValue);
				return false;
			}
			return true;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000077A4 File Offset: 0x000059A4
		private static bool TryReadAllFromStream(long hashCode, Stream oofHistoryStream, int historyLength, out byte[] buffer)
		{
			buffer = new byte[historyLength];
			int num = oofHistoryStream.Read(buffer, 0, historyLength);
			if (historyLength != num)
			{
				OofHistory.Tracer.TraceError<int, int>(hashCode, "OOF history data corruption detected, only {0} bytes can be read from property having length {1}.", num, historyLength);
				return false;
			}
			return true;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000077E0 File Offset: 0x000059E0
		private static int RemoveEntriesWithProperty(int historyLength, byte[] buffer, OofHistory.PropId propId, byte[] propValue, long hashCode)
		{
			int num = 6;
			int currentEntryStart = 6;
			int result = 6;
			uint num2 = BitConverter.ToUInt32(buffer, 2);
			if (10000U < num2)
			{
				num2 = 10000U;
			}
			uint num3 = num2;
			for (uint num4 = 0U; num4 < num2; num4 += 1U)
			{
				if (num >= historyLength)
				{
					OofHistory.HandleInsufficientDataCorruption(hashCode, "Unable to get property count of entry " + num4);
					return result;
				}
				byte propertyCount = buffer[num++];
				bool flag;
				if (OofHistory.MatchAnyPropertiesInEntry(buffer, propId, propValue, propertyCount, historyLength, num4, hashCode, ref num, out flag))
				{
					num3 -= 1U;
				}
				else
				{
					if (flag)
					{
						return result;
					}
					OofHistory.HandleNonMatchingProperty(buffer, num, currentEntryStart, ref result);
				}
				currentEntryStart = num;
			}
			if (num3 < num2)
			{
				ExBitConverter.Write(num3, buffer, 2);
			}
			return result;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00007884 File Offset: 0x00005A84
		private static void HandleNonMatchingProperty(byte[] buffer, int index, int currentEntryStart, ref int nextEntryStart)
		{
			if (currentEntryStart == nextEntryStart)
			{
				nextEntryStart = index;
				return;
			}
			int num = index - currentEntryStart;
			Buffer.BlockCopy(buffer, currentEntryStart, buffer, nextEntryStart, num);
			nextEntryStart += num;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000078B0 File Offset: 0x00005AB0
		private static bool MatchProperty(byte[] buffer, byte currentPropId, OofHistory.PropId propId, ushort currentPropertySize, byte[] propValue, ref int index)
		{
			if (currentPropId == (byte)propId && (int)currentPropertySize == propValue.Length)
			{
				for (ushort num = 0; num < currentPropertySize; num += 1)
				{
					if (propValue[(int)num] != buffer[index++])
					{
						index += (int)(currentPropertySize - num - 1);
						return false;
					}
				}
				return true;
			}
			index += (int)currentPropertySize;
			return false;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00007900 File Offset: 0x00005B00
		private static bool MatchAnyPropertiesInEntry(byte[] buffer, OofHistory.PropId propId, byte[] propValue, byte propertyCount, int historyLength, uint currentEntry, long hashCode, ref int index, out bool isCorrupt)
		{
			isCorrupt = false;
			bool flag = false;
			for (byte b = 0; b < propertyCount; b += 1)
			{
				if (index >= historyLength)
				{
					OofHistory.HandleInsufficientDataCorruption(hashCode, string.Concat(new object[]
					{
						"Unable to get property id of property ",
						b,
						" entry ",
						currentEntry
					}));
					isCorrupt = true;
					return false;
				}
				byte b2 = buffer[index++];
				if (index + 2 >= historyLength)
				{
					OofHistory.HandleInsufficientDataCorruption(hashCode, string.Concat(new object[]
					{
						"Unable to get property size of property ",
						b,
						" entry ",
						currentEntry,
						" property id ",
						b2,
						" index ",
						index
					}));
					isCorrupt = true;
					return false;
				}
				ushort num = BitConverter.ToUInt16(buffer, index);
				index += 2;
				if (index + (int)num > historyLength)
				{
					OofHistory.HandleInsufficientDataCorruption(hashCode, string.Concat(new object[]
					{
						"Unable to get property content of property ",
						b,
						" entry ",
						currentEntry,
						" property id ",
						b2,
						" property size ",
						num,
						" index ",
						index
					}));
					isCorrupt = true;
					return false;
				}
				if (flag)
				{
					index += (int)num;
				}
				else if (OofHistory.MatchProperty(buffer, b2, propId, num, propValue, ref index))
				{
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00007AB0 File Offset: 0x00005CB0
		// Note: this type is marked as 'beforefieldinit'.
		static OofHistory()
		{
			byte[] array = new byte[6];
			array[0] = 1;
			array[1] = 1;
			OofHistory.InitialBytesOofStateOn = array;
			byte[] array2 = new byte[6];
			array2[0] = 1;
			OofHistory.InitialBytesOofStateOff = array2;
			OofHistory.Tracer = ExTraceGlobals.LegacyOofStateHandlerTracer;
		}

		// Token: 0x040000A9 RID: 169
		internal const int LockRetryLimit = 6;

		// Token: 0x040000AA RID: 170
		internal const int LockRetrySleepMilliSeconds = 100;

		// Token: 0x040000AB RID: 171
		internal const string OofHistoryFolderName = "Freebusy Data";

		// Token: 0x040000AC RID: 172
		private const int HistoryEntryLimit = 10000;

		// Token: 0x040000AD RID: 173
		internal static readonly byte[] InitialBytesOofStateOn;

		// Token: 0x040000AE RID: 174
		internal static readonly byte[] InitialBytesOofStateOff;

		// Token: 0x040000AF RID: 175
		private static readonly Trace Tracer;

		// Token: 0x0200003B RID: 59
		internal enum PropId : byte
		{
			// Token: 0x040000B1 RID: 177
			SenderAddress = 1,
			// Token: 0x040000B2 RID: 178
			GlobalRuleId
		}
	}
}
