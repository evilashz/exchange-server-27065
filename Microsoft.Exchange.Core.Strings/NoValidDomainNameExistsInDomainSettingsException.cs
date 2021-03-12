using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Core
{
	// Token: 0x0200001B RID: 27
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoValidDomainNameExistsInDomainSettingsException : LocalizedException
	{
		// Token: 0x060003AB RID: 939 RVA: 0x0000D17E File Offset: 0x0000B37E
		public NoValidDomainNameExistsInDomainSettingsException() : base(CoreStrings.NoValidDomainNameExistsInDomainSettings)
		{
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000D18B File Offset: 0x0000B38B
		public NoValidDomainNameExistsInDomainSettingsException(Exception innerException) : base(CoreStrings.NoValidDomainNameExistsInDomainSettings, innerException)
		{
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000D199 File Offset: 0x0000B399
		protected NoValidDomainNameExistsInDomainSettingsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000D1A3 File Offset: 0x0000B3A3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
