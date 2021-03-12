using System;
using System.Collections;
using System.IO;
using System.Management.Automation;
using System.Security;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020000D4 RID: 212
	[LocDescription(Strings.IDs.InstallCentralAdminServiceTask)]
	[Cmdlet("execute", "sqlcommand")]
	public class ExecuteSqlCommand : ManageSqlDatabase
	{
		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x0001B3A1 File Offset: 0x000195A1
		// (set) Token: 0x06000661 RID: 1633 RVA: 0x0001B3B8 File Offset: 0x000195B8
		[Parameter(Mandatory = true)]
		public string Command
		{
			get
			{
				return (string)base.Fields["Command"];
			}
			set
			{
				base.Fields["Command"] = value;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000662 RID: 1634 RVA: 0x0001B3CB File Offset: 0x000195CB
		// (set) Token: 0x06000663 RID: 1635 RVA: 0x0001B3F1 File Offset: 0x000195F1
		[Parameter(Mandatory = false)]
		public SwitchParameter ExecuteScalar
		{
			get
			{
				return (SwitchParameter)(base.Fields["ExecuteScalar"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ExecuteScalar"] = value;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x0001B409 File Offset: 0x00019609
		// (set) Token: 0x06000665 RID: 1637 RVA: 0x0001B42F File Offset: 0x0001962F
		[Parameter(Mandatory = false)]
		public SwitchParameter ExecuteScript
		{
			get
			{
				return (SwitchParameter)(base.Fields["ExecuteScript"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ExecuteScript"] = value;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000666 RID: 1638 RVA: 0x0001B447 File Offset: 0x00019647
		// (set) Token: 0x06000667 RID: 1639 RVA: 0x0001B467 File Offset: 0x00019667
		[Parameter(Mandatory = false)]
		public Hashtable Arguments
		{
			get
			{
				return (Hashtable)(base.Fields["Arguments"] ?? new Hashtable());
			}
			set
			{
				base.Fields["Arguments"] = value;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x0001B47A File Offset: 0x0001967A
		// (set) Token: 0x06000669 RID: 1641 RVA: 0x0001B49B File Offset: 0x0001969B
		[Parameter(Mandatory = false)]
		[ValidateRange(1, 2147483647)]
		public int Timeout
		{
			get
			{
				return (int)(base.Fields["Timeout"] ?? 0);
			}
			set
			{
				base.Fields["Timeout"] = value;
			}
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0001B4B4 File Offset: 0x000196B4
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (this.ExecuteScript)
			{
				this.ExecuteSqlScript();
			}
			else
			{
				base.ExecuteCommand(this.ApplyArguments(this.Command, this.Arguments), this.ExecuteScalar, this.Timeout);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0001B50C File Offset: 0x0001970C
		private void ExecuteSqlScript()
		{
			StringBuilder stringBuilder = new StringBuilder();
			try
			{
				FileInfo fileInfo = new FileInfo(this.Command);
				using (StreamReader streamReader = fileInfo.OpenText())
				{
					string text;
					while ((text = streamReader.ReadLine()) != null)
					{
						text = text.Trim();
						if (string.Compare(text, "GO", true) == 0)
						{
							base.ExecuteCommand(this.ApplyArguments(stringBuilder.ToString(), this.Arguments), false, this.Timeout);
							stringBuilder.Remove(0, stringBuilder.Length);
						}
						else
						{
							stringBuilder.Append(text);
							stringBuilder.Append(Environment.NewLine);
						}
					}
					base.ExecuteCommand(this.ApplyArguments(stringBuilder.ToString(), this.Arguments), false, this.Timeout);
				}
			}
			catch (SecurityException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidOperation, null);
			}
			catch (FileNotFoundException exception2)
			{
				base.WriteError(exception2, ErrorCategory.InvalidOperation, null);
			}
			catch (UnauthorizedAccessException exception3)
			{
				base.WriteError(exception3, ErrorCategory.InvalidOperation, null);
			}
			catch (DirectoryNotFoundException exception4)
			{
				base.WriteError(exception4, ErrorCategory.InvalidOperation, null);
			}
			catch (IOException exception5)
			{
				base.WriteError(exception5, ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0001B658 File Offset: 0x00019858
		private string ApplyArguments(string command, Hashtable arguments)
		{
			if (command != null && arguments != null && arguments.Count != 0)
			{
				foreach (object obj in arguments.Keys)
				{
					string text = (string)obj;
					if (arguments[text] != null)
					{
						string pattern = string.Format("$({0})", text);
						command = ExecuteSqlCommand.Replace(command, pattern, arguments[text].ToString());
					}
				}
			}
			return command;
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0001B6E4 File Offset: 0x000198E4
		private static string Replace(string command, string pattern, string value)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			for (int num2 = command.IndexOf(pattern, StringComparison.OrdinalIgnoreCase); num2 != -1; num2 = command.IndexOf(pattern, num, StringComparison.OrdinalIgnoreCase))
			{
				stringBuilder.Append(command.Substring(num, num2 - num));
				stringBuilder.Append(value);
				num = num2 + pattern.Length;
			}
			stringBuilder.Append(command.Substring(num));
			return stringBuilder.ToString();
		}
	}
}
