using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Cluster
{
	// Token: 0x02000426 RID: 1062
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class HaRpcExceptionHelper
	{
		// Token: 0x06002FA4 RID: 12196 RVA: 0x000C3A05 File Offset: 0x000C1C05
		public static bool TryGetExceptionOrInnerOfType<TException>(this Exception ex, out Exception convertedException) where TException : Exception
		{
			convertedException = null;
			if (ex is TException)
			{
				convertedException = ex;
				return true;
			}
			return ex.TryGetInnerExceptionOfType(out convertedException);
		}

		// Token: 0x06002FA5 RID: 12197 RVA: 0x000C3A20 File Offset: 0x000C1C20
		public static bool TryGetInnerExceptionOfType<TException>(this Exception ex, out Exception innerException) where TException : Exception
		{
			innerException = null;
			while (ex.InnerException != null)
			{
				Exception innerException2 = ex.InnerException;
				if (innerException2 is TException)
				{
					innerException = innerException2;
					return true;
				}
				ex = ex.InnerException;
			}
			return false;
		}

		// Token: 0x06002FA6 RID: 12198 RVA: 0x000C3A5C File Offset: 0x000C1C5C
		public static bool TryGetTypedInnerException<TException>(this Exception ex, out TException innerException) where TException : Exception
		{
			innerException = default(TException);
			while (ex.InnerException != null)
			{
				TException ex2 = ex.InnerException as TException;
				if (ex2 != null)
				{
					innerException = ex2;
					return true;
				}
				ex = ex.InnerException;
			}
			return false;
		}

		// Token: 0x06002FA7 RID: 12199 RVA: 0x000C3AA8 File Offset: 0x000C1CA8
		internal static string GetCompleteExceptionMessage(Exception ex, bool fCalledFromToString)
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			bool flag = false;
			while (ex != null)
			{
				if (flag)
				{
					stringBuilder.Append(" ---> ");
				}
				else
				{
					flag = true;
				}
				string text = string.Empty;
				IHaRpcServerBaseExceptionInternal haRpcServerBaseExceptionInternal = ex as IHaRpcServerBaseExceptionInternal;
				if (haRpcServerBaseExceptionInternal != null)
				{
					text = haRpcServerBaseExceptionInternal.MessageInternal;
				}
				else
				{
					text = ex.Message;
				}
				if (fCalledFromToString)
				{
					stringBuilder.AppendFormat("{0}: {1}", ex.GetType(), text);
				}
				else
				{
					stringBuilder.Append(text);
				}
				ex = ex.InnerException;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002FA8 RID: 12200 RVA: 0x000C3B28 File Offset: 0x000C1D28
		internal static string GetFullString(IHaRpcServerBaseException ex, IHaRpcServerBaseExceptionInternal exInternal)
		{
			StringBuilder stringBuilder = new StringBuilder(2048);
			stringBuilder.AppendFormat("{0}: {1}", ex.GetType(), exInternal.MessageInternal);
			if (ex.InnerException != null)
			{
				stringBuilder.Append(" ---> ");
				stringBuilder.Append(ex.InnerException.ToString());
				stringBuilder.AppendLine();
				stringBuilder.Append(string.Format("   --- End of inner exception stack trace ({0}) ---", ex.InnerException.GetType()));
			}
			if (!string.IsNullOrEmpty(ex.OriginatingStackTrace))
			{
				stringBuilder.AppendLine();
				stringBuilder.Append(ex.OriginatingStackTrace);
			}
			if (!string.IsNullOrEmpty(ex.OriginatingServer))
			{
				stringBuilder.AppendLine();
				stringBuilder.Append(string.Format("   --- End of stack trace on server ({0}) ---", ex.OriginatingServer));
			}
			if (!string.IsNullOrEmpty(ex.StackTrace))
			{
				stringBuilder.AppendLine();
				stringBuilder.Append(ex.StackTrace);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002FA9 RID: 12201 RVA: 0x000C3C14 File Offset: 0x000C1E14
		internal static string GetOriginatingServerString(string originatingServer, string databaseName)
		{
			if (string.IsNullOrEmpty(originatingServer))
			{
				return string.Empty;
			}
			if (string.IsNullOrEmpty(databaseName))
			{
				return string.Format(" [{0}: {1}]", ServerStrings.OriginatingServer, originatingServer);
			}
			return string.Format(" [{0}: {1}, {2}: {3}]", new object[]
			{
				ServerStrings.Database,
				databaseName,
				ServerStrings.OriginatingServer,
				originatingServer
			});
		}

		// Token: 0x06002FAA RID: 12202 RVA: 0x000C3C84 File Offset: 0x000C1E84
		public static string AppendLastErrorString(string message, string lastError)
		{
			string arg = string.Format(" [{0}: {1}]", ServerStrings.LastErrorMessage, lastError);
			return string.Format("{0}{1}", message, arg);
		}

		// Token: 0x04001A03 RID: 6659
		internal const string ClientSideExceptionFormatToString = "{0}: {1}";

		// Token: 0x04001A04 RID: 6660
		internal const string ClientSideInnerSeperator = " ---> ";

		// Token: 0x04001A05 RID: 6661
		internal const string OriginatingServerFormatString = " [{0}: {1}]";

		// Token: 0x04001A06 RID: 6662
		internal const string OriginatingServerWithDbFormatString = " [{0}: {1}, {2}: {3}]";

		// Token: 0x04001A07 RID: 6663
		internal const string LastErrorFormatString = " [{0}: {1}]";

		// Token: 0x04001A08 RID: 6664
		internal const string HaExceptionMessageFormatString = "{0}{1}";

		// Token: 0x04001A09 RID: 6665
		internal const string ServerSideStackTraceFormatString = "   --- End of stack trace on server ({0}) ---";

		// Token: 0x04001A0A RID: 6666
		internal const string InnerExceptionStackTraceFormatString = "   --- End of inner exception stack trace ({0}) ---";
	}
}
