using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000005 RID: 5
	public abstract class Argument
	{
		// Token: 0x06000021 RID: 33 RVA: 0x0000252F File Offset: 0x0000072F
		public Argument(Type type)
		{
			this.type = type;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000022 RID: 34 RVA: 0x0000253E File Offset: 0x0000073E
		public Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002546 File Offset: 0x00000746
		public bool IsNumerical
		{
			get
			{
				return this.type == typeof(int) || this.type == typeof(ulong);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002576 File Offset: 0x00000776
		public bool IsString
		{
			get
			{
				return Argument.IsStringType(this.type);
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002584 File Offset: 0x00000784
		public static Type GetTypeForName(string typeName)
		{
			Type result;
			if (Argument.knownTypes.TryGetValue(typeName, out result))
			{
				return result;
			}
			throw new RulesValidationException(RulesStrings.InvalidArgumentType(typeName));
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000025B0 File Offset: 0x000007B0
		public static string GetTypeName(Type type)
		{
			if (type.Equals(typeof(string)))
			{
				return "string";
			}
			if (type.Equals(typeof(int)))
			{
				return "integer";
			}
			if (type.Equals(typeof(ulong)))
			{
				return "ulong";
			}
			if (type.Equals(typeof(string[])))
			{
				return "string[]";
			}
			if (type.Equals(typeof(List<string>)))
			{
				return "stringlist";
			}
			if (type.Equals(typeof(ShortList<string>)))
			{
				return "stringshortlist";
			}
			if (type.Equals(typeof(ShortList<ShortList<KeyValuePair<string, string>>>)))
			{
				return "keyValue[][]";
			}
			throw new RulesValidationException(RulesStrings.InvalidArgumentType(type.Name));
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002678 File Offset: 0x00000878
		public static bool IsStringType(Type type)
		{
			return type == typeof(string) || type == typeof(string[]) || type == typeof(List<string>) || type == typeof(ShortList<string>);
		}

		// Token: 0x06000028 RID: 40
		public abstract object GetValue(RulesEvaluationContext context);

		// Token: 0x06000029 RID: 41 RVA: 0x000026D0 File Offset: 0x000008D0
		private static Dictionary<string, Type> InitializeKnownTypes()
		{
			return new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase)
			{
				{
					"string",
					typeof(string)
				},
				{
					"integer",
					typeof(int)
				},
				{
					"string[]",
					typeof(string[])
				},
				{
					"stringlist",
					typeof(List<string>)
				},
				{
					"stringshortlist",
					typeof(ShortList<string>)
				},
				{
					"ulong",
					typeof(ulong)
				},
				{
					"keyValue[][]",
					typeof(ShortList<ShortList<KeyValuePair<string, string>>>)
				}
			};
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000277C File Offset: 0x0000097C
		public virtual int GetEstimatedSize()
		{
			return 36;
		}

		// Token: 0x04000009 RID: 9
		private static Dictionary<string, Type> knownTypes = Argument.InitializeKnownTypes();

		// Token: 0x0400000A RID: 10
		private Type type;
	}
}
