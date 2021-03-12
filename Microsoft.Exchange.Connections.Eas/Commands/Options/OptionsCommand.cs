using System;
using System.Linq;
using System.Net;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands.Options
{
	// Token: 0x0200005B RID: 91
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class OptionsCommand : EasPseudoCommand<OptionsRequest, OptionsResponse>
	{
		// Token: 0x060001A4 RID: 420 RVA: 0x00005164 File Offset: 0x00003364
		protected internal OptionsCommand(EasConnectionSettings easConnectionSettings) : base(Command.Options, easConnectionSettings)
		{
			string domain = base.EasConnectionSettings.EasEndpointSettings.Domain;
			base.UriString = string.Format("{0}//{1}/Microsoft-Server-ActiveSync", this.UseSsl ? "https:" : "http:", domain);
			base.InitializeExpectedHttpStatusCodes(typeof(HttpStatus));
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x000051C0 File Offset: 0x000033C0
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x000051C8 File Offset: 0x000033C8
		internal OptionsStatus OptionsStatus { get; private set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x000051D1 File Offset: 0x000033D1
		protected override string RequestMethodName
		{
			get
			{
				return "OPTIONS";
			}
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x000051D8 File Offset: 0x000033D8
		protected override void AddWebRequestHeaders(HttpWebRequest webRequest)
		{
			webRequest.Headers.Add("MS-ASProtocolVersion", base.ProtocolVersion);
			webRequest.Host = base.EasConnectionSettings.EasEndpointSettings.Domain;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00005206 File Offset: 0x00003406
		protected override void AddWebRequestBody(HttpWebRequest webRequest, OptionsRequest easRequest)
		{
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00005220 File Offset: 0x00003420
		protected override OptionsResponse ExtractResponse(HttpWebResponse webResponse)
		{
			base.LogInfoHeaders(webResponse.Headers);
			string responseHeader = webResponse.GetResponseHeader("MS-ASProtocolCommands");
			string responseHeader2 = webResponse.GetResponseHeader("MS-ASProtocolVersions");
			string responseHeader3 = webResponse.GetResponseHeader("X-OLK-Extensions");
			if (string.IsNullOrWhiteSpace(responseHeader) || string.IsNullOrWhiteSpace(responseHeader2))
			{
				return new OptionsResponse
				{
					OptionsStatus = OptionsStatus.MissingHeaderInResponse
				};
			}
			string[] first = (from sValue in responseHeader.Split(new char[]
			{
				','
			})
			select sValue.Trim()).ToArray<string>();
			string[] second = (from sValue in responseHeader2.Split(new char[]
			{
				','
			})
			select sValue.Trim()).ToArray<string>();
			string[] capabilities = (from sValue in responseHeader3.Split(new char[]
			{
				','
			})
			select sValue.Trim()).ToArray<string>();
			EasServerCapabilities easServerCapabilities = new EasServerCapabilities(first.Union(second));
			EasExtensionCapabilities easExtensionCapabilities = new EasExtensionCapabilities(capabilities);
			this.OptionsStatus = OptionsStatus.Success;
			return new OptionsResponse
			{
				OptionsStatus = OptionsStatus.Success,
				EasServerCapabilities = easServerCapabilities,
				EasExtensionCapabilities = easExtensionCapabilities
			};
		}

		// Token: 0x04000183 RID: 387
		private const string UriFormatString = "{0}//{1}/Microsoft-Server-ActiveSync";
	}
}
