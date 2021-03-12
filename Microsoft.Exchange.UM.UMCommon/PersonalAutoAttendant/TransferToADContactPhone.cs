using System;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.PersonalAutoAttendant
{
	// Token: 0x0200011B RID: 283
	internal class TransferToADContactPhone : TransferToADContact, IPhoneNumberTarget
	{
		// Token: 0x06000931 RID: 2353 RVA: 0x000240B7 File Offset: 0x000222B7
		internal TransferToADContactPhone(int key, string context, string legacyExchangeDN) : base(KeyMappingTypeEnum.TransferToADContactPhone, key, context, legacyExchangeDN)
		{
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x000240C4 File Offset: 0x000222C4
		public override bool Validate(IDataValidator validator)
		{
			IDataValidationResult dataValidationResult;
			bool result = validator.ValidateADContactForOutdialing(base.LegacyExchangeDN, out dataValidationResult);
			this.numberToDial = dataValidationResult.PhoneNumber;
			base.ValidationResult = dataValidationResult.PAAValidationResult;
			return result;
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x000240FB File Offset: 0x000222FB
		public PhoneNumber GetDialableNumber()
		{
			return this.numberToDial;
		}

		// Token: 0x04000522 RID: 1314
		private PhoneNumber numberToDial;
	}
}
