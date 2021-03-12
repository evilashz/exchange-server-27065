using System;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.SecureMail;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x0200044F RID: 1103
	internal abstract class SmtpCommand : ILegacySmtpCommand, IDisposeTrackable, IDisposable
	{
		// Token: 0x060032C3 RID: 12995 RVA: 0x000C7A30 File Offset: 0x000C5C30
		protected SmtpCommand(ISmtpSession session, string protocolCommandKeyword, string commandEventString, LatencyComponent commandEventComponent)
		{
			this.session = session;
			this.commandEventString = commandEventString;
			this.commandEventComponent = commandEventComponent;
			this.isResponseReady = true;
			this.protocolCommandKeyword = protocolCommandKeyword;
			ISmtpInSession smtpInSession = this.session as ISmtpInSession;
			if (smtpInSession != null)
			{
				this.tarpitInterval = smtpInSession.Connector.TarpitInterval;
			}
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x060032C4 RID: 12996 RVA: 0x000C7AA3 File Offset: 0x000C5CA3
		internal SmtpCommand(string protocolCommandKeyword)
		{
			this.protocolCommandKeyword = protocolCommandKeyword;
		}

		// Token: 0x17000F7C RID: 3964
		// (get) Token: 0x060032C5 RID: 12997 RVA: 0x000C7ABD File Offset: 0x000C5CBD
		// (set) Token: 0x060032C6 RID: 12998 RVA: 0x000C7AC5 File Offset: 0x000C5CC5
		internal bool IsResponseReady
		{
			get
			{
				return this.isResponseReady;
			}
			set
			{
				this.isResponseReady = value;
			}
		}

		// Token: 0x17000F7D RID: 3965
		// (get) Token: 0x060032C7 RID: 12999 RVA: 0x000C7ACE File Offset: 0x000C5CCE
		// (set) Token: 0x060032C8 RID: 13000 RVA: 0x000C7AD6 File Offset: 0x000C5CD6
		protected internal TarpitAction LowAuthenticationLevelTarpitOverride
		{
			get
			{
				return this.lowAuthenticationLevelTarpitOverride;
			}
			set
			{
				this.lowAuthenticationLevelTarpitOverride = value;
			}
		}

		// Token: 0x17000F7E RID: 3966
		// (get) Token: 0x060032C9 RID: 13001 RVA: 0x000C7ADF File Offset: 0x000C5CDF
		// (set) Token: 0x060032CA RID: 13002 RVA: 0x000C7AE7 File Offset: 0x000C5CE7
		protected internal TarpitAction HighAuthenticationLevelTarpitOverride
		{
			get
			{
				return this.highAuthenticationLevelTarpitOverride;
			}
			set
			{
				this.highAuthenticationLevelTarpitOverride = value;
			}
		}

		// Token: 0x17000F7F RID: 3967
		// (get) Token: 0x060032CB RID: 13003 RVA: 0x000C7AF0 File Offset: 0x000C5CF0
		// (set) Token: 0x060032CC RID: 13004 RVA: 0x000C7AF8 File Offset: 0x000C5CF8
		protected internal TimeSpan TarpitInterval
		{
			get
			{
				return this.tarpitInterval;
			}
			set
			{
				this.tarpitInterval = value;
			}
		}

		// Token: 0x17000F80 RID: 3968
		// (get) Token: 0x060032CD RID: 13005 RVA: 0x000C7B01 File Offset: 0x000C5D01
		// (set) Token: 0x060032CE RID: 13006 RVA: 0x000C7B12 File Offset: 0x000C5D12
		protected internal string TarpitReason
		{
			get
			{
				return this.tarpitReason ?? string.Empty;
			}
			set
			{
				this.tarpitReason = value;
			}
		}

		// Token: 0x17000F81 RID: 3969
		// (get) Token: 0x060032CF RID: 13007 RVA: 0x000C7B1B File Offset: 0x000C5D1B
		// (set) Token: 0x060032D0 RID: 13008 RVA: 0x000C7B23 File Offset: 0x000C5D23
		protected internal string TarpitContext
		{
			get
			{
				return this.tarpitContext;
			}
			set
			{
				this.tarpitContext = value;
			}
		}

		// Token: 0x17000F82 RID: 3970
		// (get) Token: 0x060032D1 RID: 13009 RVA: 0x000C7B2C File Offset: 0x000C5D2C
		// (set) Token: 0x060032D2 RID: 13010 RVA: 0x000C7B34 File Offset: 0x000C5D34
		public byte[] ProtocolCommand
		{
			get
			{
				return this.protocolCommand;
			}
			set
			{
				this.protocolCommand = value;
				if (this.protocolCommand != null)
				{
					this.protocolCommandLength = this.protocolCommand.Length;
					return;
				}
				this.protocolCommandLength = 0;
			}
		}

		// Token: 0x17000F83 RID: 3971
		// (get) Token: 0x060032D3 RID: 13011 RVA: 0x000C7B5B File Offset: 0x000C5D5B
		public int ProtocolCommandLength
		{
			get
			{
				return this.protocolCommandLength;
			}
		}

		// Token: 0x17000F84 RID: 3972
		// (get) Token: 0x060032D4 RID: 13012 RVA: 0x000C7B63 File Offset: 0x000C5D63
		internal string ProtocolCommandKeyword
		{
			get
			{
				return this.protocolCommandKeyword;
			}
		}

		// Token: 0x17000F85 RID: 3973
		// (get) Token: 0x060032D5 RID: 13013 RVA: 0x000C7B6B File Offset: 0x000C5D6B
		// (set) Token: 0x060032D6 RID: 13014 RVA: 0x000C7B73 File Offset: 0x000C5D73
		public int CurrentOffset
		{
			get
			{
				return this.currentOffset;
			}
			set
			{
				this.currentOffset = value;
			}
		}

		// Token: 0x17000F86 RID: 3974
		// (get) Token: 0x060032D7 RID: 13015 RVA: 0x000C7B7C File Offset: 0x000C5D7C
		// (set) Token: 0x060032D8 RID: 13016 RVA: 0x000C7B84 File Offset: 0x000C5D84
		internal string ProtocolCommandString
		{
			get
			{
				return this.protocolCommandString;
			}
			set
			{
				this.protocolCommandString = value;
			}
		}

		// Token: 0x17000F87 RID: 3975
		// (get) Token: 0x060032D9 RID: 13017 RVA: 0x000C7B8D File Offset: 0x000C5D8D
		// (set) Token: 0x060032DA RID: 13018 RVA: 0x000C7B95 File Offset: 0x000C5D95
		internal string RedactedProtocolCommandString
		{
			get
			{
				return this.redactedProtocolCommandString;
			}
			set
			{
				this.redactedProtocolCommandString = value;
			}
		}

		// Token: 0x17000F88 RID: 3976
		// (get) Token: 0x060032DB RID: 13019 RVA: 0x000C7B9E File Offset: 0x000C5D9E
		// (set) Token: 0x060032DC RID: 13020 RVA: 0x000C7BA6 File Offset: 0x000C5DA6
		internal SmtpResponse SmtpResponse
		{
			get
			{
				return this.smtpResponse;
			}
			set
			{
				this.smtpResponse = value;
			}
		}

		// Token: 0x17000F89 RID: 3977
		// (get) Token: 0x060032DD RID: 13021 RVA: 0x000C7BAF File Offset: 0x000C5DAF
		// (set) Token: 0x060032DE RID: 13022 RVA: 0x000C7BB7 File Offset: 0x000C5DB7
		internal bool IsResponseBuffered
		{
			get
			{
				return this.isResponseBuffered;
			}
			set
			{
				this.isResponseBuffered = value;
			}
		}

		// Token: 0x17000F8A RID: 3978
		// (get) Token: 0x060032DF RID: 13023 RVA: 0x000C7BC0 File Offset: 0x000C5DC0
		// (set) Token: 0x060032E0 RID: 13024 RVA: 0x000C7BC8 File Offset: 0x000C5DC8
		internal ParsingStatus ParsingStatus
		{
			get
			{
				return this.parsingStatus;
			}
			set
			{
				this.parsingStatus = value;
			}
		}

		// Token: 0x17000F8B RID: 3979
		// (get) Token: 0x060032E1 RID: 13025 RVA: 0x000C7BD1 File Offset: 0x000C5DD1
		internal ISmtpSession SmtpSession
		{
			get
			{
				return this.session;
			}
		}

		// Token: 0x17000F8C RID: 3980
		// (get) Token: 0x060032E2 RID: 13026 RVA: 0x000C7BD9 File Offset: 0x000C5DD9
		internal EventArgs OriginalEventArgsWrapper
		{
			get
			{
				return this.originalEventArgsWrapper;
			}
		}

		// Token: 0x17000F8D RID: 3981
		// (get) Token: 0x060032E3 RID: 13027 RVA: 0x000C7BE1 File Offset: 0x000C5DE1
		internal LatencyComponent CommandEventComponent
		{
			get
			{
				return this.commandEventComponent;
			}
		}

		// Token: 0x17000F8E RID: 3982
		// (get) Token: 0x060032E4 RID: 13028 RVA: 0x000C7BEC File Offset: 0x000C5DEC
		protected bool OnlyCheckMessageSizeAfterEoh
		{
			get
			{
				ISmtpInSession smtpInSession = (ISmtpInSession)this.SmtpSession;
				return SmtpInSessionUtils.HasSMTPAcceptOrgHeadersPermission(smtpInSession.Permissions) || (smtpInSession.SmtpInServer.IsBridgehead && !SmtpInSessionUtils.IsAnonymous(smtpInSession.RemoteIdentity));
			}
		}

		// Token: 0x060032E5 RID: 13029 RVA: 0x000C7C31 File Offset: 0x000C5E31
		public virtual DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SmtpCommand>(this);
		}

		// Token: 0x060032E6 RID: 13030 RVA: 0x000C7C39 File Offset: 0x000C5E39
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060032E7 RID: 13031 RVA: 0x000C7C4E File Offset: 0x000C5E4E
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060032E8 RID: 13032 RVA: 0x000C7C60 File Offset: 0x000C5E60
		public virtual IAsyncResult BeginRaiseEvent(AsyncCallback callback, object state)
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)this.SmtpSession;
			if (this.commandEventString == null)
			{
				return smtpInSession.AgentSession.BeginNoEvent(callback, state);
			}
			if (this.CommandEventArgs != null)
			{
				this.CommandEventArgs.Initialize(smtpInSession.SessionSource);
				this.originalEventArgsWrapper = this.CommandEventArgs;
			}
			return smtpInSession.AgentSession.BeginRaiseEvent(this.commandEventString, ReceiveCommandEventSourceImpl.Create(smtpInSession.SessionSource), this.originalEventArgsWrapper, callback, state);
		}

		// Token: 0x060032E9 RID: 13033 RVA: 0x000C7CD8 File Offset: 0x000C5ED8
		internal static bool CompareArg(byte[] byteEncodedStrBuf, byte[] buffer, int beginOffset, int count)
		{
			return BufferParser.CompareArg(byteEncodedStrBuf, buffer, beginOffset, count);
		}

		// Token: 0x060032EA RID: 13034 RVA: 0x000C7CE3 File Offset: 0x000C5EE3
		internal static string GetBracketedString(string emailAddress)
		{
			if (emailAddress.Length >= 2 && emailAddress[0] == '<' && emailAddress[emailAddress.Length - 1] == '>')
			{
				return emailAddress;
			}
			return "<" + emailAddress + ">";
		}

		// Token: 0x060032EB RID: 13035
		internal abstract void InboundParseCommand();

		// Token: 0x060032EC RID: 13036 RVA: 0x000C7D1D File Offset: 0x000C5F1D
		internal virtual void InboundAgentEventCompleted()
		{
		}

		// Token: 0x060032ED RID: 13037
		internal abstract void InboundProcessCommand();

		// Token: 0x060032EE RID: 13038 RVA: 0x000C7D1F File Offset: 0x000C5F1F
		internal virtual void InboundCompleteCommand()
		{
		}

		// Token: 0x060032EF RID: 13039
		internal abstract void OutboundCreateCommand();

		// Token: 0x060032F0 RID: 13040
		internal abstract void OutboundFormatCommand();

		// Token: 0x060032F1 RID: 13041
		internal abstract void OutboundProcessResponse();

		// Token: 0x060032F2 RID: 13042 RVA: 0x000C7D24 File Offset: 0x000C5F24
		internal bool VerifyHelloReceived()
		{
			ISmtpInSession smtpInSession = this.SmtpSession as ISmtpInSession;
			if (smtpInSession != null && !smtpInSession.SeenHelo)
			{
				this.SmtpResponse = SmtpResponse.NeedHello;
				this.ParsingStatus = ParsingStatus.ProtocolError;
				return false;
			}
			return true;
		}

		// Token: 0x060032F3 RID: 13043 RVA: 0x000C7D60 File Offset: 0x000C5F60
		internal bool VerifyEhloReceived()
		{
			ISmtpInSession smtpInSession = this.SmtpSession as ISmtpInSession;
			if (smtpInSession != null && !smtpInSession.SeenEhlo)
			{
				this.SmtpResponse = SmtpResponse.NeedEhlo;
				this.ParsingStatus = ParsingStatus.ProtocolError;
				return false;
			}
			return true;
		}

		// Token: 0x060032F4 RID: 13044 RVA: 0x000C7D9C File Offset: 0x000C5F9C
		internal bool VerifyMailFromReceived()
		{
			ISmtpInSession smtpInSession = this.SmtpSession as ISmtpInSession;
			if (smtpInSession != null && smtpInSession.TransportMailItem == null)
			{
				this.SmtpResponse = SmtpResponse.NeedMailFrom;
				this.ParsingStatus = ParsingStatus.ProtocolError;
				return false;
			}
			return true;
		}

		// Token: 0x060032F5 RID: 13045 RVA: 0x000C7DD8 File Offset: 0x000C5FD8
		internal bool VerifyRcptToReceived()
		{
			ISmtpInSession smtpInSession = this.SmtpSession as ISmtpInSession;
			if (smtpInSession != null && smtpInSession.TransportMailItem.Recipients.Count == 0)
			{
				this.SmtpResponse = SmtpResponse.NeedRcptTo;
				this.ParsingStatus = ParsingStatus.ProtocolError;
				return false;
			}
			return true;
		}

		// Token: 0x060032F6 RID: 13046 RVA: 0x000C7E1C File Offset: 0x000C601C
		internal bool VerifyNoOngoingBdat()
		{
			ISmtpInSession smtpInSession = this.SmtpSession as ISmtpInSession;
			if (smtpInSession != null && smtpInSession.IsBdatOngoing)
			{
				this.SmtpResponse = SmtpResponse.BadCommandSequence;
				this.ParsingStatus = ParsingStatus.ProtocolError;
				return false;
			}
			return true;
		}

		// Token: 0x060032F7 RID: 13047 RVA: 0x000C7E58 File Offset: 0x000C6058
		internal string GetNextArg()
		{
			int num;
			int num2;
			if (!this.GetNextArgOffsets(out num, out num2))
			{
				return string.Empty;
			}
			return ByteString.BytesToString(this.protocolCommand, num, num2 - num, true);
		}

		// Token: 0x060032F8 RID: 13048 RVA: 0x000C7E88 File Offset: 0x000C6088
		internal bool GetNextArgOffsets(out int argBeginOffset, out int argEndOffset)
		{
			argBeginOffset = 0;
			argEndOffset = 0;
			if (this.IsEndOfCommand())
			{
				return false;
			}
			argBeginOffset = BufferParser.GetNextToken(this.protocolCommand, this.currentOffset, this.protocolCommand.Length - this.currentOffset, out argEndOffset);
			this.currentOffset = argEndOffset;
			return argBeginOffset != argEndOffset;
		}

		// Token: 0x060032F9 RID: 13049 RVA: 0x000C7ED9 File Offset: 0x000C60D9
		protected virtual void Dispose(bool disposing)
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
		}

		// Token: 0x060032FA RID: 13050 RVA: 0x000C7EF0 File Offset: 0x000C60F0
		protected bool VerifyNotAuthenticatedThroughAuthVerb()
		{
			ISmtpInSession smtpInSession = this.SmtpSession as ISmtpInSession;
			if (smtpInSession != null && SmtpInSessionUtils.IsAuthenticated(smtpInSession.RemoteIdentity) && (smtpInSession.AuthMethod == MultilevelAuthMechanism.TLSAuthLogin || smtpInSession.AuthMethod == MultilevelAuthMechanism.Login || smtpInSession.AuthMethod == MultilevelAuthMechanism.NTLM || smtpInSession.AuthMethod == MultilevelAuthMechanism.GSSAPI || smtpInSession.AuthMethod == MultilevelAuthMechanism.MUTUALGSSAPI))
			{
				this.SmtpResponse = SmtpResponse.AuthAlreadySpecified;
				this.ParsingStatus = ParsingStatus.ProtocolError;
				return false;
			}
			return true;
		}

		// Token: 0x060032FB RID: 13051 RVA: 0x000C7F60 File Offset: 0x000C6160
		protected bool VerifyNotAuthenticatedThroughAuthLoginVerb()
		{
			ISmtpInSession smtpInSession = this.SmtpSession as ISmtpInSession;
			if (smtpInSession != null && SmtpInSessionUtils.IsAuthenticated(smtpInSession.RemoteIdentity) && (smtpInSession.AuthMethod == MultilevelAuthMechanism.TLSAuthLogin || smtpInSession.AuthMethod == MultilevelAuthMechanism.Login))
			{
				this.SmtpResponse = SmtpResponse.AuthAlreadySpecified;
				this.ParsingStatus = ParsingStatus.ProtocolError;
				return false;
			}
			return true;
		}

		// Token: 0x060032FC RID: 13052 RVA: 0x000C7FB0 File Offset: 0x000C61B0
		protected bool VerifyNotAuthenticatedForInboundClientProxy()
		{
			ISmtpInSession smtpInSession = this.SmtpSession as ISmtpInSession;
			if (smtpInSession != null && smtpInSession.InboundClientProxyState == InboundClientProxyStates.XProxyReceivedAndAuthenticated)
			{
				this.SmtpResponse = SmtpResponse.AuthAlreadySpecified;
				this.ParsingStatus = ParsingStatus.ProtocolError;
				return false;
			}
			return true;
		}

		// Token: 0x060032FD RID: 13053 RVA: 0x000C7FEC File Offset: 0x000C61EC
		protected bool VerifyXexch50NotReceived()
		{
			ISmtpInSession smtpInSession = this.SmtpSession as ISmtpInSession;
			if (smtpInSession != null && smtpInSession.IsXexch50Received)
			{
				this.SmtpResponse = SmtpResponse.BadCommandSequence;
				this.ParsingStatus = ParsingStatus.ProtocolError;
				return false;
			}
			return true;
		}

		// Token: 0x060032FE RID: 13054 RVA: 0x000C8028 File Offset: 0x000C6228
		protected bool VerifyNoOngoingMailTransaction()
		{
			ISmtpInSession smtpInSession = this.SmtpSession as ISmtpInSession;
			if (smtpInSession != null && smtpInSession.TransportMailItem != null)
			{
				this.SmtpResponse = SmtpResponse.BadCommandSequence;
				this.ParsingStatus = ParsingStatus.ProtocolError;
				return false;
			}
			return true;
		}

		// Token: 0x060032FF RID: 13055 RVA: 0x000C8061 File Offset: 0x000C6261
		protected bool IsEndOfCommand()
		{
			return this.currentOffset >= this.protocolCommandLength;
		}

		// Token: 0x040019B8 RID: 6584
		internal const int DefaultCommandLength = 4;

		// Token: 0x040019B9 RID: 6585
		internal static readonly byte[] AUTH = Util.AsciiStringToBytes("AUTH".ToLowerInvariant());

		// Token: 0x040019BA RID: 6586
		internal static readonly byte[] EXPS = Util.AsciiStringToBytes("X-EXPS".ToLowerInvariant());

		// Token: 0x040019BB RID: 6587
		internal static readonly byte[] BDAT = Util.AsciiStringToBytes("BDAT".ToLowerInvariant());

		// Token: 0x040019BC RID: 6588
		internal static readonly byte[] DATA = Util.AsciiStringToBytes("DATA".ToLowerInvariant());

		// Token: 0x040019BD RID: 6589
		internal static readonly byte[] EHLO = Util.AsciiStringToBytes("EHLO".ToLowerInvariant());

		// Token: 0x040019BE RID: 6590
		internal static readonly byte[] HELO = Util.AsciiStringToBytes("HELO".ToLowerInvariant());

		// Token: 0x040019BF RID: 6591
		internal static readonly byte[] HELP = Util.AsciiStringToBytes("HELP".ToLowerInvariant());

		// Token: 0x040019C0 RID: 6592
		internal static readonly byte[] MAIL = Util.AsciiStringToBytes("MAIL".ToLowerInvariant());

		// Token: 0x040019C1 RID: 6593
		internal static readonly byte[] NOOP = Util.AsciiStringToBytes("NOOP".ToLowerInvariant());

		// Token: 0x040019C2 RID: 6594
		internal static readonly byte[] QUIT = Util.AsciiStringToBytes("QUIT".ToLowerInvariant());

		// Token: 0x040019C3 RID: 6595
		internal static readonly byte[] RCPT = Util.AsciiStringToBytes("RCPT".ToLowerInvariant());

		// Token: 0x040019C4 RID: 6596
		internal static readonly byte[] RCPT2 = Util.AsciiStringToBytes("RCPT2".ToLowerInvariant());

		// Token: 0x040019C5 RID: 6597
		internal static readonly byte[] RSET = Util.AsciiStringToBytes("RSET".ToLowerInvariant());

		// Token: 0x040019C6 RID: 6598
		internal static readonly byte[] STARTTLS = Util.AsciiStringToBytes("STARTTLS".ToLowerInvariant());

		// Token: 0x040019C7 RID: 6599
		internal static readonly byte[] ANONYMOUSTLS = Util.AsciiStringToBytes("X-ANONYMOUSTLS".ToLowerInvariant());

		// Token: 0x040019C8 RID: 6600
		internal static readonly byte[] VRFY = Util.AsciiStringToBytes("VRFY".ToLowerInvariant());

		// Token: 0x040019C9 RID: 6601
		internal static readonly byte[] EXPN = Util.AsciiStringToBytes("EXPN".ToLowerInvariant());

		// Token: 0x040019CA RID: 6602
		internal static readonly byte[] XEXCH50 = Util.AsciiStringToBytes("XEXCH50".ToLowerInvariant());

		// Token: 0x040019CB RID: 6603
		internal static readonly byte[] XSHADOW = Util.AsciiStringToBytes("XSHADOW".ToLowerInvariant());

		// Token: 0x040019CC RID: 6604
		internal static readonly byte[] XSHADOWREQUEST = Util.AsciiStringToBytes("XSHADOWREQUEST".ToLowerInvariant());

		// Token: 0x040019CD RID: 6605
		internal static readonly byte[] XQDISCARD = Util.AsciiStringToBytes("XQDISCARD".ToLowerInvariant());

		// Token: 0x040019CE RID: 6606
		internal static readonly byte[] XPROXY = Util.AsciiStringToBytes("XPROXY".ToLowerInvariant());

		// Token: 0x040019CF RID: 6607
		internal static readonly byte[] XPROXYFROM = Util.AsciiStringToBytes("XPROXYFROM".ToLowerInvariant());

		// Token: 0x040019D0 RID: 6608
		internal static readonly byte[] XSESSIONPARAMS = Util.AsciiStringToBytes("XSESSIONPARAMS".ToLowerInvariant());

		// Token: 0x040019D1 RID: 6609
		internal static readonly byte[] XPROXYTO = Util.AsciiStringToBytes("XPROXYTO".ToLowerInvariant());

		// Token: 0x040019D2 RID: 6610
		internal static ExEventLog EventLogger = new ExEventLog(ExTraceGlobals.SmtpSendTracer.Category, TransportEventLog.GetEventSource());

		// Token: 0x040019D3 RID: 6611
		internal static IExEventLog EventLog = new ExEventLogWrapper(SmtpCommand.EventLogger);

		// Token: 0x040019D4 RID: 6612
		internal ReceiveCommandEventArgs CommandEventArgs;

		// Token: 0x040019D5 RID: 6613
		protected EventArgs originalEventArgsWrapper;

		// Token: 0x040019D6 RID: 6614
		private readonly DisposeTracker disposeTracker;

		// Token: 0x040019D7 RID: 6615
		private byte[] protocolCommand;

		// Token: 0x040019D8 RID: 6616
		private readonly string protocolCommandKeyword;

		// Token: 0x040019D9 RID: 6617
		private int currentOffset;

		// Token: 0x040019DA RID: 6618
		private int protocolCommandLength;

		// Token: 0x040019DB RID: 6619
		private string protocolCommandString;

		// Token: 0x040019DC RID: 6620
		private string redactedProtocolCommandString;

		// Token: 0x040019DD RID: 6621
		private SmtpResponse smtpResponse = SmtpResponse.Empty;

		// Token: 0x040019DE RID: 6622
		private ParsingStatus parsingStatus;

		// Token: 0x040019DF RID: 6623
		private bool isResponseReady;

		// Token: 0x040019E0 RID: 6624
		private bool isResponseBuffered;

		// Token: 0x040019E1 RID: 6625
		private TarpitAction lowAuthenticationLevelTarpitOverride;

		// Token: 0x040019E2 RID: 6626
		private TarpitAction highAuthenticationLevelTarpitOverride;

		// Token: 0x040019E3 RID: 6627
		private TimeSpan tarpitInterval;

		// Token: 0x040019E4 RID: 6628
		private string tarpitReason;

		// Token: 0x040019E5 RID: 6629
		private string tarpitContext;

		// Token: 0x040019E6 RID: 6630
		private readonly ISmtpSession session;

		// Token: 0x040019E7 RID: 6631
		private readonly string commandEventString;

		// Token: 0x040019E8 RID: 6632
		private readonly LatencyComponent commandEventComponent;
	}
}
