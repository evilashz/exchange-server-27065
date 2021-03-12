using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.Assistants
{
	// Token: 0x02000010 RID: 16
	public abstract class AssistantLogEntryBase
	{
		// Token: 0x06000099 RID: 153 RVA: 0x000048F4 File Offset: 0x00002AF4
		public AssistantLogEntryBase()
		{
			this.fieldsMap = new Dictionary<string, MethodInfo>();
			this.extendedPropertiesMap = new Dictionary<string, object>();
			this.exceptions = new List<AssistantLogEntryBase.ExceptionInfo>();
			PropertyInfo[] properties = base.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
			foreach (PropertyInfo propertyInfo in properties)
			{
				LogFieldAttribute[] array2 = (LogFieldAttribute[])propertyInfo.GetCustomAttributes(typeof(LogFieldAttribute), false);
				if (array2.Length > 0)
				{
					string name = array2[0].Name;
					string name2 = propertyInfo.Name;
					if (string.IsNullOrEmpty(name))
					{
						this.fieldsMap.Add(name2, propertyInfo.GetGetMethod());
					}
					else if (this.fieldsMap.ContainsKey(name))
					{
						this.fieldsMap.Add(name2, propertyInfo.GetGetMethod());
					}
					else
					{
						this.fieldsMap.Add(name, propertyInfo.GetGetMethod());
					}
				}
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004A08 File Offset: 0x00002C08
		internal void AddExceptionToLog(Exception ex)
		{
			if (ex != null)
			{
				string key = ex.GetType().Name;
				string diagnosticContext = AssistantsLog.GetDiagnosticContext(ex);
				int num = this.exceptions.FindIndex((AssistantLogEntryBase.ExceptionInfo x) => x.Name == key && x.DiagnosticContext == diagnosticContext);
				string message = "No exception message";
				if (!string.IsNullOrEmpty(ex.Message))
				{
					message = ex.Message;
				}
				if (num == -1)
				{
					this.exceptions.Add(new AssistantLogEntryBase.ExceptionInfo
					{
						Name = key,
						InnerException = ((ex.InnerException != null) ? ex.InnerException.GetType().Name : "NoInnerException"),
						Message = message,
						StackTrace = ex.StackTrace,
						Count = 1,
						DiagnosticContext = diagnosticContext
					});
					return;
				}
				AssistantLogEntryBase.ExceptionInfo exceptionInfo = this.exceptions[num];
				exceptionInfo.Count++;
				exceptionInfo.InnerException = ((ex.InnerException != null) ? ex.InnerException.GetType().Name : "NoInnerException");
				exceptionInfo.Message = message;
				exceptionInfo.StackTrace = ex.StackTrace;
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004B38 File Offset: 0x00002D38
		public virtual List<KeyValuePair<string, object>> FormatCustomData()
		{
			List<KeyValuePair<string, object>> list = new List<KeyValuePair<string, object>>();
			foreach (KeyValuePair<string, MethodInfo> keyValuePair in this.fieldsMap)
			{
				object value = string.Format("{0}", keyValuePair.Value.Invoke(this, null));
				list.Add(new KeyValuePair<string, object>(keyValuePair.Key, value));
			}
			foreach (KeyValuePair<string, object> keyValuePair2 in this.extendedPropertiesMap)
			{
				list.Add(new KeyValuePair<string, object>(keyValuePair2.Key, keyValuePair2.Value));
			}
			string value2 = this.FormatExceptionListToString();
			list.Add(new KeyValuePair<string, object>("EX", value2));
			return list;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004C28 File Offset: 0x00002E28
		protected string FormatDictionaryToString(Dictionary<string, int> dictionary)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, int> keyValuePair in dictionary)
			{
				stringBuilder.Append(string.Format("{0}_{1}|", keyValuePair.Key, keyValuePair.Value));
			}
			return stringBuilder.ToString().TrimEnd(new char[]
			{
				'|'
			});
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004CB4 File Offset: 0x00002EB4
		protected string FormatExceptionListToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (AssistantLogEntryBase.ExceptionInfo exceptionInfo in this.exceptions)
			{
				stringBuilder.Append(string.Format("{0}_{1}_{2}_{3}_{4}_{5}|", new object[]
				{
					exceptionInfo.Name,
					exceptionInfo.InnerException,
					exceptionInfo.Count,
					SpecialCharacters.SanitizeForLogging(exceptionInfo.Message),
					SpecialCharacters.SanitizeForLogging(exceptionInfo.StackTrace),
					exceptionInfo.DiagnosticContext
				}));
			}
			return stringBuilder.ToString().TrimEnd(new char[]
			{
				'|'
			});
		}

		// Token: 0x040000DB RID: 219
		private readonly Dictionary<string, MethodInfo> fieldsMap;

		// Token: 0x040000DC RID: 220
		protected Dictionary<string, object> extendedPropertiesMap;

		// Token: 0x040000DD RID: 221
		private List<AssistantLogEntryBase.ExceptionInfo> exceptions;

		// Token: 0x02000011 RID: 17
		private class ExceptionInfo
		{
			// Token: 0x17000005 RID: 5
			// (get) Token: 0x0600009E RID: 158 RVA: 0x00004D80 File Offset: 0x00002F80
			// (set) Token: 0x0600009F RID: 159 RVA: 0x00004D88 File Offset: 0x00002F88
			public string Name { get; set; }

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x060000A0 RID: 160 RVA: 0x00004D91 File Offset: 0x00002F91
			// (set) Token: 0x060000A1 RID: 161 RVA: 0x00004D99 File Offset: 0x00002F99
			public string InnerException { get; set; }

			// Token: 0x17000007 RID: 7
			// (get) Token: 0x060000A2 RID: 162 RVA: 0x00004DA2 File Offset: 0x00002FA2
			// (set) Token: 0x060000A3 RID: 163 RVA: 0x00004DAA File Offset: 0x00002FAA
			public string Message { get; set; }

			// Token: 0x17000008 RID: 8
			// (get) Token: 0x060000A4 RID: 164 RVA: 0x00004DB3 File Offset: 0x00002FB3
			// (set) Token: 0x060000A5 RID: 165 RVA: 0x00004DBB File Offset: 0x00002FBB
			public string StackTrace { get; set; }

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x060000A6 RID: 166 RVA: 0x00004DC4 File Offset: 0x00002FC4
			// (set) Token: 0x060000A7 RID: 167 RVA: 0x00004DCC File Offset: 0x00002FCC
			public int Count { get; set; }

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x060000A8 RID: 168 RVA: 0x00004DD5 File Offset: 0x00002FD5
			// (set) Token: 0x060000A9 RID: 169 RVA: 0x00004DDD File Offset: 0x00002FDD
			public string DiagnosticContext { get; set; }
		}
	}
}
