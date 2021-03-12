using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace System.Runtime.DesignerServices
{
	// Token: 0x020006EE RID: 1774
	public sealed class WindowsRuntimeDesignerContext
	{
		// Token: 0x06005019 RID: 20505
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern IntPtr CreateDesignerContext([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 1)] string[] paths, int count, bool shared);

		// Token: 0x0600501A RID: 20506 RVA: 0x00119C2C File Offset: 0x00117E2C
		[SecurityCritical]
		internal static IntPtr CreateDesignerContext(IEnumerable<string> paths, [MarshalAs(UnmanagedType.Bool)] bool shared)
		{
			List<string> list = new List<string>(paths);
			string[] array = list.ToArray();
			foreach (string text in array)
			{
				if (text == null)
				{
					throw new ArgumentNullException(Environment.GetResourceString("ArgumentNull_Path"));
				}
				if (Path.IsRelative(text))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_AbsolutePathRequired"));
				}
			}
			return WindowsRuntimeDesignerContext.CreateDesignerContext(array, array.Length, shared);
		}

		// Token: 0x0600501B RID: 20507
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void SetCurrentContext([MarshalAs(UnmanagedType.Bool)] bool isDesignerContext, IntPtr context);

		// Token: 0x0600501C RID: 20508 RVA: 0x00119C94 File Offset: 0x00117E94
		[SecurityCritical]
		private WindowsRuntimeDesignerContext(IEnumerable<string> paths, string name, bool designModeRequired)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (paths == null)
			{
				throw new ArgumentNullException("paths");
			}
			if (!AppDomain.CurrentDomain.IsDefaultAppDomain())
			{
				throw new NotSupportedException();
			}
			if (!AppDomain.IsAppXModel())
			{
				throw new NotSupportedException();
			}
			if (designModeRequired && !AppDomain.IsAppXDesignMode())
			{
				throw new NotSupportedException();
			}
			this.m_name = name;
			object obj = WindowsRuntimeDesignerContext.s_lock;
			lock (obj)
			{
				if (WindowsRuntimeDesignerContext.s_sharedContext == IntPtr.Zero)
				{
					WindowsRuntimeDesignerContext.InitializeSharedContext(new string[0]);
				}
			}
			this.m_contextObject = WindowsRuntimeDesignerContext.CreateDesignerContext(paths, false);
		}

		// Token: 0x0600501D RID: 20509 RVA: 0x00119D50 File Offset: 0x00117F50
		[SecurityCritical]
		public WindowsRuntimeDesignerContext(IEnumerable<string> paths, string name) : this(paths, name, true)
		{
		}

		// Token: 0x0600501E RID: 20510 RVA: 0x00119D5C File Offset: 0x00117F5C
		[SecurityCritical]
		public static void InitializeSharedContext(IEnumerable<string> paths)
		{
			if (!AppDomain.CurrentDomain.IsDefaultAppDomain())
			{
				throw new NotSupportedException();
			}
			if (paths == null)
			{
				throw new ArgumentNullException("paths");
			}
			object obj = WindowsRuntimeDesignerContext.s_lock;
			lock (obj)
			{
				if (WindowsRuntimeDesignerContext.s_sharedContext != IntPtr.Zero)
				{
					throw new NotSupportedException();
				}
				IntPtr context = WindowsRuntimeDesignerContext.CreateDesignerContext(paths, true);
				WindowsRuntimeDesignerContext.SetCurrentContext(false, context);
				WindowsRuntimeDesignerContext.s_sharedContext = context;
			}
		}

		// Token: 0x0600501F RID: 20511 RVA: 0x00119DE4 File Offset: 0x00117FE4
		[SecurityCritical]
		public static void SetIterationContext(WindowsRuntimeDesignerContext context)
		{
			if (!AppDomain.CurrentDomain.IsDefaultAppDomain())
			{
				throw new NotSupportedException();
			}
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			object obj = WindowsRuntimeDesignerContext.s_lock;
			lock (obj)
			{
				WindowsRuntimeDesignerContext.SetCurrentContext(true, context.m_contextObject);
			}
		}

		// Token: 0x06005020 RID: 20512 RVA: 0x00119E4C File Offset: 0x0011804C
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Assembly GetAssembly(string assemblyName)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeAssembly.InternalLoad(assemblyName, null, ref stackCrawlMark, this.m_contextObject, false);
		}

		// Token: 0x06005021 RID: 20513 RVA: 0x00119E6C File Offset: 0x0011806C
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Type GetType(string typeName)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeTypeHandle.GetTypeByName(typeName, false, false, false, ref stackCrawlMark, this.m_contextObject, false);
		}

		// Token: 0x17000D44 RID: 3396
		// (get) Token: 0x06005022 RID: 20514 RVA: 0x00119E9B File Offset: 0x0011809B
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x04002342 RID: 9026
		private static object s_lock = new object();

		// Token: 0x04002343 RID: 9027
		private static IntPtr s_sharedContext;

		// Token: 0x04002344 RID: 9028
		private IntPtr m_contextObject;

		// Token: 0x04002345 RID: 9029
		private string m_name;
	}
}
