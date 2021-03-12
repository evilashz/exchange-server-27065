using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Pop3
{
	// Token: 0x0200000A RID: 10
	internal sealed class Pop3RequestCapa : Pop3Request
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00003258 File Offset: 0x00001458
		public Pop3RequestCapa(Pop3ResponseFactory factory, string input) : base(factory, input)
		{
			this.PerfCounterTotal = base.Pop3CountersInstance.PerfCounter_CAPA_Total;
			this.PerfCounterFailures = base.Pop3CountersInstance.PerfCounter_CAPA_Failures;
			base.AllowedStates = Pop3State.All;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000328C File Offset: 0x0000148C
		public override ProtocolResponse Process()
		{
			ProtocolResponse protocolResponse = new Pop3Response(Pop3Response.Type.unknown);
			bool isTls = base.Factory.Session.IsTls;
			bool gssapiandNTLMAuthDisabled = base.Factory.Session.Server.GSSAPIAndNTLMAuthDisabled;
			bool flag = base.Factory.Session.VirtualServer.Certificate != null;
			protocolResponse.Append("+OK\r\nTOP\r\nUIDL\r\n");
			switch (base.Factory.Session.Server.LoginType)
			{
			case LoginOptions.PlainTextLogin:
				if (gssapiandNTLMAuthDisabled)
				{
					protocolResponse.Append("SASL PLAIN\r\n");
				}
				else
				{
					protocolResponse.Append("SASL NTLM GSSAPI PLAIN\r\n");
				}
				protocolResponse.Append("USER\r\n");
				if (!isTls && flag)
				{
					protocolResponse.Append("STLS\r\n");
				}
				break;
			case LoginOptions.PlainTextAuthentication:
				if (isTls)
				{
					if (gssapiandNTLMAuthDisabled)
					{
						protocolResponse.Append("SASL PLAIN\r\n");
					}
					else
					{
						protocolResponse.Append("SASL NTLM GSSAPI PLAIN\r\n");
					}
					protocolResponse.Append("USER\r\n");
				}
				else
				{
					if (!gssapiandNTLMAuthDisabled)
					{
						protocolResponse.Append("SASL NTLM GSSAPI\r\n");
					}
					if (flag)
					{
						protocolResponse.Append("STLS\r\n");
					}
				}
				break;
			case LoginOptions.SecureLogin:
				if (!isTls)
				{
					if (flag)
					{
						protocolResponse.Append("STLS\r\n");
					}
				}
				else
				{
					if (gssapiandNTLMAuthDisabled)
					{
						protocolResponse.Append("SASL PLAIN\r\n");
					}
					else
					{
						protocolResponse.Append("SASL NTLM GSSAPI PLAIN\r\n");
					}
					protocolResponse.Append("USER\r\n");
				}
				break;
			}
			if (ProtocolBaseServices.ServerRoleService == ServerServiceRole.mailbox && ResponseFactory.GetClientAccessRulesEnabled())
			{
				protocolResponse.Append("CLIENTACCESSRULES\r\n");
			}
			protocolResponse.Append(".");
			return protocolResponse;
		}

		// Token: 0x04000037 RID: 55
		internal const string CAPAResponseBeginning = "+OK\r\nTOP\r\nUIDL\r\n";

		// Token: 0x04000038 RID: 56
		internal const string CAPAResponseEnd = ".";

		// Token: 0x04000039 RID: 57
		internal const string CAPAResponseBASIC = "USER\r\n";

		// Token: 0x0400003A RID: 58
		internal const string CAPAResponseSASLALL = "SASL NTLM GSSAPI PLAIN\r\n";

		// Token: 0x0400003B RID: 59
		internal const string CAPAResponseSASLSECURE = "SASL NTLM GSSAPI\r\n";

		// Token: 0x0400003C RID: 60
		internal const string CAPAResponseSASLLiveId = "SASL PLAIN\r\n";

		// Token: 0x0400003D RID: 61
		internal const string CAPAResponseSTLS = "STLS\r\n";

		// Token: 0x0400003E RID: 62
		private const string CAPAResponseClientAccessRules = "CLIENTACCESSRULES\r\n";
	}
}
