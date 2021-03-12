using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020002B2 RID: 690
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SharedConfigurationValidationException : LocalizedException
	{
		// Token: 0x060018EC RID: 6380 RVA: 0x0005C793 File Offset: 0x0005A993
		public SharedConfigurationValidationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x0005C79C File Offset: 0x0005A99C
		public SharedConfigurationValidationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060018EE RID: 6382 RVA: 0x0005C7A6 File Offset: 0x0005A9A6
		protected SharedConfigurationValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060018EF RID: 6383 RVA: 0x0005C7B0 File Offset: 0x0005A9B0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
