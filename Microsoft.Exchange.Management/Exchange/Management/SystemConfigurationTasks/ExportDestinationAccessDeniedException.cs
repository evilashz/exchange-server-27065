using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F5C RID: 3932
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExportDestinationAccessDeniedException : LocalizedException
	{
		// Token: 0x0600ABC1 RID: 43969 RVA: 0x0028F7EF File Offset: 0x0028D9EF
		public ExportDestinationAccessDeniedException(string name) : base(Strings.ExportDestinationAccessDenied(name))
		{
			this.name = name;
		}

		// Token: 0x0600ABC2 RID: 43970 RVA: 0x0028F804 File Offset: 0x0028DA04
		public ExportDestinationAccessDeniedException(string name, Exception innerException) : base(Strings.ExportDestinationAccessDenied(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x0600ABC3 RID: 43971 RVA: 0x0028F81A File Offset: 0x0028DA1A
		protected ExportDestinationAccessDeniedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x0600ABC4 RID: 43972 RVA: 0x0028F844 File Offset: 0x0028DA44
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x1700376E RID: 14190
		// (get) Token: 0x0600ABC5 RID: 43973 RVA: 0x0028F85F File Offset: 0x0028DA5F
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x040060D4 RID: 24788
		private readonly string name;
	}
}
