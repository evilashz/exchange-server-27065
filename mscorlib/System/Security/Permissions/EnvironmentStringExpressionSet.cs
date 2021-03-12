using System;
using System.Security.Util;

namespace System.Security.Permissions
{
	// Token: 0x020002B0 RID: 688
	[Serializable]
	internal class EnvironmentStringExpressionSet : StringExpressionSet
	{
		// Token: 0x0600248E RID: 9358 RVA: 0x00084BB8 File Offset: 0x00082DB8
		public EnvironmentStringExpressionSet() : base(true, null, false)
		{
		}

		// Token: 0x0600248F RID: 9359 RVA: 0x00084BC3 File Offset: 0x00082DC3
		public EnvironmentStringExpressionSet(string str) : base(true, str, false)
		{
		}

		// Token: 0x06002490 RID: 9360 RVA: 0x00084BCE File Offset: 0x00082DCE
		protected override StringExpressionSet CreateNewEmpty()
		{
			return new EnvironmentStringExpressionSet();
		}

		// Token: 0x06002491 RID: 9361 RVA: 0x00084BD5 File Offset: 0x00082DD5
		protected override bool StringSubsetString(string left, string right, bool ignoreCase)
		{
			if (!ignoreCase)
			{
				return string.Compare(left, right, StringComparison.Ordinal) == 0;
			}
			return string.Compare(left, right, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06002492 RID: 9362 RVA: 0x00084BF1 File Offset: 0x00082DF1
		protected override string ProcessWholeString(string str)
		{
			return str;
		}

		// Token: 0x06002493 RID: 9363 RVA: 0x00084BF4 File Offset: 0x00082DF4
		protected override string ProcessSingleString(string str)
		{
			return str;
		}

		// Token: 0x06002494 RID: 9364 RVA: 0x00084BF7 File Offset: 0x00082DF7
		[SecuritySafeCritical]
		public override string ToString()
		{
			return base.UnsafeToString();
		}
	}
}
