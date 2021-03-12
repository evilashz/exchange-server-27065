using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000169 RID: 361
	[Serializable]
	public class LdapPolicy
	{
		// Token: 0x06000BFB RID: 3067 RVA: 0x0002548B File Offset: 0x0002368B
		internal LdapPolicy(string policyName, int value)
		{
			if (string.IsNullOrEmpty(policyName))
			{
				throw new ArgumentNullException("policyName");
			}
			this.PolicyName = policyName;
			this.Value = value;
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000BFC RID: 3068 RVA: 0x000254B4 File Offset: 0x000236B4
		// (set) Token: 0x06000BFD RID: 3069 RVA: 0x000254BC File Offset: 0x000236BC
		public string PolicyName { get; private set; }

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000BFE RID: 3070 RVA: 0x000254C5 File Offset: 0x000236C5
		// (set) Token: 0x06000BFF RID: 3071 RVA: 0x000254CD File Offset: 0x000236CD
		public int Value { get; private set; }

		// Token: 0x06000C00 RID: 3072 RVA: 0x000254D8 File Offset: 0x000236D8
		public static LdapPolicy Parse(string input)
		{
			if (string.IsNullOrEmpty(input))
			{
				throw new ArgumentNullException("input");
			}
			string[] array = input.Split(LdapPolicy.PolicySeparatorArray, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length != 2)
			{
				throw new FormatException(DataStrings.InvalidFormat);
			}
			return new LdapPolicy(array[0].Trim(), int.Parse(array[1]));
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x00025530 File Offset: 0x00023730
		public string ToADString()
		{
			return this.PolicyName + "=" + this.Value;
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x0002554D File Offset: 0x0002374D
		public override string ToString()
		{
			return this.ToADString();
		}

		// Token: 0x0400073F RID: 1855
		private const string PolicySeparator = "=";

		// Token: 0x04000740 RID: 1856
		private static readonly string[] PolicySeparatorArray = new string[]
		{
			"="
		};
	}
}
