using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Management.Hybrid;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200123B RID: 4667
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class HybridConfigurationAlreadyDefinedException : HybridConfigurationException
	{
		// Token: 0x0600BBF8 RID: 48120 RVA: 0x002ABAC4 File Offset: 0x002A9CC4
		public HybridConfigurationAlreadyDefinedException() : base(HybridStrings.ErrorHybridConfigurationAlreadyDefined)
		{
		}

		// Token: 0x0600BBF9 RID: 48121 RVA: 0x002ABAD1 File Offset: 0x002A9CD1
		public HybridConfigurationAlreadyDefinedException(Exception innerException) : base(HybridStrings.ErrorHybridConfigurationAlreadyDefined, innerException)
		{
		}

		// Token: 0x0600BBFA RID: 48122 RVA: 0x002ABADF File Offset: 0x002A9CDF
		protected HybridConfigurationAlreadyDefinedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600BBFB RID: 48123 RVA: 0x002ABAE9 File Offset: 0x002A9CE9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
