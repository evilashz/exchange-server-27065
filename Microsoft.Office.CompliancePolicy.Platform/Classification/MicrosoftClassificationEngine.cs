using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Microsoft.Office.CompliancePolicy.Classification
{
	// Token: 0x02000028 RID: 40
	[ClassInterface(ClassInterfaceType.None)]
	[TypeLibType(TypeLibTypeFlags.FCanCreate)]
	[Guid("9ACB63F5-A24E-4FDF-80F5-EC909334F2D2")]
	[ComImport]
	public class MicrosoftClassificationEngine : IMicrosoftClassificationEngine, ICAClassificationEngine
	{
		// Token: 0x06000086 RID: 134
		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void Init([MarshalAs(UnmanagedType.Interface)] [In] [Optional] IPropertyBag engineSettings, [MarshalAs(UnmanagedType.Interface)] [In] [Optional] IRulePackageLoader rulePackageLoader);

		// Token: 0x06000087 RID: 135
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		public virtual extern ICAClassificationDefinitionCollection GetClassificationDefinitions([ComAliasName("Microsoft.Mce.Interop.Api.RULE_PACKAGE_DETAILS")] [In] ref RULE_PACKAGE_DETAILS rulePackageDetails);

		// Token: 0x06000088 RID: 136
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		public virtual extern ICAClassificationResultCollection ClassifyTextStream([MarshalAs(UnmanagedType.Interface)] [In] IStream stream, [In] uint rulePackageSize, [ComAliasName("Microsoft.Mce.Interop.Api.RULE_PACKAGE_DETAILS")] [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [In] RULE_PACKAGE_DETAILS[] rulePackageDetails);

		// Token: 0x06000089 RID: 137
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		public virtual extern ICAClassificationSession GetClassificationSession([In] uint rulePackageSize, [ComAliasName("Microsoft.Mce.Interop.Api.RULE_PACKAGE_DETAILS")] [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] RULE_PACKAGE_DETAILS[] rulePackageDetails);

		// Token: 0x0600008A RID: 138
		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void ClearRulePackageCache([MarshalAs(UnmanagedType.BStr)] [In] string rulePackageSetID, [MarshalAs(UnmanagedType.BStr)] [In] string rulePackageID);

		// Token: 0x0600008B RID: 139
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: ComAliasName("Microsoft.Mce.Interop.Api.VERSION_INFORMATION_DETAILS")]
		public virtual extern VERSION_INFORMATION_DETAILS GetEngineVersion();

		// Token: 0x0600008C RID: 140
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.BStr)]
		public virtual extern string GetPerformanceDiagnostics([In] PerformanceDiagnosticsType performanceDiagnosticsType);

		// Token: 0x0600008D RID: 141
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		public virtual extern ICAClassificationStreamSession GetClassificationStreamSession([In] uint rulePackageSize, [ComAliasName("Microsoft.Mce.Interop.Api.RULE_PACKAGE_DETAILS")] [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] RULE_PACKAGE_DETAILS[] rulePackageDetails);

		// Token: 0x0600008E RID: 142
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern MicrosoftClassificationEngine();
	}
}
