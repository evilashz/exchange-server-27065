using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001188 RID: 4488
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExchangeSettingsEnableUsageException : ExchangeSettingsException
	{
		// Token: 0x0600B699 RID: 46745 RVA: 0x002A0218 File Offset: 0x0029E418
		public ExchangeSettingsEnableUsageException() : base(Strings.ExchangeSettingsEnableUsage)
		{
		}

		// Token: 0x0600B69A RID: 46746 RVA: 0x002A0225 File Offset: 0x0029E425
		public ExchangeSettingsEnableUsageException(Exception innerException) : base(Strings.ExchangeSettingsEnableUsage, innerException)
		{
		}

		// Token: 0x0600B69B RID: 46747 RVA: 0x002A0233 File Offset: 0x0029E433
		protected ExchangeSettingsEnableUsageException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B69C RID: 46748 RVA: 0x002A023D File Offset: 0x0029E43D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
