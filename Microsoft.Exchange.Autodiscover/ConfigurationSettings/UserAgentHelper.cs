using System;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Autodiscover.ConfigurationSettings
{
	// Token: 0x02000043 RID: 67
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class UserAgentHelper
	{
		// Token: 0x060001B8 RID: 440 RVA: 0x00008A88 File Offset: 0x00006C88
		internal static bool IsWindowsClient(string userAgentString)
		{
			return UserAgentHelper.ValidateClientSoftwareVersions(userAgentString, new UserAgentHelper.WindowsVersionNumberPredicate(UserAgentHelper.AcceptAnyWindowsVersion), new UserAgentHelper.OfficeVersionNumberPredicate(UserAgentHelper.AcceptAnyOfficeVersion));
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00008ABD File Offset: 0x00006CBD
		internal static bool IsClientWin7OrGreater(string userAgentString)
		{
			return UserAgentHelper.ValidateClientSoftwareVersions(userAgentString, (int windowsVersionMajor, int windowsVersionMinor) => windowsVersionMajor > 6 || (windowsVersionMajor == 6 && windowsVersionMinor >= 1), new UserAgentHelper.OfficeVersionNumberPredicate(UserAgentHelper.AcceptAnyOfficeVersion));
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00008AF0 File Offset: 0x00006CF0
		internal static bool TryGetOfficeVersion(string userAgentString, out Version officeVersion)
		{
			officeVersion = null;
			int major = 0;
			int minor = 0;
			int build = 0;
			int num = 0;
			int num2 = 0;
			if (!string.IsNullOrEmpty(userAgentString) && UserAgentHelper.TryParseUserAgent(userAgentString, out major, out minor, out build, out num, out num2))
			{
				officeVersion = new Version(major, minor, build, 0);
				return true;
			}
			return false;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00008B34 File Offset: 0x00006D34
		public static bool ValidateClientSoftwareVersions(string userAgentString, UserAgentHelper.WindowsVersionNumberPredicate windowsVersionValidator, UserAgentHelper.OfficeVersionNumberPredicate officeVersionValidator)
		{
			if (!string.IsNullOrEmpty(userAgentString))
			{
				int majorVersion = 0;
				int minorVersion = 0;
				int buildNumber = 0;
				int majorVersion2 = 0;
				int minorVersion2 = 0;
				bool flag = UserAgentHelper.TryParseUserAgent(userAgentString, out majorVersion, out minorVersion, out buildNumber, out majorVersion2, out minorVersion2);
				return flag && windowsVersionValidator(majorVersion2, minorVersion2) && officeVersionValidator(majorVersion, minorVersion, buildNumber);
			}
			return false;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00008B84 File Offset: 0x00006D84
		private static bool TryParseUserAgent(string userAgentString, out int officeVersionMajor, out int officeVersionMinor, out int productBuildMajor, out int windowsVersionMajor, out int windowsVersionMinor)
		{
			officeVersionMajor = 0;
			officeVersionMinor = 0;
			productBuildMajor = 0;
			windowsVersionMajor = 0;
			windowsVersionMinor = 0;
			Match match = Regex.Match(userAgentString, "Microsoft Office/(\\d+)\\.(\\d+) \\(Windows NT (\\d+)\\.(\\d+)\\D*\\d+\\.\\d+\\.(\\d+).*\\)");
			if (match.Success)
			{
				try
				{
					officeVersionMajor = int.Parse(match.Groups[1].Value);
					officeVersionMinor = int.Parse(match.Groups[2].Value);
					windowsVersionMajor = int.Parse(match.Groups[3].Value);
					windowsVersionMinor = int.Parse(match.Groups[4].Value);
					productBuildMajor = int.Parse(match.Groups[5].Value);
					return true;
				}
				catch (FormatException)
				{
					return false;
				}
				catch (OverflowException)
				{
					return false;
				}
				return false;
			}
			return false;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00008C60 File Offset: 0x00006E60
		private static bool AcceptAnyWindowsVersion(int unusedMajor, int unusedMinor)
		{
			return true;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00008C63 File Offset: 0x00006E63
		private static bool AcceptAnyOfficeVersion(int unusedMajor, int unusedMinor, int unusedBuildNumber)
		{
			return true;
		}

		// Token: 0x02000044 RID: 68
		// (Invoke) Token: 0x060001C1 RID: 449
		internal delegate bool WindowsVersionNumberPredicate(int majorVersion, int minorVersion);

		// Token: 0x02000045 RID: 69
		// (Invoke) Token: 0x060001C5 RID: 453
		internal delegate bool OfficeVersionNumberPredicate(int majorVersion, int minorVersion, int buildNumber);
	}
}
