using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.VariantConfiguration
{
	// Token: 0x0200014A RID: 330
	public sealed class VariantType
	{
		// Token: 0x06000F33 RID: 3891 RVA: 0x00026527 File Offset: 0x00024727
		private VariantType(string name, Type type, VariantTypeFlags flags, Func<string, bool> valueValidator)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			this.Name = name;
			this.Type = type;
			this.Flags = flags;
			this.ValidateValue = valueValidator;
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x0002655F File Offset: 0x0002475F
		public static VariantType Create(string name, Type type, VariantTypeFlags flags)
		{
			return VariantType.Create(name, type, flags, VariantType.GetDefaultValidator(type));
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x0002656F File Offset: 0x0002476F
		public static VariantType Create(string name, Type type, VariantTypeFlags flags, Func<string, bool> valueValidator)
		{
			return new VariantType(name, type, flags, valueValidator);
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x0002657A File Offset: 0x0002477A
		public static implicit operator string(VariantType variant)
		{
			return variant.Name;
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x00026584 File Offset: 0x00024784
		private static VariantTypeCollection InitializeVariants()
		{
			List<VariantType> list = new List<VariantType>();
			foreach (FieldInfo fieldInfo in typeof(VariantType).GetFields(BindingFlags.Static | BindingFlags.Public))
			{
				VariantType variantType = fieldInfo.GetValue(null) as VariantType;
				if (variantType != null)
				{
					list.Add(variantType);
				}
			}
			return VariantTypeCollection.Create(list);
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x000265DD File Offset: 0x000247DD
		private static bool ServiceValidator(string value)
		{
			return VariantType.ServiceVariantValues.Contains(value);
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x000265EA File Offset: 0x000247EA
		private static bool ModeValidator(string value)
		{
			return VariantType.ModeVariantValues.Contains(value);
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x000265F7 File Offset: 0x000247F7
		private static bool RegionValidator(string value)
		{
			return value.Length == 3;
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x00026604 File Offset: 0x00024804
		private static bool BoolValidator(string value)
		{
			bool flag;
			return bool.TryParse(value, out flag);
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x00026619 File Offset: 0x00024819
		private static bool GuidValidator(string value)
		{
			return VariantType.GuidRegex.IsMatch(value);
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x00026626 File Offset: 0x00024826
		private static bool VersionValidator(string value)
		{
			return VariantType.VersionRegex.IsMatch(value);
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x00026633 File Offset: 0x00024833
		private static bool DefaultValidator(string value)
		{
			return true;
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x00026638 File Offset: 0x00024838
		private static Func<string, bool> GetDefaultValidator(Type type)
		{
			if (type == typeof(bool))
			{
				return new Func<string, bool>(VariantType.BoolValidator);
			}
			if (type == typeof(Version))
			{
				return new Func<string, bool>(VariantType.VersionValidator);
			}
			if (type == typeof(Guid))
			{
				return new Func<string, bool>(VariantType.GuidValidator);
			}
			return new Func<string, bool>(VariantType.DefaultValidator);
		}

		// Token: 0x04000512 RID: 1298
		public static readonly VariantType Organization = VariantType.Create("org", typeof(string), VariantTypeFlags.AllowedInSettings | VariantTypeFlags.AllowedInFlights | VariantTypeFlags.AllowedInTeams);

		// Token: 0x04000513 RID: 1299
		public static readonly VariantType User = VariantType.Create("user", typeof(string), VariantTypeFlags.Public | VariantTypeFlags.AllowedInFlights | VariantTypeFlags.AllowedInTeams);

		// Token: 0x04000514 RID: 1300
		public static readonly VariantType Locale = VariantType.Create("loc", typeof(string), VariantTypeFlags.Public | VariantTypeFlags.AllowedInSettings | VariantTypeFlags.AllowedInFlights);

		// Token: 0x04000515 RID: 1301
		public static readonly VariantType Mode = VariantType.Create("mode", typeof(string), VariantTypeFlags.AllowedInSettings, new Func<string, bool>(VariantType.ModeValidator));

		// Token: 0x04000516 RID: 1302
		public static readonly VariantType Dogfood = VariantType.Create("dogfood", typeof(bool), VariantTypeFlags.AllowedInFlights);

		// Token: 0x04000517 RID: 1303
		public static readonly VariantType Region = VariantType.Create("region", typeof(string), VariantTypeFlags.AllowedInSettings | VariantTypeFlags.AllowedInFlights, new Func<string, bool>(VariantType.RegionValidator));

		// Token: 0x04000518 RID: 1304
		public static readonly VariantType Process = VariantType.Create("process", typeof(string), VariantTypeFlags.Public | VariantTypeFlags.AllowedInSettings);

		// Token: 0x04000519 RID: 1305
		public static readonly VariantType FirstRelease = VariantType.Create("FirstRelease", typeof(bool), VariantTypeFlags.AllowedInFlights);

		// Token: 0x0400051A RID: 1306
		public static readonly VariantType Primary = VariantType.Create("primary", typeof(bool), VariantTypeFlags.AllowedInSettings);

		// Token: 0x0400051B RID: 1307
		public static readonly VariantType Test = VariantType.Create("test", typeof(bool), VariantTypeFlags.AllowedInSettings | VariantTypeFlags.AllowedInFlights);

		// Token: 0x0400051C RID: 1308
		public static readonly VariantType Machine = VariantType.Create("machine", typeof(string), VariantTypeFlags.Public | VariantTypeFlags.AllowedInSettings | VariantTypeFlags.AllowedInFlights);

		// Token: 0x0400051D RID: 1309
		public static readonly VariantType Dag = VariantType.Create("dag", typeof(string), VariantTypeFlags.AllowedInSettings | VariantTypeFlags.AllowedInFlights);

		// Token: 0x0400051E RID: 1310
		public static readonly VariantType Pod = VariantType.Create("pod", typeof(string), VariantTypeFlags.AllowedInSettings | VariantTypeFlags.AllowedInFlights);

		// Token: 0x0400051F RID: 1311
		public static readonly VariantType Forest = VariantType.Create("forest", typeof(string), VariantTypeFlags.AllowedInSettings | VariantTypeFlags.AllowedInFlights);

		// Token: 0x04000520 RID: 1312
		public static readonly VariantType Service = VariantType.Create("Service", typeof(string), VariantTypeFlags.AllowedInSettings | VariantTypeFlags.AllowedInFlights, new Func<string, bool>(VariantType.ServiceValidator));

		// Token: 0x04000521 RID: 1313
		public static readonly VariantType Flight = VariantType.Create("flt", typeof(bool), VariantTypeFlags.Prefix | VariantTypeFlags.AllowedInSettings);

		// Token: 0x04000522 RID: 1314
		public static readonly VariantType Team = VariantType.Create("team", typeof(bool), VariantTypeFlags.Prefix | VariantTypeFlags.AllowedInFlights);

		// Token: 0x04000523 RID: 1315
		public static readonly VariantType Preview = VariantType.Create("Preview", typeof(bool), VariantTypeFlags.AllowedInFlights);

		// Token: 0x04000524 RID: 1316
		public static readonly VariantType MdbGuid = VariantType.Create("mdbguid", typeof(Guid), VariantTypeFlags.AllowedInFlights);

		// Token: 0x04000525 RID: 1317
		public static readonly VariantType MdbName = VariantType.Create("mdbname", typeof(string), VariantTypeFlags.AllowedInFlights);

		// Token: 0x04000526 RID: 1318
		public static readonly VariantType MdbVersion = VariantType.Create("mdbversion", typeof(Version), VariantTypeFlags.AllowedInFlights);

		// Token: 0x04000527 RID: 1319
		public static readonly VariantType AuthMethod = VariantType.Create("AuthMethod", typeof(string), VariantTypeFlags.AllowedInSettings);

		// Token: 0x04000528 RID: 1320
		public static readonly VariantType UserType = VariantType.Create("UserType", typeof(VariantConfigurationUserType), VariantTypeFlags.AllowedInSettings | VariantTypeFlags.AllowedInFlights);

		// Token: 0x04000529 RID: 1321
		public static readonly VariantTypeCollection Variants = VariantType.InitializeVariants();

		// Token: 0x0400052A RID: 1322
		public readonly string Name;

		// Token: 0x0400052B RID: 1323
		public readonly Type Type;

		// Token: 0x0400052C RID: 1324
		public readonly VariantTypeFlags Flags;

		// Token: 0x0400052D RID: 1325
		public readonly Func<string, bool> ValidateValue;

		// Token: 0x0400052E RID: 1326
		internal static readonly Regex VersionRegex = new Regex("^\\d{2}\\.\\d{2}\\.\\d{4}\\.\\d{3}$", RegexOptions.Compiled);

		// Token: 0x0400052F RID: 1327
		internal static readonly Regex GuidRegex = new Regex("^[\\da-f]{8}(\\-[\\da-f]{4}){3}\\-[\\da-f]{12}$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x04000530 RID: 1328
		private static readonly HashSet<string> ServiceVariantValues = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"Gallatin",
			"PROD",
			"ServiceDogfood"
		};

		// Token: 0x04000531 RID: 1329
		private static readonly HashSet<string> ModeVariantValues = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"dedicated",
			"datacenter",
			"enterprise"
		};
	}
}
