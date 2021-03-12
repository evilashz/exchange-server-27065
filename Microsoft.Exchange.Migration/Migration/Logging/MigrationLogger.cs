using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Migration.Logging
{
	// Token: 0x02000078 RID: 120
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class MigrationLogger
	{
		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x0001E919 File Offset: 0x0001CB19
		// (set) Token: 0x060006C1 RID: 1729 RVA: 0x0001E920 File Offset: 0x0001CB20
		internal static Action<string, MigrationEventType, object, string> InMemoryLogger { get; set; }

		// Token: 0x060006C2 RID: 1730 RVA: 0x0001E928 File Offset: 0x0001CB28
		public static void Log(MigrationEventType eventType, Exception exception, string format, params object[] args)
		{
			if (exception != null)
			{
				MigrationLogger.Log(eventType, format + ", exception " + MigrationLogger.GetDiagnosticInfo(exception, null), args);
				return;
			}
			MigrationLogger.Log(eventType, format, args);
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0001E950 File Offset: 0x0001CB50
		public static void Log(MigrationEventType eventType, string format, params object[] args)
		{
			MigrationLogContext migrationLogContext = MigrationLogContext.Current;
			MigrationLogger.Log(migrationLogContext.Source, eventType, migrationLogContext, format, args);
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0001E974 File Offset: 0x0001CB74
		public static void Log(string source, MigrationEventType eventType, object context, string format, params object[] args)
		{
			lock (MigrationLogger.objLock)
			{
				if (MigrationLogger.log != null)
				{
					MigrationLogger.log.Log(source, eventType, context, format, args);
				}
				if (MigrationLogger.InMemoryLogger != null)
				{
					string arg = string.Format(format, args);
					MigrationLogger.InMemoryLogger(source, eventType, context, arg);
				}
			}
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0001E9E4 File Offset: 0x0001CBE4
		public static void Flush()
		{
			lock (MigrationLogger.objLock)
			{
				if (MigrationLogger.log != null)
				{
					MigrationLogger.log.Flush();
				}
			}
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0001EA30 File Offset: 0x0001CC30
		public static void Close()
		{
			lock (MigrationLogger.objLock)
			{
				MigrationLogger.refCount--;
				if (MigrationLogger.refCount <= 0)
				{
					if (MigrationLogger.log != null)
					{
						MigrationLogger.log.Close();
						MigrationLogger.log.Dispose();
						MigrationLogger.log = null;
					}
					MigrationLogger.refCount = 0;
				}
			}
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0001EAA4 File Offset: 0x0001CCA4
		public static string PropertyBagToString(PropertyBag bag)
		{
			MigrationUtil.ThrowOnNullArgument(bag, "bag");
			StringBuilder stringBuilder = new StringBuilder(bag.Count * 128);
			foreach (object obj in bag.Keys)
			{
				PropertyDefinition propertyDefinition = obj as PropertyDefinition;
				if (propertyDefinition != null)
				{
					stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, "[{0}:{1}]", new object[]
					{
						propertyDefinition.Name,
						bag[propertyDefinition]
					}));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0001EB7C File Offset: 0x0001CD7C
		public static string GetDiagnosticInfo(Exception ex, string diagnosticInfo)
		{
			string internalError = null;
			MigrationApplication.RunOperationWithCulture(CultureInfo.InvariantCulture, delegate
			{
				internalError = MigrationLogger.InternalGetDiagnosticInfo(ex, diagnosticInfo);
			});
			return internalError;
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0001EBFC File Offset: 0x0001CDFC
		public static string GetDiagnosticInfo(StackTrace st, string diagnosticInfo)
		{
			MigrationUtil.ThrowOnNullArgument(st, "st");
			MigrationUtil.ThrowOnNullOrEmptyArgument(diagnosticInfo, "diagnosticInfo");
			string internalError = null;
			MigrationApplication.RunOperationWithCulture(CultureInfo.InvariantCulture, delegate
			{
				internalError = MigrationLogger.ConcatAndSanitizeDiagnosticFields(new object[]
				{
					st,
					diagnosticInfo
				});
			});
			return internalError;
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x0001EC60 File Offset: 0x0001CE60
		public static string SanitizeDiagnosticInfo(string diagnosticInfo)
		{
			MigrationUtil.ThrowOnNullOrEmptyArgument(diagnosticInfo, "diagnosticInfo");
			diagnosticInfo = diagnosticInfo.Replace("  ", " ");
			diagnosticInfo = diagnosticInfo.Replace("\n", " ");
			diagnosticInfo = diagnosticInfo.Replace("\r", " ");
			diagnosticInfo = diagnosticInfo.Replace("\t", " ");
			diagnosticInfo = diagnosticInfo.Replace("{", "[");
			diagnosticInfo = diagnosticInfo.Replace("}", "]");
			if (diagnosticInfo.Length > 32768)
			{
				return diagnosticInfo.Substring(0, 32765) + "...";
			}
			return diagnosticInfo;
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0001ED0C File Offset: 0x0001CF0C
		public static void Initialize()
		{
			lock (MigrationLogger.objLock)
			{
				if (MigrationLogger.log == null)
				{
					MigrationLogger.log = new MigrationLog();
				}
				MigrationLogger.refCount++;
			}
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x0001ED64 File Offset: 0x0001CF64
		public static string GetInternalError(Exception ex)
		{
			string result = null;
			MigrationPermanentException ex2 = ex as MigrationPermanentException;
			MigrationTransientException ex3 = ex as MigrationTransientException;
			MigrationDataCorruptionException ex4 = ex as MigrationDataCorruptionException;
			if (ex2 != null)
			{
				result = ex2.InternalError;
			}
			else if (ex3 != null)
			{
				result = ex3.InternalError;
			}
			else if (ex4 != null)
			{
				result = ex4.InternalError;
			}
			return result;
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x0001EDAC File Offset: 0x0001CFAC
		public static string CombineInternalError(string internalError, Exception ex)
		{
			string internalError2 = MigrationLogger.GetInternalError(ex);
			string result = internalError;
			if (!string.IsNullOrEmpty(internalError2))
			{
				result = internalError + " -- " + internalError2;
			}
			return result;
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x0001EDD8 File Offset: 0x0001CFD8
		private static string ExtractMapiDiagnostic(Exception ex)
		{
			string text = null;
			Exception innerException = ex.InnerException;
			int num = 0;
			while (num < 10 && innerException != null)
			{
				MapiPermanentException ex2 = innerException as MapiPermanentException;
				MapiRetryableException ex3 = innerException as MapiRetryableException;
				string text2 = innerException.Message;
				if (ex2 != null)
				{
					text2 = ex2.DiagCtx.ToCompactString();
				}
				else if (ex3 != null)
				{
					text2 = ex3.DiagCtx.ToCompactString();
				}
				if (!string.IsNullOrEmpty(text2))
				{
					if (text == null)
					{
						text = string.Format(CultureInfo.InvariantCulture, "InnerException:{0}:{1}", new object[]
						{
							innerException.GetType().Name,
							text2
						});
					}
					else
					{
						text = string.Format(CultureInfo.InvariantCulture, "{0} InnerException:{1}:{2}", new object[]
						{
							text,
							innerException.GetType().Name,
							text2
						});
					}
				}
				num++;
				innerException = innerException.InnerException;
			}
			if (text != null)
			{
				text = text.Replace("  ", " ");
			}
			return text;
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0001EECC File Offset: 0x0001D0CC
		private static string InternalGetDiagnosticInfo(Exception ex, string internalContext)
		{
			string internalError = MigrationLogger.GetInternalError(ex);
			return MigrationLogger.ConcatAndSanitizeDiagnosticFields(new object[]
			{
				ex.StackTrace,
				internalContext,
				internalError,
				ex.GetType(),
				ex.Message,
				MigrationLogger.ExtractMapiDiagnostic(ex),
				ex.InnerException
			});
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0001EF24 File Offset: 0x0001D124
		private static string ConcatAndSanitizeDiagnosticFields(params object[] sourceFields)
		{
			MigrationUtil.ThrowOnCollectionEmptyArgument(sourceFields, "sourceFields");
			int num = 0;
			List<string> list = new List<string>(sourceFields.Length);
			foreach (object obj in sourceFields)
			{
				if (obj != null)
				{
					string text = obj.ToString();
					int num2 = 32768 - num;
					if (text.Length >= num2)
					{
						if (num2 > 0)
						{
							list.Add(text.Substring(0, num2));
						}
						num = 32768;
						break;
					}
					num += text.Length;
					list.Add(text);
				}
			}
			StringBuilder stringBuilder = new StringBuilder(num);
			foreach (string text2 in list)
			{
				stringBuilder.Append(text2.Replace('|', '_'));
				stringBuilder.Append('|');
			}
			return MigrationLogger.SanitizeDiagnosticInfo(stringBuilder.ToString());
		}

		// Token: 0x040002D4 RID: 724
		public const char FieldDelimiter = '|';

		// Token: 0x040002D5 RID: 725
		private const int MaxDiagnosticInfoLength = 32768;

		// Token: 0x040002D6 RID: 726
		private static readonly object objLock = new object();

		// Token: 0x040002D7 RID: 727
		private static MigrationLog log;

		// Token: 0x040002D8 RID: 728
		private static int refCount;
	}
}
