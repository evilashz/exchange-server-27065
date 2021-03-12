using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.OAB;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.OABGenerator
{
	// Token: 0x020001E3 RID: 483
	internal class DiffFileGenerator
	{
		// Token: 0x060012B9 RID: 4793 RVA: 0x0006D0C8 File Offset: 0x0006B2C8
		public DiffFileGenerator(OABFile oldFile, OABFile newFile, Action abortProcessingOnShutdown, GenerationStats stats)
		{
			this.oldFile = oldFile;
			this.newFile = newFile;
			this.stats = stats;
			this.abortProcessingOnShutdown = abortProcessingOnShutdown;
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x0006D0EF File Offset: 0x0006B2EF
		public DiffFileGenerator(OABFile oldFile, OABFile newFile, GenerationStats stats) : this(oldFile, newFile, delegate()
		{
		}, stats)
		{
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x0006D118 File Offset: 0x0006B318
		public OABFile GenerateDiffFile(FileSet fileSet)
		{
			this.abortProcessingOnShutdown();
			FileStream fileStream = fileSet.Create("CHG");
			this.diffFile = new OABFile(fileStream, OABDataFileType.Diff);
			bool flag = false;
			using (IOCostStream iocostStream = new IOCostStream(new NoCloseStream(fileStream)))
			{
				using (new FileSystemPerformanceTracker("FinishGeneratingAddressListFiles.GenerateDiffFiles", iocostStream, this.stats))
				{
					flag = this.CreatePatch(iocostStream);
				}
			}
			if (flag)
			{
				this.diffFile.SequenceNumber = (this.newFile.SequenceNumber += 1U);
				this.diffFile.DnToUseInOABFile = this.newFile.DnToUseInOABFile;
				this.diffFile.NameToUseInOABFile = this.newFile.NameToUseInOABFile;
				this.diffFile.Guid = this.newFile.Guid;
				this.diffFile.UncompressedFileSize = this.newFile.UncompressedFileSize;
				this.diffFile.CompressedFileSize = (uint)this.diffFile.UncompressedFileStream.Length;
				this.diffFile.CompressedFileHash = OABFileHash.GetHash(this.diffFile.UncompressedFileStream);
				return this.diffFile;
			}
			this.diffFile = null;
			return null;
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x0006D26C File Offset: 0x0006B46C
		private bool CreatePatch(Stream diffStream)
		{
			this.abortProcessingOnShutdown();
			bool result;
			using (DataPatching dataPatching = new DataPatching(Globals.MaxCompressionBlockSize, Globals.MaxCompressionBlockSize, Globals.MaxCompressionBlockSize))
			{
				using (IOCostStream iocostStream = new IOCostStream(new NoCloseStream(this.oldFile.UncompressedFileStream)))
				{
					using (new FileSystemPerformanceTracker("FinishGeneratingAddressListFiles.GenerateDiffFiles", iocostStream, this.stats))
					{
						using (IOCostStream iocostStream2 = new IOCostStream(new NoCloseStream(this.newFile.UncompressedFileStream)))
						{
							using (new FileSystemPerformanceTracker("FinishGeneratingAddressListFiles.GenerateDiffFiles", iocostStream2, this.stats))
							{
								byte[] array = new byte[Globals.MaxCompressionBlockSize];
								byte[] array2 = new byte[Globals.MaxCompressionBlockSize];
								int num;
								uint oldCRC;
								DiffFileGenerator.ReadFileHeader(iocostStream, array, out num, out oldCRC);
								int num2;
								uint newCRC;
								DiffFileGenerator.ReadFileHeader(iocostStream2, array2, out num2, out newCRC);
								this.WriteDiffFileHeader(diffStream, oldCRC, newCRC);
								uint num3 = 0U;
								uint num4 = 0U;
								long num5 = iocostStream.Length - (long)num;
								long num6 = iocostStream2.Length - (long)num2;
								byte[] array3 = null;
								byte[] array4 = null;
								int num7 = 0;
								int num8 = 0;
								Guid? guid = null;
								Guid? guid2 = null;
								byte[] propertyDescriptorsBuffer = null;
								byte[] propertyDescriptorsBuffer2 = null;
								int headerPropertyDescriptorsCount = 0;
								int headerPropertyDescriptorsCount2 = 0;
								int num9 = 0;
								int num10 = 0;
								bool flag = false;
								int num11 = 0;
								for (;;)
								{
									this.abortProcessingOnShutdown();
									if (num5 != 0L && num11 <= 0 && num7 == 0)
									{
										DiffFileGenerator.ReadNextRecord(iocostStream, true, ref num3, ref num5, ref array3, ref num7, ref guid, ref propertyDescriptorsBuffer, ref headerPropertyDescriptorsCount, ref num9);
									}
									if (num6 != 0L && num11 >= 0 && num8 == 0)
									{
										DiffFileGenerator.ReadNextRecord(iocostStream2, false, ref num4, ref num6, ref array4, ref num8, ref guid2, ref propertyDescriptorsBuffer2, ref headerPropertyDescriptorsCount2, ref num10);
									}
									bool flag2 = false;
									if (guid != null && guid2 != null)
									{
										num11 = guid.Value.CompareTo(guid2.Value);
									}
									else if (guid == null && guid2 != null)
									{
										flag = true;
										num11 = 2;
									}
									else
									{
										if (guid == null || guid2 != null)
										{
											break;
										}
										flag = true;
										num11 = -2;
									}
									if (num11 == 0 && num > 0 && num2 > 0 && (num + array3.Length > Globals.MaxCompressionBlockSize || num2 + array4.Length > Globals.MaxCompressionBlockSize))
									{
										flag2 = true;
									}
									else
									{
										if (num11 != 0)
										{
											flag = true;
											if (num11 < 0)
											{
												this.stats.IncrementRecordsDeletedChurn();
											}
											else
											{
												this.stats.IncrementRecordsAddedChurn();
											}
										}
										else if (num3 == 2U && num4 == 2U)
										{
											string a = (string)DiffFileGenerator.GetHeaderPropertyValue(OABFilePropTags.OabName, array3, propertyDescriptorsBuffer, headerPropertyDescriptorsCount, true);
											string b = (string)DiffFileGenerator.GetHeaderPropertyValue(OABFilePropTags.OabName, array4, propertyDescriptorsBuffer2, headerPropertyDescriptorsCount2, false);
											flag = (a != b);
										}
										else if (array3.Length != array4.Length || !array3.SequenceEqual(array4))
										{
											flag = true;
											this.stats.IncrementRecordsModifiedChurn();
										}
										if (num11 <= 0)
										{
											if (num + array3.Length <= Globals.MaxCompressionBlockSize || array3.Length > Globals.MaxCompressionBlockSize)
											{
												DiffFileGenerator.ConsumeRecord(array, ref num, array3, ref num7, ref guid, ref flag2);
											}
											else
											{
												flag2 = true;
											}
										}
										if (num11 >= 0)
										{
											if (num2 + array4.Length <= Globals.MaxCompressionBlockSize || array4.Length > Globals.MaxCompressionBlockSize)
											{
												DiffFileGenerator.ConsumeRecord(array2, ref num2, array4, ref num8, ref guid2, ref flag2);
											}
											else
											{
												flag2 = true;
											}
										}
									}
									if (flag2)
									{
										this.WriteLzxPatch(diffStream, dataPatching, array, num, array2, num2);
										num = 0;
										num2 = 0;
									}
								}
								if (num5 == 0L && num6 != 0L)
								{
								}
								if (num != 0 || num2 != 0)
								{
									this.WriteLzxPatch(diffStream, dataPatching, array, num, array2, num2);
								}
								result = flag;
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x0006D664 File Offset: 0x0006B864
		private void WriteDiffFileHeader(Stream stream, uint oldCRC, uint newCRC)
		{
			List<byte> list = new List<byte>();
			list.AddRange(BitConverter.GetBytes(3));
			list.AddRange(BitConverter.GetBytes(2));
			list.AddRange(BitConverter.GetBytes(Globals.MaxCompressionBlockSize));
			list.AddRange(BitConverter.GetBytes(this.oldFile.UncompressedFileSize));
			list.AddRange(BitConverter.GetBytes(this.newFile.UncompressedFileSize));
			list.AddRange(BitConverter.GetBytes(oldCRC));
			list.AddRange(BitConverter.GetBytes(newCRC));
			stream.Write(list.ToArray(), 0, list.Count);
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x0006D6F8 File Offset: 0x0006B8F8
		private void WriteLzxPatch(Stream stream, DataPatching patchGenerator, byte[] oldBuffer, int oldSize, byte[] newBuffer, int newSize)
		{
			if (oldBuffer.Length != oldSize)
			{
				oldBuffer = DiffFileGenerator.GetArraySegment(oldBuffer, 0, oldSize);
			}
			if (newBuffer.Length != newSize)
			{
				newBuffer = DiffFileGenerator.GetArraySegment(newBuffer, 0, newSize);
			}
			byte[] array;
			if (patchGenerator.TryGenerate(oldBuffer, newBuffer, out array))
			{
				uint crc = OABCRC.ComputeCRC(OABCRC.DefaultSeed, newBuffer);
				this.WritePatchBlock(stream, oldBuffer.Length, newBuffer.Length, array, array.Length, crc);
				return;
			}
			byte[][] array2 = DiffFileGenerator.SplitByteArray(oldBuffer);
			byte[][] array3 = DiffFileGenerator.SplitByteArray(newBuffer);
			for (int i = 0; i < 2; i++)
			{
				if (patchGenerator.TryGenerate(array2[i], array3[i], out array))
				{
					uint crc2 = OABCRC.ComputeCRC(OABCRC.DefaultSeed, array3[i]);
					this.WritePatchBlock(stream, array2[i].Length, array3[i].Length, array, array.Length, crc2);
				}
			}
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x0006D7B4 File Offset: 0x0006B9B4
		private static byte[][] SplitByteArray(byte[] byteArray)
		{
			int num = byteArray.Length / 2;
			int length = byteArray.Length - num;
			return new byte[][]
			{
				DiffFileGenerator.GetArraySegment(byteArray, 0, num),
				DiffFileGenerator.GetArraySegment(byteArray, num, length)
			};
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x0006D7EC File Offset: 0x0006B9EC
		private static byte[] GetArraySegment(byte[] byteArray, int start, int length)
		{
			byte[] array = new byte[length];
			Array.Copy(byteArray, start, array, 0, length);
			return array;
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x0006D80C File Offset: 0x0006BA0C
		private void WritePatchBlock(Stream stream, int oldSize, int newSize, byte[] patchData, int patchSize, uint crc32)
		{
			List<byte> list = new List<byte>();
			list.AddRange(BitConverter.GetBytes(patchSize));
			list.AddRange(BitConverter.GetBytes(newSize));
			list.AddRange(BitConverter.GetBytes(oldSize));
			list.AddRange(BitConverter.GetBytes(crc32));
			stream.Write(list.ToArray(), 0, list.Count);
			stream.Write(patchData, 0, patchSize);
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x0006D86F File Offset: 0x0006BA6F
		private static void ReadFileHeader(Stream stream, byte[] buffer, out int numberOfBytesRead, out uint crc)
		{
			stream.Seek(0L, SeekOrigin.Begin);
			numberOfBytesRead = 12;
			stream.Read(buffer, 0, numberOfBytesRead);
			crc = BitConverter.ToUInt32(buffer, 4);
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x0006D894 File Offset: 0x0006BA94
		private static int GetPropertyOffset(PropTag propTagToFind, byte[] propertyDescriptorsBuffer, int propertyDescriptorsOffset, int propertyCount, byte[] recordBuffer)
		{
			int num = -1;
			for (int i = 0; i < propertyCount; i++)
			{
				PropTag propTag = (PropTag)BitConverter.ToUInt32(propertyDescriptorsBuffer, propertyDescriptorsOffset + i * 2 * 4);
				if (propTag == propTagToFind)
				{
					num = i;
					break;
				}
			}
			if (num == -1)
			{
				return 0;
			}
			if (((int)recordBuffer[4 + num / 8] & 128 >> num % 8) == 0)
			{
				return 0;
			}
			int result = 4 + (propertyCount + 7) / 8;
			for (int j = 0; j < num; j++)
			{
				if (((int)recordBuffer[4 + j / 8] & 128 >> j % 8) != 0)
				{
					PropTag propTag2 = (PropTag)BitConverter.ToUInt32(propertyDescriptorsBuffer, propertyDescriptorsOffset + j * 2 * 4);
					DiffFileGenerator.SkipProperty(propTag2, recordBuffer, ref result);
				}
			}
			return result;
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x0006D934 File Offset: 0x0006BB34
		private static void SkipProperty(PropTag propTag, byte[] recordBuffer, ref int propertyOffset)
		{
			int i = 1;
			int num = 0;
			if (propTag.IsMultiValued())
			{
				DiffFileGenerator.GetV4IntegerValue(recordBuffer, ref propertyOffset, out i);
			}
			while (i > 0)
			{
				PropType propType = propTag.ValueType() & (PropType)(-4097);
				if (propType <= PropType.Boolean)
				{
					if (propType != PropType.Int && propType != PropType.Boolean)
					{
						goto IL_69;
					}
					DiffFileGenerator.GetV4IntegerValue(recordBuffer, ref propertyOffset, out num);
				}
				else
				{
					switch (propType)
					{
					case PropType.AnsiString:
					case PropType.String:
						while (recordBuffer[propertyOffset++] != 0)
						{
						}
						break;
					default:
						if (propType != PropType.Binary)
						{
							goto IL_69;
						}
						goto IL_69;
					}
				}
				IL_78:
				i--;
				continue;
				IL_69:
				DiffFileGenerator.GetV4IntegerValue(recordBuffer, ref propertyOffset, out num);
				propertyOffset += num;
				goto IL_78;
			}
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x0006D9C4 File Offset: 0x0006BBC4
		private static object GetPropertyValue(PropTag propTag, byte[] recordBuffer, ref int propertyOffset, bool isFromOldFile)
		{
			PropType propType = propTag.ValueType() & (PropType)(-4097);
			object result;
			switch (propType)
			{
			case PropType.AnsiString:
			case PropType.String:
			{
				int num = propertyOffset;
				while (recordBuffer[propertyOffset] != 0)
				{
					propertyOffset++;
				}
				result = Encoding.UTF8.GetString(recordBuffer, num, propertyOffset - num);
				break;
			}
			default:
			{
				if (propType != PropType.Binary)
				{
					throw new NotSupportedException(string.Format("GetProperty doesn't support property type {0} now", propTag.ValueType()));
				}
				int num2;
				DiffFileGenerator.GetV4IntegerValue(recordBuffer, ref propertyOffset, out num2);
				if (num2 <= 0)
				{
					OABLogger.LogRecord(TraceType.FatalTrace, "Invalid binary property data for tag {0} from {1} file . The offset is {2}. The binary blob is {3}", new object[]
					{
						propTag.ToString(),
						isFromOldFile ? "old" : "new",
						propertyOffset,
						BitConverter.ToString(recordBuffer)
					});
					throw new InvalidDataException(string.Format("Invalid binary property data for tag {0}", propTag.ToString()));
				}
				byte[] array = new byte[num2];
				Array.Copy(recordBuffer, propertyOffset, array, 0, num2);
				propertyOffset += num2;
				result = array;
				break;
			}
			}
			return result;
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x0006DAD0 File Offset: 0x0006BCD0
		private static void GetV4IntegerValue(byte[] recordBuffer, ref int propertyOffset, out int integerValue)
		{
			integerValue = 0;
			if (recordBuffer[propertyOffset] == 128)
			{
				return;
			}
			if (recordBuffer[propertyOffset] == 129)
			{
				propertyOffset++;
				integerValue = (int)recordBuffer[propertyOffset++];
				return;
			}
			if (recordBuffer[propertyOffset] == 130)
			{
				propertyOffset++;
				integerValue = (int)recordBuffer[propertyOffset++];
				integerValue |= (int)recordBuffer[propertyOffset++] << 8;
				return;
			}
			if (recordBuffer[propertyOffset] == 131)
			{
				propertyOffset++;
				integerValue = (int)recordBuffer[propertyOffset++];
				integerValue |= (int)recordBuffer[propertyOffset++] << 8;
				integerValue |= (int)recordBuffer[propertyOffset++] << 16;
				return;
			}
			if (recordBuffer[propertyOffset] == 132)
			{
				propertyOffset++;
				integerValue = (int)recordBuffer[propertyOffset++];
				integerValue |= (int)recordBuffer[propertyOffset++] << 8;
				integerValue |= (int)recordBuffer[propertyOffset++] << 16;
				integerValue |= (int)recordBuffer[propertyOffset++] << 24;
				return;
			}
			if ((recordBuffer[propertyOffset] & 128) != 0)
			{
				return;
			}
			integerValue = (int)recordBuffer[propertyOffset++];
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x0006DC00 File Offset: 0x0006BE00
		private static byte[] ReadNextUncompressedRecord(Stream fileStream)
		{
			byte[] array = new byte[4];
			fileStream.Read(array, 0, array.Length);
			int num = BitConverter.ToInt32(array, 0);
			if (num < array.Length)
			{
				string text = "(no name)";
				Stream stream = fileStream;
				while (stream != null && stream is BaseStream)
				{
					stream = ((((BaseStream)stream).InnerStream == stream) ? null : ((BaseStream)stream).InnerStream);
				}
				if (stream != null && stream is FileStream)
				{
					text = ((FileStream)stream).Name;
				}
				throw new InvalidDataException(string.Format("DiffFileGenerator.ReadNextUncompressedStream: recordSize < recordSizeBuffer.Length.  recordSize = {0}, recordSizeBuffer.Length = {1}, fileStream.Name = {2}, fileStream.Length = {3}, fileStream.Position = {4}", new object[]
				{
					num,
					array.Length,
					text,
					fileStream.Length,
					fileStream.Position
				}));
			}
			byte[] array2 = new byte[num];
			Array.Copy(array, array2, array.Length);
			fileStream.Read(array2, array.Length, num - array.Length);
			return array2;
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x0006DCF4 File Offset: 0x0006BEF4
		private static object GetHeaderPropertyValue(PropTag propTag, byte[] recordBuffer, byte[] propertyDescriptorsBuffer, int headerPropertyDescriptorsCount, bool isFromOldFile)
		{
			int propertyOffset = DiffFileGenerator.GetPropertyOffset(propTag, propertyDescriptorsBuffer, 8, headerPropertyDescriptorsCount, recordBuffer);
			return DiffFileGenerator.GetPropertyValue(propTag, recordBuffer, ref propertyOffset, isFromOldFile);
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x0006DD18 File Offset: 0x0006BF18
		private static object GetDetailPropertyValue(PropTag propTag, byte[] recordBuffer, byte[] propertyDescriptorsBuffer, int headerPropertyDescriptorsCount, int detailPropertyDescriptorsCount, bool isFromOldFile)
		{
			int propertyOffset = DiffFileGenerator.GetPropertyOffset(propTag, propertyDescriptorsBuffer, 12 + headerPropertyDescriptorsCount * 2 * 4, detailPropertyDescriptorsCount, recordBuffer);
			return DiffFileGenerator.GetPropertyValue(propTag, recordBuffer, ref propertyOffset, isFromOldFile);
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x0006DD44 File Offset: 0x0006BF44
		private static void ConsumeRecord(byte[] fileBuffer, ref int bufferLength, byte[] recordBuffer, ref int recordBytesRemaining, ref Guid? objectGuid, ref bool patchBufferIsFull)
		{
			int num = Math.Min(recordBytesRemaining, fileBuffer.Length - bufferLength);
			Array.Copy(recordBuffer, recordBuffer.Length - recordBytesRemaining, fileBuffer, bufferLength, num);
			recordBytesRemaining -= num;
			bufferLength += num;
			if (bufferLength == Globals.MaxCompressionBlockSize)
			{
				patchBufferIsFull = true;
			}
			if (recordBytesRemaining == 0)
			{
				objectGuid = null;
			}
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x0006DD94 File Offset: 0x0006BF94
		private static void ReadNextRecord(Stream stream, bool isFromOldFile, ref uint recordCount, ref long fileBytesRemaining, ref byte[] recordBuffer, ref int recordBytesRemaining, ref Guid? objectGuid, ref byte[] propertyDescriptorsBuffer, ref int headerPropertyDescriptorsCount, ref int detailPropertyDescriptorsCount)
		{
			recordBuffer = DiffFileGenerator.ReadNextUncompressedRecord(stream);
			fileBytesRemaining -= (long)((ulong)recordBuffer.Length);
			recordBytesRemaining = recordBuffer.Length;
			recordCount += 1U;
			if (recordCount > 2U)
			{
				object detailPropertyValue = DiffFileGenerator.GetDetailPropertyValue((PropTag)2355953922U, recordBuffer, propertyDescriptorsBuffer, headerPropertyDescriptorsCount, detailPropertyDescriptorsCount, isFromOldFile);
				objectGuid = new Guid?(new Guid((byte[])detailPropertyValue));
				return;
			}
			if (recordCount == 1U)
			{
				propertyDescriptorsBuffer = recordBuffer;
				headerPropertyDescriptorsCount = BitConverter.ToInt32(propertyDescriptorsBuffer, 4);
				detailPropertyDescriptorsCount = BitConverter.ToInt32(propertyDescriptorsBuffer, 8 + headerPropertyDescriptorsCount * 2 * 4);
				OABLogger.LogRecord(TraceType.InfoTrace, "PropertyDescriptorsBuffer is: {0}", new object[]
				{
					BitConverter.ToString(propertyDescriptorsBuffer)
				});
				OABLogger.LogRecord(TraceType.InfoTrace, "Header property count is {0}, detail property count is {1}", new object[]
				{
					headerPropertyDescriptorsCount,
					detailPropertyDescriptorsCount
				});
			}
			objectGuid = new Guid?(Guid.Empty);
		}

		// Token: 0x04000B5B RID: 2907
		private readonly Action abortProcessingOnShutdown;

		// Token: 0x04000B5C RID: 2908
		private OABFile oldFile;

		// Token: 0x04000B5D RID: 2909
		private OABFile newFile;

		// Token: 0x04000B5E RID: 2910
		private OABFile diffFile;

		// Token: 0x04000B5F RID: 2911
		private readonly GenerationStats stats;
	}
}
