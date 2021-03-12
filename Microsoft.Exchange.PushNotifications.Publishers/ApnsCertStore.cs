using System;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200001B RID: 27
	internal class ApnsCertStore
	{
		// Token: 0x060000F1 RID: 241 RVA: 0x000045E7 File Offset: 0x000027E7
		public ApnsCertStore(X509Store store)
		{
			if (store == null)
			{
				throw new ArgumentNullException("store");
			}
			this.Store = store;
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00004604 File Offset: 0x00002804
		public virtual X509Certificate2Collection Certificates
		{
			get
			{
				return this.Store.Certificates;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00004611 File Offset: 0x00002811
		public StoreLocation Location
		{
			get
			{
				return this.Store.Location;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x0000461E File Offset: 0x0000281E
		public string Name
		{
			get
			{
				return this.Store.Name;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x0000462B File Offset: 0x0000282B
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x00004633 File Offset: 0x00002833
		private X509Store Store { get; set; }

		// Token: 0x060000F7 RID: 247 RVA: 0x0000463C File Offset: 0x0000283C
		public virtual void Open(OpenFlags flags)
		{
			this.Store.Open(flags);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000464A File Offset: 0x0000284A
		public virtual void Close()
		{
			this.Store.Close();
		}
	}
}
