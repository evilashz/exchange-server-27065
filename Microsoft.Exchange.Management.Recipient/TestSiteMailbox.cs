using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Management.Automation;
using System.Net;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Security.OAuth;
using Microsoft.SharePoint.Client;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000E9 RID: 233
	[Cmdlet("Test", "SiteMailbox", SupportsShouldProcess = true)]
	public sealed class TestSiteMailbox : TeamMailboxDiagnosticsBase
	{
		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x060011F8 RID: 4600 RVA: 0x000411EC File Offset: 0x0003F3EC
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageTestTeamMailbox((this.Identity != null) ? this.Identity.ToString() : this.SharePointUrl.AbsoluteUri.ToString());
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x060011F9 RID: 4601 RVA: 0x00041218 File Offset: 0x0003F418
		// (set) Token: 0x060011FA RID: 4602 RVA: 0x0004122F File Offset: 0x0003F42F
		[Parameter(Mandatory = false, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		public new RecipientIdParameter Identity
		{
			get
			{
				return (RecipientIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x060011FB RID: 4603 RVA: 0x00041242 File Offset: 0x0003F442
		// (set) Token: 0x060011FC RID: 4604 RVA: 0x00041259 File Offset: 0x0003F459
		[Parameter(Mandatory = false, ParameterSetName = "TeamMailboxITPro")]
		public Uri SharePointUrl
		{
			get
			{
				return (Uri)base.Fields["SharePointUrl"];
			}
			set
			{
				base.Fields["SharePointUrl"] = value;
			}
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x060011FD RID: 4605 RVA: 0x0004126C File Offset: 0x0003F46C
		// (set) Token: 0x060011FE RID: 4606 RVA: 0x00041283 File Offset: 0x0003F483
		[Parameter(Mandatory = false)]
		public RecipientIdParameter RequestorIdentity
		{
			get
			{
				return (RecipientIdParameter)base.Fields["RequestorIdentity"];
			}
			set
			{
				base.Fields["RequestorIdentity"] = value;
			}
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x060011FF RID: 4607 RVA: 0x00041296 File Offset: 0x0003F496
		// (set) Token: 0x06001200 RID: 4608 RVA: 0x000412BC File Offset: 0x0003F4BC
		[Parameter(Mandatory = false)]
		public SwitchParameter UseAppTokenOnly
		{
			get
			{
				return (SwitchParameter)(base.Fields["UseAppTokenOnly"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["UseAppTokenOnly"] = value;
			}
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x000412D4 File Offset: 0x0003F4D4
		protected override void InternalValidate()
		{
			if (this.Identity != null && this.SharePointUrl != null)
			{
				base.WriteError(new InvalidOperationException(Strings.TestTeamMailboxConstraintError("Identity", "SharePointUrl")), ErrorCategory.InvalidOperation, null);
			}
			else if (this.Identity == null && this.SharePointUrl == null)
			{
				base.WriteError(new InvalidOperationException(Strings.TestTeamMailboxConstraintError("Identity", "SharePointUrl")), ErrorCategory.InvalidOperation, null);
			}
			if (this.UseAppTokenOnly && this.RequestorIdentity != null)
			{
				base.WriteError(new InvalidOperationException(Strings.TestTeamMailboxConstraintError("UseAppTokenOnly", "RequestorIdentity")), ErrorCategory.InvalidOperation, null);
			}
			ADObjectId adobjectId = null;
			base.TryGetExecutingUserId(out adobjectId);
			if (this.RequestorIdentity == null)
			{
				if (adobjectId == null)
				{
					base.WriteError(new InvalidOperationException(Strings.CouldNotGetExecutingUser), ErrorCategory.InvalidOperation, null);
				}
				try
				{
					this.requestor = (ADUser)base.GetDataObject(new RecipientIdParameter(adobjectId));
					goto IL_145;
				}
				catch (ManagementObjectNotFoundException)
				{
					if (this.UseAppTokenOnly && base.Organization != null)
					{
						this.requestor = null;
						goto IL_145;
					}
					throw;
				}
			}
			this.requestor = (ADUser)base.GetDataObject(this.RequestorIdentity);
			if (adobjectId != this.requestor.Id)
			{
				this.additionalConstrainedIdentity = this.requestor.Id;
			}
			IL_145:
			if (this.Identity != null)
			{
				base.InternalValidate();
				if (base.TMPrincipals.Count > 1)
				{
					base.WriteError(new InvalidOperationException(Strings.MoreThanOneTeamMailboxes), ErrorCategory.InvalidOperation, null);
				}
				using (Dictionary<ADUser, ExchangePrincipal>.KeyCollection.Enumerator enumerator = base.TMPrincipals.Keys.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						ADUser aduser = enumerator.Current;
						this.tmADObject = aduser;
						if (this.tmADObject.SharePointUrl == null)
						{
							base.WriteError(new InvalidOperationException(Strings.TeamMailboxSharePointUrlMissing), ErrorCategory.InvalidOperation, null);
						}
					}
				}
			}
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x000414E0 File Offset: 0x0003F6E0
		protected override void InternalProcessRecord()
		{
			ValidationResultCollector validationResultCollector = new ValidationResultCollector();
			LocalConfiguration localConfiguration = LocalConfiguration.Load(validationResultCollector);
			foreach (ValidationResultNode sendToPipeline in validationResultCollector.Results)
			{
				base.WriteObject(sendToPipeline);
			}
			SharePointException ex = null;
			Uri uri = this.SharePointUrl ?? this.tmADObject.SharePointUrl;
			OAuthCredentials oauthCredentials = null;
			try
			{
				using (ClientContext clientContext = new ClientContext(uri))
				{
					bool flag = false;
					ICredentials credentialAndConfigureClientContext = TeamMailboxHelper.GetCredentialAndConfigureClientContext(this.requestor, (this.requestor != null) ? this.requestor.OrganizationId : base.CurrentOrganizationId, clientContext, this.UseAppTokenOnly, out flag);
					if (!flag)
					{
						base.WriteError(new InvalidOperationException(Strings.OauthIsTurnedOff), ErrorCategory.InvalidOperation, null);
					}
					oauthCredentials = (credentialAndConfigureClientContext as OAuthCredentials);
					oauthCredentials.Tracer = new TestSiteMailbox.TaskOauthOutboundTracer();
					oauthCredentials.LocalConfiguration = localConfiguration;
					Web web = clientContext.Web;
					clientContext.Load<Web>(web, new Expression<Func<Web, object>>[0]);
					clientContext.ExecuteQuery();
				}
			}
			catch (ClientRequestException e)
			{
				ex = new SharePointException(uri.AbsoluteUri, e);
			}
			catch (ServerException e2)
			{
				ex = new SharePointException(uri.AbsoluteUri, e2);
			}
			catch (IOException ex2)
			{
				ex = new SharePointException(uri.AbsoluteUri, new LocalizedString(ex2.Message));
			}
			catch (WebException e3)
			{
				ex = new SharePointException(uri.AbsoluteUri, e3, true);
			}
			if (ex != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine(Strings.TestTeamMailboxOutboundOauthLog);
				stringBuilder.AppendLine(oauthCredentials.Tracer.ToString());
				stringBuilder.AppendLine(Strings.TestTeamMailboxSharePointResponseDetails);
				stringBuilder.AppendLine(ex.DiagnosticInfo);
				ValidationResultNode sendToPipeline2 = new ValidationResultNode(Strings.TestTeamMailboxSharepointCallUnderOauthTask, new LocalizedString(stringBuilder.ToString()), ResultType.Error);
				base.WriteObject(sendToPipeline2);
				return;
			}
			StringBuilder stringBuilder2 = new StringBuilder();
			stringBuilder2.AppendLine(Strings.TestTeamMailboxSharepointCallUnderOauthSuccess(uri.AbsoluteUri));
			stringBuilder2.AppendLine(Strings.TestTeamMailboxOutboundOauthLog);
			stringBuilder2.AppendLine(oauthCredentials.Tracer.ToString());
			ValidationResultNode sendToPipeline3 = new ValidationResultNode(Strings.TestTeamMailboxSharepointCallUnderOauthTask, new LocalizedString(stringBuilder2.ToString()), ResultType.Success);
			base.WriteObject(sendToPipeline3);
		}

		// Token: 0x04000372 RID: 882
		private ADUser requestor;

		// Token: 0x04000373 RID: 883
		private ADUser tmADObject;

		// Token: 0x020000EA RID: 234
		private sealed class TaskOauthOutboundTracer : IOutboundTracer
		{
			// Token: 0x06001204 RID: 4612 RVA: 0x0004177C File Offset: 0x0003F97C
			public void LogInformation(int hashCode, string formatString, params object[] args)
			{
				this.result.Append("Information:");
				this.result.AppendLine(string.Format(formatString, args));
			}

			// Token: 0x06001205 RID: 4613 RVA: 0x000417A2 File Offset: 0x0003F9A2
			public void LogWarning(int hashCode, string formatString, params object[] args)
			{
				this.result.Append("Warning:");
				this.result.AppendLine(string.Format(formatString, args));
			}

			// Token: 0x06001206 RID: 4614 RVA: 0x000417C8 File Offset: 0x0003F9C8
			public void LogError(int hashCode, string formatString, params object[] args)
			{
				this.result.Append("Error:");
				this.result.AppendLine(string.Format(formatString, args));
			}

			// Token: 0x06001207 RID: 4615 RVA: 0x000417EE File Offset: 0x0003F9EE
			public void LogToken(int hashCode, string tokenString)
			{
				this.result.Append("Token:");
				this.result.AppendLine(tokenString);
			}

			// Token: 0x06001208 RID: 4616 RVA: 0x0004180E File Offset: 0x0003FA0E
			public override string ToString()
			{
				return this.result.ToString();
			}

			// Token: 0x04000374 RID: 884
			private readonly StringBuilder result = new StringBuilder();
		}
	}
}
