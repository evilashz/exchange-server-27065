using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001185 RID: 4485
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExchangeSettingsException : LocalizedException
	{
		// Token: 0x0600B68B RID: 46731 RVA: 0x002A0101 File Offset: 0x0029E301
		public ExchangeSettingsException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600B68C RID: 46732 RVA: 0x002A010A File Offset: 0x0029E30A
		public ExchangeSettingsException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600B68D RID: 46733 RVA: 0x002A0114 File Offset: 0x0029E314
		protected ExchangeSettingsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B68E RID: 46734 RVA: 0x002A011E File Offset: 0x0029E31E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
