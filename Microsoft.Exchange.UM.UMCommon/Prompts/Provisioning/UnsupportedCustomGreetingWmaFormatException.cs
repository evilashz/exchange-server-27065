using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.Prompts.Provisioning
{
	// Token: 0x020001CB RID: 459
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnsupportedCustomGreetingWmaFormatException : PublishingException
	{
		// Token: 0x06000F1C RID: 3868 RVA: 0x00035FFF File Offset: 0x000341FF
		public UnsupportedCustomGreetingWmaFormatException() : base(Strings.UnsupportedCustomGreetingWmaFormat)
		{
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x0003600C File Offset: 0x0003420C
		public UnsupportedCustomGreetingWmaFormatException(Exception innerException) : base(Strings.UnsupportedCustomGreetingWmaFormat, innerException)
		{
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x0003601A File Offset: 0x0003421A
		protected UnsupportedCustomGreetingWmaFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x00036024 File Offset: 0x00034224
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
