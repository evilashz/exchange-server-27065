using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200016B RID: 363
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ProvisioningThrottledTransientException : MigrationTransientException
	{
		// Token: 0x0600167D RID: 5757 RVA: 0x0006F791 File Offset: 0x0006D991
		public ProvisioningThrottledTransientException() : base(Strings.ProvisioningThrottledBack)
		{
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x0006F79E File Offset: 0x0006D99E
		public ProvisioningThrottledTransientException(Exception innerException) : base(Strings.ProvisioningThrottledBack, innerException)
		{
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x0006F7AC File Offset: 0x0006D9AC
		protected ProvisioningThrottledTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x0006F7B6 File Offset: 0x0006D9B6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
