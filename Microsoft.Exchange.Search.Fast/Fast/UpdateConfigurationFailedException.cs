using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Search.Fast
{
	// Token: 0x02000034 RID: 52
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UpdateConfigurationFailedException : ComponentFailedPermanentException
	{
		// Token: 0x0600029C RID: 668 RVA: 0x0000F331 File Offset: 0x0000D531
		public UpdateConfigurationFailedException() : base(Strings.UpdateConfigurationFailed)
		{
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000F33E File Offset: 0x0000D53E
		public UpdateConfigurationFailedException(Exception innerException) : base(Strings.UpdateConfigurationFailed, innerException)
		{
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000F34C File Offset: 0x0000D54C
		protected UpdateConfigurationFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000F356 File Offset: 0x0000D556
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
