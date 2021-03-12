using System;
using System.Security;
using System.Threading;

namespace System.Reflection.Emit
{
	// Token: 0x02000605 RID: 1541
	internal class DynamicResolver : Resolver
	{
		// Token: 0x060048DD RID: 18653 RVA: 0x00107244 File Offset: 0x00105444
		internal DynamicResolver(DynamicILGenerator ilGenerator)
		{
			this.m_stackSize = ilGenerator.GetMaxStackSize();
			this.m_exceptions = ilGenerator.GetExceptions();
			this.m_code = ilGenerator.BakeByteArray();
			this.m_localSignature = ilGenerator.m_localSignature.InternalGetSignatureArray();
			this.m_scope = ilGenerator.m_scope;
			this.m_method = (DynamicMethod)ilGenerator.m_methodBuilder;
			this.m_method.m_resolver = this;
		}

		// Token: 0x060048DE RID: 18654 RVA: 0x001072B8 File Offset: 0x001054B8
		internal DynamicResolver(DynamicILInfo dynamicILInfo)
		{
			this.m_stackSize = dynamicILInfo.MaxStackSize;
			this.m_code = dynamicILInfo.Code;
			this.m_localSignature = dynamicILInfo.LocalSignature;
			this.m_exceptionHeader = dynamicILInfo.Exceptions;
			this.m_scope = dynamicILInfo.DynamicScope;
			this.m_method = dynamicILInfo.DynamicMethod;
			this.m_method.m_resolver = this;
		}

