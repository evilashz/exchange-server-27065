using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection.Emit
{
	// Token: 0x02000602 RID: 1538
	internal class TypeNameBuilder
	{
		// Token: 0x0600486E RID: 18542
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern IntPtr CreateTypeNameBuilder();

		// Token: 0x0600486F RID: 18543
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void ReleaseTypeNameBuilder(IntPtr pAQN);

		// Token: 0x06004870 RID: 18544
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void OpenGenericArguments(IntPtr tnb);

		// Token: 0x06004871 RID: 18545
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void CloseGenericArguments(IntPtr tnb);

		// Token: 0x06004872 RID: 18546
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void OpenGenericArgument(IntPtr tnb);

		// Token: 0x06004873 RID: 18547
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void CloseGenericArgument(IntPtr tnb);

		// Token: 0x06004874 RID: 18548
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void AddName(IntPtr tnb, string name);

		// Token: 0x06004875 RID: 18549
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void AddPointer(IntPtr tnb);

		// Token: 0x06004876 RID: 18550
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void AddByRef(IntPtr tnb);

		// Token: 0x06004877 RID: 18551
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void AddSzArray(IntPtr tnb);

		// Token: 0x06004878 RID: 18552
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void AddArray(IntPtr tnb, int rank);

		// Token: 0x06004879 RID: 18553
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void AddAssemblySpec(IntPtr tnb, string assemblySpec);

		// Token: 0x0600487A RID: 18554
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void ToString(IntPtr tnb, StringHandleOnStack retString);

		// Token: 0x0600487B RID: 18555
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void Clear(IntPtr tnb);

		// Token: 0x0600487C RID: 18556 RVA: 0x00105F14 File Offset: 0x00104114
		[SecuritySafeCritical]
		internal static string ToString(Type type, TypeNameBuilder.Format format)
		{
			if ((format == TypeNameBuilder.Format.FullName || format == TypeNameBuilder.Format.AssemblyQualifiedName) && !type.IsGenericTypeDefinition && type.ContainsGenericParameters)
			{
				return null;
			}
			TypeNameBuilder typeNameBuilder = new TypeNameBuilder(TypeNameBuilder.CreateTypeNameBuilder());
			typeNameBuilder.Clear();
			typeNameBuilder.ConstructAssemblyQualifiedNameWorker(type, format);
			string result = typeNameBuilder.ToString();
			typeNameBuilder.Dispose();
			return result;
		}

		// Token: 0x0600487D RID: 18557 RVA: 0x00105F62 File Offset: 0x00104162
		private TypeNameBuilder(IntPtr typeNameBuilder)
		{
			this.m_typeNameBuilder = typeNameBuilder;
		}

		// Token: 0x0600487E RID: 18558 RVA: 0x00105F71 File Offset: 0x00104171
		[SecurityCritical]
		internal void Dispose()
		{
			TypeNameBuilder.ReleaseTypeNameBuilder(this.m_typeNameBuilder);
		}

		// Token: 0x0600487F RID: 18559 RVA: 0x00105F80 File Offset: 0x00104180
		[SecurityCritical]
		private void AddElementType(Type elementType)
		{
			if (elementType.HasElementType)
			{
				this.AddElementType(elementType.GetElementType());
			}
			if (elementType.IsPointer)
			{
				this.AddPointer();
				return;
			}
			if (elementType.IsByRef)
			{
				this.AddByRef();
				return;
			}
			if (elementType.IsSzArray)
			{
				this.AddSzArray();
				return;
			}
			if (elementType.IsArray)
			{
				this.AddArray(elementType.GetArrayRank());
			}
		}

		// Token: 0x06004880 RID: 18560 RVA: 0x00105FE4 File Offset: 0x001041E4
		[SecurityCritical]
		private void ConstructAssemblyQualifiedNameWorker(Type type, TypeNameBuilder.Format format)
		{
			Type type2 = type;
			while (type2.HasElementType)
			{
				type2 = type2.GetElementType();
			}
			List<Type> list = new List<Type>();
			Type type3 = type2;
			while (type3 != null)
			{
				list.Add(type3);
				type3 = (type3.IsGenericParameter ? null : type3.DeclaringType);
			}
			for (int i = list.Count - 1; i >= 0; i--)
			{
				Type type4 = list[i];
				string text = type4.Name;
				if (i == list.Count - 1 && type4.Namespace != null && type4.Namespace.Length != 0)
				{
					text = type4.Namespace + "." + text;
				}
				this.AddName(text);
			}
			if (type2.IsGenericType && (!type2.IsGenericTypeDefinition || format == TypeNameBuilder.Format.ToString))
			{
				Type[] genericArguments = type2.GetGenericArguments();
				this.OpenGenericArguments();
				for (int j = 0; j < genericArguments.Length; j++)
				{
					TypeNameBuilder.Format format2 = (format == TypeNameBuilder.Format.FullName) ? TypeNameBuilder.Format.AssemblyQualifiedName : format;
					this.OpenGenericArgument();
					this.ConstructAssemblyQualifiedNameWorker(genericArguments[j], format2);
					this.CloseGenericArgument();
				}
				this.CloseGenericArguments();
			}
			this.AddElementType(type);
			if (format == TypeNameBuilder.Format.AssemblyQualifiedName)
			{
				this.AddAssemblySpec(type.Module.Assembly.FullName);
			}
		}

		// Token: 0x06004881 RID: 18561 RVA: 0x00106112 File Offset: 0x00104312
		[SecurityCritical]
		private void OpenGenericArguments()
		{
			TypeNameBuilder.OpenGenericArguments(this.m_typeNameBuilder);
		}

		// Token: 0x06004882 RID: 18562 RVA: 0x0010611F File Offset: 0x0010431F
		[SecurityCritical]
		private void CloseGenericArguments()
		{
			TypeNameBuilder.CloseGenericArguments(this.m_typeNameBuilder);
		}

		// Token: 0x06004883 RID: 18563 RVA: 0x0010612C File Offset: 0x0010432C
		[SecurityCritical]
		private void OpenGenericArgument()
		{
			TypeNameBuilder.OpenGenericArgument(this.m_typeNameBuilder);
		}

		// Token: 0x06004884 RID: 18564 RVA: 0x00106139 File Offset: 0x00104339
		[SecurityCritical]
		private void CloseGenericArgument()
		{
			TypeNameBuilder.CloseGenericArgument(this.m_typeNameBuilder);
		}

		// Token: 0x06004885 RID: 18565 RVA: 0x00106146 File Offset: 0x00104346
		[SecurityCritical]
		private void AddName(string name)
		{
			TypeNameBuilder.AddName(this.m_typeNameBuilder, name);
		}

		// Token: 0x06004886 RID: 18566 RVA: 0x00106154 File Offset: 0x00104354
		[SecurityCritical]
		private void AddPointer()
		{
			TypeNameBuilder.AddPointer(this.m_typeNameBuilder);
		}

		// Token: 0x06004887 RID: 18567 RVA: 0x00106161 File Offset: 0x00104361
		[SecurityCritical]
		private void AddByRef()
		{
			TypeNameBuilder.AddByRef(this.m_typeNameBuilder);
		}

		// Token: 0x06004888 RID: 18568 RVA: 0x0010616E File Offset: 0x0010436E
		[SecurityCritical]
		private void AddSzArray()
		{
			TypeNameBuilder.AddSzArray(this.m_typeNameBuilder);
		}

		// Token: 0x06004889 RID: 18569 RVA: 0x0010617B File Offset: 0x0010437B
		[SecurityCritical]
		private void AddArray(int rank)
		{
			TypeNameBuilder.AddArray(this.m_typeNameBuilder, rank);
		}

		// Token: 0x0600488A RID: 18570 RVA: 0x00106189 File Offset: 0x00104389
		[SecurityCritical]
		private void AddAssemblySpec(string assemblySpec)
		{
			TypeNameBuilder.AddAssemblySpec(this.m_typeNameBuilder, assemblySpec);
		}

		// Token: 0x0600488B RID: 18571 RVA: 0x00106198 File Offset: 0x00104398
		[SecuritySafeCritical]
		public override string ToString()
		{
			string result = null;
			TypeNameBuilder.ToString(this.m_typeNameBuilder, JitHelpers.GetStringHandleOnStack(ref result));
			return result;
		}

		// Token: 0x0600488C RID: 18572 RVA: 0x001061BA File Offset: 0x001043BA
		[SecurityCritical]
		private void Clear()
		{
			TypeNameBuilder.Clear(this.m_typeNameBuilder);
		}

		// Token: 0x04001DC7 RID: 7623
		private IntPtr m_typeNameBuilder;

		// Token: 0x02000C09 RID: 3081
		internal enum Format
		{
			// Token: 0x04003664 RID: 13924
			ToString,
			// Token: 0x04003665 RID: 13925
			FullName,
			// Token: 0x04003666 RID: 13926
			AssemblyQualifiedName
		}
	}
}
