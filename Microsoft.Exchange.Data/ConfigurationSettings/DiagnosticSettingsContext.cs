using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ConfigurationSettings
{
	// Token: 0x02000200 RID: 512
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DiagnosticSettingsContext : SettingsContextBase
	{
		// Token: 0x060011E9 RID: 4585 RVA: 0x00035F78 File Offset: 0x00034178
		public DiagnosticSettingsContext(IConfigSchema schema, ConfigDiagnosticArgument argument) : base(null)
		{
			this.serverName = argument.GetArgumentOrDefault<string>("servername", null);
			this.serverVersion = DiagnosticSettingsContext.GetServerVersion(argument, "serverversion");
			this.processName = argument.GetArgumentOrDefault<string>("processname", null);
			this.databaseName = argument.GetArgumentOrDefault<string>("dbname", null);
			this.databaseVersion = DiagnosticSettingsContext.GetServerVersion(argument, "dbversion");
			this.organizationName = argument.GetArgumentOrDefault<string>("orgname", null);
			this.organizationVersion = DiagnosticSettingsContext.GetExchangeObjectVersion(argument, "orgversion");
			this.mailboxGuid = argument.GetArgumentOrDefault<Guid?>("mailboxguid", null);
			if (argument.HasArgument("genericscope"))
			{
				this.propertyName = argument.GetArgument<string>("genericscope");
				this.propertyValue = argument.GetArgumentOrDefault<string>("genericscopevalue", null);
				if (!argument.HasArgument("force"))
				{
					this.propertyValue = schema.ParseAndValidateScopeValue(this.propertyName, this.propertyValue);
				}
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x060011EA RID: 4586 RVA: 0x00036076 File Offset: 0x00034276
		public override string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x060011EB RID: 4587 RVA: 0x0003607E File Offset: 0x0003427E
		public override ServerVersion ServerVersion
		{
			get
			{
				return this.serverVersion;
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x060011EC RID: 4588 RVA: 0x00036086 File Offset: 0x00034286
		public override string ProcessName
		{
			get
			{
				return this.processName;
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x060011ED RID: 4589 RVA: 0x0003608E File Offset: 0x0003428E
		public override string DatabaseName
		{
			get
			{
				return this.databaseName;
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x060011EE RID: 4590 RVA: 0x00036096 File Offset: 0x00034296
		public override ServerVersion DatabaseVersion
		{
			get
			{
				return this.databaseVersion;
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x060011EF RID: 4591 RVA: 0x0003609E File Offset: 0x0003429E
		public override string OrganizationName
		{
			get
			{
				return this.organizationName;
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x060011F0 RID: 4592 RVA: 0x000360A6 File Offset: 0x000342A6
		public override ExchangeObjectVersion OrganizationVersion
		{
			get
			{
				return this.organizationVersion;
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x060011F1 RID: 4593 RVA: 0x000360AE File Offset: 0x000342AE
		public override Guid? MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x000360B6 File Offset: 0x000342B6
		public override string GetGenericProperty(string propertyName)
		{
			if (StringComparer.InvariantCultureIgnoreCase.Equals(propertyName, this.propertyName))
			{
				return this.propertyValue;
			}
			return null;
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x000360D4 File Offset: 0x000342D4
		public override XElement GetDiagnosticInfo(string argument)
		{
			XElement diagnosticInfo = base.GetDiagnosticInfo(argument);
			if (!string.IsNullOrEmpty(this.propertyName))
			{
				diagnosticInfo.Add(new XElement(this.propertyName, this.propertyValue));
			}
			return diagnosticInfo;
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x00036113 File Offset: 0x00034313
		private static ServerVersion GetServerVersion(ConfigDiagnosticArgument argument, string argumentName)
		{
			if (!argument.HasArgument(argumentName))
			{
				return null;
			}
			return ServerVersion.ParseFromSerialNumber(argument.GetArgument<string>(argumentName));
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x0003612C File Offset: 0x0003432C
		private static ExchangeObjectVersion GetExchangeObjectVersion(ConfigDiagnosticArgument argument, string argumentName)
		{
			if (!argument.HasArgument(argumentName))
			{
				return null;
			}
			return ExchangeObjectVersion.Parse(argument.GetArgument<string>(argumentName));
		}

		// Token: 0x04000ACE RID: 2766
		private readonly string serverName;

		// Token: 0x04000ACF RID: 2767
		private readonly ServerVersion serverVersion;

		// Token: 0x04000AD0 RID: 2768
		private readonly string processName;

		// Token: 0x04000AD1 RID: 2769
		private readonly string databaseName;

		// Token: 0x04000AD2 RID: 2770
		private readonly ServerVersion databaseVersion;

		// Token: 0x04000AD3 RID: 2771
		private readonly string organizationName;

		// Token: 0x04000AD4 RID: 2772
		private readonly ExchangeObjectVersion organizationVersion;

		// Token: 0x04000AD5 RID: 2773
		private readonly Guid? mailboxGuid;

		// Token: 0x04000AD6 RID: 2774
		private readonly string propertyName;

		// Token: 0x04000AD7 RID: 2775
		private readonly string propertyValue;
	}
}
