using System;

namespace Microsoft.Exchange.DxStore.HA.Instance
{
	// Token: 0x02000003 RID: 3
	public class Program
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002428 File Offset: 0x00000628
		public static int Main(string[] args)
		{
			string text = null;
			string text2 = null;
			bool isZeroboxMode = false;
			foreach (string arg in args)
			{
				if (Program.IsArgumentMatches(arg, "-ZeroboxMode"))
				{
					isZeroboxMode = true;
				}
				else
				{
					if (string.IsNullOrEmpty(text))
					{
						text = Program.GetArgumentValue(arg, "-GroupName:");
						if (!string.IsNullOrEmpty(text))
						{
							goto IL_5C;
						}
					}
					if (string.IsNullOrEmpty(text2))
					{
						text2 = Program.GetArgumentValue(arg, "-Self:");
						string.IsNullOrEmpty(text2);
					}
				}
				IL_5C:;
			}
			if (string.IsNullOrEmpty(text))
			{
				Program.ReportError("Group name not specified", new object[0]);
				return 1;
			}
			DxStoreInstanceHost dxStoreInstanceHost = new DxStoreInstanceHost(text, text2, isZeroboxMode);
			if (dxStoreInstanceHost.Start(true))
			{
				dxStoreInstanceHost.WaitForStop();
				return 0;
			}
			Program.ReportError("Failed to start instance", new object[0]);
			return -1;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000024E7 File Offset: 0x000006E7
		public static bool IsArgumentMatches(string arg, string switchName)
		{
			return string.Equals(arg, switchName, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000024F1 File Offset: 0x000006F1
		public static string GetArgumentValue(string arg, string optionName)
		{
			if (arg.StartsWith(optionName))
			{
				return arg.Substring(optionName.Length);
			}
			return null;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000250C File Offset: 0x0000070C
		private static void ReportError(string formatString, params object[] args)
		{
			ConsoleColor foregroundColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(formatString, args);
			Console.ForegroundColor = foregroundColor;
		}

		// Token: 0x04000007 RID: 7
		public const string GroupNameSwitch = "-GroupName:";

		// Token: 0x04000008 RID: 8
		public const string SelfSwitch = "-Self:";

		// Token: 0x04000009 RID: 9
		public const string ZeroboxModeSwitch = "-ZeroboxMode";
	}
}
