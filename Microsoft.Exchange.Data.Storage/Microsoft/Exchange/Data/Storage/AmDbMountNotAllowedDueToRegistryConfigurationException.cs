using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000DD RID: 221
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmDbMountNotAllowedDueToRegistryConfigurationException : AmServerException
	{
		// Token: 0x060012CC RID: 4812 RVA: 0x0006839F File Offset: 0x0006659F
		public AmDbMountNotAllowedDueToRegistryConfigurationException() : base(ServerStrings.AmDbMountNotAllowedDueToRegistryConfigurationException)
		{
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x000683B1 File Offset: 0x000665B1
		public AmDbMountNotAllowedDueToRegistryConfigurationException(Exception innerException) : base(ServerStrings.AmDbMountNotAllowedDueToRegistryConfigurationException, innerException)
		{
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x000683C4 File Offset: 0x000665C4
		protected AmDbMountNotAllowedDueToRegistryConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x000683CE File Offset: 0x000665CE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
