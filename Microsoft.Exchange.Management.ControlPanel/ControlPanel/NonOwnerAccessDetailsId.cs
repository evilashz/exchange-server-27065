using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003E8 RID: 1000
	internal class NonOwnerAccessDetailsId : AuditLogDetailsId
	{
		// Token: 0x1700201D RID: 8221
		// (get) Token: 0x06003357 RID: 13143 RVA: 0x0009F3EE File Offset: 0x0009D5EE
		// (set) Token: 0x06003358 RID: 13144 RVA: 0x0009F3F6 File Offset: 0x0009D5F6
		public string LogonTypes { get; private set; }

		// Token: 0x06003359 RID: 13145 RVA: 0x0009F3FF File Offset: 0x0009D5FF
		public NonOwnerAccessDetailsId(Identity identity) : base(identity)
		{
			this.Parse(identity.RawIdentity);
		}

		// Token: 0x0600335A RID: 13146 RVA: 0x0009F414 File Offset: 0x0009D614
		protected override void Parse(string rawIdentity)
		{
			string[] array = rawIdentity.Split(new char[]
			{
				';'
			});
			if (array.Length == 4)
			{
				this.LogonTypes = array[3];
				base.Parse(rawIdentity);
				return;
			}
			throw new FaultException(new ArgumentException("rawIdentity").Message);
		}
	}
}
