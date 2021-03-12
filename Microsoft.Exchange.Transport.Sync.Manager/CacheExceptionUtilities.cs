using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x02000004 RID: 4
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CacheExceptionUtilities
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002158 File Offset: 0x00000358
		protected CacheExceptionUtilities(GlobalSyncLogSession globalSyncLogSession)
		{
			SyncUtilities.ThrowIfArgumentNull("globalSyncLogSession", globalSyncLogSession);
			this.globalSyncLogSession = globalSyncLogSession;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002174 File Offset: 0x00000374
		public static CacheExceptionUtilities Instance
		{
			get
			{
				if (CacheExceptionUtilities.instance == null)
				{
					lock (CacheExceptionUtilities.syncLock)
					{
						if (CacheExceptionUtilities.instance == null)
						{
							CacheExceptionUtilities.instance = new CacheExceptionUtilities(ContentAggregationConfig.SyncLogSession);
						}
					}
				}
				return CacheExceptionUtilities.instance;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021D0 File Offset: 0x000003D0
		internal bool IsSessionReusableAfterException(Exception e)
		{
			return !(e is LocalizedException) || (e is StorageTransientException && !(e is ConnectionFailedTransientException) && (e.InnerException == null || !(e.InnerException is MapiExceptionRpcServerTooBusy)));
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002208 File Offset: 0x00000408
		internal CacheTransientException CreateCacheTransientException(Trace tracer, int objectHash, Guid databaseGuid, Guid mailboxGuid, LocalizedString exceptionInfo)
		{
			this.globalSyncLogSession.LogError((TSLID)276UL, tracer, (long)objectHash, Guid.Empty, mailboxGuid, "In database {0}, encountered:{1}.", new object[]
			{
				databaseGuid,
				exceptionInfo
			});
			return new CacheTransientException(databaseGuid, mailboxGuid, exceptionInfo);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002264 File Offset: 0x00000464
		internal Exception ConvertToCacheException(Trace tracer, int objectHash, Guid databaseGuid, Guid mailboxGuid, Exception exception, out bool reuseSession)
		{
			reuseSession = this.IsSessionReusableAfterException(exception);
			bool flag = false;
			Exception ex;
			if (exception is TransientException)
			{
				ex = new CacheTransientException(databaseGuid, mailboxGuid, (TransientException)exception);
			}
			else
			{
				bool flag2 = ExceptionUtilities.IndicatesUserNotFound(exception);
				if (flag2 || exception is SerializationException || exception is CorruptDataException)
				{
					if (!mailboxGuid.Equals(Guid.Empty))
					{
						DataAccessLayer.ScheduleMailboxCrawl(databaseGuid, mailboxGuid);
					}
					if (flag2)
					{
						ex = new MailboxNotFoundException(databaseGuid, mailboxGuid, exception);
					}
					else
					{
						ex = new CacheCorruptException(databaseGuid, mailboxGuid, exception);
						flag = (exception is SerializationException && !(exception is VersionMismatchException));
					}
				}
				else if (ExceptionUtilities.ShouldPermanentMapiOrXsoExceptionBeTreatedAsTransient(this.globalSyncLogSession, exception))
				{
					ex = new CacheTransientException(databaseGuid, mailboxGuid, exception);
				}
				else
				{
					ex = new CachePermanentException(databaseGuid, mailboxGuid, exception);
					flag = true;
				}
			}
			if (flag)
			{
				this.ReportWatson(null, ex);
			}
			else
			{
				bool flag3 = ex is CacheTransientException && ((CacheTransientException)ex).StoreRequestedBackOff;
				if (flag3)
				{
					this.globalSyncLogSession.LogError((TSLID)329UL, tracer, (long)objectHash, Guid.Empty, mailboxGuid, "In database {0}, encountered exception {1}, mailboxSession usable: {2}.", new object[]
					{
						databaseGuid,
						exception,
						reuseSession
					});
				}
				else
				{
					this.globalSyncLogSession.LogError((TSLID)277UL, tracer, (long)objectHash, Guid.Empty, mailboxGuid, "In database {0}, encountered: {1}:{2}:{3}, mailboxSession usable:{4}.", new object[]
					{
						databaseGuid,
						exception.GetType().FullName,
						exception.Message,
						(exception.InnerException != null) ? exception.InnerException.GetType().FullName : null,
						reuseSession
					});
				}
			}
			return ex;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002435 File Offset: 0x00000635
		protected virtual void ReportWatson(string message, Exception exception)
		{
			DataAccessLayer.ReportWatson(message, exception);
		}

		// Token: 0x04000003 RID: 3
		private static readonly object syncLock = new object();

		// Token: 0x04000004 RID: 4
		private static CacheExceptionUtilities instance;

		// Token: 0x04000005 RID: 5
		private readonly GlobalSyncLogSession globalSyncLogSession;
	}
}
