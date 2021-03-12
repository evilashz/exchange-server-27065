using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000050 RID: 80
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class NonOperationalAdmissionControlException : LocalizedException
	{
		// Token: 0x060002EB RID: 747 RVA: 0x0000DBE1 File Offset: 0x0000BDE1
		public NonOperationalAdmissionControlException(ResourceKey resource) : base(Strings.NonOperationalAdmissionControl(resource))
		{
			this.resource = resource;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000DBF6 File Offset: 0x0000BDF6
		public NonOperationalAdmissionControlException(ResourceKey resource, Exception innerException) : base(Strings.NonOperationalAdmissionControl(resource), innerException)
		{
			this.resource = resource;
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000DC0C File Offset: 0x0000BE0C
		protected NonOperationalAdmissionControlException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.resource = (ResourceKey)info.GetValue("resource", typeof(ResourceKey));
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000DC36 File Offset: 0x0000BE36
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("resource", this.resource);
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000DC51 File Offset: 0x0000BE51
		public ResourceKey Resource
		{
			get
			{
				return this.resource;
			}
		}

		// Token: 0x0400019A RID: 410
		private readonly ResourceKey resource;
	}
}
