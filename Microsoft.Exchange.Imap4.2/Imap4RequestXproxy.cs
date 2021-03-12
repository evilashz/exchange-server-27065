using System;
using System.Security;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Security;
using Microsoft.Exchange.PopImap.Core;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x0200004B RID: 75
	internal sealed class Imap4RequestXproxy : Imap4RequestWithStringParameters, IProxyLogin
	{
		// Token: 0x060002A1 RID: 673 RVA: 0x00013980 File Offset: 0x00011B80
		public Imap4RequestXproxy(Imap4ResponseFactory factory, string tag, string data) : base(factory, tag, data, 0, 0)
		{
			this.PerfCounterTotal = base.Imap4CountersInstance.PerfCounter_XPROXY_Total;
			this.PerfCounterFailures = base.Imap4CountersInstance.PerfCounter_XPROXY_Failures;
			base.AllowedStates = Imap4State.None;
			if (ProtocolBaseServices.GCCEnabledWithKeys)
			{
				base.AllowedStates |= Imap4State.Authenticated;
				base.MinNumberOfArguments = 3;
				base.MaxNumberOfArguments = 3;
			}
			if (ProtocolBaseServices.ServerRoleService == ServerServiceRole.mailbox && !factory.Session.Server.GSSAPIAndNTLMAuthDisabled)
			{
				base.AllowedStates |= Imap4State.AuthenticatedAsCafe;
				base.MaxNumberOfArguments += 4;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x00013A1B File Offset: 0x00011C1B
		// (set) Token: 0x060002A3 RID: 675 RVA: 0x00013A23 File Offset: 0x00011C23
		public string UserName { get; set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x00013A2C File Offset: 0x00011C2C
		// (set) Token: 0x060002A5 RID: 677 RVA: 0x00013A34 File Offset: 0x00011C34
		public SecureString Password { get; set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x00013A3D File Offset: 0x00011C3D
		// (set) Token: 0x060002A7 RID: 679 RVA: 0x00013A45 File Offset: 0x00011C45
		public string ClientIp { get; set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x00013A4E File Offset: 0x00011C4E
		// (set) Token: 0x060002A9 RID: 681 RVA: 0x00013A56 File Offset: 0x00011C56
		public string ClientPort { get; set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060002AA RID: 682 RVA: 0x00013A5F File Offset: 0x00011C5F
		// (set) Token: 0x060002AB RID: 683 RVA: 0x00013A67 File Offset: 0x00011C67
		public string AuthenticationType { get; set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060002AC RID: 684 RVA: 0x00013A70 File Offset: 0x00011C70
		// (set) Token: 0x060002AD RID: 685 RVA: 0x00013A78 File Offset: 0x00011C78
		public string AuthenticationError { get; set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060002AE RID: 686 RVA: 0x00013A81 File Offset: 0x00011C81
		// (set) Token: 0x060002AF RID: 687 RVA: 0x00013A89 File Offset: 0x00011C89
		public LiveIdAuthResult AuthResult { get; set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x00013A92 File Offset: 0x00011C92
		// (set) Token: 0x060002B1 RID: 689 RVA: 0x00013A9A File Offset: 0x00011C9A
		public string ProxyDestination { get; set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x00013AA3 File Offset: 0x00011CA3
		// (set) Token: 0x060002B3 RID: 691 RVA: 0x00013AAB File Offset: 0x00011CAB
		public ILiveIdBasicAuthentication LiveIdBasicAuth { get; set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x00013AB4 File Offset: 0x00011CB4
		// (set) Token: 0x060002B5 RID: 693 RVA: 0x00013ABC File Offset: 0x00011CBC
		public ADUser AdUser { get; set; }

		// Token: 0x060002B6 RID: 694 RVA: 0x00013AC8 File Offset: 0x00011CC8
		public override ProtocolResponse Process()
		{
			int count = base.ArrayOfArguments.Count;
			if (base.Factory == null)
			{
				ProtocolBaseServices.SessionTracer.TraceError(base.Session.SessionId, "XPROXY: Factory is null");
				return new Imap4Response(this, Imap4Response.Type.no, "XPROXY failed.");
			}
			if (ProtocolBaseServices.GCCEnabledWithKeys)
			{
				try
				{
					if (!GccUtils.SetStoreSessionClientIPEndpointsFromXproxy(base.Factory.Store, base.ArrayOfArguments[count - 3], base.ArrayOfArguments[count - 2], base.ArrayOfArguments[count - 1], base.Session.Connection))
					{
						ProtocolBaseServices.SessionTracer.TraceError(base.Session.SessionId, "XPROXY: SetStoreSessionClientIPEndpointsFromXproxy failed");
						return new Imap4Response(this, Imap4Response.Type.no, "XPROXY failed.");
					}
					this.ClientIp = base.ArrayOfArguments[count - 2];
					if (base.Session.LrsSession != null)
					{
						base.Session.LrsSession.SetEndpoints(base.ArrayOfArguments[count - 2], base.ArrayOfArguments[count - 1]);
					}
				}
				catch (InvalidDatacenterProxyKeyException ex)
				{
					ProtocolBaseServices.LogEvent(Imap4EventLogConstants.Tuple_InvalidDatacenterProxyKey, null, new string[]
					{
						ex.Message
					});
					ProtocolBaseServices.SessionTracer.TraceError<string>(base.Session.SessionId, "XPROXY: InvalidDatacenterProxyKeyException {0}", ex.Message);
					return new Imap4Response(this, Imap4Response.Type.no, "XPROXY failed.");
				}
			}
			if (base.Factory.SessionState == Imap4State.AuthenticatedAsCafe)
			{
				if (count < 4)
				{
					ProtocolBaseServices.SessionTracer.TraceError(base.Session.SessionId, "XPROXY3 needs at least 4 arguments");
					return new Imap4Response(this, Imap4Response.Type.bad, "Command Argument Error. 12");
				}
				try
				{
					this.UserName = base.ArrayOfArguments[0];
					base.Factory.IncompleteRequest = this;
					if (!base.Factory.TryToConnect(base.ArrayOfArguments[1], base.ArrayOfArguments[2], base.ArrayOfArguments[3]))
					{
						ProtocolBaseServices.SessionTracer.TraceError(base.Session.SessionId, "XPROXY3 failed to connect to user's mailbox");
						return new Imap4Response(this, Imap4Response.Type.no, "XPROXY failed.");
					}
					base.Factory.SessionState = Imap4State.Authenticated;
				}
				finally
				{
					base.Factory.IncompleteRequest = null;
				}
			}
			return new Imap4Response(this, Imap4Response.Type.ok, "XPROXY completed.");
		}

		// Token: 0x04000207 RID: 519
		internal const string XPROXYResponseCompleted = "XPROXY completed.";

		// Token: 0x04000208 RID: 520
		internal const string XPROXYResponseFailed = "XPROXY failed.";
	}
}
