using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E09 RID: 3593
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExAllHostedOrganizationsGroupNotFoundException : LocalizedException
	{
		// Token: 0x0600A528 RID: 42280 RVA: 0x00285800 File Offset: 0x00283A00
		public ExAllHostedOrganizationsGroupNotFoundException(Guid guid) : base(Strings.ExAllHostedOrganizationsGroupNotFoundException(guid))
		{
			this.guid = guid;
		}

		// Token: 0x0600A529 RID: 42281 RVA: 0x00285815 File Offset: 0x00283A15
		public ExAllHostedOrganizationsGroupNotFoundException(Guid guid, Exception innerException) : base(Strings.ExAllHostedOrganizationsGroupNotFoundException(guid), innerException)
		{
			this.guid = guid;
		}

		// Token: 0x0600A52A RID: 42282 RVA: 0x0028582B File Offset: 0x00283A2B
		protected ExAllHostedOrganizationsGroupNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.guid = (Guid)info.GetValue("guid", typeof(Guid));
		}

		// Token: 0x0600A52B RID: 42283 RVA: 0x00285855 File Offset: 0x00283A55
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("guid", this.guid);
		}

		// Token: 0x17003621 RID: 13857
		// (get) Token: 0x0600A52C RID: 42284 RVA: 0x00285875 File Offset: 0x00283A75
		public Guid Guid
		{
			get
			{
				return this.guid;
			}
		}

		// Token: 0x04005F87 RID: 24455
		private readonly Guid guid;
	}
}
