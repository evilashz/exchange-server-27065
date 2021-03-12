using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Cobalt;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200003E RID: 62
	public class CobaltStore : DisposeTrackableBase
	{
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00005E27 File Offset: 0x00004027
		public string OwnerEmailAddress
		{
			get
			{
				return this.userAddress;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00005E2F File Offset: 0x0000402F
		public int EditorCount
		{
			get
			{
				return this.editorCount;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00005E37 File Offset: 0x00004037
		internal CobaltStoreSaver Saver
		{
			get
			{
				return this.saver;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00005E3F File Offset: 0x0000403F
		internal string WorkingDirectory
		{
			get
			{
				return this.workingDirectory;
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00005E47 File Offset: 0x00004047
		static CobaltStore()
		{
			CobaltHostServices.Initialize();
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00005E58 File Offset: 0x00004058
		public CobaltStore(string parentDirectory, string userId, string correlationId, bool diagnosticsEnabled, int memoryBudget)
		{
			if (string.IsNullOrEmpty(parentDirectory))
			{
				throw new ArgumentException("CobaltStore parent directory must be specified.");
			}
			if (string.IsNullOrEmpty(userId))
			{
				throw new ArgumentException("CobaltStore user ID must be specified.");
			}
			if (string.IsNullOrEmpty(correlationId))
			{
				throw new ArgumentException("CobaltStore correlation ID must be specified.");
			}
			this.userAddress = userId;
			this.correlationId = correlationId;
			this.diagnosticsEnabled = diagnosticsEnabled;
			this.workingDirectory = Path.Combine(parentDirectory, this.correlationId);
			Directory.CreateDirectory(this.workingDirectory);
			this.storeDisposalEscrow = new DisposalEscrow("CobaltStore");
			this.InitializePartitionBlobStores(memoryBudget);
			this.synchronizationObject = new object();
			this.saver = new CobaltStoreSaver();
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00005F12 File Offset: 0x00004112
		public override string ToString()
		{
			return this.userAddress + "-" + this.correlationId;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00005F2C File Offset: 0x0000412C
		public void Save(Stream stream)
		{
			this.SaveDiagnosticStream(stream, "Original.bin");
			lock (this.synchronizationObject)
			{
				using (DisposalEscrow disposalEscrow = new DisposalEscrow("CobaltStore.Save"))
				{
					CobaltFile cobaltFile = this.CreateCobaltFile(disposalEscrow, true);
					CobaltFilePartition cobaltFilePartition = cobaltFile.GetCobaltFilePartition(FilePartitionId.Content);
					Metrics metrics;
					cobaltFilePartition.SetStream(GenericFda.ContentStreamId, new BytesFromStream(stream), ref metrics);
					cobaltFilePartition.CommitChanges();
					this.SaveDiagnosticDocument(cobaltFile, "AfterSave.bin");
				}
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00005FD4 File Offset: 0x000041D4
		public Stream GetDocumentStream()
		{
			Stream result;
			lock (this.synchronizationObject)
			{
				using (DisposalEscrow disposalEscrow = new DisposalEscrow("CobaltStore.GetDocumentStream"))
				{
					CobaltFile cobaltFile = this.CreateCobaltFile(disposalEscrow, false);
					CobaltFilePartition cobaltFilePartition = cobaltFile.GetCobaltFilePartition(FilePartitionId.Content);
					Bytes stream = cobaltFilePartition.GetStream(CobaltFilePartition.ContentStreamId);
					Stream stream2 = new StreamFromBytes(stream, 0UL);
					Stream stream3 = new StreamDisposalWrapper(stream2, disposalEscrow.Transfer("GetDocumentStream-StreamDisposalWrapper"));
					result = stream3;
				}
			}
			return result;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00006078 File Offset: 0x00004278
		public void SaveFailed(Exception exception)
		{
			this.permanentException = exception;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00006084 File Offset: 0x00004284
		public void ProcessRequest(Stream requestStream, Stream responseStream, Action<Enum, string> logDetail)
		{
			if (this.permanentException != null)
			{
				throw new CobaltStore.OrphanedCobaltStoreException(string.Format("The attachment is no longer available for CobaltStore {0}.", this.correlationId), this.permanentException);
			}
			try
			{
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();
				if (!Monitor.TryEnter(this.synchronizationObject, TimeSpan.FromSeconds(15.0)))
				{
					throw new Exception("Unable to acquire CobaltStore lock.");
				}
				stopwatch.Stop();
				logDetail(WacRequestHandlerMetadata.LockWaitTime, stopwatch.ElapsedMilliseconds.ToString());
				using (DisposalEscrow disposalEscrow = new DisposalEscrow("CobaltStore.ProcessRequest"))
				{
					using (Stream stream = new MemoryStream(65536))
					{
						requestStream.CopyTo(stream, 65536);
						stream.Position = 0L;
						logDetail(WacRequestHandlerMetadata.CobaltRequestLength, stream.Length.ToString());
						CobaltFile cobaltFile = this.CreateCobaltFile(disposalEscrow, false);
						this.SaveDiagnosticDocument(cobaltFile, "BeforeRoundTrip.bin");
						this.SaveDiagnosticStream(requestStream, "Request.xml");
						using (DisposableAtomFromStream disposableAtomFromStream = new DisposableAtomFromStream(stream))
						{
							Roundtrip roundtrip = cobaltFile.CobaltEndpoint.CreateRoundtrip();
							bool exceptionThrown = false;
							Atom atom = null;
							Stopwatch stopwatch2 = new Stopwatch();
							stopwatch2.Start();
							try
							{
								object obj;
								ProtocolVersion protocolVersion;
								roundtrip.DeserializeInputFromProtocol(disposableAtomFromStream, ref obj, ref protocolVersion);
								roundtrip.Execute();
								cobaltFile.CommitChanges();
								atom = roundtrip.SerializeOutputToProtocol(1, obj, null);
							}
							catch (Exception)
							{
								exceptionThrown = true;
								throw;
							}
							finally
							{
								stopwatch2.Stop();
								logDetail(WacRequestHandlerMetadata.CobaltTime, stopwatch2.ElapsedMilliseconds.ToString());
								this.LogBlobStoreMetrics(logDetail, cobaltFile);
								this.LogRequestDetails(logDetail, roundtrip, exceptionThrown);
							}
							this.UpdateEditorCount(roundtrip.RequestBatch);
							this.SaveDiagnosticDocument(cobaltFile, "AfterRoundTrip.bin");
							atom.CopyTo(responseStream);
							logDetail(WacRequestHandlerMetadata.CobaltResponseLength, atom.Length.ToString());
							if (this.diagnosticsEnabled)
							{
								using (MemoryStream memoryStream = new MemoryStream())
								{
									atom.CopyTo(memoryStream);
									this.SaveDiagnosticStream(memoryStream, "Response.xml");
								}
							}
						}
					}
				}
			}
			finally
			{
				if (Monitor.IsEntered(this.synchronizationObject))
				{
					Monitor.Exit(this.synchronizationObject);
				}
			}
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000635C File Offset: 0x0000455C
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.storeDisposalEscrow != null)
				{
					this.storeDisposalEscrow.Dispose();
					this.storeDisposalEscrow = null;
				}
				if (this.saver != null)
				{
					this.saver.Dispose();
					this.saver = null;
				}
			}
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00006395 File Offset: 0x00004595
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<CobaltStore>(this);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000063A0 File Offset: 0x000045A0
		private void SaveDiagnosticDocument(CobaltFile cobaltFile, string fileName)
		{
			if (!this.diagnosticsEnabled)
			{
				return;
			}
			CobaltFilePartition cobaltFilePartition = cobaltFile.GetCobaltFilePartition(FilePartitionId.Content);
			Bytes stream = cobaltFilePartition.GetStream(CobaltFilePartition.ContentStreamId);
			string filePath = this.GetFilePath(fileName);
			using (StreamFromBytes streamFromBytes = new StreamFromBytes(stream, 0UL))
			{
				using (FileStream fileStream = File.Open(filePath, FileMode.Create, FileAccess.Write))
				{
					streamFromBytes.CopyTo(fileStream);
				}
			}
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00006428 File Offset: 0x00004628
		private string GetFilePath(string fileName)
		{
			string path = WacUtilities.GetCurrentTimeForFileName() + "-" + fileName;
			return Path.Combine(this.workingDirectory, path);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00006468 File Offset: 0x00004668
		private void InitializePartitionBlobStores(int memoryBudget)
		{
			TemporaryHostBlobStore.Config config = new TemporaryHostBlobStore.Config();
			config.AllocateBsn = (() => (ulong)Interlocked.Increment(ref CobaltStore.bsn));
			config.StartBsn = (() => (ulong)CobaltStore.bsn);
			this.contentBlobStore = this.CreateBlobStore(config, memoryBudget, "Content");
			this.metadataBlobStore = this.CreateBlobStore(config, memoryBudget, "Metadata");
			this.editorTableBlobStore = this.CreateBlobStore(config, memoryBudget, "Editors");
			this.convertedDocumentBlobStore = this.CreateBlobStore(config, memoryBudget, "WWConv");
			this.updateBlobStore = this.CreateBlobStore(config, memoryBudget, "WWUpdate");
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00006520 File Offset: 0x00004720
		private HostBlobStore CreateBlobStore(TemporaryHostBlobStore.Config configuration, int memoryBudget, string directoryName)
		{
			string text = Path.Combine(this.workingDirectory, directoryName);
			Directory.CreateDirectory(text);
			TemporaryHostBlobStore temporaryHostBlobStore = new TemporaryHostBlobStore(configuration, this.storeDisposalEscrow, "CobaltStore." + directoryName, (long)memoryBudget, text, false);
			this.blobStores.Add(temporaryHostBlobStore);
			return temporaryHostBlobStore;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000656C File Offset: 0x0000476C
		private CobaltFile CreateCobaltFile(DisposalEscrow disposalEscrow, bool isNewFile)
		{
			if (CobaltStore.partitionNames == null)
			{
				Dictionary<Guid, string> dictionary = new Dictionary<Guid, string>();
				Dictionary<Guid, string> dictionary2 = dictionary;
				FilePartitionId content = FilePartitionId.Content;
				dictionary2.Add(content.GuidId, "C");
				Dictionary<Guid, string> dictionary3 = dictionary;
				FilePartitionId coauthMetadata = FilePartitionId.CoauthMetadata;
				dictionary3.Add(coauthMetadata.GuidId, "CAM");
				Dictionary<Guid, string> dictionary4 = dictionary;
				FilePartitionId o14EditorsTable = FilePartitionId.O14EditorsTable;
				dictionary4.Add(o14EditorsTable.GuidId, "O14E");
				Dictionary<Guid, string> dictionary5 = dictionary;
				FilePartitionId wordWacConvertedDocument = FilePartitionId.WordWacConvertedDocument;
				dictionary5.Add(wordWacConvertedDocument.GuidId, "WWCD");
				Dictionary<Guid, string> dictionary6 = dictionary;
				FilePartitionId wordWacUpdate = FilePartitionId.WordWacUpdate;
				dictionary6.Add(wordWacUpdate.GuidId, "WWU");
				CobaltStore.partitionNames = dictionary;
			}
			CobaltFilePartitionConfig value = new CobaltFilePartitionConfig
			{
				PartitionId = new FilePartitionId?(FilePartitionId.Content),
				HostBlobStore = this.contentBlobStore,
				Schema = 2,
				IsNewFile = isNewFile
			};
			CobaltFilePartitionConfig value2 = new CobaltFilePartitionConfig
			{
				PartitionId = new FilePartitionId?(FilePartitionId.CoauthMetadata),
				HostBlobStore = this.metadataBlobStore,
				Schema = 2,
				IsNewFile = isNewFile
			};
			CobaltFilePartitionConfig value3 = new CobaltFilePartitionConfig
			{
				PartitionId = new FilePartitionId?(FilePartitionId.O14EditorsTable),
				HostBlobStore = this.editorTableBlobStore,
				Schema = 2,
				IsNewFile = isNewFile
			};
			CobaltFilePartitionConfig value4 = new CobaltFilePartitionConfig
			{
				PartitionId = new FilePartitionId?(FilePartitionId.WordWacConvertedDocument),
				HostBlobStore = this.convertedDocumentBlobStore,
				Schema = 2,
				IsNewFile = isNewFile
			};
			CobaltFilePartitionConfig value5 = new CobaltFilePartitionConfig
			{
				PartitionId = new FilePartitionId?(FilePartitionId.WordWacUpdate),
				HostBlobStore = this.updateBlobStore,
				Schema = 2,
				IsNewFile = isNewFile
			};
			return new CobaltFile(disposalEscrow, new Dictionary<FilePartitionId, CobaltFilePartitionConfig>
			{
				{
					FilePartitionId.Content,
					value
				},
				{
					FilePartitionId.CoauthMetadata,
					value2
				},
				{
					FilePartitionId.O14EditorsTable,
					value3
				},
				{
					FilePartitionId.WordWacConvertedDocument,
					value4
				},
				{
					FilePartitionId.WordWacUpdate,
					value5
				}
			}, new CobaltServerLockingStore(this), null);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00006780 File Offset: 0x00004980
		private void SaveDiagnosticStream(Stream input, string fileName)
		{
			if (!this.diagnosticsEnabled)
			{
				return;
			}
			string filePath = this.GetFilePath(fileName);
			long position = input.Position;
			input.Position = 0L;
			using (Stream stream = File.Open(filePath, FileMode.Create, FileAccess.Write))
			{
				input.CopyTo(stream);
			}
			input.Position = position;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000067E0 File Offset: 0x000049E0
		private static string GetRequestNameAndPartition(Request request)
		{
			string str;
			if (!CobaltStore.partitionNames.TryGetValue(request.PartitionId.GuidId, out str))
			{
				str = request.PartitionId.GuidId.ToString();
			}
			return request.GetType().Name + "." + str;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00006844 File Offset: 0x00004A44
		private void LogRequestDetails(Action<Enum, string> logDetail, Roundtrip roundtrip, bool exceptionThrown)
		{
			string arg = string.Join(" ", from request in roundtrip.RequestBatch.Requests
			select CobaltStore.GetRequestNameAndPartition(request));
			logDetail(WacRequestHandlerMetadata.CobaltOperations, arg);
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			foreach (Request request2 in roundtrip.RequestBatch.Requests)
			{
				if (request2.Failed)
				{
					flag = true;
					stringBuilder.Append(request2.GetType().Name);
					stringBuilder.Append(" failed. ");
					try
					{
						string conciseLoggingStatement = Log.GetConciseLoggingStatement(request2, this.userAddress);
						if (exceptionThrown || request2.Failed)
						{
							stringBuilder.AppendLine(conciseLoggingStatement);
						}
					}
					catch (ErrorException)
					{
						stringBuilder.AppendLine("Concise logging not supported.");
					}
				}
			}
			try
			{
				roundtrip.ThrowIfAnyError();
			}
			catch (Exception ex)
			{
				exceptionThrown = true;
				stringBuilder.AppendLine("ThrowIfAnyError: " + ex.ToString());
			}
			if (exceptionThrown || flag)
			{
				logDetail(WacRequestHandlerMetadata.ErrorDetails, stringBuilder.ToString());
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00006994 File Offset: 0x00004B94
		private void LogBlobStoreMetrics(Action<Enum, string> logDetail, CobaltFile cobaltFile)
		{
			int num = 0;
			int num2 = 0;
			ulong num3 = 0UL;
			ulong num4 = 0UL;
			long num5 = 0L;
			long num6 = 0L;
			foreach (HostBlobStore hostBlobStore in this.blobStores)
			{
				HostBlobStoreMetrics metrics = hostBlobStore.GetMetrics();
				if (metrics != null)
				{
					num += metrics.ReadBlobsFound + metrics.ReadBlobsNotFound;
					num2 += metrics.WrittenBlobs;
					num3 += metrics.ReadBlobBytes;
					num4 += metrics.WrittenBlobBytes;
					TemporaryHostBlobStoreMetrics tempHostBlobStoreMetrics = ((TemporaryHostBlobStore)hostBlobStore).TempHostBlobStoreMetrics;
					num5 += (long)tempHostBlobStoreMetrics.NumberOfBlobsOnDisk;
					num6 += tempHostBlobStoreMetrics.TotalSizeOfBlobsOnDisk;
				}
			}
			logDetail(WacRequestHandlerMetadata.CobaltReads, num.ToString());
			logDetail(WacRequestHandlerMetadata.CobaltWrites, num2.ToString());
			logDetail(WacRequestHandlerMetadata.CobaltBytesRead, num3.ToString());
			logDetail(WacRequestHandlerMetadata.CobaltBytesWritten, num4.ToString());
			logDetail(WacRequestHandlerMetadata.DiskBlobCount, num5.ToString());
			logDetail(WacRequestHandlerMetadata.DiskBlobSize, num6.ToString());
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00006AD0 File Offset: 0x00004CD0
		private void UpdateEditorCount(RequestBatch batch)
		{
			foreach (Request request in batch.Requests)
			{
				if (request is JoinCoauthoringRequest)
				{
					this.editorCount++;
				}
				else if (request is ExitCoauthoringRequest)
				{
					this.Saver.SaveAndThrowExceptions();
					this.editorCount--;
				}
			}
		}

		// Token: 0x0400008D RID: 141
		internal const string CobaltSubdirectory = "OwaCobalt";

		// Token: 0x0400008E RID: 142
		private const int myBlockSize = 65536;

		// Token: 0x0400008F RID: 143
		private static long bsn = 0L;

		// Token: 0x04000090 RID: 144
		private static IReadOnlyDictionary<Guid, string> partitionNames;

		// Token: 0x04000091 RID: 145
		private readonly string userAddress;

		// Token: 0x04000092 RID: 146
		private readonly string correlationId;

		// Token: 0x04000093 RID: 147
		private readonly string workingDirectory;

		// Token: 0x04000094 RID: 148
		private readonly bool diagnosticsEnabled;

		// Token: 0x04000095 RID: 149
		private HostBlobStore contentBlobStore;

		// Token: 0x04000096 RID: 150
		private HostBlobStore metadataBlobStore;

		// Token: 0x04000097 RID: 151
		private HostBlobStore editorTableBlobStore;

		// Token: 0x04000098 RID: 152
		private HostBlobStore convertedDocumentBlobStore;

		// Token: 0x04000099 RID: 153
		private HostBlobStore updateBlobStore;

		// Token: 0x0400009A RID: 154
		private List<TemporaryHostBlobStore> blobStores = new List<TemporaryHostBlobStore>(5);

		// Token: 0x0400009B RID: 155
		private DisposalEscrow storeDisposalEscrow;

		// Token: 0x0400009C RID: 156
		private object synchronizationObject;

		// Token: 0x0400009D RID: 157
		private CobaltStoreSaver saver;

		// Token: 0x0400009E RID: 158
		private Exception permanentException;

		// Token: 0x0400009F RID: 159
		private int editorCount;

		// Token: 0x0200003F RID: 63
		private class OrphanedCobaltStoreException : Exception
		{
			// Token: 0x0600018E RID: 398 RVA: 0x00006B50 File Offset: 0x00004D50
			public OrphanedCobaltStoreException(string message, Exception exception) : base(message, exception)
			{
			}
		}
	}
}
