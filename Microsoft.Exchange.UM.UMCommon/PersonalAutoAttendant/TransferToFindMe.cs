using System;
using System.Globalization;

namespace Microsoft.Exchange.UM.PersonalAutoAttendant
{
	// Token: 0x0200011C RID: 284
	internal class TransferToFindMe : KeyMapping<FindMeNumbers>
	{
		// Token: 0x06000934 RID: 2356 RVA: 0x00024103 File Offset: 0x00022303
		internal TransferToFindMe(int key, string context, FindMeNumbers findmenumbers) : base(KeyMappingTypeEnum.FindMe, key, context, findmenumbers)
		{
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x0002410F File Offset: 0x0002230F
		internal FindMeNumbers Numbers
		{
			get
			{
				return base.Data;
			}
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x00024118 File Offset: 0x00022318
		public override bool Validate(IDataValidator validator)
		{
			PAAValidationResult validationResult = PAAValidationResult.Valid;
			bool result = true;
			for (int i = 0; i < this.Numbers.Count; i++)
			{
				FindMe findMe = this.Numbers[i];
				IDataValidationResult dataValidationResult;
				if (!validator.ValidatePhoneNumberForOutdialing(findMe.Number, out dataValidationResult))
				{
					validationResult = dataValidationResult.PAAValidationResult;
					result = false;
				}
				findMe.PhoneNumber = dataValidationResult.PhoneNumber;
				findMe.ValidationResult = dataValidationResult.PAAValidationResult;
			}
			base.ValidationResult = validationResult;
			return result;
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0002418C File Offset: 0x0002238C
		internal void AddFindMe(string number, int timeout)
		{
			this.AddFindMe(number, timeout, string.Empty);
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0002419C File Offset: 0x0002239C
		internal void AddFindMe(string number, int timeout, string label)
		{
			FindMeNumbers data = base.Data;
			if (data.NumberList.Length >= 3)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Cannot add more than {0} findme numbers", new object[]
				{
					3
				}));
			}
			data.Add(number, timeout, label);
		}

		// Token: 0x04000523 RID: 1315
		private const int MaxFindMeNumbers = 3;
	}
}
