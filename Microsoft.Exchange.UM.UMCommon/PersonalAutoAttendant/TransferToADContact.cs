using System;

namespace Microsoft.Exchange.UM.PersonalAutoAttendant
{
	// Token: 0x02000118 RID: 280
	internal abstract class TransferToADContact : KeyMapping<string>
	{
		// Token: 0x0600092B RID: 2347 RVA: 0x00024062 File Offset: 0x00022262
		internal TransferToADContact(KeyMappingTypeEnum keyMappingType, int key, string context, string legacyExchangeDN) : base(keyMappingType, key, context, legacyExchangeDN)
		{
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x0002406F File Offset: 0x0002226F
		internal string LegacyExchangeDN
		{
			get
			{
				return base.Data;
			}
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x00024077 File Offset: 0x00022277
		public override bool Validate(IDataValidator validator)
		{
			throw new NotImplementedException();
		}
	}
}
