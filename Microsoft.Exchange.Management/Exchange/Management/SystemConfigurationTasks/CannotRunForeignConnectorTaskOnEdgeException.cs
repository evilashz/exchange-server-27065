using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000FA1 RID: 4001
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotRunForeignConnectorTaskOnEdgeException : LocalizedException
	{
		// Token: 0x0600ACF3 RID: 44275 RVA: 0x00290D33 File Offset: 0x0028EF33
		public CannotRunForeignConnectorTaskOnEdgeException() : base(Strings.CannotRunForeignConnectorTaskOnEdge)
		{
		}

		// Token: 0x0600ACF4 RID: 44276 RVA: 0x00290D40 File Offset: 0x0028EF40
		public CannotRunForeignConnectorTaskOnEdgeException(Exception innerException) : base(Strings.CannotRunForeignConnectorTaskOnEdge, innerException)
		{
		}

		// Token: 0x0600ACF5 RID: 44277 RVA: 0x00290D4E File Offset: 0x0028EF4E
		protected CannotRunForeignConnectorTaskOnEdgeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600ACF6 RID: 44278 RVA: 0x00290D58 File Offset: 0x0028EF58
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
