using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Win32;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000028 RID: 40
	public class JobQuarantineProvider : IJobQuarantineProvider
	{
		// Token: 0x060001B4 RID: 436 RVA: 0x0000946F File Offset: 0x0000766F
		private JobQuarantineProvider()
		{
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00009477 File Offset: 0x00007677
		public static IJobQuarantineProvider Instance
		{
			get
			{
				return JobQuarantineProvider.hookableInstance.Value;
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00009483 File Offset: 0x00007683
		internal static IDisposable SetTestHook(IJobQuarantineProvider testHook)
		{
			return JobQuarantineProvider.hookableInstance.SetTestHook(testHook);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00009490 File Offset: 0x00007690
		public void QuarantineJob(Guid requestGuid, Exception ex)
		{
			try
			{
				FailureRec failureRec = FailureRec.Create(ex);
				string subkeyName = string.Format(JobQuarantineProvider.KeyNameFormatQuarantinedJob, requestGuid);
				RegistryWriter.Instance.CreateSubKey(Registry.LocalMachine, subkeyName);
				RegistryWriter.Instance.SetValue(Registry.LocalMachine, subkeyName, "FailureType", failureRec.FailureType ?? string.Empty, RegistryValueKind.String);
				RegistryWriter.Instance.SetValue(Registry.LocalMachine, subkeyName, "Message", failureRec.Message ?? string.Empty, RegistryValueKind.String);
				RegistryWriter.Instance.SetValue(Registry.LocalMachine, subkeyName, "StackTrace", failureRec.StackTrace ?? string.Empty, RegistryValueKind.String);
				string dataContext = failureRec.DataContext ?? string.Empty;
				RegistryWriter.Instance.SetValue(Registry.LocalMachine, subkeyName, "DataContext", FailureLog.GetDataContextToPersist(dataContext), RegistryValueKind.String);
				string text = string.Empty;
				if (failureRec.InnerException != null)
				{
					text = failureRec.InnerException.StackTrace;
				}
				RegistryWriter.Instance.SetValue(Registry.LocalMachine, subkeyName, "InnerException", text ?? string.Empty, RegistryValueKind.String);
			}
			catch (ArgumentException)
			{
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000095B0 File Offset: 0x000077B0
		public void UnquarantineJob(Guid requestGuid)
		{
			string subkeyName = string.Format(JobQuarantineProvider.KeyNameFormatQuarantinedJob, requestGuid);
			try
			{
				RegistryWriter.Instance.DeleteSubKeyTree(Registry.LocalMachine, subkeyName);
			}
			catch (ArgumentException)
			{
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000095F4 File Offset: 0x000077F4
		public IDictionary<Guid, FailureRec> GetQuarantinedJobs()
		{
			IRegistryReader instance = RegistryReader.Instance;
			IRegistryWriter instance2 = RegistryWriter.Instance;
			string[] array = null;
			try
			{
				array = instance.GetSubKeyNames(Registry.LocalMachine, JobQuarantineProvider.KeyNameFormatQuarantinedJobRoot);
			}
			catch (ArgumentException)
			{
			}
			if (array == null)
			{
				return new Dictionary<Guid, FailureRec>();
			}
			Dictionary<Guid, FailureRec> dictionary = new Dictionary<Guid, FailureRec>(array.Length);
			string[] array2 = array;
			int i = 0;
			while (i < array2.Length)
			{
				string text = array2[i];
				Guid key = Guid.Empty;
				string subkeyName = Path.Combine(JobQuarantineProvider.KeyNameFormatQuarantinedJobRoot, text);
				try
				{
					key = new Guid(text);
				}
				catch (FormatException)
				{
					try
					{
						instance2.DeleteSubKeyTree(Registry.LocalMachine, subkeyName);
					}
					catch (ArgumentException)
					{
					}
					goto IL_118;
				}
				goto IL_80;
				IL_118:
				i++;
				continue;
				IL_80:
				string value = instance.GetValue<string>(Registry.LocalMachine, subkeyName, "FailureType", string.Empty);
				string value2 = instance.GetValue<string>(Registry.LocalMachine, subkeyName, "Message", string.Empty);
				string value3 = instance.GetValue<string>(Registry.LocalMachine, subkeyName, "StackTrace", string.Empty);
				string value4 = instance.GetValue<string>(Registry.LocalMachine, subkeyName, "DataContext", string.Empty);
				string value5 = instance.GetValue<string>(Registry.LocalMachine, subkeyName, "InnerException", string.Empty);
				FailureRec value6 = FailureRec.Create(value, value2, value3, value4, value5);
				dictionary.Add(key, value6);
				goto IL_118;
			}
			return dictionary;
		}

		// Token: 0x040000B5 RID: 181
		private const string ValueNameStackTrace = "StackTrace";

		// Token: 0x040000B6 RID: 182
		private const string ValueNameDataContext = "DataContext";

		// Token: 0x040000B7 RID: 183
		private const string ValueNameFailureType = "FailureType";

		// Token: 0x040000B8 RID: 184
		private const string ValueNameMessage = "Message";

		// Token: 0x040000B9 RID: 185
		private const string ValueNameInnerException = "InnerException";

		// Token: 0x040000BA RID: 186
		private static readonly string KeyNameFormatQuarantinedJobRoot = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\MailboxReplicationService\\QuarantinedJobs";

		// Token: 0x040000BB RID: 187
		private static readonly string KeyNameFormatQuarantinedJob = JobQuarantineProvider.KeyNameFormatQuarantinedJobRoot + "\\{0}";

		// Token: 0x040000BC RID: 188
		private static Hookable<IJobQuarantineProvider> hookableInstance = Hookable<IJobQuarantineProvider>.Create(false, new JobQuarantineProvider());
	}
}
