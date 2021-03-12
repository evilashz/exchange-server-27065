using System;
using System.Collections;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002B8 RID: 696
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UserConfigurationName
	{
		// Token: 0x06001CFC RID: 7420 RVA: 0x00085544 File Offset: 0x00083744
		public UserConfigurationName(string name, ConfigurationNameKind kind)
		{
			EnumValidator.ThrowIfInvalid<ConfigurationNameKind>(kind, "kind");
			this.kind = kind;
			this.name = UserConfigurationName.CheckConfigurationName(name, kind);
			switch (kind)
			{
			case ConfigurationNameKind.Name:
			case ConfigurationNameKind.PartialName:
				this.fullName = "IPM.Configuration." + this.name;
				return;
			case ConfigurationNameKind.ItemClass:
				this.fullName = this.name;
				return;
			default:
				throw new InvalidOperationException(string.Format("Unrecognized kind. Kind = {0}.", this.kind));
			}
		}

		// Token: 0x06001CFD RID: 7421 RVA: 0x000855CC File Offset: 0x000837CC
		internal static bool IsValidName(string name, ConfigurationNameKind kind)
		{
			string text;
			return UserConfigurationName.TryCheckConfigurationName(name, kind, out text);
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06001CFE RID: 7422 RVA: 0x000855E2 File Offset: 0x000837E2
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06001CFF RID: 7423 RVA: 0x000855EA File Offset: 0x000837EA
		public string FullName
		{
			get
			{
				return this.fullName;
			}
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06001D00 RID: 7424 RVA: 0x000855F2 File Offset: 0x000837F2
		internal ConfigurationNameKind Kind
		{
			get
			{
				return this.kind;
			}
		}

		// Token: 0x06001D01 RID: 7425 RVA: 0x000855FA File Offset: 0x000837FA
		internal static IComparer GetCustomComparer(UserConfigurationSearchFlags searchFlags)
		{
			return new UserConfigurationName.CustomConfigNameComparer(searchFlags);
		}

		// Token: 0x06001D02 RID: 7426 RVA: 0x00085602 File Offset: 0x00083802
		public override string ToString()
		{
			return string.Format("<{0}, Kind = {1}>", this.name, this.kind);
		}

		// Token: 0x06001D03 RID: 7427 RVA: 0x00085620 File Offset: 0x00083820
		private static string CheckConfigurationName(string name, ConfigurationNameKind kind)
		{
			Util.ThrowOnNullArgument(name, "name");
			string result;
			if (!UserConfigurationName.TryCheckConfigurationName(name, kind, out result))
			{
				throw new ArgumentException(ServerStrings.ExConfigNameInvalid(name));
			}
			return result;
		}

		// Token: 0x06001D04 RID: 7428 RVA: 0x00085658 File Offset: 0x00083858
		private static bool TryCheckConfigurationName(string name, ConfigurationNameKind kind, out string validatedName)
		{
			validatedName = null;
			int num = name.IndexOf("IPM.Configuration.");
			switch (kind)
			{
			case ConfigurationNameKind.Name:
			case ConfigurationNameKind.PartialName:
				if (num >= 0)
				{
					ExTraceGlobals.StorageTracer.TraceError<string>(0L, "UserConfigurationName::TryCheckConfigurationName. Invalid config name contains well known prefix. Name = {0}", name);
					return false;
				}
				break;
			case ConfigurationNameKind.ItemClass:
				if (num < 0)
				{
					ExTraceGlobals.StorageTracer.TraceError<string>(0L, "UserConfigurationName::TryCheckConfigurationName. Invalid full name. Name = {0}", name);
					return false;
				}
				name = name.Substring("IPM.Configuration.".Length);
				break;
			}
			if (name.Length == 0 && kind != ConfigurationNameKind.PartialName)
			{
				ExTraceGlobals.StorageTracer.TraceError<string, ConfigurationNameKind>(0L, "UserConfigurationName::TryCheckConfigurationName. ConfigName is empty. Name = {0}. Kind = {1}", name, kind);
				return false;
			}
			if (name.Length > UserConfigurationName.MaxUserConfigurationNameLength)
			{
				ExTraceGlobals.StorageTracer.TraceError<string, int, int>(0L, "UserConfigurationName::TryCheckConfigurationName. Name = {0}, Length = {1}, MaxLength = {2}.", name, name.Length, UserConfigurationName.MaxUserConfigurationNameLength);
				return false;
			}
			if (!UserConfigurationName.TryValidateConfigName(name))
			{
				return false;
			}
			validatedName = name;
			return true;
		}

		// Token: 0x06001D05 RID: 7429 RVA: 0x0008572C File Offset: 0x0008392C
		private static bool TryValidateConfigName(string configurationName)
		{
			char c = '.';
			foreach (char c2 in configurationName)
			{
				if ((c2 < 'a' || c2 > 'z') && (c2 < 'A' || c2 > 'Z') && (c2 < '0' || c2 > '9') && c2 != c)
				{
					ExTraceGlobals.StorageTracer.TraceError<char>(0L, "UserConfigurationName::CheckConfigurationName. The configuration name contains invalid character. Char = {0}.", c2);
					return false;
				}
			}
			return true;
		}

		// Token: 0x040013A2 RID: 5026
		public const string IPMConfiguration = "IPM.Configuration.";

		// Token: 0x040013A3 RID: 5027
		private readonly string name;

		// Token: 0x040013A4 RID: 5028
		private readonly string fullName;

		// Token: 0x040013A5 RID: 5029
		private readonly ConfigurationNameKind kind;

		// Token: 0x040013A6 RID: 5030
		private static readonly int IPMConfigurationLength = "IPM.Configuration.".Length;

		// Token: 0x040013A7 RID: 5031
		private static readonly int MaxUserConfigurationNameLength = 255 - UserConfigurationName.IPMConfigurationLength;

		// Token: 0x020002B9 RID: 697
		private class CustomConfigNameComparer : IComparer
		{
			// Token: 0x06001D07 RID: 7431 RVA: 0x000857B8 File Offset: 0x000839B8
			internal CustomConfigNameComparer(UserConfigurationSearchFlags searchFlags)
			{
				this.searchFlags = searchFlags;
			}

			// Token: 0x06001D08 RID: 7432 RVA: 0x000857C8 File Offset: 0x000839C8
			public int Compare(object x, object y)
			{
				string text = x as string;
				UserConfigurationName userConfigurationName = y as UserConfigurationName;
				if (text == null || userConfigurationName == null)
				{
					return -1;
				}
				switch (this.searchFlags)
				{
				case UserConfigurationSearchFlags.FullString:
					if (text == userConfigurationName.FullName)
					{
						return 0;
					}
					break;
				case UserConfigurationSearchFlags.SubString:
				{
					int num = text.IndexOf("IPM.Configuration.");
					int num2 = text.IndexOf(userConfigurationName.Name);
					if (num == 0 && num2 >= "IPM.Configuration.".Length)
					{
						return 0;
					}
					break;
				}
				case UserConfigurationSearchFlags.Prefix:
					if (text.IndexOf(userConfigurationName.FullName) == 0)
					{
						return 0;
					}
					break;
				}
				return -1;
			}

			// Token: 0x040013A8 RID: 5032
			private readonly UserConfigurationSearchFlags searchFlags;
		}
	}
}
