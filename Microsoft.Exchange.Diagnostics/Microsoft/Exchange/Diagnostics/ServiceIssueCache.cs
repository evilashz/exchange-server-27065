using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Timers;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200015B RID: 347
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class ServiceIssueCache : IDiagnosable
	{
		// Token: 0x060009EA RID: 2538 RVA: 0x0002502C File Offset: 0x0002322C
		protected ServiceIssueCache()
		{
			this.issueCache = new Dictionary<string, ServiceIssue>();
			this.cacheLock = new object();
			this.scanTimer = new System.Timers.Timer();
			this.scanTimer.Elapsed += this.RunScansEventHandler;
			this.isScanning = false;
			this.invokeScan = false;
			this.lastScanDuration = TimeSpan.Zero;
			this.lastScanStartTime = DateTime.UtcNow;
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060009EB RID: 2539 RVA: 0x0002509C File Offset: 0x0002329C
		public virtual bool ScanningIsEnabled
		{
			get
			{
				bool enabled;
				lock (this.cacheLock)
				{
					enabled = this.scanTimer.Enabled;
				}
				return enabled;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060009EC RID: 2540 RVA: 0x000250E4 File Offset: 0x000232E4
		// (set) Token: 0x060009ED RID: 2541 RVA: 0x000250EC File Offset: 0x000232EC
		public Exception LastScanError { get; protected set; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060009EE RID: 2542 RVA: 0x000250F5 File Offset: 0x000232F5
		protected virtual string ComponentName
		{
			get
			{
				return "IssueCache";
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060009EF RID: 2543
		protected abstract TimeSpan FullScanFrequency { get; }

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060009F0 RID: 2544
		protected abstract int IssueLimit { get; }

		// Token: 0x060009F1 RID: 2545 RVA: 0x000250FC File Offset: 0x000232FC
		public void EnableScanning()
		{
			lock (this.cacheLock)
			{
				if (!this.scanTimer.Enabled)
				{
					this.scanTimer.Interval = this.FullScanFrequency.TotalMilliseconds;
					this.scanTimer.Start();
					this.InvokeScan();
				}
			}
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x00025170 File Offset: 0x00023370
		public void DisableScanning()
		{
			lock (this.cacheLock)
			{
				this.scanTimer.Stop();
			}
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x000251C0 File Offset: 0x000233C0
		public void InvokeScan()
		{
			lock (this.cacheLock)
			{
				this.invokeScan = true;
			}
			ThreadPool.QueueUserWorkItem(delegate(object obj)
			{
				this.RunScans();
			});
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x00025214 File Offset: 0x00023414
		string IDiagnosable.GetDiagnosticComponentName()
		{
			return this.ComponentName;
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x00025244 File Offset: 0x00023444
		XElement IDiagnosable.GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			XElement xelement = new XElement(this.ComponentName);
			SICDiagnosticArgument arguments = this.CreateDiagnosticArgumentParser();
			try
			{
				arguments.Initialize(parameters);
			}
			catch (DiagnosticArgumentException ex)
			{
				xelement.Add(new XElement("Error", "Encountered exception: " + ex.Message));
				xelement.Add(new XElement("Help", "Supported arguments: " + arguments.GetSupportedArguments()));
				return xelement;
			}
			List<ServiceIssue> list = null;
			if (arguments.HasArgument("invokescan"))
			{
				this.InvokeScan();
			}
			lock (this.cacheLock)
			{
				xelement.Add(new object[]
				{
					new XElement("ScanFrequency", this.FullScanFrequency.ToString()),
					new XElement("IssueLimit", this.IssueLimit),
					new XElement("IsScanning", this.isScanning),
					new XElement("IsEnabled", this.ScanningIsEnabled),
					new XElement("LastScanStartTime", this.lastScanStartTime),
					new XElement("LastScanDuration", this.lastScanDuration.TotalMilliseconds),
					new XElement("NumberOfIssues", this.issueCache.Count)
				});
				if (this.LastScanError != null)
				{
					xelement.Add(new object[]
					{
						new XElement("LastScanErrorName", this.LastScanError.GetType().Name),
						new XElement("LastScanErrorMessage", this.LastScanError.Message)
					});
				}
				if (arguments.HasArgument("issue"))
				{
					list = new List<ServiceIssue>(this.issueCache.Values);
				}
			}
			if (list != null)
			{
				int argumentOrDefault = arguments.GetArgumentOrDefault<int>("maxsize", list.Count);
				XElement xelement2 = new XElement("ServiceIssues");
				using (IEnumerator<ServiceIssue> enumerator = list.Take(argumentOrDefault).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ServiceIssue issue = enumerator.Current;
						xelement2.Add(arguments.RunDiagnosticOperation(() => issue.GetDiagnosticInfo(arguments)));
					}
				}
				xelement.Add(xelement2);
			}
			if (arguments.ArgumentCount == 0)
			{
				xelement.Add(new XElement("Help", "Supported arguments: " + arguments.GetSupportedArguments()));
			}
			return xelement;
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x000255C0 File Offset: 0x000237C0
		protected virtual SICDiagnosticArgument CreateDiagnosticArgumentParser()
		{
			return new SICDiagnosticArgument();
		}

		// Token: 0x060009F7 RID: 2551
		protected abstract ICollection<ServiceIssue> RunFullIssueScan();

		// Token: 0x060009F8 RID: 2552 RVA: 0x000255C8 File Offset: 0x000237C8
		private void UpdateCache(ICollection<ServiceIssue> issues)
		{
			Dictionary<string, ServiceIssue> dictionary = new Dictionary<string, ServiceIssue>();
			if (issues != null)
			{
				foreach (ServiceIssue serviceIssue in issues)
				{
					if (dictionary.Count >= this.IssueLimit)
					{
						break;
					}
					if (this.issueCache.ContainsKey(serviceIssue.IdentifierString))
					{
						serviceIssue.DeriveFromIssue(this.issueCache[serviceIssue.IdentifierString]);
					}
					dictionary[serviceIssue.IdentifierString] = serviceIssue;
				}
			}
			this.issueCache = dictionary;
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x00025660 File Offset: 0x00023860
		private void RunScansEventHandler(object source, ElapsedEventArgs e)
		{
			this.RunScans();
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x00025668 File Offset: 0x00023868
		private void RunScans()
		{
			lock (this.cacheLock)
			{
				if ((!this.ScanningIsEnabled && !this.invokeScan) || this.isScanning)
				{
					return;
				}
				this.isScanning = true;
				this.lastScanStartTime = DateTime.UtcNow;
				this.LastScanError = null;
			}
			ICollection<ServiceIssue> issues = null;
			Stopwatch stopwatch = Stopwatch.StartNew();
			try
			{
				issues = this.RunFullIssueScan();
			}
			catch (LocalizedException lastScanError)
			{
				this.LastScanError = lastScanError;
			}
			stopwatch.Stop();
			lock (this.cacheLock)
			{
				this.UpdateCache(issues);
				this.lastScanDuration = stopwatch.Elapsed;
				this.isScanning = false;
				this.invokeScan = false;
			}
		}

		// Token: 0x040006C5 RID: 1733
		private const string DiagnosticsComponentName = "IssueCache";

		// Token: 0x040006C6 RID: 1734
		private object cacheLock;

		// Token: 0x040006C7 RID: 1735
		private Dictionary<string, ServiceIssue> issueCache;

		// Token: 0x040006C8 RID: 1736
		private bool isScanning;

		// Token: 0x040006C9 RID: 1737
		private bool invokeScan;

		// Token: 0x040006CA RID: 1738
		private TimeSpan lastScanDuration;

		// Token: 0x040006CB RID: 1739
		private DateTime lastScanStartTime;

		// Token: 0x040006CC RID: 1740
		private System.Timers.Timer scanTimer;
	}
}
