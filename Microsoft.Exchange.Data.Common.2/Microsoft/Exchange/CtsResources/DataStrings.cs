using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;

namespace Microsoft.Exchange.CtsResources
{
	// Token: 0x02000019 RID: 25
	internal static class DataStrings
	{
		// Token: 0x060000FB RID: 251 RVA: 0x00005080 File Offset: 0x00003280
		static DataStrings()
		{
			DataStrings.stringIDs.Add(1256740561U, "ErrorPathCanNotBeRoot");
			DataStrings.stringIDs.Add(2058499689U, "ConstraintViolationNoLeadingOrTrailingWhitespace");
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000050E3 File Offset: 0x000032E3
		public static string ErrorInvalidFullyQualifiedFileName(string path)
		{
			return string.Format(DataStrings.ResourceManager.GetString("ErrorInvalidFullyQualifiedFileName"), path);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000050FA File Offset: 0x000032FA
		public static string ErrorFilePathMismatchExpectedExtension(string path, string extension)
		{
			return string.Format(DataStrings.ResourceManager.GetString("ErrorFilePathMismatchExpectedExtension"), path, extension);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005112 File Offset: 0x00003312
		public static string ErrorEdbFileCannotBeUncPath(string pathName)
		{
			return string.Format(DataStrings.ResourceManager.GetString("ErrorEdbFileCannotBeUncPath"), pathName);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005129 File Offset: 0x00003329
		public static string ErrorUncPathMustUseServerName(string path)
		{
			return string.Format(DataStrings.ResourceManager.GetString("ErrorUncPathMustUseServerName"), path);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005140 File Offset: 0x00003340
		public static string ErrorEdbFilePathCannotConvert(string pathName)
		{
			return string.Format(DataStrings.ResourceManager.GetString("ErrorEdbFilePathCannotConvert"), pathName);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005157 File Offset: 0x00003357
		public static string ErrorEdbFileNameTooLong(string fileName)
		{
			return string.Format(DataStrings.ResourceManager.GetString("ErrorEdbFileNameTooLong"), fileName);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000516E File Offset: 0x0000336E
		public static string ErrorUncPathMustBeUncPath(string path)
		{
			return string.Format(DataStrings.ResourceManager.GetString("ErrorUncPathMustBeUncPath"), path);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005185 File Offset: 0x00003385
		public static string ErrorEdbFileCannotBeTmp(string pathName)
		{
			return string.Format(DataStrings.ResourceManager.GetString("ErrorEdbFileCannotBeTmp"), pathName);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0000519C File Offset: 0x0000339C
		public static string ErrorLocalLongFullPathTooLong(string path)
		{
			return string.Format(DataStrings.ResourceManager.GetString("ErrorLocalLongFullPathTooLong"), path);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000051B3 File Offset: 0x000033B3
		public static string ErrorUncPathMustBeUncPathOnly(string path)
		{
			return string.Format(DataStrings.ResourceManager.GetString("ErrorUncPathMustBeUncPathOnly"), path);
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000106 RID: 262 RVA: 0x000051CA File Offset: 0x000033CA
		public static string ErrorPathCanNotBeRoot
		{
			get
			{
				return DataStrings.ResourceManager.GetString("ErrorPathCanNotBeRoot");
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000051DB File Offset: 0x000033DB
		public static string ErrorLocalLongFullPathCannotConvert(string pathName)
		{
			return string.Format(DataStrings.ResourceManager.GetString("ErrorLocalLongFullPathCannotConvert"), pathName);
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000108 RID: 264 RVA: 0x000051F2 File Offset: 0x000033F2
		public static string ConstraintViolationNoLeadingOrTrailingWhitespace
		{
			get
			{
				return DataStrings.ResourceManager.GetString("ConstraintViolationNoLeadingOrTrailingWhitespace");
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005203 File Offset: 0x00003403
		public static string ErrorUncPathTooLong(string path)
		{
			return string.Format(DataStrings.ResourceManager.GetString("ErrorUncPathTooLong"), path);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000521A File Offset: 0x0000341A
		public static string ErrorLongPathCannotConvert(string pathName)
		{
			return string.Format(DataStrings.ResourceManager.GetString("ErrorLongPathCannotConvert"), pathName);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00005231 File Offset: 0x00003431
		public static string ErrorInvalidExtension(string extension)
		{
			return string.Format(DataStrings.ResourceManager.GetString("ErrorInvalidExtension"), extension);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00005248 File Offset: 0x00003448
		public static string ErrorLocalLongFullAsciiPathCannotConvert(string pathName)
		{
			return string.Format(DataStrings.ResourceManager.GetString("ErrorLocalLongFullAsciiPathCannotConvert"), pathName);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000525F File Offset: 0x0000345F
		public static string ErrorStmFilePathCannotConvert(string pathName)
		{
			return string.Format(DataStrings.ResourceManager.GetString("ErrorStmFilePathCannotConvert"), pathName);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00005276 File Offset: 0x00003476
		public static string GetLocalizedString(DataStrings.IDs key)
		{
			return DataStrings.ResourceManager.GetString(DataStrings.stringIDs[(uint)key]);
		}

		// Token: 0x040000A5 RID: 165
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(2);

		// Token: 0x040000A6 RID: 166
		private static ResourceManager ResourceManager = new ResourceManager("Microsoft.Exchange.CtsResources.DataStrings", typeof(DataStrings).GetTypeInfo().Assembly);

		// Token: 0x0200001A RID: 26
		public enum IDs : uint
		{
			// Token: 0x040000A8 RID: 168
			ErrorPathCanNotBeRoot = 1256740561U,
			// Token: 0x040000A9 RID: 169
			ConstraintViolationNoLeadingOrTrailingWhitespace = 2058499689U
		}

		// Token: 0x0200001B RID: 27
		private enum ParamIDs
		{
			// Token: 0x040000AB RID: 171
			ErrorInvalidFullyQualifiedFileName,
			// Token: 0x040000AC RID: 172
			ErrorFilePathMismatchExpectedExtension,
			// Token: 0x040000AD RID: 173
			ErrorEdbFileCannotBeUncPath,
			// Token: 0x040000AE RID: 174
			ErrorUncPathMustUseServerName,
			// Token: 0x040000AF RID: 175
			ErrorEdbFilePathCannotConvert,
			// Token: 0x040000B0 RID: 176
			ErrorEdbFileNameTooLong,
			// Token: 0x040000B1 RID: 177
			ErrorUncPathMustBeUncPath,
			// Token: 0x040000B2 RID: 178
			ErrorEdbFileCannotBeTmp,
			// Token: 0x040000B3 RID: 179
			ErrorLocalLongFullPathTooLong,
			// Token: 0x040000B4 RID: 180
			ErrorUncPathMustBeUncPathOnly,
			// Token: 0x040000B5 RID: 181
			ErrorLocalLongFullPathCannotConvert,
			// Token: 0x040000B6 RID: 182
			ErrorUncPathTooLong,
			// Token: 0x040000B7 RID: 183
			ErrorLongPathCannotConvert,
			// Token: 0x040000B8 RID: 184
			ErrorInvalidExtension,
			// Token: 0x040000B9 RID: 185
			ErrorLocalLongFullAsciiPathCannotConvert,
			// Token: 0x040000BA RID: 186
			ErrorStmFilePathCannotConvert
		}
	}
}
