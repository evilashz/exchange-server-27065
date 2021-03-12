using System;
using System.IO;
using System.Management.Automation;
using System.Reflection;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management
{
	// Token: 0x020000EF RID: 239
	[Cmdlet("Set", "InstallPathInAppConfig")]
	[LocDescription(Strings.IDs.SetInstallPathInAppConfig)]
	public class SetInstallPathInAppConfig : Task
	{
		// Token: 0x06000711 RID: 1809 RVA: 0x0001D180 File Offset: 0x0001B380
		public SetInstallPathInAppConfig()
		{
			TaskLogger.LogEnter();
			this.ExchangeInstallPath = ConfigurationContext.Setup.InstallPath;
			this.ReplacementString = "%ExchangeInstallDir%";
			TaskLogger.LogExit();
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0001D1A8 File Offset: 0x0001B3A8
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			bool flag = base.Fields.Contains("ConfigFileAbsolutePath") && !string.IsNullOrEmpty(this.ConfigFileAbsolutePath);
			bool flag2 = base.Fields.Contains("ConfigFileRelativePath") && !string.IsNullOrEmpty(this.ConfigFileRelativePath);
			if (!flag && !flag2)
			{
				base.WriteError(new ArgumentException(Strings.ErrorMustSpecifyEitherAbsoluteOrRelativePath), ErrorCategory.InvalidArgument, string.Empty);
			}
			if (flag)
			{
				this.ConfigFileFullPath = this.ConfigFileAbsolutePath;
			}
			else
			{
				this.ConfigFileFullPath = Path.Combine(this.ExchangeInstallPath, this.ConfigFileRelativePath);
			}
			string text = Path.Combine(this.ConfigFileFullPath, this.ConfigFileName);
			string text2 = text + ".bak";
			TaskLogger.Trace("ExchangeInstallPath = {0}, Replacement String = {1}, File Path = {2}", new object[]
			{
				this.ExchangeInstallPath,
				this.ReplacementString,
				text
			});
			File.Copy(text, text2, true);
			File.Delete(text);
			using (StreamReader streamReader = new StreamReader(text2, true))
			{
				streamReader.Peek();
				using (StreamWriter streamWriter = new StreamWriter(text, false, streamReader.CurrentEncoding))
				{
					string text3;
					while ((text3 = streamReader.ReadLine()) != null)
					{
						text3 = text3.Replace(this.ReplacementString, this.ExchangeInstallPath);
						streamWriter.WriteLine(text3);
					}
					streamWriter.Flush();
					streamWriter.Close();
				}
				streamReader.Close();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x0001D344 File Offset: 0x0001B544
		// (set) Token: 0x06000714 RID: 1812 RVA: 0x0001D35B File Offset: 0x0001B55B
		[Parameter(Mandatory = false)]
		public string ExchangeInstallPath
		{
			get
			{
				return (string)base.Fields["ExchangeInstallPath"];
			}
			set
			{
				base.Fields["ExchangeInstallPath"] = value;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x0001D36E File Offset: 0x0001B56E
		// (set) Token: 0x06000716 RID: 1814 RVA: 0x0001D385 File Offset: 0x0001B585
		[Parameter(Mandatory = false)]
		public string ReplacementString
		{
			get
			{
				return (string)base.Fields["ReplacementString"];
			}
			set
			{
				base.Fields["ReplacementString"] = value;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x0001D398 File Offset: 0x0001B598
		// (set) Token: 0x06000718 RID: 1816 RVA: 0x0001D3AF File Offset: 0x0001B5AF
		[Parameter(Mandatory = false)]
		public string ConfigFileAbsolutePath
		{
			get
			{
				return (string)base.Fields["ConfigFileAbsolutePath"];
			}
			set
			{
				base.Fields["ConfigFileAbsolutePath"] = value;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x0001D3C2 File Offset: 0x0001B5C2
		// (set) Token: 0x0600071A RID: 1818 RVA: 0x0001D3D9 File Offset: 0x0001B5D9
		[Parameter(Mandatory = false)]
		public string ConfigFileRelativePath
		{
			get
			{
				return (string)base.Fields["ConfigFileRelativePath"];
			}
			set
			{
				base.Fields["ConfigFileRelativePath"] = value;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x0001D3EC File Offset: 0x0001B5EC
		// (set) Token: 0x0600071C RID: 1820 RVA: 0x0001D403 File Offset: 0x0001B603
		[Parameter(Mandatory = true)]
		public string ConfigFileName
		{
			get
			{
				return (string)base.Fields["ConfigFileName"];
			}
			set
			{
				base.Fields["ConfigFileName"] = value;
			}
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0001D418 File Offset: 0x0001B618
		private AssemblyName FindAssemblyName(string assemblyName)
		{
			string[] array = new string[]
			{
				this.ExchangeInstallPath + "bin",
				this.ExchangeInstallPath + "Common"
			};
			foreach (string path in array)
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(path);
				FileInfo[] files = directoryInfo.GetFiles(assemblyName + ".*");
				FileInfo[] array3 = files;
				int j = 0;
				while (j < array3.Length)
				{
					FileInfo fileInfo = array3[j];
					if (fileInfo.Name.EndsWith(".dll"))
					{
						goto IL_95;
					}
					if (fileInfo.Name.EndsWith(".exe"))
					{
						goto Block_2;
					}
					IL_F5:
					j++;
					continue;
					Block_2:
					try
					{
						IL_95:
						TaskLogger.Trace("Found qualify Assembly {0}", new object[]
						{
							fileInfo.FullName
						});
						return AssemblyName.GetAssemblyName(fileInfo.FullName);
					}
					catch (IOException ex)
					{
						TaskLogger.Trace("Fault reflecting assembly {0}, returning null.  Error: ", new object[]
						{
							fileInfo.FullName,
							ex.Message
						});
						return null;
					}
					goto IL_F5;
				}
			}
			TaskLogger.Trace("QualifyAssembly {0} Not Found, returning null.", new object[]
			{
				assemblyName
			});
			return null;
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0001D56C File Offset: 0x0001B76C
		private static string PublicKeyTokenFromFullName(string fullName)
		{
			string text = "PublicKeyToken=";
			return fullName.Substring(fullName.IndexOf(text) + text.Length);
		}

		// Token: 0x04000357 RID: 855
		private const string ConfigFileAbsolutePathKey = "ConfigFileAbsolutePath";

		// Token: 0x04000358 RID: 856
		private const string ConfigFileRelativePathKey = "ConfigFileRelativePath";

		// Token: 0x04000359 RID: 857
		private string ConfigFileFullPath;
	}
}
