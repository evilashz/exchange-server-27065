using System;
using System.Net;
using System.Security.Principal;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000495 RID: 1173
	internal static class XProxySmtpCommandParser
	{
		// Token: 0x0600353B RID: 13627 RVA: 0x000D9C74 File Offset: 0x000D7E74
		public static ParseResult ParseArguments(CommandContext commandContext, bool allowUtf8, out string sessionId, out IPAddress clientIP, out int clientPort, out string clientHelloDomain, out SecurityIdentifier securityId, out SmtpAddress clientProxyAddress, out byte[] redactedBuffer, out int? capabilitiesInt)
		{
			ArgumentValidator.ThrowIfNull("CommandContext", commandContext);
			uint num;
			uint? num2;
			AuthenticationSource? authenticationSource;
			return XProxyParserUtils.ParseXProxyAndXProxyFromArguments(commandContext, false, allowUtf8, out sessionId, out clientIP, out clientPort, out clientHelloDomain, out num, out num2, out authenticationSource, out securityId, out clientProxyAddress, out redactedBuffer, out capabilitiesInt);
		}

		// Token: 0x04001B38 RID: 6968
		public const string CommandKeyword = "XPROXY";
	}
}
