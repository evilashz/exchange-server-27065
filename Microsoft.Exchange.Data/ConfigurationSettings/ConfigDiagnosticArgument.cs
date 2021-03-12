using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ConfigurationSettings
{
	// Token: 0x020001F5 RID: 501
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConfigDiagnosticArgument : DiagnosableArgument
	{
		// Token: 0x06001156 RID: 4438 RVA: 0x000347AF File Offset: 0x000329AF
		public ConfigDiagnosticArgument(string argument)
		{
			base.Initialize(argument);
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x000347C0 File Offset: 0x000329C0
		protected override void InitializeSchema(Dictionary<string, Type> schema)
		{
			schema["force"] = typeof(bool);
			schema["invokescan"] = typeof(bool);
			schema["configname"] = typeof(string);
			schema["servername"] = typeof(string);
			schema["serverversion"] = typeof(string);
			schema["processname"] = typeof(string);
			schema["dbname"] = typeof(string);
			schema["dbversion"] = typeof(string);
			schema["orgname"] = typeof(string);
			schema["orgversion"] = typeof(string);
			schema["mailboxguid"] = typeof(Guid);
			schema["genericscope"] = typeof(string);
			schema["genericscopevalue"] = typeof(string);
		}

		// Token: 0x04000AA1 RID: 2721
		public const string InvokeScan = "invokescan";

		// Token: 0x04000AA2 RID: 2722
		public const string ConfigName = "configname";

		// Token: 0x04000AA3 RID: 2723
		public const string ServerName = "servername";

		// Token: 0x04000AA4 RID: 2724
		public const string ServerVersion = "serverversion";

		// Token: 0x04000AA5 RID: 2725
		public const string ProcessName = "processname";

		// Token: 0x04000AA6 RID: 2726
		public const string DatabaseName = "dbname";

		// Token: 0x04000AA7 RID: 2727
		public const string DatabaseVersion = "dbversion";

		// Token: 0x04000AA8 RID: 2728
		public const string OrganizationName = "orgname";

		// Token: 0x04000AA9 RID: 2729
		public const string OrganizationVersion = "orgversion";

		// Token: 0x04000AAA RID: 2730
		public const string MailboxGuid = "mailboxguid";

		// Token: 0x04000AAB RID: 2731
		public const string GenericScope = "genericscope";

		// Token: 0x04000AAC RID: 2732
		public const string GenericScopeValue = "genericscopevalue";

		// Token: 0x04000AAD RID: 2733
		public const string Force = "force";
	}
}
