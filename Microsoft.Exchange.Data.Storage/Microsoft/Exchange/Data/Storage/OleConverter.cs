using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.ProcessManager;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000608 RID: 1544
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class OleConverter : ComProcessManager<IOleConverter>
	{
		// Token: 0x170012F6 RID: 4854
		// (get) Token: 0x06003F88 RID: 16264 RVA: 0x00107D8C File Offset: 0x00105F8C
		internal static OleConverter Instance
		{
			get
			{
				return OleConverter.InstanceCreator.Instance;
			}
		}

		// Token: 0x06003F89 RID: 16265 RVA: 0x00107D94 File Offset: 0x00105F94
		internal OleConverter() : base(OleConverter.MaxWorkerNumber, OleConverter.WorkerConfigurationObject, ExTraceGlobals.CcOleTracer)
		{
			this.nQueueSize = 0;
			this.cleanupLock = new object();
			this.cleanupTimer = new Timer(new TimerCallback(OleConverter.DirectoryCleanup), this.cleanupLock, new TimeSpan(0, 0, 0), OleConverter.CleanupTimespan);
			this.CreateWorkerCallback = (ComProcessManager<IOleConverter>.OnCreateWorker)Delegate.Combine(this.CreateWorkerCallback, new ComProcessManager<IOleConverter>.OnCreateWorker(OleConverter.OnNewConverterCreatedCallback));
			this.DestroyWorkerCallback = (ComProcessManager<IOleConverter>.OnDestroyWorker)Delegate.Combine(this.DestroyWorkerCallback, new ComProcessManager<IOleConverter>.OnDestroyWorker(OleConverter.OnConverterDestroyedCallback));
			this.ExecuteRequestCallback = (ComProcessManager<IOleConverter>.OnExecuteRequest)Delegate.Combine(this.ExecuteRequestCallback, new ComProcessManager<IOleConverter>.OnExecuteRequest(OleConverter.OnExecuteRequestCallback));
		}

		// Token: 0x06003F8A RID: 16266 RVA: 0x00107E58 File Offset: 0x00106058
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<OleConverter>(this);
		}

		// Token: 0x06003F8B RID: 16267 RVA: 0x00107E60 File Offset: 0x00106060
		protected override void InternalDispose(bool isDisposing)
		{
			if (isDisposing)
			{
				this.cleanupTimer.Dispose();
			}
			base.InternalDispose(isDisposing);
		}

		// Token: 0x06003F8C RID: 16268 RVA: 0x00107E78 File Offset: 0x00106078
		private static void OnNewConverterCreatedCallback(IComWorker<IOleConverter> converterProcess, object requestParameters)
		{
			IOleConverter worker = converterProcess.Worker;
			uint num = 0U;
			string conversionsDirectory = OleConverter.GetConversionsDirectory(true);
			if (conversionsDirectory == null)
			{
				StorageGlobals.ContextTraceError(ExTraceGlobals.CcOleTracer, "OleConverter::OnNewConverterCreatedCallback: conversions directory full or inaccessible.");
				throw new OleConversionFailedException(ServerStrings.OleConversionFailed);
			}
			int num2;
			worker.ConfigureConverter(out num2, 4194304U, 262144U, conversionsDirectory, out num);
			if (num2 != 0)
			{
				StorageGlobals.ContextTraceError(ExTraceGlobals.CcOleTracer, "OleConverter::OnNewConverterCreatedCallback: failed to configure converter.");
				throw new OleConversionFailedException(ServerStrings.OleConversionFailed, new COMException("HRESULT =", num2));
			}
			if (num != (uint)converterProcess.ProcessId)
			{
				StorageGlobals.ContextTraceError(ExTraceGlobals.CcOleTracer, "OleConverter::OnNewConverterCreatedCallback: process id mismatch.");
				throw new OleConversionFailedException(ServerStrings.OleConversionInitError(Process.GetCurrentProcess().ProcessName, converterProcess.ProcessId, (int)num));
			}
		}

		// Token: 0x06003F8D RID: 16269 RVA: 0x00107F23 File Offset: 0x00106123
		private static void OnConverterDestroyedCallback(IComWorker<IOleConverter> converterProcess, object requestParameters, bool isForcedTermination)
		{
		}

		// Token: 0x06003F8E RID: 16270 RVA: 0x00107F28 File Offset: 0x00106128
		private static bool OnExecuteRequestCallback(IComWorker<IOleConverter> converterProcess, object requestParameters)
		{
			OleConverter.ConversionRequestParameters conversionRequestParameters = (OleConverter.ConversionRequestParameters)requestParameters;
			object responseData = null;
			int num;
			converterProcess.Worker.OleConvertToBmp(out num, conversionRequestParameters.RequestData, out responseData);
			if (num != 0)
			{
				throw new OleConversionFailedException(ServerStrings.OleConversionFailed, new COMException("HRESULT =", num));
			}
			conversionRequestParameters.ResponseData = responseData;
			return true;
		}

		// Token: 0x06003F8F RID: 16271 RVA: 0x00107F74 File Offset: 0x00106174
		private static string GetConversionsDirectory(bool checkIfFull)
		{
			return ConvertUtils.GetOleConversionsDirectory("Working\\OleConverter", checkIfFull);
		}

		// Token: 0x06003F90 RID: 16272 RVA: 0x00107F84 File Offset: 0x00106184
		internal Stream ConvertToBitmap(Stream oleDataStream)
		{
			object obj = null;
			int num = Interlocked.Increment(ref this.nQueueSize);
			Stream result;
			try
			{
				try
				{
					bool canCacheInMemory = num <= 30;
					obj = OleConverter.CreateOleObjectData(oleDataStream, canCacheInMemory);
					OleConverter.ConversionRequestParameters conversionRequestParameters = new OleConverter.ConversionRequestParameters(obj, null);
					base.ExecuteRequest(conversionRequestParameters);
					result = OleConverter.CreateResultStream(conversionRequestParameters.ResponseData);
				}
				catch (ComInterfaceInitializeException ex)
				{
					StorageGlobals.ContextTraceError<ComInterfaceInitializeException>(ExTraceGlobals.CcOleTracer, "OleConverter::ConvertToBitmap: ole conversion failed. Exception:\n {0}", ex);
					throw new OleConversionServerBusyException(ServerStrings.OleConversionFailed, ex);
				}
				catch (ComProcessBusyException ex2)
				{
					StorageGlobals.ContextTraceError<ComProcessBusyException>(ExTraceGlobals.CcOleTracer, "OleConverter::ConvertToBitmap: ole conversion failed. Exception:\n {0}", ex2);
					throw new OleConversionServerBusyException(ServerStrings.OleConversionFailed, ex2);
				}
				catch (ComProcessTimeoutException ex3)
				{
					StorageGlobals.ContextTraceError<ComProcessTimeoutException>(ExTraceGlobals.CcOleTracer, "OleConverter::ConvertToBitmap: ole conversion failed. Exception:\n {0}", ex3);
					throw new OleConversionFailedException(ServerStrings.OleConversionFailed, ex3);
				}
				catch (ComProcessBeyondMemoryLimitException ex4)
				{
					StorageGlobals.ContextTraceError<ComProcessBeyondMemoryLimitException>(ExTraceGlobals.CcOleTracer, "OleConverter::ConvertToBitmap: ole conversion failed. Exception:\n {0}", ex4);
					throw new OleConversionFailedException(ServerStrings.OleConversionFailed, ex4);
				}
				catch (COMException ex5)
				{
					StorageGlobals.ContextTraceError<COMException>(ExTraceGlobals.CcOleTracer, "OleConverter::ConvertToBitmap: ole conversion failed. Exception:\n {0}", ex5);
					throw new OleConversionFailedException(ServerStrings.OleConversionFailed, ex5);
				}
				catch (InvalidComObjectException ex6)
				{
					StorageGlobals.ContextTraceError<InvalidComObjectException>(ExTraceGlobals.CcOleTracer, "OleConverter::ConvertToBitmap: ole conversion failed. Exception:\n {0}", ex6);
					throw new OleConversionFailedException(ServerStrings.OleConversionFailed, ex6);
				}
				catch (InvalidCastException ex7)
				{
					StorageGlobals.ContextTraceError<InvalidCastException>(ExTraceGlobals.CcOleTracer, "OleConverter::ConvertToBitmap: ole conversion failed. Exception:\n {0}", ex7);
					throw new OleConversionFailedException(ServerStrings.OleConversionFailed, ex7);
				}
				catch (NoSupportException ex8)
				{
					StorageGlobals.ContextTraceError<NoSupportException>(ExTraceGlobals.CcOleTracer, "OleConverter::ConvertToBitmap: ole conversion failed. Exception:\n {0}", ex8);
					throw new OleConversionFailedException(ServerStrings.OleConversionFailed, ex8);
				}
			}
			catch (StoragePermanentException exc)
			{
				OleConverter.SaveFailedConversionData(obj, exc, null);
				throw;
			}
			catch (StorageTransientException exc2)
			{
				OleConverter.SaveFailedConversionData(obj, exc2, null);
				throw;
			}
			finally
			{
				Interlocked.Decrement(ref this.nQueueSize);
				if (obj != null)
				{
					OleConverter.DestroyOleObjectData(obj);
				}
			}
			return result;
		}

		// Token: 0x06003F91 RID: 16273 RVA: 0x00108210 File Offset: 0x00106410
		internal void ForceRunDirectoryCleanup()
		{
			OleConverter.DirectoryCleanup(this.cleanupLock, true);
		}

		// Token: 0x06003F92 RID: 16274 RVA: 0x00108220 File Offset: 0x00106420
		private static object CreateOleObjectData(Stream oleDataStream, bool canCacheInMemory)
		{
			if (canCacheInMemory && oleDataStream.CanSeek)
			{
				long length = oleDataStream.Length;
				if (length <= 262144L)
				{
					byte[] array = new byte[length];
					int num;
					for (int i = 0; i < array.Length; i += num)
					{
						num = oleDataStream.Read(array, i, (int)length - i);
						if (num == 0)
						{
							StorageGlobals.ContextTraceError(ExTraceGlobals.CcOleTracer, "OleConverter::CreateOleObjectData: unable to load full stream.");
							throw new OleConversionFailedException(ServerStrings.OleUnableToReadAttachment);
						}
					}
					return array;
				}
			}
			return OleConverter.SaveStreamToTempFile(oleDataStream);
		}

		// Token: 0x06003F93 RID: 16275 RVA: 0x00108290 File Offset: 0x00106490
		private static void DestroyOleObjectData(object oleObjectData)
		{
			string text = oleObjectData as string;
			if (text != null)
			{
				OleConverter.DeleteTempFile(text);
			}
		}

		// Token: 0x06003F94 RID: 16276 RVA: 0x001082AD File Offset: 0x001064AD
		internal static void DeleteTempFile(string filename)
		{
			NativeMethods.DeleteFile(filename);
		}

		// Token: 0x06003F95 RID: 16277 RVA: 0x001082B8 File Offset: 0x001064B8
		private static string SaveStreamToTempFile(Stream stream)
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			string conversionsDirectory = OleConverter.GetConversionsDirectory(true);
			if (NativeMethods.GetTempFileName(conversionsDirectory, "ole", 0U, stringBuilder) == 0)
			{
				StorageGlobals.ContextTraceError<string, int>(ExTraceGlobals.CcOleTracer, "OleConverter::SaveStreamToTempFile: failed to create temp file name, directory = {0}. Error {1}", conversionsDirectory, Marshal.GetLastWin32Error());
				throw new OleConversionFailedException(ServerStrings.OleConversionPrepareFailed);
			}
			try
			{
				using (FileStream fileStream = new FileStream(stringBuilder.ToString(), FileMode.OpenOrCreate, FileAccess.Write))
				{
					Util.StreamHandler.CopyStreamData(stream, fileStream);
					fileStream.Close();
				}
			}
			catch (IOException arg)
			{
				OleConverter.DeleteTempFile(stringBuilder.ToString());
				StorageGlobals.ContextTraceError<IOException>(ExTraceGlobals.CcOleTracer, "OleConverter::SaveStreamToTempFile: IOException caught. Exception:\n {0}", arg);
				throw new OleConversionFailedException(ServerStrings.OleConversionPrepareFailed);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003F96 RID: 16278 RVA: 0x0010837C File Offset: 0x0010657C
		private static Stream CreateResultStream(object result)
		{
			string text = result as string;
			if (text != null)
			{
				try
				{
					return new OleConverter.ConversionResultFileStream(text);
				}
				catch (IOException arg)
				{
					OleConverter.DeleteTempFile(text);
					StorageGlobals.ContextTraceError<IOException>(ExTraceGlobals.CcOleTracer, "OleConverter::CreateResultStream: IOException caught. Exception:\n {0}", arg);
					throw new OleConversionFailedException(ServerStrings.OleConversionResultFailed);
				}
			}
			byte[] array = result as byte[];
			if (array != null)
			{
				return new MemoryStream(array);
			}
			StorageGlobals.ContextTraceError<Type>(ExTraceGlobals.CcOleTracer, "OleConverter::CreateResultStream: result type is invalid, {0}.", result.GetType());
			throw new OleConversionFailedException(ServerStrings.OleConversionInvalidResultType);
		}

		// Token: 0x06003F97 RID: 16279 RVA: 0x00108400 File Offset: 0x00106600
		private static ExDateTime GetFileCreateTimeUtc(ref NativeMethods.WIN32_FIND_DATA findData)
		{
			long fileTime = ((long)findData.CreationTime.dwHighDateTime << 32) + (long)findData.CreationTime.dwLowDateTime;
			return ExDateTime.FromFileTimeUtc(fileTime);
		}

		// Token: 0x06003F98 RID: 16280 RVA: 0x00108430 File Offset: 0x00106630
		private static void DirectoryCleanup(object cleanupLock)
		{
			OleConverter.DirectoryCleanup(cleanupLock, false);
		}

		// Token: 0x06003F99 RID: 16281 RVA: 0x0010843C File Offset: 0x0010663C
		private static void DirectoryCleanup(object cleanupLock, bool isForced)
		{
			string conversionsDirectory = OleConverter.GetConversionsDirectory(false);
			if (conversionsDirectory == null)
			{
				return;
			}
			try
			{
				if (Monitor.TryEnter(cleanupLock) || (isForced && Monitor.TryEnter(cleanupLock, 30000)))
				{
					NativeMethods.WIN32_FIND_DATA win32_FIND_DATA = default(NativeMethods.WIN32_FIND_DATA);
					using (SafeFindHandle safeFindHandle = NativeMethods.FindFirstFile(Path.Combine(conversionsDirectory, "*"), out win32_FIND_DATA))
					{
						if (!safeFindHandle.IsInvalid)
						{
							do
							{
								if ((win32_FIND_DATA.FileAttributes & NativeMethods.FileAttributes.Directory) != NativeMethods.FileAttributes.Directory)
								{
									ExDateTime fileCreateTimeUtc = OleConverter.GetFileCreateTimeUtc(ref win32_FIND_DATA);
									TimeSpan t = ExDateTime.UtcNow.Subtract(fileCreateTimeUtc);
									if (t > OleConverter.MaxFileLifetime)
									{
										NativeMethods.DeleteFile(Path.Combine(conversionsDirectory, win32_FIND_DATA.FileName));
									}
								}
							}
							while (NativeMethods.FindNextFile(safeFindHandle, out win32_FIND_DATA));
						}
					}
				}
			}
			finally
			{
				if (Monitor.IsEntered(cleanupLock))
				{
					Monitor.Exit(cleanupLock);
				}
			}
		}

		// Token: 0x06003F9A RID: 16282 RVA: 0x00108520 File Offset: 0x00106720
		private static bool RunAsLocalService()
		{
			return string.Compare(Environment.UserName, "SYSTEM", StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06003F9B RID: 16283 RVA: 0x00108538 File Offset: 0x00106738
		private static Mutex CreateWorkerLaunchMutex()
		{
			try
			{
				return Mutex.OpenExisting("OleConverterProcessStartMutex", MutexRights.Modify | MutexRights.Synchronize);
			}
			catch (WaitHandleCannotBeOpenedException)
			{
			}
			SecurityIdentifier[] array = new SecurityIdentifier[]
			{
				new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null),
				new SecurityIdentifier(WellKnownSidType.NetworkServiceSid, null),
				new SecurityIdentifier(WellKnownSidType.LocalServiceSid, null),
				new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null),
				new SecurityIdentifier(WellKnownSidType.AuthenticatedUserSid, null)
			};
			MutexSecurity mutexSecurity = new MutexSecurity();
			foreach (SecurityIdentifier identity in array)
			{
				MutexAccessRule rule = new MutexAccessRule(identity, MutexRights.Modify | MutexRights.Synchronize, AccessControlType.Allow);
				mutexSecurity.AddAccessRule(rule);
			}
			bool flag;
			return new Mutex(false, "OleConverterProcessStartMutex", ref flag, mutexSecurity);
		}

		// Token: 0x06003F9C RID: 16284 RVA: 0x001085FC File Offset: 0x001067FC
		private static void SaveFailedConversionData(object conversionData, Exception exc, string logDirectoryPath)
		{
			string failedOutboundConversionsDirectory = ConvertUtils.GetFailedOutboundConversionsDirectory(logDirectoryPath);
			if (failedOutboundConversionsDirectory != null)
			{
				try
				{
					string str = Path.Combine(failedOutboundConversionsDirectory, Guid.NewGuid().ToString());
					string path = str + ".txt";
					string path2 = str + ".ole";
					using (FileStream fileStream = new FileStream(path, FileMode.CreateNew, FileAccess.Write))
					{
						using (StreamWriter streamWriter = new StreamWriter(fileStream))
						{
							streamWriter.WriteLine(exc.ToString());
							streamWriter.Close();
						}
						fileStream.Close();
					}
					if (conversionData != null)
					{
						using (FileStream fileStream2 = new FileStream(path2, FileMode.CreateNew, FileAccess.Write))
						{
							string text = conversionData as string;
							if (text != null)
							{
								using (FileStream fileStream3 = new FileStream(text, FileMode.Open, FileAccess.Read))
								{
									Util.StreamHandler.CopyStreamData(fileStream3, fileStream2);
									goto IL_DC;
								}
							}
							byte[] array = (byte[])conversionData;
							fileStream2.Write(array, 0, array.Length);
							IL_DC:;
						}
					}
				}
				catch (IOException)
				{
				}
			}
		}

		// Token: 0x040022F0 RID: 8944
		private const string OleFileExtension = ".ole";

		// Token: 0x040022F1 RID: 8945
		private const string ErrorInfoExtension = ".txt";

		// Token: 0x040022F2 RID: 8946
		private const int ImageSizeConversionThresholdBytes = 4194304;

		// Token: 0x040022F3 RID: 8947
		private const int ImageSizeMarshallingThresholdBytes = 262144;

		// Token: 0x040022F4 RID: 8948
		private const string ConversionsSubdir = "Working\\OleConverter";

		// Token: 0x040022F5 RID: 8949
		private const string TempFilenamePrefix = "ole";

		// Token: 0x040022F6 RID: 8950
		private const int TempFilenameBufferLength = 256;

		// Token: 0x040022F7 RID: 8951
		private const int WorkerProcessMemoryLimit = 20971520;

		// Token: 0x040022F8 RID: 8952
		private const string WorkerProcessPath = "bin\\OleConverter.exe";

		// Token: 0x040022F9 RID: 8953
		private const int WorkerProcessTransactionsLimit = 512;

		// Token: 0x040022FA RID: 8954
		private const int WorkerProcessTransactionTimeout = 15000;

		// Token: 0x040022FB RID: 8955
		private const int WorkerProcessAllocationTimeout = 10000;

		// Token: 0x040022FC RID: 8956
		private const int WorkerProcessLifetimeLimit = 600000;

		// Token: 0x040022FD RID: 8957
		private const int WorkerProcessIdleTimeout = 120000;

		// Token: 0x040022FE RID: 8958
		private const string WorkerLaunchMutexName = "OleConverterProcessStartMutex";

		// Token: 0x040022FF RID: 8959
		private const int MemoryCacheQueueSizeLimit = 30;

		// Token: 0x04002300 RID: 8960
		private static readonly int MaxWorkerNumber = Environment.ProcessorCount * 3;

		// Token: 0x04002301 RID: 8961
		private static readonly Mutex WorkerLaunchMutex = OleConverter.CreateWorkerLaunchMutex();

		// Token: 0x04002302 RID: 8962
		private static Guid oleConverterClassId = OleConverter.RunAsLocalService() ? new Guid("{B5D1252D-4EE6-47CF-AE46-9D1223806F8E}") : new Guid("{B5D12274-5222-418D-8D7B-7D7F674FC111}");

		// Token: 0x04002303 RID: 8963
		private static string extraParams = OleConverter.RunAsLocalService() ? "-RunAs LocalService" : null;

		// Token: 0x04002304 RID: 8964
		private static readonly ComWorkerConfiguration.RunAsFlag RunAsFlag = ComWorkerConfiguration.RunAsFlag.MayRunUnderAnotherJobObject | (OleConverter.RunAsLocalService() ? ComWorkerConfiguration.RunAsFlag.RunAsLocalService : ComWorkerConfiguration.RunAsFlag.None);

		// Token: 0x04002305 RID: 8965
		private static readonly ComWorkerConfiguration WorkerConfigurationObject = new ComWorkerConfiguration(Path.Combine(ConvertUtils.ExchangeSetupPath, "bin\\OleConverter.exe"), OleConverter.extraParams, OleConverter.oleConverterClassId, OleConverter.RunAsFlag, OleConverter.WorkerLaunchMutex, 20971520, 600000, 10000, 512, 15000, 120000);

		// Token: 0x04002306 RID: 8966
		private int nQueueSize;

		// Token: 0x04002307 RID: 8967
		private static readonly TimeSpan CleanupTimespan = new TimeSpan(0, 30, 0);

		// Token: 0x04002308 RID: 8968
		private static readonly TimeSpan MaxFileLifetime = new TimeSpan(0, 5, 0);

		// Token: 0x04002309 RID: 8969
		private Timer cleanupTimer;

		// Token: 0x0400230A RID: 8970
		private object cleanupLock;

		// Token: 0x02000609 RID: 1545
		internal class ConversionRequestParameters
		{
			// Token: 0x06003F9E RID: 16286 RVA: 0x00108809 File Offset: 0x00106A09
			internal ConversionRequestParameters(object requestData, object responseData)
			{
				this.requestData = requestData;
				this.responseData = responseData;
			}

			// Token: 0x170012F7 RID: 4855
			// (get) Token: 0x06003F9F RID: 16287 RVA: 0x0010881F File Offset: 0x00106A1F
			// (set) Token: 0x06003FA0 RID: 16288 RVA: 0x00108827 File Offset: 0x00106A27
			internal object RequestData
			{
				get
				{
					return this.requestData;
				}
				set
				{
					this.requestData = value;
				}
			}

			// Token: 0x170012F8 RID: 4856
			// (get) Token: 0x06003FA1 RID: 16289 RVA: 0x00108830 File Offset: 0x00106A30
			// (set) Token: 0x06003FA2 RID: 16290 RVA: 0x00108838 File Offset: 0x00106A38
			internal object ResponseData
			{
				get
				{
					return this.responseData;
				}
				set
				{
					this.responseData = value;
				}
			}

			// Token: 0x0400230B RID: 8971
			private object requestData;

			// Token: 0x0400230C RID: 8972
			private object responseData;
		}

		// Token: 0x0200060A RID: 1546
		private static class InstanceCreator
		{
			// Token: 0x0400230D RID: 8973
			public static OleConverter Instance = new OleConverter();
		}

		// Token: 0x0200060B RID: 1547
		private class ConversionResultFileStream : FileStream
		{
			// Token: 0x06003FA4 RID: 16292 RVA: 0x0010884D File Offset: 0x00106A4D
			internal ConversionResultFileStream(string filename) : base(filename, FileMode.Open, FileAccess.Read)
			{
				this.filename = filename;
				this.isDeleted = false;
			}

			// Token: 0x06003FA5 RID: 16293 RVA: 0x00108866 File Offset: 0x00106A66
			public override void Close()
			{
				base.Close();
				if (!this.isDeleted)
				{
					OleConverter.DeleteTempFile(this.filename);
					this.isDeleted = true;
				}
			}

			// Token: 0x06003FA6 RID: 16294 RVA: 0x00108888 File Offset: 0x00106A88
			protected override void Dispose(bool disposing)
			{
				base.Dispose(disposing);
				if (!this.isDeleted && this.filename != null)
				{
					OleConverter.DeleteTempFile(this.filename);
					this.isDeleted = true;
				}
			}

			// Token: 0x0400230E RID: 8974
			private readonly string filename;

			// Token: 0x0400230F RID: 8975
			private bool isDeleted;
		}
	}
}
