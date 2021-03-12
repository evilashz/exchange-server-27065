using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200095B RID: 2395
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IMoniker instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("0000000f-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMIMoniker
	{
		// Token: 0x06006187 RID: 24967
		void GetClassID(out Guid pClassID);

		// Token: 0x06006188 RID: 24968
		[PreserveSig]
		int IsDirty();

		// Token: 0x06006189 RID: 24969
		void Load(UCOMIStream pStm);

		// Token: 0x0600618A RID: 24970
		void Save(UCOMIStream pStm, [MarshalAs(UnmanagedType.Bool)] bool fClearDirty);

		// Token: 0x0600618B RID: 24971
		void GetSizeMax(out long pcbSize);

		// Token: 0x0600618C RID: 24972
		void BindToObject(UCOMIBindCtx pbc, UCOMIMoniker pmkToLeft, [In] ref Guid riidResult, [MarshalAs(UnmanagedType.Interface)] out object ppvResult);

		// Token: 0x0600618D RID: 24973
		void BindToStorage(UCOMIBindCtx pbc, UCOMIMoniker pmkToLeft, [In] ref Guid riid, [MarshalAs(UnmanagedType.Interface)] out object ppvObj);

		// Token: 0x0600618E RID: 24974
		void Reduce(UCOMIBindCtx pbc, int dwReduceHowFar, ref UCOMIMoniker ppmkToLeft, out UCOMIMoniker ppmkReduced);

		// Token: 0x0600618F RID: 24975
		void ComposeWith(UCOMIMoniker pmkRight, [MarshalAs(UnmanagedType.Bool)] bool fOnlyIfNotGeneric, out UCOMIMoniker ppmkComposite);

		// Token: 0x06006190 RID: 24976
		void Enum([MarshalAs(UnmanagedType.Bool)] bool fForward, out UCOMIEnumMoniker ppenumMoniker);

		// Token: 0x06006191 RID: 24977
		void IsEqual(UCOMIMoniker pmkOtherMoniker);

		// Token: 0x06006192 RID: 24978
		void Hash(out int pdwHash);

		// Token: 0x06006193 RID: 24979
		void IsRunning(UCOMIBindCtx pbc, UCOMIMoniker pmkToLeft, UCOMIMoniker pmkNewlyRunning);

		// Token: 0x06006194 RID: 24980
		void GetTimeOfLastChange(UCOMIBindCtx pbc, UCOMIMoniker pmkToLeft, out FILETIME pFileTime);

		// Token: 0x06006195 RID: 24981
		void Inverse(out UCOMIMoniker ppmk);

		// Token: 0x06006196 RID: 24982
		void CommonPrefixWith(UCOMIMoniker pmkOther, out UCOMIMoniker ppmkPrefix);

		// Token: 0x06006197 RID: 24983
		void RelativePathTo(UCOMIMoniker pmkOther, out UCOMIMoniker ppmkRelPath);

		// Token: 0x06006198 RID: 24984
		void GetDisplayName(UCOMIBindCtx pbc, UCOMIMoniker pmkToLeft, [MarshalAs(UnmanagedType.LPWStr)] out string ppszDisplayName);

		// Token: 0x06006199 RID: 24985
		void ParseDisplayName(UCOMIBindCtx pbc, UCOMIMoniker pmkToLeft, [MarshalAs(UnmanagedType.LPWStr)] string pszDisplayName, out int pchEaten, out UCOMIMoniker ppmkOut);

		// Token: 0x0600619A RID: 24986
		void IsSystemMoniker(out int pdwMksys);
	}
}
