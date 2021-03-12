using System;
using System.Management.Automation;
using System.Security;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000CC0 RID: 3264
	[Cmdlet("Set", "SyncRequest", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetSyncRequest : SetRequest<SyncRequestIdParameter>
	{
		// Token: 0x170026E0 RID: 9952
		// (get) Token: 0x06007D43 RID: 32067 RVA: 0x002002CD File Offset: 0x001FE4CD
		// (set) Token: 0x06007D44 RID: 32068 RVA: 0x002002E4 File Offset: 0x001FE4E4
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		[ValidateNotNull]
		public string RemoteServerName
		{
			get
			{
				return (string)base.Fields["RemoteServerName"];
			}
			set
			{
				base.Fields["RemoteServerName"] = value;
			}
		}

		// Token: 0x170026E1 RID: 9953
		// (get) Token: 0x06007D45 RID: 32069 RVA: 0x002002F7 File Offset: 0x001FE4F7
		// (set) Token: 0x06007D46 RID: 32070 RVA: 0x0020030E File Offset: 0x001FE50E
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public int RemoteServerPort
		{
			get
			{
				return (int)base.Fields["RemoteServerPort"];
			}
			set
			{
				base.Fields["RemoteServerPort"] = value;
			}
		}

		// Token: 0x170026E2 RID: 9954
		// (get) Token: 0x06007D47 RID: 32071 RVA: 0x00200326 File Offset: 0x001FE526
		// (set) Token: 0x06007D48 RID: 32072 RVA: 0x0020033D File Offset: 0x001FE53D
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		[ValidateNotNull]
		public string SmtpServerName
		{
			get
			{
				return (string)base.Fields["SmtpServerName"];
			}
			set
			{
				base.Fields["SmtpServerName"] = value;
			}
		}

		// Token: 0x170026E3 RID: 9955
		// (get) Token: 0x06007D49 RID: 32073 RVA: 0x00200350 File Offset: 0x001FE550
		// (set) Token: 0x06007D4A RID: 32074 RVA: 0x00200367 File Offset: 0x001FE567
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public int SmtpServerPort
		{
			get
			{
				return (int)base.Fields["SmtpServerPort"];
			}
			set
			{
				base.Fields["SmtpServerPort"] = value;
			}
		}

		// Token: 0x170026E4 RID: 9956
		// (get) Token: 0x06007D4B RID: 32075 RVA: 0x0020037F File Offset: 0x001FE57F
		// (set) Token: 0x06007D4C RID: 32076 RVA: 0x00200396 File Offset: 0x001FE596
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		[ValidateNotNull]
		public SecureString Password
		{
			get
			{
				return (SecureString)base.Fields["Password"];
			}
			set
			{
				base.Fields["Password"] = value;
			}
		}

		// Token: 0x170026E5 RID: 9957
		// (get) Token: 0x06007D4D RID: 32077 RVA: 0x002003A9 File Offset: 0x001FE5A9
		// (set) Token: 0x06007D4E RID: 32078 RVA: 0x002003C0 File Offset: 0x001FE5C0
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public AuthenticationMethod Authentication
		{
			get
			{
				return (AuthenticationMethod)base.Fields["Authentication"];
			}
			set
			{
				base.Fields["Authentication"] = value;
			}
		}

		// Token: 0x170026E6 RID: 9958
		// (get) Token: 0x06007D4F RID: 32079 RVA: 0x002003D8 File Offset: 0x001FE5D8
		// (set) Token: 0x06007D50 RID: 32080 RVA: 0x002003EF File Offset: 0x001FE5EF
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public IMAPSecurityMechanism Security
		{
			get
			{
				return (IMAPSecurityMechanism)base.Fields["Security"];
			}
			set
			{
				base.Fields["Security"] = value;
			}
		}

		// Token: 0x170026E7 RID: 9959
		// (get) Token: 0x06007D51 RID: 32081 RVA: 0x00200407 File Offset: 0x001FE607
		// (set) Token: 0x06007D52 RID: 32082 RVA: 0x0020041E File Offset: 0x001FE61E
		[Parameter(Mandatory = false)]
		public DateTime? StartAfter
		{
			get
			{
				return (DateTime?)base.Fields["StartAfter"];
			}
			set
			{
				base.Fields["StartAfter"] = value;
			}
		}

		// Token: 0x170026E8 RID: 9960
		// (get) Token: 0x06007D53 RID: 32083 RVA: 0x00200436 File Offset: 0x001FE636
		// (set) Token: 0x06007D54 RID: 32084 RVA: 0x0020044D File Offset: 0x001FE64D
		[Parameter(Mandatory = false)]
		public DateTime? CompleteAfter
		{
			get
			{
				return (DateTime?)base.Fields["CompleteAfter"];
			}
			set
			{
				base.Fields["CompleteAfter"] = value;
			}
		}

		// Token: 0x170026E9 RID: 9961
		// (get) Token: 0x06007D55 RID: 32085 RVA: 0x00200465 File Offset: 0x001FE665
		// (set) Token: 0x06007D56 RID: 32086 RVA: 0x0020047C File Offset: 0x001FE67C
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public TimeSpan IncrementalSyncInterval
		{
			get
			{
				return (TimeSpan)base.Fields["IncrementalSyncInterval"];
			}
			set
			{
				base.Fields["IncrementalSyncInterval"] = value;
			}
		}

		// Token: 0x170026EA RID: 9962
		// (get) Token: 0x06007D57 RID: 32087 RVA: 0x00200494 File Offset: 0x001FE694
		protected override RequestIndexId DefaultRequestIndexId
		{
			get
			{
				return new RequestIndexId(this.Identity.MailboxId);
			}
		}

		// Token: 0x06007D58 RID: 32088 RVA: 0x002004A6 File Offset: 0x001FE6A6
		protected override IConfigDataProvider CreateSession()
		{
			if (this.Identity != null && this.Identity.OrganizationId != null)
			{
				base.CurrentOrganizationId = this.Identity.OrganizationId;
			}
			return base.CreateSession();
		}

		// Token: 0x06007D59 RID: 32089 RVA: 0x002004DA File Offset: 0x001FE6DA
		protected override void ValidateRequest(TransactionalRequestJob requestJob)
		{
			if (base.IsFieldSet("IncrementalSyncInterval"))
			{
				RequestTaskHelper.ValidateIncrementalSyncInterval(this.IncrementalSyncInterval, new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			base.ValidateRequest(requestJob);
		}

		// Token: 0x06007D5A RID: 32090 RVA: 0x00200508 File Offset: 0x001FE708
		protected override void ModifyRequestInternal(TransactionalRequestJob requestJob, StringBuilder changedValuesTracker)
		{
			if (base.IsFieldSet("RemoteServerName"))
			{
				changedValuesTracker.AppendLine(string.Format("RemoteServerName: {0} -> {1}", requestJob.RemoteHostName, this.RemoteServerName));
				requestJob.RemoteHostName = this.RemoteServerName;
			}
			if (base.IsFieldSet("RemoteServerPort"))
			{
				changedValuesTracker.AppendLine(string.Format("RemoteServerPort: {0} -> {1}", requestJob.RemoteHostPort, this.RemoteServerPort));
				requestJob.RemoteHostPort = this.RemoteServerPort;
			}
			if (base.IsFieldSet("SmtpServerName"))
			{
				changedValuesTracker.AppendLine(string.Format("SmtpServerName: {0} -> {1}", requestJob.SmtpServerName, this.SmtpServerName));
				requestJob.SmtpServerName = this.SmtpServerName;
			}
			if (base.IsFieldSet("SmtpServerPort"))
			{
				changedValuesTracker.AppendLine(string.Format("SmtpServerPort: {0} -> {1}", requestJob.SmtpServerPort, this.SmtpServerPort));
				requestJob.SmtpServerPort = this.SmtpServerPort;
			}
			if (base.IsFieldSet("Authentication"))
			{
				changedValuesTracker.AppendLine(string.Format("Authentication: {0} -> {1}", requestJob.AuthenticationMethod, this.Authentication));
				requestJob.AuthenticationMethod = new AuthenticationMethod?(this.Authentication);
			}
			if (base.IsFieldSet("Security"))
			{
				changedValuesTracker.AppendLine(string.Format("Security: {0} -> {1}", requestJob.SecurityMechanism, this.Security));
				requestJob.SecurityMechanism = this.Security;
			}
			if (base.IsFieldSet("Password"))
			{
				changedValuesTracker.AppendLine("Password: <secure> -> <secure>");
				PSCredential psCred = new PSCredential(requestJob.RemoteCredential.UserName, this.Password);
				requestJob.RemoteCredential = RequestTaskHelper.GetNetworkCredential(psCred, requestJob.AuthenticationMethod);
			}
			if (base.IsFieldSet("IncrementalSyncInterval"))
			{
				changedValuesTracker.AppendLine(string.Format("IncrementalSyncInterval: {0} -> {1}", requestJob.IncrementalSyncInterval, this.IncrementalSyncInterval));
				requestJob.IncrementalSyncInterval = this.IncrementalSyncInterval;
			}
			if (base.IsFieldSet("StartAfter") && !RequestTaskHelper.CompareUtcTimeWithLocalTime(requestJob.TimeTracker.GetTimestamp(RequestJobTimestamp.StartAfter), this.StartAfter))
			{
				RequestTaskHelper.SetStartAfter(this.StartAfter, requestJob, changedValuesTracker);
			}
			if (base.IsFieldSet("CompleteAfter") && !RequestTaskHelper.CompareUtcTimeWithLocalTime(requestJob.TimeTracker.GetTimestamp(RequestJobTimestamp.CompleteAfter), this.CompleteAfter))
			{
				RequestTaskHelper.SetCompleteAfter(this.CompleteAfter, requestJob, changedValuesTracker);
			}
		}

		// Token: 0x04003DCC RID: 15820
		public const string ParameterRemoteServerName = "RemoteServerName";

		// Token: 0x04003DCD RID: 15821
		public const string ParameterRemoteServerPort = "RemoteServerPort";

		// Token: 0x04003DCE RID: 15822
		public const string ParameterSmtpServerName = "SmtpServerName";

		// Token: 0x04003DCF RID: 15823
		public const string ParameterSmtpServerPort = "SmtpServerPort";

		// Token: 0x04003DD0 RID: 15824
		public const string ParameterPassword = "Password";

		// Token: 0x04003DD1 RID: 15825
		public const string ParameterAuthentication = "Authentication";

		// Token: 0x04003DD2 RID: 15826
		public const string ParameterSecurity = "Security";

		// Token: 0x04003DD3 RID: 15827
		public const string ParameterIncrementalSyncInterval = "IncrementalSyncInterval";
	}
}
