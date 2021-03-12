using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000F36 RID: 3894
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class AuditingOpticsLogger
	{
		// Token: 0x060085DE RID: 34270 RVA: 0x0024AFD4 File Offset: 0x002491D4
		static AuditingOpticsLogger()
		{
			AuditingOpticsLogger.auditingOpticsLoggerInstanceHook = Hookable<IAuditingOpticsLoggerInstance>.Create(true, null);
		}

		// Token: 0x060085DF RID: 34271 RVA: 0x0024B004 File Offset: 0x00249204
		private static void Stop(AuditingOpticsLoggerType loggerType)
		{
			EnumValidator.AssertValid<AuditingOpticsLoggerType>(loggerType);
			lock (AuditingOpticsLogger.instanceLock)
			{
				AuditingOpticsLoggerInstance auditingOpticsLoggerInstance = AuditingOpticsLogger.instances[(int)loggerType];
				if (auditingOpticsLoggerInstance != null)
				{
					auditingOpticsLoggerInstance.Stop();
					AuditingOpticsLogger.instances[(int)loggerType] = null;
				}
			}
		}

		// Token: 0x060085E0 RID: 34272 RVA: 0x0024B05C File Offset: 0x0024925C
		internal static IAuditingOpticsLoggerInstance GetInstance(AuditingOpticsLoggerType loggerType)
		{
			EnumValidator.AssertValid<AuditingOpticsLoggerType>(loggerType);
			if (AuditingOpticsLogger.auditingOpticsLoggerInstanceHook.Value != null)
			{
				return AuditingOpticsLogger.auditingOpticsLoggerInstanceHook.Value;
			}
			IAuditingOpticsLoggerInstance result;
			lock (AuditingOpticsLogger.instanceLock)
			{
				AuditingOpticsLoggerInstance auditingOpticsLoggerInstance = AuditingOpticsLogger.instances[(int)loggerType];
				if (auditingOpticsLoggerInstance == null)
				{
					auditingOpticsLoggerInstance = new AuditingOpticsLoggerInstance(loggerType);
					AuditingOpticsLogger.instances[(int)loggerType] = auditingOpticsLoggerInstance;
				}
				result = auditingOpticsLoggerInstance;
			}
			return result;
		}

		// Token: 0x060085E1 RID: 34273 RVA: 0x0024B0D0 File Offset: 0x002492D0
		internal static IDisposable SetLoggerInstanceTestHook(IAuditingOpticsLoggerInstance loggerInstance)
		{
			return AuditingOpticsLogger.auditingOpticsLoggerInstanceHook.SetTestHook(loggerInstance);
		}

		// Token: 0x060085E2 RID: 34274 RVA: 0x0024B0DD File Offset: 0x002492DD
		public static void LogAuditOpticsEntry(AuditingOpticsLoggerType loggerType, List<KeyValuePair<string, object>> customData)
		{
			EnumValidator.AssertValid<AuditingOpticsLoggerType>(loggerType);
			AuditingOpticsLogger.GetInstance(loggerType).InternalLogRow(customData);
		}

		// Token: 0x060085E3 RID: 34275 RVA: 0x0024B0F4 File Offset: 0x002492F4
		public static List<KeyValuePair<string, object>> GetLogColumns<T>(T data, LogTableSchema<T>[] schema)
		{
			List<KeyValuePair<string, object>> list = new List<KeyValuePair<string, object>>(schema.Length);
			foreach (LogTableSchema<T> logTableSchema in schema)
			{
				string text = logTableSchema.Getter(data);
				if (text == null)
				{
					text = string.Empty;
				}
				list.Add(new KeyValuePair<string, object>(logTableSchema.ColumnName, text));
			}
			return list;
		}

		// Token: 0x17002378 RID: 9080
		// (get) Token: 0x060085E4 RID: 34276 RVA: 0x0024B14C File Offset: 0x0024934C
		public static IExceptionLogFormatter DefaultExceptionFormatter
		{
			get
			{
				return AuditingOpticsLogger.defaultFormatter;
			}
		}

		// Token: 0x060085E5 RID: 34277 RVA: 0x0024B153 File Offset: 0x00249353
		public static string GetExceptionNamesForTrace(Exception exception)
		{
			return AuditingOpticsLogger.GetExceptionNamesForTrace(exception, AuditingOpticsLogger.DefaultExceptionFormatter);
		}

		// Token: 0x060085E6 RID: 34278 RVA: 0x0024B160 File Offset: 0x00249360
		public static string GetExceptionNamesForTrace(Exception exception, IExceptionLogFormatter formatter)
		{
			if (exception == null)
			{
				return string.Empty;
			}
			if (formatter == null)
			{
				formatter = AuditingOpticsLogger.DefaultExceptionFormatter;
			}
			StringBuilder stringBuilder = null;
			do
			{
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder(256);
				}
				else
				{
					stringBuilder.Append("+");
				}
				string value = formatter.FormatException(exception);
				if (string.IsNullOrEmpty(value))
				{
					value = AuditingOpticsLogger.DefaultExceptionFormatter.FormatException(exception);
				}
				stringBuilder.Append(value);
				exception = exception.InnerException;
			}
			while (exception != null);
			return stringBuilder.ToString();
		}

		// Token: 0x060085E7 RID: 34279 RVA: 0x0024B1D4 File Offset: 0x002493D4
		public static string GetDiagnosticContextFromException(Exception exception)
		{
			string result = string.Empty;
			for (Exception ex = exception; ex != null; ex = ex.InnerException)
			{
				DiagnosticContext diagnosticContext = null;
				MapiPermanentException ex2 = ex as MapiPermanentException;
				MapiRetryableException ex3 = ex as MapiRetryableException;
				if (ex2 != null)
				{
					diagnosticContext = ex2.DiagCtx;
				}
				else if (ex3 != null)
				{
					diagnosticContext = ex3.DiagCtx;
				}
				if (diagnosticContext != null)
				{
					result = string.Format("[e::{0}]", diagnosticContext.ToCompactString());
					break;
				}
			}
			return result;
		}

		// Token: 0x060085E8 RID: 34280 RVA: 0x0024B238 File Offset: 0x00249438
		public static string GetDiagnosticContextFromThread()
		{
			if (!DiagnosticContext.HasData)
			{
				return string.Empty;
			}
			byte[] array = DiagnosticContext.PackInfo();
			byte[] array2 = new byte[array.Length + 6];
			int num = 0;
			ExBitConverter.Write(0, array2, num);
			num += 2;
			ExBitConverter.Write((uint)array.Length, array2, num);
			num += 4;
			Array.Copy(array, 0, array2, num, array.Length);
			return string.Format("[diag::{0}]", Convert.ToBase64String(array2));
		}

		// Token: 0x060085E9 RID: 34281 RVA: 0x0024B2A0 File Offset: 0x002494A0
		public static string GetDiagnosticContext(Exception exception)
		{
			string result = string.Empty;
			if (exception != null)
			{
				result = string.Format("{0}{1}", AuditingOpticsLogger.GetDiagnosticContextFromThread(), AuditingOpticsLogger.GetDiagnosticContextFromException(exception));
			}
			return result;
		}

		// Token: 0x040059AF RID: 22959
		private static AuditingOpticsLoggerInstance[] instances = new AuditingOpticsLoggerInstance[4];

		// Token: 0x040059B0 RID: 22960
		private static readonly object instanceLock = new object();

		// Token: 0x040059B1 RID: 22961
		private static Hookable<IAuditingOpticsLoggerInstance> auditingOpticsLoggerInstanceHook;

		// Token: 0x040059B2 RID: 22962
		private static readonly IExceptionLogFormatter defaultFormatter = new AuditingOpticsLogger._DefaultExceptionFormatter();

		// Token: 0x02000F37 RID: 3895
		private class _DefaultExceptionFormatter : IExceptionLogFormatter
		{
			// Token: 0x060085EA RID: 34282 RVA: 0x0024B2CD File Offset: 0x002494CD
			public string FormatException(Exception exception)
			{
				if (exception == null)
				{
					return string.Empty;
				}
				return exception.GetType().ToString();
			}
		}
	}
}
