using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Text;
using Microsoft.Exchange.Management.SystemManager;
using Microsoft.Exchange.ManagementGUI;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SnapIn.Esm.Toolbox
{
	// Token: 0x0200000B RID: 11
	public class Tool : IComparable<Tool>
	{
		// Token: 0x06000014 RID: 20 RVA: 0x00002948 File Offset: 0x00000B48
		static Tool()
		{
			string[] value = new string[]
			{
				Strings.ConfigTools,
				Strings.DeveloperTools,
				Strings.MailflowTools,
				Strings.PerfTools,
				Strings.SecurityTools,
				Strings.UnifiedMessagingTools
			};
			Tool.basicGroups.AddRange(value);
			Tool.availableGroups.AddRange(value);
			Tool.safeToolsList.Add("Performance Monitor", "ExchPrf.msc");
			Tool.safeToolsList.Add("Queue Viewer", "Exchange Queue Viewer.msc");
			Tool.safeToolsList.Add("Details Templates Editor", "Details Templates Editor.msc");
			Tool.safeToolsList.Add("Public Folder Management Console", "Public Folder Management Console.msc");
			Tool.nonEdgeToolsList.Add("Details Templates Editor");
			Tool.nonEdgeToolsList.Add("Public Folder Management Console");
			Tool.nonEdgeToolsList.Add("Message Tracking");
			Tool.nonEdgeToolsList.Add("Rbac Assignment");
			Tool.nonEdgeToolsList.Add("Call Statistics");
			Tool.nonEdgeToolsList.Add("User Call Logs");
			Tool.cloudAndRemoteOnPremiseToolsList.Add("Message Tracking");
			Tool.cloudAndRemoteOnPremiseToolsList.Add("Rbac Assignment");
			Tool.cloudAndRemoteOnPremiseToolsList.Add("Call Statistics");
			Tool.cloudAndRemoteOnPremiseToolsList.Add("User Call Logs");
			Tool.iconLibrary = new IconLibrary();
			Tool.iconLibrary.Icons.Add("Error", Icons.Error);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002B07 File Offset: 0x00000D07
		public Tool()
		{
			this.errorMessage = new StringBuilder(Strings.ToolErrorHeader);
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002B36 File Offset: 0x00000D36
		public static IconLibrary ToolIcons
		{
			get
			{
				return Tool.iconLibrary;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002B3D File Offset: 0x00000D3D
		public static StringCollection AvailableGroups
		{
			get
			{
				return Tool.availableGroups;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002B44 File Offset: 0x00000D44
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002B4C File Offset: 0x00000D4C
		public string WorkingFolder
		{
			get
			{
				return this.workingFolder;
			}
			set
			{
				this.workingFolder = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002B55 File Offset: 0x00000D55
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002B5D File Offset: 0x00000D5D
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002B66 File Offset: 0x00000D66
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00002B6E File Offset: 0x00000D6E
		public string Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002B77 File Offset: 0x00000D77
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002B7F File Offset: 0x00000D7F
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002B88 File Offset: 0x00000D88
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002B90 File Offset: 0x00000D90
		public string GroupName
		{
			get
			{
				return this.groupName;
			}
			set
			{
				this.groupName = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002B99 File Offset: 0x00000D99
		// (set) Token: 0x06000023 RID: 35 RVA: 0x00002BA1 File Offset: 0x00000DA1
		public int GroupId
		{
			get
			{
				return this.groupId;
			}
			set
			{
				this.groupId = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002BAA File Offset: 0x00000DAA
		// (set) Token: 0x06000025 RID: 37 RVA: 0x00002BB2 File Offset: 0x00000DB2
		public string IconKey
		{
			get
			{
				return this.iconKey;
			}
			set
			{
				this.iconKey = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002BBB File Offset: 0x00000DBB
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00002BC3 File Offset: 0x00000DC3
		public string Assembly
		{
			get
			{
				return this.assembly;
			}
			set
			{
				this.assembly = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002BCC File Offset: 0x00000DCC
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00002BD4 File Offset: 0x00000DD4
		public bool ValidTool
		{
			get
			{
				return this.validTool;
			}
			set
			{
				this.validTool = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002BDD File Offset: 0x00000DDD
		public bool NonEdgeTool
		{
			get
			{
				return this.nonEdgeTool;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002BE5 File Offset: 0x00000DE5
		public bool CloudAndRemoteOnPremiseTool
		{
			get
			{
				return this.cloudAndRemoteOnPremiseTool;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002BED File Offset: 0x00000DED
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage.ToString();
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002BFA File Offset: 0x00000DFA
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00002C02 File Offset: 0x00000E02
		public int LocalizedName
		{
			get
			{
				return this.localizedName;
			}
			set
			{
				this.localizedName = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002C0B File Offset: 0x00000E0B
		// (set) Token: 0x06000030 RID: 48 RVA: 0x00002C13 File Offset: 0x00000E13
		public int LocalizedDescription
		{
			get
			{
				return this.localizedDescription;
			}
			set
			{
				this.localizedDescription = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002C1C File Offset: 0x00000E1C
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002C24 File Offset: 0x00000E24
		public int LocalizedGroupName
		{
			get
			{
				return this.localizedGroupName;
			}
			set
			{
				this.localizedGroupName = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002C2D File Offset: 0x00000E2D
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002C35 File Offset: 0x00000E35
		public string Version
		{
			get
			{
				return this.version;
			}
			set
			{
				this.version = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002C3E File Offset: 0x00000E3E
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002C46 File Offset: 0x00000E46
		public string Command
		{
			get
			{
				return this.command;
			}
			set
			{
				this.command = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002C4F File Offset: 0x00000E4F
		// (set) Token: 0x06000038 RID: 56 RVA: 0x00002C57 File Offset: 0x00000E57
		public string CommandFile
		{
			get
			{
				return this.commandFile;
			}
			set
			{
				this.commandFile = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002C60 File Offset: 0x00000E60
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002C68 File Offset: 0x00000E68
		public string CommandParameters
		{
			get
			{
				return this.commandParameters;
			}
			set
			{
				this.commandParameters = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002C71 File Offset: 0x00000E71
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002C79 File Offset: 0x00000E79
		public string DataSource
		{
			get
			{
				return this.dataSource;
			}
			set
			{
				this.dataSource = value;
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002C84 File Offset: 0x00000E84
		public static bool IsToolDuplicate(DataList<Tool> toolList, Tool tool)
		{
			bool result = false;
			for (int i = 0; i < toolList.Count; i++)
			{
				if (toolList[i].CompareTo(tool) == 0)
				{
					result = true;
					break;
				}
			}
			return result;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002CB8 File Offset: 0x00000EB8
		public void AddErrorMessage(string error)
		{
			this.errorMessage.Append(Strings.ToolboxErrorMessageFormat(error));
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002CD4 File Offset: 0x00000ED4
		public virtual void Initialize()
		{
			if (!this.validTool)
			{
				return;
			}
			if (string.IsNullOrEmpty(this.name))
			{
				this.validTool = false;
				this.AddErrorMessage(Strings.NameError);
			}
			if (string.IsNullOrEmpty(this.type))
			{
				this.validTool = false;
				this.AddErrorMessage(Strings.TypeError);
			}
			else if (string.Compare(this.type, "SnapIn", true) != 0 && string.Compare(this.type, "Executable", true) != 0 && string.Compare(this.type, "MonadScript", true) != 0 && string.Compare(this.type, "DynamicURL", true) != 0 && string.Compare(this.type, "StaticURL", true) != 0)
			{
				this.validTool = false;
				this.AddErrorMessage(Strings.InvalidType(this.type));
			}
			if (string.IsNullOrEmpty(this.command) && string.Compare(this.type, "DynamicURL", true) != 0 && string.IsNullOrEmpty(this.command) && string.Compare(this.type, "StaticURL", true) != 0)
			{
				this.validTool = false;
				this.AddErrorMessage(Strings.CommandError);
			}
			SafeLibraryHandle safeLibraryHandle = new SafeLibraryHandle();
			if (string.IsNullOrEmpty(this.assembly))
			{
				this.validTool = false;
				this.AddErrorMessage(Strings.AssemblyError);
			}
			else
			{
				string text = this.assembly;
				if (!Path.IsPathRooted(this.assembly))
				{
					if (PSConnectionInfoSingleton.GetInstance().Type == OrganizationType.Cloud)
					{
						text = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.GetFileName(text));
					}
					else
					{
						text = Path.Combine(this.workingFolder, this.assembly);
					}
				}
				if (!File.Exists(text))
				{
					this.validTool = false;
					this.AddErrorMessage(Strings.AssemblyMissing(this.assembly));
				}
				else
				{
					if (safeLibraryHandle != null)
					{
						safeLibraryHandle.Dispose();
						safeLibraryHandle = null;
					}
					safeLibraryHandle = SafeLibraryHandle.LoadLibrary(text);
				}
			}
			if (string.Compare(this.type, "MonadScript", true) == 0 || string.Compare(this.type, "SnapIn", true) == 0)
			{
				if (string.IsNullOrEmpty(this.commandFile))
				{
					this.validTool = false;
					this.AddErrorMessage(Strings.CommandFileError(this.name));
				}
				else if (string.Compare(this.type, "SnapIn", true) == 0)
				{
					if (string.Compare(Path.GetFileName(this.command), "mmc.exe", true) != 0)
					{
						this.validTool = false;
						this.AddErrorMessage(Strings.InvalidSnapinTool(this.command));
					}
					else
					{
						string empty = string.Empty;
						if (!Tool.safeToolsList.TryGetValue(this.name, out empty))
						{
							this.validTool = false;
							this.AddErrorMessage(Strings.SnapInNotInSafeList(this.name));
						}
						else if (string.Compare(Path.GetFileName(this.commandFile.Replace("\"", "")), empty, true) != 0)
						{
							this.validTool = false;
							this.AddErrorMessage(Strings.SnapInCommandFileNotInSafeList(this.commandFile, this.name));
						}
					}
				}
				else if (string.Compare(Path.GetFileName(this.command), "PowerShell.exe", true) != 0)
				{
					this.validTool = false;
					this.AddErrorMessage(Strings.InvalidCmdletTool(this.command));
				}
				else
				{
					string empty2 = string.Empty;
					if (!Tool.safeToolsList.TryGetValue(this.name, out empty2))
					{
						this.validTool = false;
						this.AddErrorMessage(Strings.SnapInNotInSafeList(this.name));
					}
					else if (string.Compare(Path.GetFileName(this.commandFile.Replace("\"", "")), empty2, true) != 0)
					{
						this.validTool = false;
						this.AddErrorMessage(Strings.SnapInCommandFileNotInSafeList(this.commandFile, this.name));
					}
				}
			}
			else if (string.Compare(this.type, "Executable", true) == 0)
			{
				if (string.IsNullOrEmpty(this.command))
				{
					this.validTool = false;
					this.AddErrorMessage(Strings.CommandError);
				}
				else
				{
					string empty3 = string.Empty;
					if (!Tool.safeToolsList.TryGetValue(this.name, out empty3))
					{
						this.validTool = false;
						this.AddErrorMessage(Strings.ExecutableNotInSafeList(this.name));
					}
					else if (string.Compare(Path.GetFileName(this.command), empty3, true) != 0)
					{
						this.validTool = false;
						this.AddErrorMessage(Strings.ExecutableCommandNotInSafeList(this.command, this.name));
					}
				}
			}
			try
			{
				this.nonEdgeTool = Tool.nonEdgeToolsList.Contains(this.Name);
				this.cloudAndRemoteOnPremiseTool = Tool.cloudAndRemoteOnPremiseToolsList.Contains(this.Name);
				if (!safeLibraryHandle.IsInvalid)
				{
					if (this.localizedDescription != 0)
					{
						string @string = this.GetString(this.localizedDescription, safeLibraryHandle);
						if (!string.IsNullOrEmpty(@string))
						{
							this.description = @string;
						}
					}
					if (this.localizedGroupName != 0)
					{
						string @string = this.GetString(this.LocalizedGroupName, safeLibraryHandle);
						if (!string.IsNullOrEmpty(@string))
						{
							this.groupName = @string;
						}
					}
					if (this.localizedName != 0)
					{
						string @string = this.GetString(this.LocalizedName, safeLibraryHandle);
						if (!string.IsNullOrEmpty(@string))
						{
							this.name = @string;
						}
					}
				}
				this.LoadIcon(safeLibraryHandle);
			}
			finally
			{
				safeLibraryHandle.Dispose();
			}
			this.UpdateGroup();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000323C File Offset: 0x0000143C
		private string GetString(int resourceId, SafeLibraryHandle moduleHandle)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = NativeMethods.LoadString(moduleHandle, resourceId, stringBuilder, 0);
			if (num != 0)
			{
				stringBuilder.EnsureCapacity(num + 1);
				NativeMethods.LoadString(moduleHandle, resourceId, stringBuilder, stringBuilder.Capacity);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000327C File Offset: 0x0000147C
		private void LoadIcon(SafeLibraryHandle moduleHandle)
		{
			Icon icon = null;
			int value;
			if (!string.IsNullOrEmpty(this.IconKey) && !moduleHandle.IsInvalid && int.TryParse(this.IconKey, out value))
			{
				IntPtr intPtr = NativeMethods.LoadIcon(moduleHandle, new IntPtr(value));
				if (intPtr != IntPtr.Zero)
				{
					icon = Icon.FromHandle(intPtr);
				}
			}
			if (icon != null)
			{
				if (Tool.ToolIcons.Icons[this.name] == null)
				{
					Tool.ToolIcons.Icons.Add(this.name, icon);
				}
				this.IconKey = this.name;
				return;
			}
			if (string.Compare(this.type, "SnapIn", true) == 0)
			{
				this.IconKey = "SnapIn";
				return;
			}
			if (string.Compare(this.type, "MonadScript", true) == 0)
			{
				this.IconKey = "MonadScript";
				return;
			}
			if (string.Compare(this.type, "Executable", true) == 0)
			{
				this.IconKey = "Executable";
				return;
			}
			this.IconKey = "Error";
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003378 File Offset: 0x00001578
		private void UpdateGroup()
		{
			bool flag = false;
			if (this.GroupId == 0)
			{
				if (!string.IsNullOrEmpty(this.GroupName))
				{
					bool flag2 = false;
					for (int i = 0; i < Tool.availableGroups.Count; i++)
					{
						if (string.Compare(Tool.availableGroups[i], this.GroupName, true) == 0)
						{
							flag2 = true;
							this.GroupId = i + 1;
							break;
						}
					}
					if (!flag2)
					{
						Tool.availableGroups.Add(this.GroupName);
						this.GroupId = Tool.availableGroups.IndexOf(this.GroupName) + 1;
					}
					flag = true;
				}
				else
				{
					this.AddErrorMessage(Strings.GroupError);
				}
			}
			else if (Tool.basicGroups.Count >= this.GroupId)
			{
				this.GroupName = Tool.basicGroups[this.GroupId - 1];
				flag = true;
			}
			else
			{
				this.AddErrorMessage(Strings.InvalidGroupId(this.GroupId));
			}
			if (!flag)
			{
				this.validTool = false;
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000346C File Offset: 0x0000166C
		public int CompareTo(Tool item)
		{
			return string.Compare(this.name, item.Name);
		}

		// Token: 0x040001B3 RID: 435
		private static IconLibrary iconLibrary;

		// Token: 0x040001B4 RID: 436
		private static StringCollection basicGroups = new StringCollection();

		// Token: 0x040001B5 RID: 437
		private static StringCollection availableGroups = new StringCollection();

		// Token: 0x040001B6 RID: 438
		private static Dictionary<string, string> safeToolsList = new Dictionary<string, string>();

		// Token: 0x040001B7 RID: 439
		private static StringCollection nonEdgeToolsList = new StringCollection();

		// Token: 0x040001B8 RID: 440
		private static StringCollection cloudAndRemoteOnPremiseToolsList = new StringCollection();

		// Token: 0x040001B9 RID: 441
		private bool nonEdgeTool;

		// Token: 0x040001BA RID: 442
		private bool cloudAndRemoteOnPremiseTool;

		// Token: 0x040001BB RID: 443
		private string name = string.Empty;

		// Token: 0x040001BC RID: 444
		private string description;

		// Token: 0x040001BD RID: 445
		private string groupName;

		// Token: 0x040001BE RID: 446
		private int localizedName;

		// Token: 0x040001BF RID: 447
		private int localizedGroupName;

		// Token: 0x040001C0 RID: 448
		private int localizedDescription;

		// Token: 0x040001C1 RID: 449
		private StringBuilder errorMessage;

		// Token: 0x040001C2 RID: 450
		private string command;

		// Token: 0x040001C3 RID: 451
		private string commandFile;

		// Token: 0x040001C4 RID: 452
		private string commandParameters;

		// Token: 0x040001C5 RID: 453
		private string assembly;

		// Token: 0x040001C6 RID: 454
		private string type;

		// Token: 0x040001C7 RID: 455
		private string workingFolder;

		// Token: 0x040001C8 RID: 456
		private string dataSource;

		// Token: 0x040001C9 RID: 457
		private int groupId;

		// Token: 0x040001CA RID: 458
		private string version;

		// Token: 0x040001CB RID: 459
		private string iconKey;

		// Token: 0x040001CC RID: 460
		private bool validTool = true;
	}
}
