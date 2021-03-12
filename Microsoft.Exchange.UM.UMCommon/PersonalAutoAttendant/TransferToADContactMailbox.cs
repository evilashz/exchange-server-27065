using System;

namespace Microsoft.Exchange.UM.PersonalAutoAttendant
{
	// Token: 0x02000119 RID: 281
	internal class TransferToADContactMailbox : TransferToADContact
	{
		// Token: 0x0600092E RID: 2350 RVA: 0x0002407E File Offset: 0x0002227E
		internal TransferToADContactMailbox(int key, string context, string legacyExchangeDN) : base(KeyMappingTypeEnum.TransferToADContactMailbox, key, context, legacyExchangeDN)
		{
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0002408C File Offset: 0x0002228C
		public override bool Validate(IDataValidator validator)
		{
			IDataValidationResult dataValidationResult;
			bool result = validator.ValidateADContactForTransferToMailbox(base.LegacyExchangeDN, out dataValidationResult);
			base.ValidationResult = dataValidationResult.PAAValidationResult;
			return result;
		}
	}
}
