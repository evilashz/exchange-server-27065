using System;
using System.Text;
using System.Web.Security;

namespace Microsoft.Exchange.Common
{
	// Token: 0x02000054 RID: 84
	public static class PasswordHelper
	{
		// Token: 0x060001AC RID: 428 RVA: 0x0000854C File Offset: 0x0000674C
		private static bool IsIncludeSameSubstring(string left, string right, int length)
		{
			bool result = false;
			for (int i = 0; i <= left.Length - length; i++)
			{
				string value = left.Substring(i, length);
				if (right.IndexOf(value, StringComparison.Ordinal) != -1)
				{
					result = true;
					break;
				}
			}
			return result;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00008588 File Offset: 0x00006788
		public static string GetRandomPassword(string name, string samAccountName, int length)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			if (string.IsNullOrEmpty(samAccountName))
			{
				throw new ArgumentNullException("samAccountName");
			}
			if (length < 3)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			Random random = new Random(Environment.TickCount);
			string text;
			do
			{
				text = Membership.GeneratePassword(length, length / 4);
				string value = string.Format("{0}{1}{2}", (char)(65 + random.Next(26)), (char)(97 + random.Next(26)), (char)(48 + random.Next(10)));
				StringBuilder stringBuilder = new StringBuilder(text);
				stringBuilder.Remove(random.Next(length - 3), 3);
				stringBuilder.Insert(random.Next(length - 3), value);
				text = stringBuilder.ToString();
			}
			while ((samAccountName.Length >= 3 && text.IndexOf(samAccountName, StringComparison.Ordinal) != -1) || (name.Length >= 3 && PasswordHelper.IsIncludeSameSubstring(name, text, 3)));
			return text;
		}
	}
}
