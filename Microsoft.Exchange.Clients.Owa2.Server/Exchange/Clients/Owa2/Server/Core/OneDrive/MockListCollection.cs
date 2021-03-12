using System;
using Microsoft.SharePoint.Client;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.OneDrive
{
	// Token: 0x0200001F RID: 31
	public class MockListCollection : MockClientObject<ListCollection>, IListCollection, IClientObject<ListCollection>
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x0000300A File Offset: 0x0000120A
		public MockListCollection(MockClientContext context)
		{
			this.context = context;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003019 File Offset: 0x00001219
		public override void LoadMockData()
		{
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000301B File Offset: 0x0000121B
		public IList GetByTitle(string title)
		{
			return new MockList(this.context, title);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003029 File Offset: 0x00001229
		public IList GetById(Guid guid)
		{
			return new MockList(this.context);
		}

		// Token: 0x0400003A RID: 58
		private MockClientContext context;
	}
}
