using System;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Pop3
{
	// Token: 0x02000011 RID: 17
	internal class Pop3RequestRetr : Pop3RequestWithIntParameters
	{
		// Token: 0x0600006F RID: 111 RVA: 0x00003964 File Offset: 0x00001B64
		public Pop3RequestRetr(Pop3ResponseFactory factory, string input) : this(factory, input, Pop3RequestWithIntParameters.ArgumentsTypes.one_mandatory)
		{
			this.PerfCounterTotal = base.Pop3CountersInstance.PerfCounter_RETR_Total;
			this.PerfCounterFailures = base.Pop3CountersInstance.PerfCounter_RETR_Failures;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003991 File Offset: 0x00001B91
		protected Pop3RequestRetr(Pop3ResponseFactory factory, string input, Pop3RequestWithIntParameters.ArgumentsTypes argumentsType) : base(factory, input, argumentsType)
		{
			base.AllowedStates = Pop3State.Authenticated;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000071 RID: 113 RVA: 0x000039A3 File Offset: 0x00001BA3
		public override bool NeedsStoreConnection
		{
			get
			{
				return base.Factory.SessionState == Pop3State.Authenticated;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000072 RID: 114 RVA: 0x000039B3 File Offset: 0x00001BB3
		// (set) Token: 0x06000073 RID: 115 RVA: 0x000039BB File Offset: 0x00001BBB
		protected internal string CurrentMessageSubject { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000074 RID: 116 RVA: 0x000039C4 File Offset: 0x00001BC4
		// (set) Token: 0x06000075 RID: 117 RVA: 0x000039CC File Offset: 0x00001BCC
		protected internal string CurrentMessageSentTime { get; set; }

		// Token: 0x06000076 RID: 118 RVA: 0x000039D5 File Offset: 0x00001BD5
		public override ProtocolResponse ProcessMessage(Pop3Message message)
		{
			return this.ProcessMessage(message, -1);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000039E0 File Offset: 0x00001BE0
		protected ProtocolResponse ProcessMessage(Pop3Message message, int linesToReturn)
		{
			if (linesToReturn == 0)
			{
				StreamWrapper headerMimeStream = message.GetHeaderMimeStream(this);
				if (headerMimeStream != null)
				{
					if (!base.Factory.Session.SendToClient(new StringResponseItem("+OK\r\n")) || !base.Factory.Session.SendToClient(new StreamResponseItem(headerMimeStream)))
					{
						return null;
					}
					return new Pop3Response(Pop3Response.Type.unknown, ".");
				}
			}
			else
			{
				Pop3MimeStream mimeStream = message.GetMimeStream(this);
				if (mimeStream != null)
				{
					mimeStream.LinesToReturn = linesToReturn;
					if (!base.Factory.Session.SendToClient(new StringResponseItem("+OK\r\n")) || !base.Factory.Session.SendToClient(new StreamResponseItem(mimeStream)))
					{
						return null;
					}
					message.IsRead = true;
					return new Pop3Response(Pop3Response.Type.unknown, ".");
				}
			}
			return new Pop3Response(Pop3Response.Type.err, "Message corrupted");
		}

		// Token: 0x0400004C RID: 76
		internal const string RETRResponseFailed = "Message corrupted";

		// Token: 0x0400004D RID: 77
		private const int WholeStream = -1;
	}
}
