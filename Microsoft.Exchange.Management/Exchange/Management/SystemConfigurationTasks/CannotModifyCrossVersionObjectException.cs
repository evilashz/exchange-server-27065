using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000FA3 RID: 4003
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotModifyCrossVersionObjectException : LocalizedException
	{
		// Token: 0x0600ACFB RID: 44283 RVA: 0x00290D91 File Offset: 0x0028EF91
		public CannotModifyCrossVersionObjectException(string id) : base(Strings.ErrorCannotModifyCrossVersionObject(id))
		{
			this.id = id;
		}

		// Token: 0x0600ACFC RID: 44284 RVA: 0x00290DA6 File Offset: 0x0028EFA6
		public CannotModifyCrossVersionObjectException(string id, Exception innerException) : base(Strings.ErrorCannotModifyCrossVersionObject(id), innerException)
		{
			this.id = id;
		}

		// Token: 0x0600ACFD RID: 44285 RVA: 0x00290DBC File Offset: 0x0028EFBC
		protected CannotModifyCrossVersionObjectException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.id = (string)info.GetValue("id", typeof(string));
		}

		// Token: 0x0600ACFE RID: 44286 RVA: 0x00290DE6 File Offset: 0x0028EFE6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("id", this.id);
		}

		// Token: 0x1700378C RID: 14220
		// (get) Token: 0x0600ACFF RID: 44287 RVA: 0x00290E01 File Offset: 0x0028F001
		public string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x040060F2 RID: 24818
		private readonly string id;
	}
}
