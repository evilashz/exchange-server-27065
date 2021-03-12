using System;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020002A0 RID: 672
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MsiNativeMethods
	{
		// Token: 0x06001827 RID: 6183 RVA: 0x00065F29 File Offset: 0x00064129
		private MsiNativeMethods()
		{
		}

		// Token: 0x06001828 RID: 6184
		[DllImport("msi", CharSet = CharSet.Unicode, EntryPoint = "MsiInstallProductW")]
		internal static extern uint InstallProduct(string packagePath, string commandLine);

		// Token: 0x06001829 RID: 6185
		[DllImport("msi", CharSet = CharSet.Unicode, EntryPoint = "MsiDetermineApplicablePatchesW")]
		internal static extern uint DetermineApplicablePatches(string packagePath, int count, [In] [Out] MsiNativeMethods.PatchSequenceInfo[] patches);

		// Token: 0x0600182A RID: 6186
		[DllImport("msi", CharSet = CharSet.Unicode, EntryPoint = "MsiConfigureProductExW")]
		internal static extern uint ConfigureProduct(string productCodeString, InstallLevel installLevel, InstallState installState, string commandLine);

		// Token: 0x0600182B RID: 6187
		[DllImport("msi", EntryPoint = "MsiSetInternalUI")]
		internal static extern InstallUILevel SetInternalUI(InstallUILevel uiLevel, [In] [Out] ref IntPtr window);

		// Token: 0x0600182C RID: 6188
		[DllImport("msi", EntryPoint = "MsiSetExternalUI")]
		internal static extern MsiUIHandlerDelegate SetExternalUI([MarshalAs(UnmanagedType.FunctionPtr)] MsiUIHandlerDelegate handler, InstallLogMode filter, object context);

		// Token: 0x0600182D RID: 6189
		[DllImport("msi", CharSet = CharSet.Unicode, EntryPoint = "MsiEnableLogW")]
		internal static extern uint EnableLog(InstallLogMode logMode, string logFile, InstallLogAttributes logAttributes);

		// Token: 0x0600182E RID: 6190
		[DllImport("msi", CharSet = CharSet.Unicode, EntryPoint = "MsiGetProductInfoW")]
		internal static extern uint GetProductInfo(string productCodeString, string propertyName, StringBuilder propertyValue, ref uint propertyValueSize);

		// Token: 0x0600182F RID: 6191
		[DllImport("msi", CharSet = CharSet.Unicode, EntryPoint = "MsiGetProductPropertyW")]
		internal static extern uint GetProductProperty(SafeMsiHandle product, string propertyName, StringBuilder propertyValue, ref uint propertyValueSize);

		// Token: 0x06001830 RID: 6192
		[DllImport("msi", CharSet = CharSet.Unicode, EntryPoint = "MsiQueryProductStateW")]
		internal static extern InstallState QueryProductState(string productCodeString);

		// Token: 0x06001831 RID: 6193
		[DllImport("msi", CharSet = CharSet.Unicode, EntryPoint = "MsiOpenPackageW")]
		internal static extern uint OpenPackage(string packagePath, out SafeMsiHandle product);

		// Token: 0x06001832 RID: 6194
		[DllImport("msi", CharSet = CharSet.Unicode, EntryPoint = "MsiOpenPackageExW")]
		internal static extern uint OpenPackageEx(string packagePath, OpenPackageFlags options, out SafeMsiHandle product);

		// Token: 0x06001833 RID: 6195 RVA: 0x00065F31 File Offset: 0x00064131
		internal static int ComparePatchSequence(MsiNativeMethods.PatchSequenceInfo p1, MsiNativeMethods.PatchSequenceInfo p2)
		{
			if (p1.order < p2.order)
			{
				return -1;
			}
			if (p1.order == p2.order)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x020002A1 RID: 673
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct PatchSequenceInfo
		{
			// Token: 0x04000A30 RID: 2608
			public string patchData;

			// Token: 0x04000A31 RID: 2609
			public PatchDataType patchDataType;

			// Token: 0x04000A32 RID: 2610
			public int order;

			// Token: 0x04000A33 RID: 2611
			public uint status;
		}
	}
}
