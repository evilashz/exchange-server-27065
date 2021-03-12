using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x0200061E RID: 1566
	internal sealed class SymbolType : TypeInfo
	{
		// Token: 0x06004A7E RID: 19070 RVA: 0x0010D12D File Offset: 0x0010B32D
		public override bool IsAssignableFrom(TypeInfo typeInfo)
		{
			return !(typeInfo == null) && this.IsAssignableFrom(typeInfo.AsType());
		}

		// Token: 0x06004A7F RID: 19071 RVA: 0x0010D148 File Offset: 0x0010B348
		internal static Type FormCompoundType(char[] bFormat, Type baseType, int curIndex)
		{
			if (bFormat == null || curIndex == bFormat.Length)
			{
				return baseType;
			}
			if (bFormat[curIndex] == '&')
			{
				SymbolType symbolType = new SymbolType(TypeKind.IsByRef);
				symbolType.SetFormat(bFormat, curIndex, 1);
				curIndex++;
				if (curIndex != bFormat.Length)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_BadSigFormat"));
				}
				symbolType.SetElementType(baseType);
				return symbolType;
			}
			else
			{
				if (bFormat[curIndex] == '[')
				{
					SymbolType symbolType = new SymbolType(TypeKind.IsArray);
					int num = curIndex;
					curIndex++;
					int num2 = 0;
					int num3 = -1;
					while (bFormat[curIndex] != ']')
					{
						if (bFormat[curIndex] == '*')
						{
							symbolType.m_isSzArray = false;
							curIndex++;
						}
						if ((bFormat[curIndex] >= '0' && bFormat[curIndex] <= '9') || bFormat[curIndex] == '-')
						{
							bool flag = false;
							if (bFormat[curIndex] == '-')
							{
								flag = true;
								curIndex++;
							}
							while (bFormat[curIndex] >= '0' && bFormat[curIndex] <= '9')
							{
								num2 *= 10;
								num2 += (int)(bFormat[curIndex] - '0');
								curIndex++;
							}
							if (flag)
							{
								num2 = 0 - num2;
							}
							num3 = num2 - 1;
						}
						if (bFormat[curIndex] == '.')
						{
							curIndex++;
							if (bFormat[curIndex] != '.')
							{
								throw new ArgumentException(Environment.GetResourceString("Argument_BadSigFormat"));
							}
							curIndex++;
							if ((bFormat[curIndex] >= '0' && bFormat[curIndex] <= '9') || bFormat[curIndex] == '-')
							{
								bool flag2 = false;
								num3 = 0;
								if (bFormat[curIndex] == '-')
								{
									flag2 = true;
									curIndex++;
								}
								while (bFormat[curIndex] >= '0' && bFormat[curIndex] <= '9')
								{
									num3 *= 10;
									num3 += (int)(bFormat[curIndex] - '0');
									curIndex++;
								}
								if (flag2)
								{
									num3 = 0 - num3;
								}
								if (num3 < num2)
								{
									throw new ArgumentException(Environment.GetResourceString("Argument_BadSigFormat"));
								}
							}
						}
						if (bFormat[curIndex] == ',')
						{
							curIndex++;
							symbolType.SetBounds(num2, num3);
							num2 = 0;
							num3 = -1;
						}
						else if (bFormat[curIndex] != ']')
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_BadSigFormat"));
						}
					}
					symbolType.SetBounds(num2, num3);
					curIndex++;
					symbolType.SetFormat(bFormat, num, curIndex - num);
					symbolType.SetElementType(baseType);
					return SymbolType.FormCompoundType(bFormat, symbolType, curIndex);
				}
				if (bFormat[curIndex] == '*')
				{
					SymbolType symbolType = new SymbolType(TypeKind.IsPointer);
					symbolType.SetFormat(bFormat, curIndex, 1);
					curIndex++;
					symbolType.SetElementType(baseType);
					return SymbolType.FormCompoundType(bFormat, symbolType, curIndex);
				}
				return null;
			}
		}

		// Token: 0x06004A80 RID: 19072 RVA: 0x0010D348 File Offset: 0x0010B548
		internal SymbolType(TypeKind typeKind)
		{
			this.m_typeKind = typeKind;
			this.m_iaLowerBound = new int[4];
			this.m_iaUpperBound = new int[4];
		}

		// Token: 0x06004A81 RID: 19073 RVA: 0x0010D376 File Offset: 0x0010B576
		internal void SetElementType(Type baseType)
		{
			if (baseType == null)
			{
				throw new ArgumentNullException("baseType");
			}
			this.m_baseType = baseType;
		}

		// Token: 0x06004A82 RID: 19074 RVA: 0x0010D394 File Offset: 0x0010B594
		private void SetBounds(int lower, int upper)
		{
			if (lower != 0 || upper != -1)
			{
				this.m_isSzArray = false;
			}
			if (this.m_iaLowerBound.Length <= this.m_cRank)
			{
				int[] array = new int[this.m_cRank * 2];
				Array.Copy(this.m_iaLowerBound, array, this.m_cRank);
				this.m_iaLowerBound = array;
				Array.Copy(this.m_iaUpperBound, array, this.m_cRank);
				this.m_iaUpperBound = array;
			}
			this.m_iaLowerBound[this.m_cRank] = lower;
			this.m_iaUpperBound[this.m_cRank] = upper;
			this.m_cRank++;
		}

		// Token: 0x06004A83 RID: 19075 RVA: 0x0010D42C File Offset: 0x0010B62C
		internal void SetFormat(char[] bFormat, int curIndex, int length)
		{
			char[] array = new char[length];
			Array.Copy(bFormat, curIndex, array, 0, length);
			this.m_bFormat = array;
		}

		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x06004A84 RID: 19076 RVA: 0x0010D451 File Offset: 0x0010B651
		internal override bool IsSzArray
		{
			get
			{
				return this.m_cRank <= 1 && this.m_isSzArray;
			}
		}

		// Token: 0x06004A85 RID: 19077 RVA: 0x0010D464 File Offset: 0x0010B664
		public override Type MakePointerType()
		{
			return SymbolType.FormCompoundType((new string(this.m_bFormat) + "*").ToCharArray(), this.m_baseType, 0);
		}

		// Token: 0x06004A86 RID: 19078 RVA: 0x0010D48C File Offset: 0x0010B68C
		public override Type MakeByRefType()
		{
			return SymbolType.FormCompoundType((new string(this.m_bFormat) + "&").ToCharArray(), this.m_baseType, 0);
		}

		// Token: 0x06004A87 RID: 19079 RVA: 0x0010D4B4 File Offset: 0x0010B6B4
		public override Type MakeArrayType()
		{
			return SymbolType.FormCompoundType((new string(this.m_bFormat) + "[]").ToCharArray(), this.m_baseType, 0);
		}

		// Token: 0x06004A88 RID: 19080 RVA: 0x0010D4DC File Offset: 0x0010B6DC
		public override Type MakeArrayType(int rank)
		{
			if (rank <= 0)
			{
				throw new IndexOutOfRangeException();
			}
			string text = "";
			if (rank == 1)
			{
				text = "*";
			}
			else
			{
				for (int i = 1; i < rank; i++)
				{
					text += ",";
				}
			}
			string str = string.Format(CultureInfo.InvariantCulture, "[{0}]", text);
			return SymbolType.FormCompoundType((new string(this.m_bFormat) + str).ToCharArray(), this.m_baseType, 0) as SymbolType;
		}

		// Token: 0x06004A89 RID: 19081 RVA: 0x0010D557 File Offset: 0x0010B757
		public override int GetArrayRank()
		{
			if (!base.IsArray)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_SubclassOverride"));
			}
			return this.m_cRank;
		}

		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x06004A8A RID: 19082 RVA: 0x0010D577 File Offset: 0x0010B777
		public override Guid GUID
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
			}
		}

		// Token: 0x06004A8B RID: 19083 RVA: 0x0010D588 File Offset: 0x0010B788
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
		}

		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x06004A8C RID: 19084 RVA: 0x0010D59C File Offset: 0x0010B79C
		public override Module Module
		{
			get
			{
				Type baseType = this.m_baseType;
				while (baseType is SymbolType)
				{
					baseType = ((SymbolType)baseType).m_baseType;
				}
				return baseType.Module;
			}
		}

		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x06004A8D RID: 19085 RVA: 0x0010D5CC File Offset: 0x0010B7CC
		public override Assembly Assembly
		{
			get
			{
				Type baseType = this.m_baseType;
				while (baseType is SymbolType)
				{
					baseType = ((SymbolType)baseType).m_baseType;
				}
				return baseType.Assembly;
			}
		}

		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x06004A8E RID: 19086 RVA: 0x0010D5FC File Offset: 0x0010B7FC
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
			}
		}

		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x06004A8F RID: 19087 RVA: 0x0010D610 File Offset: 0x0010B810
		public override string Name
		{
			get
			{
				string str = new string(this.m_bFormat);
				Type baseType = this.m_baseType;
				while (baseType is SymbolType)
				{
					str = new string(((SymbolType)baseType).m_bFormat) + str;
					baseType = ((SymbolType)baseType).m_baseType;
				}
				return baseType.Name + str;
			}
		}

		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x06004A90 RID: 19088 RVA: 0x0010D669 File Offset: 0x0010B869
		public override string FullName
		{
			get
			{
				return TypeNameBuilder.ToString(this, TypeNameBuilder.Format.FullName);
			}
		}

		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x06004A91 RID: 19089 RVA: 0x0010D672 File Offset: 0x0010B872
		public override string AssemblyQualifiedName
		{
			get
			{
				return TypeNameBuilder.ToString(this, TypeNameBuilder.Format.AssemblyQualifiedName);
			}
		}

		// Token: 0x06004A92 RID: 19090 RVA: 0x0010D67B File Offset: 0x0010B87B
		public override string ToString()
		{
			return TypeNameBuilder.ToString(this, TypeNameBuilder.Format.ToString);
		}

		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x06004A93 RID: 19091 RVA: 0x0010D684 File Offset: 0x0010B884
		public override string Namespace
		{
			get
			{
				return this.m_baseType.Namespace;
			}
		}

		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x06004A94 RID: 19092 RVA: 0x0010D691 File Offset: 0x0010B891
		public override Type BaseType
		{
			get
			{
				return typeof(Array);
			}
		}

		// Token: 0x06004A95 RID: 19093 RVA: 0x0010D69D File Offset: 0x0010B89D
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
		}

		// Token: 0x06004A96 RID: 19094 RVA: 0x0010D6AE File Offset: 0x0010B8AE
		[ComVisible(true)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
		}

		// Token: 0x06004A97 RID: 19095 RVA: 0x0010D6BF File Offset: 0x0010B8BF
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
		}

		// Token: 0x06004A98 RID: 19096 RVA: 0x0010D6D0 File Offset: 0x0010B8D0
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
		}

		// Token: 0x06004A99 RID: 19097 RVA: 0x0010D6E1 File Offset: 0x0010B8E1
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
		}

		// Token: 0x06004A9A RID: 19098 RVA: 0x0010D6F2 File Offset: 0x0010B8F2
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
		}

		// Token: 0x06004A9B RID: 19099 RVA: 0x0010D703 File Offset: 0x0010B903
		public override Type GetInterface(string name, bool ignoreCase)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
		}

		// Token: 0x06004A9C RID: 19100 RVA: 0x0010D714 File Offset: 0x0010B914
		public override Type[] GetInterfaces()
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
		}

		// Token: 0x06004A9D RID: 19101 RVA: 0x0010D725 File Offset: 0x0010B925
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
		}

		// Token: 0x06004A9E RID: 19102 RVA: 0x0010D736 File Offset: 0x0010B936
		public override EventInfo[] GetEvents()
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
		}

		// Token: 0x06004A9F RID: 19103 RVA: 0x0010D747 File Offset: 0x0010B947
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
		}

		// Token: 0x06004AA0 RID: 19104 RVA: 0x0010D758 File Offset: 0x0010B958
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
		}

		// Token: 0x06004AA1 RID: 19105 RVA: 0x0010D769 File Offset: 0x0010B969
		public override Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
		}

		// Token: 0x06004AA2 RID: 19106 RVA: 0x0010D77A File Offset: 0x0010B97A
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
		}

		// Token: 0x06004AA3 RID: 19107 RVA: 0x0010D78B File Offset: 0x0010B98B
		public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
		}

		// Token: 0x06004AA4 RID: 19108 RVA: 0x0010D79C File Offset: 0x0010B99C
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
		}

		// Token: 0x06004AA5 RID: 19109 RVA: 0x0010D7AD File Offset: 0x0010B9AD
		[ComVisible(true)]
		public override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
		}

		// Token: 0x06004AA6 RID: 19110 RVA: 0x0010D7BE File Offset: 0x0010B9BE
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
		}

		// Token: 0x06004AA7 RID: 19111 RVA: 0x0010D7D0 File Offset: 0x0010B9D0
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			Type baseType = this.m_baseType;
			while (baseType is SymbolType)
			{
				baseType = ((SymbolType)baseType).m_baseType;
			}
			return baseType.Attributes;
		}

		// Token: 0x06004AA8 RID: 19112 RVA: 0x0010D800 File Offset: 0x0010BA00
		protected override bool IsArrayImpl()
		{
			return this.m_typeKind == TypeKind.IsArray;
		}

		// Token: 0x06004AA9 RID: 19113 RVA: 0x0010D80B File Offset: 0x0010BA0B
		protected override bool IsPointerImpl()
		{
			return this.m_typeKind == TypeKind.IsPointer;
		}

		// Token: 0x06004AAA RID: 19114 RVA: 0x0010D816 File Offset: 0x0010BA16
		protected override bool IsByRefImpl()
		{
			return this.m_typeKind == TypeKind.IsByRef;
		}

		// Token: 0x06004AAB RID: 19115 RVA: 0x0010D821 File Offset: 0x0010BA21
		protected override bool IsPrimitiveImpl()
		{
			return false;
		}

		// Token: 0x06004AAC RID: 19116 RVA: 0x0010D824 File Offset: 0x0010BA24
		protected override bool IsValueTypeImpl()
		{
			return false;
		}

		// Token: 0x06004AAD RID: 19117 RVA: 0x0010D827 File Offset: 0x0010BA27
		protected override bool IsCOMObjectImpl()
		{
			return false;
		}

		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x06004AAE RID: 19118 RVA: 0x0010D82A File Offset: 0x0010BA2A
		public override bool IsConstructedGenericType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004AAF RID: 19119 RVA: 0x0010D82D File Offset: 0x0010BA2D
		public override Type GetElementType()
		{
			return this.m_baseType;
		}

		// Token: 0x06004AB0 RID: 19120 RVA: 0x0010D835 File Offset: 0x0010BA35
		protected override bool HasElementTypeImpl()
		{
			return this.m_baseType != null;
		}

		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x06004AB1 RID: 19121 RVA: 0x0010D843 File Offset: 0x0010BA43
		public override Type UnderlyingSystemType
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06004AB2 RID: 19122 RVA: 0x0010D846 File Offset: 0x0010BA46
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
		}

		// Token: 0x06004AB3 RID: 19123 RVA: 0x0010D857 File Offset: 0x0010BA57
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
		}

		// Token: 0x06004AB4 RID: 19124 RVA: 0x0010D868 File Offset: 0x0010BA68
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
		}

		// Token: 0x04001E84 RID: 7812
		internal TypeKind m_typeKind;

		// Token: 0x04001E85 RID: 7813
		internal Type m_baseType;

		// Token: 0x04001E86 RID: 7814
		internal int m_cRank;

		// Token: 0x04001E87 RID: 7815
		internal int[] m_iaLowerBound;

		// Token: 0x04001E88 RID: 7816
		internal int[] m_iaUpperBound;

		// Token: 0x04001E89 RID: 7817
		private char[] m_bFormat;

		// Token: 0x04001E8A RID: 7818
		private bool m_isSzArray = true;
	}
}
