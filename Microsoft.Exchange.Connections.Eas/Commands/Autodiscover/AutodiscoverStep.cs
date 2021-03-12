using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands.Autodiscover
{
	// Token: 0x02000020 RID: 32
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class AutodiscoverStep : EasCommand<AutodiscoverRequest, AutodiscoverResponse>, IExecuteStep
	{
		// Token: 0x060000BC RID: 188 RVA: 0x000039F4 File Offset: 0x00001BF4
		internal AutodiscoverStep(EasConnectionSettings easConnectionSettings, Step nextStepOnFailure) : base(Command.Autodiscover, easConnectionSettings)
		{
			this.NextStepOnFailure = nextStepOnFailure;
			base.InitializeExpectedHttpStatusCodes(typeof(AutodiscoverHttpStatus));
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00003A15 File Offset: 0x00001C15
		// (set) Token: 0x060000BE RID: 190 RVA: 0x00003A1D File Offset: 0x00001C1D
		private protected Step NextStepOnFailure { protected get; private set; }

		// Token: 0x060000BF RID: 191 RVA: 0x00003A26 File Offset: 0x00001C26
		public virtual Step ExecuteStep(StepContext stepContext)
		{
			return this.PrimitiveExecuteStep(stepContext);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003A2F File Offset: 0x00001C2F
		public Step PrepareAndExecuteStep(StepContext stepContext)
		{
			stepContext.ProbeStack.Clear();
			stepContext.HttpStatusCode = (HttpStatusCode)0;
			stepContext.Error = null;
			stepContext.Response = null;
			if (!this.IsStepAllowable(stepContext))
			{
				return this.NextStepOnFailure;
			}
			return this.ExecuteStep(stepContext);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003A68 File Offset: 0x00001C68
		protected Step SendWebRequest(HttpWebRequest webRequest, StepContext stepContext)
		{
			Step result = this.NextStepOnFailure;
			using (HttpWebResponse httpResponse = webRequest.GetHttpResponse(base.ExpectedHttpStatusCodes))
			{
				stepContext.EasConnectionSettings.Log.Info("{0} {1}", new object[]
				{
					(int)httpResponse.StatusCode,
					httpResponse.StatusDescription
				});
				stepContext.HttpStatusCode = httpResponse.StatusCode;
				HttpStatusCode statusCode = httpResponse.StatusCode;
				if (statusCode <= HttpStatusCode.TemporaryRedirect)
				{
					if (statusCode != HttpStatusCode.OK)
					{
						switch (statusCode)
						{
						case HttpStatusCode.MovedPermanently:
						case HttpStatusCode.Found:
							break;
						default:
							if (statusCode != HttpStatusCode.TemporaryRedirect)
							{
								goto IL_DE;
							}
							break;
						}
						result = this.ProcessRedirection(httpResponse, stepContext);
						goto IL_FB;
					}
					result = this.ProcessSuccessfulHttpResponse(httpResponse, stepContext);
					goto IL_FB;
				}
				else
				{
					if (statusCode == HttpStatusCode.Unauthorized || statusCode == HttpStatusCode.NotFound)
					{
						goto IL_FB;
					}
					switch (statusCode)
					{
					case HttpStatusCode.InternalServerError:
					case HttpStatusCode.BadGateway:
						goto IL_FB;
					case HttpStatusCode.ServiceUnavailable:
						base.ThrowRetryAfterException(httpResponse);
						goto IL_FB;
					}
				}
				IL_DE:
				string msg = string.Format("HTTP status code = '{0}'", httpResponse.StatusCode);
				throw new EasUnexpectedHttpStatusException(msg);
				IL_FB:;
			}
			return result;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003B90 File Offset: 0x00001D90
		protected virtual Step ProcessSuccessfulHttpResponse(HttpWebResponse webResponse, StepContext stepContext)
		{
			stepContext.Response = this.ExtractResponse(webResponse);
			stepContext.Response.HttpStatus = (HttpStatus)webResponse.StatusCode;
			return Step.Succeeded;
		}

		// Token: 0x060000C3 RID: 195
		protected abstract bool IsStepAllowable(StepContext stepContext);

		// Token: 0x060000C4 RID: 196 RVA: 0x00003BB4 File Offset: 0x00001DB4
		protected override AutodiscoverResponse ExtractResponse(HttpWebResponse webResponse)
		{
			AutodiscoverResponse result;
			using (Stream responseStream = webResponse.GetResponseStream())
			{
				if (responseStream == null)
				{
					result = new AutodiscoverResponse
					{
						AutodiscoverStatus = AutodiscoverStatus.ProtocolError
					};
				}
				else
				{
					XmlSerializer xmlSerializer = new XmlSerializer(typeof(AutodiscoverResponse));
					if (!base.EasConnectionSettings.Log.IsEnabled(LogLevel.LogInfo))
					{
						result = (AutodiscoverResponse)xmlSerializer.Deserialize(responseStream);
					}
					else
					{
						using (MemoryStream memoryStream = new MemoryStream())
						{
							responseStream.CopyTo(memoryStream);
							memoryStream.Seek(0L, SeekOrigin.Begin);
							XmlDocument xmlDocument = new XmlDocument();
							xmlDocument.Load(memoryStream);
							base.LogInfoXml(xmlDocument);
							memoryStream.Seek(0L, SeekOrigin.Begin);
							result = (AutodiscoverResponse)xmlSerializer.Deserialize(memoryStream);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003C94 File Offset: 0x00001E94
		protected override void AddWebRequestHeaders(HttpWebRequest webRequest)
		{
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003C98 File Offset: 0x00001E98
		protected override void AddWebRequestBody(HttpWebRequest webRequest, AutodiscoverRequest easRequest)
		{
			XmlDocument requestXmlDocument = EasCommand<AutodiscoverRequest, AutodiscoverResponse>.GetRequestXmlDocument(easRequest);
			byte[] bytes = new UTF8Encoding().GetBytes(requestXmlDocument.OuterXml);
			using (Stream requestStream = webRequest.GetRequestStream())
			{
				requestStream.Write(bytes, 0, (int)bytes.LongLength);
				requestStream.Flush();
			}
			webRequest.ContentType = "text/xml";
			base.LogInfoXml(requestXmlDocument);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003D08 File Offset: 0x00001F08
		protected string GetUriString(string currentUriProbe)
		{
			return string.Format("{0}//{1}/autodiscover/autodiscover.xml", this.UseSsl ? "https:" : "http:", currentUriProbe);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003D2C File Offset: 0x00001F2C
		protected bool TryStrippingPrefixFromDomain(string originalDomain, out string strippedDomain)
		{
			string[] array = originalDomain.Split(new char[]
			{
				'.'
			});
			int num = array.Length;
			strippedDomain = ((num > 2) ? (array[num - 2] + "." + array[num - 1]) : originalDomain);
			return num > 2;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003D74 File Offset: 0x00001F74
		protected string GetStrippedDomain(string domain)
		{
			string result;
			this.TryStrippingPrefixFromDomain(domain, out result);
			return result;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003D8C File Offset: 0x00001F8C
		protected string GetAutodiscoverDomain(string domain)
		{
			string strippedDomain = this.GetStrippedDomain(domain);
			return "autodiscover." + strippedDomain;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003DB0 File Offset: 0x00001FB0
		protected Step PrimitiveExecuteStep(StepContext stepContext)
		{
			Step result = this.NextStepOnFailure;
			try
			{
				string uriString = this.GetUriString(stepContext.ProbeStack.Peek());
				HttpWebRequest httpWebRequest = base.CreateWebRequest(uriString);
				this.AddWebRequestHeaders(httpWebRequest);
				this.AddWebRequestBody(httpWebRequest, stepContext.Request);
				result = this.SendWebRequest(httpWebRequest, stepContext);
			}
			catch (WebException ex)
			{
				stepContext.EasConnectionSettings.Log.Error("Step failed with exception: Status = {0}, Message = {1}", new object[]
				{
					ex.Status,
					ex.Message
				});
				stepContext.Error = ex;
				HttpWebResponse httpWebResponse = ex.Response as HttpWebResponse;
				if (httpWebResponse != null)
				{
					stepContext.EasConnectionSettings.Log.Info("{0} {1}", new object[]
					{
						(int)httpWebResponse.StatusCode,
						httpWebResponse.StatusDescription
					});
					stepContext.HttpStatusCode = httpWebResponse.StatusCode;
					if (httpWebResponse.StatusCode != HttpStatusCode.BadGateway)
					{
						throw new EasWebException(httpWebResponse.StatusCode.ToString(), ex);
					}
				}
			}
			return result;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003ED4 File Offset: 0x000020D4
		private Step ProcessRedirection(HttpWebResponse webResponse, StepContext stepContext)
		{
			if (stepContext.ProbeStack.Count >= 10)
			{
				stepContext.EasConnectionSettings.Log.Error("Autodiscover.ProcessRedirection exceeded MaximumProbeDepth.", new object[0]);
				return this.NextStepOnFailure;
			}
			string text = webResponse.Headers["Location"];
			if (text != null)
			{
				return this.FollowRedirection(text, stepContext);
			}
			return this.NextStepOnFailure;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003F38 File Offset: 0x00002138
		private Step FollowRedirection(string newLocationUrl, StepContext stepContext)
		{
			stepContext.EasConnectionSettings.Log.Debug("Redirect: {0}", new object[]
			{
				newLocationUrl
			});
			Uri uri = new Uri(newLocationUrl);
			stepContext.ProbeStack.Push(uri.Host);
			return this.PrimitiveExecuteStep(stepContext);
		}

		// Token: 0x040000E5 RID: 229
		protected const string UriFormatString = "{0}//{1}/autodiscover/autodiscover.xml";

		// Token: 0x040000E6 RID: 230
		private const string AutodiscoverPrefix = "autodiscover.";

		// Token: 0x040000E7 RID: 231
		private const int MaximumProbeDepth = 10;
	}
}
