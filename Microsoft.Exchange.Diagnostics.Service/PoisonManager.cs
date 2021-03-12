using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Diagnostics.Service.Common;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service
{
	// Token: 0x02000014 RID: 20
	public class PoisonManager
	{
		// Token: 0x06000075 RID: 117 RVA: 0x0000655C File Offset: 0x0000475C
		private PoisonManager()
		{
			this.poisonDirectory = Path.Combine(DiagnosticsService.ServiceLogFolderPath, "Poison");
			Directory.CreateDirectory(this.poisonDirectory);
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00006585 File Offset: 0x00004785
		public static PoisonManager Instance
		{
			get
			{
				return PoisonManager.instance;
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000658C File Offset: 0x0000478C
		public void Initialize()
		{
			FileInfo[] files = new DirectoryInfo(this.poisonDirectory).GetFiles("*.crash");
			Dictionary<string, int> dictionary = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
			Regex regex = new Regex("^(.*?)_", RegexOptions.IgnoreCase | RegexOptions.Compiled);
			foreach (FileInfo fileInfo in files)
			{
				if (fileInfo.CreationTimeUtc < DateTime.UtcNow.Subtract(TimeSpan.FromHours(1.0)))
				{
					fileInfo.Delete();
				}
				else
				{
					Match match = regex.Match(fileInfo.Name);
					if (match.Success)
					{
						string value = match.Groups[1].Value;
						if (dictionary.ContainsKey(value))
						{
							Dictionary<string, int> dictionary2;
							string key;
							(dictionary2 = dictionary)[key = value] = dictionary2[key] + 1;
							if (dictionary[value] == 3)
							{
								this.PoisonJob(value);
							}
						}
						else
						{
							dictionary.Add(value, 1);
						}
					}
				}
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00006688 File Offset: 0x00004888
		public void Crash(string jobName, string message, Exception e)
		{
			Logger.LogEvent(MSExchangeDiagnosticsEventLogConstants.Tuple_JobCrashed, new object[]
			{
				jobName,
				message,
				e.ToString()
			});
			string fileName = string.Format("{0}_{1}.crash", jobName, DateTime.UtcNow.ToString("MMddHHmmssfff"));
			this.WriteFile(fileName, message, e.ToString());
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000066E8 File Offset: 0x000048E8
		public bool IsPoisoned(string jobName)
		{
			string path = string.Format("{0}.poison", jobName);
			return File.Exists(Path.Combine(this.poisonDirectory, path));
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00006714 File Offset: 0x00004914
		private void PoisonJob(string jobName)
		{
			TriggerHandler.Trigger("JobPoisoned", new object[]
			{
				jobName,
				base.GetType().Name
			});
			string fileName = string.Format("{0}.poison", jobName);
			StringBuilder stringBuilder = new StringBuilder();
			FileInfo[] files = new DirectoryInfo(this.poisonDirectory).GetFiles(jobName + "_*.crash");
			foreach (FileInfo fileInfo in files)
			{
				stringBuilder.AppendLine(string.Format("Crash file '{0}' contained the following information: ", fileInfo.FullName));
				using (StreamReader streamReader = new StreamReader(fileInfo.FullName))
				{
					stringBuilder.AppendLine(streamReader.ReadToEnd());
				}
				fileInfo.Delete();
			}
			this.WriteFile(fileName, "Job has been poisoned at: " + DateTime.UtcNow.ToString("MM-dd HH:mm:ss"), stringBuilder.ToString());
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00006814 File Offset: 0x00004A14
		private void WriteFile(string fileName, string message, string content)
		{
			try
			{
				Directory.CreateDirectory(this.poisonDirectory);
				fileName = Path.Combine(this.poisonDirectory, fileName);
				using (FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite, FileShare.None))
				{
					using (StreamWriter streamWriter = new StreamWriter(fileStream))
					{
						streamWriter.WriteLine(message);
						streamWriter.Write(content);
					}
				}
			}
			catch (Exception ex)
			{
				if (!(ex is IOException) && !(ex is UnauthorizedAccessException))
				{
					throw;
				}
			}
		}

		// Token: 0x04000055 RID: 85
		private const string PoisonFileNameFormat = "{0}.poison";

		// Token: 0x04000056 RID: 86
		private static readonly PoisonManager instance = new PoisonManager();

		// Token: 0x04000057 RID: 87
		private readonly string poisonDirectory;
	}
}
