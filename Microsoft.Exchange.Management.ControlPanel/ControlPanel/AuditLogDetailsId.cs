using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003B9 RID: 953
	internal class AuditLogDetailsId
	{
		// Token: 0x17001F8D RID: 8077
		// (get) Token: 0x060031DF RID: 12767 RVA: 0x0009A0A2 File Offset: 0x000982A2
		// (set) Token: 0x060031E0 RID: 12768 RVA: 0x0009A0AA File Offset: 0x000982AA
		public string Object { get; private set; }

		// Token: 0x17001F8E RID: 8078
		// (get) Token: 0x060031E1 RID: 12769 RVA: 0x0009A0B3 File Offset: 0x000982B3
		// (set) Token: 0x060031E2 RID: 12770 RVA: 0x0009A0BB File Offset: 0x000982BB
		public string StartDate { get; private set; }

		// Token: 0x17001F8F RID: 8079
		// (get) Token: 0x060031E3 RID: 12771 RVA: 0x0009A0C4 File Offset: 0x000982C4
		// (set) Token: 0x060031E4 RID: 12772 RVA: 0x0009A0CC File Offset: 0x000982CC
		public string EndDate { get; private set; }

		// Token: 0x060031E5 RID: 12773 RVA: 0x0009A0D5 File Offset: 0x000982D5
		public AuditLogDetailsId(Identity identity)
		{
			this.Parse(identity.RawIdentity);
		}

		// Token: 0x060031E6 RID: 12774 RVA: 0x0009A0EC File Offset: 0x000982EC
		protected virtual void Parse(string rawIdentity)
		{
			string[] array = rawIdentity.Split(new char[]
			{
				';'
			});
			if (array.Length >= 3)
			{
				this.Object = array[0];
				this.StartDate = array[1];
				this.EndDate = array[2];
				return;
			}
			throw new FaultException(new ArgumentException("rawIdentity").Message);
		}
	}
}
