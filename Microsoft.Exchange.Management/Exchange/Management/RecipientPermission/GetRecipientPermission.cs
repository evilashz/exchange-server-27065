using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Management.Automation;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Permission;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Common;

namespace Microsoft.Exchange.Management.RecipientPermission
{
	// Token: 0x0200068E RID: 1678
	[Cmdlet("Get", "RecipientPermission", DefaultParameterSetName = "Identity")]
	public sealed class GetRecipientPermission : GetRecipientObjectTask<RecipientIdParameter, ReducedRecipient>
	{
		// Token: 0x170011B3 RID: 4531
		// (get) Token: 0x06003B66 RID: 15206 RVA: 0x000FCC4B File Offset: 0x000FAE4B
		// (set) Token: 0x06003B67 RID: 15207 RVA: 0x000FCC62 File Offset: 0x000FAE62
		[Parameter]
		public SecurityPrincipalIdParameter Trustee
		{
			get
			{
				return (SecurityPrincipalIdParameter)base.Fields["Trustee"];
			}
			set
			{
				base.Fields["Trustee"] = value;
			}
		}

		// Token: 0x170011B4 RID: 4532
		// (get) Token: 0x06003B68 RID: 15208 RVA: 0x000FCC75 File Offset: 0x000FAE75
		// (set) Token: 0x06003B69 RID: 15209 RVA: 0x000FCC8C File Offset: 0x000FAE8C
		[Parameter]
		public MultiValuedProperty<RecipientAccessRight> AccessRights
		{
			get
			{
				return (MultiValuedProperty<RecipientAccessRight>)base.Fields["AccessRights"];
			}
			set
			{
				base.Fields["AccessRights"] = value;
			}
		}

		// Token: 0x170011B5 RID: 4533
		// (get) Token: 0x06003B6A RID: 15210 RVA: 0x000FCC9F File Offset: 0x000FAE9F
		private new PSCredential Credential
		{
			get
			{
				return base.Credential;
			}
		}

		// Token: 0x170011B6 RID: 4534
		// (get) Token: 0x06003B6B RID: 15211 RVA: 0x000FCCA7 File Offset: 0x000FAEA7
		protected override QueryFilter InternalFilter
		{
			get
			{
				return RecipientIdParameter.GetRecipientTypeFilter(RecipientIdParameter.AllowedRecipientTypes);
			}
		}

		// Token: 0x06003B6C RID: 15212 RVA: 0x000FCCB3 File Offset: 0x000FAEB3
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			if (this.Trustee != null)
			{
				this.trusteeSid = SecurityPrincipalIdParameter.GetUserSid(base.TenantGlobalCatalogSession, this.Trustee, new Task.TaskErrorLoggingDelegate(base.WriteError), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
			}
		}

		// Token: 0x06003B6D RID: 15213 RVA: 0x000FCCF2 File Offset: 0x000FAEF2
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.GetReducedRecipientSession((IRecipientSession)base.CreateSession(), 100, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RecipientPermission\\GetRecipientPermission.cs");
		}

		// Token: 0x06003B6E RID: 15214 RVA: 0x000FCD18 File Offset: 0x000FAF18
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.Identity
			});
			if (this.Identity != null)
			{
				LocalizedString? localizedString;
				IEnumerable<ReducedRecipient> dataObjects = base.GetDataObjects<ReducedRecipient>(this.Identity, base.DataSession, this.RootId, base.OptionalIdentityData, out localizedString);
				EnumerableWrapper<ReducedRecipient> wrapper = EnumerableWrapper<ReducedRecipient>.GetWrapper(dataObjects);
				if (!base.HasErrors && !wrapper.HasElements())
				{
					base.WriteError(new ManagementObjectNotFoundException(localizedString ?? base.GetErrorMessageObjectNotFound(this.Identity.ToString(), typeof(ReducedRecipient).ToString(), (base.DataSession != null) ? base.DataSession.Source : null)), (ErrorCategory)1003, null);
				}
				this.WriteResult<ReducedRecipient>(dataObjects);
			}
			else
			{
				base.InternalProcessRecord();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06003B6F RID: 15215 RVA: 0x000FCDF0 File Offset: 0x000FAFF0
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter();
			IDirectorySession directorySession = (IDirectorySession)base.DataSession;
			if (TaskHelper.ShouldUnderscopeDataSessionToOrganization(directorySession, (ADObject)dataObject))
			{
				directorySession = TaskHelper.UnderscopeSessionToOrganization(directorySession, ((ADObject)dataObject).OrganizationId, true);
			}
			ActiveDirectorySecurity activeDirectorySecurity = PermissionTaskHelper.ReadAdSecurityDescriptor((ADRawEntry)dataObject, directorySession, new Task.TaskErrorLoggingDelegate(base.WriteError));
			AuthorizationRuleCollection accessRules = activeDirectorySecurity.GetAccessRules(true, true, typeof(SecurityIdentifier));
			foreach (object obj in accessRules)
			{
				ActiveDirectoryAccessRule activeDirectoryAccessRule = (ActiveDirectoryAccessRule)obj;
				if (this.Trustee == null || this.trusteeSid == activeDirectoryAccessRule.IdentityReference)
				{
					RecipientAccessRight? recipientAccessRight = this.FilterByRecipientAccessRights(activeDirectoryAccessRule, this.AccessRights);
					if (recipientAccessRight != null)
					{
						string text = string.Empty;
						if (Globals.IsDatacenter && base.TenantGlobalCatalogSession != null)
						{
							try
							{
								SecurityIdentifier sId = (SecurityIdentifier)activeDirectoryAccessRule.IdentityReference;
								ADRecipient adrecipient = base.TenantGlobalCatalogSession.FindBySid(sId);
								if (adrecipient != null)
								{
									text = ((!string.IsNullOrEmpty(adrecipient.DisplayName)) ? adrecipient.DisplayName : adrecipient.Name);
								}
							}
							catch
							{
							}
						}
						if (string.IsNullOrEmpty(text))
						{
							text = RecipientPermissionTaskHelper.GetFriendlyNameOfSecurityIdentifier((SecurityIdentifier)activeDirectoryAccessRule.IdentityReference, base.TenantGlobalCatalogSession, new Task.TaskErrorLoggingDelegate(base.WriteError), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
						}
						RecipientPermission dataObject2 = new RecipientPermission(activeDirectoryAccessRule, ((ADRawEntry)dataObject).Id, text, recipientAccessRight.Value);
						base.WriteResult(dataObject2);
					}
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06003B70 RID: 15216 RVA: 0x000FCFC8 File Offset: 0x000FB1C8
		private RecipientAccessRight? FilterByRecipientAccessRights(ActiveDirectoryAccessRule ace, MultiValuedProperty<RecipientAccessRight> accessRights)
		{
			RecipientAccessRight? recipientAccessRight = RecipientPermissionHelper.GetRecipientAccessRight(ace);
			if (recipientAccessRight == null)
			{
				return null;
			}
			if (accessRights == null)
			{
				return recipientAccessRight;
			}
			if (accessRights.Contains(recipientAccessRight.Value))
			{
				return recipientAccessRight;
			}
			return null;
		}

		// Token: 0x040026C1 RID: 9921
		private SecurityIdentifier trusteeSid;
	}
}
