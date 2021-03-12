using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.CompliancePolicy.Classification
{
	// Token: 0x0200000D RID: 13
	[TypeLibType(TypeLibTypeFlags.FNonExtensible)]
	[Guid("64F24FE0-700A-4C7D-8589-EA47BD09EC57")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IRulePackageLoader
	{
		// Token: 0x06000043 RID: 67
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetRulePackages([In] uint ulRulePackageRequestDetailsSize, [ComAliasName("Microsoft.Mce.Interop.Api.RULE_PACKAGE_REQUEST_DETAILS")] [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] [Out] RULE_PACKAGE_REQUEST_DETAILS[] rulePackageRequestDetails);

		// Token: 0x06000044 RID: 68
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetUpdatedRulePackageInfo([In] uint ulRulePackageTimestampDetailsSize, [ComAliasName("Microsoft.Mce.Interop.Api.RULE_PACKAGE_TIMESTAMP_DETAILS")] [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] [Out] RULE_PACKAGE_TIMESTAMP_DETAILS[] rulePackageTimestampDetails);
	}
}
