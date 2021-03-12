using System;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Pop3
{
	// Token: 0x02000006 RID: 6
	internal abstract class Pop3Request : ProtocolRequest
	{
		// Token: 0x06000023 RID: 35 RVA: 0x00002CFA File Offset: 0x00000EFA
		protected Pop3Request(ResponseFactory factory, string arguments) : base(factory, arguments)
		{
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002D04 File Offset: 0x00000F04
		public Pop3ResponseFactory Factory
		{
			get
			{
				return (Pop3ResponseFactory)base.ResponseFactory;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002D11 File Offset: 0x00000F11
		protected Pop3CountersInstance Pop3CountersInstance
		{
			get
			{
				return ((Pop3VirtualServer)ProtocolBaseServices.VirtualServer).Pop3CountersInstance;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002D22 File Offset: 0x00000F22
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00002D2A File Offset: 0x00000F2A
		protected Pop3State AllowedStates
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

		// Token: 0x06000028 RID: 40 RVA: 0x00002D34 File Offset: 0x00000F34
		public override bool VerifyState()
		{
			Pop3ResponseFactory factory = this.Factory;
			return factory != null && (this.AllowedStates & factory.SessionState) != Pop3State.None;
		}

		// Token: 0x04000018 RID: 24
		internal const string MultiLineStart = "\r\n";

		// Token: 0x04000019 RID: 25
		internal const string MultiLineEnd = ".";

		// Token: 0x0400001A RID: 26
		internal const string TwoArg = "{0} {1}\r\n";

		// Token: 0x0400001B RID: 27
		private Pop3State allowedStates;
	}
}
