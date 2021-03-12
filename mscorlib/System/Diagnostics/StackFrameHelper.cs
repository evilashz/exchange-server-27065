using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Diagnostics
{
	// Token: 0x020003CA RID: 970
	[Serializable]
	internal class StackFrameHelper : IDisposable
	{
		// Token: 0x0600323B RID: 12859 RVA: 0x000C091C File Offset: 0x000BEB1C
		public StackFrameHelper(Thread target)
		{
			this.targetThread = target;
			this.rgMethodBase = null;
			this.rgMethodHandle = null;
			this.rgiMethodToken = null;
			this.rgiOffset = null;
			this.rgiILOffset = null;
			this.rgAssemblyPath = null;
			this.rgLoadedPeAddress = null;
			this.rgiLoadedPeSize = null;
			this.rgInMemoryPdbAddress = null;
			this.rgiInMemoryPdbSize = null;
			this.dynamicMethods = null;
			this.rgFilename = null;
			this.rgiLineNumber = null;
			this.rgiColumnNumber = null;
			this.rgiLastFrameFromForeignExceptionStackTrace = null;
			this.iFrameCount = 0;
		}

		// Token: 0x0600323C RID: 12860 RVA: 0x000C09A8 File Offset: 0x000BEBA8
		[SecuritySafeCritical]
		internal void InitializeSourceInfo(int iSkip, bool fNeedFileInfo, Exception exception)
		{
			StackTrace.GetStackFramesInternal(this, iSkip, fNeedFileInfo, exception);
			if (!fNeedFileInfo)
			{
				return;
			}
			if (!RuntimeFeature.IsSupported("PortablePdb"))
			{
				return;
			}
			if (StackFrameHelper.t_reentrancy > 0)
			{
				return;
			}
			StackFrameHelper.t_reentrancy++;
			try
			{
				if (!CodeAccessSecurityEngine.QuickCheckForAllDemands())
				{
					new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Assert();
					new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
				}
				if (StackFrameHelper.s_getSourceLineInfo == null)
				{
					Type type = Type.GetType("System.Diagnostics.StackTraceSymbols, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", false);
					if (type == null)
					{
						return;
					}
					MethodInfo method = type.GetMethod("GetSourceLineInfoWithoutCasAssert", new Type[]
					{
						typeof(string),
						typeof(IntPtr),
						typeof(int),
						typeof(IntPtr),
						typeof(int),
						typeof(int),
						typeof(int),
						typeof(string).MakeByRefType(),
						typeof(int).MakeByRefType(),
						typeof(int).MakeByRefType()
					});
					if (method == null)
					{
						method = type.GetMethod("GetSourceLineInfo", new Type[]
						{
							typeof(string),
							typeof(IntPtr),
							typeof(int),
							typeof(IntPtr),
							typeof(int),
							typeof(int),
							typeof(int),
							typeof(string).MakeByRefType(),
							typeof(int).MakeByRefType(),
							typeof(int).MakeByRefType()
						});
					}
					if (method == null)
					{
						return;
					}
					object target = Activator.CreateInstance(type);
					StackFrameHelper.GetSourceLineInfoDelegate value = (StackFrameHelper.GetSourceLineInfoDelegate)method.CreateDelegate(typeof(StackFrameHelper.GetSourceLineInfoDelegate), target);
					Interlocked.CompareExchange<StackFrameHelper.GetSourceLineInfoDelegate>(ref StackFrameHelper.s_getSourceLineInfo, value, null);
				}
				for (int i = 0; i < this.iFrameCount; i++)
				{
					if (this.rgiMethodToken[i] != 0)
					{
						StackFrameHelper.s_getSourceLineInfo(this.rgAssemblyPath[i], this.rgLoadedPeAddress[i], this.rgiLoadedPeSize[i], this.rgInMemoryPdbAddress[i], this.rgiInMemoryPdbSize[i], this.rgiMethodToken[i], this.rgiILOffset[i], out this.rgFilename[i], out this.rgiLineNumber[i], out this.rgiColumnNumber[i]);
					}
				}
			}
			catch
			{
			}
			finally
			{
				StackFrameHelper.t_reentrancy--;
			}
		}

		// Token: 0x0600323D RID: 12861 RVA: 0x000C0C90 File Offset: 0x000BEE90
		void IDisposable.Dispose()
		{
		}

		// Token: 0x0600323E RID: 12862 RVA: 0x000C0C94 File Offset: 0x000BEE94
		[SecuritySafeCritical]
		public virtual MethodBase GetMethodBase(int i)
		{
			IntPtr methodHandleValue = this.rgMethodHandle[i];
			if (methodHandleValue.IsNull())
			{
				return null;
			}
			IRuntimeMethodInfo typicalMethodDefinition = RuntimeMethodHandle.GetTypicalMethodDefinition(new RuntimeMethodInfoStub(methodHandleValue, this));
			return RuntimeType.GetMethodBase(typicalMethodDefinition);
		}

		// Token: 0x0600323F RID: 12863 RVA: 0x000C0CC8 File Offset: 0x000BEEC8
		public virtual int GetOffset(int i)
		{
			return this.rgiOffset[i];
		}

		// Token: 0x06003240 RID: 12864 RVA: 0x000C0CD2 File Offset: 0x000BEED2
		public virtual int GetILOffset(int i)
		{
			return this.rgiILOffset[i];
		}

		// Token: 0x06003241 RID: 12865 RVA: 0x000C0CDC File Offset: 0x000BEEDC
		public virtual string GetFilename(int i)
		{
			if (this.rgFilename != null)
			{
				return this.rgFilename[i];
			}
			return null;
		}

		// Token: 0x06003242 RID: 12866 RVA: 0x000C0CF0 File Offset: 0x000BEEF0
		public virtual int GetLineNumber(int i)
		{
			if (this.rgiLineNumber != null)
			{
				return this.rgiLineNumber[i];
			}
			return 0;
		}

		// Token: 0x06003243 RID: 12867 RVA: 0x000C0D04 File Offset: 0x000BEF04
		public virtual int GetColumnNumber(int i)
		{
			if (this.rgiColumnNumber != null)
			{
				return this.rgiColumnNumber[i];
			}
			return 0;
		}

		// Token: 0x06003244 RID: 12868 RVA: 0x000C0D18 File Offset: 0x000BEF18
		public virtual bool IsLastFrameFromForeignExceptionStackTrace(int i)
		{
			return this.rgiLastFrameFromForeignExceptionStackTrace != null && this.rgiLastFrameFromForeignExceptionStackTrace[i];
		}

		// Token: 0x06003245 RID: 12869 RVA: 0x000C0D2C File Offset: 0x000BEF2C
		public virtual int GetNumberOfFrames()
		{
			return this.iFrameCount;
		}

		// Token: 0x06003246 RID: 12870 RVA: 0x000C0D34 File Offset: 0x000BEF34
		public virtual void SetNumberOfFrames(int i)
		{
			this.iFrameCount = i;
		}

		// Token: 0x06003247 RID: 12871 RVA: 0x000C0D40 File Offset: 0x000BEF40
		[OnSerializing]
		[SecuritySafeCritical]
		private void OnSerializing(StreamingContext context)
		{
			this.rgMethodBase = ((this.rgMethodHandle == null) ? null : new MethodBase[this.rgMethodHandle.Length]);
			if (this.rgMethodHandle != null)
			{
				for (int i = 0; i < this.rgMethodHandle.Length; i++)
				{
					if (!this.rgMethodHandle[i].IsNull())
					{
						this.rgMethodBase[i] = RuntimeType.GetMethodBase(new RuntimeMethodInfoStub(this.rgMethodHandle[i], this));
					}
				}
			}
		}

		// Token: 0x06003248 RID: 12872 RVA: 0x000C0DB4 File Offset: 0x000BEFB4
		[OnSerialized]
		private void OnSerialized(StreamingContext context)
		{
			this.rgMethodBase = null;
		}

		// Token: 0x06003249 RID: 12873 RVA: 0x000C0DC0 File Offset: 0x000BEFC0
		[OnDeserialized]
		[SecuritySafeCritical]
		private void OnDeserialized(StreamingContext context)
		{
			this.rgMethodHandle = ((this.rgMethodBase == null) ? null : new IntPtr[this.rgMethodBase.Length]);
			if (this.rgMethodBase != null)
			{
				for (int i = 0; i < this.rgMethodBase.Length; i++)
				{
					if (this.rgMethodBase[i] != null)
					{
						this.rgMethodHandle[i] = this.rgMethodBase[i].MethodHandle.Value;
					}
				}
			}
			this.rgMethodBase = null;
		}

		// Token: 0x04001613 RID: 5651
		[NonSerialized]
		private Thread targetThread;

		// Token: 0x04001614 RID: 5652
		private int[] rgiOffset;

		// Token: 0x04001615 RID: 5653
		private int[] rgiILOffset;

		// Token: 0x04001616 RID: 5654
		private MethodBase[] rgMethodBase;

		// Token: 0x04001617 RID: 5655
		private object dynamicMethods;

		// Token: 0x04001618 RID: 5656
		[NonSerialized]
		private IntPtr[] rgMethodHandle;

		// Token: 0x04001619 RID: 5657
		private string[] rgAssemblyPath;

		// Token: 0x0400161A RID: 5658
		private IntPtr[] rgLoadedPeAddress;

		// Token: 0x0400161B RID: 5659
		private int[] rgiLoadedPeSize;

		// Token: 0x0400161C RID: 5660
		private IntPtr[] rgInMemoryPdbAddress;

		// Token: 0x0400161D RID: 5661
		private int[] rgiInMemoryPdbSize;

		// Token: 0x0400161E RID: 5662
		private int[] rgiMethodToken;

		// Token: 0x0400161F RID: 5663
		private string[] rgFilename;

		// Token: 0x04001620 RID: 5664
		private int[] rgiLineNumber;

		// Token: 0x04001621 RID: 5665
		private int[] rgiColumnNumber;

		// Token: 0x04001622 RID: 5666
		[OptionalField]
		private bool[] rgiLastFrameFromForeignExceptionStackTrace;

		// Token: 0x04001623 RID: 5667
		private int iFrameCount;

		// Token: 0x04001624 RID: 5668
		private static StackFrameHelper.GetSourceLineInfoDelegate s_getSourceLineInfo;

		// Token: 0x04001625 RID: 5669
		[ThreadStatic]
		private static int t_reentrancy;

		// Token: 0x02000B4B RID: 2891
		// (Invoke) Token: 0x06006B30 RID: 27440
		private delegate void GetSourceLineInfoDelegate(string assemblyPath, IntPtr loadedPeAddress, int loadedPeSize, IntPtr inMemoryPdbAddress, int inMemoryPdbSize, int methodToken, int ilOffset, out string sourceFile, out int sourceLine, out int sourceColumn);
	}
}
