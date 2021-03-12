using System;

namespace Microsoft.Exchange.Data.Transport.Smtp
{
	// Token: 0x0200001F RID: 31
	public abstract class SmtpReceiveAgent : Agent
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000077 RID: 119 RVA: 0x00002448 File Offset: 0x00000648
		// (remove) Token: 0x06000078 RID: 120 RVA: 0x00002456 File Offset: 0x00000656
		internal event ProcessAuthenticationEventHandler OnProcessAuthentication
		{
			add
			{
				base.AddHandler("OnProcessAuthentication", value);
			}
			remove
			{
				base.RemoveHandler("OnProcessAuthentication");
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000079 RID: 121 RVA: 0x00002463 File Offset: 0x00000663
		// (remove) Token: 0x0600007A RID: 122 RVA: 0x00002471 File Offset: 0x00000671
		internal event ProxyInboundMessageEventHandler OnProxyInboundMessage
		{
			add
			{
				base.AddHandler("OnProxyInboundMessage", value);
			}
			remove
			{
				base.RemoveHandler("OnProxyInboundMessage");
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600007B RID: 123 RVA: 0x0000247E File Offset: 0x0000067E
		// (remove) Token: 0x0600007C RID: 124 RVA: 0x0000248C File Offset: 0x0000068C
		internal event Rcpt2CommandEventHandler OnRcpt2Command
		{
			add
			{
				base.AddHandler("OnRcpt2Command", value);
			}
			remove
			{
				base.RemoveHandler("OnRcpt2Command");
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600007D RID: 125 RVA: 0x00002499 File Offset: 0x00000699
		// (remove) Token: 0x0600007E RID: 126 RVA: 0x000024A7 File Offset: 0x000006A7
		internal event XSessionParamsCommandEventHandler OnXSessionParamsCommand
		{
			add
			{
				base.AddHandler("OnXSessionParamsCommand", value);
			}
			remove
			{
				base.RemoveHandler("OnXSessionParamsCommand");
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600007F RID: 127 RVA: 0x000024B4 File Offset: 0x000006B4
		// (remove) Token: 0x06000080 RID: 128 RVA: 0x000024C2 File Offset: 0x000006C2
		protected event AuthCommandEventHandler OnAuthCommand
		{
			add
			{
				base.AddHandler("OnAuthCommand", value);
			}
			remove
			{
				base.RemoveHandler("OnAuthCommand");
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000081 RID: 129 RVA: 0x000024CF File Offset: 0x000006CF
		// (remove) Token: 0x06000082 RID: 130 RVA: 0x000024DD File Offset: 0x000006DD
		protected event DataCommandEventHandler OnDataCommand
		{
			add
			{
				base.AddHandler("OnDataCommand", value);
			}
			remove
			{
				base.RemoveHandler("OnDataCommand");
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000083 RID: 131 RVA: 0x000024EA File Offset: 0x000006EA
		// (remove) Token: 0x06000084 RID: 132 RVA: 0x000024F8 File Offset: 0x000006F8
		protected event EhloCommandEventHandler OnEhloCommand
		{
			add
			{
				base.AddHandler("OnEhloCommand", value);
			}
			remove
			{
				base.RemoveHandler("OnEhloCommand");
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000085 RID: 133 RVA: 0x00002505 File Offset: 0x00000705
		// (remove) Token: 0x06000086 RID: 134 RVA: 0x00002513 File Offset: 0x00000713
		protected event EndOfAuthenticationEventHandler OnEndOfAuthentication
		{
			add
			{
				base.AddHandler("OnEndOfAuthentication", value);
			}
			remove
			{
				base.RemoveHandler("OnEndOfAuthentication");
			}
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000087 RID: 135 RVA: 0x00002520 File Offset: 0x00000720
		// (remove) Token: 0x06000088 RID: 136 RVA: 0x0000252E File Offset: 0x0000072E
		protected event EndOfDataEventHandler OnEndOfData
		{
			add
			{
				base.AddHandler("OnEndOfData", value);
			}
			remove
			{
				base.RemoveHandler("OnEndOfData");
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000089 RID: 137 RVA: 0x0000253B File Offset: 0x0000073B
		// (remove) Token: 0x0600008A RID: 138 RVA: 0x00002549 File Offset: 0x00000749
		protected event EndOfHeadersEventHandler OnEndOfHeaders
		{
			add
			{
				base.AddHandler("OnEndOfHeaders", value);
			}
			remove
			{
				base.RemoveHandler("OnEndOfHeaders");
			}
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x0600008B RID: 139 RVA: 0x00002556 File Offset: 0x00000756
		// (remove) Token: 0x0600008C RID: 140 RVA: 0x00002564 File Offset: 0x00000764
		protected event HeloCommandEventHandler OnHeloCommand
		{
			add
			{
				base.AddHandler("OnHeloCommand", value);
			}
			remove
			{
				base.RemoveHandler("OnHeloCommand");
			}
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x0600008D RID: 141 RVA: 0x00002571 File Offset: 0x00000771
		// (remove) Token: 0x0600008E RID: 142 RVA: 0x0000257F File Offset: 0x0000077F
		protected event HelpCommandEventHandler OnHelpCommand
		{
			add
			{
				base.AddHandler("OnHelpCommand", value);
			}
			remove
			{
				base.RemoveHandler("OnHelpCommand");
			}
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x0600008F RID: 143 RVA: 0x0000258C File Offset: 0x0000078C
		// (remove) Token: 0x06000090 RID: 144 RVA: 0x0000259A File Offset: 0x0000079A
		protected event MailCommandEventHandler OnMailCommand
		{
			add
			{
				base.AddHandler("OnMailCommand", value);
			}
			remove
			{
				base.RemoveHandler("OnMailCommand");
			}
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000091 RID: 145 RVA: 0x000025A7 File Offset: 0x000007A7
		// (remove) Token: 0x06000092 RID: 146 RVA: 0x000025B5 File Offset: 0x000007B5
		protected event NoopCommandEventHandler OnNoopCommand
		{
			add
			{
				base.AddHandler("OnNoopCommand", value);
			}
			remove
			{
				base.RemoveHandler("OnNoopCommand");
			}
		}

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000093 RID: 147 RVA: 0x000025C2 File Offset: 0x000007C2
		// (remove) Token: 0x06000094 RID: 148 RVA: 0x000025D0 File Offset: 0x000007D0
		protected event RcptCommandEventHandler OnRcptCommand
		{
			add
			{
				base.AddHandler("OnRcptCommand", value);
			}
			remove
			{
				base.RemoveHandler("OnRcptCommand");
			}
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000095 RID: 149 RVA: 0x000025DD File Offset: 0x000007DD
		// (remove) Token: 0x06000096 RID: 150 RVA: 0x000025EB File Offset: 0x000007EB
		protected event RejectEventHandler OnReject
		{
			add
			{
				base.AddHandler("OnReject", value);
			}
			remove
			{
				base.RemoveHandler("OnReject");
			}
		}

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000097 RID: 151 RVA: 0x000025F8 File Offset: 0x000007F8
		// (remove) Token: 0x06000098 RID: 152 RVA: 0x00002606 File Offset: 0x00000806
		protected event RsetCommandEventHandler OnRsetCommand
		{
			add
			{
				base.AddHandler("OnRsetCommand", value);
			}
			remove
			{
				base.RemoveHandler("OnRsetCommand");
			}
		}

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000099 RID: 153 RVA: 0x00002613 File Offset: 0x00000813
		// (remove) Token: 0x0600009A RID: 154 RVA: 0x00002621 File Offset: 0x00000821
		protected event ConnectEventHandler OnConnect
		{
			add
			{
				base.AddHandler("OnConnectEvent", value);
			}
			remove
			{
				base.RemoveHandler("OnConnectEvent");
			}
		}

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x0600009B RID: 155 RVA: 0x0000262E File Offset: 0x0000082E
		// (remove) Token: 0x0600009C RID: 156 RVA: 0x0000263C File Offset: 0x0000083C
		protected event DisconnectEventHandler OnDisconnect
		{
			add
			{
				base.AddHandler("OnDisconnectEvent", value);
			}
			remove
			{
				base.RemoveHandler("OnDisconnectEvent");
			}
		}

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x0600009D RID: 157 RVA: 0x00002649 File Offset: 0x00000849
		// (remove) Token: 0x0600009E RID: 158 RVA: 0x00002657 File Offset: 0x00000857
		protected event StartTlsEventHandler OnStartTlsCommand
		{
			add
			{
				base.AddHandler("OnStartTlsCommand", value);
			}
			remove
			{
				base.RemoveHandler("OnStartTlsCommand");
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00002664 File Offset: 0x00000864
		internal override string SnapshotPrefix
		{
			get
			{
				return "SmtpReceive";
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x0000266B File Offset: 0x0000086B
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00002673 File Offset: 0x00000873
		internal override object HostState
		{
			get
			{
				return base.HostStateInternal;
			}
			set
			{
				base.HostStateInternal = value;
				((SmtpServer)base.HostStateInternal).AssociatedAgent = this;
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00002690 File Offset: 0x00000890
		internal override void AsyncComplete()
		{
			if (base.EventTopic == "OnEndOfData")
			{
				base.EnsureMimeWriteStreamClosed();
				base.MailItem = null;
			}
			((SmtpServer)this.HostState).AddressBook.RecipientCache = null;
			base.EventArgId = null;
			base.SnapshotWriter = null;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000026E0 File Offset: 0x000008E0
		internal override void Invoke(string eventTopic, object source, object e)
		{
			SnapshotWriter snapshotWriter = null;
			base.MailItem = SmtpReceiveAgent.GetMailItem((EventArgs)e);
			if (base.MailItem != null && base.MailItem.SnapshotWriter != null && base.MailItem.PipelineTracingEnabled && base.SnapshotEnabled)
			{
				base.SnapshotWriter = base.MailItem.SnapshotWriter;
				this.WriteOriginalDataSnapshot(source, (EventArgs)e, eventTopic);
				snapshotWriter = base.SnapshotWriter;
				base.SnapshotWriter = null;
			}
			Delegate @delegate = (Delegate)base.Handlers[eventTopic];
			if (@delegate == null)
			{
				return;
			}
			switch (eventTopic)
			{
			case "OnAuthCommand":
				((AuthCommandEventHandler)@delegate)((ReceiveCommandEventSource)source, (AuthCommandEventArgs)e);
				break;
			case "OnDataCommand":
				if (!((ReceiveCommandEventSource)source).SmtpSession.DiscardingMessage)
				{
					((SmtpServer)this.HostState).AddressBook.RecipientCache = base.MailItem.RecipientCache;
					((DataCommandEventHandler)@delegate)((ReceiveCommandEventSource)source, (DataCommandEventArgs)e);
				}
				break;
			case "OnEhloCommand":
				((EhloCommandEventHandler)@delegate)((ReceiveCommandEventSource)source, (EhloCommandEventArgs)e);
				break;
			case "OnProcessAuthentication":
				((ProcessAuthenticationEventHandler)@delegate)((ReceiveCommandEventSource)source, (ProcessAuthenticationEventArgs)e);
				break;
			case "OnEndOfAuthentication":
				((EndOfAuthenticationEventHandler)@delegate)((EndOfAuthenticationEventSource)source, (EndOfAuthenticationEventArgs)e);
				break;
			case "OnEndOfData":
				if (!((ReceiveMessageEventSource)source).SmtpSession.DiscardingMessage)
				{
					base.SnapshotWriter = snapshotWriter;
					((SmtpServer)this.HostState).AddressBook.RecipientCache = base.MailItem.RecipientCache;
					base.EventArgId = ((ReceiveEventSource)source).SmtpSession.CurrentMessageTemporaryId;
					if (base.SnapshotWriter != null)
					{
						base.SnapshotWriter.WritePreProcessedData(this.GetHashCode(), "SmtpReceive", base.EventArgId, eventTopic, base.MailItem);
					}
					((EndOfDataEventHandler)@delegate)((ReceiveMessageEventSource)source, (EndOfDataEventArgs)e);
					if (base.Synchronous)
					{
						base.EnsureMimeWriteStreamClosed();
						if (base.SnapshotWriter != null)
						{
							base.SnapshotWriter.WriteProcessedData("SmtpReceive", base.EventArgId, eventTopic, base.Name, base.MailItem);
						}
					}
				}
				break;
			case "OnEndOfHeaders":
				if (!((ReceiveMessageEventSource)source).SmtpSession.DiscardingMessage)
				{
					base.SnapshotWriter = snapshotWriter;
					((SmtpServer)this.HostState).AddressBook.RecipientCache = base.MailItem.RecipientCache;
					base.EventArgId = ((ReceiveEventSource)source).SmtpSession.CurrentMessageTemporaryId;
					if (base.SnapshotWriter != null)
					{
						base.SnapshotWriter.WritePreProcessedData(this.GetHashCode(), "SmtpReceive", base.EventArgId, eventTopic, base.MailItem);
					}
					((EndOfHeadersEventHandler)@delegate)((ReceiveMessageEventSource)source, (EndOfHeadersEventArgs)e);
					if (base.Synchronous && base.SnapshotWriter != null)
					{
						ReceiveMessageEventSource receiveMessageEventSource = (ReceiveMessageEventSource)source;
						base.SnapshotWriter.WriteProcessedData("SmtpReceive", base.EventArgId, eventTopic, base.Name, base.MailItem);
					}
				}
				break;
			case "OnHeloCommand":
				((HeloCommandEventHandler)@delegate)((ReceiveCommandEventSource)source, (HeloCommandEventArgs)e);
				break;
			case "OnHelpCommand":
				((HelpCommandEventHandler)@delegate)((ReceiveCommandEventSource)source, (HelpCommandEventArgs)e);
				break;
			case "OnMailCommand":
				((MailCommandEventHandler)@delegate)((ReceiveCommandEventSource)source, (MailCommandEventArgs)e);
				break;
			case "OnNoopCommand":
				((NoopCommandEventHandler)@delegate)((ReceiveCommandEventSource)source, (NoopCommandEventArgs)e);
				break;
			case "OnRcptCommand":
				((SmtpServer)this.HostState).AddressBook.RecipientCache = base.MailItem.RecipientCache;
				((RcptCommandEventHandler)@delegate)((ReceiveCommandEventSource)source, (RcptCommandEventArgs)e);
				break;
			case "OnRcpt2Command":
				((SmtpServer)this.HostState).AddressBook.RecipientCache = base.MailItem.RecipientCache;
				((Rcpt2CommandEventHandler)@delegate)((ReceiveCommandEventSource)source, (Rcpt2CommandEventArgs)e);
				break;
			case "OnReject":
				((RejectEventHandler)@delegate)((RejectEventSource)source, (RejectEventArgs)e);
				break;
			case "OnRsetCommand":
				((RsetCommandEventHandler)@delegate)((ReceiveCommandEventSource)source, (RsetCommandEventArgs)e);
				break;
			case "OnConnectEvent":
				((ConnectEventHandler)@delegate)((ConnectEventSource)source, (ConnectEventArgs)e);
				break;
			case "OnDisconnectEvent":
				((DisconnectEventHandler)@delegate)((DisconnectEventSource)source, (DisconnectEventArgs)e);
				break;
			case "OnStartTlsCommand":
				((StartTlsEventHandler)@delegate)((ReceiveCommandEventSource)source, (StartTlsCommandEventArgs)e);
				break;
			case "OnProxyInboundMessage":
				((ProxyInboundMessageEventHandler)@delegate)((ProxyInboundMessageEventSource)source, (ProxyInboundMessageEventArgs)e);
				break;
			case "OnXSessionParamsCommand":
				((XSessionParamsCommandEventHandler)@delegate)((ReceiveCommandEventSource)source, (XSessionParamsCommandEventArgs)e);
				break;
			}
			if (base.Synchronous)
			{
				((SmtpServer)this.HostState).AddressBook.RecipientCache = null;
				base.EventArgId = null;
				base.SnapshotWriter = null;
				base.MailItem = null;
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00002D38 File Offset: 0x00000F38
		private static MailItem GetMailItem(EventArgs e)
		{
			RcptCommandEventArgs rcptCommandEventArgs = e as RcptCommandEventArgs;
			if (rcptCommandEventArgs != null)
			{
				return rcptCommandEventArgs.MailItem;
			}
			Rcpt2CommandEventArgs rcpt2CommandEventArgs = e as Rcpt2CommandEventArgs;
			if (rcpt2CommandEventArgs != null)
			{
				return rcpt2CommandEventArgs.MailItem;
			}
			DataCommandEventArgs dataCommandEventArgs = e as DataCommandEventArgs;
			if (dataCommandEventArgs != null)
			{
				return dataCommandEventArgs.MailItem;
			}
			EndOfDataEventArgs endOfDataEventArgs = e as EndOfDataEventArgs;
			if (endOfDataEventArgs != null)
			{
				return endOfDataEventArgs.MailItem;
			}
			EndOfHeadersEventArgs endOfHeadersEventArgs = e as EndOfHeadersEventArgs;
			if (endOfHeadersEventArgs != null)
			{
				return endOfHeadersEventArgs.MailItem;
			}
			return null;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00002DA0 File Offset: 0x00000FA0
		private void WriteOriginalDataSnapshot(object source, EventArgs e, string eventTopic)
		{
			string currentMessageTemporaryId = ((ReceiveEventSource)source).SmtpSession.CurrentMessageTemporaryId;
			string address = null;
			if (eventTopic == "OnRcptCommand")
			{
				address = ((RcptCommandEventArgs)e).RecipientAddress.ToString();
			}
			base.SnapshotWriter.WriteOriginalData(this.GetHashCode(), currentMessageTemporaryId, eventTopic, address, base.MailItem);
		}

		// Token: 0x04000046 RID: 70
		private const string SnapshotFileNamePrefix = "SmtpReceive";
	}
}
