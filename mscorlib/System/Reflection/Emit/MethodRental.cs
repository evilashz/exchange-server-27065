using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Reflection.Emit
{
	// Token: 0x02000621 RID: 1569
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_MethodRental))]
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class MethodRental : _MethodRental
	{
		// Token: 0x06004ADA RID: 19162 RVA: 0x0010E5DC File Offset: 0x0010C7DC
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void SwapMethodBody(Type cls, int methodtoken, IntPtr rgIL, int methodSize, int flags)
		{
			if (methodSize <= 0 || methodSize >= 4128768)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_BadSizeForData"), "methodSize");
			}
			if (cls == null)
			{
				throw new ArgumentNullException("cls");
			}
			Module module = cls.Module;
			ModuleBuilder moduleBuilder = module as ModuleBuilder;
			InternalModuleBuilder internalModuleBuilder;
			if (moduleBuilder != null)
			{
				internalModuleBuilder = moduleBuilder.InternalModule;
			}
			else
			{
				internalModuleBuilder = (module as InternalModuleBuilder);
			}
			if (internalModuleBuilder == null)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_NotDynamicModule"));
			}
			RuntimeType runtimeType;
			if (cls is TypeBuilder)
			{
				TypeBuilder typeBuilder = (TypeBuilder)cls;
				if (!typeBuilder.IsCreated())
				{
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_NotAllTypesAreBaked", new object[]
					{
						typeBuilder.Name
					}));
				}
				runtimeType = typeBuilder.BakedRuntimeType;
			}
			else
			{
				runtimeType = (cls as RuntimeType);
			}
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), "cls");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			RuntimeAssembly runtimeAssembly = internalModuleBuilder.GetRuntimeAssembly();
			object syncRoot = runtimeAssembly.SyncRoot;
			lock (syncRoot)
			{
				MethodRental.SwapMethodBody(runtimeType.GetTypeHandleInternal(), methodtoken, rgIL, methodSize, flags, JitHelpers.GetStackCrawlMarkHandle(ref stackCrawlMark));
			}
		}

		// Token: 0x06004ADB RID: 19163
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void SwapMethodBody(RuntimeTypeHandle cls, int methodtoken, IntPtr rgIL, int methodSize, int flags, StackCrawlMarkHandle stackMark);

		// Token: 0x06004ADC RID: 19164 RVA: 0x0010E71C File Offset: 0x0010C91C
		private MethodRental()
		{
		}

		// Token: 0x06004ADD RID: 19165 RVA: 0x0010E724 File Offset: 0x0010C924
		void _MethodRental.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004ADE RID: 19166 RVA: 0x0010E72B File Offset: 0x0010C92B
		void _MethodRental.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004ADF RID: 19167 RVA: 0x0010E732 File Offset: 0x0010C932
		void _MethodRental.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004AE0 RID: 19168 RVA: 0x0010E739 File Offset: 0x0010C939
		void _MethodRental.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001E96 RID: 7830
		public const int JitOnDemand = 0;

		// Token: 0x04001E97 RID: 7831
		public const int JitImmediate = 1;
	}
}
