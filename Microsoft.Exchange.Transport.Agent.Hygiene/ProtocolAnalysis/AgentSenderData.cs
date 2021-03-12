using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Transport.Agent.Hygiene;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis
{
	// Token: 0x02000034 RID: 52
	internal class AgentSenderData : SenderData
	{
		// Token: 0x0600012C RID: 300 RVA: 0x0000A1DF File Offset: 0x000083DF
		public AgentSenderData(DateTime tsCreate) : base(tsCreate)
		{
			this.msgsNormValid = 0;
			this.msgsNormInvalid = 0;
			this.helloDomain = string.Empty;
			this.reverseDns = string.Empty;
			this.msgStarted = false;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600012D RID: 301 RVA: 0x0000A213 File Offset: 0x00008413
		public string HelloDomain
		{
			get
			{
				return this.helloDomain;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600012E RID: 302 RVA: 0x0000A21B File Offset: 0x0000841B
		// (set) Token: 0x0600012F RID: 303 RVA: 0x0000A223 File Offset: 0x00008423
		public string ReverseDns
		{
			get
			{
				return this.reverseDns;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					this.reverseDns = value;
				}
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000A234 File Offset: 0x00008434
		public void OnMailFrom()
		{
			if (this.msgStarted)
			{
				throw new InvalidOperationException("SenderData::OnMailFrom called while msg_started being true");
			}
			this.msgStarted = true;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000A250 File Offset: 0x00008450
		public override void OnValidRecipient(string recipient)
		{
			if (this.msgStarted)
			{
				this.msgsNormValid++;
				base.OnValidRecipient(recipient);
				return;
			}
			throw new InvalidOperationException("SenderData::OnValidRecipient called while msg_started being false");
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000A27A File Offset: 0x0000847A
		public override void OnInvalidRecipient(string recipient)
		{
			if (this.msgStarted)
			{
				this.msgsNormInvalid++;
				base.OnInvalidRecipient(recipient);
				return;
			}
			throw new InvalidOperationException("SenderData::OnInvalidRecipient called while msg_started being false");
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000A2A4 File Offset: 0x000084A4
		public override void OnEndOfData(int scl, long msglen, CallerIdStatus status)
		{
			if (scl < 0 || scl > 10)
			{
				throw new LocalizedException(AgentStrings.InvalidScl(scl));
			}
			if (this.msgStarted)
			{
				base.OnEndOfData(scl, msglen, status);
				this.ValidScl[scl] += this.msgsNormValid;
				this.InvalidScl[scl] += this.msgsNormInvalid;
				this.msgsNormValid = 0;
				this.msgsNormInvalid = 0;
				this.msgStarted = false;
				return;
			}
			throw new InvalidOperationException("SenderData::OnEndOfData called while msg_started being false");
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000A333 File Offset: 0x00008533
		public void OnEndOfSession(string helloDomain)
		{
			if (this.msgStarted)
			{
				throw new InvalidOperationException("SenderData::OnEndOfSession called while msg_started being true");
			}
			if (!this.msgStarted && !string.IsNullOrEmpty(helloDomain))
			{
				this.helloDomain = helloDomain;
			}
		}

		// Token: 0x0400013E RID: 318
		private int msgsNormValid;

		// Token: 0x0400013F RID: 319
		private int msgsNormInvalid;

		// Token: 0x04000140 RID: 320
		private string helloDomain;

		// Token: 0x04000141 RID: 321
		private string reverseDns;

		// Token: 0x04000142 RID: 322
		private bool msgStarted;
	}
}
