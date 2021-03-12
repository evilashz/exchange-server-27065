using System;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x0200001C RID: 28
	internal abstract class Imap4Request : ProtocolRequest
	{
		// Token: 0x06000153 RID: 339 RVA: 0x00009E27 File Offset: 0x00008027
		public Imap4Request(ResponseFactory factory, string tag, string data) : base(factory, data)
		{
			this.tag = tag;
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00009E3F File Offset: 0x0000803F
		public Imap4ResponseFactory Factory
		{
			get
			{
				return (Imap4ResponseFactory)base.ResponseFactory;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00009E4C File Offset: 0x0000804C
		// (set) Token: 0x06000156 RID: 342 RVA: 0x00009E54 File Offset: 0x00008054
		public override bool IsComplete
		{
			get
			{
				return this.isComplete;
			}
			set
			{
				this.isComplete = value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00009E5D File Offset: 0x0000805D
		public virtual bool AllowsExpungeNotifications
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00009E60 File Offset: 0x00008060
		internal string Tag
		{
			get
			{
				return this.tag;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00009E68 File Offset: 0x00008068
		// (set) Token: 0x0600015A RID: 346 RVA: 0x00009E70 File Offset: 0x00008070
		internal bool SendSyncResponse
		{
			get
			{
				return this.sendSyncResponse;
			}
			set
			{
				this.sendSyncResponse = value;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00009E79 File Offset: 0x00008079
		protected Imap4CountersInstance Imap4CountersInstance
		{
			get
			{
				return ((Imap4VirtualServer)ProtocolBaseServices.VirtualServer).Imap4CountersInstance;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00009E8A File Offset: 0x0000808A
		protected bool SupportUidPlus
		{
			get
			{
				return ((Imap4Server)base.Session.Server).SupportUidPlus;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00009EA1 File Offset: 0x000080A1
		// (set) Token: 0x0600015E RID: 350 RVA: 0x00009EA9 File Offset: 0x000080A9
		protected Imap4State AllowedStates
		{
			get
			{
				return this.allowedStates;
			}
			set
			{
				this.allowedStates = value;
			}
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00009EB4 File Offset: 0x000080B4
		public override bool VerifyState()
		{
			Imap4ResponseFactory factory = this.Factory;
			return factory != null && (this.AllowedStates & factory.SessionState) != Imap4State.None;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00009EE0 File Offset: 0x000080E0
		public virtual ProtocolResponse SyncResponse(ProtocolRequest request)
		{
			if (this.sendSyncResponse)
			{
				return new ProtocolResponse(request, "+ Ready for additional command text.");
			}
			return null;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00009EF7 File Offset: 0x000080F7
		public virtual void AddData(byte[] data, int offset, int cData, out int cConsumed)
		{
			ProtocolBaseServices.Assert(false, "Imap4Request.AddData should never be called", new object[0]);
			cConsumed = 0;
		}

		// Token: 0x040000FE RID: 254
		internal const string InvalidArgument = "Command Argument Error. 11";

		// Token: 0x040000FF RID: 255
		internal const string InvalidNumberOfArguments = "Command Argument Error. 12";

		// Token: 0x04000100 RID: 256
		internal const string MailboxDoesNotExist = "Mailbox has been deleted or moved.";

		// Token: 0x04000101 RID: 257
		private const string MoreDataResponse = "+ Ready for additional command text.";

		// Token: 0x04000102 RID: 258
		private Imap4State allowedStates;

		// Token: 0x04000103 RID: 259
		private string tag;

		// Token: 0x04000104 RID: 260
		private bool isComplete = true;

		// Token: 0x04000105 RID: 261
		private bool sendSyncResponse;
	}
}
