using System;
using System.Collections.Generic;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using System.Web;
using Microsoft.Exchange.Clients.EventLogs;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Core.Transcoding
{
	// Token: 0x020002E4 RID: 740
	internal class Cache : DisposeTrackableBase
	{
		// Token: 0x06001C60 RID: 7264 RVA: 0x000A2980 File Offset: 0x000A0B80
		public Cache(Cache.CacheOption option)
		{
			if (string.IsNullOrEmpty(option.RootPath))
			{
				ExTraceGlobals.TranscodingTracer.TraceError((long)this.GetHashCode(), "ctor: root path is empty.");
				throw new ArgumentException("ctor: root path is empty.");
			}
			this.option = option;
			this.option.RootPath = Path.Combine(this.option.RootPath, "XCCache");
			this.cleanupThread = new Thread(new ThreadStart(this.CleanupThreadProc));
			this.cleanupThread.Start();
			if (Directory.Exists(this.option.RootPath))
			{
				try
				{
					Directory.Delete(this.option.RootPath, true);
				}
				catch (IOException arg)
				{
					ExTraceGlobals.TranscodingTracer.TraceError<IOException>((long)this.GetHashCode(), "Failed to delete the cache folders when intializing the Cache instance. Exception message: {0}", arg);
					DirectoryInfo dirInfo = new DirectoryInfo(this.option.RootPath);
					this.cacheSize = this.GetFolderSize(dirInfo);
				}
				catch (UnauthorizedAccessException ex)
				{
					OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_TranscodingCacheFolderDeletingAccessDenied, string.Empty, new object[]
					{
						ex.Message
					});
					throw new TranscodingFatalFaultException("Failed to delete the cache root folder. Exception message: " + ex.ToString(), ex, this);
				}
			}
			string path = Cache.GenerateGuidString();
			this.processFolder = Path.Combine(this.option.RootPath, path);
			if (Directory.Exists(this.processFolder))
			{
				OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_TranscodingManagerInitializationFailed, string.Empty, new object[]
				{
					string.Empty
				});
				throw new TranscodingFatalFaultException("The sub folder for the current OWA process has already existed.", null, this);
			}
			SecurityIdentifier identity;
			SecurityIdentifier identity2;
			try
			{
				identity = new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null);
				identity2 = new SecurityIdentifier(WellKnownSidType.LocalServiceSid, null);
			}
			catch (SystemException ex2)
			{
				ExTraceGlobals.TranscodingTracer.TraceError((long)this.GetHashCode(), "Failed to create security identifier in Cache.ctor.");
				OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_TranscodingManagerInitializationFailed, string.Empty, new object[]
				{
					ex2.Message
				});
				throw new TranscodingFatalFaultException("Failed to create security identifier in Cache.ctor.", ex2, this);
			}
			this.localServiceReadRule = new FileSystemAccessRule(identity2, FileSystemRights.Read, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow);
			this.localServiceWriteRule = new FileSystemAccessRule(identity2, FileSystemRights.Write, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow);
			this.localServiceCreateFileRule = new FileSystemAccessRule(identity2, FileSystemRights.WriteData, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow);
			this.localServiceDeleteFileRule = new FileSystemAccessRule(identity2, FileSystemRights.Delete, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow);
			FileSystemAccessRule rule = new FileSystemAccessRule(identity, FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow);
			this.directorySercurity = new DirectorySecurity();
			this.directorySercurity.SetAccessRuleProtection(true, false);
			this.directorySercurity.AddAccessRule(rule);
			try
			{
				if (!Directory.Exists(this.option.RootPath))
				{
					Directory.CreateDirectory(this.option.RootPath, this.directorySercurity);
				}
				this.directorySercurity.AddAccessRule(this.localServiceReadRule);
				this.directorySercurity.AddAccessRule(this.localServiceWriteRule);
				this.directorySercurity.AddAccessRule(this.localServiceCreateFileRule);
				this.directorySercurity.AddAccessRule(this.localServiceDeleteFileRule);
				Directory.CreateDirectory(this.processFolder, this.directorySercurity);
			}
			catch (IOException ex3)
			{
				OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_TranscodingCacheFolderCreationFailed, string.Empty, new object[]
				{
					ex3.Message
				});
				throw new TranscodingFatalFaultException("Failed to create the sub folder and set the ACL for the current OWA process. Exception message: " + ex3.ToString(), ex3, this);
			}
			catch (UnauthorizedAccessException ex4)
			{
				OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_TranscodingCacheFolderACLSettingAccessDenied, string.Empty, new object[]
				{
					ex4.Message
				});
				throw new TranscodingFatalFaultException("Failed to create the sub folder and set the ACL for the current OWA process. Exception message: " + ex4.ToString(), ex4, this);
			}
		}

		// Token: 0x06001C61 RID: 7265 RVA: 0x000A2D68 File Offset: 0x000A0F68
		public void CacheInputDocument(string sessionId, string documentId, Stream documentStream, out string sourceDocumentPath, out string targetDocumentPath)
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("the cache system has been disposed already.");
			}
			if (string.IsNullOrEmpty(sessionId))
			{
				ExTraceGlobals.TranscodingTracer.TraceError((long)this.GetHashCode(), "the session ID is empty.");
				throw new ArgumentException("the session ID is empty.");
			}
			if (string.IsNullOrEmpty(documentId))
			{
				ExTraceGlobals.TranscodingTracer.TraceError((long)this.GetHashCode(), "the document ID is empty.");
				throw new ArgumentException("the document ID is empty.");
			}
			if (documentStream == null)
			{
				ExTraceGlobals.TranscodingTracer.TraceError((long)this.GetHashCode(), "the document data stream is null.");
				throw new ArgumentNullException("documentStream");
			}
			bool flag = false;
			int num = 0;
			lock (this.myLock)
			{
				this.transcodingTaskCount++;
				if (100 == this.transcodingTaskCount)
				{
					this.activeCleanupEvent.Set();
					this.transcodingTaskCount = 0;
				}
				int num2 = this.cacheSize;
				if (num2 > (int)(0.8 * (double)this.option.TotalSizeQuota))
				{
					this.activeCleanupEvent.Set();
				}
				sourceDocumentPath = this.GetAvailableSourceDocument(sessionId, documentId);
				flag = (sourceDocumentPath != null);
				int num3 = flag ? this.option.OutputThreshold : (this.option.OutputThreshold + this.option.InputThreshold);
				if (num2 + num3 > this.option.TotalSizeQuota)
				{
					this.activeCleanupEvent.Set();
					OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_TranscodingCacheReachedQuota, string.Empty, new object[0]);
					throw new TranscodingFatalFaultException("The size of the cache system has reached the quota.", null, this);
				}
				if (!flag)
				{
					sourceDocumentPath = this.GenerateSourceDocumentName(sessionId, documentId);
				}
			}
			if (!flag)
			{
				try
				{
					string directoryName = Path.GetDirectoryName(sourceDocumentPath);
					if (!Directory.Exists(directoryName))
					{
						Directory.CreateDirectory(directoryName, this.directorySercurity);
					}
					this.SaveSourceStreamIntoFile(documentStream, sourceDocumentPath, out num);
					if (num == 0)
					{
						Directory.Delete(Path.GetDirectoryName(sourceDocumentPath), true);
						throw new TranscodingUnconvertibleFileException("the size of the source stream is zero.", null, this);
					}
					if (this.option.InputThreshold < num)
					{
						Directory.Delete(Path.GetDirectoryName(sourceDocumentPath), true);
						throw new TranscodingOverMaximumFileSizeException("the size of the source stream beyond the threshold.", null, this);
					}
				}
				catch (IOException ex)
				{
					lock (this.myLock)
					{
						this.UpdateCacheBeforeTranscoding(false, false, sessionId, documentId, Path.GetFileName(sourceDocumentPath), num);
					}
					throw new TranscodingFatalFaultException("Failed to store the source stream data to file. Exception message: " + ex.ToString(), ex, this);
				}
				catch (UnauthorizedAccessException ex2)
				{
					ExTraceGlobals.TranscodingTracer.TraceError<string>((long)this.GetHashCode(), "Failed to store the source stream data to file. Exception message: ", ex2.ToString());
					throw new TranscodingFatalFaultException("Failed to store the source stream data to file. Exception message: " + ex2.ToString(), ex2, this);
				}
			}
			lock (this.myLock)
			{
				this.UpdateCacheBeforeTranscoding(flag, true, sessionId, documentId, Path.GetFileName(sourceDocumentPath), num);
				targetDocumentPath = this.GenerateNewHtmlFilePath(sessionId, documentId);
			}
		}

		// Token: 0x06001C62 RID: 7266 RVA: 0x000A3090 File Offset: 0x000A1290
		public void TransmitFile(string sessionId, string documentId, string fileName, HttpResponse response)
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("the cache system has been disposed already.");
			}
			if (string.IsNullOrEmpty(sessionId))
			{
				ExTraceGlobals.TranscodingTracer.TraceError((long)this.GetHashCode(), "the session ID is empty.");
				throw new ArgumentException("the session ID is empty.");
			}
			if (string.IsNullOrEmpty(documentId))
			{
				ExTraceGlobals.TranscodingTracer.TraceError((long)this.GetHashCode(), "the document ID is empty.");
				throw new ArgumentException("the document ID is empty.");
			}
			if (string.IsNullOrEmpty(fileName))
			{
				ExTraceGlobals.TranscodingTracer.TraceError((long)this.GetHashCode(), "the file name is empty.");
				throw new ArgumentException("the file name is empty.");
			}
			if (response == null)
			{
				ExTraceGlobals.TranscodingTracer.TraceError((long)this.GetHashCode(), "the Http response is null.");
				throw new ArgumentNullException("response");
			}
			string text = null;
			lock (this.myLock)
			{
				if (!this.sessionDictionary.ContainsKey(sessionId) || !this.sessionDictionary[sessionId].Documents.ContainsKey(documentId))
				{
					throw new TranscodingFatalFaultException("Either the session Id or document Id is illegal.");
				}
				if (!this.sessionDictionary[sessionId].Documents[documentId].Files.ContainsKey(fileName))
				{
					throw new TranscodingFatalFaultException(string.Format("The file is not in the cache. SessionId:{0}, DocumentId:{1}, fileName:{2}.", sessionId, documentId, fileName), null, this);
				}
				if (this.sessionDictionary[sessionId].Documents[documentId].Files[fileName].Status != Cache.FileStatus.Ready)
				{
					return;
				}
				this.sessionDictionary[sessionId].Documents[documentId].Files[fileName].Status = Cache.FileStatus.Serving;
				text = Cache.CombinePath(new string[]
				{
					this.processFolder,
					sessionId,
					documentId,
					fileName
				});
			}
			try
			{
				if (text == null || !File.Exists(text))
				{
					throw new TranscodingFatalFaultException(string.Format("Failed to transmit the File. File--{0} is missing.", text), null, this);
				}
				this.TransmitFile(response, text);
			}
			finally
			{
				lock (this.myLock)
				{
					if (this.sessionDictionary.ContainsKey(sessionId) && this.sessionDictionary[sessionId].Documents.ContainsKey(documentId) && this.sessionDictionary[sessionId].Documents[documentId].Files.ContainsKey(fileName))
					{
						this.sessionDictionary[sessionId].Documents[documentId].Files[fileName].Status = Cache.FileStatus.Ready;
					}
				}
			}
		}

		// Token: 0x06001C63 RID: 7267 RVA: 0x000A336C File Offset: 0x000A156C
		public void NotifyTranscodingFinish(string sessionId, string documentId, string htmlFileName, out string rewrittenHtmlFileName, bool isTranscodingSuccess)
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("the cache system has been disposed already.");
			}
			if (string.IsNullOrEmpty(sessionId))
			{
				ExTraceGlobals.TranscodingTracer.TraceError((long)this.GetHashCode(), "the session ID is empty.");
				throw new ArgumentException("the session ID is empty.");
			}
			if (string.IsNullOrEmpty(documentId))
			{
				ExTraceGlobals.TranscodingTracer.TraceError((long)this.GetHashCode(), "the document ID is empty.");
				throw new ArgumentException("the document ID is empty.");
			}
			if (string.IsNullOrEmpty(htmlFileName))
			{
				ExTraceGlobals.TranscodingTracer.TraceError((long)this.GetHashCode(), "the html file name is empty.");
				throw new ArgumentException("the html file name is empty.");
			}
			if (!this.sessionDictionary.ContainsKey(sessionId) || !this.sessionDictionary[sessionId].Documents.ContainsKey(documentId))
			{
				throw new TranscodingFatalFaultException("Either the session Id or document Id is illegal.");
			}
			string text = Cache.CombinePath(new string[]
			{
				this.processFolder,
				sessionId,
				documentId,
				htmlFileName
			});
			string path = null;
			rewrittenHtmlFileName = null;
			try
			{
				if (isTranscodingSuccess)
				{
					if (!File.Exists(text))
					{
						throw new TranscodingFatalFaultException("The output html file doesn't exist.");
					}
					lock (this.myLock)
					{
						path = this.GenerateNewHtmlFilePath(sessionId, documentId);
					}
					rewrittenHtmlFileName = Path.GetFileName(path);
					using (FileStream fileStream = new FileStream(text, FileMode.Open))
					{
						using (FileStream fileStream2 = new FileStream(path, FileMode.CreateNew))
						{
							fileStream.Seek(0L, SeekOrigin.Begin);
							fileStream2.Seek(0L, SeekOrigin.Begin);
							Utilities.RewriteAndSanitizeWebReadyHtml(documentId, fileStream, fileStream2);
						}
					}
				}
			}
			catch (OwaBodyConversionFailedException ex)
			{
				throw new TranscodingFatalFaultException("Failed to rewrite the html File. Exception message: " + ex.ToString(), ex, this);
			}
			catch (IOException ex2)
			{
				throw new TranscodingFatalFaultException("Failed to rewrite the html File. Exception message: " + ex2.ToString(), ex2, this);
			}
			finally
			{
				lock (this.myLock)
				{
					this.UpdateCacheAfterTranscoding(isTranscodingSuccess, sessionId, documentId, text, rewrittenHtmlFileName);
				}
			}
		}

		// Token: 0x06001C64 RID: 7268 RVA: 0x000A35C0 File Offset: 0x000A17C0
		public void RemoveSession(string sessionId)
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("the cache system has been disposed already.");
			}
			if (string.IsNullOrEmpty(sessionId))
			{
				ExTraceGlobals.TranscodingTracer.TraceError((long)this.GetHashCode(), "the session ID is empty.");
				throw new ArgumentException("the session ID is empty.");
			}
			lock (this.myLock)
			{
				if (this.sessionDictionary.ContainsKey(sessionId))
				{
					this.sessionDictionary[sessionId].Expired = true;
				}
			}
		}

		// Token: 0x06001C65 RID: 7269 RVA: 0x000A3658 File Offset: 0x000A1858
		private int GetFolderSize(DirectoryInfo dirInfo)
		{
			long num = 0L;
			try
			{
				FileInfo[] files = dirInfo.GetFiles();
				foreach (FileInfo fileInfo in files)
				{
					num += fileInfo.Length;
				}
				DirectoryInfo[] directories = dirInfo.GetDirectories();
				foreach (DirectoryInfo dirInfo2 in directories)
				{
					num += (long)this.GetFolderSize(dirInfo2);
				}
			}
			catch (DirectoryNotFoundException)
			{
			}
			return (int)num;
		}

		// Token: 0x06001C66 RID: 7270 RVA: 0x000A36DC File Offset: 0x000A18DC
		private string GenerateNewHtmlFilePath(string sessionId, string documentId)
		{
			this.sessionDictionary[sessionId].Documents[documentId].HtmlFileNumber++;
			int htmlFileNumber = this.sessionDictionary[sessionId].Documents[documentId].HtmlFileNumber;
			return Cache.CombinePath(new string[]
			{
				this.processFolder,
				sessionId,
				documentId,
				string.Format("{0}{1}", htmlFileNumber, ".htm")
			});
		}

		// Token: 0x06001C67 RID: 7271 RVA: 0x000A3760 File Offset: 0x000A1960
		private string GenerateSourceDocumentName(string sessionId, string documentId)
		{
			int num = 1;
			if (this.sessionDictionary.ContainsKey(sessionId) && this.sessionDictionary[sessionId].Documents.ContainsKey(documentId))
			{
				this.sessionDictionary[sessionId].Documents[documentId].SourceFileNumber++;
				num = this.sessionDictionary[sessionId].Documents[documentId].SourceFileNumber;
			}
			string text = string.Format("{0}{1}", "sourcedoc", num);
			return Cache.CombinePath(new string[]
			{
				this.processFolder,
				sessionId,
				documentId,
				text
			});
		}

		// Token: 0x06001C68 RID: 7272 RVA: 0x000A3810 File Offset: 0x000A1A10
		private string GetAvailableSourceDocument(string sessionId, string documentId)
		{
			if (!this.sessionDictionary.ContainsKey(sessionId))
			{
				return null;
			}
			if (!this.sessionDictionary[sessionId].Documents.ContainsKey(documentId))
			{
				return null;
			}
			int sourceFileNumber = this.sessionDictionary[sessionId].Documents[documentId].SourceFileNumber;
			string text = string.Format("{0}{1}", "sourcedoc", sourceFileNumber);
			string text2 = Cache.CombinePath(new string[]
			{
				this.processFolder,
				sessionId,
				documentId,
				text
			});
			if (!this.sessionDictionary[sessionId].Documents[documentId].Files.ContainsKey(text))
			{
				return null;
			}
			if (this.sessionDictionary[sessionId].Documents[documentId].Files[text].Status == Cache.FileStatus.Deleting || this.sessionDictionary[sessionId].Documents[documentId].Files[text].Status == Cache.FileStatus.SavingFailed)
			{
				return null;
			}
			if (!File.Exists(text2))
			{
				return null;
			}
			return text2;
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x000A3928 File Offset: 0x000A1B28
		private static string CombinePath(params string[] paths)
		{
			string text = string.Empty;
			try
			{
				foreach (string path in paths)
				{
					text = Path.Combine(text, path);
				}
			}
			catch (ArgumentException ex)
			{
				ExTraceGlobals.TranscodingTracer.TraceError<ArgumentException>(0L, "the paths are illegal. Exception message:{0}", ex);
				throw new TranscodingFatalFaultException(ex.ToString(), ex, null);
			}
			return text;
		}

		// Token: 0x06001C6A RID: 7274 RVA: 0x000A3990 File Offset: 0x000A1B90
		private void DeleteOtherOWACacheFolders()
		{
			string[] directories = Directory.GetDirectories(this.option.RootPath);
			foreach (string text in directories)
			{
				if (!string.Equals(text, this.processFolder, StringComparison.OrdinalIgnoreCase))
				{
					int num = 0;
					int num2 = 0;
					DirectoryInfo dirInfo = new DirectoryInfo(text);
					try
					{
						num = this.GetFolderSize(dirInfo);
						Directory.Delete(text, true);
					}
					catch (IOException arg)
					{
						if (Directory.Exists(text))
						{
							num2 = this.GetFolderSize(dirInfo);
						}
						ExTraceGlobals.TranscodingTracer.TraceError<string, IOException>((long)this.GetHashCode(), "Failed to delete the folder--{0}. Exception message: {1}", text, arg);
					}
					finally
					{
						lock (this.myLock)
						{
							this.cacheSize += num2 - num;
						}
					}
				}
			}
		}

		// Token: 0x06001C6B RID: 7275 RVA: 0x000A3A8C File Offset: 0x000A1C8C
		private void UpdateCacheBeforeTranscoding(bool sourceDocumentExist, bool savingSuccess, string sessionId, string documentId, string sourceDocumentName, int sourceDocumentSize)
		{
			if (!this.sessionDictionary.ContainsKey(sessionId))
			{
				this.sessionDictionary.Add(sessionId, new Cache.Session());
			}
			if (!this.sessionDictionary[sessionId].Documents.ContainsKey(documentId))
			{
				this.sessionDictionary[sessionId].Documents.Add(documentId, new Cache.Document());
				this.sessionDictionary[sessionId].Documents[documentId].SourceFileNumber = 1;
			}
			if (!this.sessionDictionary[sessionId].Documents[documentId].Files.ContainsKey(sourceDocumentName))
			{
				Cache.FileStatus status = savingSuccess ? Cache.FileStatus.Transcoding : Cache.FileStatus.SavingFailed;
				this.sessionDictionary[sessionId].Documents[documentId].Files.Add(sourceDocumentName, new Cache.InputFileInformation(ExDateTime.UtcNow, status, sourceDocumentSize));
			}
			else
			{
				Cache.InputFileInformation inputFileInformation = this.sessionDictionary[sessionId].Documents[documentId].Files[sourceDocumentName] as Cache.InputFileInformation;
				inputFileInformation.LastAccessTime = ExDateTime.UtcNow;
				inputFileInformation.Status = Cache.FileStatus.Transcoding;
			}
			int num = 0;
			if (sourceDocumentExist)
			{
				num += this.option.OutputThreshold;
			}
			else
			{
				num += sourceDocumentSize;
				if (savingSuccess)
				{
					num += this.option.OutputThreshold;
				}
			}
			this.cacheSize += num;
		}

		// Token: 0x06001C6C RID: 7276 RVA: 0x000A3BE4 File Offset: 0x000A1DE4
		private void UpdateCacheAfterTranscoding(bool transcodingSuccess, string sessionId, string documentId, string htmlFileName, string rewrittenHtmlFileName)
		{
			int sourceFileNumber = this.sessionDictionary[sessionId].Documents[documentId].SourceFileNumber;
			string key = string.Format("{0}{1}", "sourcedoc", sourceFileNumber);
			if (this.sessionDictionary[sessionId].Documents[documentId].Files.ContainsKey(key))
			{
				this.sessionDictionary[sessionId].Documents[documentId].Files[key].Status = Cache.FileStatus.Ready;
			}
			string text = Cache.CombinePath(new string[]
			{
				this.processFolder,
				sessionId,
				documentId,
				htmlFileName
			});
			string text2 = Path.ChangeExtension(text, "css");
			ExDateTime utcNow = ExDateTime.UtcNow;
			long num = 0L;
			if (File.Exists(text2))
			{
				FileInfo fileInfo = new FileInfo(text2);
				num += fileInfo.Length;
				string fileName = Path.GetFileName(text2);
				this.sessionDictionary[sessionId].Documents[documentId].Files.Add(fileName, new Cache.OutputFileInformation(utcNow, Cache.FileStatus.Ready, (int)fileInfo.Length));
			}
			string searchPattern = Path.GetFileNameWithoutExtension(text) + "_*";
			string[] files = Directory.GetFiles(Path.GetDirectoryName(text), searchPattern);
			foreach (string text3 in files)
			{
				FileInfo fileInfo = new FileInfo(text3);
				num += fileInfo.Length;
				string fileName2 = Path.GetFileName(text3);
				this.sessionDictionary[sessionId].Documents[documentId].Files.Add(fileName2, new Cache.OutputFileInformation(utcNow, Cache.FileStatus.Ready, (int)fileInfo.Length));
			}
			this.cacheSize += (int)num - this.option.OutputThreshold;
			if (transcodingSuccess)
			{
				string text4 = Cache.CombinePath(new string[]
				{
					this.processFolder,
					sessionId,
					documentId,
					rewrittenHtmlFileName
				});
				if (File.Exists(text4))
				{
					FileInfo fileInfo = new FileInfo(text4);
					this.cacheSize += (int)fileInfo.Length;
					this.sessionDictionary[sessionId].Documents[documentId].Files.Add(rewrittenHtmlFileName, new Cache.OutputFileInformation(utcNow, Cache.FileStatus.Ready, (int)fileInfo.Length));
				}
				try
				{
					File.Delete(text);
					return;
				}
				catch (IOException)
				{
					if (File.Exists(text))
					{
						FileInfo fileInfo = new FileInfo(text);
						this.cacheSize += (int)fileInfo.Length;
						this.sessionDictionary[sessionId].Documents[documentId].Files.Add(htmlFileName, new Cache.OutputFileInformation(utcNow, Cache.FileStatus.Ready, (int)fileInfo.Length));
					}
					return;
				}
			}
			if (File.Exists(text))
			{
				FileInfo fileInfo = new FileInfo(text);
				this.cacheSize += (int)fileInfo.Length;
				this.sessionDictionary[sessionId].Documents[documentId].Files.Add(htmlFileName, new Cache.OutputFileInformation(utcNow, Cache.FileStatus.Ready, (int)fileInfo.Length));
			}
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x000A3F10 File Offset: 0x000A2110
		private void DenyLocalServiceAccess(string path)
		{
			try
			{
				DirectorySecurity accessControl = Directory.GetAccessControl(path);
				accessControl.RemoveAccessRule(this.localServiceReadRule);
				accessControl.RemoveAccessRule(this.localServiceWriteRule);
				accessControl.RemoveAccessRule(this.localServiceCreateFileRule);
				accessControl.RemoveAccessRule(this.localServiceDeleteFileRule);
				Directory.SetAccessControl(path, accessControl);
			}
			catch (DirectoryNotFoundException arg)
			{
				ExTraceGlobals.TranscodingTracer.TraceError<string, DirectoryNotFoundException>((long)this.GetHashCode(), "Failed to set the access permission of path--{0}. Exception message: {1}", path, arg);
			}
			catch (IOException arg2)
			{
				ExTraceGlobals.TranscodingTracer.TraceError<string, IOException>((long)this.GetHashCode(), "Failed to set the access permission of path--{0}. Exception message: {1}", path, arg2);
			}
			catch (PlatformNotSupportedException arg3)
			{
				ExTraceGlobals.TranscodingTracer.TraceError<string, PlatformNotSupportedException>((long)this.GetHashCode(), "Failed to set the access permission of path--{0}. Exception message: {1}", path, arg3);
			}
			catch (UnauthorizedAccessException arg4)
			{
				ExTraceGlobals.TranscodingTracer.TraceError<string, UnauthorizedAccessException>((long)this.GetHashCode(), "Fail to set the access permission of path--{0}. Exception message: {1}", path, arg4);
			}
			catch (InvalidOperationException arg5)
			{
				ExTraceGlobals.TranscodingTracer.TraceError<string, InvalidOperationException>((long)this.GetHashCode(), "Failed to set the access permission of path--{0}. Exception message: {1}", path, arg5);
			}
		}

		// Token: 0x06001C6E RID: 7278 RVA: 0x000A4030 File Offset: 0x000A2230
		private void CleanupThreadProc()
		{
			WaitHandle[] waitHandles = new WaitHandle[]
			{
				this.abortCleanupEvent,
				this.activeCleanupEvent
			};
			while (WaitHandle.WaitAny(waitHandles) != 0)
			{
				this.cleanupThreadIdle = false;
				int num = (0 < this.expiredTime) ? this.expiredTime : 600;
				try
				{
					this.DeleteOtherOWACacheFolders();
					List<string> list = new List<string>();
					lock (this.myLock)
					{
						foreach (KeyValuePair<string, Cache.Session> keyValuePair in this.sessionDictionary)
						{
							if (keyValuePair.Value.Expired)
							{
								list.Add(keyValuePair.Key);
							}
						}
					}
					for (int i = list.Count - 1; i >= 0; i--)
					{
						int num2 = 0;
						int num3 = 0;
						string text = Path.Combine(this.processFolder, list[i]);
						DirectoryInfo dirInfo = new DirectoryInfo(text);
						try
						{
							num2 = this.GetFolderSize(dirInfo);
							Directory.Delete(text, true);
						}
						catch (IOException arg)
						{
							if (Directory.Exists(text))
							{
								list.Remove(list[i]);
								try
								{
									num3 = this.GetFolderSize(dirInfo);
								}
								catch (UnauthorizedAccessException arg2)
								{
									ExTraceGlobals.TranscodingTracer.TraceError<string, UnauthorizedAccessException>((long)this.GetHashCode(), "Failed to get the size of the folder--{0}. Exception message: {1}", text, arg2);
									num3 = num2;
								}
							}
							ExTraceGlobals.TranscodingTracer.TraceError<string, IOException>((long)this.GetHashCode(), "Failed to delete the folder--{0}. Exception message: {1}", text, arg);
						}
						finally
						{
							lock (this.myLock)
							{
								this.cacheSize += num3 - num2;
							}
						}
					}
					lock (this.myLock)
					{
						foreach (string key in list)
						{
							this.sessionDictionary.Remove(key);
						}
					}
					List<Cache.ExpiredFile> list2 = new List<Cache.ExpiredFile>();
					lock (this.myLock)
					{
						foreach (KeyValuePair<string, Cache.Session> keyValuePair2 in this.sessionDictionary)
						{
							foreach (KeyValuePair<string, Cache.Document> keyValuePair3 in keyValuePair2.Value.Documents)
							{
								foreach (KeyValuePair<string, Cache.FileInformation> keyValuePair4 in keyValuePair3.Value.Files)
								{
									TimeSpan timeSpan = TimeSpan.Zero;
									if (keyValuePair4.Value is Cache.InputFileInformation)
									{
										Cache.InputFileInformation inputFileInformation = keyValuePair4.Value as Cache.InputFileInformation;
										timeSpan = ExDateTime.UtcNow - inputFileInformation.LastAccessTime;
									}
									else if (keyValuePair4.Value is Cache.OutputFileInformation)
									{
										Cache.OutputFileInformation outputFileInformation = keyValuePair4.Value as Cache.OutputFileInformation;
										timeSpan = ExDateTime.UtcNow - outputFileInformation.CreateTime;
									}
									if (((double)num < timeSpan.TotalSeconds && keyValuePair4.Value.Status == Cache.FileStatus.Ready) || keyValuePair4.Value.Status == Cache.FileStatus.Deleting || keyValuePair4.Value.Status == Cache.FileStatus.SavingFailed)
									{
										Cache.ExpiredFile item = new Cache.ExpiredFile(keyValuePair2.Key, keyValuePair3.Key, keyValuePair4.Key, keyValuePair4.Value.Size);
										list2.Add(item);
										keyValuePair4.Value.Status = Cache.FileStatus.Deleting;
									}
								}
							}
						}
					}
					for (int j = list2.Count - 1; j >= 0; j--)
					{
						string text2 = Cache.CombinePath(new string[]
						{
							this.processFolder,
							list2[j].SessionId,
							list2[j].DocumentId,
							list2[j].FileName
						});
						try
						{
							File.Delete(text2);
							lock (this.myLock)
							{
								this.cacheSize -= list2[j].Size;
							}
						}
						catch (IOException arg3)
						{
							if (File.Exists(text2))
							{
								list2.Remove(list2[j]);
							}
							ExTraceGlobals.TranscodingTracer.TraceError<string, IOException>((long)this.GetHashCode(), "Failed to delete the file--{0}. Exception message: {1}", text2, arg3);
						}
					}
					lock (this.myLock)
					{
						foreach (Cache.ExpiredFile expiredFile in list2)
						{
							this.sessionDictionary[expiredFile.SessionId].Documents[expiredFile.DocumentId].Files.Remove(expiredFile.FileName);
						}
					}
				}
				catch (IOException arg4)
				{
					ExTraceGlobals.TranscodingTracer.TraceError<IOException>((long)this.GetHashCode(), "{0}", arg4);
				}
				finally
				{
					lock (this.myLock)
					{
						this.activeCleanupEvent.Reset();
					}
					this.cleanupThreadIdle = true;
				}
			}
		}

		// Token: 0x06001C6F RID: 7279 RVA: 0x000A478C File Offset: 0x000A298C
		private void SaveSourceStreamIntoFile(Stream documentStream, string sourceDocumentPath, out int sourceDocumentSize)
		{
			sourceDocumentSize = 0;
			byte[] buffer = new byte[10240];
			using (FileStream fileStream = new FileStream(sourceDocumentPath, FileMode.CreateNew))
			{
				int num;
				do
				{
					num = documentStream.Read(buffer, 0, 10240);
					fileStream.Write(buffer, 0, num);
					sourceDocumentSize += num;
				}
				while (0 < num && this.option.InputThreshold >= sourceDocumentSize);
			}
		}

		// Token: 0x06001C70 RID: 7280 RVA: 0x000A4800 File Offset: 0x000A2A00
		private static string GenerateGuidString()
		{
			return Guid.NewGuid().ToString().Trim(Cache.trimChars);
		}

		// Token: 0x06001C71 RID: 7281 RVA: 0x000A482C File Offset: 0x000A2A2C
		protected override void InternalDispose(bool isDisposing)
		{
			if (isDisposing)
			{
				this.abortCleanupEvent.Set();
				if (!this.cleanupThread.Join(30000))
				{
					this.cleanupThread.Abort();
				}
				if (this.activeCleanupEvent != null)
				{
					this.activeCleanupEvent.Close();
				}
				if (this.abortCleanupEvent != null)
				{
					this.abortCleanupEvent.Close();
				}
				if (Directory.Exists(this.option.RootPath))
				{
					try
					{
						Directory.Delete(this.option.RootPath, true);
					}
					catch (IOException arg)
					{
						ExTraceGlobals.TranscodingTracer.TraceError<IOException>((long)this.GetHashCode(), "Failed to delete the cache folders. Exception message: {0}", arg);
						if (Directory.Exists(this.processFolder))
						{
							this.DenyLocalServiceAccess(this.processFolder);
						}
					}
				}
			}
		}

		// Token: 0x06001C72 RID: 7282 RVA: 0x000A48F8 File Offset: 0x000A2AF8
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<Cache>(this);
		}

		// Token: 0x06001C73 RID: 7283 RVA: 0x000A4900 File Offset: 0x000A2B00
		private void TransmitFile(HttpResponse response, string filePath)
		{
			try
			{
				using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
				{
					int num = (int)fileStream.Length;
					int num2 = 65536;
					int num3 = (num < num2) ? num : num2;
					byte[] buffer = new byte[num3];
					int num4;
					do
					{
						num4 = fileStream.Read(buffer, 0, num3);
						if (num4 > 0)
						{
							response.OutputStream.Write(buffer, 0, num4);
							response.OutputStream.Flush();
						}
					}
					while (num4 > 0);
				}
			}
			catch (IOException ex)
			{
				ExTraceGlobals.TranscodingTracer.TraceError<string, IOException>((long)this.GetHashCode(), "File '{0}' can not be transmitted. Error Message: '{1}'", filePath, ex);
				throw new TranscodingFatalFaultException(string.Format("File '{0}' can not be transmitted. Error Message: '{1}'", filePath, ex.ToString()), ex, this);
			}
			catch (NotSupportedException ex2)
			{
				ExTraceGlobals.TranscodingTracer.TraceError<string, NotSupportedException>((long)this.GetHashCode(), "File '{0}' can not be transmitted. Error Message: '{1}'", filePath, ex2);
				throw new TranscodingFatalFaultException(string.Format("File '{0}' can not be transmitted. Error Message: '{1}'", filePath, ex2.ToString()), ex2, this);
			}
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06001C74 RID: 7284 RVA: 0x000A4A0C File Offset: 0x000A2C0C
		public int CacheSize
		{
			get
			{
				return this.cacheSize;
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06001C75 RID: 7285 RVA: 0x000A4A14 File Offset: 0x000A2C14
		// (set) Token: 0x06001C76 RID: 7286 RVA: 0x000A4A1C File Offset: 0x000A2C1C
		public int ExpiredTime
		{
			get
			{
				return this.expiredTime;
			}
			set
			{
				this.expiredTime = value;
			}
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06001C77 RID: 7287 RVA: 0x000A4A25 File Offset: 0x000A2C25
		public bool CleanupThreadIdle
		{
			get
			{
				return this.cleanupThreadIdle;
			}
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06001C78 RID: 7288 RVA: 0x000A4A2D File Offset: 0x000A2C2D
		public static int XCRootPathMaxLength
		{
			get
			{
				return 100;
			}
		}

		// Token: 0x040014EE RID: 5358
		private const string SourceDocumentName = "sourcedoc";

		// Token: 0x040014EF RID: 5359
		private const string HtmlExtension = ".htm";

		// Token: 0x040014F0 RID: 5360
		private const int DefaultBufferSize = 10240;

		// Token: 0x040014F1 RID: 5361
		private const int DefaultExpiredTime = 600;

		// Token: 0x040014F2 RID: 5362
		private const string CacheFolderName = "XCCache";

		// Token: 0x040014F3 RID: 5363
		private const int DefaultRecycleCount = 100;

		// Token: 0x040014F4 RID: 5364
		private const double ActivateCleanupThreadBar = 0.8;

		// Token: 0x040014F5 RID: 5365
		private const int WaitingThreadTerminationTime = 30000;

		// Token: 0x040014F6 RID: 5366
		private const int RootPathMaxLength = 100;

		// Token: 0x040014F7 RID: 5367
		private Cache.CacheOption option;

		// Token: 0x040014F8 RID: 5368
		private Dictionary<string, Cache.Session> sessionDictionary = new Dictionary<string, Cache.Session>();

		// Token: 0x040014F9 RID: 5369
		private string processFolder;

		// Token: 0x040014FA RID: 5370
		private EventWaitHandle activeCleanupEvent = new EventWaitHandle(false, EventResetMode.ManualReset);

		// Token: 0x040014FB RID: 5371
		private EventWaitHandle abortCleanupEvent = new EventWaitHandle(false, EventResetMode.ManualReset);

		// Token: 0x040014FC RID: 5372
		private Thread cleanupThread;

		// Token: 0x040014FD RID: 5373
		private int cacheSize;

		// Token: 0x040014FE RID: 5374
		private int expiredTime;

		// Token: 0x040014FF RID: 5375
		private int transcodingTaskCount;

		// Token: 0x04001500 RID: 5376
		private bool cleanupThreadIdle = true;

		// Token: 0x04001501 RID: 5377
		private object myLock = new object();

		// Token: 0x04001502 RID: 5378
		private FileSystemAccessRule localServiceReadRule;

		// Token: 0x04001503 RID: 5379
		private FileSystemAccessRule localServiceWriteRule;

		// Token: 0x04001504 RID: 5380
		private FileSystemAccessRule localServiceCreateFileRule;

		// Token: 0x04001505 RID: 5381
		private FileSystemAccessRule localServiceDeleteFileRule;

		// Token: 0x04001506 RID: 5382
		private DirectorySecurity directorySercurity;

		// Token: 0x04001507 RID: 5383
		private static char[] trimChars = new char[]
		{
			'-',
			'{',
			'}'
		};

		// Token: 0x020002E5 RID: 741
		public class CacheOption
		{
			// Token: 0x17000785 RID: 1925
			// (get) Token: 0x06001C7A RID: 7290 RVA: 0x000A4A56 File Offset: 0x000A2C56
			// (set) Token: 0x06001C7B RID: 7291 RVA: 0x000A4A5E File Offset: 0x000A2C5E
			public string RootPath
			{
				get
				{
					return this.rootPath;
				}
				set
				{
					if (string.IsNullOrEmpty(value))
					{
						throw new ArgumentException("rootPath can not be empty or null");
					}
					this.rootPath = value;
				}
			}

			// Token: 0x17000786 RID: 1926
			// (get) Token: 0x06001C7C RID: 7292 RVA: 0x000A4A7A File Offset: 0x000A2C7A
			public int TotalSizeQuota
			{
				get
				{
					return this.totalSizeQuota;
				}
			}

			// Token: 0x17000787 RID: 1927
			// (get) Token: 0x06001C7D RID: 7293 RVA: 0x000A4A82 File Offset: 0x000A2C82
			public int InputThreshold
			{
				get
				{
					return this.inputThreshold;
				}
			}

			// Token: 0x17000788 RID: 1928
			// (get) Token: 0x06001C7E RID: 7294 RVA: 0x000A4A8A File Offset: 0x000A2C8A
			public int OutputThreshold
			{
				get
				{
					return this.outputThreshold;
				}
			}

			// Token: 0x06001C7F RID: 7295 RVA: 0x000A4A94 File Offset: 0x000A2C94
			public CacheOption(string rootPath, int totalSizeQuota, int inputThreshold, int outputThreshold)
			{
				if (string.IsNullOrEmpty(rootPath))
				{
					ExTraceGlobals.TranscodingTracer.TraceError((long)this.GetHashCode(), "ctor: root path is empty.");
					throw new ArgumentException("ctor: root path is empty.");
				}
				this.rootPath = rootPath;
				this.totalSizeQuota = totalSizeQuota;
				this.inputThreshold = inputThreshold;
				this.outputThreshold = outputThreshold;
			}

			// Token: 0x04001508 RID: 5384
			private string rootPath;

			// Token: 0x04001509 RID: 5385
			private int totalSizeQuota;

			// Token: 0x0400150A RID: 5386
			private int inputThreshold;

			// Token: 0x0400150B RID: 5387
			private int outputThreshold;
		}

		// Token: 0x020002E6 RID: 742
		private enum FileStatus
		{
			// Token: 0x0400150D RID: 5389
			Ready,
			// Token: 0x0400150E RID: 5390
			Transcoding,
			// Token: 0x0400150F RID: 5391
			SavingFailed,
			// Token: 0x04001510 RID: 5392
			Serving,
			// Token: 0x04001511 RID: 5393
			Deleting
		}

		// Token: 0x020002E7 RID: 743
		private abstract class FileInformation
		{
			// Token: 0x06001C80 RID: 7296 RVA: 0x000A4AED File Offset: 0x000A2CED
			public FileInformation(Cache.FileStatus status, int size)
			{
				this.Status = status;
				this.Size = size;
			}

			// Token: 0x04001512 RID: 5394
			public Cache.FileStatus Status;

			// Token: 0x04001513 RID: 5395
			public int Size;
		}

		// Token: 0x020002E8 RID: 744
		private sealed class InputFileInformation : Cache.FileInformation
		{
			// Token: 0x06001C81 RID: 7297 RVA: 0x000A4B03 File Offset: 0x000A2D03
			public InputFileInformation(ExDateTime lastAccessTime, Cache.FileStatus status, int size) : base(status, size)
			{
				this.LastAccessTime = lastAccessTime;
			}

			// Token: 0x04001514 RID: 5396
			public ExDateTime LastAccessTime;
		}

		// Token: 0x020002E9 RID: 745
		private sealed class OutputFileInformation : Cache.FileInformation
		{
			// Token: 0x06001C82 RID: 7298 RVA: 0x000A4B14 File Offset: 0x000A2D14
			public OutputFileInformation(ExDateTime createTime, Cache.FileStatus status, int size) : base(status, size)
			{
				this.CreateTime = createTime;
			}

			// Token: 0x04001515 RID: 5397
			public ExDateTime CreateTime;
		}

		// Token: 0x020002EA RID: 746
		private sealed class Document
		{
			// Token: 0x04001516 RID: 5398
			public int HtmlFileNumber;

			// Token: 0x04001517 RID: 5399
			public int SourceFileNumber;

			// Token: 0x04001518 RID: 5400
			public Dictionary<string, Cache.FileInformation> Files = new Dictionary<string, Cache.FileInformation>();
		}

		// Token: 0x020002EB RID: 747
		private sealed class Session
		{
			// Token: 0x04001519 RID: 5401
			public bool Expired;

			// Token: 0x0400151A RID: 5402
			public Dictionary<string, Cache.Document> Documents = new Dictionary<string, Cache.Document>();
		}

		// Token: 0x020002EC RID: 748
		private sealed class ExpiredFile
		{
			// Token: 0x06001C85 RID: 7301 RVA: 0x000A4B4B File Offset: 0x000A2D4B
			public ExpiredFile(string sessionId, string documentId, string fileName, int size)
			{
				this.SessionId = sessionId;
				this.DocumentId = documentId;
				this.FileName = fileName;
				this.Size = size;
			}

			// Token: 0x0400151B RID: 5403
			public string SessionId;

			// Token: 0x0400151C RID: 5404
			public string DocumentId;

			// Token: 0x0400151D RID: 5405
			public string FileName;

			// Token: 0x0400151E RID: 5406
			public int Size;
		}
	}
}
