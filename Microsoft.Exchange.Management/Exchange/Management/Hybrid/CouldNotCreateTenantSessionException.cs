using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x02001237 RID: 4663
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CouldNotCreateTenantSessionException : LocalizedException
	{
		// Token: 0x0600BBE4 RID: 48100 RVA: 0x002AB8E0 File Offset: 0x002A9AE0
		public CouldNotCreateTenantSessionException(Exception e) : base(HybridStrings.HybridCouldNotCreateTenantSessionException(e))
		{
			this.e = e;
		}

		// Token: 0x0600BBE5 RID: 48101 RVA: 0x002AB8F5 File Offset: 0x002A9AF5
		public CouldNotCreateTenantSessionException(Exception e, Exception innerException) : base(HybridStrings.HybridCouldNotCreateTenantSessionException(e), innerException)
		{
			this.e = e;
		}

		// Token: 0x0600BBE6 RID: 48102 RVA: 0x002AB90B File Offset: 0x002A9B0B
		protected CouldNotCreateTenantSessionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.e = (Exception)info.GetValue("e", typeof(Exception));
		}

		// Token: 0x0600BBE7 RID: 48103 RVA: 0x002AB935 File Offset: 0x002A9B35
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("e", this.e);
		}

		// Token: 0x17003B3F RID: 15167
		// (get) Token: 0x0600BBE8 RID: 48104 RVA: 0x002AB950 File Offset: 0x002A9B50
		public Exception E
		{
			get
			{
				return this.e;
			}
		}

		// Token: 0x04006605 RID: 26117
		private readonly Exception e;
	}
}
