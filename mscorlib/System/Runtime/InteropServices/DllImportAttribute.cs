using System;
using System.Reflection;
using System.Security;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000907 RID: 2311
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class DllImportAttribute : Attribute
	{
		// Token: 0x06005F1D RID: 24349 RVA: 0x00147030 File Offset: 0x00145230
		[SecurityCritical]
		internal static Attribute GetCustomAttribute(RuntimeMethodInfo method)
		{
			if ((method.Attributes & MethodAttributes.PinvokeImpl) == MethodAttributes.PrivateScope)
			{
				return null;
			}
			MetadataImport metadataImport = ModuleHandle.GetMetadataImport(method.Module.ModuleHandle.GetRuntimeModule());
			string dllName = null;
			int metadataToken = method.MetadataToken;
			PInvokeAttributes pinvokeAttributes = PInvokeAttributes.CharSetNotSpec;
			string entryPoint;
			metadataImport.GetPInvokeMap(metadataToken, out pinvokeAttributes, out entryPoint, out dllName);
			CharSet charSet = CharSet.None;
			switch (pinvokeAttributes & PInvokeAttributes.CharSetMask)
			{
			case PInvokeAttributes.CharSetNotSpec:
				charSet = CharSet.None;
				break;
			case PInvokeAttributes.CharSetAnsi:
				charSet = CharSet.Ansi;
				break;
			case PInvokeAttributes.CharSetUnicode:
				charSet = CharSet.Unicode;
				break;
			case PInvokeAttributes.CharSetMask:
				charSet = CharSet.Auto;
				break;
			}
			CallingConvention callingConvention = CallingConvention.Cdecl;
			PInvokeAttributes pinvokeAttributes2 = pinvokeAttributes & PInvokeAttributes.CallConvMask;
			if (pinvokeAttributes2 <= PInvokeAttributes.CallConvCdecl)
			{
				if (pinvokeAttributes2 != PInvokeAttributes.CallConvWinapi)
				{
					if (pinvokeAttributes2 == PInvokeAttributes.CallConvCdecl)
					{
						callingConvention = CallingConvention.Cdecl;
					}
				}
				else
				{
					callingConvention = CallingConvention.Winapi;
				}
			}
			else if (pinvokeAttributes2 != PInvokeAttributes.CallConvStdcall)
			{
				if (pinvokeAttributes2 != PInvokeAttributes.CallConvThiscall)
				{
					if (pinvokeAttributes2 == PInvokeAttributes.CallConvFastcall)
					{
						callingConvention = CallingConvention.FastCall;
					}
				}
				else
				{
					callingConvention = CallingConvention.ThisCall;
				}
			}
			else
			{
				callingConvention = CallingConvention.StdCall;
			}
			bool exactSpelling = (pinvokeAttributes & PInvokeAttributes.NoMangle) > PInvokeAttributes.CharSetNotSpec;
			bool setLastError = (pinvokeAttributes & PInvokeAttributes.SupportsLastError) > PInvokeAttributes.CharSetNotSpec;
			bool bestFitMapping = (pinvokeAttributes & PInvokeAttributes.BestFitMask) == PInvokeAttributes.BestFitEnabled;
			bool throwOnUnmappableChar = (pinvokeAttributes & PInvokeAttributes.ThrowOnUnmappableCharMask) == PInvokeAttributes.ThrowOnUnmappableCharEnabled;
			bool preserveSig = (method.GetMethodImplementationFlags() & MethodImplAttributes.PreserveSig) > MethodImplAttributes.IL;
			return new DllImportAttribute(dllName, entryPoint, charSet, exactSpelling, setLastError, preserveSig, callingConvention, bestFitMapping, throwOnUnmappableChar);
		}

		// Token: 0x06005F1E RID: 24350 RVA: 0x00147174 File Offset: 0x00145374
		internal static bool IsDefined(RuntimeMethodInfo method)
		{
			return (method.Attributes & MethodAttributes.PinvokeImpl) > MethodAttributes.PrivateScope;
		}

		// Token: 0x06005F1F RID: 24351 RVA: 0x00147188 File Offset: 0x00145388
		internal DllImportAttribute(string dllName, string entryPoint, CharSet charSet, bool exactSpelling, bool setLastError, bool preserveSig, CallingConvention callingConvention, bool bestFitMapping, bool throwOnUnmappableChar)
		{
			this._val = dllName;
			this.EntryPoint = entryPoint;
			this.CharSet = charSet;
			this.ExactSpelling = exactSpelling;
			this.SetLastError = setLastError;
			this.PreserveSig = preserveSig;
			this.CallingConvention = callingConvention;
			this.BestFitMapping = bestFitMapping;
			this.ThrowOnUnmappableChar = throwOnUnmappableChar;
		}

		// Token: 0x06005F20 RID: 24352 RVA: 0x001471E0 File Offset: 0x001453E0
		[__DynamicallyInvokable]
		public DllImportAttribute(string dllName)
		{
			this._val = dllName;
		}

		// Token: 0x170010CB RID: 4299
		// (get) Token: 0x06005F21 RID: 24353 RVA: 0x001471EF File Offset: 0x001453EF
		[__DynamicallyInvokable]
		public string Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002A57 RID: 10839
		internal string _val;

		// Token: 0x04002A58 RID: 10840
		[__DynamicallyInvokable]
		public string EntryPoint;

		// Token: 0x04002A59 RID: 10841
		[__DynamicallyInvokable]
		public CharSet CharSet;

		// Token: 0x04002A5A RID: 10842
		[__DynamicallyInvokable]
		public bool SetLastError;

		// Token: 0x04002A5B RID: 10843
		[__DynamicallyInvokable]
		public bool ExactSpelling;

		// Token: 0x04002A5C RID: 10844
		[__DynamicallyInvokable]
		public bool PreserveSig;

		// Token: 0x04002A5D RID: 10845
		[__DynamicallyInvokable]
		public CallingConvention CallingConvention;

		// Token: 0x04002A5E RID: 10846
		[__DynamicallyInvokable]
		public bool BestFitMapping;

		// Token: 0x04002A5F RID: 10847
		[__DynamicallyInvokable]
		public bool ThrowOnUnmappableChar;
	}
}
