using System;
using System.Collections.Generic;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using Microsoft.Exchange.Audio;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200016A RID: 362
	internal static class TempFileFactory
	{
		// Token: 0x06000B6F RID: 2927 RVA: 0x0002A2F4 File Offset: 0x000284F4
		internal static void StartCleanUpTimer()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, 0, "TempFileFactory.StartCleanUpTimer", new object[0]);
			if (TempFileFactory.cleanUpTimer != null)
			{
				throw new InvalidOperationException("Cleanup timer already started");
			}
			TimeSpan timeSpan = TimeSpan.FromSeconds(TempFileFactory.cleanUpDelayQueue.RetryInterval.TotalSeconds / 2.0);
			TempFileFactory.cleanUpTimer = new Timer(new TimerCallback(TempFileFactory.CleanUpTimerCallback), null, timeSpan, timeSpan);
			TempFileFactory.timerSyncEvent.Set();
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x0002A374 File Offset: 0x00028574
		internal static void StopCleanUpTimer()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, 0, "TempFileFactory.StopCleanUpTimer", new object[0]);
			if (TempFileFactory.cleanUpTimer != null)
			{
				TempFileFactory.cleanUpTimer.Dispose();
				TempFileFactory.cleanUpTimer = null;
				TimeSpan timeout = TimeSpan.FromSeconds(30.0);
				if (!TempFileFactory.timerSyncEvent.WaitOne(timeout, false))
				{
					CallIdTracer.TraceError(ExTraceGlobals.UtilTracer, 0, "TempFileFactory.Shutdown: Didn't get timerSyncEvent within {0}s", new object[]
					{
						timeout.TotalSeconds
					});
				}
			}
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0002A3FC File Offset: 0x000285FC
		internal static ITempFile CreateTempFile()
		{
			return TempFileFactory.CreateTempFile(".tmp");
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x0002A408 File Offset: 0x00028608
		internal static ITempFile CreateTempDir()
		{
			TempFileFactory.TempDir tempDir = new TempFileFactory.TempDir();
			TempFileFactory.AddFileToTable(tempDir);
			return tempDir;
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x0002A422 File Offset: 0x00028622
		internal static ITempFile CreateTempWmaFile()
		{
			return TempFileFactory.CreateTempFile(".wma");
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0002A42E File Offset: 0x0002862E
		internal static ITempFile CreateTempMp3File()
		{
			return TempFileFactory.CreateTempFile(".mp3");
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x0002A43A File Offset: 0x0002863A
		internal static ITempFile CreateTempSoundFileFromAttachmentName(string attachmentName)
		{
			if (AudioFile.IsWma(attachmentName) || AudioFile.IsProtectedWma(attachmentName))
			{
				return TempFileFactory.CreateTempWmaFile();
			}
			if (AudioFile.IsMp3(attachmentName) || AudioFile.IsProtectedMp3(attachmentName))
			{
				return TempFileFactory.CreateTempMp3File();
			}
			return TempFileFactory.CreateTempWavFile();
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x0002A470 File Offset: 0x00028670
		internal static ITempFile CreateTempFileFromAttachment(Attachment attachment)
		{
			ITempFile result = null;
			string contentType;
			if ((contentType = attachment.ContentType) != null)
			{
				if (!(contentType == "image/tiff"))
				{
					if (contentType == "audio/wav")
					{
						result = TempFileFactory.CreateTempSoundFileFromAttachmentName(attachment.FileName);
					}
				}
				else
				{
					result = TempFileFactory.CreateTempTifFile();
				}
			}
			return result;
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x0002A4BA File Offset: 0x000286BA
		internal static ITempFile CreateTempTifFile()
		{
			return TempFileFactory.CreateTempFile(".tif");
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0002A4C8 File Offset: 0x000286C8
		internal static ITempFile CreateTempGrammarFile(string fileNameWithoutExtension, string extension)
		{
			string fileName = string.IsNullOrEmpty(fileNameWithoutExtension) ? Guid.NewGuid().ToString() : fileNameWithoutExtension;
			ITempFile tempFile = TempFileFactory.CreateTempFile(fileName, extension);
			TempFileFactory.AddNetworkServiceReadAccess(tempFile.FilePath, false);
			return tempFile;
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0002A509 File Offset: 0x00028709
		internal static ITempFile CreateTempGrammarFile(string fileNameWithoutExtension)
		{
			return TempFileFactory.CreateTempGrammarFile(fileNameWithoutExtension, ".grxml");
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0002A516 File Offset: 0x00028716
		internal static ITempFile CreateTempGrammarFile()
		{
			return TempFileFactory.CreateTempGrammarFile(null, ".grxml");
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0002A523 File Offset: 0x00028723
		internal static ITempFile CreateTempCompiledGrammarFile(string fileNameWithoutExtension)
		{
			return TempFileFactory.CreateTempGrammarFile(fileNameWithoutExtension, ".cfg");
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x0002A530 File Offset: 0x00028730
		internal static ITempFile CreateTempCompiledGrammarFile()
		{
			return TempFileFactory.CreateTempCompiledGrammarFile(Guid.NewGuid().ToString());
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0002A555 File Offset: 0x00028755
		internal static ITempWavFile CreateTempWavFile()
		{
			return TempFileFactory.CreateTempWavFile(false);
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0002A560 File Offset: 0x00028760
		internal static ITempWavFile CreateTempWavFile(string extraInfo)
		{
			ITempWavFile tempWavFile = TempFileFactory.CreateTempWavFile(false);
			tempWavFile.ExtraInfo = extraInfo;
			return tempWavFile;
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x0002A57C File Offset: 0x0002877C
		internal static ITempWavFile CreateTempWavFile(bool addWriteAccessToo)
		{
			return TempFileFactory.CreateTempWavFile(addWriteAccessToo, ".wav");
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0002A58C File Offset: 0x0002878C
		internal static ITempWavFile CreateTempWavFile(bool addWriteAccessToo, string fileExtension)
		{
			TempFileFactory.TempWavFile tempWavFile = new TempFileFactory.TempWavFile(fileExtension, null);
			TempFileFactory.AddFileToTable(tempWavFile);
			TempFileFactory.AddNetworkServiceReadAccess(tempWavFile.FilePath, addWriteAccessToo);
			return tempWavFile;
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0002A5B4 File Offset: 0x000287B4
		internal static void DisposeSessionFiles(string sessionId)
		{
			lock (TempFileFactory.tempFileTable)
			{
				if (sessionId != null)
				{
					List<ITempFile> list;
					TempFileFactory.tempFileTable.TryGetValue(sessionId, out list);
					if (list != null)
					{
						lock (TempFileFactory.cleanUpDelayQueue)
						{
							TempFileFactory.cleanUpDelayQueue.Enqueue(list);
						}
						TempFileFactory.tempFileTable.Remove(sessionId);
					}
				}
			}
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x0002A644 File Offset: 0x00028844
		internal static void AddNetworkServiceReadAccess(string filePath, bool addWriteAccessToo)
		{
			if (!File.Exists(filePath))
			{
				using (File.Create(filePath))
				{
				}
			}
			SecurityIdentifier securityIdentifier = new SecurityIdentifier(WellKnownSidType.NetworkServiceSid, null);
			NTAccount identity = (NTAccount)securityIdentifier.Translate(typeof(NTAccount));
			FileSecurity accessControl = File.GetAccessControl(filePath);
			accessControl.AddAccessRule(new FileSystemAccessRule(identity, addWriteAccessToo ? (FileSystemRights.ReadData | FileSystemRights.WriteData | FileSystemRights.AppendData | FileSystemRights.ReadExtendedAttributes | FileSystemRights.WriteExtendedAttributes | FileSystemRights.ReadAttributes | FileSystemRights.WriteAttributes | FileSystemRights.ReadPermissions) : FileSystemRights.Read, AccessControlType.Allow));
			File.SetAccessControl(filePath, accessControl);
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x0002A6C8 File Offset: 0x000288C8
		private static void AddFileToTable(ITempFile file)
		{
			lock (TempFileFactory.tempFileTable)
			{
				string text = CallId.Id;
				if (text == null)
				{
					text = "NULL";
				}
				List<ITempFile> list;
				TempFileFactory.tempFileTable.TryGetValue(text, out list);
				if (list == null)
				{
					list = new List<ITempFile>();
					TempFileFactory.tempFileTable.Add(text, list);
				}
				list.Add(file);
			}
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0002A73C File Offset: 0x0002893C
		private static ITempFile CreateTempFile(string fileName, string fileExtension)
		{
			TempFileFactory.TempFile tempFile = new TempFileFactory.TempFile(fileName, fileExtension);
			TempFileFactory.AddFileToTable(tempFile);
			return tempFile;
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0002A758 File Offset: 0x00028958
		private static ITempFile CreateTempFile(string fileExtension)
		{
			return TempFileFactory.CreateTempFile(Guid.NewGuid().ToString(), fileExtension);
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0002A780 File Offset: 0x00028980
		private static void CleanUpTimerCallback(object state)
		{
			bool flag = false;
			try
			{
				flag = TempFileFactory.timerSyncEvent.WaitOne(0, false);
				if (!flag || TempFileFactory.cleanUpTimer == null)
				{
					CallIdTracer.TraceWarning(ExTraceGlobals.UtilTracer, 0, "TempFileFactory.CleanUpTimerCallback: Shutdown or overlaping timer calls", new object[0]);
				}
				else
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, 0, "TempFileFactory.CleanUpTimerCallback: Sync event acquired.", new object[0]);
					List<ITempFile> list = null;
					lock (TempFileFactory.cleanUpDelayQueue)
					{
						List<ITempFile> collection;
						while ((collection = TempFileFactory.cleanUpDelayQueue.Dequeue()) != null)
						{
							CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, 0, "TempFileFactory.CleanUpTimerCallback: Found files to delete.", new object[0]);
							if (list == null)
							{
								list = new List<ITempFile>();
							}
							list.AddRange(collection);
						}
					}
					if (list != null)
					{
						foreach (ITempFile tempFile in list)
						{
							tempFile.Dispose();
						}
					}
				}
			}
			catch (Exception ex)
			{
				CallIdTracer.TraceError(ExTraceGlobals.UtilTracer, 0, "TempFileFactory.CleanUpTimerCallback: {0}", new object[]
				{
					ex
				});
				if (!GrayException.IsGrayException(ex))
				{
					throw;
				}
				ExWatson.SendReport(ex, ReportOptions.None, null);
			}
			finally
			{
				if (flag)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, 0, "TempFileFactory.CleanUpTimerCallback: Signaling the sync event...", new object[0]);
					TempFileFactory.timerSyncEvent.Set();
				}
			}
		}

		// Token: 0x0400061D RID: 1565
		private const string NullSessionId = "NULL";

		// Token: 0x0400061E RID: 1566
		private static Dictionary<string, List<ITempFile>> tempFileTable = new Dictionary<string, List<ITempFile>>();

		// Token: 0x0400061F RID: 1567
		private static RetryQueue<List<ITempFile>> cleanUpDelayQueue = new RetryQueue<List<ITempFile>>(ExTraceGlobals.UtilTracer, TimeSpan.FromMinutes(1.0));

		// Token: 0x04000620 RID: 1568
		private static AutoResetEvent timerSyncEvent = new AutoResetEvent(false);

		// Token: 0x04000621 RID: 1569
		private static Timer cleanUpTimer;

		// Token: 0x0200016B RID: 363
		private class TempFile : DisposableBase, ITempFile, IDisposeTrackable, IDisposable
		{
			// Token: 0x06000B88 RID: 2952 RVA: 0x0002A978 File Offset: 0x00028B78
			internal TempFile(string fileName)
			{
				this.fileName = fileName;
			}

			// Token: 0x06000B89 RID: 2953 RVA: 0x0002A98E File Offset: 0x00028B8E
			internal TempFile(string fileName, string fileExtension) : this(fileName + fileExtension)
			{
			}

			// Token: 0x170002C3 RID: 707
			// (get) Token: 0x06000B8A RID: 2954 RVA: 0x0002A99D File Offset: 0x00028B9D
			public string FilePath
			{
				get
				{
					return Path.Combine(Utils.UMTempPath, this.fileName);
				}
			}

			// Token: 0x06000B8B RID: 2955 RVA: 0x0002A9AF File Offset: 0x00028BAF
			public override string ToString()
			{
				return this.FilePath;
			}

			// Token: 0x06000B8C RID: 2956 RVA: 0x0002A9B7 File Offset: 0x00028BB7
			public void KeepAlive()
			{
				this.deleteOnDispose = false;
			}

			// Token: 0x06000B8D RID: 2957 RVA: 0x0002A9C0 File Offset: 0x00028BC0
			protected virtual void DeleteFile()
			{
				File.Delete(this.FilePath);
			}

			// Token: 0x06000B8E RID: 2958 RVA: 0x0002A9D0 File Offset: 0x00028BD0
			protected override void InternalDispose(bool disposing)
			{
				if (disposing)
				{
					if (!this.deleteOnDispose)
					{
						CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, this, "Skipping deletion of tempfile {0}", new object[]
						{
							this.FilePath
						});
						return;
					}
					try
					{
						if (this.FilePath != null)
						{
							this.DeleteFile();
						}
						CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, this, "Successfully deleted tempfile {0}", new object[]
						{
							this.FilePath
						});
					}
					catch (IOException ex)
					{
						CallIdTracer.TraceError(ExTraceGlobals.UtilTracer, this, "Could not delete file={0}", new object[]
						{
							ex
						});
					}
					catch (UnauthorizedAccessException ex2)
					{
						CallIdTracer.TraceError(ExTraceGlobals.UtilTracer, this, "Could not delete file. It might be used by a different process. {0}", new object[]
						{
							ex2
						});
					}
				}
			}

			// Token: 0x06000B8F RID: 2959 RVA: 0x0002AAA0 File Offset: 0x00028CA0
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<TempFileFactory.TempFile>(this);
			}

			// Token: 0x04000622 RID: 1570
			private string fileName;

			// Token: 0x04000623 RID: 1571
			private bool deleteOnDispose = true;
		}

		// Token: 0x0200016C RID: 364
		private class TempWavFile : TempFileFactory.TempFile, ITempWavFile, ITempFile, IDisposeTrackable, IDisposable
		{
			// Token: 0x06000B90 RID: 2960 RVA: 0x0002AAA8 File Offset: 0x00028CA8
			internal TempWavFile(string extraInfo) : this(".wav", extraInfo)
			{
			}

			// Token: 0x06000B91 RID: 2961 RVA: 0x0002AAB8 File Offset: 0x00028CB8
			internal TempWavFile(string extension, string extraInfo) : base(Guid.NewGuid().ToString(), extension)
			{
				this.extraInfo = extraInfo;
			}

			// Token: 0x170002C4 RID: 708
			// (get) Token: 0x06000B92 RID: 2962 RVA: 0x0002AAE6 File Offset: 0x00028CE6
			// (set) Token: 0x06000B93 RID: 2963 RVA: 0x0002AAEE File Offset: 0x00028CEE
			public string ExtraInfo
			{
				get
				{
					return this.extraInfo;
				}
				set
				{
					this.extraInfo = value;
				}
			}

			// Token: 0x06000B94 RID: 2964 RVA: 0x0002AAF7 File Offset: 0x00028CF7
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<TempFileFactory.TempWavFile>(this);
			}

			// Token: 0x04000624 RID: 1572
			private string extraInfo;
		}

		// Token: 0x0200016D RID: 365
		private class TempDir : TempFileFactory.TempFile
		{
			// Token: 0x06000B95 RID: 2965 RVA: 0x0002AB00 File Offset: 0x00028D00
			internal TempDir() : base(Guid.NewGuid().ToString())
			{
				Directory.CreateDirectory(base.FilePath);
			}

			// Token: 0x06000B96 RID: 2966 RVA: 0x0002AB32 File Offset: 0x00028D32
			protected override void DeleteFile()
			{
				Directory.Delete(base.FilePath, true);
			}

			// Token: 0x06000B97 RID: 2967 RVA: 0x0002AB40 File Offset: 0x00028D40
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<TempFileFactory.TempDir>(this);
			}
		}
	}
}
