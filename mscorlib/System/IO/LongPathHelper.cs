﻿using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Win32;

namespace System.IO
{
	// Token: 0x02000198 RID: 408
	internal class LongPathHelper
	{
		// Token: 0x060018D7 RID: 6359 RVA: 0x00051128 File Offset: 0x0004F328
		[SecurityCritical]
		internal unsafe static string Normalize(string path, uint maxPathLength, bool checkInvalidCharacters, bool expandShortPaths)
		{
			StringBuffer stringBuffer;
			if ((stringBuffer = LongPathHelper.t_fullPathBuffer) == null)
			{
				stringBuffer = (LongPathHelper.t_fullPathBuffer = new StringBuffer(260U));
			}
			StringBuffer stringBuffer2 = stringBuffer;
			string result;
			try
			{
				LongPathHelper.GetFullPathName(path, stringBuffer2);
				stringBuffer2.TrimEnd(LongPathHelper.s_trimEndChars);
				if (stringBuffer2.Length >= maxPathLength)
				{
					throw new PathTooLongException();
				}
				bool flag = false;
				bool flag2 = false;
				bool flag3 = stringBuffer2.Length > 1U && stringBuffer2[0U] == '\\' && stringBuffer2[1U] == '\\';
				bool flag4 = PathInternal.IsDevice(stringBuffer2);
				bool flag5 = flag3 && !flag4;
				uint num = flag3 ? 2U : 0U;
				uint num2 = flag3 ? 1U : 0U;
				char* charPointer = stringBuffer2.CharPointer;
				uint num3;
				while (num < stringBuffer2.Length)
				{
					char c = charPointer[(ulong)num * 2UL / 2UL];
					if (c < '?' || c == '\\' || c == '|' || c == '~')
					{
						if (c <= '>')
						{
							if (c != '"' && c != '<' && c != '>')
							{
								goto IL_155;
							}
						}
						else if (c != '\\')
						{
							if (c != '|')
							{
								if (c != '~')
								{
									goto IL_155;
								}
								flag2 = true;
								goto IL_16E;
							}
						}
						else
						{
							num3 = num - num2 - 1U;
							if (num3 > (uint)PathInternal.MaxComponentLength)
							{
								throw new PathTooLongException();
							}
							num2 = num;
							if (flag2)
							{
								if (num3 <= 12U)
								{
									flag = true;
								}
								flag2 = false;
							}
							if (!flag5)
							{
								goto IL_16E;
							}
							if (num == stringBuffer2.Length - 1U)
							{
								throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegalUNC"));
							}
							flag5 = false;
							goto IL_16E;
						}
						if (checkInvalidCharacters)
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPathChars"));
						}
						flag2 = false;
						goto IL_16E;
						IL_155:
						if (checkInvalidCharacters && c < ' ')
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPathChars"));
						}
					}
					IL_16E:
					num += 1U;
				}
				if (flag5)
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegalUNC"));
				}
				num3 = stringBuffer2.Length - num2 - 1U;
				if (num3 > (uint)PathInternal.MaxComponentLength)
				{
					throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
				}
				if (flag2 && num3 <= 12U)
				{
					flag = true;
				}
				if (expandShortPaths && flag)
				{
					result = LongPathHelper.TryExpandShortFileName(stringBuffer2, path);
				}
				else if (stringBuffer2.Length == (uint)path.Length && stringBuffer2.StartsWith(path))
				{
					result = path;
				}
				else
				{
					result = stringBuffer2.ToString();
				}
			}
			finally
			{
				stringBuffer2.Free();
			}
			return result;
		}

		// Token: 0x060018D8 RID: 6360 RVA: 0x00051358 File Offset: 0x0004F558
		[SecurityCritical]
		private unsafe static void GetFullPathName(string path, StringBuffer fullPath)
		{
			int num = PathInternal.PathStartSkip(path);
			fixed (string text = path)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				uint fullPathNameW;
				while ((fullPathNameW = Win32Native.GetFullPathNameW(ptr + num, fullPath.CharCapacity, fullPath.GetHandle(), IntPtr.Zero)) > fullPath.CharCapacity)
				{
					fullPath.EnsureCharCapacity(fullPathNameW);
				}
				if (fullPathNameW == 0U)
				{
					int num2 = Marshal.GetLastWin32Error();
					if (num2 == 0)
					{
						num2 = 161;
					}
					__Error.WinIOError(num2, path);
				}
				fullPath.Length = fullPathNameW;
			}
		}

		// Token: 0x060018D9 RID: 6361 RVA: 0x000513D4 File Offset: 0x0004F5D4
		[SecurityCritical]
		internal static string GetLongPathName(StringBuffer path)
		{
			string result;
			using (StringBuffer stringBuffer = new StringBuffer(path.Length))
			{
				uint longPathNameW;
				while ((longPathNameW = Win32Native.GetLongPathNameW(path.GetHandle(), stringBuffer.GetHandle(), stringBuffer.CharCapacity)) > stringBuffer.CharCapacity)
				{
					stringBuffer.EnsureCharCapacity(longPathNameW);
				}
				if (longPathNameW == 0U)
				{
					LongPathHelper.GetErrorAndThrow(path.ToString());
				}
				stringBuffer.Length = longPathNameW;
				result = stringBuffer.ToString();
			}
			return result;
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x00051454 File Offset: 0x0004F654
		[SecurityCritical]
		internal static string GetLongPathName(string path)
		{
			string result;
			using (StringBuffer stringBuffer = new StringBuffer((uint)path.Length))
			{
				uint longPathNameW;
				while ((longPathNameW = Win32Native.GetLongPathNameW(path, stringBuffer.GetHandle(), stringBuffer.CharCapacity)) > stringBuffer.CharCapacity)
				{
					stringBuffer.EnsureCharCapacity(longPathNameW);
				}
				if (longPathNameW == 0U)
				{
					LongPathHelper.GetErrorAndThrow(path);
				}
				stringBuffer.Length = longPathNameW;
				result = stringBuffer.ToString();
			}
			return result;
		}

		// Token: 0x060018DB RID: 6363 RVA: 0x000514C8 File Offset: 0x0004F6C8
		[SecurityCritical]
		private static void GetErrorAndThrow(string path)
		{
			int num = Marshal.GetLastWin32Error();
			if (num == 0)
			{
				num = 161;
			}
			__Error.WinIOError(num, path);
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x000514EC File Offset: 0x0004F6EC
		[SecuritySafeCritical]
		private static string TryExpandShortFileName(StringBuffer outputBuffer, string originalPath)
		{
			string result;
			using (StringBuffer stringBuffer = new StringBuffer(outputBuffer))
			{
				bool flag = false;
				uint num = outputBuffer.Length - 1U;
				uint num2 = num;
				uint rootLength = PathInternal.GetRootLength(outputBuffer);
				while (!flag)
				{
					uint longPathNameW = Win32Native.GetLongPathNameW(stringBuffer.GetHandle(), outputBuffer.GetHandle(), outputBuffer.CharCapacity);
					if (stringBuffer[num2] == '\0')
					{
						stringBuffer[num2] = '\\';
					}
					if (longPathNameW == 0U)
					{
						int lastWin32Error = Marshal.GetLastWin32Error();
						if (lastWin32Error != 2 && lastWin32Error != 3)
						{
							break;
						}
						num2 -= 1U;
						while (num2 > rootLength && stringBuffer[num2] != '\\')
						{
							num2 -= 1U;
						}
						if (num2 == rootLength)
						{
							break;
						}
						stringBuffer[num2] = '\0';
					}
					else if (longPathNameW > outputBuffer.CharCapacity)
					{
						outputBuffer.EnsureCharCapacity(longPathNameW);
					}
					else
					{
						flag = true;
						outputBuffer.Length = longPathNameW;
						if (num2 < num)
						{
							outputBuffer.Append(stringBuffer, num2, stringBuffer.Length - num2);
						}
					}
				}
				StringBuffer stringBuffer2 = flag ? outputBuffer : stringBuffer;
				if (stringBuffer2.SubstringEquals(originalPath, 0U, -1))
				{
					result = originalPath;
				}
				else
				{
					result = stringBuffer2.ToString();
				}
			}
			return result;
		}

		// Token: 0x040008B4 RID: 2228
		private const int MaxShortName = 12;

		// Token: 0x040008B5 RID: 2229
		private const char LastAnsi = 'ÿ';

		// Token: 0x040008B6 RID: 2230
		private const char Delete = '\u007f';

		// Token: 0x040008B7 RID: 2231
		internal static readonly char[] s_trimEndChars = new char[]
		{
			'\t',
			'\n',
			'\v',
			'\f',
			'\r',
			' ',
			'\u0085',
			'\u00a0'
		};

		// Token: 0x040008B8 RID: 2232
		[ThreadStatic]
		private static StringBuffer t_fullPathBuffer;
	}
}
