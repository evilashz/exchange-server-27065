using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000EC RID: 236
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ConfigurationSettingsException : LocalizedException
	{
		// Token: 0x0600083A RID: 2106 RVA: 0x0001B35E File Offset: 0x0001955E
		public ConfigurationSettingsException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x0001B367 File Offset: 0x00019567
		public ConfigurationSettingsException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0001B371 File Offset: 0x00019571
		protected ConfigurationSettingsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0001B37B File Offset: 0x0001957B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
