using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;

namespace Microsoft.Exchange.Configuration.ObjectModel
{
	// Token: 0x02000029 RID: 41
	internal class ValidationResult : Dictionary<string, string>
	{
		// Token: 0x0600017C RID: 380 RVA: 0x00006B18 File Offset: 0x00004D18
		public ValidationResult()
		{
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00006B32 File Offset: 0x00004D32
		public ValidationResult(string mess)
		{
			this.errorMessage = mess;
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00006B54 File Offset: 0x00004D54
		// (set) Token: 0x0600017F RID: 383 RVA: 0x00006B84 File Offset: 0x00004D84
		public string ErrorMessage
		{
			get
			{
				string text = this.errorMessage;
				if (!this.Valid && string.IsNullOrEmpty(text))
				{
					text = Strings.InvalidProperties;
				}
				return text;
			}
			set
			{
				this.errorMessage = value;
				this.valid &= string.IsNullOrEmpty(this.errorMessage);
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00006BA5 File Offset: 0x00004DA5
		// (set) Token: 0x06000181 RID: 385 RVA: 0x00006BAD File Offset: 0x00004DAD
		public bool Valid
		{
			get
			{
				return this.valid;
			}
			set
			{
				this.valid = value;
			}
		}

		// Token: 0x1700005E RID: 94
		public new string this[string PropertyID]
		{
			get
			{
				string result = "";
				base.TryGetValue(PropertyID, out result);
				return result;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					base.Add(PropertyID, value);
					this.valid = false;
				}
			}
		}

		// Token: 0x0400007B RID: 123
		private string errorMessage = "";

		// Token: 0x0400007C RID: 124
		private bool valid = true;
	}
}
