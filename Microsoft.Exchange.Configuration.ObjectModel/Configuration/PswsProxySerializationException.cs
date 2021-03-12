using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration
{
	// Token: 0x020002E3 RID: 739
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PswsProxySerializationException : PswsProxyException
	{
		// Token: 0x060019D3 RID: 6611 RVA: 0x0005DA75 File Offset: 0x0005BC75
		public PswsProxySerializationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x0005DA7E File Offset: 0x0005BC7E
		public PswsProxySerializationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060019D5 RID: 6613 RVA: 0x0005DA88 File Offset: 0x0005BC88
		protected PswsProxySerializationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x0005DA92 File Offset: 0x0005BC92
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
