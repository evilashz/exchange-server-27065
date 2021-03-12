using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance.Diagnostics
{
	// Token: 0x02000065 RID: 101
	[DataContract]
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class PowershellCommandDiagnosticData
	{
		// Token: 0x0600037E RID: 894 RVA: 0x0000A797 File Offset: 0x00008997
		protected PowershellCommandDiagnosticData()
		{
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0000A79F File Offset: 0x0000899F
		// (set) Token: 0x06000380 RID: 896 RVA: 0x0000A7A7 File Offset: 0x000089A7
		[DataMember]
		public string CommandName { get; set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0000A7B0 File Offset: 0x000089B0
		// (set) Token: 0x06000382 RID: 898 RVA: 0x0000A7B8 File Offset: 0x000089B8
		[DataMember]
		public KeyValuePair<string, object>[] Parameters { get; set; }

		// Token: 0x06000383 RID: 899 RVA: 0x0000A7D8 File Offset: 0x000089D8
		public static PowershellCommandDiagnosticData[] FromPSCommand(PSCommand command, bool includeParameterValues)
		{
			return (from cmd in command.Commands
			select PowershellCommandDiagnosticData.FromCommand(cmd, includeParameterValues)).ToArray<PowershellCommandDiagnosticData>();
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000A810 File Offset: 0x00008A10
		private static PowershellCommandDiagnosticData FromCommand(Command command, bool includeParameterValues)
		{
			PowershellCommandDiagnosticData powershellCommandDiagnosticData = new PowershellCommandDiagnosticData();
			powershellCommandDiagnosticData.CommandName = command.CommandText;
			IEnumerable<KeyValuePair<string, object>> source = includeParameterValues ? command.Parameters.Select(new Func<CommandParameter, KeyValuePair<string, object>>(PowershellCommandDiagnosticData.CommandParametersWithValues)) : command.Parameters.Select(new Func<CommandParameter, KeyValuePair<string, object>>(PowershellCommandDiagnosticData.CommandParametersWithoutValues));
			powershellCommandDiagnosticData.Parameters = source.ToArray<KeyValuePair<string, object>>();
			return powershellCommandDiagnosticData;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000A870 File Offset: 0x00008A70
		private static KeyValuePair<string, object> CommandParametersWithoutValues(CommandParameter parameter)
		{
			return new KeyValuePair<string, object>(parameter.Name, null);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000A87E File Offset: 0x00008A7E
		private static KeyValuePair<string, object> CommandParametersWithValues(CommandParameter parameter)
		{
			return new KeyValuePair<string, object>(parameter.Name, parameter.Value);
		}
	}
}
