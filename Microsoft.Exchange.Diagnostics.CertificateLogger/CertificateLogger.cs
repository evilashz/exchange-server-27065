using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Timers;
using Microsoft.Exchange.Diagnostics.Service.Common;
using Microsoft.Exchange.LogAnalyzer.Extensions.CertificateLog;

namespace Microsoft.Exchange.Diagnostics.CertificateLogger
{
	// Token: 0x02000003 RID: 3
	public sealed class CertificateLogger : IDisposable
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000022C0 File Offset: 0x000004C0
		public CertificateLogger(string logDirectory, TimeSpan monitorInterval, long maxDirectorySize, long maxFileSize, int maxBufferSize, TimeSpan flushInterval)
		{
			if (monitorInterval.TotalMilliseconds < 0.0 || monitorInterval.TotalMilliseconds > 2147483647.0)
			{
				throw new ArgumentOutOfRangeException("monitorInterval");
			}
			this.log = new CertificateLog(logDirectory, maxDirectorySize, maxFileSize, maxBufferSize, flushInterval);
			this.refreshInterval = monitorInterval;
			if (monitorInterval.TotalMilliseconds > 0.0)
			{
				this.refreshTimer = new Timer(300000.0);
				this.refreshTimer.Elapsed += this.TimerEvent;
				this.refreshTimer.Start();
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002364 File Offset: 0x00000564
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002374 File Offset: 0x00000574
		internal void PollCertificateStores()
		{
			IEnumerable<X509Store> certificateStores = this.GetCertificateStores();
			foreach (X509Store store in certificateStores)
			{
				this.LogCertificatesInStore(store);
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000023C4 File Offset: 0x000005C4
		internal void LogCertificatesInStore(X509Store store)
		{
			IList<CertificateInformation> certificatesFromStore = this.GetCertificatesFromStore(store);
			foreach (CertificateInformation info in certificatesFromStore)
			{
				this.log.LogCertificateInformation(info);
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000241C File Offset: 0x0000061C
		internal void TimerEvent(object sender, ElapsedEventArgs e)
		{
			if (this.refreshTimer.Interval != this.refreshInterval.TotalMilliseconds)
			{
				this.refreshTimer.Interval = this.refreshInterval.TotalMilliseconds;
			}
			this.PollCertificateStores();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002464 File Offset: 0x00000664
		private void Dispose(bool disposing)
		{
			if (!this.disposed && disposing)
			{
				if (this.refreshTimer != null)
				{
					this.refreshTimer.Stop();
					this.refreshTimer.Enabled = false;
					this.refreshTimer.Dispose();
				}
				if (this.log != null)
				{
					this.log.Dispose();
				}
				this.disposed = true;
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000024C0 File Offset: 0x000006C0
		private IEnumerable<X509Store> GetCertificateStores()
		{
			List<X509Store> list = new List<X509Store>();
			foreach (object obj in Enum.GetValues(typeof(StoreName)))
			{
				StoreName storeName = (StoreName)obj;
				list.Add(new X509Store(storeName, StoreLocation.LocalMachine));
			}
			return list;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002548 File Offset: 0x00000748
		private IList<CertificateInformation> GetCertificatesFromStore(X509Store store)
		{
			if (store == null)
			{
				throw new ArgumentNullException("store");
			}
			List<CertificateInformation> list = new List<CertificateInformation>();
			try
			{
				store.Open(OpenFlags.OpenExistingOnly);
				list.AddRange(from X509Certificate2 certificate in store.Certificates
				select new CertificateInformation(certificate, store));
			}
			catch (CryptographicException ex)
			{
				Logger.LogErrorMessage("The following exception occured: " + ex.Message, new object[0]);
			}
			finally
			{
				store.Close();
			}
			return list;
		}

		// Token: 0x04000005 RID: 5
		private readonly TimeSpan refreshInterval;

		// Token: 0x04000006 RID: 6
		private readonly Timer refreshTimer;

		// Token: 0x04000007 RID: 7
		private readonly CertificateLog log;

		// Token: 0x04000008 RID: 8
		private bool disposed;
	}
}
