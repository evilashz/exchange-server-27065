using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore.Exceptions;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200026D RID: 621
	internal class LargeGrammarFetcher
	{
		// Token: 0x06001268 RID: 4712 RVA: 0x00051AA8 File Offset: 0x0004FCA8
		internal LargeGrammarFetcher(GrammarIdentifier grammarId)
		{
			ValidateArgument.NotNull(grammarId, "GrammarIdentifier");
			LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("LargeGrammarFetcher: C'tor, grammarId = {0}", new object[]
			{
				grammarId
			});
			this.grammarId = grammarId;
			LargeGrammarFetcher.LocalGrammarCache.Instance.TryPrepareGrammar(this.grammarId, out this.grammarFilePath, out this.grammarAvailableEvent);
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06001269 RID: 4713 RVA: 0x00051B00 File Offset: 0x0004FD00
		internal GrammarIdentifier GrammarId
		{
			get
			{
				return this.grammarId;
			}
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x00051B08 File Offset: 0x0004FD08
		internal void WaitAsync(LargeGrammarFetcher.GrammarDownloadedCallBack callback, TimeSpan timeout, object userstate)
		{
			ValidateArgument.NotNull(callback, "GrammarDownloadedCallBack");
			ValidateArgument.NotNull(timeout, "Timeout");
			LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("LargeGrammarFetcher:FetchAsync, Timeout = {0}", new object[]
			{
				timeout
			});
			this.CheckAndThrowIfAlreadyFetching();
			this.userCallback = callback;
			this.userTimeout = timeout;
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.ExecuteUserCallback), userstate);
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x00051B74 File Offset: 0x0004FD74
		internal LargeGrammarFetcher.FetchResult Wait(TimeSpan timeout)
		{
			ValidateArgument.NotNull(timeout, "Timeout");
			LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("LargeGrammarFetcher:FetchSync, Timeout = {0}", new object[]
			{
				timeout
			});
			this.CheckAndThrowIfAlreadyFetching();
			LargeGrammarFetcher.FetchResult result = null;
			try
			{
				result = this.GetFetchResult(timeout);
			}
			finally
			{
				Interlocked.Exchange(ref this.grammarFetchInProgress, 0);
			}
			return result;
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x00051BE0 File Offset: 0x0004FDE0
		private void CheckAndThrowIfAlreadyFetching()
		{
			if (Interlocked.CompareExchange(ref this.grammarFetchInProgress, 1, 0) != 0)
			{
				throw new InvalidOperationException("A Grammar Fetch Operation is already in progress");
			}
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x00051BFC File Offset: 0x0004FDFC
		private void ExecuteUserCallback(object state)
		{
			try
			{
				this.userCallback(state, this.GetFetchResult(this.userTimeout));
			}
			finally
			{
				Interlocked.Exchange(ref this.grammarFetchInProgress, 0);
			}
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x00051C44 File Offset: 0x0004FE44
		private LargeGrammarFetcher.FetchResult GetFetchResult(TimeSpan timeout)
		{
			LargeGrammarFetcher.FetchResult fetchResult = null;
			if (this.grammarFilePath != null)
			{
				LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("LargeGrammarFetcher:GetFetchResult, FileOnDisk path = {0}", new object[]
				{
					this.grammarFilePath
				});
				fetchResult = new LargeGrammarFetcher.FetchResult(this.grammarFilePath, LargeGrammarFetcher.FetchStatus.Success);
			}
			else if (this.grammarAvailableEvent.SafeWaitHandle.IsClosed)
			{
				LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("LargeGrammarFetcher:GetFetchResult, WaitHandle closed", new object[0]);
				fetchResult = this.ReFetchGrammarFilePath();
			}
			else
			{
				try
				{
					if (this.grammarAvailableEvent.WaitOne(timeout))
					{
						LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("LargeGrammarFetcher:GetFetchResult, Wait finished early", new object[0]);
						fetchResult = this.ReFetchGrammarFilePath();
					}
					else
					{
						LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("LargeGrammarFetcher:GetFetchResult, Wait timed out", new object[0]);
						UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMGrammarFetcherError, null, new object[]
						{
							this.grammarId,
							CommonUtil.ToEventLogString(Strings.SpeechGrammarFetchTimeoutException(this.grammarId.ToString()))
						});
						fetchResult = new LargeGrammarFetcher.FetchResult(this.grammarFilePath, LargeGrammarFetcher.FetchStatus.Timeout);
					}
				}
				catch (ObjectDisposedException ex)
				{
					LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("LargeGrammarFetcher:GetFetchResult, got expected exception ={0}", new object[]
					{
						ex
					});
					fetchResult = this.ReFetchGrammarFilePath();
				}
			}
			if (fetchResult.Status == LargeGrammarFetcher.FetchStatus.Success)
			{
				UMEventNotificationHelper.PublishUMSuccessEventNotificationItem(ExchangeComponent.UMProtocol, UMNotificationEvent.UMGrammarUsage.ToString());
			}
			else
			{
				UMEventNotificationHelper.PublishUMFailureEventNotificationItem(ExchangeComponent.UMProtocol, UMNotificationEvent.UMGrammarUsage.ToString());
			}
			return fetchResult;
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x00051DA4 File Offset: 0x0004FFA4
		private LargeGrammarFetcher.FetchResult ReFetchGrammarFilePath()
		{
			LargeGrammarFetcher.LocalGrammarCache.Instance.TryCheckIfFileExistsInCache(this.grammarId, out this.grammarFilePath);
			LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("LargeGrammarFetcher:ReFetchGrammarFilePath, grammarFilePath={0}", new object[]
			{
				string.IsNullOrEmpty(this.grammarFilePath) ? "null" : this.grammarFilePath
			});
			return new LargeGrammarFetcher.FetchResult(this.grammarFilePath, string.IsNullOrEmpty(this.grammarFilePath) ? LargeGrammarFetcher.FetchStatus.Error : LargeGrammarFetcher.FetchStatus.Success);
		}

		// Token: 0x04000C15 RID: 3093
		private GrammarIdentifier grammarId;

		// Token: 0x04000C16 RID: 3094
		private int grammarFetchInProgress;

		// Token: 0x04000C17 RID: 3095
		private LargeGrammarFetcher.GrammarDownloadedCallBack userCallback;

		// Token: 0x04000C18 RID: 3096
		private TimeSpan userTimeout;

		// Token: 0x04000C19 RID: 3097
		private string grammarFilePath;

		// Token: 0x04000C1A RID: 3098
		private ManualResetEvent grammarAvailableEvent;

		// Token: 0x0200026E RID: 622
		internal enum FetchStatus
		{
			// Token: 0x04000C1C RID: 3100
			Success,
			// Token: 0x04000C1D RID: 3101
			Timeout,
			// Token: 0x04000C1E RID: 3102
			Error
		}

		// Token: 0x0200026F RID: 623
		// (Invoke) Token: 0x06001271 RID: 4721
		internal delegate void GrammarDownloadedCallBack(object userState, LargeGrammarFetcher.FetchResult result);

		// Token: 0x02000270 RID: 624
		internal class FetchResult
		{
			// Token: 0x17000480 RID: 1152
			// (get) Token: 0x06001274 RID: 4724 RVA: 0x00051E13 File Offset: 0x00050013
			// (set) Token: 0x06001275 RID: 4725 RVA: 0x00051E1B File Offset: 0x0005001B
			internal string FilePath { get; private set; }

			// Token: 0x17000481 RID: 1153
			// (get) Token: 0x06001276 RID: 4726 RVA: 0x00051E24 File Offset: 0x00050024
			// (set) Token: 0x06001277 RID: 4727 RVA: 0x00051E2C File Offset: 0x0005002C
			internal LargeGrammarFetcher.FetchStatus Status { get; private set; }

			// Token: 0x06001278 RID: 4728 RVA: 0x00051E35 File Offset: 0x00050035
			internal FetchResult(string filePath, LargeGrammarFetcher.FetchStatus status)
			{
				this.FilePath = filePath;
				this.Status = status;
			}
		}

		// Token: 0x02000271 RID: 625
		private class LocalGrammarCache
		{
			// Token: 0x06001279 RID: 4729 RVA: 0x00051E4B File Offset: 0x0005004B
			private LocalGrammarCache()
			{
				this.grammarsBeingDownloaded = new Dictionary<GrammarIdentifier, ManualResetEvent>();
				this.grammarsBeingUpdated = new HashSet<GrammarIdentifier>();
				this.grammarsBeingDownloadedLock = new object();
				this.grammarsBeingUpdatedLock = new object();
			}

			// Token: 0x17000482 RID: 1154
			// (get) Token: 0x0600127A RID: 4730 RVA: 0x00051E7F File Offset: 0x0005007F
			internal static LargeGrammarFetcher.LocalGrammarCache Instance
			{
				get
				{
					return LargeGrammarFetcher.LocalGrammarCache.grammarCache;
				}
			}

			// Token: 0x0600127B RID: 4731 RVA: 0x00051E88 File Offset: 0x00050088
			public bool TryPrepareGrammar(GrammarIdentifier grammarId, out string path, out ManualResetEvent grammarWaitHandle)
			{
				ValidateArgument.NotNull(grammarId, "grammarId");
				LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("LocalGrammarCache.TryPrepareGrammar, Grammar='{0}'", new object[]
				{
					grammarId
				});
				path = null;
				grammarWaitHandle = null;
				if (!this.TryCheckIfFileExistsInCache(grammarId, out path))
				{
					grammarWaitHandle = this.FetchGrammarAsync(grammarId);
				}
				else
				{
					this.CheckForUpdatesAsync(grammarId);
				}
				LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("LocalGrammarCache.TryPrepareGrammar, Grammar='{0}', Path='{1}'", new object[]
				{
					grammarId,
					path ?? "<null>"
				});
				return path != null;
			}

			// Token: 0x0600127C RID: 4732 RVA: 0x00051F5C File Offset: 0x0005015C
			public bool TryCheckIfFileExistsInCache(GrammarIdentifier grammarId, out string path)
			{
				ValidateArgument.NotNull(grammarId, "grammarId");
				LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("LocalGrammarCache.TryCheckIfFileExistsInCache, Grammar='{0}'", new object[]
				{
					grammarId
				});
				path = null;
				string tmp = null;
				LargeGrammarFetcher.LocalGrammarCache.DoGrammarOperation(grammarId, delegate
				{
					tmp = LargeGrammarFetcher.GrammarFetcherUtils.GetLatestGrammarFilePathInCache(grammarId);
					LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("LocalGrammarCache.TryCheckIfFileExistsInCache, Grammar='{0}', Path='{1}'", new object[]
					{
						grammarId,
						tmp ?? "<null>"
					});
				}, delegate
				{
				});
				path = tmp;
				return path != null;
			}

			// Token: 0x0600127D RID: 4733 RVA: 0x00051FEF File Offset: 0x000501EF
			private static bool IsExpectedException(Exception e)
			{
				return e is IOException || e is UnauthorizedAccessException || e is GrammarFileNotFoundException || e is LocalizedException;
			}

			// Token: 0x0600127E RID: 4734 RVA: 0x00052014 File Offset: 0x00050214
			private static void DoGrammarOperation(GrammarIdentifier grammarId, Action operation, Action cleanup)
			{
				try
				{
					operation();
				}
				catch (Exception ex)
				{
					LargeGrammarFetcher.GrammarFetcherUtils.ErrorTrace("LocalGrammarCache.DoGrammarOperation, Grammar='{0}', Error:'{1}'", new object[]
					{
						grammarId,
						ex
					});
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMGrammarFetcherError, null, new object[]
					{
						grammarId,
						CommonUtil.ToEventLogString(ex)
					});
					if (!LargeGrammarFetcher.LocalGrammarCache.IsExpectedException(ex))
					{
						ExceptionHandling.SendWatsonWithExtraData(ex, false);
						throw;
					}
				}
				finally
				{
					cleanup();
				}
			}

			// Token: 0x0600127F RID: 4735 RVA: 0x000520A4 File Offset: 0x000502A4
			private ManualResetEvent FetchGrammarAsync(GrammarIdentifier grammarId)
			{
				LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("LocalGrammarCache.FetchGrammarAsync, grammarId='{0}'", new object[]
				{
					grammarId
				});
				ManualResetEvent manualResetEvent = null;
				lock (this.grammarsBeingDownloadedLock)
				{
					if (!this.grammarsBeingDownloaded.ContainsKey(grammarId))
					{
						LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("LocalGrammarCache.FetchGrammarAsync, Adding grammarId='{0}' to grammarsBeingDownloaded", new object[]
						{
							grammarId
						});
						manualResetEvent = new ManualResetEvent(false);
						this.grammarsBeingDownloaded.Add(grammarId, manualResetEvent);
						ThreadPool.QueueUserWorkItem(new WaitCallback(this.FetchGrammar), grammarId);
					}
					else
					{
						LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("LocalGrammarCache.FetchGrammarAsync, grammarId='{0}' already in grammarsBeingDownloaded", new object[]
						{
							grammarId
						});
						manualResetEvent = this.grammarsBeingDownloaded[grammarId];
					}
				}
				return manualResetEvent;
			}

			// Token: 0x06001280 RID: 4736 RVA: 0x000522CC File Offset: 0x000504CC
			private void FetchGrammar(object state)
			{
				GrammarIdentifier grammarId = (GrammarIdentifier)state;
				LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("LocalGrammarCache.FetchGrammar grammarId='{0}'", new object[]
				{
					grammarId
				});
				LargeGrammarFetcher.LocalGrammarCache.DoGrammarOperation(grammarId, delegate
				{
					string latestGrammarFilePathInShare = LargeGrammarFetcher.GrammarFetcherUtils.GetLatestGrammarFilePathInShare(grammarId);
					if (latestGrammarFilePathInShare != null)
					{
						LargeGrammarFetcher.GrammarFetcherUtils.DownloadGrammarFromShare(grammarId, latestGrammarFilePathInShare, DateTime.MinValue);
						LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("LocalGrammarCache.FetchGrammar, Downloaded grammarId='{0}' from share path='{1}'", new object[]
						{
							grammarId,
							latestGrammarFilePathInShare
						});
					}
					else
					{
						LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("LocalGrammarCache.FetchGrammar, Try to download from mailbox, grammarId='{0}'", new object[]
						{
							grammarId
						});
						bool flag = LargeGrammarFetcher.GrammarFetcherUtils.DownloadGrammarFromMailbox(grammarId, DateTime.MinValue);
						LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("LocalGrammarCache.FetchGrammar, Download from mailbox, grammarId='{0}', grammarDownloaded='{1}'", new object[]
						{
							grammarId,
							flag
						});
						if (!flag)
						{
							throw new GrammarFileNotFoundException(grammarId.ToString());
						}
					}
					LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("LocalGrammarCache.FetchGrammar, Downloaded grammarId='{0}'", new object[]
					{
						grammarId
					});
				}, delegate
				{
					lock (this.grammarsBeingDownloadedLock)
					{
						ManualResetEvent manualResetEvent = this.grammarsBeingDownloaded[grammarId];
						manualResetEvent.Set();
						manualResetEvent.Close();
						this.grammarsBeingDownloaded.Remove(grammarId);
					}
				});
			}

			// Token: 0x06001281 RID: 4737 RVA: 0x00052330 File Offset: 0x00050530
			private void CheckForUpdatesAsync(GrammarIdentifier grammarId)
			{
				LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("LocalGrammarCache.CheckForUpdatesAsync grammarId='{0}'", new object[]
				{
					grammarId
				});
				lock (this.grammarsBeingUpdatedLock)
				{
					if (!this.grammarsBeingUpdated.Contains(grammarId))
					{
						LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("LocalGrammarCache.CheckForUpdatesAsync, Adding grammarId='{0}' to grammarsBeingUpdated", new object[]
						{
							grammarId
						});
						this.grammarsBeingUpdated.Add(grammarId);
						ThreadPool.QueueUserWorkItem(new WaitCallback(this.CheckForUpdates), grammarId);
					}
				}
			}

			// Token: 0x06001282 RID: 4738 RVA: 0x00052564 File Offset: 0x00050764
			private void CheckForUpdates(object state)
			{
				GrammarIdentifier grammarId = (GrammarIdentifier)state;
				LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("LocalGrammarCache.CheckForUpdates, grammarId='{0}'", new object[]
				{
					grammarId
				});
				LargeGrammarFetcher.LocalGrammarCache.DoGrammarOperation(grammarId, delegate
				{
					string path;
					if (this.TryCheckIfFileExistsInCache(grammarId, out path))
					{
						bool flag = false;
						DateTime creationTimeUtc = File.GetCreationTimeUtc(path);
						LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("LocalGrammarCache.CheckForUpdates, grammarId='{0}', cachedCreationTimeUtc ='{1}'", new object[]
						{
							grammarId,
							creationTimeUtc
						});
						string latestGrammarFilePathInShare = LargeGrammarFetcher.GrammarFetcherUtils.GetLatestGrammarFilePathInShare(grammarId);
						if (latestGrammarFilePathInShare != null)
						{
							LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("LocalGrammarCache.CheckForUpdates, Downloading grammarId='{0}' from share '{1}'", new object[]
							{
								grammarId,
								latestGrammarFilePathInShare
							});
							flag = LargeGrammarFetcher.GrammarFetcherUtils.DownloadGrammarFromShare(grammarId, latestGrammarFilePathInShare, creationTimeUtc);
							LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("LocalGrammarCache.CheckForUpdates, Download attempt grammarId='{0}', share='{1}', downloadedGrammar='{2}'", new object[]
							{
								grammarId,
								latestGrammarFilePathInShare,
								flag
							});
						}
						if (!flag && (DateTime.UtcNow - TimeSpan.FromDays(5.0)).CompareTo(creationTimeUtc) > 0)
						{
							LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("LocalGrammarCache.CheckForUpdates, Try to download from mailbox, grammarId='{0}'", new object[]
							{
								grammarId
							});
							flag = LargeGrammarFetcher.GrammarFetcherUtils.DownloadGrammarFromMailbox(grammarId, creationTimeUtc);
							LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("LocalGrammarCache.CheckForUpdates, Download attempt grammarId='{0}', downloadedGrammar='{1}'", new object[]
							{
								grammarId,
								flag
							});
						}
					}
				}, delegate
				{
					lock (this.grammarsBeingUpdatedLock)
					{
						this.grammarsBeingUpdated.Remove(grammarId);
					}
				});
			}

			// Token: 0x04000C21 RID: 3105
			private static readonly LargeGrammarFetcher.LocalGrammarCache grammarCache = new LargeGrammarFetcher.LocalGrammarCache();

			// Token: 0x04000C22 RID: 3106
			private readonly Dictionary<GrammarIdentifier, ManualResetEvent> grammarsBeingDownloaded;

			// Token: 0x04000C23 RID: 3107
			private readonly object grammarsBeingDownloadedLock;

			// Token: 0x04000C24 RID: 3108
			private readonly HashSet<GrammarIdentifier> grammarsBeingUpdated;

			// Token: 0x04000C25 RID: 3109
			private readonly object grammarsBeingUpdatedLock;
		}

		// Token: 0x02000272 RID: 626
		private class GrammarFetcherUtils
		{
			// Token: 0x06001285 RID: 4741 RVA: 0x000525D4 File Offset: 0x000507D4
			public static void DebugTrace(string formatString, params object[] formatObjects)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, null, formatString, formatObjects);
			}

			// Token: 0x06001286 RID: 4742 RVA: 0x000525E3 File Offset: 0x000507E3
			public static void ErrorTrace(string formatString, params object[] formatObjects)
			{
				CallIdTracer.TraceError(ExTraceGlobals.UMGrammarGeneratorTracer, null, formatString, formatObjects);
			}

			// Token: 0x06001287 RID: 4743 RVA: 0x000525F4 File Offset: 0x000507F4
			public static string GetLatestGrammarFilePathInCache(GrammarIdentifier grammarId)
			{
				ValidateArgument.NotNull(grammarId, "grammarId");
				LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("GrammarFetcherUtils.GetLatestGrammarFilePathInCache grammar='{0}'", new object[]
				{
					grammarId
				});
				FileInfo[] cachedGrammarFiles = LargeGrammarFetcher.GrammarFetcherUtils.GetCachedGrammarFiles(grammarId);
				FileInfo fileInfo = null;
				if (cachedGrammarFiles != null)
				{
					foreach (FileInfo fileInfo2 in cachedGrammarFiles)
					{
						if (fileInfo == null || fileInfo2.CreationTimeUtc > fileInfo.CreationTimeUtc)
						{
							fileInfo = fileInfo2;
						}
					}
				}
				string text = (fileInfo == null) ? null : fileInfo.FullName;
				LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("GrammarFetcherUtils:GetLatestGrammarFilePathInCache grammar='{0}' returning '{1}'", new object[]
				{
					grammarId,
					text ?? "<null>"
				});
				return text;
			}

			// Token: 0x06001288 RID: 4744 RVA: 0x0005269C File Offset: 0x0005089C
			public static bool DownloadGrammarFromShare(GrammarIdentifier grammarId, string grammarFilePath, DateTime threshold)
			{
				ValidateArgument.NotNull(grammarId, "grammarId");
				ValidateArgument.NotNullOrEmpty(grammarFilePath, "grammarFilePath");
				LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("GrammarFetcherUtils.DownloadGrammarFromShare, grammar='{0}', grammarFilePath='{1}', threshold='{2}'", new object[]
				{
					grammarId,
					grammarFilePath,
					threshold
				});
				bool result = false;
				DateTime dateTime = (threshold == DateTime.MinValue) ? DateTime.MaxValue : File.GetCreationTimeUtc(grammarFilePath);
				LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("GrammarFetcherUtils.DownloadGrammarFromShare, grammar='{0}', grammarFilePath='{1}', fileCreationTimeUtc='{2}'", new object[]
				{
					grammarId,
					grammarFilePath,
					dateTime
				});
				if (dateTime > threshold)
				{
					LargeGrammarFetcher.GrammarFetcherUtils.UpdateCache(grammarId, grammarFilePath);
					result = true;
				}
				return result;
			}

			// Token: 0x06001289 RID: 4745 RVA: 0x00052738 File Offset: 0x00050938
			public static bool DownloadGrammarFromMailbox(GrammarIdentifier grammarId, DateTime threshold)
			{
				ValidateArgument.NotNull(grammarId, "grammarId");
				LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("GrammarFetcherUtils.DownloadGrammarFromMailbox, grammar='{0}', threshold='{1}'", new object[]
				{
					grammarId,
					threshold
				});
				bool result = false;
				Guid systemMailboxGuid = GrammarIdentifier.GetSystemMailboxGuid(grammarId.OrgId);
				if (systemMailboxGuid != Guid.Empty)
				{
					LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("GrammarFetcherUtils.DownloadGrammarFromMailbox, grammar='{0}', mailbox='{1}'", new object[]
					{
						grammarId,
						systemMailboxGuid
					});
					GrammarMailboxFileStore grammarMailboxFileStore = GrammarMailboxFileStore.FromMailboxGuid(grammarId.OrgId, systemMailboxGuid);
					string text = grammarMailboxFileStore.DownloadGrammar(grammarId.GrammarName + ".grxml", grammarId.Culture, threshold);
					if (text != null)
					{
						LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("GrammarFetcherUtils.DownloadGrammarFromMailbox, tmpFilePath='{0}'", new object[]
						{
							text
						});
						try
						{
							LargeGrammarFetcher.GrammarFetcherUtils.UpdateCache(grammarId, text);
						}
						finally
						{
							Util.TryDeleteFile(text);
						}
						result = true;
					}
				}
				return result;
			}

			// Token: 0x0600128A RID: 4746 RVA: 0x0005281C File Offset: 0x00050A1C
			public static string GetLatestGrammarFilePathInShare(GrammarIdentifier grammarId)
			{
				LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("GrammarFetcherUtils.GetLatestGrammarFilePathInShare, grammar='{0}'", new object[]
				{
					grammarId
				});
				Guid mailboxGuid = GrammarFileDistributionShare.GetMailboxGuid(grammarId.OrgId);
				string grammarManifestPath = GrammarFileDistributionShare.GetGrammarManifestPath(grammarId.OrgId, mailboxGuid);
				LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("GrammarFetcherUtils.GetLatestGrammarFilePathInShare, grammar='{0}', manifestPath='{1}'", new object[]
				{
					grammarId,
					grammarManifestPath
				});
				string text = null;
				if (mailboxGuid != Guid.Empty && File.Exists(grammarManifestPath))
				{
					LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("GrammarFetcherUtils.GetLatestGrammarFilePathInShare, grammar='{0}', manifestPath='{1}' exists", new object[]
					{
						grammarId,
						grammarManifestPath
					});
					GrammarGenerationMetadata grammarGenerationMetadata = GrammarGenerationMetadata.Deserialize(grammarManifestPath);
					string grammarFileFolderPath = GrammarFileDistributionShare.GetGrammarFileFolderPath(grammarId.OrgId, mailboxGuid, grammarGenerationMetadata.RunId, grammarId.Culture);
					LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("GrammarFetcherUtils.GetLatestGrammarFilePathInShare, grammar='{0}', grammarFolderPath='{1}'", new object[]
					{
						grammarId,
						grammarFileFolderPath
					});
					text = Path.Combine(grammarFileFolderPath, grammarId.GrammarName + ".grxml");
					LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("GrammarFetcherUtils.GetLatestGrammarFilePathInShare, grammar='{0}', grammarFilePath='{1}'", new object[]
					{
						grammarId,
						text
					});
				}
				else
				{
					LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("GrammarFetcherUtils.GetLatestGrammarFilePathInShare, grammar='{0}', manifestPath='{1}' does not exist", new object[]
					{
						grammarId,
						grammarManifestPath
					});
				}
				return text;
			}

			// Token: 0x0600128B RID: 4747 RVA: 0x0005294C File Offset: 0x00050B4C
			private static string GetUniqueCompiledGrammarFilePath(GrammarIdentifier grammarId)
			{
				int num = 0;
				string tenantTopLevelGrammarDirPath = grammarId.TenantTopLevelGrammarDirPath;
				string text;
				do
				{
					ExAssert.RetailAssert(num < 100, "Couldn't generate unique name!");
					string path = string.Format(CultureInfo.InvariantCulture, "{0}-{1}{2}", new object[]
					{
						grammarId.GrammarName,
						num.ToString(CultureInfo.InvariantCulture),
						".cfg"
					});
					text = Path.Combine(tenantTopLevelGrammarDirPath, path);
					LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("GrammarFetcherUtils.GetUniqueCompiledGrammarFilePath, grammar='{0}', Checking filePath='{1}'", new object[]
					{
						grammarId,
						text
					});
					num++;
				}
				while (File.Exists(text));
				LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("GrammarFetcherUtils.GetUniqueCompiledGrammarFilePath, grammar='{0}', filePath='{1}'", new object[]
				{
					grammarId,
					text
				});
				return text;
			}

			// Token: 0x0600128C RID: 4748 RVA: 0x00052A04 File Offset: 0x00050C04
			public static void UpdateCache(GrammarIdentifier grammarId, string grammarFilePath)
			{
				ValidateArgument.NotNull(grammarId, "grammarId");
				ValidateArgument.NotNull(grammarFilePath, "grammarFilePath");
				LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("GrammarFetcherUtils.UpdateCache, grammar='{0}', grammarFilePath='{1}'", new object[]
				{
					grammarId,
					grammarFilePath
				});
				string tenantTopLevelGrammarDirPath = grammarId.TenantTopLevelGrammarDirPath;
				Directory.CreateDirectory(tenantTopLevelGrammarDirPath);
				string text = Path.Combine(tenantTopLevelGrammarDirPath, Guid.NewGuid().ToString());
				LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("GrammarFetcherUtils.UpdateCache, grammar='{0}', tmpCompiledGrammarFilePath='{1}'", new object[]
				{
					grammarId,
					text
				});
				Platform.Utilities.CompileGrammar(grammarFilePath, text, grammarId.Culture);
				string uniqueCompiledGrammarFilePath = LargeGrammarFetcher.GrammarFetcherUtils.GetUniqueCompiledGrammarFilePath(grammarId);
				LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("GrammarFetcherUtils.UpdateCache, grammar='{0}', finalFilePath='{1}'", new object[]
				{
					grammarId,
					uniqueCompiledGrammarFilePath
				});
				File.Move(text, uniqueCompiledGrammarFilePath);
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMGrammarFetcherSuccess, null, new object[]
				{
					grammarId,
					uniqueCompiledGrammarFilePath
				});
				LargeGrammarFetcher.GrammarFetcherUtils.CleanUpOldGrammarFiles(grammarId, uniqueCompiledGrammarFilePath);
			}

			// Token: 0x0600128D RID: 4749 RVA: 0x00052BC8 File Offset: 0x00050DC8
			private static void CleanUpOldGrammarFiles(GrammarIdentifier grammarId, string currentFilePath)
			{
				Utils.TryDiskOperation(delegate
				{
					FileInfo[] cachedGrammarFiles = LargeGrammarFetcher.GrammarFetcherUtils.GetCachedGrammarFiles(grammarId);
					if (cachedGrammarFiles != null)
					{
						foreach (FileInfo fileInfo in cachedGrammarFiles)
						{
							string fullName = fileInfo.FullName;
							if (!string.Equals(fullName, currentFilePath, StringComparison.OrdinalIgnoreCase))
							{
								LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("GrammarFetcherUtils.CleanUpOldGrammarFiles, Adding sentinel file for '{0}'", new object[]
								{
									fullName
								});
								string path = fullName + ".delete";
								if (!File.Exists(path))
								{
									File.Create(path).Close();
								}
							}
						}
					}
				}, delegate(Exception exception)
				{
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_GrammarFetcherCleanupFailed, null, new object[]
					{
						grammarId.OrgId,
						CommonUtil.ToEventLogString(exception)
					});
				});
			}

			// Token: 0x0600128E RID: 4750 RVA: 0x00052C08 File Offset: 0x00050E08
			private static FileInfo[] GetCachedGrammarFiles(GrammarIdentifier grammarId)
			{
				LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("GrammarFetcherUtils.GetCachedGrammarFiles grammar='{0}'", new object[]
				{
					grammarId
				});
				FileInfo[] result = null;
				DirectoryInfo directoryInfo = new DirectoryInfo(grammarId.TenantTopLevelGrammarDirPath);
				if (directoryInfo.Exists)
				{
					string text = string.Format(CultureInfo.InvariantCulture, "{0}-{1}{2}", new object[]
					{
						grammarId.GrammarName,
						"*",
						".cfg"
					});
					LargeGrammarFetcher.GrammarFetcherUtils.DebugTrace("GrammarFetcherUtils.GetCachedGrammarFiles grammar='{0}', searchPattern='{1}'", new object[]
					{
						grammarId,
						text
					});
					result = directoryInfo.GetFiles(text, SearchOption.TopDirectoryOnly);
				}
				return result;
			}

			// Token: 0x04000C27 RID: 3111
			private const string CompiledGrammarNameFormat = "{0}-{1}{2}";
		}
	}
}
