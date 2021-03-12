using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F68 RID: 3944
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CatchAllRecipientTypeNotAllowedException : LocalizedException
	{
		// Token: 0x0600ABF9 RID: 44025 RVA: 0x0028FC6B File Offset: 0x0028DE6B
		public CatchAllRecipientTypeNotAllowedException() : base(Strings.CatchAllRecipientTypeNotAllowed)
		{
		}

		// Token: 0x0600ABFA RID: 44026 RVA: 0x0028FC78 File Offset: 0x0028DE78
		public CatchAllRecipientTypeNotAllowedException(Exception innerException) : base(Strings.CatchAllRecipientTypeNotAllowed, innerException)
		{
		}

		// Token: 0x0600ABFB RID: 44027 RVA: 0x0028FC86 File Offset: 0x0028DE86
		protected CatchAllRecipientTypeNotAllowedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600ABFC RID: 44028 RVA: 0x0028FC90 File Offset: 0x0028DE90
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
