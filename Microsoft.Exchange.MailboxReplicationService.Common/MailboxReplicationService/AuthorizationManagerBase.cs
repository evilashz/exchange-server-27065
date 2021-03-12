using System;
using System.ServiceModel;
using System.Web;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000CC RID: 204
	public abstract class AuthorizationManagerBase : ServiceAuthorizationManager
	{
		// Token: 0x060007CF RID: 1999
		internal abstract AdminRoleDefinition[] ComputeAdminRoles(IRootOrganizationRecipientSession recipientSession, ITopologyConfigurationSession configSession);

		// Token: 0x060007D0 RID: 2000 RVA: 0x0000CC18 File Offset: 0x0000AE18
		internal virtual IAuthenticationInfo Authenticate(OperationContext operationContext)
		{
			ServiceSecurityContext serviceSecurityContext = operationContext.ServiceSecurityContext;
			if (serviceSecurityContext == null || serviceSecurityContext.IsAnonymous)
			{
				return null;
			}
			return new AuthenticationInfo(serviceSecurityContext.WindowsIdentity, serviceSecurityContext.PrimaryIdentity.Name);
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x0000CC50 File Offset: 0x0000AE50
		internal virtual bool PostAuthnCheck(OperationContext operationContext, IAuthenticationInfo authenticationInfo)
		{
			AdminRoleDefinition[] array = this.GetAdminRoles();
			if (array != null)
			{
				foreach (AdminRoleDefinition adminRoleDefinition in array)
				{
					if (authenticationInfo.Sid != null)
					{
						if (authenticationInfo.Sid == adminRoleDefinition.Sid)
						{
							MrsTracer.Authorization.Debug("AuthorizationManagerBase.PostAuthnCheck: client is in '{0}' role, MRS access is granted by Sid.", new object[]
							{
								adminRoleDefinition.RoleName
							});
							return true;
						}
					}
					else if (authenticationInfo.WindowsPrincipal.IsInRole(adminRoleDefinition.Sid))
					{
						MrsTracer.Authorization.Debug("AuthorizationManagerBase.PostAuthnCheck: client is in '{0}' role, MRS access is granted.", new object[]
						{
							adminRoleDefinition.RoleName
						});
						return true;
					}
				}
			}
			MrsTracer.Authorization.Debug("AuthorizationManagerBase.PostAuthnCheck: client is not an Admin, MRS access is denied.", new object[0]);
			return false;
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0000CD30 File Offset: 0x0000AF30
		protected override bool CheckAccessCore(OperationContext operationContext)
		{
			bool result;
			try
			{
				if (operationContext.ServiceSecurityContext == null)
				{
					MrsTracer.Authorization.Debug("AuthorizationManagerBase.CheckAccessCore: operationContext.ServiceSecurityContext was null", new object[0]);
					result = false;
				}
				else
				{
					IAuthenticationInfo authenticationInfo = this.Authenticate(operationContext);
					if (authenticationInfo == null)
					{
						MrsTracer.Authorization.Debug("AuthorizationManagerBase.CheckAccessCore: Client is not authenticated", new object[0]);
						result = false;
					}
					else
					{
						MrsTracer.Authorization.Debug("AuthorizationManagerBase.CheckAccessCore: user '{0}'", new object[]
						{
							authenticationInfo.PrincipalName
						});
						if (!this.PostAuthnCheck(operationContext, authenticationInfo))
						{
							MrsTracer.Authorization.Debug("AuthorizationManagerBase.CheckAccessCore: PostAuthnCheck failed for '{0}'", new object[]
							{
								authenticationInfo.PrincipalName
							});
							result = false;
						}
						else
						{
							if (HttpContext.Current != null && authenticationInfo.WindowsPrincipal != null)
							{
								HttpContext.Current.User = authenticationInfo.WindowsPrincipal;
							}
							result = base.CheckAccessCore(operationContext);
						}
					}
				}
			}
			catch (SystemException ex)
			{
				MrsTracer.Authorization.Error("SystemException in CheckAccessCore:\n{0}\n{1}", new object[]
				{
					CommonUtils.FullExceptionMessage(ex),
					ex.StackTrace
				});
				result = false;
			}
			catch (Exception ex2)
			{
				MrsTracer.Authorization.Error("Unhandled exception in CheckAccessCore:\n{0}\n{1}", new object[]
				{
					CommonUtils.FullExceptionMessage(ex2),
					ex2.StackTrace
				});
				ExWatson.SendReport(ex2);
				throw;
			}
			return result;
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0000CEFC File Offset: 0x0000B0FC
		private AdminRoleDefinition[] GetAdminRoles()
		{
			if (this.adminRoles == null)
			{
				lock (this.adminRolesLock)
				{
					if (this.adminRoles == null)
					{
						CommonUtils.CatchKnownExceptions(delegate
						{
							IRootOrganizationRecipientSession recipientSession = DirectorySessionFactory.Default.CreateRootOrgRecipientSession(true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 241, "GetAdminRoles", "f:\\15.00.1497\\sources\\dev\\mrs\\src\\Common\\AuthorizationManagerBase.cs");
							ITopologyConfigurationSession configSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 246, "GetAdminRoles", "f:\\15.00.1497\\sources\\dev\\mrs\\src\\Common\\AuthorizationManagerBase.cs");
							this.adminRoles = this.ComputeAdminRoles(recipientSession, configSession);
						}, null);
					}
				}
			}
			return this.adminRoles;
		}

		// Token: 0x040004A3 RID: 1187
		private object adminRolesLock = new object();

		// Token: 0x040004A4 RID: 1188
		private AdminRoleDefinition[] adminRoles;
	}
}
