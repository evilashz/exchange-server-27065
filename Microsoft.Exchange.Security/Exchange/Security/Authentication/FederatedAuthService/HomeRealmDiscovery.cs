using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Security;

namespace Microsoft.Exchange.Security.Authentication.FederatedAuthService
{
	// Token: 0x0200006B RID: 107
	internal class HomeRealmDiscovery : IHomeRealmDiscovery
	{
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000390 RID: 912 RVA: 0x0001C2EE File Offset: 0x0001A4EE
		private static ExEventLog eventLogger
		{
			get
			{
				return AuthServiceHelper.EventLogger;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0001C2F5 File Offset: 0x0001A4F5
		private static LiveIdBasicAuthenticationCountersInstance counters
		{
			get
			{
				return AuthServiceHelper.PerformanceCounters;
			}
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0001C2FC File Offset: 0x0001A4FC
		private HomeRealmDiscovery()
		{
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0001C304 File Offset: 0x0001A504
		public HomeRealmDiscovery(int traceId, LiveIdInstanceType instance, string uri)
		{
			this.Instance = instance;
			this.RealmDiscoveryUri = uri;
			this.traceId = traceId;
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000394 RID: 916 RVA: 0x0001C321 File Offset: 0x0001A521
		// (set) Token: 0x06000395 RID: 917 RVA: 0x0001C329 File Offset: 0x0001A529
		public string RealmDiscoveryUri { get; private set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000396 RID: 918 RVA: 0x0001C332 File Offset: 0x0001A532
		// (set) Token: 0x06000397 RID: 919 RVA: 0x0001C33A File Offset: 0x0001A53A
		public LiveIdInstanceType Instance { get; private set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000398 RID: 920 RVA: 0x0001C343 File Offset: 0x0001A543
		// (set) Token: 0x06000399 RID: 921 RVA: 0x0001C34B File Offset: 0x0001A54B
		public long Latency { get; protected set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600039A RID: 922 RVA: 0x0001C354 File Offset: 0x0001A554
		// (set) Token: 0x0600039B RID: 923 RVA: 0x0001C35C File Offset: 0x0001A55C
		public long SSLConnectionLatency { get; protected set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600039C RID: 924 RVA: 0x0001C365 File Offset: 0x0001A565
		public string StsTag
		{
			get
			{
				return "HRD";
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600039D RID: 925 RVA: 0x0001C36C File Offset: 0x0001A56C
		// (set) Token: 0x0600039E RID: 926 RVA: 0x0001C374 File Offset: 0x0001A574
		public string LiveServer { get; protected set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600039F RID: 927 RVA: 0x0001C37D File Offset: 0x0001A57D
		public string ConnectionGroupName
		{
			get
			{
				if (string.IsNullOrEmpty(this.connectionGroupName))
				{
					this.connectionGroupName = AuthServiceHelper.GetConnectionGroup(this.traceId);
				}
				return this.connectionGroupName;
			}
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0001C3A4 File Offset: 0x0001A5A4
		public IAsyncResult StartRequestChain(object userId, AsyncCallback callback, object state)
		{
			ExTraceGlobals.AuthenticationTracer.TraceFunction((long)this.traceId, "Entering HomeRealmDiscovery.StartRequestChain()");
			if (userId == null)
			{
				ExTraceGlobals.AuthenticationTracer.TraceError((long)this.traceId, "userId is null");
				throw new ArgumentNullException("userId");
			}
			Interlocked.Increment(ref HomeRealmDiscovery.numberOfOutgoingRequests);
			HomeRealmDiscovery.counters.NumberOfOutgoingHrdRequests.RawValue = (long)HomeRealmDiscovery.numberOfOutgoingRequests;
			this.remoteUserName = (byte[])userId;
			this.traceUserName = Encoding.UTF8.GetString(this.remoteUserName);
			this.stopwatch = Stopwatch.StartNew();
			ExTraceGlobals.AuthenticationTracer.Information<string, string>((long)this.traceId, "Creating home realm discovery request to '{0}' for '{1}'", this.RealmDiscoveryUri, this.traceUserName);
			this.hrdRequest = AuthServiceHelper.CreateHttpWebRequest(this.RealmDiscoveryUri);
			if (AuthServiceStaticConfig.Config.MsoSSLEndpointType != MsoEndpointType.OLD)
			{
				AuthServiceHelper.UpdateConnectionSettingsInRequest(ref this.hrdRequest, this.ConnectionGroupName);
			}
			this.hrdRequest.ContentType = "application/x-www-form-urlencoded";
			IAsyncResult result = this.hrdRequest.BeginGetRequestStream(callback, state);
			ExTraceGlobals.AuthenticationTracer.TraceFunction((long)this.traceId, "Leaving HomeRealmDiscovery.StartRequestChain()");
			return result;
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0001C4D4 File Offset: 0x0001A6D4
		public IAsyncResult ProcessRequest(IAsyncResult asyncResult, AsyncCallback callback, object state)
		{
			ExTraceGlobals.AuthenticationTracer.TraceFunction((long)this.traceId, "Entering HomeRealmDiscovery.ProcessRequest()");
			string s = "login=" + Uri.EscapeDataString(this.traceUserName) + "&xml=1";
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			Stream stream = this.hrdRequest.EndGetRequestStream(asyncResult);
			this.SSLConnectionLatency = this.stopwatch.ElapsedMilliseconds;
			HomeRealmDiscovery.WriteBytes(stream, bytes);
			stream.Close();
			IAsyncResult result = this.hrdRequest.BeginGetResponse(callback, state);
			ExTraceGlobals.AuthenticationTracer.TraceFunction((long)this.traceId, "Leaving HomeRealmDiscovery.ProcessRequest()");
			return result;
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0001C570 File Offset: 0x0001A770
		public DomainConfig ProcessResponse(IAsyncResult asyncResult)
		{
			ExTraceGlobals.AuthenticationTracer.TraceFunction((long)this.traceId, "Entering HomeRealmDiscovery.ProcessResponse()");
			DomainConfig result = null;
			bool closeConnectionGroup = false;
			ExTraceGlobals.AuthenticationTracer.TracePerformance<long>((long)this.traceId, "LiveID home realm discovery responded in {0}ms", this.stopwatch.ElapsedMilliseconds);
			ExTraceGlobals.AuthenticationTracer.Information<string, string>((long)this.traceId, "Parsing home realm discovery request to '{0}' for '{1}'", this.RealmDiscoveryUri, this.traceUserName);
			try
			{
				using (WebResponse webResponse = this.hrdRequest.EndGetResponse(asyncResult))
				{
					this.stopwatch.Stop();
					this.Latency = this.stopwatch.ElapsedMilliseconds;
					HomeRealmDiscovery.counters.AverageHrdResponseTime.IncrementBy(this.stopwatch.ElapsedMilliseconds);
					HomeRealmDiscovery.counters.AverageHrdResponseTimeBase.Increment();
					HttpWebResponse httpWebResponse = (HttpWebResponse)webResponse;
					if (webResponse.Headers != null)
					{
						this.LiveServer = webResponse.Headers.Get("PPServer");
					}
					if (httpWebResponse.StatusCode == HttpStatusCode.OK)
					{
						using (Stream responseStream = httpWebResponse.GetResponseStream())
						{
							using (StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8))
							{
								int num = 0;
								int num2 = 0;
								string text = null;
								SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
								safeXmlDocument.PreserveWhitespace = true;
								string xml = streamReader.ReadToEnd();
								try
								{
									safeXmlDocument.LoadXml(xml);
								}
								catch (XmlException)
								{
									this.ThrowMissingNodeError(xml, "/", this.traceUserName, this.traceUserName);
								}
								string text2 = "/RealmInfo/State";
								XmlNode xmlNode = safeXmlDocument.SelectSingleNode(text2);
								if (xmlNode == null)
								{
									this.ThrowMissingNodeError(safeXmlDocument.OuterXml, text2, this.traceUserName, this.traceUserName);
								}
								int num3 = int.Parse(xmlNode.InnerText);
								bool flag = num3 == 4 || num3 == 3;
								if (this.Instance == LiveIdInstanceType.Business)
								{
									text2 = "/RealmInfo/EDUDomainFlags";
									xmlNode = safeXmlDocument.SelectSingleNode(text2);
									if (xmlNode != null && !string.IsNullOrEmpty(xmlNode.InnerText))
									{
										num = int.Parse(xmlNode.InnerText);
										text2 = "/RealmInfo/DomainName";
										xmlNode = safeXmlDocument.SelectSingleNode(text2);
										if (xmlNode == null)
										{
											this.ThrowMissingNodeError(safeXmlDocument.OuterXml, text2, this.traceUserName, this.traceUserName);
										}
										text = xmlNode.InnerText;
										switch (num)
										{
										case 0:
										case 1:
										case 3:
											ExTraceGlobals.AuthenticationTracer.Information<int, string, string>((long)this.traceId, "home realm discovery EDUDomainFlags are '{0}' for domain {1} from {2}", num, text, this.RealmDiscoveryUri);
											goto IL_2B3;
										}
										ExTraceGlobals.AuthenticationTracer.TraceWarning<int, string, string>((long)this.traceId, "home realm discovery returned invalid EDUDomainFlags value '{0}' for domain {1} from {2}", num, text, this.RealmDiscoveryUri);
										HomeRealmDiscovery.eventLogger.LogEvent(SecurityEventLogConstants.Tuple_RealmDiscoveryReadError, text, new object[]
										{
											this.traceUserName,
											this.RealmDiscoveryUri,
											text2,
											safeXmlDocument.OuterXml
										});
									}
								}
								IL_2B3:
								LiveIdInstanceType instance = this.Instance;
								string text3;
								bool federated;
								if (num == 1)
								{
									text3 = null;
									text = null;
									federated = false;
									instance = LiveIdInstanceType.Consumer;
									flag = true;
								}
								else if (num == 3)
								{
									text3 = null;
									federated = true;
									instance = LiveIdInstanceType.Consumer;
								}
								else if (num3 == 1 || num3 == 3 || num3 == 2 || num3 == 0)
								{
									text2 = "/RealmInfo/DomainName";
									xmlNode = safeXmlDocument.SelectSingleNode(text2);
									if (xmlNode == null)
									{
										this.ThrowMissingNodeError(safeXmlDocument.OuterXml, text2, this.traceUserName, this.traceUserName);
									}
									text = xmlNode.InnerText;
									text2 = "/RealmInfo/STSAuthURL";
									xmlNode = safeXmlDocument.SelectSingleNode(text2);
									if (xmlNode == null)
									{
										this.ThrowMissingNodeError(safeXmlDocument.OuterXml, text2, this.traceUserName, text);
									}
									text3 = xmlNode.InnerText;
									text2 = "/RealmInfo/PreferredProtocol";
									xmlNode = safeXmlDocument.SelectSingleNode(text2);
									if (xmlNode == null)
									{
										ExTraceGlobals.AuthenticationTracer.Information<string>((long)this.traceId, "home realm discovery did not return PreferredProtocol for domain {0}", text);
									}
									if (string.IsNullOrEmpty(xmlNode.InnerText))
									{
										num2 = 0;
										ExTraceGlobals.AuthenticationTracer.TraceWarning<string>((long)this.traceId, "home realm discovery returned empty PreferredProtocol for domain {0}", text);
									}
									else if (!int.TryParse(xmlNode.InnerText, out num2) || num2 < 0 || num2 > 2)
									{
										num2 = 0;
										ExTraceGlobals.AuthenticationTracer.TraceWarning<string, string>((long)this.traceId, "home realm discovery returned invalid PreferredProtocol in '{0}' for domain {1}", xmlNode.InnerXml, text);
										HomeRealmDiscovery.eventLogger.LogEvent(SecurityEventLogConstants.Tuple_RealmDiscoveryReadError, text, new object[]
										{
											this.traceUserName,
											this.RealmDiscoveryUri,
											text2,
											safeXmlDocument.OuterXml
										});
									}
									ExTraceGlobals.AuthenticationTracer.Information((long)this.traceId, "home realm discovery request to '{0}' for '{1}' resulted in federated state='{2}' cache='{3}' DomainName='{4}' Protocol='{5}' STSAuthUrl='{6}'", new object[]
									{
										this.RealmDiscoveryUri,
										this.traceUserName,
										num3,
										flag,
										text,
										(LivePreferredProtocol)num2,
										text3
									});
									federated = true;
								}
								else
								{
									ExTraceGlobals.AuthenticationTracer.Information((long)this.traceId, "home realm discovery request to '{0}' for '{1}' resulted in non federated state='{2}' cache='{3}'", new object[]
									{
										this.RealmDiscoveryUri,
										this.traceUserName,
										num3,
										flag
									});
									text3 = null;
									text = null;
									federated = false;
								}
								result = new DomainConfig(text, instance, federated, text3, flag, (LivePreferredProtocol)num2);
							}
						}
					}
				}
			}
			catch (WebException ex)
			{
				this.ErrorString = string.Format("HomeRealmDiscovery to {0} failed with exception {1}", this.RealmDiscoveryUri, ex.ToString());
				ExTraceGlobals.AuthenticationTracer.TraceWarning<string, WebException>((long)this.traceId, "HomeRealmDiscovery.ProcessResponse() caught WebException.  URI='{0}' Exception={1}", this.RealmDiscoveryUri, ex);
				HomeRealmDiscovery.eventLogger.LogEvent(SecurityEventLogConstants.Tuple_CannotConnectToHomeRealmDiscovery, this.traceUserName, new object[]
				{
					this.RealmDiscoveryUri,
					this.traceUserName,
					ex
				});
				string str;
				if (ex.Status == WebExceptionStatus.ProtocolError)
				{
					if (ex.Response != null && ex.Response.Headers != null)
					{
						this.LiveServer = ex.Response.Headers.Get("PPServer");
					}
				}
				else if (ex.Status == WebExceptionStatus.TrustFailure && AuthService.CertErrorCache.TryGetValue(this.hrdRequest, out str))
				{
					this.ErrorString += str;
					AuthService.CertErrorCache.Remove(this.hrdRequest);
				}
				closeConnectionGroup = true;
				throw;
			}
			finally
			{
				bool flag2 = AuthServiceHelper.CloseConnectionGroupIfNeeded(closeConnectionGroup, this.RealmDiscoveryUri, this.ConnectionGroupName, this.traceId);
				if (flag2)
				{
					this.ErrorString += "<ConnectionGroupClosed>";
				}
			}
			ExTraceGlobals.AuthenticationTracer.TraceFunction((long)this.traceId, "Leaving HomeRealmDiscovery.ProcessResponse()");
			return result;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0001CC98 File Offset: 0x0001AE98
		public void Abort()
		{
			ExTraceGlobals.AuthenticationTracer.TraceFunction((long)this.traceId, "Entering HomeRealmDiscovery.Abort()");
			if (this.hrdRequest != null)
			{
				ExTraceGlobals.AuthenticationTracer.Information<string, string>((long)this.traceId, "Aborting home realm federation request to LiveID '{0}' for '{1}'", this.RealmDiscoveryUri, this.traceUserName);
				this.hrdRequest.Abort();
			}
			ExTraceGlobals.AuthenticationTracer.TraceFunction((long)this.traceId, "Leaving HomeRealmDiscovery.Abort()");
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x0001CD06 File Offset: 0x0001AF06
		// (set) Token: 0x060003A5 RID: 933 RVA: 0x0001CD0E File Offset: 0x0001AF0E
		public string ErrorString { get; private set; }

		// Token: 0x060003A6 RID: 934 RVA: 0x0001CD18 File Offset: 0x0001AF18
		public string GetLatency()
		{
			return string.Format("<{0}-{1}-{3}ms-ppserver={2}>", new object[]
			{
				this.StsTag,
				this.Instance.ToString(),
				this.LiveServer,
				this.Latency
			});
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0001CD6C File Offset: 0x0001AF6C
		private void ThrowMissingNodeError(string xml, string path, string user, string periodicKey)
		{
			this.ErrorString = string.Format("Realm Discovery XML missing path {0} in\n{1}", path, xml);
			ExTraceGlobals.AuthenticationTracer.TraceWarning((long)this.traceId, this.ErrorString);
			HomeRealmDiscovery.eventLogger.LogEvent(SecurityEventLogConstants.Tuple_RealmDiscoveryReadError, periodicKey, new object[]
			{
				user,
				this.RealmDiscoveryUri,
				path,
				xml
			});
			throw new XmlException(this.ErrorString);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0001CDDC File Offset: 0x0001AFDC
		private static void WriteBytes(Stream stream, byte[] bytes)
		{
			stream.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x04000391 RID: 913
		private static int numberOfOutgoingRequests;

		// Token: 0x04000392 RID: 914
		private Stopwatch stopwatch;

		// Token: 0x04000393 RID: 915
		private HttpWebRequest hrdRequest;

		// Token: 0x04000394 RID: 916
		private byte[] remoteUserName;

		// Token: 0x04000395 RID: 917
		private string traceUserName;

		// Token: 0x04000396 RID: 918
		private int traceId;

		// Token: 0x04000397 RID: 919
		private string connectionGroupName;
	}
}