		// Token: 0x060048DF RID: 18655 RVA: 0x00107320 File Offset: 0x00105520
		protected override void Finalize()
		{
			try
			{
				DynamicMethod method = this.m_method;
				if (!(method == null))
				{
					if (method.m_methodHandle != null)
					{
						DynamicResolver.DestroyScout destroyScout = null;
						try
						{
							destroyScout = new DynamicResolver.DestroyScout();
						}
						catch
						{
							if (!Environment.HasShutdownStarted && !AppDomain.CurrentDomain.IsFinalizingForUnload())
							{
								GC.ReRegisterForFinalize(this);
							}
							return;
						}
						destroyScout.m_methodHandle = method.m_methodHandle.Value;
					}
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x060048E0 RID: 18656 RVA: 0x001073A8 File Offset: 0x001055A8
		internal override RuntimeType GetJitContext(ref int securityControlFlags)
		{
			DynamicResolver.SecurityControlFlags securityControlFlags2 = DynamicResolver.SecurityControlFlags.Default;
			if (this.m_method.m_restrictedSkipVisibility)
			{
				securityControlFlags2 |= DynamicResolver.SecurityControlFlags.RestrictedSkipVisibilityChecks;
			}
			else if (this.m_method.m_skipVisibility)
			{
				securityControlFlags2 |= DynamicResolver.SecurityControlFlags.SkipVisibilityChecks;
			}
			RuntimeType typeOwner = this.m_method.m_typeOwner;
			if (this.m_method.m_creationContext != null)
			{
				securityControlFlags2 |= DynamicResolver.SecurityControlFlags.HasCreationContext;
				if (this.m_method.m_creationContext.CanSkipEvaluation)
				{
					securityControlFlags2 |= DynamicResolver.SecurityControlFlags.CanSkipCSEvaluation;
				}
			}
			securityControlFlags = (int)securityControlFlags2;
			return typeOwner;
		}

		// Token: 0x060048E1 RID: 18657 RVA: 0x00107414 File Offset: 0x00105614
		private static int CalculateNumberOfExceptions(__ExceptionInfo[] excp)
		{
			int num = 0;
			if (excp == null)
			{
				return 0;
			}
			for (int i = 0; i < excp.Length; i++)
			{
				num += excp[i].GetNumberOfCatches();
			}
			return num;
		}

		// Token: 0x060048E2 RID: 18658 RVA: 0x00107444 File Offset: 0x00105644
		internal override byte[] GetCodeInfo(ref int stackSize, ref int initLocals, ref int EHCount)
		{
			stackSize = this.m_stackSize;
			if (this.m_exceptionHeader != null && this.m_exceptionHeader.Length != 0)
			{
				if (this.m_exceptionHeader.Length < 4)
				{
					throw new FormatException();
				}
				byte b = this.m_exceptionHeader[0];
				if ((b & 64) != 0)
				{
					byte[] array = new byte[4];
					for (int i = 0; i < 3; i++)
					{
						array[i] = this.m_exceptionHeader[i + 1];
					}
					EHCount = (BitConverter.ToInt32(array, 0) - 4) / 24;
				}
				else
				{
					EHCount = (int)((this.m_exceptionHeader[1] - 2) / 12);
				}
			}
			else
			{
				EHCount = DynamicResolver.CalculateNumberOfExceptions(this.m_exceptions);
			}
			initLocals = (this.m_method.InitLocals ? 1 : 0);
			return this.m_code;
		}

		// Token: 0x060048E3 RID: 18659 RVA: 0x001074F1 File Offset: 0x001056F1
		internal override byte[] GetLocalsSignature()
		{
			return this.m_localSignature;
		}

		// Token: 0x060048E4 RID: 18660 RVA: 0x001074F9 File Offset: 0x001056F9
		internal override byte[] GetRawEHInfo()
		{
			return this.m_exceptionHeader;
		}

		// Token: 0x060048E5 RID: 18661 RVA: 0x00107504 File Offset: 0x00105704
		[SecurityCritical]
		internal unsafe override void GetEHInfo(int excNumber, void* exc)
		{
			for (int i = 0; i < this.m_exceptions.Length; i++)
			{
				int numberOfCatches = this.m_exceptions[i].GetNumberOfCatches();
				if (excNumber < numberOfCatches)
				{
					((Resolver.CORINFO_EH_CLAUSE*)exc)->Flags = this.m_exceptions[i].GetExceptionTypes()[excNumber];
					((Resolver.CORINFO_EH_CLAUSE*)exc)->TryOffset = this.m_exceptions[i].GetStartAddress();
					if ((((Resolver.CORINFO_EH_CLAUSE*)exc)->Flags & 2) != 2)
					{
						((Resolver.CORINFO_EH_CLAUSE*)exc)->TryLength = this.m_exceptions[i].GetEndAddress() - ((Resolver.CORINFO_EH_CLAUSE*)exc)->TryOffset;
					}
					else
					{
						((Resolver.CORINFO_EH_CLAUSE*)exc)->TryLength = this.m_exceptions[i].GetFinallyEndAddress() - ((Resolver.CORINFO_EH_CLAUSE*)exc)->TryOffset;
					}
					((Resolver.CORINFO_EH_CLAUSE*)exc)->HandlerOffset = this.m_exceptions[i].GetCatchAddresses()[excNumber];
					((Resolver.CORINFO_EH_CLAUSE*)exc)->HandlerLength = this.m_exceptions[i].GetCatchEndAddresses()[excNumber] - ((Resolver.CORINFO_EH_CLAUSE*)exc)->HandlerOffset;
					((Resolver.CORINFO_EH_CLAUSE*)exc)->ClassTokenOrFilterOffset = this.m_exceptions[i].GetFilterAddresses()[excNumber];
					return;
				}
				excNumber -= numberOfCatches;
			}
		}

		// Token: 0x060048E6 RID: 18662 RVA: 0x001075F6 File Offset: 0x001057F6
		internal override string GetStringLiteral(int token)
		{
			return this.m_scope.GetString(token);
		}

		// Token: 0x060048E7 RID: 18663 RVA: 0x00107604 File Offset: 0x00105804
		internal override CompressedStack GetSecurityContext()
		{
			return this.m_method.m_creationContext;
		}

		// Token: 0x060048E8 RID: 18664 RVA: 0x00107614 File Offset: 0x00105814
		[SecurityCritical]
		internal override void ResolveToken(int token, out IntPtr typeHandle, out IntPtr methodHandle, out IntPtr fieldHandle)
		{
			typeHandle = 0;
			methodHandle = 0;
			fieldHandle = 0;
			object obj = this.m_scope[token];
			if (obj == null)
			{
				throw new InvalidProgramException();
			}
			if (obj is RuntimeTypeHandle)
			{
				typeHandle = ((RuntimeTypeHandle)obj).Value;
				return;
			}
			if (obj is RuntimeMethodHandle)
			{
				methodHandle = ((RuntimeMethodHandle)obj).Value;
				return;
			}
			if (obj is RuntimeFieldHandle)
			{
				fieldHandle = ((RuntimeFieldHandle)obj).Value;
				return;
			}
			DynamicMethod dynamicMethod = obj as DynamicMethod;
			if (dynamicMethod != null)
			{
				methodHandle = dynamicMethod.GetMethodDescriptor().Value;
				return;
			}
			GenericMethodInfo genericMethodInfo = obj as GenericMethodInfo;
			if (genericMethodInfo != null)
			{
				methodHandle = genericMethodInfo.m_methodHandle.Value;
				typeHandle = genericMethodInfo.m_context.Value;
				return;
			}
			GenericFieldInfo genericFieldInfo = obj as GenericFieldInfo;
			if (genericFieldInfo != null)
			{
				fieldHandle = genericFieldInfo.m_fieldHandle.Value;
				typeHandle = genericFieldInfo.m_context.Value;
				return;
			}
			VarArgMethod varArgMethod = obj as VarArgMethod;
			if (varArgMethod == null)
			{
				return;
			}
			if (varArgMethod.m_dynamicMethod == null)
			{
				methodHandle = varArgMethod.m_method.MethodHandle.Value;
				typeHandle = varArgMethod.m_method.GetDeclaringTypeInternal().GetTypeHandleInternal().Value;
				return;
			}
			methodHandle = varArgMethod.m_dynamicMethod.GetMethodDescriptor().Value;
		}

		// Token: 0x060048E9 RID: 18665 RVA: 0x00107770 File Offset: 0x00105970
		internal override byte[] ResolveSignature(int token, int fromMethod)
		{
			return this.m_scope.ResolveSignature(token, fromMethod);
		}

		// Token: 0x060048EA RID: 18666 RVA: 0x0010777F File Offset: 0x0010597F
		internal override MethodInfo GetDynamicMethod()
		{
			return this.m_method.GetMethodInfo();
		}

		// Token: 0x04001DCC RID: 7628
		private __ExceptionInfo[] m_exceptions;

		// Token: 0x04001DCD RID: 7629
		private byte[] m_exceptionHeader;

		// Token: 0x04001DCE RID: 7630
		private DynamicMethod m_method;

		// Token: 0x04001DCF RID: 7631
		private byte[] m_code;

		// Token: 0x04001DD0 RID: 7632
		private byte[] m_localSignature;

		// Token: 0x04001DD1 RID: 7633
		private int m_stackSize;

		// Token: 0x04001DD2 RID: 7634
		private DynamicScope m_scope;

		// Token: 0x02000C0A RID: 3082
		private class DestroyScout
		{
			// Token: 0x06006F22 RID: 28450 RVA: 0x0017E3D0 File Offset: 0x0017C5D0
			[SecuritySafeCritical]
			~DestroyScout()
			{
				if (!this.m_methodHandle.IsNullHandle())
				{
					if (RuntimeMethodHandle.GetResolver(this.m_methodHandle) != null)
					{
						if (!Environment.HasShutdownStarted && !AppDomain.CurrentDomain.IsFinalizingForUnload())
						{
							GC.ReRegisterForFinalize(this);
						}
					}
					else
					{
						RuntimeMethodHandle.Destroy(this.m_methodHandle);
					}
				}
			}

			// Token: 0x04003667 RID: 13927
			internal RuntimeMethodHandleInternal m_methodHandle;
		}

		// Token: 0x02000C0B RID: 3083
		[Flags]
		internal enum SecurityControlFlags
		{
			// Token: 0x04003669 RID: 13929
			Default = 0,
			// Token: 0x0400366A RID: 13930
			SkipVisibilityChecks = 1,
			// Token: 0x0400366B RID: 13931
			RestrictedSkipVisibilityChecks = 2,
			// Token: 0x0400366C RID: 13932
			HasCreationContext = 4,
			// Token: 0x0400366D RID: 13933
			CanSkipCSEvaluation = 8
		}
	}
}
