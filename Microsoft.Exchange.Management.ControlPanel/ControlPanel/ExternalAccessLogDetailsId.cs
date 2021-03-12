using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003CF RID: 975
	internal class ExternalAccessLogDetailsId
	{
		// Token: 0x17001FA3 RID: 8099
		// (get) Token: 0x0600323B RID: 12859 RVA: 0x0009C0DF File Offset: 0x0009A2DF
		// (set) Token: 0x0600323C RID: 12860 RVA: 0x0009C0E7 File Offset: 0x0009A2E7
		public string ObjectModified { get; private set; }

		// Token: 0x17001FA4 RID: 8100
		// (get) Token: 0x0600323D RID: 12861 RVA: 0x0009C0F0 File Offset: 0x0009A2F0
		// (set) Token: 0x0600323E RID: 12862 RVA: 0x0009C0F8 File Offset: 0x0009A2F8
		public string Cmdlet { get; private set; }

		// Token: 0x17001FA5 RID: 8101
		// (get) Token: 0x0600323F RID: 12863 RVA: 0x0009C101 File Offset: 0x0009A301
		// (set) Token: 0x06003240 RID: 12864 RVA: 0x0009C109 File Offset: 0x0009A309
		public string StartDate { get; private set; }

		// Token: 0x17001FA6 RID: 8102
		// (get) Token: 0x06003241 RID: 12865 RVA: 0x0009C112 File Offset: 0x0009A312
		// (set) Token: 0x06003242 RID: 12866 RVA: 0x0009C11A File Offset: 0x0009A31A
		public string EndDate { get; private set; }

		// Token: 0x06003243 RID: 12867 RVA: 0x0009C123 File Offset: 0x0009A323
		public ExternalAccessLogDetailsId(Identity identity)
		{
			this.Parse(identity.RawIdentity);
		}

		// Token: 0x06003244 RID: 12868 RVA: 0x0009C138 File Offset: 0x0009A338
		protected virtual void Parse(string rawIdentity)
		{
			string[] array = rawIdentity.Split(new char[]
			{
				';'
			});
			if (array.Length >= 4)
			{
				this.ObjectModified = array[0];
				this.Cmdlet = array[1];
				this.StartDate = array[2];
				this.EndDate = array[3];
				return;
			}
			throw new FaultException(new ArgumentException("rawIdentity").Message);
		}
	}
}
