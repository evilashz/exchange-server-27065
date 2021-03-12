using System;
using System.Diagnostics;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.ServiceSupport;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.MailboxLoadBalance
{
	// Token: 0x02000091 RID: 145
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class VersionedServiceBase : DisposeTrackableBase, IVersionedService, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000546 RID: 1350 RVA: 0x0000DB9D File Offset: 0x0000BD9D
		protected VersionedServiceBase(ILogger logger)
		{
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			this.Logger = logger;
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x0000DBBA File Offset: 0x0000BDBA
		// (set) Token: 0x06000548 RID: 1352 RVA: 0x0000DBC2 File Offset: 0x0000BDC2
		public VersionInformation ClientVersion { get; private set; }

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x0000DBCB File Offset: 0x0000BDCB
		// (set) Token: 0x0600054A RID: 1354 RVA: 0x0000DBD3 File Offset: 0x0000BDD3
		private protected ILogger Logger { protected get; private set; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x0600054B RID: 1355
		protected abstract VersionInformation ServiceVersion { get; }

		// Token: 0x0600054C RID: 1356 RVA: 0x0000DBDC File Offset: 0x0000BDDC
		public void ExchangeVersionInformation(VersionInformation clientVersion, out VersionInformation serverVersion)
		{
			this.ClientVersion = clientVersion;
			serverVersion = this.ServiceVersion;
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0000DC14 File Offset: 0x0000BE14
		protected TResult ForwardExceptions<TResult>(Func<TResult> operation)
		{
			this.Logger.Log(MigrationEventType.Instrumentation, "BEGIN Processing service call: {0}", new object[]
			{
				operation.Method
			});
			Stopwatch stopwatch = Stopwatch.StartNew();
			Exception failure = null;
			TResult result = default(TResult);
			try
			{
				CommonUtils.FailureDelegate processFailure = delegate(Exception exception)
				{
					failure = exception;
					return true;
				};
				CommonUtils.ProcessKnownExceptionsWithoutTracing(delegate
				{
					result = operation();
				}, processFailure);
			}
			catch (Exception ex)
			{
				if (failure == null)
				{
					failure = ex;
				}
				else
				{
					this.Logger.Log(MigrationEventType.Warning, "Re-throwing exception while processing service call {0} since another failure was already recorded: {1}", new object[]
					{
						operation.Method,
						ex
					});
				}
				throw;
			}
			finally
			{
				stopwatch.Stop();
				this.Logger.Log(MigrationEventType.Instrumentation, "END Processing service call: {0}. Duration: {1} ms. Exception: {2}", new object[]
				{
					operation.Method,
					stopwatch.ElapsedMilliseconds,
					failure
				});
			}
			if (failure != null)
			{
				this.Logger.LogError(failure, "Service call {0} failed.", new object[]
				{
					operation.Method
				});
				LoadBalanceFault.Throw(failure);
			}
			return result;
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0000DDBC File Offset: 0x0000BFBC
		protected void ForwardExceptions(Action operation)
		{
			this.Logger.Log(MigrationEventType.Instrumentation, "BEGIN Processing service call: {0}", new object[]
			{
				operation.Method
			});
			Stopwatch stopwatch = Stopwatch.StartNew();
			Exception failure = null;
			try
			{
				CommonUtils.CatchKnownExceptions(operation, delegate(Exception exception)
				{
					failure = exception;
				});
			}
			catch (Exception ex)
			{
				if (failure == null)
				{
					failure = ex;
				}
				else
				{
					this.Logger.Log(MigrationEventType.Warning, "Re-throwing exception while processing service call {0} since another failure was already recorded: {1}", new object[]
					{
						operation.Method,
						ex
					});
				}
				throw;
			}
			finally
			{
				stopwatch.Stop();
				this.Logger.Log(MigrationEventType.Instrumentation, "END Processing service call: {0}. Duration: {1} ms. Exception: {2}", new object[]
				{
					operation.Method,
					stopwatch.ElapsedMilliseconds,
					failure
				});
			}
			if (failure != null)
			{
				this.Logger.LogError(failure, "Service call {0} failed.", new object[]
				{
					operation.Method
				});
				LoadBalanceFault.Throw(failure);
			}
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0000DEFC File Offset: 0x0000C0FC
		protected override void InternalDispose(bool disposing)
		{
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0000DEFE File Offset: 0x0000C0FE
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<VersionedServiceBase>(this);
		}
	}
}
