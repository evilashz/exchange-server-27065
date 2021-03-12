using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.Prompts.Provisioning
{
	// Token: 0x020001CA RID: 458
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnsupportedCustomGreetingWaveFormatException : PublishingException
	{
		// Token: 0x06000F18 RID: 3864 RVA: 0x00035FD0 File Offset: 0x000341D0
		public UnsupportedCustomGreetingWaveFormatException() : base(Strings.UnsupportedCustomGreetingWaveFormat)
		{
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x00035FDD File Offset: 0x000341DD
		public UnsupportedCustomGreetingWaveFormatException(Exception innerException) : base(Strings.UnsupportedCustomGreetingWaveFormat, innerException)
		{
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x00035FEB File Offset: 0x000341EB
		protected UnsupportedCustomGreetingWaveFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x00035FF5 File Offset: 0x000341F5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
