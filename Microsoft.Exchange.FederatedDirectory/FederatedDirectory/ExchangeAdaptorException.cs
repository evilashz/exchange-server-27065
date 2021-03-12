using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.FederatedDirectory
{
	// Token: 0x0200001A RID: 26
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExchangeAdaptorException : LocalizedException
	{
		// Token: 0x060000FC RID: 252 RVA: 0x00005CA9 File Offset: 0x00003EA9
		public ExchangeAdaptorException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005CB2 File Offset: 0x00003EB2
		public ExchangeAdaptorException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005CBC File Offset: 0x00003EBC
		protected ExchangeAdaptorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005CC6 File Offset: 0x00003EC6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
