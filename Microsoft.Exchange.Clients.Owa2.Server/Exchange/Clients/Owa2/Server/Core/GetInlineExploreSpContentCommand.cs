using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Security.OAuth;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000316 RID: 790
	internal class GetInlineExploreSpContentCommand : ServiceCommand<InlineExploreSpResultListType>
	{
		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06001A3D RID: 6717 RVA: 0x0006057A File Offset: 0x0005E77A
		// (set) Token: 0x06001A3E RID: 6718 RVA: 0x00060582 File Offset: 0x0005E782
		internal string TargetUrl { get; private set; }

		// Token: 0x06001A3F RID: 6719 RVA: 0x0006058C File Offset: 0x0005E78C
		internal GetInlineExploreSpContentCommand(CallContext callContext, string query, string targetUrl) : base(callContext)
		{
			if (callContext == null)
			{
				throw new OwaInvalidRequestException("callContext parameter was null");
			}
			if (string.IsNullOrEmpty(query))
			{
				throw new OwaInvalidRequestException("query parameter was null");
			}
			if (string.IsNullOrEmpty(targetUrl))
			{
				throw new OwaInvalidRequestException("targetUrl parameter was null");
			}
			this.currentCallContext = callContext;
			this.TargetUrl = this.ConstructTargetUrl(targetUrl, query);
		}

		// Token: 0x06001A40 RID: 6720 RVA: 0x000605FC File Offset: 0x0005E7FC
		protected override InlineExploreSpResultListType InternalExecute()
		{
			return this.GetQueryResults();
		}

		// Token: 0x06001A41 RID: 6721 RVA: 0x00060614 File Offset: 0x0005E814
		private InlineExploreSpResultListType GetQueryResults()
		{
			OAuthCredentials oauthCredentials = this.GetOAuthCredentials();
			string resultText = this.QuerySharePoint(oauthCredentials);
			return this.ParseQueryResults(resultText);
		}

		// Token: 0x06001A42 RID: 6722 RVA: 0x000606FC File Offset: 0x0005E8FC
		internal InlineExploreSpResultListType ParseQueryResults(string resultText)
		{
			InlineExploreSpResultListType inlineExploreSpResultListType = new InlineExploreSpResultListType();
			List<InlineExploreSpResultItemType> list = new List<InlineExploreSpResultItemType>();
			inlineExploreSpResultListType.ResultCount = 0;
			inlineExploreSpResultListType.Status = "Success";
			if (string.IsNullOrEmpty(resultText))
			{
				this.LogError("[GetInlineExploreSpContent.ParseQueryResults(string resultText)]:XML Parsing Error", "Input is empty");
				inlineExploreSpResultListType.Status = "Error: SharePoint query returned empty";
			}
			else
			{
				try
				{
					XElement xelement = XElement.Parse(resultText);
					string value = xelement.Descendants(this.d + "RelevantResults").Elements(this.d + "TotalRows").First<XElement>().Value;
					inlineExploreSpResultListType.ResultCount = int.Parse(value);
					List<XElement> list2 = xelement.Descendants(this.d + "RelevantResults").Elements(this.d + "Table").Elements(this.d + "Rows").Elements(this.d + "element").ToList<XElement>();
					foreach (XElement xelement2 in list2)
					{
						IEnumerable<XElement> source = xelement2.Element(this.d + "Cells").Descendants(this.d + "element");
						InlineExploreSpResultItemType inlineExploreSpResultItemType = new InlineExploreSpResultItemType();
						inlineExploreSpResultItemType.Title = source.First((XElement a) => a.Element(this.d + "Key").Value == "Title").Element(this.d + "Value").Value;
						inlineExploreSpResultItemType.Url = source.First((XElement a) => a.Element(this.d + "Key").Value == "Path").Element(this.d + "Value").Value;
						inlineExploreSpResultItemType.FileType = source.First((XElement a) => a.Element(this.d + "Key").Value == "FileType").Element(this.d + "Value").Value;
						inlineExploreSpResultItemType.LastModifiedTime = source.First((XElement a) => a.Element(this.d + "Key").Value == "LastModifiedTime").Element(this.d + "Value").Value;
						inlineExploreSpResultItemType.Summary = source.First((XElement a) => a.Element(this.d + "Key").Value == "HitHighlightedSummary").Element(this.d + "Value").Value;
						InlineExploreSpResultItemType inlineExploreSpResultItemType2 = inlineExploreSpResultItemType;
						if (inlineExploreSpResultItemType2.Title != null && inlineExploreSpResultItemType2.Url != null)
						{
							list.Add(inlineExploreSpResultItemType2);
						}
						else
						{
							this.LogError("[GetInlineExploreSpContent.ParseQueryResults(string resultText)]:XML Parsing Error", "Linq returned empty result fields");
							inlineExploreSpResultListType.Status = "Error: Empty result fields";
						}
					}
				}
				catch (Exception ex)
				{
					this.LogError("[GetInlineExploreSpContent.ParseQueryResults(string resultText)]:XML Parsing Exception", ex.ToString());
					inlineExploreSpResultListType.Status = "Error: XML parsing";
				}
			}
			inlineExploreSpResultListType.ResultItems = list.ToArray();
			return inlineExploreSpResultListType;
		}

		// Token: 0x06001A43 RID: 6723 RVA: 0x00060A38 File Offset: 0x0005EC38
		private OAuthCredentials GetOAuthCredentials()
		{
			ADUser accessingADUser = this.currentCallContext.AccessingADUser;
			if (accessingADUser == null)
			{
				this.LogError("[GetInlineExploreSpContent.GetOAuthCredentials()]:OAuth error", "AccessingADUser is null");
				return null;
			}
			return OAuthCredentials.GetOAuthCredentialsForAppActAsToken(accessingADUser.OrganizationId, accessingADUser, null);
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x00060A74 File Offset: 0x0005EC74
		private string QuerySharePoint(OAuthCredentials oAuthCredential)
		{
			string text = string.Empty;
			if (oAuthCredential == null)
			{
				this.LogError("[GetInlineExploreSpContent.QuerySharePoint(OAuthCredentials oAuthCredential)]", "Credentials missing");
				return text;
			}
			if (string.IsNullOrEmpty(this.TargetUrl))
			{
				this.LogError("[GetInlineExploreSpContent.QuerySharePoint(OAuthCredentials oAuthCredential)]", "TargetUrl missing");
				return text;
			}
			Guid value = Guid.NewGuid();
			oAuthCredential.ClientRequestId = new Guid?(value);
			oAuthCredential.Tracer = new GetInlineExploreSpContentCommand.GetInlineExploreSpContentOauthOutboundTracer();
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(this.TargetUrl);
			httpWebRequest.Method = "GET";
			httpWebRequest.ContentType = "text/xml";
			httpWebRequest.UserAgent = "Exchange/15.00.0000.000/InlineExplore";
			httpWebRequest.Headers.Add("client-request-id", value.ToString());
			httpWebRequest.Headers.Add("return-client-request-id", "true");
			httpWebRequest.Credentials = oAuthCredential;
			httpWebRequest.PreAuthenticate = true;
			httpWebRequest.Headers.Add(HttpRequestHeader.Authorization, "Bearer");
			httpWebRequest.Timeout = 3000;
			try
			{
				using (HttpWebResponse httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse)
				{
					if (httpWebResponse != null && httpWebResponse.StatusCode != HttpStatusCode.OK)
					{
						throw new Exception(string.Format("Http status code is {0}", httpWebResponse.StatusCode));
					}
					using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
					{
						text = streamReader.ReadToEnd();
					}
				}
			}
			catch (OAuthTokenRequestFailedException ex)
			{
				this.LogError("[GetInlineExploreSpContent.QuerySharePoint(OAuthCredentials oAuthCredential)]:OAuthException", oAuthCredential.Tracer.ToString() + ex.ToString(), value.ToString());
			}
			catch (WebException ex2)
			{
				this.LogError("[GetInlineExploreSpContent.QuerySharePoint(OAuthCredentials oAuthCredential)]:WebException", ex2.ToString(), value.ToString());
			}
			catch (Exception ex3)
			{
				this.LogError("[GetInlineExploreSpContent.QuerySharePoint(OAuthCredentials oAuthCredential)]:Exception", ex3.ToString(), value.ToString());
			}
			if (string.IsNullOrEmpty(text))
			{
				this.LogError("[GetInlineExploreSpContent.QuerySharePoint(OAuthCredentials oAuthCredential)]:No content in response", "Expected response from SharePoint", value.ToString());
			}
			return text;
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x00060CB8 File Offset: 0x0005EEB8
		private string ConstructTargetUrl(string url, string query)
		{
			string result = string.Empty;
			if (url == null)
			{
				this.LogError("[ConstructTargetUrl(string url, string query)]", "Empty url");
				return result;
			}
			query = query.Replace("'", "''");
			try
			{
				Uri uri = new Uri(url);
				result = string.Format("{0}://{1}/_api/search/query?querytext='{2}'", uri.Scheme, uri.Authority, query);
			}
			catch (UriFormatException ex)
			{
				this.LogError("[ConstructTargetUrl(string url, string query)]:UriFormatException", ex.ToString());
				return string.Empty;
			}
			return result;
		}

		// Token: 0x06001A46 RID: 6726 RVA: 0x00060D40 File Offset: 0x0005EF40
		private void LogError(string errorType, string errorDetails)
		{
			this.LogError(errorType, errorDetails, null);
		}

		// Token: 0x06001A47 RID: 6727 RVA: 0x00060D4C File Offset: 0x0005EF4C
		private void LogError(string errorType, string errorDetails, string clientRequestId)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("Diagnostics for GetInlineExploreSpContentCommand:");
			if (clientRequestId != null)
			{
				stringBuilder.AppendLine(string.Format("Client RequestId: {0}", clientRequestId));
			}
			stringBuilder.AppendLine(errorType);
			stringBuilder.AppendLine(errorDetails);
			ExTraceGlobals.InlineExploreTracer.TraceError((long)this.GetHashCode(), stringBuilder.ToString());
		}

		// Token: 0x04000E8F RID: 3727
		private const string componentDiagnosticIdentifier = "Diagnostics for GetInlineExploreSpContentCommand:";

		// Token: 0x04000E90 RID: 3728
		private readonly XNamespace d = "http://schemas.microsoft.com/ado/2007/08/dataservices";

		// Token: 0x04000E91 RID: 3729
		private readonly CallContext currentCallContext;

		// Token: 0x02000317 RID: 791
		private sealed class GetInlineExploreSpContentOauthOutboundTracer : IOutboundTracer
		{
			// Token: 0x06001A4D RID: 6733 RVA: 0x00060DA7 File Offset: 0x0005EFA7
			private void Log(string prefix, int hashCode, string formatString, params object[] args)
			{
				this.result.Append(prefix);
				this.result.AppendLine(string.Format(formatString, args));
			}

			// Token: 0x06001A4E RID: 6734 RVA: 0x00060DCA File Offset: 0x0005EFCA
			public void LogInformation(int hashCode, string formatString, params object[] args)
			{
				this.Log("Information: ", hashCode, formatString, args);
			}

			// Token: 0x06001A4F RID: 6735 RVA: 0x00060DDA File Offset: 0x0005EFDA
			public void LogWarning(int hashCode, string formatString, params object[] args)
			{
				this.Log("Warning: ", hashCode, formatString, args);
			}

			// Token: 0x06001A50 RID: 6736 RVA: 0x00060DEA File Offset: 0x0005EFEA
			public void LogError(int hashCode, string formatString, params object[] args)
			{
				this.Log("Error: ", hashCode, formatString, args);
			}

			// Token: 0x06001A51 RID: 6737 RVA: 0x00060DFA File Offset: 0x0005EFFA
			public void LogToken(int hashCode, string tokenString)
			{
				this.result.Append("Token: ");
				this.result.AppendLine(tokenString);
			}

			// Token: 0x06001A52 RID: 6738 RVA: 0x00060E1A File Offset: 0x0005F01A
			public override string ToString()
			{
				return this.result.ToString();
			}

			// Token: 0x04000E93 RID: 3731
			private readonly StringBuilder result = new StringBuilder();
		}
	}
}
