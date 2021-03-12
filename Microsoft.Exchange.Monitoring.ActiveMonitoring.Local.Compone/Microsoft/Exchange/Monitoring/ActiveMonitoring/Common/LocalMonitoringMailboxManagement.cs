using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x02000564 RID: 1380
	internal class LocalMonitoringMailboxManagement
	{
		// Token: 0x060022A3 RID: 8867 RVA: 0x000D19DC File Offset: 0x000CFBDC
		private static X509Certificate2 GetLamConfigCert()
		{
			X509Store x509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
			X509Certificate2 result;
			try
			{
				x509Store.Open(OpenFlags.ReadOnly);
				X509Certificate2Collection x509Certificate2Collection = x509Store.Certificates.Find(X509FindType.FindBySubjectName, "lamconfig", true);
				if (x509Certificate2Collection == null || x509Certificate2Collection.Count < 1)
				{
					throw new Exception("Can't find 'lamconfig' certificate in the local machine's cert store. This is a setup failure that must not be ignored. This machine is not ready to go live!");
				}
				X509Certificate2 x509Certificate = null;
				foreach (X509Certificate2 x509Certificate2 in x509Certificate2Collection)
				{
					if (x509Certificate == null || x509Certificate.NotAfter < x509Certificate2.NotAfter)
					{
						x509Certificate = x509Certificate2;
					}
				}
				result = x509Certificate;
			}
			finally
			{
				x509Store.Close();
			}
			return result;
		}

		// Token: 0x060022A4 RID: 8868 RVA: 0x000D1A78 File Offset: 0x000CFC78
		private static string EncryptText(string plainText, X509Certificate2 cert)
		{
			RSACryptoServiceProvider rsacryptoServiceProvider = (RSACryptoServiceProvider)cert.PublicKey.Key;
			byte[] bytes = Encoding.UTF8.GetBytes(plainText);
			byte[] inArray;
			try
			{
				inArray = rsacryptoServiceProvider.Encrypt(bytes, true);
			}
			catch (CryptographicException ex)
			{
				if (ex.HResult == -2146893820)
				{
					throw new Exception("Can't encrypt plaintext longer than 245 characters with a 2048-bit certificate.", ex);
				}
				throw;
			}
			return Convert.ToBase64String(inArray);
		}

		// Token: 0x060022A5 RID: 8869 RVA: 0x000D1AE4 File Offset: 0x000CFCE4
		private static string DecryptText(string cipherText, X509Certificate2 cert)
		{
			RSACryptoServiceProvider rsacryptoServiceProvider;
			try
			{
				rsacryptoServiceProvider = (RSACryptoServiceProvider)cert.PrivateKey;
			}
			catch (CryptographicException innerException)
			{
				throw new Exception("Can't decrypt the ciphertext. The private key is installed but the process is not running elevated.", innerException);
			}
			if (rsacryptoServiceProvider == null)
			{
				throw new Exception("Can't decrypt the ciphertext. The private key is not installed.");
			}
			byte[] rgb = Convert.FromBase64String(cipherText);
			byte[] bytes = rsacryptoServiceProvider.Decrypt(rgb, true);
			return Encoding.UTF8.GetString(bytes);
		}

		// Token: 0x060022A6 RID: 8870 RVA: 0x000D1B4C File Offset: 0x000CFD4C
		private static string GetStaticPasswordWorker(TracingContext traceContext)
		{
			X509Certificate2 x509Certificate = null;
			try
			{
				x509Certificate = LocalMonitoringMailboxManagement.GetLamConfigCert();
			}
			catch (Exception ex)
			{
				WTFDiagnostics.TraceError<string>(ExTraceGlobals.CommonComponentsTracer, traceContext, "LocalMonitoringMailboxManagement.GetStaticPassword: Could not find valid LAMConfig certificate. Error: {0}", ex.Message, null, "GetStaticPasswordWorker", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\LocalMonitoringMailboxManagement.cs", 177);
				return null;
			}
			string thumbprint = x509Certificate.Thumbprint;
			string text = "LAMMailboxPassword" + thumbprint;
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, traceContext, "LocalMonitoringMailboxManagement.GetStaticPassword: Attempting to locate an encrypted password named '{0}'", text, null, "GetStaticPasswordWorker", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\LocalMonitoringMailboxManagement.cs", 190);
			OverridableSetting<string> os = new OverridableSetting<string>(text, null, LocalMonitoringMailboxManagement.NoOpConverter, true);
			if (string.IsNullOrEmpty(os))
			{
				WTFDiagnostics.TraceWarning<string>(ExTraceGlobals.CommonComponentsTracer, traceContext, "LocalMonitoringMailboxManagement.GetStaticPassword: Could not find a value for '{0}' in this process's app config or in this forest's GlobalMonitoringOverrides", text, null, "GetStaticPasswordWorker", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\LocalMonitoringMailboxManagement.cs", 196);
				return null;
			}
			string text2;
			try
			{
				text2 = LocalMonitoringMailboxManagement.DecryptText(os, x509Certificate);
			}
			catch (Exception ex2)
			{
				WTFDiagnostics.TraceError<string>(ExTraceGlobals.CommonComponentsTracer, traceContext, "LocalMonitoringMailboxManagement.GetStaticPassword: Error decrypting static password: {0}", ex2.Message, null, "GetStaticPasswordWorker", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\LocalMonitoringMailboxManagement.cs", 208);
				return null;
			}
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, traceContext, "LocalMonitoringMailboxManagement.GetStaticPassword: Returning static password '{0}'", text2, null, "GetStaticPasswordWorker", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\LocalMonitoringMailboxManagement.cs", 212);
			return text2;
		}

		// Token: 0x060022A7 RID: 8871 RVA: 0x000D1C88 File Offset: 0x000CFE88
		internal static string GetStaticPassword(TracingContext traceContext)
		{
			string text = LocalMonitoringMailboxManagement.GetStaticPasswordWorker(traceContext);
			if (string.IsNullOrEmpty(text))
			{
				text = Settings.StaticMonitoringPassword;
				if (string.IsNullOrEmpty(text))
				{
					WTFDiagnostics.TraceWarning(ExTraceGlobals.CommonComponentsTracer, traceContext, "LocalMonitoringMailboxManagement.GetStaticPassword: Could not find StaticMonitoringPassword setting in this forest's GlobalMonitoringOverrides", null, "GetStaticPassword", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\LocalMonitoringMailboxManagement.cs", 233);
					return null;
				}
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, traceContext, "LocalMonitoringMailboxManagement.GetStaticPassword: Falling back to unencrypted StaticMonitoringPassword: {0}", text, null, "GetStaticPassword", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\LocalMonitoringMailboxManagement.cs", 237);
			}
			return text;
		}

		// Token: 0x040018E8 RID: 6376
		private const string CertSubjectSearchString = "lamconfig";

		// Token: 0x040018E9 RID: 6377
		private static readonly Func<string, string> NoOpConverter = (string s) => s;
	}
}
