using System;
using System.Text;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PopImap.Core
{
	// Token: 0x02000028 RID: 40
	internal abstract class ProtocolRequest
	{
		// Token: 0x0600022E RID: 558 RVA: 0x00008F4E File Offset: 0x0000714E
		protected ProtocolRequest(ResponseFactory factory, string arguments)
		{
			this.factory = factory;
			this.arguments = arguments;
			this.parseResult = ParseResult.notYetParsed;
			this.perfCounterTotal = ProtocolBaseServices.VirtualServer.InvalidCommands;
			this.perfCounterFailures = ProtocolBaseServices.VirtualServer.InvalidCommands;
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600022F RID: 559 RVA: 0x00008F8B File Offset: 0x0000718B
		// (set) Token: 0x06000230 RID: 560 RVA: 0x00008F93 File Offset: 0x00007193
		public virtual ExPerformanceCounter PerfCounterTotal
		{
			get
			{
				return this.perfCounterTotal;
			}
			set
			{
				this.perfCounterTotal = value;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00008F9C File Offset: 0x0000719C
		// (set) Token: 0x06000232 RID: 562 RVA: 0x00008FA4 File Offset: 0x000071A4
		public virtual ExPerformanceCounter PerfCounterFailures
		{
			get
			{
				return this.perfCounterFailures;
			}
			set
			{
				this.perfCounterFailures = value;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00008FAD File Offset: 0x000071AD
		public virtual bool NeedToDelayStoreAction
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00008FB0 File Offset: 0x000071B0
		// (set) Token: 0x06000235 RID: 565 RVA: 0x00008FB3 File Offset: 0x000071B3
		public virtual bool IsComplete
		{
			get
			{
				return true;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00008FBA File Offset: 0x000071BA
		public virtual bool NeedsStoreConnection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00008FBD File Offset: 0x000071BD
		// (set) Token: 0x06000238 RID: 568 RVA: 0x00008FC5 File Offset: 0x000071C5
		public virtual ParseResult ParseResult
		{
			get
			{
				return this.parseResult;
			}
			set
			{
				this.parseResult = value;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000239 RID: 569 RVA: 0x00008FCE File Offset: 0x000071CE
		public ResponseFactory ResponseFactory
		{
			get
			{
				return this.factory;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600023A RID: 570 RVA: 0x00008FD6 File Offset: 0x000071D6
		public ProtocolSession Session
		{
			get
			{
				return this.factory.Session;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00008FE3 File Offset: 0x000071E3
		public MailboxSession Store
		{
			get
			{
				return this.factory.Store;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00008FF0 File Offset: 0x000071F0
		// (set) Token: 0x0600023D RID: 573 RVA: 0x00008FF8 File Offset: 0x000071F8
		public string Arguments
		{
			get
			{
				return this.arguments;
			}
			set
			{
				this.arguments = value;
			}
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00009001 File Offset: 0x00007201
		public virtual bool VerifyState()
		{
			return true;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00009004 File Offset: 0x00007204
		public virtual void ParseArguments()
		{
			if (this.arguments != null && this.arguments.Length > 0)
			{
				this.parseResult = ParseResult.invalidNumberOfArguments;
				return;
			}
			this.parseResult = ParseResult.success;
		}

		// Token: 0x06000240 RID: 576
		public abstract ProtocolResponse Process();

		// Token: 0x06000241 RID: 577 RVA: 0x0000902B File Offset: 0x0000722B
		public override string ToString()
		{
			return this.factory.Session.ToString() + " " + base.ToString();
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000904D File Offset: 0x0000724D
		public virtual TimeSpan GetBudgetActionTimeout()
		{
			return Budget.GetMaxActionTime(CostType.CAS);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00009058 File Offset: 0x00007258
		internal Decoder GetPasswordDecoder(int codePage)
		{
			Decoder decoder = Encoding.GetEncoding(codePage).GetDecoder();
			if (decoder == null)
			{
				decoder = Encoding.GetEncoding(20127).GetDecoder();
			}
			return decoder;
		}

		// Token: 0x04000147 RID: 327
		private ResponseFactory factory;

		// Token: 0x04000148 RID: 328
		private string arguments;

		// Token: 0x04000149 RID: 329
		private ExPerformanceCounter perfCounterTotal;

		// Token: 0x0400014A RID: 330
		private ExPerformanceCounter perfCounterFailures;

		// Token: 0x0400014B RID: 331
		private ParseResult parseResult;
	}
}
