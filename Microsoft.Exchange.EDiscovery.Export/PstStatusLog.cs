using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000074 RID: 116
	internal class PstStatusLog : IStatusLog, IDisposable
	{
		// Token: 0x060007B1 RID: 1969 RVA: 0x0001BEA4 File Offset: 0x0001A0A4
		public PstStatusLog(string statusLogFilePath)
		{
			PstStatusLog <>4__this = this;
			LocalFileHelper.CallFileOperation(delegate
			{
				<>4__this.statusLogFile = new FileStream(statusLogFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
			}, ExportErrorType.FailedToOpenStatusFile);
			LocalFileHelper.CallFileOperation(delegate
			{
				if (!this.LoadHeader())
				{
					this.statusLogFile.SetLength(0L);
				}
			}, ExportErrorType.FailedToCleanupCorruptedStatusLog);
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0001BEFF File Offset: 0x0001A0FF
		public void Dispose()
		{
			if (this.statusLogFile != null)
			{
				this.statusLogFile.Flush();
				this.statusLogFile.Dispose();
				this.statusLogFile = null;
			}
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x0001BFB0 File Offset: 0x0001A1B0
		public void UpdateSourceStatus(SourceInformation source, int sourceIndex)
		{
			LocalFileHelper.CallFileOperation(delegate
			{
				this.statusLogFile.Seek(0L, SeekOrigin.End);
				this.statusLogFile.Write(BitConverter.GetBytes(sourceIndex), 0, 4);
				BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);
				binaryFormatter.TypeFormat = FormatterTypeStyle.TypesWhenNeeded;
				binaryFormatter.AssemblyFormat = FormatterAssemblyStyle.Simple;
				binaryFormatter.Serialize(this.statusLogFile, source.Status);
				this.statusLogFile.Flush(true);
			}, ExportErrorType.FailedToUpdateSourceStatus);
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x0001C2D4 File Offset: 0x0001A4D4
		public void ResetStatusLog(SourceInformationCollection allSourceInformation, OperationStatus status, ExportSettings exportSettings)
		{
			LocalFileHelper.CallFileOperation(delegate
			{
				this.statusLogFile.SetLength(0L);
				this.statusLogFile.Flush(true);
				this.statusLogFile.Seek(0L, SeekOrigin.Begin);
				this.statusSectionSelector = 0U;
				this.statusLogFile.Write(BitConverter.GetBytes(this.statusSectionSelector), 0, 4);
				this.sourceCount = allSourceInformation.Count;
				this.statusLogFile.Write(BitConverter.GetBytes(this.sourceCount), 0, 4);
				this.statusLogFile.Write(exportSettings.ToBinary(), 0, 256);
				this.exportSettings = exportSettings;
				this.statusLogFile.Seek(24L, SeekOrigin.Current);
				foreach (SourceInformation sourceInformation in allSourceInformation.Values)
				{
					BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);
					binaryFormatter.TypeFormat = FormatterTypeStyle.TypesWhenNeeded;
					binaryFormatter.AssemblyFormat = FormatterAssemblyStyle.Simple;
					binaryFormatter.Serialize(this.statusLogFile, sourceInformation.Configuration);
				}
				this.statusSection1StartPointer = this.statusLogFile.Length;
				this.statusLogFile.Write(BitConverter.GetBytes((uint)status), 0, 4);
				foreach (SourceInformation sourceInformation2 in allSourceInformation.Values)
				{
					sourceInformation2.Status.SaveToStream(this.statusLogFile);
				}
				this.statusSection2StartPointer = this.statusLogFile.Length;
				this.statusLogFile.SetLength(this.statusSection2StartPointer + this.statusSection2StartPointer - this.statusSection1StartPointer);
				this.sequentialStatusStartPointer = this.statusLogFile.Length;
				this.statusLogFile.Seek(264L, SeekOrigin.Begin);
				this.statusLogFile.Write(BitConverter.GetBytes(this.statusSection1StartPointer), 0, 8);
				this.statusLogFile.Write(BitConverter.GetBytes(this.statusSection2StartPointer), 0, 8);
				this.statusLogFile.Write(BitConverter.GetBytes(this.sequentialStatusStartPointer), 0, 8);
				this.statusLogFile.Flush(true);
			}, ExportErrorType.FailedToUpdateStatus);
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0001C43C File Offset: 0x0001A63C
		public void UpdateStatus(SourceInformationCollection allSourceInformation, OperationStatus status)
		{
			LocalFileHelper.CallFileOperation(delegate
			{
				this.statusLogFile.Seek((this.statusSectionSelector == 0U) ? this.statusSection2StartPointer : this.statusSection1StartPointer, SeekOrigin.Begin);
				this.statusLogFile.Write(BitConverter.GetBytes((uint)status), 0, 4);
				foreach (SourceInformation sourceInformation in allSourceInformation.Values)
				{
					sourceInformation.Status.SaveToStream(this.statusLogFile);
				}
				this.statusLogFile.Seek(0L, SeekOrigin.Begin);
				this.statusLogFile.Write(BitConverter.GetBytes(1U - this.statusSectionSelector), 0, 4);
				this.statusLogFile.SetLength(this.sequentialStatusStartPointer);
				this.statusLogFile.Flush(true);
			}, ExportErrorType.FailedToUpdateStatus);
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x0001C8E8 File Offset: 0x0001AAE8
		public ExportSettings LoadStatus(out SourceInformationCollection allSourceInformation, out OperationStatus status)
		{
			SourceInformationCollection retAllSourceInformation = null;
			OperationStatus retStatus = OperationStatus.None;
			LocalFileHelper.CallFileOperation(delegate
			{
				if (this.statusLogFile.Length > 0L)
				{
					retAllSourceInformation = new SourceInformationCollection(this.sourceCount);
					BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);
					binaryFormatter.TypeFormat = FormatterTypeStyle.TypesWhenNeeded;
					binaryFormatter.AssemblyFormat = FormatterAssemblyStyle.Simple;
					try
					{
						for (int i = 0; i < this.sourceCount; i++)
						{
							SourceInformation.SourceConfiguration sourceConfiguration = null;
							try
							{
								Type[] allowList = new Type[]
								{
									typeof(SourceInformation.SourceConfiguration),
									typeof(Uri)
								};
								sourceConfiguration = (SourceInformation.SourceConfiguration)SafeSerialization.SafeBinaryFormatterDeserializeWithAllowList(this.statusLogFile, allowList, null);
							}
							catch (SafeSerialization.BlockedTypeException innerException)
							{
								ExportException ex = new ExportException(ExportErrorType.CorruptedStatus, innerException);
								Tracer.TraceError("PstStatusLog.LoadStatus: Corrupted status log found. Unsafe to deserialize to SourceConfiguration. Exception: {0}", new object[]
								{
									ex
								});
								throw ex;
							}
							retAllSourceInformation[sourceConfiguration.Id] = new SourceInformation(sourceConfiguration.Name, sourceConfiguration.Id, sourceConfiguration.SourceFilter, sourceConfiguration.ServiceEndpoint, sourceConfiguration.LegacyExchangeDN);
						}
						this.statusLogFile.Seek((this.statusSectionSelector == 0U) ? this.statusSection1StartPointer : this.statusSection2StartPointer, SeekOrigin.Begin);
						byte[] array = new byte[4];
						this.statusLogFile.Read(array, 0, 4);
						retStatus = (OperationStatus)BitConverter.ToUInt32(array, 0);
						for (int j = 0; j < this.sourceCount; j++)
						{
							retAllSourceInformation[j].Status.LoadFromStream(this.statusLogFile);
						}
					}
					catch (InvalidCastException ex2)
					{
						Tracer.TraceError("PstStatusLog.LoadStatus: Corrupted status log found. Exception: {0}", new object[]
						{
							ex2
						});
						retAllSourceInformation = null;
					}
					catch (ExportException ex3)
					{
						Tracer.TraceError("PstStatusLog.LoadStatus: Corrupted status log found. Exception: {0}", new object[]
						{
							ex3
						});
						retAllSourceInformation = null;
					}
					catch (SerializationException ex4)
					{
						Tracer.TraceError("PstStatusLog.LoadStatus: Corrupted status log found. Exception: {0}", new object[]
						{
							ex4
						});
						retAllSourceInformation = null;
					}
					if (retAllSourceInformation == null)
					{
						this.statusLogFile.SetLength(0L);
						return;
					}
					if (this.sequentialStatusStartPointer != this.statusLogFile.Length)
					{
						try
						{
							bool flag = false;
							this.statusLogFile.Seek(this.sequentialStatusStartPointer, SeekOrigin.Begin);
							byte[] array2 = new byte[4];
							while (!flag)
							{
								int num = this.statusLogFile.Read(array2, 0, 4);
								if (num == 4)
								{
									int index = BitConverter.ToInt32(array2, 0);
									SourceInformation.SourceStatus status2 = null;
									try
									{
										Type[] allowList2 = new Type[]
										{
											typeof(SourceInformation.SourceStatus)
										};
										status2 = (SourceInformation.SourceStatus)SafeSerialization.SafeBinaryFormatterDeserializeWithAllowList(this.statusLogFile, allowList2, null);
									}
									catch (SafeSerialization.BlockedTypeException innerException2)
									{
										ExportException ex5 = new ExportException(ExportErrorType.CorruptedStatus, innerException2);
										Tracer.TraceError("PstStatusLog.LoadStatus: Corrupted status log found. Unsafe to deserialize to SourceStatus. Exception: {0}", new object[]
										{
											ex5
										});
										throw ex5;
									}
									retAllSourceInformation[index].Status = status2;
								}
								else
								{
									flag = true;
								}
							}
						}
						catch (ArgumentOutOfRangeException ex6)
						{
							Tracer.TraceError("PstStatusLog.LoadStatus: Ignoring exception and all records after: {0}", new object[]
							{
								ex6
							});
						}
						catch (SerializationException ex7)
						{
							Tracer.TraceError("PstStatusLog.LoadStatus: Ignoring exception and all records after: {0}", new object[]
							{
								ex7
							});
						}
						catch (InvalidCastException ex8)
						{
							Tracer.TraceError("PstStatusLog.LoadStatus: Ignoring exception and all records after: {0}", new object[]
							{
								ex8
							});
						}
						this.UpdateStatus(retAllSourceInformation, retStatus);
					}
				}
			}, ExportErrorType.FailedToLoadStatus);
			allSourceInformation = retAllSourceInformation;
			status = retStatus;
			return this.exportSettings;
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0001C980 File Offset: 0x0001AB80
		public void Delete()
		{
			LocalFileHelper.CallFileOperation(delegate
			{
				string name = this.statusLogFile.Name;
				this.statusLogFile.Flush();
				this.statusLogFile.Dispose();
				this.statusLogFile = null;
				if (File.Exists(name))
				{
					File.Delete(name);
				}
			}, ExportErrorType.FailedToDeleteStatusFile);
			this.exportSettings = null;
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0001C99C File Offset: 0x0001AB9C
		private bool LoadHeader()
		{
			this.statusLogFile.Seek(0L, SeekOrigin.Begin);
			if (this.statusLogFile.Length <= 288L)
			{
				return false;
			}
			byte[] array = new byte[288];
			this.statusLogFile.Read(array, 0, array.Length);
			int num = 0;
			this.statusSectionSelector = BitConverter.ToUInt32(array, num);
			num += 4;
			this.sourceCount = BitConverter.ToInt32(array, num);
			num += 4;
			this.exportSettings = ExportSettings.FromBinary(array, num);
			num += 256;
			this.statusSection1StartPointer = BitConverter.ToInt64(array, num);
			num += 8;
			this.statusSection2StartPointer = BitConverter.ToInt64(array, num);
			num += 8;
			this.sequentialStatusStartPointer = BitConverter.ToInt64(array, num);
			return this.statusSectionSelector <= 1U && this.sourceCount > 0 && !(this.exportSettings.ExportTime == DateTime.MinValue) && this.statusSection1StartPointer < this.statusSection2StartPointer && this.statusSection2StartPointer < this.sequentialStatusStartPointer && this.sequentialStatusStartPointer <= this.statusLogFile.Length;
		}

		// Token: 0x040002C5 RID: 709
		private const int HeaderSize = 288;

		// Token: 0x040002C6 RID: 710
		private FileStream statusLogFile;

		// Token: 0x040002C7 RID: 711
		private uint statusSectionSelector;

		// Token: 0x040002C8 RID: 712
		private int sourceCount;

		// Token: 0x040002C9 RID: 713
		private ExportSettings exportSettings;

		// Token: 0x040002CA RID: 714
		private long statusSection1StartPointer;

		// Token: 0x040002CB RID: 715
		private long statusSection2StartPointer;

		// Token: 0x040002CC RID: 716
		private long sequentialStatusStartPointer;
	}
}
