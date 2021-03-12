using System;
using System.Collections;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using Microsoft.Exchange.Management.DDIService;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000554 RID: 1364
	public static class EcpTraceExtensions
	{
		// Token: 0x06003FD0 RID: 16336 RVA: 0x000C0B20 File Offset: 0x000BED20
		public static string ToTraceString(this PSCommand command)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (Command command2 in command.Commands)
			{
				stringBuilder.Append(command2.CommandText);
				stringBuilder.Append(" ");
				foreach (CommandParameter commandParameter in command2.Parameters)
				{
					string format = (commandParameter.Value != null && commandParameter.Value.GetType() == typeof(bool)) ? "-{0}:{1} " : "-{0} {1} ";
					stringBuilder.AppendFormat(format, commandParameter.Name, EcpTraceExtensions.FormatParameterValue(commandParameter.Value));
				}
				stringBuilder.Append("\n");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003FD1 RID: 16337 RVA: 0x000C0C28 File Offset: 0x000BEE28
		internal static string FormatParameterValue(object value)
		{
			if (value is IDictionary)
			{
				return (value as IDictionary).ToTraceString();
			}
			if (value is IEnumerable && !(value is string))
			{
				return (value as IEnumerable).ToTraceString();
			}
			return EcpTraceExtensions.FormatNonListParameterValue(value);
		}

		// Token: 0x06003FD2 RID: 16338 RVA: 0x000C0C60 File Offset: 0x000BEE60
		private static string FormatNonListParameterValue(object value)
		{
			if (value == null)
			{
				return "$null";
			}
			switch (Type.GetTypeCode(value.GetType()))
			{
			case TypeCode.DBNull:
				return "$null";
			case TypeCode.Boolean:
				if (!(bool)value)
				{
					return "$false";
				}
				return "$true";
			case TypeCode.Char:
				return "[char]'" + value.ToString() + "'";
			case TypeCode.SByte:
			case TypeCode.Byte:
			case TypeCode.Int16:
			case TypeCode.UInt16:
			case TypeCode.Int32:
			case TypeCode.UInt32:
			case TypeCode.Int64:
			case TypeCode.UInt64:
			case TypeCode.Single:
			case TypeCode.Double:
			case TypeCode.Decimal:
				return value.ToString();
			default:
			{
				string text;
				if (value is string)
				{
					text = (value as string);
				}
				else if (value is Identity)
				{
					text = (value as Identity).RawIdentity;
				}
				else
				{
					text = value.ToString();
					if (text == value.GetType().FullName)
					{
						return "[" + text + "]";
					}
				}
				if (text != null)
				{
					return "'" + text.Replace("'", "''") + "'";
				}
				return "$null";
			}
			}
		}

		// Token: 0x06003FD3 RID: 16339 RVA: 0x000C0D75 File Offset: 0x000BEF75
		public static EcpTraceFormatter<PSCommand> GetTraceFormatter(this PSCommand command)
		{
			return new EcpTraceFormatter<PSCommand>(command);
		}

		// Token: 0x06003FD4 RID: 16340 RVA: 0x000C0D80 File Offset: 0x000BEF80
		public static string ToTraceString(this ErrorRecord[] records)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (ErrorRecord errorRecord in records)
			{
				stringBuilder.AppendLine(errorRecord.Exception.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003FD5 RID: 16341 RVA: 0x000C0DC0 File Offset: 0x000BEFC0
		public static string ToLogString(this ErrorRecord[] records)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < records.Length; i++)
			{
				if (i != 0)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(records[i].Exception.GetType().ToString());
				stringBuilder.Append(":");
				stringBuilder.Append(records[i].Exception.GetFullMessage().ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003FD6 RID: 16342 RVA: 0x000C0E34 File Offset: 0x000BF034
		public static EcpTraceFormatter<ErrorRecord[]> GetTraceFormatter(this ErrorRecord[] records)
		{
			return new EcpTraceFormatter<ErrorRecord[]>(records);
		}

		// Token: 0x06003FD7 RID: 16343 RVA: 0x000C0E3C File Offset: 0x000BF03C
		public static string ToTraceString(this PowerShellResults<JsonDictionary<object>> results)
		{
			return results.ToJsonString(DDIService.KnownTypes.Value);
		}

		// Token: 0x06003FD8 RID: 16344 RVA: 0x000C0E50 File Offset: 0x000BF050
		public static string ToTraceString(this IDictionary collection)
		{
			if (collection == null)
			{
				return "$null";
			}
			StringBuilder stringBuilder = new StringBuilder("@{");
			foreach (object obj in collection)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				stringBuilder.Append(string.Format("{0}={1},", EcpTraceExtensions.FormatNonListParameterValue(dictionaryEntry.Key), EcpTraceExtensions.FormatNonListParameterValue(dictionaryEntry.Value)));
			}
			if (stringBuilder[stringBuilder.Length - 1] == ',')
			{
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x06003FD9 RID: 16345 RVA: 0x000C0F10 File Offset: 0x000BF110
		public static string ToTraceString(this IEnumerable pipeline)
		{
			if (pipeline == null)
			{
				return "$null";
			}
			if (pipeline is string)
			{
				return pipeline as string;
			}
			StringBuilder stringBuilder = new StringBuilder("@(");
			foreach (object value in pipeline)
			{
				stringBuilder.Append(EcpTraceExtensions.FormatNonListParameterValue(value) + ",");
			}
			if (stringBuilder[stringBuilder.Length - 1] == ',')
			{
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x06003FDA RID: 16346 RVA: 0x000C0FC8 File Offset: 0x000BF1C8
		public static EcpTraceFormatter<Exception> GetTraceFormatter(this Exception exception)
		{
			return new EcpTraceFormatter<Exception>(exception);
		}
	}
}
