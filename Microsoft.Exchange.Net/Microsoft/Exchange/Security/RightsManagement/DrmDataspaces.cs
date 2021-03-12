using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.RightsManagement.StructuredStorage;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x02000978 RID: 2424
	internal static class DrmDataspaces
	{
		// Token: 0x060034A5 RID: 13477 RVA: 0x00082DF8 File Offset: 0x00080FF8
		public static string Read(IStorage rootStorage)
		{
			if (rootStorage == null)
			{
				throw new ArgumentNullException("rootStorage");
			}
			string result = string.Empty;
			StreamOverIStream streamOverIStream = new StreamOverIStream(null);
			BinaryReader binaryReader = new BinaryReader(streamOverIStream);
			IStorage storage = null;
			try
			{
				storage = DrmEmailUtils.EnsureStorage(rootStorage, "\u0006DataSpaces");
				IStream stream = null;
				try
				{
					stream = DrmEmailUtils.EnsureStream(storage, "Version");
					streamOverIStream.ReplaceIStream(stream);
					DrmDataspaces.VersionInfo versionInfo = new DrmDataspaces.VersionInfo("Microsoft.Container.DataSpaces");
					versionInfo.Read(binaryReader);
				}
				finally
				{
					if (stream != null)
					{
						Marshal.ReleaseComObject(stream);
						stream = null;
					}
				}
				IStream stream2 = null;
				try
				{
					stream2 = DrmEmailUtils.EnsureStream(storage, "DataSpaceMap");
					streamOverIStream.ReplaceIStream(stream2);
					DrmDataspaces.DataSpaceMapHeader dataSpaceMapHeader = new DrmDataspaces.DataSpaceMapHeader(1);
					dataSpaceMapHeader.Read(binaryReader);
					DrmDataspaces.DataSpaceMapEntry dataSpaceMapEntry = new DrmDataspaces.DataSpaceMapEntry(new string[][]
					{
						new string[]
						{
							"\tDRMContent",
							"\tDRMDataSpace"
						}
					});
					dataSpaceMapEntry.Read(binaryReader);
				}
				finally
				{
					if (stream2 != null)
					{
						Marshal.ReleaseComObject(stream2);
						stream2 = null;
					}
				}
				IStorage storage2 = null;
				try
				{
					storage2 = DrmEmailUtils.EnsureStorage(storage, "DataSpaceInfo");
					IStream stream3 = null;
					try
					{
						stream3 = DrmEmailUtils.EnsureStream(storage2, "\tDRMDataSpace");
						streamOverIStream.ReplaceIStream(stream3);
						DrmDataspaces.DataTransformHeader dataTransformHeader = new DrmDataspaces.DataTransformHeader(new string[]
						{
							"\tDRMTransform"
						});
						dataTransformHeader.Read(binaryReader);
					}
					finally
					{
						if (stream3 != null)
						{
							Marshal.ReleaseComObject(stream3);
							stream3 = null;
						}
					}
				}
				finally
				{
					if (storage2 != null)
					{
						Marshal.ReleaseComObject(storage2);
						storage2 = null;
					}
				}
				IStorage storage3 = null;
				try
				{
					storage3 = DrmEmailUtils.EnsureStorage(storage, "TransformInfo");
					IStorage storage4 = null;
					try
					{
						storage4 = DrmEmailUtils.EnsureStorage(storage3, "\tDRMTransform");
						IStream stream4 = null;
						try
						{
							stream4 = DrmEmailUtils.EnsureStream(storage4, "\u0006Primary");
							streamOverIStream.ReplaceIStream(stream4);
							DrmDataspaces.DataTransformInfo dataTransformInfo = new DrmDataspaces.DataTransformInfo("{C73DFACD-061F-43B0-8B64-0C620D2A8B50}");
							dataTransformInfo.Read(binaryReader);
							DrmDataspaces.VersionInfo versionInfo2 = new DrmDataspaces.VersionInfo("Microsoft.Metadata.DRMTransform");
							versionInfo2.Read(binaryReader);
							try
							{
								int num = binaryReader.ReadInt32();
								if (num != 4)
								{
									throw new InvalidRpmsgFormatException(DrmStrings.InvalidRpmsgFormat("WrongSizeBeforePublishLicense"));
								}
								result = DrmEmailUtils.ReadUTF8String(binaryReader);
							}
							catch (EndOfStreamException innerException)
							{
								throw new InvalidRpmsgFormatException(DrmStrings.InvalidRpmsgFormat("NotReadPublishLicense"), innerException);
							}
						}
						finally
						{
							if (stream4 != null)
							{
								Marshal.ReleaseComObject(stream4);
								stream4 = null;
							}
						}
					}
					finally
					{
						if (storage4 != null)
						{
							Marshal.ReleaseComObject(storage4);
							storage4 = null;
						}
					}
				}
				finally
				{
					if (storage3 != null)
					{
						Marshal.ReleaseComObject(storage3);
						storage3 = null;
					}
				}
			}
			finally
			{
				if (storage != null)
				{
					Marshal.ReleaseComObject(storage);
					storage = null;
				}
			}
			return result;
		}

		// Token: 0x060034A6 RID: 13478 RVA: 0x00083120 File Offset: 0x00081320
		public static void Write(IStorage rootStorage, string publishLicense)
		{
			if (rootStorage == null)
			{
				throw new ArgumentNullException("rootStorage");
			}
			StreamOverIStream streamOverIStream = new StreamOverIStream(null);
			BufferedStream output = new BufferedStream(streamOverIStream);
			BinaryWriter binaryWriter = new BinaryWriter(output);
			IStorage storage = null;
			try
			{
				storage = rootStorage.CreateStorage("\u0006DataSpaces", 4114, 0, 0);
				IStream stream = null;
				try
				{
					stream = storage.CreateStream("Version", 4114, 0, 0);
					streamOverIStream.ReplaceIStream(stream);
					DrmDataspaces.VersionInfo versionInfo = new DrmDataspaces.VersionInfo("Microsoft.Container.DataSpaces");
					versionInfo.Write(binaryWriter);
					binaryWriter.Flush();
					stream.Commit(STGC.STGC_DEFAULT);
				}
				finally
				{
					if (stream != null)
					{
						Marshal.ReleaseComObject(stream);
						stream = null;
					}
				}
				IStream stream2 = null;
				try
				{
					stream2 = storage.CreateStream("DataSpaceMap", 4114, 0, 0);
					streamOverIStream.ReplaceIStream(stream2);
					DrmDataspaces.DataSpaceMapHeader dataSpaceMapHeader = new DrmDataspaces.DataSpaceMapHeader(1);
					dataSpaceMapHeader.Write(binaryWriter);
					DrmDataspaces.DataSpaceMapEntry dataSpaceMapEntry = new DrmDataspaces.DataSpaceMapEntry(new string[][]
					{
						new string[]
						{
							"\tDRMContent",
							"\tDRMDataSpace"
						}
					});
					dataSpaceMapEntry.Write(binaryWriter);
					binaryWriter.Flush();
					stream2.Commit(STGC.STGC_DEFAULT);
				}
				finally
				{
					if (stream2 != null)
					{
						Marshal.ReleaseComObject(stream2);
						stream2 = null;
					}
				}
				IStorage storage2 = null;
				try
				{
					storage2 = storage.CreateStorage("DataSpaceInfo", 4114, 0, 0);
					IStream stream3 = null;
					try
					{
						stream3 = storage2.CreateStream("\tDRMDataSpace", 4114, 0, 0);
						streamOverIStream.ReplaceIStream(stream3);
						DrmDataspaces.DataTransformHeader dataTransformHeader = new DrmDataspaces.DataTransformHeader(new string[]
						{
							"\tDRMTransform"
						});
						dataTransformHeader.Write(binaryWriter);
						binaryWriter.Flush();
						stream3.Commit(STGC.STGC_DEFAULT);
					}
					finally
					{
						if (stream3 != null)
						{
							Marshal.ReleaseComObject(stream3);
							stream3 = null;
						}
					}
				}
				finally
				{
					if (storage2 != null)
					{
						Marshal.ReleaseComObject(storage2);
						storage2 = null;
					}
				}
				IStorage storage3 = null;
				try
				{
					storage3 = storage.CreateStorage("TransformInfo", 4114, 0, 0);
					IStorage storage4 = null;
					try
					{
						storage4 = storage3.CreateStorage("\tDRMTransform", 4114, 0, 0);
						IStream stream4 = null;
						try
						{
							stream4 = storage4.CreateStream("\u0006Primary", 4114, 0, 0);
							streamOverIStream.ReplaceIStream(stream4);
							DrmDataspaces.DataTransformInfo dataTransformInfo = new DrmDataspaces.DataTransformInfo("{C73DFACD-061F-43B0-8B64-0C620D2A8B50}");
							dataTransformInfo.Write(binaryWriter);
							DrmDataspaces.VersionInfo versionInfo2 = new DrmDataspaces.VersionInfo("Microsoft.Metadata.DRMTransform");
							versionInfo2.Write(binaryWriter);
							binaryWriter.Write(4);
							DrmEmailUtils.WriteUTF8String(binaryWriter, publishLicense);
							binaryWriter.Flush();
							stream4.Commit(STGC.STGC_DEFAULT);
						}
						finally
						{
							if (stream4 != null)
							{
								Marshal.ReleaseComObject(stream4);
								stream4 = null;
							}
						}
					}
					finally
					{
						if (storage4 != null)
						{
							Marshal.ReleaseComObject(storage4);
							storage4 = null;
						}
					}
				}
				finally
				{
					if (storage3 != null)
					{
						Marshal.ReleaseComObject(storage3);
						storage3 = null;
					}
				}
			}
			finally
			{
				if (storage != null)
				{
					Marshal.ReleaseComObject(storage);
					storage = null;
				}
			}
		}

		// Token: 0x02000979 RID: 2425
		private class VersionInfo
		{
			// Token: 0x060034A7 RID: 13479 RVA: 0x00083468 File Offset: 0x00081668
			public VersionInfo(string featureName)
			{
				this.featureName = featureName;
			}

			// Token: 0x060034A8 RID: 13480 RVA: 0x00083478 File Offset: 0x00081678
			public void Write(BinaryWriter writer)
			{
				DrmEmailUtils.WriteUnicodeString(writer, this.featureName);
				ushort value = 1;
				ushort value2 = 0;
				writer.Write(value);
				writer.Write(value2);
				writer.Write(value);
				writer.Write(value2);
				writer.Write(value);
				writer.Write(value2);
			}

			// Token: 0x060034A9 RID: 13481 RVA: 0x000834C0 File Offset: 0x000816C0
			public void Read(BinaryReader reader)
			{
				try
				{
					string a = DrmEmailUtils.ReadUnicodeString(reader);
					if (a != this.featureName)
					{
						throw new InvalidRpmsgFormatException(DrmStrings.InvalidRpmsgFormat("IncorrectFeatureName"));
					}
					ushort num = 1;
					ushort num2 = 0;
					ushort num3 = reader.ReadUInt16();
					ushort num4 = reader.ReadUInt16();
					if (num3 != num || num4 != num2)
					{
						throw new InvalidRpmsgFormatException(DrmStrings.InvalidRpmsgFormat("IncorrectReaderVersion"));
					}
					num3 = reader.ReadUInt16();
					num4 = reader.ReadUInt16();
					num3 = reader.ReadUInt16();
					num4 = reader.ReadUInt16();
				}
				catch (EndOfStreamException innerException)
				{
					throw new InvalidRpmsgFormatException(DrmStrings.InvalidRpmsgFormat("NotReadVersionInfo"), innerException);
				}
			}

			// Token: 0x04002C7E RID: 11390
			private readonly string featureName;
		}

		// Token: 0x0200097A RID: 2426
		private class DataSpaceMapHeader
		{
			// Token: 0x060034AA RID: 13482 RVA: 0x00083574 File Offset: 0x00081774
			public DataSpaceMapHeader(int entryCount)
			{
				this.entryCount = entryCount;
			}

			// Token: 0x060034AB RID: 13483 RVA: 0x00083584 File Offset: 0x00081784
			public void Write(BinaryWriter writer)
			{
				int value = 8;
				writer.Write(value);
				writer.Write(this.entryCount);
			}

			// Token: 0x060034AC RID: 13484 RVA: 0x000835A8 File Offset: 0x000817A8
			public void Read(BinaryReader reader)
			{
				int num = 8;
				try
				{
					int num2 = reader.ReadInt32();
					if (num2 != num)
					{
						throw new InvalidRpmsgFormatException(DrmStrings.InvalidRpmsgFormat("DataspaceMapHeaderWrongLength"));
					}
					reader.ReadInt32();
				}
				catch (EndOfStreamException innerException)
				{
					throw new InvalidRpmsgFormatException(DrmStrings.InvalidRpmsgFormat("NotReadDataspaceMapHeader"), innerException);
				}
			}

			// Token: 0x04002C7F RID: 11391
			private int entryCount;
		}

		// Token: 0x0200097B RID: 2427
		private class DataSpaceMapEntry
		{
			// Token: 0x060034AD RID: 13485 RVA: 0x00083608 File Offset: 0x00081808
			public DataSpaceMapEntry(string[][] refComponents)
			{
				this.refComponents = refComponents;
				this.entrylength = 8;
				foreach (string[] array2 in this.refComponents)
				{
					this.entrylength += 4;
					this.entrylength += DrmEmailUtils.GetUnicodeStringLength(array2[0]);
					this.entrylength += DrmEmailUtils.GetUnicodeStringLength(array2[1]);
				}
			}

			// Token: 0x060034AE RID: 13486 RVA: 0x0008367C File Offset: 0x0008187C
			public void Write(BinaryWriter writer)
			{
				writer.Write(this.entrylength);
				writer.Write(this.refComponents.Length);
				foreach (string[] array2 in this.refComponents)
				{
					writer.Write(0);
					DrmEmailUtils.WriteUnicodeString(writer, array2[0]);
					DrmEmailUtils.WriteUnicodeString(writer, array2[1]);
				}
			}

			// Token: 0x060034AF RID: 13487 RVA: 0x000836D8 File Offset: 0x000818D8
			public void Read(BinaryReader reader)
			{
				long position = reader.BaseStream.Position;
				try
				{
					int num = reader.ReadInt32();
					if (num != this.entrylength)
					{
						throw new InvalidRpmsgFormatException(DrmStrings.InvalidRpmsgFormat("DataspaceMapEntryWrongLength"));
					}
					int num2 = reader.ReadInt32();
					if (num2 != this.refComponents.Length)
					{
						throw new InvalidRpmsgFormatException(DrmStrings.InvalidRpmsgFormat("DataspaceMapEntryWrongRefComponentsCount"));
					}
					foreach (string[] array2 in this.refComponents)
					{
						int num3 = reader.ReadInt32();
						if (num3 != 0)
						{
							throw new InvalidRpmsgFormatException(DrmStrings.InvalidRpmsgFormat("DataspaceMapEntryWrongRefComponentType"));
						}
						string a = DrmEmailUtils.ReadUnicodeString(reader);
						if (a != array2[0])
						{
							throw new InvalidRpmsgFormatException(DrmStrings.InvalidRpmsgFormat("DataspaceMapEntryWrongRefComponentName1"));
						}
						string a2 = DrmEmailUtils.ReadUnicodeString(reader);
						if (a2 != array2[1])
						{
							throw new InvalidRpmsgFormatException(DrmStrings.InvalidRpmsgFormat("DataspaceMapEntryWrongRefComponentName2"));
						}
					}
					if (reader.BaseStream.Position - position != (long)this.entrylength)
					{
						throw new InvalidRpmsgFormatException(DrmStrings.InvalidRpmsgFormat("DataspaceMapEntryLengthMismatch"));
					}
				}
				catch (EndOfStreamException innerException)
				{
					throw new InvalidRpmsgFormatException(DrmStrings.InvalidRpmsgFormat("NotReadDataspaceMapEntry"), innerException);
				}
			}

			// Token: 0x04002C80 RID: 11392
			private string[][] refComponents;

			// Token: 0x04002C81 RID: 11393
			private int entrylength;
		}

		// Token: 0x0200097C RID: 2428
		private class DataTransformHeader
		{
			// Token: 0x060034B0 RID: 13488 RVA: 0x00083838 File Offset: 0x00081A38
			public DataTransformHeader(string[] transformNames)
			{
				this.transformNames = transformNames;
			}

			// Token: 0x060034B1 RID: 13489 RVA: 0x00083848 File Offset: 0x00081A48
			public void Write(BinaryWriter writer)
			{
				int value = 8;
				writer.Write(value);
				writer.Write(this.transformNames.Length);
				foreach (string value2 in this.transformNames)
				{
					DrmEmailUtils.WriteUnicodeString(writer, value2);
				}
			}

			// Token: 0x060034B2 RID: 13490 RVA: 0x0008388C File Offset: 0x00081A8C
			public void Read(BinaryReader reader)
			{
				int num = 8;
				try
				{
					int num2 = reader.ReadInt32();
					if (num2 != num)
					{
						throw new InvalidRpmsgFormatException(DrmStrings.InvalidRpmsgFormat("DataTransformHeaderWrongLength"));
					}
					int num3 = reader.ReadInt32();
					if (num3 != this.transformNames.Length)
					{
						throw new InvalidRpmsgFormatException(DrmStrings.InvalidRpmsgFormat("DataTransformHeaderWrongNameCount"));
					}
					foreach (string b in this.transformNames)
					{
						string a = DrmEmailUtils.ReadUnicodeString(reader);
						if (a != b)
						{
							throw new InvalidRpmsgFormatException(DrmStrings.InvalidRpmsgFormat("DataTransformHeaderWrongTransformName"));
						}
					}
				}
				catch (EndOfStreamException innerException)
				{
					throw new InvalidRpmsgFormatException(DrmStrings.InvalidRpmsgFormat("NotReadDataTransformHeader"), innerException);
				}
			}

			// Token: 0x04002C82 RID: 11394
			private string[] transformNames;
		}

		// Token: 0x0200097D RID: 2429
		private class DataTransformInfo
		{
			// Token: 0x060034B3 RID: 13491 RVA: 0x00083958 File Offset: 0x00081B58
			public DataTransformInfo(string transformClassName)
			{
				this.transformClassName = transformClassName;
				this.entryLength = 8 + DrmEmailUtils.GetUnicodeStringLength(this.transformClassName);
			}

			// Token: 0x060034B4 RID: 13492 RVA: 0x0008397A File Offset: 0x00081B7A
			public void Write(BinaryWriter writer)
			{
				writer.Write(this.entryLength);
				writer.Write(1);
				DrmEmailUtils.WriteUnicodeString(writer, this.transformClassName);
			}

			// Token: 0x060034B5 RID: 13493 RVA: 0x0008399C File Offset: 0x00081B9C
			public void Read(BinaryReader reader)
			{
				try
				{
					int num = reader.ReadInt32();
					if (num != this.entryLength)
					{
						throw new InvalidRpmsgFormatException(DrmStrings.InvalidRpmsgFormat("DataTransformInfoWrongLength"));
					}
					int num2 = reader.ReadInt32();
					if (num2 != 1)
					{
						throw new InvalidRpmsgFormatException(DrmStrings.InvalidRpmsgFormat("DataTransformInfoWrongEntryType"));
					}
					string a = DrmEmailUtils.ReadUnicodeString(reader);
					if (a != this.transformClassName)
					{
						throw new InvalidRpmsgFormatException(DrmStrings.InvalidRpmsgFormat("DataTransformInfoWrongName"));
					}
				}
				catch (EndOfStreamException innerException)
				{
					throw new InvalidRpmsgFormatException(DrmStrings.InvalidRpmsgFormat("NotReadDataTransformInfo"), innerException);
				}
			}

			// Token: 0x04002C83 RID: 11395
			private string transformClassName;

			// Token: 0x04002C84 RID: 11396
			private int entryLength;
		}
	}
}
