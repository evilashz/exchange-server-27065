using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.EventLogs;

namespace Microsoft.Exchange.Services
{
	// Token: 0x0200001D RID: 29
	internal sealed class ExchangeCertificateChecker : IDisposable
	{
		// Token: 0x060001A8 RID: 424 RVA: 0x000084E0 File Offset: 0x000066E0
		internal static void Initialize(int checkerIntervalInMinutes)
		{
			ExchangeCertificateChecker.Singleton = new ExchangeCertificateChecker(checkerIntervalInMinutes);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x000084ED File Offset: 0x000066ED
		internal static void Terminate()
		{
			if (ExchangeCertificateChecker.Singleton != null)
			{
				ExchangeCertificateChecker.Singleton.Dispose();
				ExchangeCertificateChecker.Singleton = null;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00008506 File Offset: 0x00006706
		// (set) Token: 0x060001AB RID: 427 RVA: 0x0000850D File Offset: 0x0000670D
		internal static ExchangeCertificateChecker Singleton { get; private set; }

		// Token: 0x060001AC RID: 428 RVA: 0x00008518 File Offset: 0x00006718
		private ExchangeCertificateChecker(int checkerIntervalInMinutes)
		{
			int period = 60000 * checkerIntervalInMinutes;
			this.certificateCheckerTimer = new Timer(new TimerCallback(this.CheckForCertificateExpiration), this, 0, period);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00008558 File Offset: 0x00006758
		private void CheckForCertificateExpiration(object state)
		{
			DateTime utcNow = DateTime.UtcNow;
			DateTime t = utcNow.AddDays(7.0);
			DateTime t2 = utcNow.AddMonths(2);
			List<X509Certificate2> localCertificates = this.GetLocalCertificates();
			List<X509Certificate2> list = new List<X509Certificate2>();
			List<X509Certificate2> list2 = new List<X509Certificate2>();
			List<X509Certificate2> list3 = new List<X509Certificate2>();
			foreach (X509Certificate2 x509Certificate in localCertificates)
			{
				if (DateTime.Compare(x509Certificate.NotAfter, utcNow) <= 0)
				{
					list3.Add(x509Certificate);
				}
				else if (DateTime.Compare(x509Certificate.NotAfter, t) <= 0)
				{
					list2.Add(x509Certificate);
				}
				else if (DateTime.Compare(x509Certificate.NotAfter, t2) <= 0)
				{
					list.Add(x509Certificate);
				}
			}
			if (list3.Count<X509Certificate2>() > 0)
			{
				this.LogCertificateExpirationEvents(ServicesEventLogConstants.Tuple_CertificateExpired, list3, true);
			}
			if (list2.Count<X509Certificate2>() > 0)
			{
				this.LogCertificateExpirationEvents(ServicesEventLogConstants.Tuple_CertificateExpiresVerySoon, list2, false);
			}
			if (list.Count<X509Certificate2>() > 0)
			{
				this.LogCertificateExpirationEvents(ServicesEventLogConstants.Tuple_CertificateExpiresSoon, list, false);
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00008674 File Offset: 0x00006874
		private List<X509Certificate2> GetLocalCertificates()
		{
			List<X509Certificate2> list = new List<X509Certificate2>();
			Dictionary<string, X509Certificate2> dictionary = new Dictionary<string, X509Certificate2>();
			X509Store x509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
			try
			{
				x509Store.Open(OpenFlags.OpenExistingOnly);
			}
			catch (CryptographicException)
			{
				x509Store = null;
			}
			X509Store x509Store2 = new X509Store("REQUEST", StoreLocation.LocalMachine);
			try
			{
				x509Store2.Open(OpenFlags.OpenExistingOnly);
			}
			catch (CryptographicException)
			{
				x509Store2 = null;
			}
			try
			{
				if (x509Store != null)
				{
					foreach (X509Certificate2 certificate in x509Store.Certificates)
					{
						this.AddLatestCertificate(dictionary, certificate);
					}
				}
				if (x509Store2 != null)
				{
					foreach (X509Certificate2 x509Certificate in x509Store2.Certificates)
					{
						string value = CertificateEnroller.ReadPkcs10Request(x509Certificate);
						if (!string.IsNullOrEmpty(value))
						{
							list.Add(x509Certificate);
						}
					}
				}
			}
			catch (LocalizedException arg)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceError<LocalizedException>(0L, "[ExchangeCertificateHelper.GetLocalCertificates -- Error while reading certificates: {0}.", arg);
			}
			catch (CryptographicException arg2)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceError<CryptographicException>(0L, "[ExchangeCertificateHelper.GetLocalCertificates -- Error while reading certificates: {0}.", arg2);
			}
			finally
			{
				if (x509Store != null)
				{
					x509Store.Close();
				}
				if (x509Store2 != null)
				{
					x509Store2.Close();
				}
			}
			list.AddRange(dictionary.Values.ToList<X509Certificate2>());
			return list;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000087C0 File Offset: 0x000069C0
		private void LogCertificateExpirationEvents(ExEventLog.EventTuple tuple, IEnumerable<X509Certificate2> certificates, bool expired)
		{
			string traceFormat = "Certificate {0} expire" + (expired ? "d" : "s") + " on {1}.";
			foreach (X509Certificate2 x509Certificate in certificates)
			{
				ServiceDiagnostics.LogEventWithTrace(tuple, null, ExTraceGlobals.CommonAlgorithmTracer, null, traceFormat, new object[]
				{
					x509Certificate.ToString(false),
					x509Certificate.NotAfter
				});
			}
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00008850 File Offset: 0x00006A50
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000885C File Offset: 0x00006A5C
		private void Dispose(bool isDisposing)
		{
			ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<bool, bool>((long)this.GetHashCode(), "ExchangeCertificateChecker.Dispose called. IsDisposed: {0} IsDisposing: {1}", this.isDisposed, isDisposing);
			lock (this.lockObject)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<bool>((long)this.GetHashCode(), "ExchangeCertificateChecker.Dispose. After lock.  IsDisposed: {0}", this.isDisposed);
				if (!this.isDisposed)
				{
					if (isDisposing && this.certificateCheckerTimer != null)
					{
						this.certificateCheckerTimer.Dispose();
					}
					this.isDisposed = true;
				}
			}
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x000088F4 File Offset: 0x00006AF4
		private void AddLatestCertificate(Dictionary<string, X509Certificate2> dictionary, X509Certificate2 certificate)
		{
			if (dictionary == null || certificate == null)
			{
				return;
			}
			if (string.IsNullOrEmpty(certificate.Subject))
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceError<string>(0L, "[ExchangeCertificateHelper.AddLatestCertificate -- certificate.Subject is null. Thumbprint: {0}.", certificate.Thumbprint ?? "<NULL>");
				return;
			}
			if (dictionary.Keys.Contains(certificate.Subject))
			{
				if (dictionary[certificate.Subject].NotAfter < certificate.NotAfter)
				{
					dictionary[certificate.Subject] = certificate;
					return;
				}
			}
			else
			{
				dictionary.Add(certificate.Subject, certificate);
			}
		}

		// Token: 0x0400015A RID: 346
		private const string RequestStoreName = "REQUEST";

		// Token: 0x0400015B RID: 347
		private Timer certificateCheckerTimer;

		// Token: 0x0400015C RID: 348
		private bool isDisposed;

		// Token: 0x0400015D RID: 349
		private object lockObject = new object();
	}
}
