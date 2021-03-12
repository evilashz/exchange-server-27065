using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Security.Principal;
using System.Web.UI;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005B2 RID: 1458
	public class DropDownCommand : Command
	{
		// Token: 0x170025DA RID: 9690
		// (get) Token: 0x060042A2 RID: 17058 RVA: 0x000CADAE File Offset: 0x000C8FAE
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[RefreshProperties(RefreshProperties.All)]
		public CommandCollection Commands
		{
			get
			{
				if (this.commands == null)
				{
					this.HasCommand = true;
					this.commands = new CommandCollection();
				}
				return this.commands;
			}
		}

		// Token: 0x060042A3 RID: 17059 RVA: 0x000CADD0 File Offset: 0x000C8FD0
		[Conditional("DEBUG")]
		private void VerifySubCommandIcon()
		{
			if (this.HasCommand && !this.AllowAddSubCommandIcon)
			{
				foreach (Command command in this.commands)
				{
				}
			}
		}

		// Token: 0x170025DB RID: 9691
		// (get) Token: 0x060042A4 RID: 17060 RVA: 0x000CAE28 File Offset: 0x000C9028
		// (set) Token: 0x060042A5 RID: 17061 RVA: 0x000CAE30 File Offset: 0x000C9030
		[DefaultValue(false)]
		public bool AllowAddSubCommandIcon { get; set; }

		// Token: 0x170025DC RID: 9692
		// (get) Token: 0x060042A6 RID: 17062 RVA: 0x000CAE39 File Offset: 0x000C9039
		// (set) Token: 0x060042A7 RID: 17063 RVA: 0x000CAE41 File Offset: 0x000C9041
		[DefaultValue(false)]
		public bool HasCommand { get; set; }

		// Token: 0x170025DD RID: 9693
		// (get) Token: 0x060042A8 RID: 17064 RVA: 0x000CAE4A File Offset: 0x000C904A
		// (set) Token: 0x060042A9 RID: 17065 RVA: 0x000CAE52 File Offset: 0x000C9052
		public string DefaultCommandName { get; set; }

		// Token: 0x170025DE RID: 9694
		// (get) Token: 0x060042AA RID: 17066 RVA: 0x000CAE5B File Offset: 0x000C905B
		// (set) Token: 0x060042AB RID: 17067 RVA: 0x000CAE63 File Offset: 0x000C9063
		public bool HideArrow { get; set; }

		// Token: 0x060042AC RID: 17068 RVA: 0x000CAE6C File Offset: 0x000C906C
		public Command GetDefaultCommand()
		{
			if (string.IsNullOrEmpty(this.DefaultCommandName))
			{
				return null;
			}
			foreach (Command command in this.Commands)
			{
				if (command.Name == this.DefaultCommandName)
				{
					return command;
				}
			}
			return null;
		}

		// Token: 0x060042AD RID: 17069 RVA: 0x000CAEDC File Offset: 0x000C90DC
		public override bool IsAccessibleToUser(IPrincipal user)
		{
			if (this.HasCommand)
			{
				foreach (Command command in this.Commands)
				{
					if (!(command is SeparatorCommand) && command.IsAccessibleToUser(user))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060042AE RID: 17070 RVA: 0x000CAF44 File Offset: 0x000C9144
		internal void ApplyRolesFilter(IPrincipal user)
		{
			if (!this.rolesFilterApplied)
			{
				if (!this.HasCommand)
				{
					return;
				}
				for (int i = this.Commands.Count - 1; i >= 0; i--)
				{
					Command command = this.Commands[i];
					if (!command.IsAccessibleToUser(user))
					{
						this.Commands.RemoveAt(i);
					}
				}
				for (int j = this.Commands.Count - 1; j >= 0; j--)
				{
					if (this.Commands[j] is SeparatorCommand && (j == this.Commands.Count - 1 || j == 0 || this.Commands[j - 1] is SeparatorCommand))
					{
						this.Commands.RemoveAt(j);
					}
				}
				this.Commands.MakeReadOnly();
				this.rolesFilterApplied = true;
			}
		}

		// Token: 0x04002BC8 RID: 11208
		private CommandCollection commands;

		// Token: 0x04002BC9 RID: 11209
		private bool rolesFilterApplied;
	}
}
