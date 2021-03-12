using System;
using System.Text;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.Server.Storage.MapiDisp
{
	// Token: 0x02000010 RID: 16
	public sealed class ParserConnectionInformation : IConnectionInformation
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600025D RID: 605 RVA: 0x000373B2 File Offset: 0x000355B2
		public bool ClientSupportsBackoffResult
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600025E RID: 606 RVA: 0x000373B5 File Offset: 0x000355B5
		public bool ClientSupportsBufferTooSmallBreakup
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600025F RID: 607 RVA: 0x000373B8 File Offset: 0x000355B8
		public ushort SessionId
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000260 RID: 608 RVA: 0x000373BB File Offset: 0x000355BB
		public Encoding String8Encoding
		{
			get
			{
				return Encoding.ASCII;
			}
		}
	}
}
