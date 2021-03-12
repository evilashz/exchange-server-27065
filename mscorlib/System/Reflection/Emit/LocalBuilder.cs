using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000618 RID: 1560
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_LocalBuilder))]
	[ComVisible(true)]
	public sealed class LocalBuilder : LocalVariableInfo, _LocalBuilder
	{
		// Token: 0x060049ED RID: 18925 RVA: 0x0010B43B File Offset: 0x0010963B
		private LocalBuilder()
		{
		}

		// Token: 0x060049EE RID: 18926 RVA: 0x0010B443 File Offset: 0x00109643
		internal LocalBuilder(int localIndex, Type localType, MethodInfo methodBuilder) : this(localIndex, localType, methodBuilder, false)
		{
		}

		// Token: 0x060049EF RID: 18927 RVA: 0x0010B44F File Offset: 0x0010964F
		internal LocalBuilder(int localIndex, Type localType, MethodInfo methodBuilder, bool isPinned)
		{
			this.m_isPinned = isPinned;
			this.m_localIndex = localIndex;
			this.m_localType = localType;
			this.m_methodBuilder = methodBuilder;
		}

		// Token: 0x060049F0 RID: 18928 RVA: 0x0010B474 File Offset: 0x00109674
		internal int GetLocalIndex()
		{
			return this.m_localIndex;
		}

		// Token: 0x060049F1 RID: 18929 RVA: 0x0010B47C File Offset: 0x0010967C
		internal MethodInfo GetMethodBuilder()
		{
			return this.m_methodBuilder;
		}

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x060049F2 RID: 18930 RVA: 0x0010B484 File Offset: 0x00109684
		public override bool IsPinned
		{
			get
			{
				return this.m_isPinned;
			}
		}

		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x060049F3 RID: 18931 RVA: 0x0010B48C File Offset: 0x0010968C
		public override Type LocalType
		{
			get
			{
				return this.m_localType;
			}
		}

		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x060049F4 RID: 18932 RVA: 0x0010B494 File Offset: 0x00109694
		public override int LocalIndex
		{
			get
			{
				return this.m_localIndex;
			}
		}

		// Token: 0x060049F5 RID: 18933 RVA: 0x0010B49C File Offset: 0x0010969C
		public void SetLocalSymInfo(string name)
		{
			this.SetLocalSymInfo(name, 0, 0);
		}

		// Token: 0x060049F6 RID: 18934 RVA: 0x0010B4A8 File Offset: 0x001096A8
		public void SetLocalSymInfo(string name, int startOffset, int endOffset)
		{
			MethodBuilder methodBuilder = this.m_methodBuilder as MethodBuilder;
			if (methodBuilder == null)
			{
				throw new NotSupportedException();
			}
			ModuleBuilder moduleBuilder = (ModuleBuilder)methodBuilder.Module;
			if (methodBuilder.IsTypeCreated())
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_TypeHasBeenCreated"));
			}
			if (moduleBuilder.GetSymWriter() == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotADebugModule"));
			}
			SignatureHelper fieldSigHelper = SignatureHelper.GetFieldSigHelper(moduleBuilder);
			fieldSigHelper.AddArgument(this.m_localType);
			int num;
			byte[] sourceArray = fieldSigHelper.InternalGetSignature(out num);
			byte[] array = new byte[num - 1];
			Array.Copy(sourceArray, 1, array, 0, num - 1);
			int currentActiveScopeIndex = methodBuilder.GetILGenerator().m_ScopeTree.GetCurrentActiveScopeIndex();
			if (currentActiveScopeIndex == -1)
			{
				methodBuilder.m_localSymInfo.AddLocalSymInfo(name, array, this.m_localIndex, startOffset, endOffset);
				return;
			}
			methodBuilder.GetILGenerator().m_ScopeTree.AddLocalSymInfoToCurrentScope(name, array, this.m_localIndex, startOffset, endOffset);
		}

		// Token: 0x060049F7 RID: 18935 RVA: 0x0010B58F File Offset: 0x0010978F
		void _LocalBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060049F8 RID: 18936 RVA: 0x0010B596 File Offset: 0x00109796
		void _LocalBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060049F9 RID: 18937 RVA: 0x0010B59D File Offset: 0x0010979D
		void _LocalBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060049FA RID: 18938 RVA: 0x0010B5A4 File Offset: 0x001097A4
		void _LocalBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001E4B RID: 7755
		private int m_localIndex;

		// Token: 0x04001E4C RID: 7756
		private Type m_localType;

		// Token: 0x04001E4D RID: 7757
		private MethodInfo m_methodBuilder;

		// Token: 0x04001E4E RID: 7758
		private bool m_isPinned;
	}
}
