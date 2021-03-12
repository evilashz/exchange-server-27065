using System;
using System.Net.Security;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200001A RID: 26
	internal class ApnsCertProvider
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x0000421E File Offset: 0x0000241E
		public ApnsCertProvider(ApnsCertStore store, string appId) : this(store, false, appId)
		{
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004229 File Offset: 0x00002429
		public ApnsCertProvider(ApnsCertStore store, ITracer tracer, string appId) : this(store, false, tracer, appId)
		{
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004235 File Offset: 0x00002435
		internal ApnsCertProvider(ApnsCertStore store, bool ignoreCertificateErrors, string appId) : this(store, ignoreCertificateErrors, ExTraceGlobals.ApnsPublisherTracer, appId)
		{
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004245 File Offset: 0x00002445
		internal ApnsCertProvider(ApnsCertStore store, bool ignoreCertificateErrors, ITracer tracer, string appId)
		{
			this.Store = store;
			this.IgnoreCertificateErrors = ignoreCertificateErrors;
			this.Tracer = tracer;
			this.AppId = appId;
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x0000426A File Offset: 0x0000246A
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00004272 File Offset: 0x00002472
		private string AppId { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x0000427B File Offset: 0x0000247B
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00004283 File Offset: 0x00002483
		private ApnsCertStore Store { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x0000428C File Offset: 0x0000248C
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00004294 File Offset: 0x00002494
		private ITracer Tracer { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000EA RID: 234 RVA: 0x0000429D File Offset: 0x0000249D
		// (set) Token: 0x060000EB RID: 235 RVA: 0x000042A5 File Offset: 0x000024A5
		private bool IgnoreCertificateErrors { get; set; }

		// Token: 0x060000EC RID: 236 RVA: 0x000042B0 File Offset: 0x000024B0
		public virtual X509Certificate2 LoadCertificate(string thumbprint, string altThumbprint = null)
		{
			X509Certificate2 x509Certificate = this.LoadCertificate(thumbprint);
			if (x509Certificate == null && !string.IsNullOrEmpty(altThumbprint))
			{
				x509Certificate = this.LoadCertificate(altThumbprint);
			}
			if (x509Certificate == null)
			{
				PushNotificationsCrimsonEvents.ApnsCertNotFound.Log<string, string, string>(string.Empty, thumbprint, string.Empty);
				throw this.HandleLoadException(thumbprint, "ApnsCertPresent", new ApnsCertificateException(Strings.ApnsCertificateNotFound(thumbprint)));
			}
			return x509Certificate;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000430C File Offset: 0x0000250C
		public bool ValidateCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			if (sslPolicyErrors == SslPolicyErrors.None)
			{
				PushNotificationsMonitoring.PublishSuccessNotification("ApnsCertValidation", this.AppId);
				return true;
			}
			string text = "<null>";
			string text2 = "<null>";
			string text3 = sslPolicyErrors.ToString();
			if (certificate != null && !string.IsNullOrEmpty(certificate.Subject))
			{
				text = certificate.Subject;
			}
			X509Certificate2 x509Certificate = certificate as X509Certificate2;
			if (x509Certificate != null)
			{
				text2 = x509Certificate.Thumbprint;
			}
			if (!this.IgnoreCertificateErrors)
			{
				this.Tracer.TraceError((long)this.GetHashCode(), string.Format("[ValidateCertificate] Validation {0} for certificate '{1}', thumbprint:'{2}', error:'{3}'", new object[]
				{
					this.IgnoreCertificateErrors ? "ignored" : "failed",
					text,
					text2,
					text3
				}));
				PushNotificationsCrimsonEvents.ApnsCertValidationFailed.Log<X509Certificate, string, string>(certificate, text2, text3);
				PushNotificationsMonitoring.PublishFailureNotification("ApnsCertValidation", this.AppId, "");
			}
			return this.IgnoreCertificateErrors;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000043F0 File Offset: 0x000025F0
		private X509Certificate2 LoadCertificate(string thumbprint)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("thumbprint", thumbprint);
			X509Certificate2 result;
			try
			{
				this.Store.Open(OpenFlags.ReadOnly);
				X509Certificate2Collection x509Certificate2Collection = this.Store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);
				if (x509Certificate2Collection == null || x509Certificate2Collection.Count <= 0)
				{
					result = null;
				}
				else
				{
					PushNotificationsMonitoring.PublishSuccessNotification("ApnsCertPresent", this.AppId);
					X509Certificate2 x509Certificate = x509Certificate2Collection[0];
					try
					{
						x509Certificate.PrivateKey.GetHashCode();
					}
					catch (NullReferenceException)
					{
						PushNotificationsCrimsonEvents.ApnsCertPrivateKeyError.Log<string, string, string>(x509Certificate.FriendlyName, thumbprint, string.Empty);
						throw this.HandleLoadException(thumbprint, "ApnsCertPrivateKey", new ApnsCertificateException(Strings.ApnsCertificatePrivateKeyError(x509Certificate.FriendlyName, thumbprint)));
					}
					PushNotificationsMonitoring.PublishSuccessNotification("ApnsCertPrivateKey", this.AppId);
					this.Tracer.TraceDebug<string, string>((long)this.GetHashCode(), "[LoadCertificate] Certificate '{0}' found for thumbprint '{1}'", x509Certificate.FriendlyName, thumbprint);
					PushNotificationsMonitoring.PublishSuccessNotification("ApnsCertLoaded", this.AppId);
					result = x509Certificate;
				}
			}
			catch (CryptographicException exception)
			{
				PushNotificationsCrimsonEvents.ApnsCertException.Log<string, string>(thumbprint, exception.ToTraceString());
				throw this.HandleLoadException(thumbprint, exception);
			}
			catch (SecurityException exception2)
			{
				PushNotificationsCrimsonEvents.ApnsCertException.Log<string, string>(thumbprint, exception2.ToTraceString());
				throw this.HandleLoadException(thumbprint, exception2);
			}
			finally
			{
				this.Store.Close();
			}
			return result;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004584 File Offset: 0x00002784
		private Exception HandleLoadException(string thumbprint, Exception exception)
		{
			PushNotificationsCrimsonEvents.ApnsCertException.Log<string, string>(thumbprint, exception.ToTraceString());
			return this.HandleLoadException(thumbprint, "ApnsCertLoaded", new ApnsCertificateException(Strings.ApnsCertificateExternalException(thumbprint, exception.Message), exception));
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000045B5 File Offset: 0x000027B5
		private Exception HandleLoadException(string thumbprint, string monitoringKey, ApnsCertificateException exception)
		{
			this.Tracer.TraceError<string, string>((long)this.GetHashCode(), "[LoadCertificate] An error occurred loading certificate for thumbprint '{0}': {1}", thumbprint, exception.ToTraceString());
			PushNotificationsMonitoring.PublishFailureNotification(monitoringKey, this.AppId, "");
			return exception;
		}
	}
}
