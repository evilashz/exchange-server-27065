using System;
using System.Text;

namespace Microsoft.Exchange.EdgeSync
{
	// Token: 0x0200001C RID: 28
	internal static class DistinguishedName
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x000040C5 File Offset: 0x000022C5
		public static bool IsEmpty(string name)
		{
			return string.IsNullOrEmpty(name);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000040D0 File Offset: 0x000022D0
		public static string Parent(string name)
		{
			if (DistinguishedName.IsEmpty(name))
			{
				throw new InvalidOperationException("cannot get parent of empty");
			}
			int num = name.IndexOf(',');
			if (num < 0)
			{
				return DistinguishedName.Empty;
			}
			return name.Substring(num + 1);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000410C File Offset: 0x0000230C
		public static bool IsChildOf(string child, string parent)
		{
			return child.EndsWith(parent);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004118 File Offset: 0x00002318
		public static string Concatinate(params string[] components)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (string text in components)
			{
				if (!DistinguishedName.IsEmpty(text))
				{
					if (!flag)
					{
						stringBuilder.Append(',');
					}
					stringBuilder.Append(text);
					flag = false;
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000416A File Offset: 0x0000236A
		public static string MakeRelativePath(string child, string parent)
		{
			if (!DistinguishedName.IsChildOf(child, parent))
			{
				throw new InvalidOperationException("invalid suffix");
			}
			if (DistinguishedName.Equals(child, parent))
			{
				return DistinguishedName.Empty;
			}
			return child.Substring(0, child.Length - parent.Length - 1);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000041A5 File Offset: 0x000023A5
		public static bool Equals(string s1, string s2)
		{
			return string.Equals(s1, s2, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000041B0 File Offset: 0x000023B0
		public static string RemoveLeafRelativeDistinguishedNames(string name, int n)
		{
			int num = 1;
			while (n > 0)
			{
				num = name.IndexOf(',', num);
				if (num < 0)
				{
					throw new InvalidOperationException("no RDN to remove");
				}
				num++;
				n--;
			}
			return name.Substring(num);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000041F0 File Offset: 0x000023F0
		public static string ExtractRDN(string name)
		{
			return name.Split(new char[]
			{
				','
			}, 2)[0];
		}

		// Token: 0x04000059 RID: 89
		public const string EdgeOrganizationRelativePath = "CN=First Organization,CN=Microsoft Exchange,CN=Services";

		// Token: 0x0400005A RID: 90
		public const string EdgeServersRelativePath = "CN=Servers,CN=Exchange Administrative Group (FYDIBOHF23SPDLT),CN=Administrative Groups,CN=First Organization,CN=Microsoft Exchange,CN=Services";

		// Token: 0x0400005B RID: 91
		public static readonly string Empty = string.Empty;
	}
}
