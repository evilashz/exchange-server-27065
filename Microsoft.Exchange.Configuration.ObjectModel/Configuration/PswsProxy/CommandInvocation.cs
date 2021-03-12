using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Net;
using System.Net.Security;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Exchange.Diagnostics.Components.Configuration.Core;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Configuration.PswsProxy
{
	// Token: 0x020000CD RID: 205
	internal class CommandInvocation
	{
		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x0001BE9C File Offset: 0x0001A09C
		// (set) Token: 0x06000793 RID: 1939 RVA: 0x0001BEA3 File Offset: 0x0001A0A3
		internal static IWebRequestSender RequestSender { get; set; }

		// Token: 0x06000794 RID: 1940 RVA: 0x0001BEAB File Offset: 0x0001A0AB
		static CommandInvocation()
		{
			CertificateValidationManager.RegisterCallback("CommandInvcocation", new RemoteCertificateValidationCallback(CommandInvocation.CertificateNoValidationCallback));
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0001BEEC File Offset: 0x0001A0EC
		internal static IEnumerable<PSObject> Invoke(Guid cmdletUniqueId, string pswsServiceUri, string cmdletCommand, NetworkCredential credential, NameValueCollection headersTobeAdd, TypeTable typeTable)
		{
			CommandInvocation.CheckArgumentNull("pswsServiceUri", pswsServiceUri);
			CommandInvocation.CheckArgumentNull("cmdletCommand", cmdletCommand);
			CommandInvocation.CheckArgumentNull("credential", credential);
			int tickCount = Environment.TickCount;
			string value;
			string value2;
			ResponseContent responseContent = CommandInvocation.CreateRequest(pswsServiceUri, cmdletCommand, credential, headersTobeAdd, out value, out value2);
			while (responseContent.Status == ExecutionStatus.Executing)
			{
				int num = Environment.TickCount - tickCount;
				if (num > 600000)
				{
					throw new PswsProxyException(Strings.PswsInvocationTimout(600000));
				}
				Thread.Sleep(1000);
				responseContent = CommandInvocation.ResumeRequest(pswsServiceUri, responseContent.Id, credential, headersTobeAdd, out value2);
			}
			CmdletLogger.SafeAppendGenericInfo(cmdletUniqueId, "TicksElapsed", (Environment.TickCount - tickCount).ToString());
			if (responseContent.Status == ExecutionStatus.Error)
			{
				CmdletLogger.SafeAppendGenericInfo(cmdletUniqueId, "RequestXml", value);
				CmdletLogger.SafeAppendGenericInfo(cmdletUniqueId, "ResponseXml", value2);
				StringBuilder stringBuilder = new StringBuilder();
				if (headersTobeAdd != null)
				{
					foreach (string text in headersTobeAdd.AllKeys)
					{
						stringBuilder.Append(text);
						stringBuilder.Append(':');
						stringBuilder.Append(headersTobeAdd[text]);
						stringBuilder.Append(' ');
					}
				}
				CmdletLogger.SafeAppendGenericInfo(cmdletUniqueId, "RequestHeaders", stringBuilder.ToString());
				throw new PswsProxyCmdletException(responseContent.Error.Exception);
			}
			return ObjectTransfer.GetResultObjects(responseContent.OutputXml, typeTable);
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x0001C064 File Offset: 0x0001A264
		private static ResponseContent CreateRequest(string pswsServiceUri, string cmdletCommand, NetworkCredential credential, NameValueCollection headersTobeAdd, out string requestXml, out string responseXml)
		{
			string requestUri = string.Format("{0}/{1}/{2}", pswsServiceUri, "Service.svc", "CommandInvocations");
			requestXml = string.Format("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?>\r\n                    <entry xmlns:d=\"http://schemas.microsoft.com/ado/2007/08/dataservices\"\r\n                        xmlns:m=\"http://schemas.microsoft.com/ado/2007/08/dataservices/metadata\"\r\n                        xmlns=\"http://www.w3.org/2005/Atom\">\r\n                        <content type=\"application/xml\">\r\n                            <m:properties>\r\n                                <d:Command>{0}</d:Command>\r\n                                <d:OutputFormat>{1}</d:OutputFormat>\r\n                                <d:WaitMsec m:type=\"Edm.Int32\">{2}</d:WaitMsec>\r\n                            </m:properties>\r\n                        </content>\r\n                    </entry>", SecurityElement.Escape(cmdletCommand), "ExchangeXml", 30000);
			NameValueCollection headers = CommandInvocation.PrepareRequestHeaders(requestUri, headersTobeAdd);
			ExTraceGlobals.InstrumentationTracer.Information<string>(0L, "CommandINvocation request xml : {0}", requestXml);
			ExTraceGlobals.InstrumentationTracer.Information<string>(0L, "CommandINvocation request header : {0}", string.Join(";", from string key in headers
			select string.Format("{0}={1}", key, headers[key])));
			WebResponse webResponse = null;
			ResponseContent response;
			try
			{
				webResponse = CommandInvocation.GetRequestSender().SendRequest(requestUri, credential, "POST", 600000, true, "application/atom+xml", headers, requestXml);
				response = CommandInvocation.GetResponse(webResponse, out responseXml);
			}
			finally
			{
				if (webResponse != null)
				{
					((IDisposable)webResponse).Dispose();
				}
			}
			return response;
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0001C158 File Offset: 0x0001A358
		private static ResponseContent ResumeRequest(string pswsServiceUri, string requrestGuid, NetworkCredential credential, NameValueCollection headersTobeAdd, out string responseXml)
		{
			string requestUri = string.Format("{0}/{1}/{2}(guid'{3}')", new object[]
			{
				pswsServiceUri,
				"Service.svc",
				"CommandInvocations",
				requrestGuid
			});
			NameValueCollection headers = CommandInvocation.PrepareRequestHeaders(requestUri, headersTobeAdd);
			WebResponse webResponse = null;
			ResponseContent response;
			try
			{
				webResponse = CommandInvocation.GetRequestSender().SendRequest(requestUri, credential, "GET", 600000, true, "application/atom+xml", headers, null);
				response = CommandInvocation.GetResponse(webResponse, out responseXml);
			}
			finally
			{
				if (webResponse != null)
				{
					((IDisposable)webResponse).Dispose();
				}
			}
			return response;
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x0001C1E8 File Offset: 0x0001A3E8
		private static ResponseContent GetResponse(WebResponse response, out string responseXml)
		{
			ResponseContent responseInformation;
			using (StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
			{
				responseXml = streamReader.ReadToEnd();
				responseInformation = ObjectTransfer.GetResponseInformation(responseXml);
			}
			return responseInformation;
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0001C234 File Offset: 0x0001A434
		private static bool CertificateNoValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			return true;
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0001C237 File Offset: 0x0001A437
		private static IWebRequestSender GetRequestSender()
		{
			if (CommandInvocation.RequestSender != null)
			{
				return CommandInvocation.RequestSender;
			}
			return CommandInvocation.DefaultHttpWebRequestSender;
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0001C24B File Offset: 0x0001A44B
		private static void CheckArgumentNull(string paramName, object paramValue)
		{
			if (paramValue == null)
			{
				throw new ArgumentNullException(paramName);
			}
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0001C258 File Offset: 0x0001A458
		private static NameValueCollection PrepareRequestHeaders(string requestUri, NameValueCollection headersTobeAdd)
		{
			NameValueCollection nameValueCollection = new NameValueCollection(10);
			if (headersTobeAdd != null)
			{
				nameValueCollection.Add(headersTobeAdd);
			}
			nameValueCollection[CertificateValidationManager.ComponentIdHeaderName] = "CommandInvcocation";
			string text = CommandInvocation.GenerateKerberosAuthHeader(new Uri(requestUri).Host);
			if (text != null)
			{
				nameValueCollection[CommandInvocation.authorizationHeader] = text;
			}
			return nameValueCollection;
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0001C2A8 File Offset: 0x0001A4A8
		private static string GenerateKerberosAuthHeader(string host)
		{
			AuthenticationContext authenticationContext = null;
			string result;
			try
			{
				authenticationContext = new AuthenticationContext();
				byte[] inputBuffer = null;
				byte[] bytes = null;
				string spn = CommandInvocation.spnPrefixForHttp + host;
				authenticationContext.InitializeForOutboundNegotiate(AuthenticationMechanism.Kerberos, spn, null, null);
				SecurityStatus securityStatus = authenticationContext.NegotiateSecurityContext(inputBuffer, out bytes);
				if (securityStatus != SecurityStatus.OK && securityStatus != SecurityStatus.ContinueNeeded)
				{
					result = null;
				}
				else
				{
					string @string = Encoding.ASCII.GetString(bytes);
					result = CommandInvocation.prefixForKerbAuthBlob + @string;
				}
			}
			finally
			{
				if (authenticationContext != null)
				{
					authenticationContext.Dispose();
					authenticationContext = null;
				}
			}
			return result;
		}

		// Token: 0x04000208 RID: 520
		internal const int DefaultTimeout = 600000;

		// Token: 0x04000209 RID: 521
		internal const string CommandInvocationName = "CommandInvocations";

		// Token: 0x0400020A RID: 522
		private const int RequestTimeout = 600000;

		// Token: 0x0400020B RID: 523
		private const int SleepInterval = 1000;

		// Token: 0x0400020C RID: 524
		private const int DefaultWaitMsecParam = 30000;

		// Token: 0x0400020D RID: 525
		private const string ServiceName = "Service.svc";

		// Token: 0x0400020E RID: 526
		private const string RequestFormat = "<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?>\r\n                    <entry xmlns:d=\"http://schemas.microsoft.com/ado/2007/08/dataservices\"\r\n                        xmlns:m=\"http://schemas.microsoft.com/ado/2007/08/dataservices/metadata\"\r\n                        xmlns=\"http://www.w3.org/2005/Atom\">\r\n                        <content type=\"application/xml\">\r\n                            <m:properties>\r\n                                <d:Command>{0}</d:Command>\r\n                                <d:OutputFormat>{1}</d:OutputFormat>\r\n                                <d:WaitMsec m:type=\"Edm.Int32\">{2}</d:WaitMsec>\r\n                            </m:properties>\r\n                        </content>\r\n                    </entry>";

		// Token: 0x0400020F RID: 527
		private const string ExchangeXmlFormat = "ExchangeXml";

		// Token: 0x04000210 RID: 528
		private const string ComponentId = "CommandInvcocation";

		// Token: 0x04000211 RID: 529
		private static IWebRequestSender DefaultHttpWebRequestSender = new HttpWebRequestSender();

		// Token: 0x04000212 RID: 530
		private static readonly string authorizationHeader = "Authorization";

		// Token: 0x04000213 RID: 531
		private static readonly string spnPrefixForHttp = "HTTP/";

		// Token: 0x04000214 RID: 532
		private static readonly string prefixForKerbAuthBlob = "Negotiate ";
	}
}
