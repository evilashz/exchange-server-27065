using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x02000020 RID: 32
	internal sealed class Imap4RequestCapability : Imap4Request
	{
		// Token: 0x06000199 RID: 409 RVA: 0x0000AB96 File Offset: 0x00008D96
		public Imap4RequestCapability(Imap4ResponseFactory factory, string tag, string data) : base(factory, tag, data)
		{
			this.PerfCounterTotal = base.Imap4CountersInstance.PerfCounter_CAPABILITY_Total;
			this.PerfCounterFailures = base.Imap4CountersInstance.PerfCounter_CAPABILITY_Failures;
			base.AllowedStates = (Imap4State.Nonauthenticated | Imap4State.Authenticated | Imap4State.Selected | Imap4State.AuthenticatedButFailed);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000ABCC File Offset: 0x00008DCC
		public override ProtocolResponse Process()
		{
			Imap4Response imap4Response = new Imap4Response(this, Imap4Response.Type.ok);
			bool isTls = base.Factory.Session.IsTls;
			bool gssapiandNTLMAuthDisabled = base.Factory.Session.Server.GSSAPIAndNTLMAuthDisabled;
			bool flag = base.Factory.Session.VirtualServer.Certificate != null;
			imap4Response.Append("* CAPABILITY IMAP4 IMAP4rev1");
			switch (base.Session.Server.LoginType)
			{
			case LoginOptions.PlainTextLogin:
				imap4Response.Append(" AUTH=PLAIN");
				if (!gssapiandNTLMAuthDisabled)
				{
					imap4Response.Append(" AUTH=NTLM AUTH=GSSAPI");
				}
				if (!isTls && flag)
				{
					imap4Response.Append(" STARTTLS");
				}
				break;
			case LoginOptions.PlainTextAuthentication:
				if (isTls)
				{
					imap4Response.Append(" AUTH=PLAIN");
				}
				else
				{
					imap4Response.Append(" LOGINDISABLED");
					if (flag)
					{
						imap4Response.Append(" STARTTLS");
					}
				}
				if (!gssapiandNTLMAuthDisabled)
				{
					imap4Response.Append(" AUTH=NTLM AUTH=GSSAPI");
				}
				break;
			case LoginOptions.SecureLogin:
				if (!isTls)
				{
					imap4Response.Append(" LOGINDISABLED");
					if (flag)
					{
						imap4Response.Append(" STARTTLS");
					}
				}
				else
				{
					imap4Response.Append(" AUTH=PLAIN");
					if (!gssapiandNTLMAuthDisabled)
					{
						imap4Response.Append(" AUTH=NTLM AUTH=GSSAPI");
					}
				}
				break;
			}
			if (base.SupportUidPlus)
			{
				imap4Response.Append(" UIDPLUS");
			}
			if ((base.Factory.MoveEnabled && ProtocolBaseServices.ServerRoleService == ServerServiceRole.mailbox) || (base.Factory.MoveEnabledCafe && ProtocolBaseServices.ServerRoleService == ServerServiceRole.cafe))
			{
				imap4Response.Append(" MOVE");
			}
			if ((base.Factory.IDEnabled && ProtocolBaseServices.ServerRoleService == ServerServiceRole.mailbox) || (base.Factory.IDEnabledCafe && ProtocolBaseServices.ServerRoleService == ServerServiceRole.cafe))
			{
				imap4Response.Append(" ID");
			}
			if (ProtocolBaseServices.ServerRoleService == ServerServiceRole.mailbox)
			{
				if (ResponseFactory.GetClientAccessRulesEnabled())
				{
					imap4Response.Append(" CLIENTACCESSRULES");
				}
				if (!gssapiandNTLMAuthDisabled)
				{
					imap4Response.Append(" XPROXY3");
				}
			}
			imap4Response.Append(" CHILDREN IDLE NAMESPACE LITERAL+\r\n[*] CAPABILITY completed.");
			return imap4Response;
		}

		// Token: 0x04000123 RID: 291
		private const string CAPABILITYResponseBeginning = "* CAPABILITY IMAP4 IMAP4rev1";

		// Token: 0x04000124 RID: 292
		private const string CAPABILITYResponseEnd = " CHILDREN IDLE NAMESPACE LITERAL+\r\n[*] CAPABILITY completed.";

		// Token: 0x04000125 RID: 293
		private const string CAPABILITYResponseSecureOnly = " AUTH=NTLM AUTH=GSSAPI";

		// Token: 0x04000126 RID: 294
		private const string CAPABILITYResponsePLAIN = " AUTH=PLAIN";

		// Token: 0x04000127 RID: 295
		private const string CAPABILITYResponseSTLS = " STARTTLS";

		// Token: 0x04000128 RID: 296
		private const string CAPABILITYResponseUIDPLUS = " UIDPLUS";

		// Token: 0x04000129 RID: 297
		private const string CAPABILITYResponseMOVE = " MOVE";

		// Token: 0x0400012A RID: 298
		private const string CAPABILITYResponseID = " ID";

		// Token: 0x0400012B RID: 299
		private const string CAPABILITYResponseLOGINDISABLED = " LOGINDISABLED";

		// Token: 0x0400012C RID: 300
		private const string CAPABILITYResponseClientAccessRules = " CLIENTACCESSRULES";

		// Token: 0x0400012D RID: 301
		private const string CAPABILITYResponseXPROXY3 = " XPROXY3";
	}
}
