using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A04 RID: 2564
	[Guid("0000000f-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[__DynamicallyInvokable]
	[ComImport]
	public interface IMoniker
	{
		// Token: 0x0600651B RID: 25883
		[__DynamicallyInvokable]
		void GetClassID(out Guid pClassID);

		// Token: 0x0600651C RID: 25884
		[__DynamicallyInvokable]
		[PreserveSig]
		int IsDirty();

		// Token: 0x0600651D RID: 25885
		[__DynamicallyInvokable]
		void Load(IStream pStm);

		// Token: 0x0600651E RID: 25886
		[__DynamicallyInvokable]
		void Save(IStream pStm, [MarshalAs(UnmanagedType.Bool)] bool fClearDirty);

		// Token: 0x0600651F RID: 25887
		[__DynamicallyInvokable]
		void GetSizeMax(out long pcbSize);

		// Token: 0x06006520 RID: 25888
		[__DynamicallyInvokable]
		void BindToObject(IBindCtx pbc, IMoniker pmkToLeft, [In] ref Guid riidResult, [MarshalAs(UnmanagedType.Interface)] out object ppvResult);

		// Token: 0x06006521 RID: 25889
		[__DynamicallyInvokable]
		void BindToStorage(IBindCtx pbc, IMoniker pmkToLeft, [In] ref Guid riid, [MarshalAs(UnmanagedType.Interface)] out object ppvObj);

		// Token: 0x06006522 RID: 25890
		[__DynamicallyInvokable]
		void Reduce(IBindCtx pbc, int dwReduceHowFar, ref IMoniker ppmkToLeft, out IMoniker ppmkReduced);

		// Token: 0x06006523 RID: 25891
		[__DynamicallyInvokable]
		void ComposeWith(IMoniker pmkRight, [MarshalAs(UnmanagedType.Bool)] bool fOnlyIfNotGeneric, out IMoniker ppmkComposite);

		// Token: 0x06006524 RID: 25892
		[__DynamicallyInvokable]
		void Enum([MarshalAs(UnmanagedType.Bool)] bool fForward, out IEnumMoniker ppenumMoniker);

		// Token: 0x06006525 RID: 25893
		[__DynamicallyInvokable]
		[PreserveSig]
		int IsEqual(IMoniker pmkOtherMoniker);

		// Token: 0x06006526 RID: 25894
		[__DynamicallyInvokable]
		void Hash(out int pdwHash);

		// Token: 0x06006527 RID: 25895
		[__DynamicallyInvokable]
		[PreserveSig]
		int IsRunning(IBindCtx pbc, IMoniker pmkToLeft, IMoniker pmkNewlyRunning);

		// Token: 0x06006528 RID: 25896
		[__DynamicallyInvokable]
		void GetTimeOfLastChange(IBindCtx pbc, IMoniker pmkToLeft, out FILETIME pFileTime);

		// Token: 0x06006529 RID: 25897
		[__DynamicallyInvokable]
		void Inverse(out IMoniker ppmk);

		// Token: 0x0600652A RID: 25898
		[__DynamicallyInvokable]
		void CommonPrefixWith(IMoniker pmkOther, out IMoniker ppmkPrefix);

		// Token: 0x0600652B RID: 25899
		[__DynamicallyInvokable]
		void RelativePathTo(IMoniker pmkOther, out IMoniker ppmkRelPath);

		// Token: 0x0600652C RID: 25900
		[__DynamicallyInvokable]
		void GetDisplayName(IBindCtx pbc, IMoniker pmkToLeft, [MarshalAs(UnmanagedType.LPWStr)] out string ppszDisplayName);

		// Token: 0x0600652D RID: 25901
		[__DynamicallyInvokable]
		void ParseDisplayName(IBindCtx pbc, IMoniker pmkToLeft, [MarshalAs(UnmanagedType.LPWStr)] string pszDisplayName, out int pchEaten, out IMoniker ppmkOut);

		// Token: 0x0600652E RID: 25902
		[__DynamicallyInvokable]
		[PreserveSig]
		int IsSystemMoniker(out int pdwMksys);
	}
}
