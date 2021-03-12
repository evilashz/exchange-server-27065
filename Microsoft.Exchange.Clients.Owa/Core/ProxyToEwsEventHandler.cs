using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Principal;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.EventLogs;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Diagnostics.LatencyDetection;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200021D RID: 541
	[OwaEventNamespace("EwsProxy")]
	internal sealed class ProxyToEwsEventHandler : OwaEventHandlerBase
	{
		// Token: 0x0600122D RID: 4653 RVA: 0x0006EB40 File Offset: 0x0006CD40
		private static IEnumerator<int> CopyStream(AsyncEnumerator ae, Stream readStream, Stream writeStream)
		{
			byte[][] buffer = new byte[][]
			{
				new byte[8192],
				new byte[8192]
			};
			int bytesRead = 0;
			int currentBuffer = 0;
			ae.AddAsync<IAsyncResult>(readStream.BeginRead(buffer[currentBuffer], 0, buffer[0].Length, ae.GetAsyncCallback(), null));
			yield return 1;
			for (bytesRead = readStream.EndRead(ae.CompletedAsyncResults[0]); bytesRead > 0; bytesRead = readStream.EndRead(ae.CompletedAsyncResults[1]))
			{
				ae.AddAsync<IAsyncResult>(writeStream.BeginWrite(buffer[currentBuffer % 2], 0, bytesRead, ae.GetAsyncCallback(), null));
				currentBuffer++;
				ae.AddAsync<IAsyncResult>(readStream.BeginRead(buffer[currentBuffer % 2], 0, buffer[currentBuffer % 2].Length, ae.GetAsyncCallback(), null));
				yield return 2;
				writeStream.EndWrite(ae.CompletedAsyncResults[0]);
			}
			ae.End();
			yield break;
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x0006EB6C File Offset: 0x0006CD6C
		private static SerializedSecurityContextType GetSecurityContext(OwaContext owaContext)
		{
			SecurityAccessToken securityAccessToken = new SecurityAccessToken();
			owaContext.LogonIdentity.ClientSecurityContext.SetSecurityAccessToken(securityAccessToken);
			return new SerializedSecurityContextType
			{
				UserSid = securityAccessToken.UserSid,
				RestrictedGroupSids = ProxyToEwsEventHandler.SidStringAndAttributesConverter(securityAccessToken.RestrictedGroupSids),
				GroupSids = ProxyToEwsEventHandler.SidStringAndAttributesConverter(securityAccessToken.GroupSids),
				PrimarySmtpAddress = owaContext.LogonIdentity.CreateExchangePrincipal().MailboxInfo.PrimarySmtpAddress.ToString()
			};
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x0006EC1C File Offset: 0x0006CE1C
		private static SidAndAttributesType[] SidStringAndAttributesConverter(SidStringAndAttributes[] sidStringAndAttributesArray)
		{
			if (sidStringAndAttributesArray == null)
			{
				return null;
			}
			return Array.ConvertAll<SidStringAndAttributes, SidAndAttributesType>(sidStringAndAttributesArray, (SidStringAndAttributes sidStringAndAttributes) => new SidAndAttributesType
			{
				SecurityIdentifier = sidStringAndAttributes.SecurityIdentifier,
				Attributes = sidStringAndAttributes.Attributes
			});
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x0006EC48 File Offset: 0x0006CE48
		[OwaEvent("Proxy", true)]
		[OwaEventVerb(OwaEventVerb.Post | OwaEventVerb.Get)]
		public IAsyncResult BeginProxyRequest(AsyncCallback callback, object extraData)
		{
			ExTraceGlobals.ProxyCallTracer.TraceDebug((long)this.GetHashCode(), "ProxyToEwsEventHandler.ProxyEvent");
			if (base.UserContext != null)
			{
				int num;
				base.UserContext.DangerousBeginUnlockedAction(false, out num);
				if (num != 1)
				{
					ExWatson.SendReport(new InvalidOperationException("Thread held more than 1 lock before async operation"), ReportOptions.None, null);
				}
			}
			AsyncEnumerator asyncEnumerator = new AsyncEnumerator(callback, extraData, new Func<AsyncEnumerator, IEnumerator<int>>(this.ProcessProxyRequest), false);
			this.asyncResult = asyncEnumerator.AsyncResult;
			asyncEnumerator.Begin();
			return this.asyncResult;
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x0006ECC3 File Offset: 0x0006CEC3
		[OwaEvent("Proxy", true)]
		public void EndProxyRequest(IAsyncResult asyncResult)
		{
			ExTraceGlobals.ProxyCallTracer.TraceDebug((long)this.GetHashCode(), "ProxyEventHandler.EndProxyRequest");
			this.CommonEndProxyRequest(asyncResult);
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x0006ECE4 File Offset: 0x0006CEE4
		private void CommonEndProxyRequest(IAsyncResult asyncResult)
		{
			ExTraceGlobals.ProxyCallTracer.TraceDebug((long)this.GetHashCode(), "ProxyEventHandler.EndProxyRequest");
			AsyncResult asyncResult2 = (AsyncResult)asyncResult;
			try
			{
				if (asyncResult2.Exception != null)
				{
					ExTraceGlobals.ProxyTracer.TraceDebug((long)this.GetHashCode(), "An exception was thrown during the processing of the async request");
					AsyncExceptionWrapperHelper.GetRootException(asyncResult2.Exception);
					Utilities.HandleException(base.OwaContext, new OwaProxyException("Error handling EwsProxy", LocalizedStrings.GetNonEncoded(-200732695), asyncResult2.Exception, false));
				}
			}
			finally
			{
				this.Dispose();
			}
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x0006F2EC File Offset: 0x0006D4EC
		private IEnumerator<int> ProcessProxyRequest(AsyncEnumerator ae)
		{
			ExTraceGlobals.ProxyCallTracer.TraceDebug((long)this.GetHashCode(), "ProxyEventHandler.SendProxyRequest");
			base.DontWriteHeaders = true;
			int newContentLength = 0;
			byte[] soapHeaderStuff = this.GetRequestSoapHeader(out newContentLength);
			HttpWebRequest webRequest = this.GetProxyRequest(newContentLength);
			WindowsImpersonationContext context = NetworkServiceImpersonator.Impersonate();
			try
			{
				ae.AddAsync<IAsyncResult>(webRequest.BeginGetRequestStream(ae.GetAsyncCallback(), null));
			}
			catch
			{
				context.Undo();
				context = null;
				throw;
			}
			finally
			{
				if (context != null)
				{
					context.Undo();
					context = null;
				}
			}
			yield return 1;
			Stream outStream = this.DoWebAction<Stream>(() => webRequest.EndGetRequestStream(ae.CompletedAsyncResults[0]));
			if (outStream == null)
			{
				ae.End();
			}
			else
			{
				using (outStream)
				{
					ae.AddAsync<IAsyncResult>(outStream.BeginWrite(soapHeaderStuff, 0, soapHeaderStuff.Length, ae.GetAsyncCallback(), null));
					yield return 1;
					outStream.EndWrite(ae.CompletedAsyncResults[0]);
					AsyncResult asyncResult = ae.AddAsyncEnumerator((AsyncEnumerator ae1) => ProxyToEwsEventHandler.CopyStream(ae1, this.HttpContext.Request.InputStream, outStream));
					yield return 1;
					asyncResult.End();
				}
				ae.AddAsync<IAsyncResult>(webRequest.BeginGetResponse(ae.GetAsyncCallback(), null));
				yield return 1;
				HttpWebResponse webResponse = this.DoWebAction<HttpWebResponse>(() => (HttpWebResponse)webRequest.EndGetResponse(ae.CompletedAsyncResults[0]));
				if (webResponse != null)
				{
					using (webResponse)
					{
						ae.AddAsyncEnumerator((AsyncEnumerator ae1) => this.ProcessWebResponse(ae1, webResponse));
						yield return 1;
					}
				}
				ae.End();
			}
			yield break;
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x0006F310 File Offset: 0x0006D510
		private T DoWebAction<T>(Func<T> callback) where T : class
		{
			T result;
			try
			{
				result = callback();
			}
			catch (WebException ex)
			{
				HttpWebResponse httpWebResponse = ex.Response as HttpWebResponse;
				ExTraceGlobals.ProxyCallTracer.TraceError<WebExceptionStatus, object>(0L, "Exception hit during AsyncProcessing, webException.Status = {0}, webRequest.Status = {1}", ex.Status, (httpWebResponse != null) ? ((int)httpWebResponse.StatusCode) : "null");
				if (httpWebResponse != null)
				{
					OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_OwaEwsConnectionError, string.Empty, new object[]
					{
						(int)httpWebResponse.StatusCode
					});
					result = (httpWebResponse as T);
				}
				else
				{
					OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_OwaEwsConnectionError, string.Empty, new object[]
					{
						string.Format("null, webException.Status = {0}", ex.Status)
					});
					switch (ex.Status)
					{
					case WebExceptionStatus.ConnectFailure:
					case WebExceptionStatus.ReceiveFailure:
					case WebExceptionStatus.SendFailure:
					case WebExceptionStatus.ConnectionClosed:
						this.HttpContext.Response.StatusCode = 584;
						this.HttpContext.Response.StatusDescription = "Ews is not found on OWA server";
						goto IL_137;
					}
					this.HttpContext.Response.StatusCode = 594;
					this.HttpContext.Response.StatusDescription = "Unknown connection failure";
					IL_137:
					this.HttpContext.Response.Output.Write("Ews Error, Handling through WebException. webException.Status = {0}, Message = '{1}'", ex.Status, ex.Message);
					result = default(T);
				}
			}
			return result;
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x0006F4AC File Offset: 0x0006D6AC
		private bool IsOkToProcess(HttpWebResponse webResponse)
		{
			if (webResponse.StatusCode >= HttpStatusCode.OK && webResponse.StatusCode < HttpStatusCode.MultipleChoices)
			{
				return true;
			}
			HttpStatusCode statusCode = webResponse.StatusCode;
			switch (statusCode)
			{
			case HttpStatusCode.Unauthorized:
				this.HttpContext.Response.StatusCode = 581;
				this.HttpContext.Response.StatusDescription = "Owa could not authorize to EWS on 127.0.0.1";
				goto IL_12E;
			case HttpStatusCode.PaymentRequired:
				break;
			case HttpStatusCode.Forbidden:
				this.HttpContext.Response.StatusCode = 583;
				this.HttpContext.Response.StatusDescription = "Owa is forbidden to connect to EWS on 127.0.0.1";
				goto IL_12E;
			case HttpStatusCode.NotFound:
				this.HttpContext.Response.StatusCode = 584;
				this.HttpContext.Response.StatusDescription = "Ews is not found on OWA server";
				goto IL_12E;
			default:
				if (statusCode == HttpStatusCode.ServiceUnavailable)
				{
					this.HttpContext.Response.StatusCode = 593;
					this.HttpContext.Response.StatusDescription = "Ews is unavailable on OWA server.";
					goto IL_12E;
				}
				break;
			}
			this.HttpContext.Response.StatusCode = (int)webResponse.StatusCode;
			this.HttpContext.Response.StatusDescription = webResponse.StatusDescription;
			IL_12E:
			this.HttpContext.Response.Output.Write("Ews Error, HttpStatus = {0}, StatusDescription = {1}", webResponse.StatusCode, webResponse.StatusDescription);
			return false;
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x0006F810 File Offset: 0x0006DA10
		private IEnumerator<int> ProcessWebResponse(AsyncEnumerator ae, HttpWebResponse webResponse)
		{
			PerformanceCounterManager.AddEwsRequestResult(this.IsOkToProcess(webResponse));
			this.HttpContext.Response.ContentType = webResponse.ContentType;
			using (Stream responseInStream = webResponse.GetResponseStream())
			{
				AsyncResult asyncResult = ae.AddAsyncEnumerator((AsyncEnumerator ae1) => ProxyToEwsEventHandler.CopyStream(ae1, responseInStream, this.HttpContext.Response.OutputStream));
				yield return 1;
				asyncResult.End();
			}
			ae.End();
			yield break;
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x0006F83C File Offset: 0x0006DA3C
		private void ProcessPerformanceDataHeader(HttpWebResponse webResponse)
		{
			string text = webResponse.Headers["X-EwsPerformanceData"];
			string[] array = text.Split(new char[]
			{
				';',
				'='
			});
			string[] array2 = new string[]
			{
				"RpcC",
				"RpcL",
				"LdapC",
				"LdapL"
			};
			string[] array3 = new string[4];
			for (int i = 0; i < array2.Length; i++)
			{
				int num = Array.IndexOf<string>(array, array2[i]);
				if (num < 0 || num == array.Length - 1)
				{
					throw new OwaProxyException("EWS performance data header does not contain expected data", null);
				}
				array3[i] = array[num + 1];
			}
			base.OwaContext.EwsRpcData = new PerformanceData(new TimeSpan(0, 0, 0, 0, int.Parse(array3[1])), uint.Parse(array3[0]));
			base.OwaContext.EwsLdapData = new PerformanceData(new TimeSpan(0, 0, 0, 0, int.Parse(array3[3])), uint.Parse(array3[2]));
			base.OwaContext.EwsPerformanceHeader = text;
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x0006F950 File Offset: 0x0006DB50
		private HttpWebRequest GetProxyRequest(int contentLength)
		{
			NetworkServiceImpersonator.Initialize();
			if (NetworkServiceImpersonator.Exception != null)
			{
				ExTraceGlobals.ProxyCallTracer.TraceError<LocalizedException>(0L, "Unable to impersonate network service to call EWS due exception {0}", NetworkServiceImpersonator.Exception);
				throw new AsyncExceptionWrapper(NetworkServiceImpersonator.Exception);
			}
			HttpWebRequest httpWebRequest = ProxyUtilities.CreateHttpWebRequestForProxying(base.OwaContext, this.proxyRequestUrl);
			httpWebRequest.Method = "POST";
			httpWebRequest.ContentType = this.HttpContext.Request.ContentType;
			httpWebRequest.ContentLength = (long)contentLength;
			httpWebRequest.UserAgent = "OwaProxy";
			httpWebRequest.PreAuthenticate = true;
			return httpWebRequest;
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x0006F9D8 File Offset: 0x0006DBD8
		private byte[] GetRequestSoapHeader(out int newContentLength)
		{
			ProxyToEwsEventHandler.StreamWrapper streamWrapper = new ProxyToEwsEventHandler.StreamWrapper(this.HttpContext.Request.InputStream);
			byte[] result;
			using (XmlReader xmlReader = XmlReader.Create(streamWrapper))
			{
				if (!xmlReader.ReadToFollowing("Envelope", "http://schemas.xmlsoap.org/soap/envelope/"))
				{
					throw new OwaInvalidRequestException("Invalid XML format for this request");
				}
				using (XmlWriter xmlWriter = XmlWriter.Create(streamWrapper.BufferStream, new XmlWriterSettings
				{
					OmitXmlDeclaration = true,
					Encoding = this.HttpContext.Request.ContentEncoding
				}))
				{
					xmlWriter.WriteStartElement("Header", "http://schemas.xmlsoap.org/soap/envelope/");
					SerializedSecurityContextType securityContext = ProxyToEwsEventHandler.GetSecurityContext(base.OwaContext);
					RequestServerVersion requestServerVersion = new RequestServerVersion();
					if ("12.1".Equals(this.HttpContext.Request.QueryString["rv"]))
					{
						requestServerVersion.Version = ExchangeVersionType.Exchange2007_SP1;
					}
					else
					{
						requestServerVersion.Version = ExchangeVersionType.Exchange2010_SP1;
					}
					TimeZoneDefinitionType timeZoneDefinitionType = new TimeZoneDefinitionType();
					timeZoneDefinitionType.Id = base.OwaContext.TimeZoneId;
					TimeZoneContextType timeZoneContextType = new TimeZoneContextType();
					timeZoneContextType.TimeZoneDefinition = timeZoneDefinitionType;
					ProxyToEwsEventHandler.securityContextSerializer.Serialize(xmlWriter, securityContext);
					ProxyToEwsEventHandler.ewsVersionSerializer.Serialize(xmlWriter, requestServerVersion);
					ProxyToEwsEventHandler.timeZoneContextSerializer.Serialize(xmlWriter, timeZoneContextType);
					xmlWriter.WriteEndElement();
					xmlWriter.Close();
					newContentLength = (int)((long)this.HttpContext.Request.ContentLength + streamWrapper.BufferStream.Position - this.HttpContext.Request.InputStream.Position);
					result = streamWrapper.BufferStream.ToArray();
				}
			}
			return result;
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x0006FB98 File Offset: 0x0006DD98
		protected override void InternalDispose(bool isDisposing)
		{
			if (isDisposing)
			{
				this.asyncResult.Dispose();
			}
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x0006FBA8 File Offset: 0x0006DDA8
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ProxyToEwsEventHandler>(this);
		}

		// Token: 0x04000C7D RID: 3197
		internal const string HandlerName = "EwsProxy";

		// Token: 0x04000C7E RID: 3198
		internal const string ProxyEvent = "Proxy";

		// Token: 0x04000C7F RID: 3199
		internal const string SerializedSecurityContext = "SerializedSecurityContext";

		// Token: 0x04000C80 RID: 3200
		internal const string SoapEnvelope = "Envelope";

		// Token: 0x04000C81 RID: 3201
		internal const string SoapHeader = "Header";

		// Token: 0x04000C82 RID: 3202
		internal const string SoapNamespace = "http://schemas.xmlsoap.org/soap/envelope/";

		// Token: 0x04000C83 RID: 3203
		internal const string RequestVersionHeader = "rv";

		// Token: 0x04000C84 RID: 3204
		internal const string E12_Sp1_Version = "12.1";

		// Token: 0x04000C85 RID: 3205
		private static readonly XmlSerializer securityContextSerializer = new XmlSerializer(typeof(SerializedSecurityContextType));

		// Token: 0x04000C86 RID: 3206
		private static readonly XmlSerializer ewsVersionSerializer = new XmlSerializer(typeof(RequestServerVersion));

		// Token: 0x04000C87 RID: 3207
		private static readonly XmlSerializer timeZoneContextSerializer = new XmlSerializer(typeof(TimeZoneContextType));

		// Token: 0x04000C88 RID: 3208
		private Uri proxyRequestUrl = new Uri("https://127.0.0.1/ews/exchange.asmx");

		// Token: 0x04000C89 RID: 3209
		private AsyncResult asyncResult;

		// Token: 0x0200021E RID: 542
		private class StreamWrapper : Stream
		{
			// Token: 0x0600123F RID: 4671 RVA: 0x0006FC06 File Offset: 0x0006DE06
			public StreamWrapper(Stream innerStream)
			{
				this.innerStream = innerStream;
			}

			// Token: 0x170004FA RID: 1274
			// (get) Token: 0x06001240 RID: 4672 RVA: 0x0006FC25 File Offset: 0x0006DE25
			public override bool CanRead
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170004FB RID: 1275
			// (get) Token: 0x06001241 RID: 4673 RVA: 0x0006FC28 File Offset: 0x0006DE28
			public override bool CanSeek
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170004FC RID: 1276
			// (get) Token: 0x06001242 RID: 4674 RVA: 0x0006FC2B File Offset: 0x0006DE2B
			public override bool CanWrite
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001243 RID: 4675 RVA: 0x0006FC2E File Offset: 0x0006DE2E
			public override void Flush()
			{
				throw new NotImplementedException();
			}

			// Token: 0x170004FD RID: 1277
			// (get) Token: 0x06001244 RID: 4676 RVA: 0x0006FC35 File Offset: 0x0006DE35
			public override long Length
			{
				get
				{
					return this.innerStream.Length;
				}
			}

			// Token: 0x170004FE RID: 1278
			// (get) Token: 0x06001245 RID: 4677 RVA: 0x0006FC42 File Offset: 0x0006DE42
			// (set) Token: 0x06001246 RID: 4678 RVA: 0x0006FC4F File Offset: 0x0006DE4F
			public override long Position
			{
				get
				{
					return this.innerStream.Position;
				}
				set
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x06001247 RID: 4679 RVA: 0x0006FC58 File Offset: 0x0006DE58
			public override int Read(byte[] buffer, int offset, int count)
			{
				int num = this.innerStream.Read(buffer, offset, 1);
				if (this.BufferStream.Length < 4096L)
				{
					this.BufferStream.Write(buffer, offset, (int)Math.Min((long)num, 4096L - this.BufferStream.Length));
				}
				return num;
			}

			// Token: 0x06001248 RID: 4680 RVA: 0x0006FCAF File Offset: 0x0006DEAF
			public override long Seek(long offset, SeekOrigin origin)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06001249 RID: 4681 RVA: 0x0006FCB6 File Offset: 0x0006DEB6
			public override void SetLength(long value)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600124A RID: 4682 RVA: 0x0006FCBD File Offset: 0x0006DEBD
			public override void Write(byte[] buffer, int offset, int count)
			{
				throw new NotImplementedException();
			}

			// Token: 0x04000C8B RID: 3211
			private const long BufferStreamMaxSize = 4096L;

			// Token: 0x04000C8C RID: 3212
			private Stream innerStream;

			// Token: 0x04000C8D RID: 3213
			public readonly MemoryStream BufferStream = new MemoryStream(4096);
		}

		// Token: 0x0200021F RID: 543
		private enum EwsResponseErrorStatus
		{
			// Token: 0x04000C8F RID: 3215
			OwaForbidden = 583,
			// Token: 0x04000C90 RID: 3216
			OwaCantAuthrize = 581,
			// Token: 0x04000C91 RID: 3217
			EwsServiceUnavailable = 593,
			// Token: 0x04000C92 RID: 3218
			EwsNotFound = 584,
			// Token: 0x04000C93 RID: 3219
			Unknown = 594
		}
	}
}
