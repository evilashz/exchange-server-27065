using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.UM.PersonalAutoAttendant
{
	// Token: 0x020000E4 RID: 228
	internal abstract class CallerIdBase : IDataItem
	{
		// Token: 0x06000793 RID: 1939 RVA: 0x0001DE57 File Offset: 0x0001C057
		internal CallerIdBase(CallerIdTypeEnum type)
		{
			this.callerIdType = type;
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x0001DE66 File Offset: 0x0001C066
		// (set) Token: 0x06000795 RID: 1941 RVA: 0x0001DE6E File Offset: 0x0001C06E
		internal CallerIdTypeEnum CallerIdType
		{
			get
			{
				return this.callerIdType;
			}
			set
			{
				this.callerIdType = value;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x0001DE77 File Offset: 0x0001C077
		// (set) Token: 0x06000797 RID: 1943 RVA: 0x0001DE7F File Offset: 0x0001C07F
		internal PAAValidationResult ValidationResult
		{
			get
			{
				return this.validationResult;
			}
			set
			{
				this.validationResult = value;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x0001DE88 File Offset: 0x0001C088
		internal virtual int EvaluationCost
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000799 RID: 1945
		public abstract bool Validate(IDataValidator validator);

		// Token: 0x0600079A RID: 1946 RVA: 0x0001DE8F File Offset: 0x0001C08F
		internal static ContactFolderCallerId CreateDefaultContactFolder()
		{
			return new ContactFolderCallerId();
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0001DE96 File Offset: 0x0001C096
		internal static ContactItemCallerId CreateContactItem(StoreObjectId contactItem)
		{
			return new ContactItemCallerId(contactItem);
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0001DE9E File Offset: 0x0001C09E
		internal static PhoneNumberCallerId CreatePhoneNumber(string phoneNumber)
		{
			return new PhoneNumberCallerId(phoneNumber);
		}

		// Token: 0x0600079D RID: 1949
		internal abstract bool Evaluate(IRuleEvaluator evaluator);

		// Token: 0x04000459 RID: 1113
		private CallerIdTypeEnum callerIdType;

		// Token: 0x0400045A RID: 1114
		private PAAValidationResult validationResult;
	}
}
