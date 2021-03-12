using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F5E RID: 3934
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExportDestinationIsReadonlyException : LocalizedException
	{
		// Token: 0x0600ABCB RID: 43979 RVA: 0x0028F8DF File Offset: 0x0028DADF
		public ExportDestinationIsReadonlyException(string name) : base(Strings.ExportDestinationIsReadonly(name))
		{
			this.name = name;
		}

		// Token: 0x0600ABCC RID: 43980 RVA: 0x0028F8F4 File Offset: 0x0028DAF4
		public ExportDestinationIsReadonlyException(string name, Exception innerException) : base(Strings.ExportDestinationIsReadonly(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x0600ABCD RID: 43981 RVA: 0x0028F90A File Offset: 0x0028DB0A
		protected ExportDestinationIsReadonlyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x0600ABCE RID: 43982 RVA: 0x0028F934 File Offset: 0x0028DB34
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x17003770 RID: 14192
		// (get) Token: 0x0600ABCF RID: 43983 RVA: 0x0028F94F File Offset: 0x0028DB4F
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x040060D6 RID: 24790
		private readonly string name;
	}
}
