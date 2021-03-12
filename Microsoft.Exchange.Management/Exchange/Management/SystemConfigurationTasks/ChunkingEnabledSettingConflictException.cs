using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F76 RID: 3958
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ChunkingEnabledSettingConflictException : LocalizedException
	{
		// Token: 0x0600AC39 RID: 44089 RVA: 0x00290150 File Offset: 0x0028E350
		public ChunkingEnabledSettingConflictException() : base(Strings.ChunkingEnabledSettingConflict)
		{
		}

		// Token: 0x0600AC3A RID: 44090 RVA: 0x0029015D File Offset: 0x0028E35D
		public ChunkingEnabledSettingConflictException(Exception innerException) : base(Strings.ChunkingEnabledSettingConflict, innerException)
		{
		}

		// Token: 0x0600AC3B RID: 44091 RVA: 0x0029016B File Offset: 0x0028E36B
		protected ChunkingEnabledSettingConflictException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AC3C RID: 44092 RVA: 0x00290175 File Offset: 0x0028E375
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
