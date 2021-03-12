using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x02000044 RID: 68
	internal sealed class Imap4RequestUid : Imap4Request
	{
		// Token: 0x06000288 RID: 648 RVA: 0x00012CEC File Offset: 0x00010EEC
		public Imap4RequestUid(Imap4ResponseFactory factory, string tag, string data) : base(factory, tag, data)
		{
			string text = string.Empty;
			string text2 = null;
			string[] array = data.Trim().Split(ResponseFactory.WordDelimiter, 2);
			if (array.Length > 0)
			{
				text = array[0].ToUpper();
				if (array.Length > 1)
				{
					text2 = array[1];
				}
				else
				{
					text2 = string.Empty;
				}
			}
			string key;
			switch (key = text)
			{
			case "COPY":
				this.nestedRequest = new Imap4RequestCopy(base.Factory, tag, text2, true);
				factory.CommandName = "uid+copy";
				return;
			case "FETCH":
				this.nestedRequest = new Imap4RequestFetch(base.Factory, tag, text2, true);
				factory.CommandName = "uid+fetch";
				return;
			case "MOVE":
				if (base.Factory.MoveEnabled)
				{
					this.nestedRequest = new Imap4RequestMove(base.Factory, tag, text2, true);
					factory.CommandName = "uid+move";
					return;
				}
				this.nestedRequest = new Imap4RequestInvalid(base.Factory, tag, "Command Error. 12");
				return;
			case "SEARCH":
				this.nestedRequest = new Imap4RequestSearch(base.Factory, tag, text2, true);
				factory.CommandName = "uid+search";
				return;
			case "STORE":
				this.nestedRequest = new Imap4RequestStore(base.Factory, tag, text2, true);
				factory.CommandName = "uid+store";
				return;
			case "EXPUNGE":
				if (base.SupportUidPlus)
				{
					this.nestedRequest = new Imap4RequestExpunge(base.Factory, tag, text2, true);
					factory.CommandName = "uid+expunge";
					return;
				}
				this.nestedRequest = new Imap4RequestInvalid(base.Factory, tag, "Command Error. 12");
				return;
			}
			this.nestedRequest = new Imap4RequestInvalid(base.Factory, tag, "Command Error. 12");
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000289 RID: 649 RVA: 0x00012EF7 File Offset: 0x000110F7
		public override ExPerformanceCounter PerfCounterTotal
		{
			get
			{
				return this.nestedRequest.PerfCounterTotal;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600028A RID: 650 RVA: 0x00012F04 File Offset: 0x00011104
		public override ExPerformanceCounter PerfCounterFailures
		{
			get
			{
				return this.nestedRequest.PerfCounterFailures;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600028B RID: 651 RVA: 0x00012F11 File Offset: 0x00011111
		public override bool IsComplete
		{
			get
			{
				return this.nestedRequest.IsComplete;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600028C RID: 652 RVA: 0x00012F1E File Offset: 0x0001111E
		public override ParseResult ParseResult
		{
			get
			{
				return this.nestedRequest.ParseResult;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600028D RID: 653 RVA: 0x00012F2B File Offset: 0x0001112B
		public override bool NeedsStoreConnection
		{
			get
			{
				return this.nestedRequest.NeedsStoreConnection;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600028E RID: 654 RVA: 0x00012F38 File Offset: 0x00011138
		public override bool NeedToDelayStoreAction
		{
			get
			{
				return this.nestedRequest.NeedToDelayStoreAction;
			}
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00012F45 File Offset: 0x00011145
		public override bool VerifyState()
		{
			return this.nestedRequest.VerifyState();
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00012F52 File Offset: 0x00011152
		public override void ParseArguments()
		{
			this.nestedRequest.ParseArguments();
			base.SendSyncResponse = this.nestedRequest.SendSyncResponse;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00012F70 File Offset: 0x00011170
		public override ProtocolResponse SyncResponse(ProtocolRequest request)
		{
			return this.nestedRequest.SyncResponse(request);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00012F7E File Offset: 0x0001117E
		public override void AddData(byte[] data, int offset, int size, out int cConsumed)
		{
			this.nestedRequest.AddData(data, offset, size, out cConsumed);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00012F90 File Offset: 0x00011190
		public override ProtocolResponse Process()
		{
			return this.nestedRequest.Process();
		}

		// Token: 0x040001F3 RID: 499
		private Imap4Request nestedRequest;
	}
}
