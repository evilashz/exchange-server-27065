using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x0200018C RID: 396
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidConfigurationObjectTypeException : TroubleshootingCmdletException
	{
		// Token: 0x06000FA8 RID: 4008 RVA: 0x0003670B File Offset: 0x0003490B
		public InvalidConfigurationObjectTypeException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x00036714 File Offset: 0x00034914
		public InvalidConfigurationObjectTypeException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x0003671E File Offset: 0x0003491E
		protected InvalidConfigurationObjectTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x00036728 File Offset: 0x00034928
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
