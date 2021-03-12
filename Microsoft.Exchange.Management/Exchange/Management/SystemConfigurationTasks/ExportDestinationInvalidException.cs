using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F5D RID: 3933
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExportDestinationInvalidException : LocalizedException
	{
		// Token: 0x0600ABC6 RID: 43974 RVA: 0x0028F867 File Offset: 0x0028DA67
		public ExportDestinationInvalidException(string path) : base(Strings.ExportDestinationInvalid(path))
		{
			this.path = path;
		}

		// Token: 0x0600ABC7 RID: 43975 RVA: 0x0028F87C File Offset: 0x0028DA7C
		public ExportDestinationInvalidException(string path, Exception innerException) : base(Strings.ExportDestinationInvalid(path), innerException)
		{
			this.path = path;
		}

		// Token: 0x0600ABC8 RID: 43976 RVA: 0x0028F892 File Offset: 0x0028DA92
		protected ExportDestinationInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.path = (string)info.GetValue("path", typeof(string));
		}

		// Token: 0x0600ABC9 RID: 43977 RVA: 0x0028F8BC File Offset: 0x0028DABC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("path", this.path);
		}

		// Token: 0x1700376F RID: 14191
		// (get) Token: 0x0600ABCA RID: 43978 RVA: 0x0028F8D7 File Offset: 0x0028DAD7
		public string Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x040060D5 RID: 24789
		private readonly string path;
	}
}
