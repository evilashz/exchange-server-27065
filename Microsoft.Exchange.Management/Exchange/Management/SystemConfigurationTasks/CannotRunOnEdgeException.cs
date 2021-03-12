using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F4E RID: 3918
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotRunOnEdgeException : LocalizedException
	{
		// Token: 0x0600AB7E RID: 43902 RVA: 0x0028F223 File Offset: 0x0028D423
		public CannotRunOnEdgeException() : base(Strings.CannotRunOnEdge)
		{
		}

		// Token: 0x0600AB7F RID: 43903 RVA: 0x0028F230 File Offset: 0x0028D430
		public CannotRunOnEdgeException(Exception innerException) : base(Strings.CannotRunOnEdge, innerException)
		{
		}

		// Token: 0x0600AB80 RID: 43904 RVA: 0x0028F23E File Offset: 0x0028D43E
		protected CannotRunOnEdgeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AB81 RID: 43905 RVA: 0x0028F248 File Offset: 0x0028D448
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
