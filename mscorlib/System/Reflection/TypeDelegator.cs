using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005F8 RID: 1528
	[ComVisible(true)]
	[Serializable]
	public class TypeDelegator : TypeInfo
	{
		// Token: 0x060047A0 RID: 18336 RVA: 0x00102FDB File Offset: 0x001011DB
		public override bool IsAssignableFrom(TypeInfo typeInfo)
		{
			return !(typeInfo == null) && this.IsAssignableFrom(typeInfo.AsType());
		}

		// Token: 0x060047A1 RID: 18337 RVA: 0x00102FF4 File Offset: 0x001011F4
		protected TypeDelegator()
		{
		}

		// Token: 0x060047A2 RID: 18338 RVA: 0x00102FFC File Offset: 0x001011FC
		public TypeDelegator(Type delegatingType)
		{
			if (delegatingType == null)
			{
				throw new ArgumentNullException("delegatingType");
			}
			this.typeImpl = delegatingType;
		}

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x060047A3 RID: 18339 RVA: 0x0010301F File Offset: 0x0010121F
		public override Guid GUID
		{
			get
			{
				return this.typeImpl.GUID;
			}
		}

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x060047A4 RID: 18340 RVA: 0x0010302C File Offset: 0x0010122C
		public override int MetadataToken
		{
			get
			{
				return this.typeImpl.MetadataToken;
			}
		}

		// Token: 0x060047A5 RID: 18341 RVA: 0x0010303C File Offset: 0x0010123C
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			return this.typeImpl.InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
		}

		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x060047A6 RID: 18342 RVA: 0x00103061 File Offset: 0x00101261
		public override Module Module
		{
			get
			{
				return this.typeImpl.Module;
			}
		}

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x060047A7 RID: 18343 RVA: 0x0010306E File Offset: 0x0010126E
		public override Assembly Assembly
		{
			get
			{
				return this.typeImpl.Assembly;
			}
		}

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x060047A8 RID: 18344 RVA: 0x0010307B File Offset: 0x0010127B
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				return this.typeImpl.TypeHandle;
			}
		}

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x060047A9 RID: 18345 RVA: 0x00103088 File Offset: 0x00101288
		public override string Name
		{
			get
			{
				return this.typeImpl.Name;
			}
		}

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x060047AA RID: 18346 RVA: 0x00103095 File Offset: 0x00101295
		public override string FullName
		{
			get
			{
				return this.typeImpl.FullName;
			}
		}

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x060047AB RID: 18347 RVA: 0x001030A2 File Offset: 0x001012A2
		public override string Namespace
		{
			get
			{
				return this.typeImpl.Namespace;
			}
		}

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x060047AC RID: 18348 RVA: 0x001030AF File Offset: 0x001012AF
		public override string AssemblyQualifiedName
		{
			get
			{
				return this.typeImpl.AssemblyQualifiedName;
			}
		}

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x060047AD RID: 18349 RVA: 0x001030BC File Offset: 0x001012BC
		public override Type BaseType
		{
			get
			{
				return this.typeImpl.BaseType;
			}
		}

		// Token: 0x060047AE RID: 18350 RVA: 0x001030C9 File Offset: 0x001012C9
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			return this.typeImpl.GetConstructor(bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x060047AF RID: 18351 RVA: 0x001030DD File Offset: 0x001012DD
		[ComVisible(true)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetConstructors(bindingAttr);
		}

		// Token: 0x060047B0 RID: 18352 RVA: 0x001030EB File Offset: 0x001012EB
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			if (types == null)
			{
				return this.typeImpl.GetMethod(name, bindingAttr);
			}
			return this.typeImpl.GetMethod(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x060047B1 RID: 18353 RVA: 0x00103113 File Offset: 0x00101313
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetMethods(bindingAttr);
		}

		// Token: 0x060047B2 RID: 18354 RVA: 0x00103121 File Offset: 0x00101321
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			return this.typeImpl.GetField(name, bindingAttr);
		}

		// Token: 0x060047B3 RID: 18355 RVA: 0x00103130 File Offset: 0x00101330
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetFields(bindingAttr);
		}

		// Token: 0x060047B4 RID: 18356 RVA: 0x0010313E File Offset: 0x0010133E
		public override Type GetInterface(string name, bool ignoreCase)
		{
			return this.typeImpl.GetInterface(name, ignoreCase);
		}

		// Token: 0x060047B5 RID: 18357 RVA: 0x0010314D File Offset: 0x0010134D
		public override Type[] GetInterfaces()
		{
			return this.typeImpl.GetInterfaces();
		}

		// Token: 0x060047B6 RID: 18358 RVA: 0x0010315A File Offset: 0x0010135A
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			return this.typeImpl.GetEvent(name, bindingAttr);
		}

		// Token: 0x060047B7 RID: 18359 RVA: 0x00103169 File Offset: 0x00101369
		public override EventInfo[] GetEvents()
		{
			return this.typeImpl.GetEvents();
		}

		// Token: 0x060047B8 RID: 18360 RVA: 0x00103176 File Offset: 0x00101376
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			if (returnType == null && types == null)
			{
				return this.typeImpl.GetProperty(name, bindingAttr);
			}
			return this.typeImpl.GetProperty(name, bindingAttr, binder, returnType, types, modifiers);
		}

		// Token: 0x060047B9 RID: 18361 RVA: 0x001031A8 File Offset: 0x001013A8
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetProperties(bindingAttr);
		}

		// Token: 0x060047BA RID: 18362 RVA: 0x001031B6 File Offset: 0x001013B6
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetEvents(bindingAttr);
		}

		// Token: 0x060047BB RID: 18363 RVA: 0x001031C4 File Offset: 0x001013C4
		public override Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetNestedTypes(bindingAttr);
		}

		// Token: 0x060047BC RID: 18364 RVA: 0x001031D2 File Offset: 0x001013D2
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			return this.typeImpl.GetNestedType(name, bindingAttr);
		}

		// Token: 0x060047BD RID: 18365 RVA: 0x001031E1 File Offset: 0x001013E1
		public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			return this.typeImpl.GetMember(name, type, bindingAttr);
		}

		// Token: 0x060047BE RID: 18366 RVA: 0x001031F1 File Offset: 0x001013F1
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetMembers(bindingAttr);
		}

		// Token: 0x060047BF RID: 18367 RVA: 0x001031FF File Offset: 0x001013FF
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return this.typeImpl.Attributes;
		}

		// Token: 0x060047C0 RID: 18368 RVA: 0x0010320C File Offset: 0x0010140C
		protected override bool IsArrayImpl()
		{
			return this.typeImpl.IsArray;
		}

		// Token: 0x060047C1 RID: 18369 RVA: 0x00103219 File Offset: 0x00101419
		protected override bool IsPrimitiveImpl()
		{
			return this.typeImpl.IsPrimitive;
		}

		// Token: 0x060047C2 RID: 18370 RVA: 0x00103226 File Offset: 0x00101426
		protected override bool IsByRefImpl()
		{
			return this.typeImpl.IsByRef;
		}

		// Token: 0x060047C3 RID: 18371 RVA: 0x00103233 File Offset: 0x00101433
		protected override bool IsPointerImpl()
		{
			return this.typeImpl.IsPointer;
		}

		// Token: 0x060047C4 RID: 18372 RVA: 0x00103240 File Offset: 0x00101440
		protected override bool IsValueTypeImpl()
		{
			return this.typeImpl.IsValueType;
		}

		// Token: 0x060047C5 RID: 18373 RVA: 0x0010324D File Offset: 0x0010144D
		protected override bool IsCOMObjectImpl()
		{
			return this.typeImpl.IsCOMObject;
		}

		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x060047C6 RID: 18374 RVA: 0x0010325A File Offset: 0x0010145A
		public override bool IsConstructedGenericType
		{
			get
			{
				return this.typeImpl.IsConstructedGenericType;
			}
		}

		// Token: 0x060047C7 RID: 18375 RVA: 0x00103267 File Offset: 0x00101467
		public override Type GetElementType()
		{
			return this.typeImpl.GetElementType();
		}

		// Token: 0x060047C8 RID: 18376 RVA: 0x00103274 File Offset: 0x00101474
		protected override bool HasElementTypeImpl()
		{
			return this.typeImpl.HasElementType;
		}

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x060047C9 RID: 18377 RVA: 0x00103281 File Offset: 0x00101481
		public override Type UnderlyingSystemType
		{
			get
			{
				return this.typeImpl.UnderlyingSystemType;
			}
		}

		// Token: 0x060047CA RID: 18378 RVA: 0x0010328E File Offset: 0x0010148E
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.typeImpl.GetCustomAttributes(inherit);
		}

		// Token: 0x060047CB RID: 18379 RVA: 0x0010329C File Offset: 0x0010149C
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.typeImpl.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x060047CC RID: 18380 RVA: 0x001032AB File Offset: 0x001014AB
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.typeImpl.IsDefined(attributeType, inherit);
		}

		// Token: 0x060047CD RID: 18381 RVA: 0x001032BA File Offset: 0x001014BA
		[ComVisible(true)]
		public override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			return this.typeImpl.GetInterfaceMap(interfaceType);
		}

		// Token: 0x04001D86 RID: 7558
		protected Type typeImpl;
	}
}
