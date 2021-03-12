using System;
using System.Net;
using System.Text.RegularExpressions;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000078 RID: 120
	internal class AzureReadRegistrationResponse : AzureResponse
	{
		// Token: 0x06000429 RID: 1065 RVA: 0x0000DDCF File Offset: 0x0000BFCF
		public AzureReadRegistrationResponse(string responseBody, WebHeaderCollection responseHeaders = null) : base(responseBody, responseHeaders)
		{
			this.ProcessResponse();
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000DDDF File Offset: 0x0000BFDF
		public AzureReadRegistrationResponse(Exception exception, Uri targetUri, string responseBody) : base(exception, targetUri, responseBody)
		{
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0000DDEA File Offset: 0x0000BFEA
		public AzureReadRegistrationResponse(WebException webException, Uri targetUri, string responseBody) : base(webException, targetUri, responseBody)
		{
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x0000DDF5 File Offset: 0x0000BFF5
		// (set) Token: 0x0600042D RID: 1069 RVA: 0x0000DDFD File Offset: 0x0000BFFD
		public string RegistrationId { get; private set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x0000DE06 File Offset: 0x0000C006
		// (set) Token: 0x0600042F RID: 1071 RVA: 0x0000DE0E File Offset: 0x0000C00E
		public ExDateTime ExpirationTimeUtc { get; private set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x0000DE17 File Offset: 0x0000C017
		// (set) Token: 0x06000431 RID: 1073 RVA: 0x0000DE1F File Offset: 0x0000C01F
		public bool HasRegistration { get; private set; }

		// Token: 0x06000432 RID: 1074 RVA: 0x0000DE28 File Offset: 0x0000C028
		protected override string InternalToTraceString()
		{
			return string.Format("HasRegistration:{0}; ExpirationTimeUtc:{1}; RegistrationId:{2}", this.HasRegistration, this.ExpirationTimeUtc, this.RegistrationId);
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0000DE50 File Offset: 0x0000C050
		private void ProcessResponse()
		{
			if (!base.HasSucceeded)
			{
				return;
			}
			this.HasRegistration = (base.OriginalBody.IndexOf("<content type=\"application/xml\">", StringComparison.InvariantCultureIgnoreCase) != -1);
			if (this.HasRegistration)
			{
				Match match = AzureReadRegistrationResponse.RegistrationIdRegex.Match(base.OriginalBody);
				if (match.Success)
				{
					this.RegistrationId = match.Value;
				}
				match = AzureReadRegistrationResponse.ExpirationTimeRegex.Match(base.OriginalBody);
				if (match.Success)
				{
					this.ExpirationTimeUtc = ExDateTime.Parse(match.Value);
				}
			}
		}

		// Token: 0x040001EB RID: 491
		private const string RegistrationXMLContentTag = "<content type=\"application/xml\">";

		// Token: 0x040001EC RID: 492
		private static readonly Regex RegistrationIdRegex = new Regex("(?<=RegistrationId>)((?i)[A-F0-9\\-])*(?=\\<\\/RegistrationId)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x040001ED RID: 493
		private static readonly Regex ExpirationTimeRegex = new Regex("(?<=ExpirationTime>)[\\S]*?(?=\\<\\/ExpirationTime)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
	}
}
