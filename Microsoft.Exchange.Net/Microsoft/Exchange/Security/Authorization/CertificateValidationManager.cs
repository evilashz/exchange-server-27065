using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Diagnostics.Components.Common;

namespace Microsoft.Exchange.Security.Authorization
{
	// Token: 0x02000641 RID: 1601
	internal static class CertificateValidationManager
	{
		// Token: 0x06001CFE RID: 7422 RVA: 0x00034644 File Offset: 0x00032844
		public static void RegisterCallback(string componentId, RemoteCertificateValidationCallback callback)
		{
			CertificateValidationManager.RegisterCallback(componentId, callback, false);
		}

		// Token: 0x06001CFF RID: 7423 RVA: 0x00034650 File Offset: 0x00032850
		public static void RegisterCallback(string componentId, RemoteCertificateValidationCallback callback, bool forceCallback)
		{
			if (string.IsNullOrEmpty(componentId))
			{
				throw new ArgumentException("componentId");
			}
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			ExTraceGlobals.CertificateValidationTracer.TraceDebug(0L, "Entering CertificateValidationManager.RegisterCallback");
			lock (CertificateValidationManager.callbackRegisterLock)
			{
				if (CertificateValidationManager.callbackTable.ContainsKey(componentId))
				{
					ExTraceGlobals.CertificateValidationTracer.TraceDebug<string>(0L, "Callback already registered.  ComponentId={0}.", componentId);
				}
				else
				{
					Dictionary<string, CertificateValidationManager.CallbackPair> dictionary = new Dictionary<string, CertificateValidationManager.CallbackPair>(CertificateValidationManager.callbackTable);
					dictionary[componentId] = new CertificateValidationManager.CallbackPair(callback, forceCallback);
					CertificateValidationManager.callbackTable = dictionary;
					if (CertificateValidationManager.callbackTable.Count == 1)
					{
						RemoteCertificateValidationCallback remoteCertificateValidationCallback = new RemoteCertificateValidationCallback(CertificateValidationManager.MainCertificateValidationCallback);
						ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)Delegate.Remove(ServicePointManager.ServerCertificateValidationCallback, remoteCertificateValidationCallback);
						ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)Delegate.Combine(ServicePointManager.ServerCertificateValidationCallback, remoteCertificateValidationCallback);
					}
				}
			}
		}

		// Token: 0x06001D00 RID: 7424 RVA: 0x00034740 File Offset: 0x00032940
		public static void SetComponentId(HttpWebRequest request, string componentId)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			if (string.IsNullOrEmpty(componentId))
			{
				throw new ArgumentException("componentId");
			}
			request.Headers[CertificateValidationManager.ComponentIdHeaderName] = componentId;
		}

		// Token: 0x06001D01 RID: 7425 RVA: 0x00034774 File Offset: 0x00032974
		public static void SetComponentId(WebHeaderCollection headers, string componentId)
		{
			if (headers == null)
			{
				throw new ArgumentNullException("headers");
			}
			if (string.IsNullOrEmpty(componentId))
			{
				throw new ArgumentException("componentId");
			}
			headers[CertificateValidationManager.ComponentIdHeaderName] = componentId;
		}

		// Token: 0x06001D02 RID: 7426 RVA: 0x000347A3 File Offset: 0x000329A3
		public static string GenerateComponentIdQueryString(string componentId)
		{
			return string.Format("?{0}={1}", CertificateValidationManager.ComponentIdHeaderName, componentId);
		}

		// Token: 0x06001D03 RID: 7427 RVA: 0x000347B8 File Offset: 0x000329B8
		public static bool MainCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			ExTraceGlobals.CertificateValidationTracer.TraceDebug(0L, "Entering CertificateValidation.CommonCertificateValidationCallback");
			HttpWebRequest httpWebRequest = sender as HttpWebRequest;
			string componentId;
			if (httpWebRequest != null)
			{
				ExTraceGlobals.CertificateValidationTracer.TraceDebug(0L, "sender is HttpWebRequest");
				componentId = CertificateValidationManager.GetComponentId(httpWebRequest);
			}
			else
			{
				CertificateValidationManager.IComponent component = sender as CertificateValidationManager.IComponent;
				if (component != null)
				{
					ExTraceGlobals.CertificateValidationTracer.TraceDebug(0L, "sender is IComponent");
					componentId = component.GetComponentId();
				}
				else
				{
					if (!CertificateValidationManager.IsSslError(sslPolicyErrors))
					{
						return true;
					}
					string text = sender.GetType().ToString();
					if (string.IsNullOrEmpty(text))
					{
						text = "<Unknown>";
					}
					ExTraceGlobals.CertificateValidationTracer.TraceDebug<string>(0L, "Unrecognized type passed as sender. Type={0}", text);
					return CertificateValidationManager.DefaultValidateSslPolicyErrors(sslPolicyErrors);
				}
			}
			if (string.IsNullOrEmpty(componentId))
			{
				ExTraceGlobals.CertificateValidationTracer.TraceDebug(0L, "Component id is not set or empty");
				return CertificateValidationManager.DefaultValidateSslPolicyErrors(sslPolicyErrors);
			}
			ExTraceGlobals.CertificateValidationTracer.TraceDebug<string>(0L, "Looking up callback for component \"{0}\"...", componentId);
			CertificateValidationManager.CallbackPair callbackPair = null;
			if (!CertificateValidationManager.callbackTable.TryGetValue(componentId, out callbackPair))
			{
				return CertificateValidationManager.DefaultValidateSslPolicyErrors(sslPolicyErrors);
			}
			if (callbackPair == null)
			{
				ExTraceGlobals.CertificateValidationTracer.TraceDebug(0L, "Callback not found, could be a missing call to RegisterCallback");
				throw new InvalidOperationException(string.Format("Couldn not find callback for component with id = \"{0}\"", componentId));
			}
			if (!CertificateValidationManager.IsSslError(sslPolicyErrors) && !callbackPair.ForceCallback)
			{
				return true;
			}
			ExTraceGlobals.CertificateValidationTracer.TraceDebug<string>(0L, "Executing callback...", componentId);
			return callbackPair.Callback(sender, certificate, chain, sslPolicyErrors);
		}

		// Token: 0x06001D04 RID: 7428 RVA: 0x00034903 File Offset: 0x00032B03
		private static bool IsSslError(SslPolicyErrors sslPolicyErrors)
		{
			if (sslPolicyErrors == SslPolicyErrors.None)
			{
				ExTraceGlobals.CertificateValidationTracer.TraceDebug(0L, "No SSL policy errors found, exiting...");
				return false;
			}
			return true;
		}

		// Token: 0x06001D05 RID: 7429 RVA: 0x0003491C File Offset: 0x00032B1C
		private static bool DefaultValidateSslPolicyErrors(SslPolicyErrors sslPolicyErrors)
		{
			return sslPolicyErrors == SslPolicyErrors.None;
		}

		// Token: 0x06001D06 RID: 7430 RVA: 0x00034924 File Offset: 0x00032B24
		private static string GetComponentId(HttpWebRequest request)
		{
			string text = request.Headers[CertificateValidationManager.ComponentIdHeaderName];
			if (!string.IsNullOrEmpty(text))
			{
				return text;
			}
			if (request.RequestUri == null || request.RequestUri.Query == null)
			{
				return text;
			}
			Match match = CertificateValidationManager.ComponentIdRegEx.Match(request.RequestUri.Query);
			if (!match.Success)
			{
				return text;
			}
			return match.Value.Substring(CertificateValidationManager.ComponentIdQueryString.Length);
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06001D07 RID: 7431 RVA: 0x000349A0 File Offset: 0x00032BA0
		private static Regex ComponentIdRegEx
		{
			get
			{
				if (CertificateValidationManager.componentIdRegEx == null)
				{
					lock (CertificateValidationManager.lockInitComponentIdRegEx)
					{
						if (CertificateValidationManager.componentIdRegEx == null)
						{
							CertificateValidationManager.componentIdRegEx = new Regex(CertificateValidationManager.ComponentIdQueryStringPattern, RegexOptions.Compiled);
						}
					}
				}
				return CertificateValidationManager.componentIdRegEx;
			}
		}

		// Token: 0x04001D35 RID: 7477
		public static readonly string ComponentIdHeaderName = "X-ExCompId";

		// Token: 0x04001D36 RID: 7478
		public static readonly string ComponentIdQueryString = string.Format("?{0}=", CertificateValidationManager.ComponentIdHeaderName);

		// Token: 0x04001D37 RID: 7479
		private static readonly string ComponentIdQueryStringPattern = string.Format("\\{0}[\\w]+", CertificateValidationManager.ComponentIdQueryString);

		// Token: 0x04001D38 RID: 7480
		private static Regex componentIdRegEx = null;

		// Token: 0x04001D39 RID: 7481
		private static readonly object lockInitComponentIdRegEx = new object();

		// Token: 0x04001D3A RID: 7482
		private static object callbackRegisterLock = new object();

		// Token: 0x04001D3B RID: 7483
		private static Dictionary<string, CertificateValidationManager.CallbackPair> callbackTable = new Dictionary<string, CertificateValidationManager.CallbackPair>();

		// Token: 0x02000642 RID: 1602
		public interface IComponent
		{
			// Token: 0x06001D09 RID: 7433
			string GetComponentId();
		}

		// Token: 0x02000643 RID: 1603
		private class CallbackPair
		{
			// Token: 0x170007EC RID: 2028
			// (get) Token: 0x06001D0A RID: 7434 RVA: 0x00034A5F File Offset: 0x00032C5F
			// (set) Token: 0x06001D0B RID: 7435 RVA: 0x00034A67 File Offset: 0x00032C67
			public RemoteCertificateValidationCallback Callback { get; private set; }

			// Token: 0x170007ED RID: 2029
			// (get) Token: 0x06001D0C RID: 7436 RVA: 0x00034A70 File Offset: 0x00032C70
			// (set) Token: 0x06001D0D RID: 7437 RVA: 0x00034A78 File Offset: 0x00032C78
			public bool ForceCallback { get; private set; }

			// Token: 0x06001D0E RID: 7438 RVA: 0x00034A81 File Offset: 0x00032C81
			public CallbackPair(RemoteCertificateValidationCallback callback, bool forceCallback)
			{
				this.Callback = callback;
				this.ForceCallback = forceCallback;
			}
		}
	}
}
