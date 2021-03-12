using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F4F RID: 3919
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotRunOnSubscribedEdgeException : LocalizedException
	{
		// Token: 0x0600AB82 RID: 43906 RVA: 0x0028F252 File Offset: 0x0028D452
		public CannotRunOnSubscribedEdgeException() : base(Strings.CannotRunOnSubscribedEdge)
		{
		}

		// Token: 0x0600AB83 RID: 43907 RVA: 0x0028F25F File Offset: 0x0028D45F
		public CannotRunOnSubscribedEdgeException(Exception innerException) : base(Strings.CannotRunOnSubscribedEdge, innerException)
		{
		}

		// Token: 0x0600AB84 RID: 43908 RVA: 0x0028F26D File Offset: 0x0028D46D
		protected CannotRunOnSubscribedEdgeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AB85 RID: 43909 RVA: 0x0028F277 File Offset: 0x0028D477
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
