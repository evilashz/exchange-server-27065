using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.Versioning
{
	// Token: 0x020006EF RID: 1775
	[FriendAccessAllowed]
	internal static class BinaryCompatibility
	{
		// Token: 0x17000D45 RID: 3397
		// (get) Token: 0x06005024 RID: 20516 RVA: 0x00119EAF File Offset: 0x001180AF
		[FriendAccessAllowed]
		internal static bool TargetsAtLeast_Phone_V7_1
		{
			[FriendAccessAllowed]
			get
			{
				return BinaryCompatibility.s_map.TargetsAtLeast_Phone_V7_1;
			}
		}

		// Token: 0x17000D46 RID: 3398
		// (get) Token: 0x06005025 RID: 20517 RVA: 0x00119EBB File Offset: 0x001180BB
		[FriendAccessAllowed]
		internal static bool TargetsAtLeast_Phone_V8_0
		{
			[FriendAccessAllowed]
			get
			{
				return BinaryCompatibility.s_map.TargetsAtLeast_Phone_V8_0;
			}
		}

		// Token: 0x17000D47 RID: 3399
		// (get) Token: 0x06005026 RID: 20518 RVA: 0x00119EC7 File Offset: 0x001180C7
		[FriendAccessAllowed]
		internal static bool TargetsAtLeast_Desktop_V4_5
		{
			[FriendAccessAllowed]
			get
			{
				return BinaryCompatibility.s_map.TargetsAtLeast_Desktop_V4_5;
			}
		}

		// Token: 0x17000D48 RID: 3400
		// (get) Token: 0x06005027 RID: 20519 RVA: 0x00119ED3 File Offset: 0x001180D3
		[FriendAccessAllowed]
		internal static bool TargetsAtLeast_Desktop_V4_5_1
		{
			[FriendAccessAllowed]
			get
			{
				return BinaryCompatibility.s_map.TargetsAtLeast_Desktop_V4_5_1;
			}
		}

		// Token: 0x17000D49 RID: 3401
		// (get) Token: 0x06005028 RID: 20520 RVA: 0x00119EDF File Offset: 0x001180DF
		[FriendAccessAllowed]
		internal static bool TargetsAtLeast_Desktop_V4_5_2
		{
			[FriendAccessAllowed]
			get
			{
				return BinaryCompatibility.s_map.TargetsAtLeast_Desktop_V4_5_2;
			}
		}

		// Token: 0x17000D4A RID: 3402
		// (get) Token: 0x06005029 RID: 20521 RVA: 0x00119EEB File Offset: 0x001180EB
		[FriendAccessAllowed]
		internal static bool TargetsAtLeast_Desktop_V4_5_3
		{
			[FriendAccessAllowed]
			get
			{
				return BinaryCompatibility.s_map.TargetsAtLeast_Desktop_V4_5_3;
			}
		}

		// Token: 0x17000D4B RID: 3403
		// (get) Token: 0x0600502A RID: 20522 RVA: 0x00119EF7 File Offset: 0x001180F7
		[FriendAccessAllowed]
		internal static bool TargetsAtLeast_Desktop_V4_5_4
		{
			[FriendAccessAllowed]
			get
			{
				return BinaryCompatibility.s_map.TargetsAtLeast_Desktop_V4_5_4;
			}
		}

		// Token: 0x17000D4C RID: 3404
		// (get) Token: 0x0600502B RID: 20523 RVA: 0x00119F03 File Offset: 0x00118103
		[FriendAccessAllowed]
		internal static bool TargetsAtLeast_Desktop_V5_0
		{
			[FriendAccessAllowed]
			get
			{
				return BinaryCompatibility.s_map.TargetsAtLeast_Desktop_V5_0;
			}
		}

		// Token: 0x17000D4D RID: 3405
		// (get) Token: 0x0600502C RID: 20524 RVA: 0x00119F0F File Offset: 0x0011810F
		[FriendAccessAllowed]
		internal static bool TargetsAtLeast_Silverlight_V4
		{
			[FriendAccessAllowed]
			get
			{
				return BinaryCompatibility.s_map.TargetsAtLeast_Silverlight_V4;
			}
		}

		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x0600502D RID: 20525 RVA: 0x00119F1B File Offset: 0x0011811B
		[FriendAccessAllowed]
		internal static bool TargetsAtLeast_Silverlight_V5
		{
			[FriendAccessAllowed]
			get
			{
				return BinaryCompatibility.s_map.TargetsAtLeast_Silverlight_V5;
			}
		}

		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x0600502E RID: 20526 RVA: 0x00119F27 File Offset: 0x00118127
		[FriendAccessAllowed]
		internal static bool TargetsAtLeast_Silverlight_V6
		{
			[FriendAccessAllowed]
			get
			{
				return BinaryCompatibility.s_map.TargetsAtLeast_Silverlight_V6;
			}
		}

		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x0600502F RID: 20527 RVA: 0x00119F33 File Offset: 0x00118133
		[FriendAccessAllowed]
		internal static TargetFrameworkId AppWasBuiltForFramework
		{
			[FriendAccessAllowed]
			get
			{
				if (BinaryCompatibility.s_AppWasBuiltForFramework == TargetFrameworkId.NotYetChecked)
				{
					BinaryCompatibility.ReadTargetFrameworkId();
				}
				return BinaryCompatibility.s_AppWasBuiltForFramework;
			}
		}

		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x06005030 RID: 20528 RVA: 0x00119F46 File Offset: 0x00118146
		[FriendAccessAllowed]
		internal static int AppWasBuiltForVersion
		{
			[FriendAccessAllowed]
			get
			{
				if (BinaryCompatibility.s_AppWasBuiltForFramework == TargetFrameworkId.NotYetChecked)
				{
					BinaryCompatibility.ReadTargetFrameworkId();
				}
				return BinaryCompatibility.s_AppWasBuiltForVersion;
			}
		}

		// Token: 0x06005031 RID: 20529 RVA: 0x00119F5C File Offset: 0x0011815C
		private static bool ParseTargetFrameworkMonikerIntoEnum(string targetFrameworkMoniker, out TargetFrameworkId targetFramework, out int targetFrameworkVersion)
		{
			targetFramework = TargetFrameworkId.NotYetChecked;
			targetFrameworkVersion = 0;
			string a = null;
			string text = null;
			BinaryCompatibility.ParseFrameworkName(targetFrameworkMoniker, out a, out targetFrameworkVersion, out text);
			if (!(a == ".NETFramework"))
			{
				if (!(a == ".NETPortable"))
				{
					if (!(a == ".NETCore"))
					{
						if (!(a == "WindowsPhone"))
						{
							if (!(a == "WindowsPhoneApp"))
							{
								if (!(a == "Silverlight"))
								{
									targetFramework = TargetFrameworkId.Unrecognized;
								}
								else
								{
									targetFramework = TargetFrameworkId.Silverlight;
									if (!string.IsNullOrEmpty(text))
									{
										if (text == "WindowsPhone")
										{
											targetFramework = TargetFrameworkId.Phone;
											targetFrameworkVersion = 70000;
										}
										else if (text == "WindowsPhone71")
										{
											targetFramework = TargetFrameworkId.Phone;
											targetFrameworkVersion = 70100;
										}
										else if (text == "WindowsPhone8")
										{
											targetFramework = TargetFrameworkId.Phone;
											targetFrameworkVersion = 80000;
										}
										else if (text.StartsWith("WindowsPhone", StringComparison.Ordinal))
										{
											targetFramework = TargetFrameworkId.Unrecognized;
											targetFrameworkVersion = 70100;
										}
										else
										{
											targetFramework = TargetFrameworkId.Unrecognized;
										}
									}
								}
							}
							else
							{
								targetFramework = TargetFrameworkId.Phone;
							}
						}
						else if (targetFrameworkVersion >= 80100)
						{
							targetFramework = TargetFrameworkId.Phone;
						}
						else
						{
							targetFramework = TargetFrameworkId.Unspecified;
						}
					}
					else
					{
						targetFramework = TargetFrameworkId.NetCore;
					}
				}
				else
				{
					targetFramework = TargetFrameworkId.Portable;
				}
			}
			else
			{
				targetFramework = TargetFrameworkId.NetFramework;
			}
			return true;
		}

		// Token: 0x06005032 RID: 20530 RVA: 0x0011A080 File Offset: 0x00118280
		private static void ParseFrameworkName(string frameworkName, out string identifier, out int version, out string profile)
		{
			if (frameworkName == null)
			{
				throw new ArgumentNullException("frameworkName");
			}
			if (frameworkName.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_StringZeroLength"), "frameworkName");
			}
			string[] array = frameworkName.Split(new char[]
			{
				','
			});
			version = 0;
			if (array.Length < 2 || array.Length > 3)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_FrameworkNameTooShort"), "frameworkName");
			}
			identifier = array[0].Trim();
			if (identifier.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_FrameworkNameInvalid"), "frameworkName");
			}
			bool flag = false;
			profile = null;
			for (int i = 1; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(new char[]
				{
					'='
				});
				if (array2.Length != 2)
				{
					throw new ArgumentException(Environment.GetResourceString("SR.Argument_FrameworkNameInvalid"), "frameworkName");
				}
				string text = array2[0].Trim();
				string text2 = array2[1].Trim();
				if (text.Equals("Version", StringComparison.OrdinalIgnoreCase))
				{
					flag = true;
					if (text2.Length > 0 && (text2[0] == 'v' || text2[0] == 'V'))
					{
						text2 = text2.Substring(1);
					}
					Version version2 = new Version(text2);
					version = version2.Major * 10000;
					if (version2.Minor > 0)
					{
						version += version2.Minor * 100;
					}
					if (version2.Build > 0)
					{
						version += version2.Build;
					}
				}
				else
				{
					if (!text.Equals("Profile", StringComparison.OrdinalIgnoreCase))
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_FrameworkNameInvalid"), "frameworkName");
					}
					if (!string.IsNullOrEmpty(text2))
					{
						profile = text2;
					}
				}
			}
			if (!flag)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_FrameworkNameMissingVersion"), "frameworkName");
			}
		}

		// Token: 0x06005033 RID: 20531 RVA: 0x0011A244 File Offset: 0x00118444
		[SecuritySafeCritical]
		private static void ReadTargetFrameworkId()
		{
			string text = AppDomain.CurrentDomain.GetTargetFrameworkName();
			string valueInternal = CompatibilitySwitch.GetValueInternal("TargetFrameworkMoniker");
			if (!string.IsNullOrEmpty(valueInternal))
			{
				text = valueInternal;
			}
			int num = 0;
			TargetFrameworkId targetFrameworkId;
			if (text == null)
			{
				targetFrameworkId = TargetFrameworkId.Unspecified;
			}
			else if (!BinaryCompatibility.ParseTargetFrameworkMonikerIntoEnum(text, out targetFrameworkId, out num))
			{
				targetFrameworkId = TargetFrameworkId.Unrecognized;
			}
			BinaryCompatibility.s_AppWasBuiltForFramework = targetFrameworkId;
			BinaryCompatibility.s_AppWasBuiltForVersion = num;
		}

		// Token: 0x04002346 RID: 9030
		private static TargetFrameworkId s_AppWasBuiltForFramework;

		// Token: 0x04002347 RID: 9031
		private static int s_AppWasBuiltForVersion;

		// Token: 0x04002348 RID: 9032
		private static readonly BinaryCompatibility.BinaryCompatibilityMap s_map = new BinaryCompatibility.BinaryCompatibilityMap();

		// Token: 0x04002349 RID: 9033
		private const char c_componentSeparator = ',';

		// Token: 0x0400234A RID: 9034
		private const char c_keyValueSeparator = '=';

		// Token: 0x0400234B RID: 9035
		private const char c_versionValuePrefix = 'v';

		// Token: 0x0400234C RID: 9036
		private const string c_versionKey = "Version";

		// Token: 0x0400234D RID: 9037
		private const string c_profileKey = "Profile";

		// Token: 0x02000C2F RID: 3119
		private sealed class BinaryCompatibilityMap
		{
			// Token: 0x06006F50 RID: 28496 RVA: 0x0017E8C0 File Offset: 0x0017CAC0
			internal BinaryCompatibilityMap()
			{
				this.AddQuirksForFramework(BinaryCompatibility.AppWasBuiltForFramework, BinaryCompatibility.AppWasBuiltForVersion);
			}

			// Token: 0x06006F51 RID: 28497 RVA: 0x0017E8D8 File Offset: 0x0017CAD8
			private void AddQuirksForFramework(TargetFrameworkId builtAgainstFramework, int buildAgainstVersion)
			{
				switch (builtAgainstFramework)
				{
				case TargetFrameworkId.NotYetChecked:
				case TargetFrameworkId.Unrecognized:
				case TargetFrameworkId.Unspecified:
				case TargetFrameworkId.Portable:
					break;
				case TargetFrameworkId.NetFramework:
				case TargetFrameworkId.NetCore:
					if (buildAgainstVersion >= 50000)
					{
						this.TargetsAtLeast_Desktop_V5_0 = true;
					}
					if (buildAgainstVersion >= 40504)
					{
						this.TargetsAtLeast_Desktop_V4_5_4 = true;
					}
					if (buildAgainstVersion >= 40503)
					{
						this.TargetsAtLeast_Desktop_V4_5_3 = true;
					}
					if (buildAgainstVersion >= 40502)
					{
						this.TargetsAtLeast_Desktop_V4_5_2 = true;
					}
					if (buildAgainstVersion >= 40501)
					{
						this.TargetsAtLeast_Desktop_V4_5_1 = true;
					}
					if (buildAgainstVersion >= 40500)
					{
						this.TargetsAtLeast_Desktop_V4_5 = true;
						this.AddQuirksForFramework(TargetFrameworkId.Phone, 70100);
						this.AddQuirksForFramework(TargetFrameworkId.Silverlight, 50000);
						return;
					}
					break;
				case TargetFrameworkId.Silverlight:
					if (buildAgainstVersion >= 40000)
					{
						this.TargetsAtLeast_Silverlight_V4 = true;
					}
					if (buildAgainstVersion >= 50000)
					{
						this.TargetsAtLeast_Silverlight_V5 = true;
					}
					if (buildAgainstVersion >= 60000)
					{
						this.TargetsAtLeast_Silverlight_V6 = true;
					}
					break;
				case TargetFrameworkId.Phone:
					if (buildAgainstVersion >= 80000)
					{
						this.TargetsAtLeast_Phone_V8_0 = true;
					}
					if (buildAgainstVersion >= 80100)
					{
						this.TargetsAtLeast_Desktop_V4_5 = true;
						this.TargetsAtLeast_Desktop_V4_5_1 = true;
					}
					if (buildAgainstVersion >= 710)
					{
						this.TargetsAtLeast_Phone_V7_1 = true;
						return;
					}
					break;
				default:
					return;
				}
			}

			// Token: 0x040036DB RID: 14043
			internal bool TargetsAtLeast_Phone_V7_1;

			// Token: 0x040036DC RID: 14044
			internal bool TargetsAtLeast_Phone_V8_0;

			// Token: 0x040036DD RID: 14045
			internal bool TargetsAtLeast_Phone_V8_1;

			// Token: 0x040036DE RID: 14046
			internal bool TargetsAtLeast_Desktop_V4_5;

			// Token: 0x040036DF RID: 14047
			internal bool TargetsAtLeast_Desktop_V4_5_1;

			// Token: 0x040036E0 RID: 14048
			internal bool TargetsAtLeast_Desktop_V4_5_2;

			// Token: 0x040036E1 RID: 14049
			internal bool TargetsAtLeast_Desktop_V4_5_3;

			// Token: 0x040036E2 RID: 14050
			internal bool TargetsAtLeast_Desktop_V4_5_4;

			// Token: 0x040036E3 RID: 14051
			internal bool TargetsAtLeast_Desktop_V5_0;

			// Token: 0x040036E4 RID: 14052
			internal bool TargetsAtLeast_Silverlight_V4;

			// Token: 0x040036E5 RID: 14053
			internal bool TargetsAtLeast_Silverlight_V5;

			// Token: 0x040036E6 RID: 14054
			internal bool TargetsAtLeast_Silverlight_V6;
		}
	}
}
