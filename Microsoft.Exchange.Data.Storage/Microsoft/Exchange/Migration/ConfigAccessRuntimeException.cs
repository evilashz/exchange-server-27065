using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020001A6 RID: 422
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ConfigAccessRuntimeException : AnchorRuntimeException
	{
		// Token: 0x06001793 RID: 6035 RVA: 0x00070EB5 File Offset: 0x0006F0B5
		public ConfigAccessRuntimeException() : base(Strings.ConfigAccessRuntimeError)
		{
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x00070EC2 File Offset: 0x0006F0C2
		public ConfigAccessRuntimeException(Exception innerException) : base(Strings.ConfigAccessRuntimeError, innerException)
		{
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x00070ED0 File Offset: 0x0006F0D0
		protected ConfigAccessRuntimeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x00070EDA File Offset: 0x0006F0DA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
