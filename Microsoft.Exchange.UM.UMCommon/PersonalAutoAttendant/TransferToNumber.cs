using System;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.PersonalAutoAttendant
{
	// Token: 0x0200011D RID: 285
	internal class TransferToNumber : KeyMapping<string>, IPhoneNumberTarget
	{
		// Token: 0x06000939 RID: 2361 RVA: 0x000241EA File Offset: 0x000223EA
		internal TransferToNumber(int key, string context, string number) : base(KeyMappingTypeEnum.TransferToNumber, key, context, number)
		{
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x000241F6 File Offset: 0x000223F6
		internal string PhoneNumberString
		{
			get
			{
				return base.Data;
			}
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x00024200 File Offset: 0x00022400
		public override bool Validate(IDataValidator validator)
		{
			IDataValidationResult dataValidationResult;
			bool result = validator.ValidatePhoneNumberForOutdialing(this.PhoneNumberString, out dataValidationResult);
			this.numberToDial = dataValidationResult.PhoneNumber;
			base.ValidationResult = dataValidationResult.PAAValidationResult;
			return result;
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x00024235 File Offset: 0x00022435
		public PhoneNumber GetDialableNumber()
		{
			return this.numberToDial;
		}

		// Token: 0x04000524 RID: 1316
		private PhoneNumber numberToDial;
	}
}
