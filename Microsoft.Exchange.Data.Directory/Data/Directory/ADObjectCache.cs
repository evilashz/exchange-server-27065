using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Threading;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020007AE RID: 1966
	internal class ADObjectCache<TResult, TLoaderException> : DisposeTrackableBase where TResult : ADObject, new() where TLoaderException : Exception
	{
		// Token: 0x060061AE RID: 25006 RVA: 0x0014E186 File Offset: 0x0014C386
		public ADObjectCache(Func<TResult[], TResult[]> loadOperator, string refreshIntervalRegistryKey = null)
		{
			this.RefreshInterval = ADObjectCache<TResult, TLoaderException>.DefaultRefreshInterval;
			this.refreshLock = new object();
			this.IsInitialized = false;
			this.LoadOperator = loadOperator;
			this.RefreshIntervalRegistryKey = refreshIntervalRegistryKey;
		}

		// Token: 0x170022D5 RID: 8917
		// (get) Token: 0x060061AF RID: 25007 RVA: 0x0014E1B9 File Offset: 0x0014C3B9
		// (set) Token: 0x060061B0 RID: 25008 RVA: 0x0014E1C1 File Offset: 0x0014C3C1
		public bool IsInitialized { get; protected set; }

		// Token: 0x170022D6 RID: 8918
		// (get) Token: 0x060061B1 RID: 25009 RVA: 0x0014E1CA File Offset: 0x0014C3CA
		public bool IsAutoRefreshed
		{
			get
			{
				return this.RefreshInterval != TimeSpan.Zero;
			}
		}

		// Token: 0x170022D7 RID: 8919
		// (get) Token: 0x060061B2 RID: 25010 RVA: 0x0014E1DC File Offset: 0x0014C3DC
		public TResult[] Value
		{
			get
			{
				return Interlocked.CompareExchange<TResult[]>(ref this.internalValue, null, null);
			}
		}

		// Token: 0x170022D8 RID: 8920
		// (get) Token: 0x060061B3 RID: 25011 RVA: 0x0014E1EB File Offset: 0x0014C3EB
		// (set) Token: 0x060061B4 RID: 25012 RVA: 0x0014E1F3 File Offset: 0x0014C3F3
		public DateTime LastModified { get; private set; }

		// Token: 0x060061B5 RID: 25013 RVA: 0x0014E1FC File Offset: 0x0014C3FC
		public void SetRefreshInterval(TimeSpan refreshInterval)
		{
			this.RefreshInterval = refreshInterval;
			this.RefreshTimer.Change(this.RefreshInterval, this.RefreshInterval);
		}

		// Token: 0x170022D9 RID: 8921
		// (get) Token: 0x060061B6 RID: 25014 RVA: 0x0014E21D File Offset: 0x0014C41D
		// (set) Token: 0x060061B7 RID: 25015 RVA: 0x0014E225 File Offset: 0x0014C425
		public TimeSpan RefreshInterval { get; private set; }

		// Token: 0x170022DA RID: 8922
		// (get) Token: 0x060061B8 RID: 25016 RVA: 0x0014E22E File Offset: 0x0014C42E
		// (set) Token: 0x060061B9 RID: 25017 RVA: 0x0014E236 File Offset: 0x0014C436
		private protected string RefreshIntervalRegistryKey { protected get; private set; }

		// Token: 0x170022DB RID: 8923
		// (get) Token: 0x060061BA RID: 25018 RVA: 0x0014E23F File Offset: 0x0014C43F
		// (set) Token: 0x060061BB RID: 25019 RVA: 0x0014E247 File Offset: 0x0014C447
		private Func<TResult[], TResult[]> LoadOperator { get; set; }

		// Token: 0x170022DC RID: 8924
		// (get) Token: 0x060061BC RID: 25020 RVA: 0x0014E250 File Offset: 0x0014C450
		// (set) Token: 0x060061BD RID: 25021 RVA: 0x0014E258 File Offset: 0x0014C458
		private GuardedTimer RefreshTimer { get; set; }

		// Token: 0x060061BE RID: 25022 RVA: 0x0014E261 File Offset: 0x0014C461
		public void Initialize(bool refreshNow = true)
		{
			this.Initialize(ADObjectCache<TResult, TLoaderException>.DefaultRefreshInterval, refreshNow);
		}

		// Token: 0x060061BF RID: 25023 RVA: 0x0014E270 File Offset: 0x0014C470
		public void Initialize(TimeSpan refreshInterval, bool refreshNow = true)
		{
			if (this.IsInitialized)
			{
				return;
			}
			if (!string.IsNullOrEmpty(this.RefreshIntervalRegistryKey))
			{
				int value = RegistryReader.Instance.GetValue<int>(Registry.LocalMachine, this.RefreshIntervalRegistryKey, "RefreshInterval", (int)ADObjectCache<TResult, TLoaderException>.DefaultRefreshInterval.TotalSeconds);
				refreshInterval = new TimeSpan(0, 0, value);
			}
			if (refreshNow)
			{
				this.Refresh(null);
			}
			this.RefreshTimer = new GuardedTimer(new TimerCallback(this.Refresh));
			this.SetRefreshInterval(refreshInterval);
			this.IsInitialized = true;
		}

		// Token: 0x060061C0 RID: 25024 RVA: 0x0014E2F8 File Offset: 0x0014C4F8
		public void Refresh(object unusedState)
		{
			try
			{
				if (Monitor.TryEnter(this.refreshLock))
				{
					TResult[] value;
					try
					{
						value = this.LoadOperator(this.Value);
						this.LastModified = DateTime.UtcNow;
					}
					catch (TLoaderException)
					{
						value = this.Value;
					}
					Interlocked.Exchange<TResult[]>(ref this.internalValue, value);
				}
			}
			finally
			{
				if (Monitor.IsEntered(this.refreshLock))
				{
					Monitor.Exit(this.refreshLock);
				}
			}
		}

		// Token: 0x060061C1 RID: 25025 RVA: 0x0014E380 File Offset: 0x0014C580
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ADObjectCache<TResult, TLoaderException>>(this);
		}

		// Token: 0x060061C2 RID: 25026 RVA: 0x0014E388 File Offset: 0x0014C588
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.RefreshTimer != null)
				{
					this.RefreshTimer.Dispose(false);
				}
				this.RefreshTimer = null;
				this.IsInitialized = false;
			}
		}

		// Token: 0x04004183 RID: 16771
		public const string RefreshIntervalName = "RefreshInterval";

		// Token: 0x04004184 RID: 16772
		private static readonly TimeSpan DefaultRefreshInterval = TimeSpan.FromMinutes(15.0);

		// Token: 0x04004185 RID: 16773
		private object refreshLock;

		// Token: 0x04004186 RID: 16774
		private TResult[] internalValue;
	}
}
