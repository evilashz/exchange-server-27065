using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000AE RID: 174
	public abstract class Argument
	{
		// Token: 0x06000472 RID: 1138 RVA: 0x0000DC4C File Offset: 0x0000BE4C
		static Argument()
		{
			Argument.supportedComparableTypes.AddRange(Argument.supportedNumericalTypes);
			Argument.supportedEquatableTypes = new List<Type>
			{
				typeof(string),
				typeof(Guid),
				typeof(bool),
				typeof(Enum)
			};
			Argument.supportedEquatableTypes.AddRange(Argument.supportedComparableTypes);
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0000DD1B File Offset: 0x0000BF1B
		public Argument(Type type)
		{
			this.type = type;
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x0000DD2A File Offset: 0x0000BF2A
		public Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x0000DD32 File Offset: 0x0000BF32
		public bool IsNumericalType
		{
			get
			{
				return Argument.supportedNumericalTypes.Contains(this.type);
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x0000DD44 File Offset: 0x0000BF44
		public bool IsComparableType
		{
			get
			{
				return Argument.supportedComparableTypes.Contains(this.type);
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x0000DD56 File Offset: 0x0000BF56
		public bool IsEquatableType
		{
			get
			{
				return Argument.IsTypeEquatable(this.type);
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x0000DD63 File Offset: 0x0000BF63
		public bool IsEquatableCollectionType
		{
			get
			{
				return Argument.IsTypeEquatableCollection(this.type);
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x0000DD70 File Offset: 0x0000BF70
		public bool IsEnumType
		{
			get
			{
				return this.type.BaseType == typeof(Enum);
			}
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0000DD8C File Offset: 0x0000BF8C
		public bool IsCollectionOfType(Type elementType)
		{
			return Argument.IsTypeCollectionOfType(this.type, elementType);
		}

		// Token: 0x0600047B RID: 1147
		public abstract object GetValue(PolicyEvaluationContext context);

		// Token: 0x0600047C RID: 1148 RVA: 0x0000DD9A File Offset: 0x0000BF9A
		public bool IsComparableTo(Argument argument)
		{
			return this.IsComparableType && this.type == argument.type;
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0000DDB8 File Offset: 0x0000BFB8
		public bool IsEquatableTo(Argument argument)
		{
			bool result = false;
			if (this.IsEquatableType)
			{
				if (argument.IsEquatableType)
				{
					result = (this.Type == argument.Type);
				}
				else
				{
					result = argument.IsCollectionOfType(this.Type);
				}
			}
			else if (argument.IsEquatableType)
			{
				result = this.IsCollectionOfType(argument.Type);
			}
			else
			{
				foreach (Type elementType in Argument.supportedEquatableTypes)
				{
					if (this.IsCollectionOfType(elementType) && argument.IsCollectionOfType(elementType))
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0000DE68 File Offset: 0x0000C068
		internal static bool IsTypeEquatable(Type type)
		{
			return Argument.supportedEquatableTypes.Contains(type) || type.BaseType == typeof(Enum);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0000DE90 File Offset: 0x0000C090
		internal static bool IsTypeEquatableCollection(Type type)
		{
			foreach (Type elementType in Argument.supportedEquatableTypes)
			{
				if (Argument.IsTypeCollectionOfType(type, elementType))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0000DEEC File Offset: 0x0000C0EC
		internal static bool IsTypeCollectionOfType(Type type, Type elementType)
		{
			Type @interface = type.GetInterface(typeof(IEnumerable<>).Name);
			if (@interface != null)
			{
				Type[] genericArguments = @interface.GetGenericArguments();
				return genericArguments.Count<Type>() == 1 && (genericArguments.First<Type>() == elementType || genericArguments.First<Type>().BaseType == elementType);
			}
			return false;
		}

		// Token: 0x040002C7 RID: 711
		private static List<Type> supportedNumericalTypes = new List<Type>
		{
			typeof(int),
			typeof(long),
			typeof(ulong)
		};

		// Token: 0x040002C8 RID: 712
		private static List<Type> supportedComparableTypes = new List<Type>
		{
			typeof(DateTime)
		};

		// Token: 0x040002C9 RID: 713
		private static List<Type> supportedEquatableTypes;

		// Token: 0x040002CA RID: 714
		private Type type;
	}
}
