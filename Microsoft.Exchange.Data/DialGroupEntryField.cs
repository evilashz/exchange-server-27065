using System;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001C3 RID: 451
	[Serializable]
	internal abstract class DialGroupEntryField
	{
		// Token: 0x06000FDE RID: 4062 RVA: 0x000307D5 File Offset: 0x0002E9D5
		protected DialGroupEntryField(string data, string fieldName)
		{
			this.data = data;
			this.fieldName = fieldName;
			this.Validate();
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06000FDF RID: 4063 RVA: 0x000307F1 File Offset: 0x0002E9F1
		protected string Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x000307F9 File Offset: 0x0002E9F9
		public override string ToString()
		{
			return this.Data;
		}

		// Token: 0x06000FE1 RID: 4065
		protected abstract void Validate();

		// Token: 0x06000FE2 RID: 4066 RVA: 0x00030801 File Offset: 0x0002EA01
		protected void ValidateNullOrEmpty()
		{
			if (string.IsNullOrEmpty(this.Data))
			{
				throw new ArgumentNullException(this.fieldName);
			}
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x0003081C File Offset: 0x0002EA1C
		protected void ValidateMaxLength(int maxLength)
		{
			if (this.Data.Length > maxLength)
			{
				throw new FormatException(DataStrings.InvalidDialGroupEntryElementLength(this.fieldName, this.Data, maxLength));
			}
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x00030849 File Offset: 0x0002EA49
		protected void ValidateRegex(Regex regex)
		{
			if (!regex.IsMatch(this.Data))
			{
				throw new FormatException(DataStrings.InvalidDialGroupEntryElementFormat(this.fieldName));
			}
		}

		// Token: 0x04000978 RID: 2424
		private string data;

		// Token: 0x04000979 RID: 2425
		private string fieldName;
	}
}
