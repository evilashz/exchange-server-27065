using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.AirSync
{
	// Token: 0x02000E2F RID: 3631
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TaskNotSupportedOnVersionException : LocalizedException
	{
		// Token: 0x0600A5F3 RID: 42483 RVA: 0x00286E3E File Offset: 0x0028503E
		public TaskNotSupportedOnVersionException(string cmdletName, int serverVersion) : base(Strings.TaskNotSupportedOnVersionException(cmdletName, serverVersion))
		{
			this.cmdletName = cmdletName;
			this.serverVersion = serverVersion;
		}

		// Token: 0x0600A5F4 RID: 42484 RVA: 0x00286E5B File Offset: 0x0028505B
		public TaskNotSupportedOnVersionException(string cmdletName, int serverVersion, Exception innerException) : base(Strings.TaskNotSupportedOnVersionException(cmdletName, serverVersion), innerException)
		{
			this.cmdletName = cmdletName;
			this.serverVersion = serverVersion;
		}

		// Token: 0x0600A5F5 RID: 42485 RVA: 0x00286E7C File Offset: 0x0028507C
		protected TaskNotSupportedOnVersionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.cmdletName = (string)info.GetValue("cmdletName", typeof(string));
			this.serverVersion = (int)info.GetValue("serverVersion", typeof(int));
		}

		// Token: 0x0600A5F6 RID: 42486 RVA: 0x00286ED1 File Offset: 0x002850D1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("cmdletName", this.cmdletName);
			info.AddValue("serverVersion", this.serverVersion);
		}

		// Token: 0x17003654 RID: 13908
		// (get) Token: 0x0600A5F7 RID: 42487 RVA: 0x00286EFD File Offset: 0x002850FD
		public string CmdletName
		{
			get
			{
				return this.cmdletName;
			}
		}

		// Token: 0x17003655 RID: 13909
		// (get) Token: 0x0600A5F8 RID: 42488 RVA: 0x00286F05 File Offset: 0x00285105
		public int ServerVersion
		{
			get
			{
				return this.serverVersion;
			}
		}

		// Token: 0x04005FBA RID: 24506
		private readonly string cmdletName;

		// Token: 0x04005FBB RID: 24507
		private readonly int serverVersion;
	}
}
