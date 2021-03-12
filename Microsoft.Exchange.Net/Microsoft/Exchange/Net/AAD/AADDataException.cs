using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Net.AAD
{
	// Token: 0x02000585 RID: 1413
	internal sealed class AADDataException : AADException
	{
		// Token: 0x170003AD RID: 941
		// (get) Token: 0x060012B6 RID: 4790 RVA: 0x0002A590 File Offset: 0x00028790
		// (set) Token: 0x060012B7 RID: 4791 RVA: 0x0002A598 File Offset: 0x00028798
		public AADDataException.AADCode Code { get; private set; }

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x060012B8 RID: 4792 RVA: 0x0002A5A1 File Offset: 0x000287A1
		// (set) Token: 0x060012B9 RID: 4793 RVA: 0x0002A5A9 File Offset: 0x000287A9
		public bool IsRetryable { get; private set; }

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x060012BA RID: 4794 RVA: 0x0002A5B2 File Offset: 0x000287B2
		public override string Message
		{
			get
			{
				return this.errorMessage.ToString();
			}
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x0002A5C0 File Offset: 0x000287C0
		public AADDataException(Exception exception) : base(new LocalizedString(exception.Message), exception)
		{
			this.IsRetryable = AADDataException.IsRetryableError(exception);
			if (exception is DataServiceClientException)
			{
				this.ParseErrorXml(exception.Message);
			}
			else if (exception.InnerException != null && exception.InnerException is DataServiceClientException)
			{
				this.ParseErrorXml(exception.InnerException.Message);
			}
			if (this.errorMessage.Length == 0)
			{
				this.errorMessage.AppendLine(exception.Message);
				if (exception.InnerException != null)
				{
					this.errorMessage.AppendLine(exception.InnerException.Message);
				}
			}
			DataServiceRequestException ex = exception as DataServiceRequestException;
			if (ex != null && ex.Response != null)
			{
				if (ex.Response.IsBatchResponse)
				{
					this.AddHeadersToErrorMessage(ex.Response.BatchHeaders);
					return;
				}
				foreach (OperationResponse operationResponse in ex.Response)
				{
					this.AddHeadersToErrorMessage(operationResponse.Headers);
				}
			}
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x0002A6E8 File Offset: 0x000288E8
		private static bool IsRetryableError(Exception exception)
		{
			HttpStatusCode? httpStatusCode = null;
			DataServiceRequestException ex;
			DataServiceClientException ex2;
			DataServiceQueryException ex3;
			if ((ex = (exception as DataServiceRequestException)) != null)
			{
				OperationResponse operationResponse = ex.Response.FirstOrDefault<OperationResponse>();
				httpStatusCode = new HttpStatusCode?((HttpStatusCode)((operationResponse != null) ? operationResponse.StatusCode : ex.Response.BatchStatusCode));
			}
			else if ((ex2 = (exception as DataServiceClientException)) != null)
			{
				httpStatusCode = new HttpStatusCode?((HttpStatusCode)ex2.StatusCode);
			}
			else if ((ex3 = (exception as DataServiceQueryException)) != null)
			{
				httpStatusCode = new HttpStatusCode?((HttpStatusCode)ex3.Response.StatusCode);
			}
			else
			{
				WebException ex4 = exception as WebException;
				if (ex4 != null || (ex4 = (exception.InnerException as WebException)) != null)
				{
					HttpWebResponse httpWebResponse = ex4.Response as HttpWebResponse;
					if (httpWebResponse == null)
					{
						return ex4.Status == WebExceptionStatus.NameResolutionFailure;
					}
					httpStatusCode = new HttpStatusCode?(httpWebResponse.StatusCode);
				}
			}
			return httpStatusCode != null && (httpStatusCode == HttpStatusCode.InternalServerError || httpStatusCode == HttpStatusCode.BadGateway || httpStatusCode == HttpStatusCode.ServiceUnavailable);
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x0002A814 File Offset: 0x00028A14
		private void ParseErrorXml(string xml)
		{
			try
			{
				using (XmlTextReader xmlTextReader = SafeXmlFactory.CreateSafeXmlTextReader(new StringReader(xml)))
				{
					if (xmlTextReader.ReadToFollowing("code", "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata"))
					{
						string key = xmlTextReader.ReadElementContentAsString();
						AADDataException.AADCode code;
						if (AADDataException.CodeMapping.TryGetValue(key, out code))
						{
							this.Code = code;
							this.errorMessage.AppendLine(this.Code.ToString());
						}
					}
					if (xmlTextReader.LocalName == "message" || xmlTextReader.ReadToFollowing("message", "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata"))
					{
						this.errorMessage.AppendLine(xmlTextReader.ReadElementContentAsString());
					}
				}
			}
			catch (XmlException)
			{
			}
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x0002A8DC File Offset: 0x00028ADC
		private void AddHeadersToErrorMessage(IDictionary<string, string> headers)
		{
			if (headers == null)
			{
				return;
			}
			foreach (string text in AADDataException.HeadersToIncludeInErrorMessage)
			{
				string str;
				if (headers.TryGetValue(text, out str))
				{
					this.errorMessage.AppendLine(text + ": " + str);
				}
			}
		}

		// Token: 0x0400185F RID: 6239
		private static readonly string[] HeadersToIncludeInErrorMessage = new string[]
		{
			"request-id",
			"Date",
			"ocp-aad-diagnostics-server-name"
		};

		// Token: 0x04001860 RID: 6240
		private StringBuilder errorMessage = new StringBuilder();

		// Token: 0x04001861 RID: 6241
		private static Dictionary<string, AADDataException.AADCode> CodeMapping = new Dictionary<string, AADDataException.AADCode>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"Request_BadRequest",
				AADDataException.AADCode.Request_BadRequest
			},
			{
				"Request_UnsupportedQuery",
				AADDataException.AADCode.Request_UnsupportedQuery
			},
			{
				"Directory_ResultSizeLimitExceeded",
				AADDataException.AADCode.Directory_ResultSizeLimitExceeded
			},
			{
				"Authentication_MissingOrMalformed",
				AADDataException.AADCode.Authentication_MissingOrMalformed
			},
			{
				"Authorization_IdentityNotFound",
				AADDataException.AADCode.Authorization_IdentityNotFound
			},
			{
				"Authorization_IdentityDisabled",
				AADDataException.AADCode.Authorization_IdentityDisabled
			},
			{
				"Authentication_ExpiredToken",
				AADDataException.AADCode.Authentication_ExpiredToken
			},
			{
				"Authorization_RequestDenied",
				AADDataException.AADCode.Authorization_RequestDenied
			},
			{
				"Authentication_Unauthorized",
				AADDataException.AADCode.Authentication_Unauthorized
			},
			{
				"Directory_QuotaExceeded",
				AADDataException.AADCode.Directory_QuotaExceeded
			},
			{
				"Request_ResourceNotFound",
				AADDataException.AADCode.Request_ResourceNotFound
			},
			{
				"Service_InternalServerError",
				AADDataException.AADCode.Service_InternalServerError
			},
			{
				"Request_ThrottledTemporarily",
				AADDataException.AADCode.Request_ThrottledTemporarily
			}
		};

		// Token: 0x02000586 RID: 1414
		public enum AADCode
		{
			// Token: 0x04001865 RID: 6245
			Unknown,
			// Token: 0x04001866 RID: 6246
			Request_BadRequest,
			// Token: 0x04001867 RID: 6247
			Request_UnsupportedQuery,
			// Token: 0x04001868 RID: 6248
			Directory_ResultSizeLimitExceeded,
			// Token: 0x04001869 RID: 6249
			Authentication_MissingOrMalformed,
			// Token: 0x0400186A RID: 6250
			Authorization_IdentityNotFound,
			// Token: 0x0400186B RID: 6251
			Authorization_IdentityDisabled,
			// Token: 0x0400186C RID: 6252
			Authentication_ExpiredToken,
			// Token: 0x0400186D RID: 6253
			Authorization_RequestDenied,
			// Token: 0x0400186E RID: 6254
			Authentication_Unauthorized,
			// Token: 0x0400186F RID: 6255
			Directory_QuotaExceeded,
			// Token: 0x04001870 RID: 6256
			Request_ResourceNotFound,
			// Token: 0x04001871 RID: 6257
			Service_InternalServerError,
			// Token: 0x04001872 RID: 6258
			Request_ThrottledTemporarily
		}
	}
}
