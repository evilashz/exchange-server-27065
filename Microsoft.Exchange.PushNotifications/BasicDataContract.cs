using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x02000002 RID: 2
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.PushNotifications.Wcf")]
	internal class BasicDataContract
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020D8 File Offset: 0x000002D8
		public bool IsValid
		{
			get
			{
				if (this.isValid == null)
				{
					this.RunValidationCheck();
				}
				return this.isValid.Value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020F8 File Offset: 0x000002F8
		public List<LocalizedString> ValidationErrors
		{
			get
			{
				if (!this.IsValid)
				{
					return this.validationErrors;
				}
				throw new InvalidOperationException("ValidationErrors are not available when the instance is valid");
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002113 File Offset: 0x00000313
		public virtual string ToJson()
		{
			return JsonConverter.Serialize<BasicDataContract>(this, null);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000211C File Offset: 0x0000031C
		public string ToFullString()
		{
			if (this.toFullString == null)
			{
				StringBuilder stringBuilder = new StringBuilder().Append("{ ");
				this.InternalToFullString(stringBuilder);
				stringBuilder.Append("}");
				this.toFullString = stringBuilder.ToString();
			}
			return this.toFullString;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002166 File Offset: 0x00000366
		protected virtual void InternalToFullString(StringBuilder sb)
		{
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002168 File Offset: 0x00000368
		protected virtual void InternalValidate(List<LocalizedString> errors)
		{
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000216C File Offset: 0x0000036C
		private void RunValidationCheck()
		{
			List<LocalizedString> list = new List<LocalizedString>();
			this.InternalValidate(list);
			if (list.Count == 0)
			{
				this.isValid = new bool?(true);
				return;
			}
			this.validationErrors = list;
			this.isValid = new bool?(false);
		}

		// Token: 0x04000001 RID: 1
		private string toFullString;

		// Token: 0x04000002 RID: 2
		private bool? isValid;

		// Token: 0x04000003 RID: 3
		private List<LocalizedString> validationErrors;
	}
}
