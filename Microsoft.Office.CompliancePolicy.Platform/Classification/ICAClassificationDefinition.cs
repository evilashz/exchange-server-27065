using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.CompliancePolicy.Classification
{
	// Token: 0x0200001A RID: 26
	[Guid("2FBDB1F0-90B0-4008-9F43-FA5FFCAAF9A2")]
	[TypeLibType(TypeLibTypeFlags.FNonExtensible)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface ICAClassificationDefinition
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600006E RID: 110
		string ID { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600006F RID: 111
		string PublisherID { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000070 RID: 112
		ICAAttributeDefinitionCollection AttributeDefinitions { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x06000071 RID: 113
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: ComAliasName("Microsoft.Mce.Interop.Api.CLASSIFICATION_DEFINITION_DETAILS")]
		CLASSIFICATION_DEFINITION_DETAILS GetLocalizableDetails([MarshalAs(UnmanagedType.BStr)] [In] string localeName);

		// Token: 0x06000072 RID: 114
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: ComAliasName("Microsoft.Mce.Interop.Api.VERSION_INFORMATION_DETAILS")]
		VERSION_INFORMATION_DETAILS GetRulePackageVersion();
	}
}
