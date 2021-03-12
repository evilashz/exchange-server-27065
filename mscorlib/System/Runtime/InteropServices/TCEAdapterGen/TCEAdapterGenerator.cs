using System;
using System.Collections;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices.TCEAdapterGen
{
	// Token: 0x02000998 RID: 2456
	internal class TCEAdapterGenerator
	{
		// Token: 0x0600629B RID: 25243 RVA: 0x0014FAB8 File Offset: 0x0014DCB8
		public void Process(ModuleBuilder ModBldr, ArrayList EventItfList)
		{
			this.m_Module = ModBldr;
			int count = EventItfList.Count;
			for (int i = 0; i < count; i++)
			{
				EventItfInfo eventItfInfo = (EventItfInfo)EventItfList[i];
				Type eventItfType = eventItfInfo.GetEventItfType();
				Type srcItfType = eventItfInfo.GetSrcItfType();
				string eventProviderName = eventItfInfo.GetEventProviderName();
				Type sinkHelperType = new EventSinkHelperWriter(this.m_Module, srcItfType, eventItfType).Perform();
				new EventProviderWriter(this.m_Module, eventProviderName, eventItfType, srcItfType, sinkHelperType).Perform();
			}
		}

		// Token: 0x0600629C RID: 25244 RVA: 0x0014FB30 File Offset: 0x0014DD30
		internal static void SetClassInterfaceTypeToNone(TypeBuilder tb)
		{
			if (TCEAdapterGenerator.s_NoClassItfCABuilder == null)
			{
				Type[] types = new Type[]
				{
					typeof(ClassInterfaceType)
				};
				ConstructorInfo constructor = typeof(ClassInterfaceAttribute).GetConstructor(types);
				TCEAdapterGenerator.s_NoClassItfCABuilder = new CustomAttributeBuilder(constructor, new object[]
				{
					ClassInterfaceType.None
				});
			}
			tb.SetCustomAttribute(TCEAdapterGenerator.s_NoClassItfCABuilder);
		}

		// Token: 0x0600629D RID: 25245 RVA: 0x0014FB98 File Offset: 0x0014DD98
		internal static TypeBuilder DefineUniqueType(string strInitFullName, TypeAttributes attrs, Type BaseType, Type[] aInterfaceTypes, ModuleBuilder mb)
		{
			string text = strInitFullName;
			int num = 2;
			while (mb.GetType(text) != null)
			{
				text = strInitFullName + "_" + num;
				num++;
			}
			return mb.DefineType(text, attrs, BaseType, aInterfaceTypes);
		}

		// Token: 0x0600629E RID: 25246 RVA: 0x0014FBDC File Offset: 0x0014DDDC
		internal static void SetHiddenAttribute(TypeBuilder tb)
		{
			if (TCEAdapterGenerator.s_HiddenCABuilder == null)
			{
				Type[] types = new Type[]
				{
					typeof(TypeLibTypeFlags)
				};
				ConstructorInfo constructor = typeof(TypeLibTypeAttribute).GetConstructor(types);
				TCEAdapterGenerator.s_HiddenCABuilder = new CustomAttributeBuilder(constructor, new object[]
				{
					TypeLibTypeFlags.FHidden
				});
			}
			tb.SetCustomAttribute(TCEAdapterGenerator.s_HiddenCABuilder);
		}

		// Token: 0x0600629F RID: 25247 RVA: 0x0014FC44 File Offset: 0x0014DE44
		internal static MethodInfo[] GetNonPropertyMethods(Type type)
		{
			MethodInfo[] methods = type.GetMethods();
			ArrayList arrayList = new ArrayList(methods);
			PropertyInfo[] properties = type.GetProperties();
			foreach (PropertyInfo propertyInfo in properties)
			{
				MethodInfo[] accessors = propertyInfo.GetAccessors();
				foreach (MethodInfo right in accessors)
				{
					for (int k = 0; k < arrayList.Count; k++)
					{
						if ((MethodInfo)arrayList[k] == right)
						{
							arrayList.RemoveAt(k);
						}
					}
				}
			}
			MethodInfo[] array3 = new MethodInfo[arrayList.Count];
			arrayList.CopyTo(array3);
			return array3;
		}

		// Token: 0x060062A0 RID: 25248 RVA: 0x0014FCF4 File Offset: 0x0014DEF4
		internal static MethodInfo[] GetPropertyMethods(Type type)
		{
			MethodInfo[] methods = type.GetMethods();
			ArrayList arrayList = new ArrayList();
			PropertyInfo[] properties = type.GetProperties();
			foreach (PropertyInfo propertyInfo in properties)
			{
				MethodInfo[] accessors = propertyInfo.GetAccessors();
				foreach (MethodInfo value in accessors)
				{
					arrayList.Add(value);
				}
			}
			MethodInfo[] array3 = new MethodInfo[arrayList.Count];
			arrayList.CopyTo(array3);
			return array3;
		}

		// Token: 0x04002C1B RID: 11291
		private ModuleBuilder m_Module;

		// Token: 0x04002C1C RID: 11292
		private Hashtable m_SrcItfToSrcItfInfoMap = new Hashtable();

		// Token: 0x04002C1D RID: 11293
		private static volatile CustomAttributeBuilder s_NoClassItfCABuilder;

		// Token: 0x04002C1E RID: 11294
		private static volatile CustomAttributeBuilder s_HiddenCABuilder;
	}
}
