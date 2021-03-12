﻿using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Runtime.Hosting
{
	// Token: 0x0200002D RID: 45
	internal static class StrongNameHelpers
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00004A80 File Offset: 0x00002C80
		private static IClrStrongName StrongName
		{
			[SecurityCritical]
			get
			{
				if (StrongNameHelpers.s_StrongName == null)
				{
					StrongNameHelpers.s_StrongName = (IClrStrongName)RuntimeEnvironment.GetRuntimeInterfaceAsObject(new Guid("B79B0ACD-F5CD-409b-B5A5-A16244610B92"), new Guid("9FD93CCF-3280-4391-B3A9-96E1CDE77C8D"));
				}
				return StrongNameHelpers.s_StrongName;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00004AB1 File Offset: 0x00002CB1
		private static IClrStrongNameUsingIntPtr StrongNameUsingIntPtr
		{
			[SecurityCritical]
			get
			{
				return (IClrStrongNameUsingIntPtr)StrongNameHelpers.StrongName;
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00004ABD File Offset: 0x00002CBD
		[SecurityCritical]
		public static int StrongNameErrorInfo()
		{
			return StrongNameHelpers.ts_LastStrongNameHR;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00004AC4 File Offset: 0x00002CC4
		[SecurityCritical]
		public static void StrongNameFreeBuffer(IntPtr pbMemory)
		{
			StrongNameHelpers.StrongNameUsingIntPtr.StrongNameFreeBuffer(pbMemory);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00004AD4 File Offset: 0x00002CD4
		[SecurityCritical]
		public static bool StrongNameGetPublicKey(string pwzKeyContainer, IntPtr pbKeyBlob, int cbKeyBlob, out IntPtr ppbPublicKeyBlob, out int pcbPublicKeyBlob)
		{
			int num = StrongNameHelpers.StrongNameUsingIntPtr.StrongNameGetPublicKey(pwzKeyContainer, pbKeyBlob, cbKeyBlob, out ppbPublicKeyBlob, out pcbPublicKeyBlob);
			if (num < 0)
			{
				StrongNameHelpers.ts_LastStrongNameHR = num;
				ppbPublicKeyBlob = IntPtr.Zero;
				pcbPublicKeyBlob = 0;
				return false;
			}
			return true;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00004B0C File Offset: 0x00002D0C
		[SecurityCritical]
		public static bool StrongNameKeyDelete(string pwzKeyContainer)
		{
			int num = StrongNameHelpers.StrongName.StrongNameKeyDelete(pwzKeyContainer);
			if (num < 0)
			{
				StrongNameHelpers.ts_LastStrongNameHR = num;
				return false;
			}
			return true;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00004B34 File Offset: 0x00002D34
		[SecurityCritical]
		public static bool StrongNameKeyGen(string pwzKeyContainer, int dwFlags, out IntPtr ppbKeyBlob, out int pcbKeyBlob)
		{
			int num = StrongNameHelpers.StrongName.StrongNameKeyGen(pwzKeyContainer, dwFlags, out ppbKeyBlob, out pcbKeyBlob);
			if (num < 0)
			{
				StrongNameHelpers.ts_LastStrongNameHR = num;
				ppbKeyBlob = IntPtr.Zero;
				pcbKeyBlob = 0;
				return false;
			}
			return true;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00004B68 File Offset: 0x00002D68
		[SecurityCritical]
		public static bool StrongNameKeyInstall(string pwzKeyContainer, IntPtr pbKeyBlob, int cbKeyBlob)
		{
			int num = StrongNameHelpers.StrongNameUsingIntPtr.StrongNameKeyInstall(pwzKeyContainer, pbKeyBlob, cbKeyBlob);
			if (num < 0)
			{
				StrongNameHelpers.ts_LastStrongNameHR = num;
				return false;
			}
			return true;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00004B90 File Offset: 0x00002D90
		[SecurityCritical]
		public static bool StrongNameSignatureGeneration(string pwzFilePath, string pwzKeyContainer, IntPtr pbKeyBlob, int cbKeyBlob)
		{
			IntPtr zero = IntPtr.Zero;
			int num = 0;
			return StrongNameHelpers.StrongNameSignatureGeneration(pwzFilePath, pwzKeyContainer, pbKeyBlob, cbKeyBlob, ref zero, out num);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00004BB4 File Offset: 0x00002DB4
		[SecurityCritical]
		public static bool StrongNameSignatureGeneration(string pwzFilePath, string pwzKeyContainer, IntPtr pbKeyBlob, int cbKeyBlob, ref IntPtr ppbSignatureBlob, out int pcbSignatureBlob)
		{
			int num = StrongNameHelpers.StrongNameUsingIntPtr.StrongNameSignatureGeneration(pwzFilePath, pwzKeyContainer, pbKeyBlob, cbKeyBlob, ppbSignatureBlob, out pcbSignatureBlob);
			if (num < 0)
			{
				StrongNameHelpers.ts_LastStrongNameHR = num;
				pcbSignatureBlob = 0;
				return false;
			}
			return true;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00004BE8 File Offset: 0x00002DE8
		[SecurityCritical]
		public static bool StrongNameSignatureSize(IntPtr pbPublicKeyBlob, int cbPublicKeyBlob, out int pcbSize)
		{
			int num = StrongNameHelpers.StrongNameUsingIntPtr.StrongNameSignatureSize(pbPublicKeyBlob, cbPublicKeyBlob, out pcbSize);
			if (num < 0)
			{
				StrongNameHelpers.ts_LastStrongNameHR = num;
				pcbSize = 0;
				return false;
			}
			return true;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00004C14 File Offset: 0x00002E14
		[SecurityCritical]
		public static bool StrongNameSignatureVerification(string pwzFilePath, int dwInFlags, out int pdwOutFlags)
		{
			int num = StrongNameHelpers.StrongName.StrongNameSignatureVerification(pwzFilePath, dwInFlags, out pdwOutFlags);
			if (num < 0)
			{
				StrongNameHelpers.ts_LastStrongNameHR = num;
				pdwOutFlags = 0;
				return false;
			}
			return true;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00004C40 File Offset: 0x00002E40
		[SecurityCritical]
		public static bool StrongNameSignatureVerificationEx(string pwzFilePath, bool fForceVerification, out bool pfWasVerified)
		{
			int num = StrongNameHelpers.StrongName.StrongNameSignatureVerificationEx(pwzFilePath, fForceVerification, out pfWasVerified);
			if (num < 0)
			{
				StrongNameHelpers.ts_LastStrongNameHR = num;
				pfWasVerified = false;
				return false;
			}
			return true;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00004C6C File Offset: 0x00002E6C
		[SecurityCritical]
		public static bool StrongNameTokenFromPublicKey(IntPtr pbPublicKeyBlob, int cbPublicKeyBlob, out IntPtr ppbStrongNameToken, out int pcbStrongNameToken)
		{
			int num = StrongNameHelpers.StrongNameUsingIntPtr.StrongNameTokenFromPublicKey(pbPublicKeyBlob, cbPublicKeyBlob, out ppbStrongNameToken, out pcbStrongNameToken);
			if (num < 0)
			{
				StrongNameHelpers.ts_LastStrongNameHR = num;
				ppbStrongNameToken = IntPtr.Zero;
				pcbStrongNameToken = 0;
				return false;
			}
			return true;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00004CA0 File Offset: 0x00002EA0
		[SecurityCritical]
		public static bool StrongNameSignatureSize(byte[] bPublicKeyBlob, int cbPublicKeyBlob, out int pcbSize)
		{
			int num = StrongNameHelpers.StrongName.StrongNameSignatureSize(bPublicKeyBlob, cbPublicKeyBlob, out pcbSize);
			if (num < 0)
			{
				StrongNameHelpers.ts_LastStrongNameHR = num;
				pcbSize = 0;
				return false;
			}
			return true;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00004CCC File Offset: 0x00002ECC
		[SecurityCritical]
		public static bool StrongNameTokenFromPublicKey(byte[] bPublicKeyBlob, int cbPublicKeyBlob, out IntPtr ppbStrongNameToken, out int pcbStrongNameToken)
		{
			int num = StrongNameHelpers.StrongName.StrongNameTokenFromPublicKey(bPublicKeyBlob, cbPublicKeyBlob, out ppbStrongNameToken, out pcbStrongNameToken);
			if (num < 0)
			{
				StrongNameHelpers.ts_LastStrongNameHR = num;
				ppbStrongNameToken = IntPtr.Zero;
				pcbStrongNameToken = 0;
				return false;
			}
			return true;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00004D00 File Offset: 0x00002F00
		[SecurityCritical]
		public static bool StrongNameGetPublicKey(string pwzKeyContainer, byte[] bKeyBlob, int cbKeyBlob, out IntPtr ppbPublicKeyBlob, out int pcbPublicKeyBlob)
		{
			int num = StrongNameHelpers.StrongName.StrongNameGetPublicKey(pwzKeyContainer, bKeyBlob, cbKeyBlob, out ppbPublicKeyBlob, out pcbPublicKeyBlob);
			if (num < 0)
			{
				StrongNameHelpers.ts_LastStrongNameHR = num;
				ppbPublicKeyBlob = IntPtr.Zero;
				pcbPublicKeyBlob = 0;
				return false;
			}
			return true;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00004D38 File Offset: 0x00002F38
		[SecurityCritical]
		public static bool StrongNameKeyInstall(string pwzKeyContainer, byte[] bKeyBlob, int cbKeyBlob)
		{
			int num = StrongNameHelpers.StrongName.StrongNameKeyInstall(pwzKeyContainer, bKeyBlob, cbKeyBlob);
			if (num < 0)
			{
				StrongNameHelpers.ts_LastStrongNameHR = num;
				return false;
			}
			return true;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00004D60 File Offset: 0x00002F60
		[SecurityCritical]
		public static bool StrongNameSignatureGeneration(string pwzFilePath, string pwzKeyContainer, byte[] bKeyBlob, int cbKeyBlob)
		{
			IntPtr zero = IntPtr.Zero;
			int num = 0;
			return StrongNameHelpers.StrongNameSignatureGeneration(pwzFilePath, pwzKeyContainer, bKeyBlob, cbKeyBlob, ref zero, out num);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00004D84 File Offset: 0x00002F84
		[SecurityCritical]
		public static bool StrongNameSignatureGeneration(string pwzFilePath, string pwzKeyContainer, byte[] bKeyBlob, int cbKeyBlob, ref IntPtr ppbSignatureBlob, out int pcbSignatureBlob)
		{
			int num = StrongNameHelpers.StrongName.StrongNameSignatureGeneration(pwzFilePath, pwzKeyContainer, bKeyBlob, cbKeyBlob, ppbSignatureBlob, out pcbSignatureBlob);
			if (num < 0)
			{
				StrongNameHelpers.ts_LastStrongNameHR = num;
				pcbSignatureBlob = 0;
				return false;
			}
			return true;
		}

		// Token: 0x040001AE RID: 430
		[ThreadStatic]
		private static int ts_LastStrongNameHR;

		// Token: 0x040001AF RID: 431
		[SecurityCritical]
		[ThreadStatic]
		private static IClrStrongName s_StrongName;
	}
}
