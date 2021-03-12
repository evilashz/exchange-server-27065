using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Isam.Esent.Interop;
using Microsoft.Isam.Esent.Interop.Windows8;

namespace Microsoft.Exchange.Transport.Storage
{
	// Token: 0x020000D8 RID: 216
	internal class Transaction : IDisposeTrackable, IDisposable
	{
		// Token: 0x060007BE RID: 1982 RVA: 0x0001ED64 File Offset: 0x0001CF64
		static Transaction()
		{
			Transaction.TransactionTracker = new OperationTracker<Transaction>(() => TimeSpan.FromTicks(Math.Max(TimeSpan.FromSeconds(1.0).Ticks, Transaction.TransactionTracker.PercentileQuery(99.0).Ticks * 2L)), new Action<Transaction, TimeSpan>(Transaction.LogLongRunningTransaction), TimeSpan.FromMilliseconds(100.0), TimeSpan.FromSeconds(10.0));
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0001EE7A File Offset: 0x0001D07A
		private static void LogLongRunningTransaction(Transaction transaction, TimeSpan duration)
		{
			if (ExTraceGlobals.StorageTracer.IsTraceEnabled(TraceType.WarningTrace))
			{
				ExTraceGlobals.StorageTracer.TraceWarning<TimeSpan, StackTrace>((long)transaction.GetHashCode(), "Long running transaction detected: Duration({0}), Stack: {1}", duration, new StackTrace(1, true));
			}
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0001EEA7 File Offset: 0x0001D0A7
		private Transaction(DataConnection connection)
		{
			this.connection = connection;
			this.connection.AddRef();
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0001EEDC File Offset: 0x0001D0DC
		internal static Transaction New(DataConnection connection)
		{
			Transaction transaction = new Transaction(connection);
			transaction.EnterTransaction();
			return transaction;
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060007C2 RID: 1986 RVA: 0x0001EEF8 File Offset: 0x0001D0F8
		// (remove) Token: 0x060007C3 RID: 1987 RVA: 0x0001EF30 File Offset: 0x0001D130
		public event Action OnExitTransaction;

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x0001EF65 File Offset: 0x0001D165
		public DataConnection Connection
		{
			get
			{
				return this.connection;
			}
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0001F030 File Offset: 0x0001D230
		public static XElement GetDiagnosticInfo(string argument)
		{
			if (argument.Equals("TransactionsOpen", StringComparison.InvariantCultureIgnoreCase))
			{
				return new XElement("TransactionsOpen", from runningOperation in Transaction.TransactionTracker.GetRunningOperations()
				select new XElement("Transaction", new object[]
				{
					new XElement("Duration", runningOperation.Item2),
					new XElement("StackTrace", runningOperation.Item3)
				}));
			}
			if (argument.StartsWith("TransactionStartTrace", StringComparison.InvariantCultureIgnoreCase))
			{
				Match match = Regex.Match(argument, "TransactionStartTrace\\((\\d+),(\\d+)\\)", RegexOptions.IgnoreCase);
				int val;
				int num;
				if (match.Success && int.TryParse(match.Groups[1].Value, out val) && int.TryParse(match.Groups[2].Value, out num))
				{
					Transaction.TransactionTracker.StartTracing(Math.Min(val, 10000), TimeSpan.FromMilliseconds((double)num));
					return new XElement("TransactionTrace", new XElement("Started", true));
				}
				return new XElement("TransactionTrace", new object[]
				{
					new XElement("Started", false),
					new XElement("Usage", "TransactionTrace([TransactionsToSample], [TransactionDurationThresholdMilliseconds])")
				});
			}
			else
			{
				if (argument.Equals("TransactionTrace", StringComparison.InvariantCultureIgnoreCase))
				{
					ICollection<OperationTracker<Transaction>.StackCounter> tracedStack = Transaction.TransactionTracker.TracedStack;
					XName name = "TransactionTrace";
					object[] array = new object[3];
					array[0] = new XElement("OperationCount", tracedStack.Sum((OperationTracker<Transaction>.StackCounter i) => i.Count));
					array[1] = new XElement("StackCount", tracedStack.Count);
					array[2] = from i in tracedStack
					select new XElement("Stack", new object[]
					{
						new XElement("OperationCount", i.Count),
						new XElement("StackTrace", i.StackTrace)
					});
					return new XElement(name, array);
				}
				if (!argument.StartsWith("TransactionPercentile", StringComparison.InvariantCultureIgnoreCase))
				{
					return null;
				}
				Match match2 = Regex.Match(argument, "TransactionPercentile\\((\\d+)\\)", RegexOptions.IgnoreCase);
				double num2;
				if (match2.Success && double.TryParse(match2.Groups[1].Value, out num2))
				{
					return new XElement("TransactionPercentile", new object[]
					{
						new XElement("Percentile", num2),
						new XElement("Duration", Transaction.TransactionTracker.PercentileQuery(num2))
					});
				}
				return new XElement("TransactionPercentile", new XElement("Usage", "TransactionPercentile([PercentileToQuery])"));
			}
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0001F2E3 File Offset: 0x0001D4E3
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0001F2F2 File Offset: 0x0001D4F2
		public virtual DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<Transaction>(this);
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0001F2FA File Offset: 0x0001D4FA
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x0001F30F File Offset: 0x0001D50F
		public void Commit()
		{
			this.Commit(TransactionCommitMode.MediumLatencyLazy);
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0001F318 File Offset: 0x0001D518
		public void Commit(TransactionCommitMode mode)
		{
			switch (mode)
			{
			case TransactionCommitMode.Lazy:
				this.Commit(CommitTransactionGrbit.LazyFlush, null);
				return;
			case TransactionCommitMode.ShortLatencyLazy:
				this.Commit(CommitTransactionGrbit.LazyFlush, new TimeSpan?(Transaction.ShortLazyTimeout));
				return;
			case TransactionCommitMode.MediumLatencyLazy:
				this.Commit(CommitTransactionGrbit.LazyFlush, new TimeSpan?(Transaction.MediumLazyTimeout));
				return;
			case TransactionCommitMode.Immediate:
				this.Commit(CommitTransactionGrbit.None, null);
				return;
			default:
				return;
			}
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x0001F387 File Offset: 0x0001D587
		public void AsyncCommit(TimeSpan durableTimeout)
		{
			this.Commit(CommitTransactionGrbit.LazyFlush, new TimeSpan?(durableTimeout));
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x0001F3F4 File Offset: 0x0001D5F4
		public IAsyncResult BeginAsyncCommit(TimeSpan durableTimeout, object asyncState, AsyncCallback callback)
		{
			AsyncResult result = new AsyncResult(callback, asyncState);
			Transaction.PerfCounters.TransactionAsyncCommitCount.Increment();
			Transaction.PerfCounters.TransactionAsyncCommitPendingCount.Increment();
			Stopwatch stopwatch = Stopwatch.StartNew();
			this.connection.Source.RegisterAsyncCommitCallback(this.Commit(CommitTransactionGrbit.LazyFlush, new TimeSpan?(durableTimeout)), delegate
			{
				result.IsCompleted = true;
				Transaction.PerfCounters.TransactionAsyncCommitAveragePendingDuration.IncrementBy(stopwatch.ElapsedTicks);
				Transaction.PerfCounters.TransactionAsyncCommitAveragePendingDurationBase.Increment();
				Transaction.PerfCounters.TransactionAsyncCommitPendingCount.Decrement();
			});
			return result;
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0001F470 File Offset: 0x0001D670
		public void EndAsyncCommit(IAsyncResult asyncResult)
		{
			AsyncResult asyncResult2 = (AsyncResult)asyncResult;
			if (!asyncResult.IsCompleted)
			{
				asyncResult.AsyncWaitHandle.WaitOne();
			}
			asyncResult2.End();
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0001F4A0 File Offset: 0x0001D6A0
		public void Abort()
		{
			this.ThrowIfTransactionNotRunning();
			try
			{
				Api.JetRollback(this.connection.Session, RollbackTransactionGrbit.None);
			}
			catch (EsentErrorException ex)
			{
				if (!DataSource.HandleIsamException(ex, this.connection.Source))
				{
					throw;
				}
			}
			Transaction.PerfCounters.TransactionAbortCount.Increment();
			this.ExitTransaction();
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0001F504 File Offset: 0x0001D704
		public void Checkpoint()
		{
			this.Checkpoint(TransactionCommitMode.Lazy, 100);
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0001F510 File Offset: 0x0001D710
		public void Checkpoint(TransactionCommitMode mode, byte maxLoad = 100)
		{
			if (maxLoad == 0 || maxLoad > 100)
			{
				throw new ArgumentException("maxLoad has to be between 1 and 100", "maxLoad");
			}
			long elapsedMilliseconds = this.openStopwatch.ElapsedMilliseconds;
			TimeSpan pause = TimeSpan.FromMilliseconds((double)(elapsedMilliseconds * 100L / (long)((ulong)maxLoad) - elapsedMilliseconds));
			this.Checkpoint(mode, pause);
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x0001F55A File Offset: 0x0001D75A
		public void Checkpoint(TransactionCommitMode mode, TimeSpan pause)
		{
			this.Commit(mode);
			if (pause > TimeSpan.Zero)
			{
				Thread.Sleep(pause);
			}
			this.EnterTransaction();
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0001F57C File Offset: 0x0001D77C
		public void RestartIfStale(byte maxLoad = 100)
		{
			this.operationsInCurrentTransaction++;
			if (this.openStopwatch.Elapsed > Transaction.StaleTransactionTimeout || this.operationsInCurrentTransaction > 500)
			{
				this.Checkpoint(TransactionCommitMode.Lazy, maxLoad);
			}
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0001F5B8 File Offset: 0x0001D7B8
		protected virtual void Dispose(bool disposing)
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
			if (this.connection != null)
			{
				if (disposing && this.transactionRunning)
				{
					this.Abort();
				}
				this.connection.Release();
				this.connection = null;
			}
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x0001F604 File Offset: 0x0001D804
		private JET_COMMIT_ID Commit(CommitTransactionGrbit grbit, TimeSpan? durableTimeout)
		{
			this.ThrowIfTransactionNotRunning();
			JET_COMMIT_ID result = null;
			if (durableTimeout != null || grbit == CommitTransactionGrbit.LazyFlush)
			{
				Transaction.PerfCounters.TransactionSoftCommitCount.Increment();
				Transaction.PerfCounters.TransactionSoftCommitPendingCount.Increment();
			}
			else
			{
				Transaction.PerfCounters.TransactionHardCommitCount.Increment();
				Transaction.PerfCounters.TransactionHardCommitPendingCount.Increment();
			}
			Stopwatch stopwatch = Stopwatch.StartNew();
			try
			{
				if (durableTimeout != null)
				{
					Windows8Api.JetCommitTransaction2(this.connection.Session, grbit, durableTimeout.Value, out result);
				}
				else
				{
					Api.JetCommitTransaction(this.connection.Session, grbit);
				}
			}
			catch (EsentErrorException ex)
			{
				if (!DataSource.HandleIsamException(ex, this.connection.Source))
				{
					throw;
				}
			}
			stopwatch.Stop();
			if (durableTimeout != null || grbit == CommitTransactionGrbit.LazyFlush)
			{
				Transaction.PerfCounters.TransactionSoftCommitAveragePendingDuration.IncrementBy(stopwatch.ElapsedTicks);
				Transaction.PerfCounters.TransactionSoftCommitAveragePendingDurationBase.Increment();
				Transaction.PerfCounters.TransactionSoftCommitPendingCount.Decrement();
			}
			else
			{
				Transaction.PerfCounters.TransactionHardCommitAveragePendingDuration.IncrementBy(stopwatch.ElapsedTicks);
				Transaction.PerfCounters.TransactionHardCommitAveragePendingDurationBase.Increment();
				Transaction.PerfCounters.TransactionHardCommitPendingCount.Decrement();
			}
			this.ExitTransaction();
			return result;
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0001F758 File Offset: 0x0001D958
		private void EnterTransaction()
		{
			this.connection.TrackStartTransaction();
			try
			{
				Api.JetBeginTransaction(this.Connection.Session);
			}
			catch (EsentErrorException ex)
			{
				if (!DataSource.HandleIsamException(ex, this.connection.Source))
				{
					throw;
				}
			}
			this.operationsInCurrentTransaction = 0;
			this.openStopwatch.Restart();
			this.transactionRunning = true;
			Transaction.TransactionTracker.Enter(this);
			Transaction.PerfCounters.TransactionPendingCount.Increment();
			Transaction.PerfCounters.TransactionCount.Increment();
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0001F7F0 File Offset: 0x0001D9F0
		private void ExitTransaction()
		{
			this.connection.TrackRemoveTransaction();
			this.transactionRunning = false;
			this.openStopwatch.Stop();
			TimeSpan timeSpan = Transaction.TransactionTracker.Exit(this);
			Transaction.PerfCounters.TransactionPendingCount.Decrement();
			Transaction.PerfCounters.TransactionAveragePendingDuration.IncrementBy(timeSpan.Ticks);
			Transaction.PerfCounters.TransactionAveragePendingDurationBase.Increment();
			if (Transaction.TransactionTracker.TotalOperationCount % 10L == 0L)
			{
				Transaction.PerfCounters.TransactionPending99PercentileDuration.RawValue = (long)Transaction.TransactionTracker.PercentileQuery(99.0).TotalSeconds;
			}
			if (this.OnExitTransaction != null)
			{
				this.OnExitTransaction();
			}
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x0001F8AD File Offset: 0x0001DAAD
		private void ThrowIfTransactionNotRunning()
		{
			if (!this.transactionRunning)
			{
				throw new InvalidOperationException(Strings.NotInTransaction);
			}
		}

		// Token: 0x040003E1 RID: 993
		private const int MaxOperationsInTransaction = 500;

		// Token: 0x040003E2 RID: 994
		private static readonly OperationTracker<Transaction> TransactionTracker;

		// Token: 0x040003E3 RID: 995
		internal static readonly DatabasePerfCountersInstance PerfCounters = DatabasePerfCounters.GetInstance("other");

		// Token: 0x040003E4 RID: 996
		public static TimeSpan StaleTransactionTimeout = TransportAppConfig.GetConfigTimeSpan("StaleTransactionTimeout", TimeSpan.FromSeconds(1.0), TimeSpan.FromMinutes(30.0), TimeSpan.FromSeconds(30.0));

		// Token: 0x040003E5 RID: 997
		public static TimeSpan ShortLazyTimeout = TransportAppConfig.GetConfigTimeSpan("ShortLazyTransactionTimeout", TimeSpan.FromMilliseconds(10.0), TimeSpan.FromMinutes(30.0), TimeSpan.FromMilliseconds(250.0));

		// Token: 0x040003E6 RID: 998
		public static TimeSpan MediumLazyTimeout = TransportAppConfig.GetConfigTimeSpan("MediumLazyTransactionTimeout", TimeSpan.FromMilliseconds(10.0), TimeSpan.FromMinutes(30.0), TimeSpan.FromSeconds(3.0));

		// Token: 0x040003E7 RID: 999
		private readonly DisposeTracker disposeTracker;

		// Token: 0x040003E8 RID: 1000
		private readonly Stopwatch openStopwatch = new Stopwatch();

		// Token: 0x040003E9 RID: 1001
		private DataConnection connection;

		// Token: 0x040003EA RID: 1002
		private bool transactionRunning;

		// Token: 0x040003EB RID: 1003
		private int operationsInCurrentTransaction;
	}
}
